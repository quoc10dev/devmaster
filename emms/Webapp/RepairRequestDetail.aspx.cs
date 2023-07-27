using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class RepairRequestDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Repair_Request;
        }
    }

    private int IDRepairRequest
    {
        get
        {
            if (ViewState["ID"] != null)
                return Convert.ToInt32(ViewState["ID"]);
            else if (Request.QueryString.AllKeys.Contains("ID"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    int result = 0;
                    int.TryParse(Request.QueryString["ID"], out result);
                    ViewState["ID"] = result;
                    return result;
                }
                else
                    return 0;
            }
            else
                return 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MasterPage.FunctionPageCode = FunctionPageCode;
                SetMaxLength();

                LoadEquipmentGroup();
                LoadDetail();

                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Edit);

                btnUpload.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Edit);

                btnDeleteAttachFile.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Add) ||
                                  FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Edit);

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                btnSave.OnClientClick = "Control.Destroy('#" + formatControl.ClientID + "')";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls",
                                                   "if(typeof(Control)!='undefined') Control.Build(" + formatControl.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtNguoiThucHien.MaxLength = 200;
        txtSoGioHoacKm.MaxLength = 10;
    }

    private void LoadEquipmentGroup()
    {
        List<tblNhomTrangThietBi> equipGroupList = EquipmentGroupManager.GetAllEquipmentGroup();
        tblNhomTrangThietBi emptyItem = new tblNhomTrangThietBi();
        emptyItem.ID = 0;
        emptyItem.TenNhom = "--- Chọn nhóm xe ---";
        equipGroupList.Insert(0, emptyItem);

        dlNhomXe.DataSource = equipGroupList;
        dlNhomXe.DataTextField = "TenNhom";
        dlNhomXe.DataValueField = "ID";
        dlNhomXe.DataBind();
    }

    protected void dlNhomXe_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEquipmentType();
    }

    private void LoadEquipmentType()
    {
        try
        {
            //Load loại trang thiết bị
            int idEquipmentGroup = 0;
            if (int.TryParse(dlNhomXe.SelectedItem.Value, out idEquipmentGroup))
                EquipmentTypeManager.LoadEquipmentType(dlLoaiTrangThietBi, idEquipmentGroup);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void dlLoaiTrangThietBi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dlTrangThietBi.Items.Clear();

            int idEquipmentType = 0;
            if (int.TryParse(dlLoaiTrangThietBi.SelectedItem.Value, out idEquipmentType))
            {
                //Load trang thiết bị
                LoadEquipment(idEquipmentType);

                //Load cấp bảo dưỡng
                LoadMaintenanceLevel(idEquipmentType);
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadEquipment(int idEquipmentType)
    {
        List<tblTrangThietBi> equipmentList = EquipmentManager.GetEquipmentByIDEquimentType(idEquipmentType);
        tblTrangThietBi emptyItem = new tblTrangThietBi();
        emptyItem.ID = 0;
        emptyItem.Ten = "--- Chọn xe ---";
        equipmentList.Insert(0, emptyItem);

        dlTrangThietBi.DataSource = equipmentList;
        dlTrangThietBi.DataTextField = "Ten";
        dlTrangThietBi.DataValueField = "ID";
        dlTrangThietBi.DataBind();
    }

    private void LoadMaintenanceLevel(int idEquipmentType)
    {
        tblLoaiTrangThietBi equipmentType = EquipmentTypeManager.GetEquipmentTypeById(idEquipmentType);
        if (equipmentType != null)
        {
            //Hiển thị số giờ hoặc km
            if (equipmentType.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Gio))
                lblSoGioHoacKm.Text = "Số giờ hoạt động";
            else if (equipmentType.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Km))
                lblSoGioHoacKm.Text = "Số km hoạt động";
        }
    }

    protected void dlTrangThietBi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(dlTrangThietBi.SelectedValue))
        {
            tblTrangThietBi equipment = EquipmentManager.GetEquipmentById(int.Parse(dlTrangThietBi.SelectedValue));
            if (equipment != null)
            {
                lblBienSo.Text = equipment.BienSo;
                lblSoMay.Text = equipment.SoMay;
                lblModel.Text = equipment.LoaiMay;
                lblSerialNo.Text = equipment.SoKhung;
            }
        }
    }

    private void EnableInfoRequest(bool enable)
    {
        txtNgayLapPhieu.Enabled = enable;
        dlNhomXe.Enabled = enable;
        dlLoaiTrangThietBi.Enabled = enable;
        dlTrangThietBi.Enabled = enable;
        txtNguoiThucHien.Enabled = enable;
    }

    private void LoadDetail()
    {
        if (IDRepairRequest > 0)
        {
            lblAction.Text = "Edit";
            tblPhieuSuaChua repairRequest = RepairRequestManager.GetRepairRequestById(IDRepairRequest);
            if (repairRequest != null)
            {
                upUploadFile.Visible = true;
                if (!string.IsNullOrEmpty(repairRequest.PathOfFileUpload))
                    ltrContentDialog.Text = string.Format("Are you sure you want to delete <b>{0}</b> ?", repairRequest.PathOfFileUpload);

                //Load thông tin phiếu sửa chữa
                tblTrangThietBi equipment = repairRequest.tblTrangThietBi;
                if (equipment != null)
                {
                    //Loại xe, tên xe
                    tblLoaiTrangThietBi equipType = equipment.tblLoaiTrangThietBi;
                    if (equipType != null && equipType.IDNhomTrangThietBi.HasValue)
                    {
                        dlNhomXe.SelectedValue = equipType.IDNhomTrangThietBi.Value.ToString();
                        LoadEquipmentType();
                        dlLoaiTrangThietBi.SelectedValue = equipment.IDLoaiTrangThietBi.ToString();
                        LoadEquipment(equipType.ID);
                        dlTrangThietBi.SelectedValue = equipment.ID.ToString();
                        LoadMaintenanceLevel(equipType.ID);
                    }

                    lblMaThe.Text = repairRequest.MaPhieu;
                    lblBienSo.Text = equipment.BienSo;
                    lblSoMay.Text = equipment.SoMay;
                    lblModel.Text = equipment.LoaiMay;
                    lblSerialNo.Text = equipment.SoKhung;

                    if (repairRequest.SoGioHoacKm.HasValue)
                        txtSoGioHoacKm.Text = repairRequest.SoGioHoacKm.Value.ToString();

                    txtNgayLapPhieu.Text = string.Format("{0:dd/MM/yyyy}", repairRequest.NgaySuaChua);
                    txtNguoiThucHien.Text = repairRequest.NguoiSuaChua;

                    //File đính kèm
                    if (!string.IsNullOrEmpty(repairRequest.PathOfFileUpload))
                        lblCurrentFile.Text = string.Format("Current file: {0}", repairRequest.PathOfFileUpload);
                    else
                        lblCurrentFile.Text = string.Format("Current file: No file");
                }

                //Không cho sửa nhóm, loại, tên xe
                dlNhomXe.Enabled = false;
                dlLoaiTrangThietBi.Enabled = false;
                dlTrangThietBi.Enabled = false;

                btnPrint.Visible = true;
            }
            else
                upUploadFile.Visible = false;
        }
        else
        {
            lblAction.Text = "Add";
            LoadEquipmentType();
            if (!string.IsNullOrEmpty(dlLoaiTrangThietBi.SelectedValue))
            {
                int idEquipmentType = 0;
                if (int.TryParse(dlLoaiTrangThietBi.SelectedItem.Value, out idEquipmentType))
                {
                    LoadEquipment(int.Parse(dlLoaiTrangThietBi.SelectedValue));
                }
            }

            upUploadFile.Visible = false;
            btnPrint.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime ngayLapPhieu = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtNgayLapPhieu.Text.Trim()) || txtNgayLapPhieu.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Ngày lập phiếu</b>", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtNgayLapPhieu.Text.Trim(), out ngayLapPhieu))
            {
                ShowMessage(this, "Error convert <b>Ngày lập phiếu</b> to datetime", MessageType.Message);
                return;
            }

            if (dlNhomXe.SelectedValue.Equals("0"))
            {
                ShowMessage(this, "Please select <b>Nhóm xe</b>", MessageType.Message);
                return;
            }

            if (dlLoaiTrangThietBi.SelectedValue.Equals("0"))
            {
                ShowMessage(this, "Please select <b>Loại xe</b>", MessageType.Message);
                return;
            }

            int idTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);
            if (idTrangThietBi == 0)
            {
                ShowMessage(this, "Please enter <b>Tên xe</b>", MessageType.Message);
                return;
            }

            double soGioHoacKm = 0;
            if (!string.IsNullOrEmpty(txtSoGioHoacKm.Text.Trim()))
            {
                if (!NumberHelper.ConvertToDouble(txtSoGioHoacKm.Text.Trim(), out soGioHoacKm))
                {
                    ShowMessage(this, "Please enter <b>Số giờ hoạt động hoặc số km</b> is a number", MessageType.Message);
                    return;
                }

                if (soGioHoacKm <= 0)
                {
                    ShowMessage(this, "Please enter <b>Số giờ hoạt động hoặc số km</b> is greater than 0", MessageType.Message);
                    return;
                }
            }

            if (IDRepairRequest == 0)
            {
                #region Insert

                //Phiếu sửa chữa
                tblPhieuSuaChua repairRequest = new tblPhieuSuaChua();
                repairRequest.IDTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue); ;
                repairRequest.NgaySuaChua = ngayLapPhieu;
                repairRequest.NguoiSuaChua = txtNguoiThucHien.Text.Trim();
                if (soGioHoacKm > 0)
                    repairRequest.SoGioHoacKm = soGioHoacKm;
                else
                    repairRequest.SoGioHoacKm = null;

                repairRequest.NgayNhap = DateTime.Now;
                repairRequest.NguoiNhap = (LoginHelper.User != null) ? LoginHelper.User.UserName : string.Empty;
                repairRequest = RepairRequestManager.InsertRepairRequest(repairRequest);

                Response.Redirect(string.Format("RepairRequestDetail.aspx?ID={0}", repairRequest.ID.ToString()));

                #endregion
            }
            else
            {
                #region Update

                //Phiếu sửa chữa
                tblPhieuSuaChua repairRequest = RepairRequestManager.GetRepairRequestById(IDRepairRequest);
                if (repairRequest != null)
                {
                    repairRequest.IDTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);
                    repairRequest.NgaySuaChua = ngayLapPhieu;
                    repairRequest.NguoiSuaChua = txtNguoiThucHien.Text.Trim();

                    if (soGioHoacKm > 0)
                        repairRequest.SoGioHoacKm = soGioHoacKm;
                    else
                        repairRequest.SoGioHoacKm = null;

                    repairRequest.NgaySua = DateTime.Now;
                    repairRequest.NguoiSua = (LoginHelper.User != null) ? LoginHelper.User.UserName : string.Empty;
                    RepairRequestManager.UpdateRepairRequest(repairRequest);
                }
                LoadDetail();
                ShowMessage(this, "Your data has been successfully saved.", MessageType.Message);

                #endregion
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = string.Format("RepairRequestForm.aspx?ID={0}", IDRepairRequest);
        string fullURL = "window.open('" + url + "', '_blank',);";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (!fileUpload.HasFile)
                ShowMessage(this, "Please select a file to upload", MessageType.Message);
            else
            {
                //Kiểm tra không được trùng tên file
                string filename = Path.GetFileName(fileUpload.FileName);
                if (Path.GetExtension(fileUpload.FileName).ToLower().Equals(".pdf"))
                {
                    if (fileUpload.PostedFile.ContentLength > 10485760)
                    {
                        // Cấm các file lớn hơn 10 MB.
                        ShowMessage(this, "File đính kèm quá lớn. Vui lòng nhập file có dung lượng không vượt quá 10 MB.", MessageType.Message);
                        return;
                    }

                    tblPhieuSuaChua repairRequest = RepairRequestManager.GetRepairRequestById(IDRepairRequest);
                    if (repairRequest != null)
                    {
                        //Xóa file cũ
                        string oldFileName = string.Format("Upload/Repair/{0}", repairRequest.PathOfFileUpload);
                        string oldPath = Server.MapPath(oldFileName);
                        if (File.Exists(oldPath))
                            File.Delete(oldPath);

                        //Upload file mới
                        string fullPath = string.Empty;
                        fullPath = Server.MapPath("~/Upload/Repair/") + string.Format("{0}-{1}", repairRequest.MaPhieu, filename);
                        fileUpload.SaveAs(fullPath);

                        //Update tên file xuống database
                        repairRequest.PathOfFileUpload = string.Format("{0}-{1}", repairRequest.MaPhieu, filename);
                        RepairRequestManager.UpdateRepairRequest(repairRequest);
                    }

                    LoadDetail();
                    ShowMessage(this, "The file has been uploaded.", MessageType.Message);
                }
                else
                    ShowMessage(this, "Please select a pdf file to upload.", MessageType.Message);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(this, ex.Message, MessageType.Message);
        }
    }

    protected void btnViewFile_Click(object sender, EventArgs e)
    {
        try
        {
            tblPhieuSuaChua repairRequest = RepairRequestManager.GetRepairRequestById(IDRepairRequest);
            if (repairRequest != null)
            {
                if (!string.IsNullOrEmpty(repairRequest.PathOfFileUpload))
                {
                    string fileName = string.Format("Upload/Repair/{0}", repairRequest.PathOfFileUpload);
                    string path = Server.MapPath(fileName);
                    if (!File.Exists(path))
                    {
                        ShowMessage(this, "File not found.", MessageType.Message);
                        return;
                    }

                    string url = string.Format("ViewPdf.aspx?Path={0}", fileName);
                    string fullURL = "window.open('" + url + "', '_blank',);";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                }
                else
                    ShowMessage(this, "File not found. Please upload a pdf file.", MessageType.Message);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(this, ex.Message, MessageType.Message);
        }
    }

    protected void btnCheckBeforeDelete_Click(object sender, EventArgs e)
    {
        tblPhieuSuaChua repairRequest = RepairRequestManager.GetRepairRequestById(IDRepairRequest);
        if (repairRequest != null)
        {
            if (!string.IsNullOrEmpty(repairRequest.PathOfFileUpload))
            {
                string fileName = string.Format("Upload/Repair/{0}", repairRequest.PathOfFileUpload);
                string path = Server.MapPath(fileName);
                if (File.Exists(path))
                {
                    //Mở popup thông báo xóa
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#modalDelete').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowModalScript", sb.ToString(), false);
                }
                else
                    ShowMessage(this, "Can't delete file. The file does not exists.", MessageType.Message);
            }
            else
                ShowMessage(this, "Can't delete file. The file does not exists.", MessageType.Message);
        }
    }

    protected void btnDeleteAttachFile_Click(object sender, EventArgs e)
    {
        try
        {
            //Đóng popup thông báo xóa
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalDelete').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeletyModalScript", sb.ToString(), false);

            tblPhieuSuaChua repairRequest = RepairRequestManager.GetRepairRequestById(IDRepairRequest);
            if (repairRequest != null)
            {
                string fileName = string.Format("Upload/Repair/{0}", repairRequest.PathOfFileUpload);
                string path = Server.MapPath(fileName);
                if (File.Exists(path))
                {
                    File.Delete(path); //xóa file trong thư mục upload
                    repairRequest.PathOfFileUpload = string.Empty;
                    RepairRequestManager.UpdateRepairRequest(repairRequest);
                    LoadDetail();
                    ShowMessage(this, "Deleted successfully.", MessageType.Message);
                }
                else
                    ShowMessage(this, "Can't delete file. The file does not exists.", MessageType.Message);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(this, ex.Message, MessageType.Message);
        }
    }
}