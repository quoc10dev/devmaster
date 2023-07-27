using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class MaintenanceRequestDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_Request;
        }
    }

    private int IDMaintenanceRequest
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

    private string StateOfRequest
    {
        get
        {
            if (ViewState["StateOfRequest"] != null)
                return ViewState["StateOfRequest"].ToString();
            else
                return MaintenanceRequestState.NhapMoi;
        }
        set
        {
            ViewState["StateOfRequest"] = value;
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
                lblSoGioHoacKm.Text = "Số giờ (hoặc km) hoạt động";

                LoadStateOfMaintenanceRequest();
                LoadEquipmentGroup();
                LoadDetail();

                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Edit);

                btnUpload.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Edit);

                btnDeleteAttachFile.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Edit);

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                btnSave.OnClientClick = "Control.Destroy('#" + formatControl.ClientID + "')";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls",
                                                   "if(typeof(Control)!='undefined') Control.Build(" + formatControl.ClientID + ")", true);
            gvMaintenanceRequest.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadStateOfMaintenanceRequest()
    {
        dlState.Items.Clear();
        dlState.Items.Add(new ListItem(MaintenanceRequestValue.NhapMoi, MaintenanceRequestState.NhapMoi));
        dlState.Items.Add(new ListItem(MaintenanceRequestValue.HoanThanh, MaintenanceRequestState.HoanThanh));
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
            dlCapBaoDuong.Items.Clear();
            dlTrangThietBi.Items.Clear();

            int idEquipmentType = 0;
            if (int.TryParse(dlLoaiTrangThietBi.SelectedItem.Value, out idEquipmentType))
            {
                //Load trang thiết bị
                LoadEquipment(idEquipmentType);

                //Load cấp bảo dưỡng
                LoadMaintenanceLevel(idEquipmentType);

                //Load các hạng mục tương ứng theo loại TTB
                LoadMaintenanceList(idEquipmentType);
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadMaintenanceList(int idEquipmentType)
    {
        DataSet ds = MaintenanceRequestManager.GetMaintenanceFromEqupmentType(idEquipmentType);

        //Tiêu đề cho các cột cấp bảo dưỡng
        DataTable dtCapBaoDuong = ds.Tables[1];
        int numberLevel = dtCapBaoDuong.Rows.Count;
        int maxNumber = 1; //tối đa hiển thị 4 cột cấp bảo dưỡng
        for (int i = 0; i < numberLevel; i++)
        {
            if (maxNumber <= 4)
            {
                gvMaintenanceRequest.Columns[maxNumber + 1].HeaderText = dtCapBaoDuong.Rows[i]["Ten"].ToString();
                maxNumber++;
            }
        }

        gvMaintenanceRequest.DataSource = ds.Tables[0];
        gvMaintenanceRequest.DataBind();
        gvMaintenanceRequest.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    private void LoadMaintenanceDetail(int idMaintenance)
    {
        DataSet ds = MaintenanceRequestManager.GetMaintenanceDetail(idMaintenance);

        //Tiêu đề cho các cột cấp bảo dưỡng
        DataTable dtCapBaoDuong = ds.Tables[1];
        int numberLevel = dtCapBaoDuong.Rows.Count;
        int maxNumber = 1; //tối đa hiển thị 4 cột cấp bảo dưỡng
        for (int i = 0; i < numberLevel; i++)
        {
            if (maxNumber <= 4)
            {
                gvMaintenanceRequest.Columns[maxNumber + 1].HeaderText = dtCapBaoDuong.Rows[i]["Ten"].ToString();
                maxNumber++;
            }
        }

        gvMaintenanceRequest.DataSource = ds.Tables[0];
        gvMaintenanceRequest.DataBind();
        gvMaintenanceRequest.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            tblLoaiBaoDuong loaiBaoDuong = equipmentType.tblLoaiBaoDuong;
            if (loaiBaoDuong != null)
            {
                ICollection<tblCapBaoDuong> capBaoDuongColl = loaiBaoDuong.tblCapBaoDuongs;
                List<tblCapBaoDuong> capBaoDuongList = capBaoDuongColl.ToList<tblCapBaoDuong>();

                tblCapBaoDuong emptyItem = new tblCapBaoDuong();
                emptyItem.ID = 0;
                emptyItem.Ten = "--- Chọn cấp bảo dưỡng ---";
                capBaoDuongList.Insert(0, emptyItem);

                dlCapBaoDuong.DataSource = capBaoDuongList;
                dlCapBaoDuong.DataTextField = "Ten";
                dlCapBaoDuong.DataValueField = "ID";
                dlCapBaoDuong.DataBind();
            }

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
                lblSoGioHoacSoKm.Text = "1000 (giờ)";
            }
        }
    }

    private void EnableInfoRequest(bool enable)
    {
        txtNgayLapPhieu.Enabled = enable;
        dlNhomXe.Enabled = enable;
        dlLoaiTrangThietBi.Enabled = enable;
        dlTrangThietBi.Enabled = enable;
        dlCapBaoDuong.Enabled = enable;
        txtNguoiThucHien.Enabled = enable;

        foreach (GridViewRow row in gvMaintenanceRequest.Rows)
        {
            CheckBox chkIsChecked = row.FindControl("chkIsChecked") as CheckBox;
            if (chkIsChecked != null)
                chkIsChecked.Enabled = enable;

            CheckBox chkIsRequiresRepair = row.FindControl("chkIsRequiresRepair") as CheckBox;
            if (chkIsRequiresRepair != null)
                chkIsRequiresRepair.Enabled = enable;
        }
    }

    private void LoadDetail()
    {
        if (IDMaintenanceRequest > 0)
        {
            lblAction.Text = "Edit";
            tblTheBaoDuong maintenanceBill = MaintenanceRequestManager.GetMaintenanceRequestById(IDMaintenanceRequest);
            if (maintenanceBill != null)
            {
                upUploadFile.Visible = true;
                if (!string.IsNullOrEmpty(maintenanceBill.PathOfFileUpload))
                    ltrContentDialog.Text = string.Format("Are you sure you want to delete <b>{0}</b> ?", maintenanceBill.PathOfFileUpload);

                //Load thông tin thẻ bảo dưỡng
                tblTrangThietBi equipment = maintenanceBill.tblTrangThietBi;
                if (equipment != null)
                {
                    //Loại xe, tên xe, cấp bảo dưỡng
                    tblLoaiTrangThietBi equipType = equipment.tblLoaiTrangThietBi;
                    if (equipType != null && equipType.IDNhomTrangThietBi.HasValue)
                    {
                        dlNhomXe.SelectedValue = equipType.IDNhomTrangThietBi.Value.ToString();
                        LoadEquipmentType();
                        dlLoaiTrangThietBi.SelectedValue = equipment.IDLoaiTrangThietBi.ToString();
                        LoadEquipment(equipType.ID);
                        dlTrangThietBi.SelectedValue = equipment.ID.ToString();
                        LoadMaintenanceLevel(equipType.ID);
                        dlCapBaoDuong.SelectedValue = maintenanceBill.IDCapBaoDuong.ToString();
                    }

                    lblMaThe.Text = maintenanceBill.MaThe;
                    lblBienSo.Text = equipment.BienSo;
                    lblSoMay.Text = equipment.SoMay;
                    lblModel.Text = equipment.LoaiMay;
                    lblSerialNo.Text = equipment.SoKhung;
                    lblSoGioHoacSoKm.Text = "1000 (giờ)";

                    if (maintenanceBill.SoGioHoacKm.HasValue)
                        txtSoGioHoacKm.Text = maintenanceBill.SoGioHoacKm.Value.ToString();

                    dlState.SelectedValue = maintenanceBill.TrangThaiKhaiThac;
                    txtNgayLapPhieu.Text = string.Format("{0:dd/MM/yyyy}", maintenanceBill.NgayBaoDuong);
                    txtNguoiThucHien.Text = maintenanceBill.NguoiBaoDuong;

                    //File đính kèm
                    if (!string.IsNullOrEmpty(maintenanceBill.PathOfFileUpload))
                        lblCurrentFile.Text = string.Format("Current file: {0}", maintenanceBill.PathOfFileUpload);
                    else
                        lblCurrentFile.Text = string.Format("Current file: No file");
                }

                //Load chi tiết thẻ bảo dưỡng
                LoadMaintenanceDetail(maintenanceBill.ID);

                //Nếu trạng thái "Đã hoàn thành" thì không cho sửa các thông tin 
                StateOfRequest = maintenanceBill.TrangThaiKhaiThac;
                if (StateOfRequest.Equals(MaintenanceRequestState.HoanThanh))
                    EnableInfoRequest(false);
                else
                    EnableInfoRequest(true);

                //Không cho sửa nhóm, loại, tên xe
                dlNhomXe.Enabled = false;
                dlLoaiTrangThietBi.Enabled = false;
                dlTrangThietBi.Enabled = false;
                dlCapBaoDuong.Enabled = false;

                btnPrint.Visible = true;
                //btnPrintAcceptanceForm.Visible = true;
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

                    //Load các hạng mục tương ứng theo loại TTB
                    LoadMaintenanceList(idEquipmentType);

                    //Ẩn các cột IsChecked, IsRequiresRepair
                    gvMaintenanceRequest.Columns[6].Visible = false; //IsChecked
                    gvMaintenanceRequest.Columns[7].Visible = false; //IsRequiresRepair
                }
            }
            dlState.SelectedValue = MaintenanceRequestState.NhapMoi;
            dlState.Enabled = false;

            upUploadFile.Visible = false;
            btnPrint.Visible = false;
            //btnPrintAcceptanceForm.Visible = false;
        }
    }

    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    HiddenField hfIDHangMuc = e.Row.FindControl("hfIDHangMuc") as HiddenField;
                    if (hfIDHangMuc != null)
                    {
                        if (row["IDHangMuc"] != DBNull.Value)
                            hfIDHangMuc.Value = row["IDHangMuc"].ToString();
                        else
                        {
                            //Ẩn các checkbox thuộc hạng mục cha
                            CheckBox chkIsChecked = e.Row.FindControl("chkIsChecked") as CheckBox;
                            if (chkIsChecked != null)
                                chkIsChecked.Visible = false;

                            CheckBox chkIsRequiresRepair = e.Row.FindControl("chkIsRequiresRepair") as CheckBox;
                            if (chkIsRequiresRepair != null)
                                chkIsRequiresRepair.Visible = false;
                        }
                    }

                    Label lblSTT = e.Row.FindControl("lblSTT") as Label;
                    if (lblSTT != null && row["STT"] != DBNull.Value)
                        lblSTT.Text = row["STT"].ToString();

                    Label lblTenHangMuc = e.Row.FindControl("lblTenHangMuc") as Label;
                    if (lblTenHangMuc != null)
                        lblTenHangMuc.Text = row["Ten"].ToString();

                    if (Convert.ToBoolean(row["IsBold"]))
                    {
                        lblSTT.Font.Bold = true;
                        lblTenHangMuc.Font.Bold = true;
                    }

                    Label lblTaskListLevel1 = e.Row.FindControl("lblTaskListLevel1") as Label;
                    if (lblTaskListLevel1 != null && row["Level1"] != DBNull.Value)
                        lblTaskListLevel1.Text = row["Level1"].ToString();

                    Label lblTaskListLevel2 = e.Row.FindControl("lblTaskListLevel2") as Label;
                    if (lblTaskListLevel2 != null && row["Level2"] != DBNull.Value)
                        lblTaskListLevel2.Text = row["Level2"].ToString();

                    Label lblTaskListLevel3 = e.Row.FindControl("lblTaskListLevel3") as Label;
                    if (lblTaskListLevel3 != null && row["Level3"] != DBNull.Value)
                        lblTaskListLevel3.Text = row["Level3"].ToString();

                    Label lblTaskListLevel4 = e.Row.FindControl("lblTaskListLevel4") as Label;
                    if (lblTaskListLevel4 != null && row["Level4"] != DBNull.Value)
                        lblTaskListLevel4.Text = row["Level4"].ToString();

                    if (IDMaintenanceRequest == 0)
                    {
                        HiddenField hfIdTaskLevel1 = e.Row.FindControl("hfIdTaskLevel1") as HiddenField;
                        if (hfIdTaskLevel1 != null && row["IdTaskLevel1"] != DBNull.Value)
                            hfIdTaskLevel1.Value = row["IdTaskLevel1"].ToString();

                        HiddenField hfIdTaskLevel2 = e.Row.FindControl("hfIdTaskLevel2") as HiddenField;
                        if (hfIdTaskLevel2 != null && row["IdTaskLevel2"] != DBNull.Value)
                            hfIdTaskLevel2.Value = row["IdTaskLevel2"].ToString();

                        HiddenField hfIdTaskLevel3 = e.Row.FindControl("hfIdTaskLevel3") as HiddenField;
                        if (hfIdTaskLevel3 != null && row["IdTaskLevel3"] != DBNull.Value)
                            hfIdTaskLevel3.Value = row["IdTaskLevel3"].ToString();

                        HiddenField hfIdTaskLevel4 = e.Row.FindControl("hfIdTaskLevel4") as HiddenField;
                        if (hfIdTaskLevel4 != null && row["IdTaskLevel4"] != DBNull.Value)
                            hfIdTaskLevel4.Value = row["IdTaskLevel4"].ToString();

                        HiddenField hfIdMaintenanceLevel1 = e.Row.FindControl("hfIdMaintenanceLevel1") as HiddenField;
                        if (hfIdMaintenanceLevel1 != null && row["IdMaintenanceLevel1"] != DBNull.Value)
                            hfIdMaintenanceLevel1.Value = row["IdMaintenanceLevel1"].ToString();

                        HiddenField hfIdMaintenanceLevel2 = e.Row.FindControl("hfIdMaintenanceLevel2") as HiddenField;
                        if (hfIdMaintenanceLevel2 != null && row["IdMaintenanceLevel2"] != DBNull.Value)
                            hfIdMaintenanceLevel2.Value = row["IdMaintenanceLevel2"].ToString();

                        HiddenField hfIdMaintenanceLevel3 = e.Row.FindControl("hfIdMaintenanceLevel3") as HiddenField;
                        if (hfIdMaintenanceLevel3 != null && row["IdMaintenanceLevel3"] != DBNull.Value)
                            hfIdMaintenanceLevel3.Value = row["IdMaintenanceLevel3"].ToString();

                        HiddenField hfIdMaintenanceLevel4 = e.Row.FindControl("hfIdMaintenanceLevel4") as HiddenField;
                        if (hfIdMaintenanceLevel4 != null && row["IdMaintenanceLevel4"] != DBNull.Value)
                            hfIdMaintenanceLevel4.Value = row["IdMaintenanceLevel4"].ToString();
                    }
                    else
                    {
                        HiddenField hfChiTietBaoDuong = e.Row.FindControl("hfChiTietBaoDuong") as HiddenField;
                        if (hfChiTietBaoDuong != null && row["IDChiTietBaoDuong"] != DBNull.Value)
                            hfChiTietBaoDuong.Value = row["IDChiTietBaoDuong"].ToString();

                        CheckBox chkIsChecked1 = e.Row.FindControl("chkIsChecked") as CheckBox;
                        if (chkIsChecked1 != null && row["IsChecked"] != DBNull.Value)
                            chkIsChecked1.Checked = Convert.ToBoolean(row["IsChecked"]);

                        CheckBox chkIsRequiresRepair1 = e.Row.FindControl("chkIsRequiresRepair") as CheckBox;
                        if (chkIsRequiresRepair1 != null && row["IsRequiresRepair"] != DBNull.Value)
                            chkIsRequiresRepair1.Checked = Convert.ToBoolean(row["IsRequiresRepair"]);
                    }
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDMaintenanceRequest > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

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

            int idCapBaoDuong = int.Parse(dlCapBaoDuong.SelectedValue);
            if (idCapBaoDuong == 0)
            {
                ShowMessage(this, "Please enter <b>Cấp bảo dưỡng</b>", MessageType.Message);
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

            //Nếu trạng thái "Đã hoàn thành" thì bắt buộc phải nhập Người thực hiện
            if (dlState.SelectedValue.Equals(MaintenanceRequestState.HoanThanh) && string.IsNullOrEmpty(txtNguoiThucHien.Text))
            {
                ShowMessage(this, "Please enter <b>Người thực hiện</b>", MessageType.Message);
                return;
            }

            if (IDMaintenanceRequest == 0)
            {
                #region Insert

                //Thẻ bảo dưỡng
                tblTheBaoDuong maintenanceBill = new tblTheBaoDuong();
                maintenanceBill.IDTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue); ;
                maintenanceBill.IDHuHong = 1;
                maintenanceBill.IDCapBaoDuong = idCapBaoDuong;
                maintenanceBill.NgayBaoDuong = ngayLapPhieu;
                maintenanceBill.NguoiBaoDuong = txtNguoiThucHien.Text.Trim();
                if (soGioHoacKm > 0)
                    maintenanceBill.SoGioHoacKm = soGioHoacKm;
                else
                    maintenanceBill.SoGioHoacKm = null;

                maintenanceBill.NgayNhap = DateTime.Now;
                maintenanceBill.NguoiNhap = (LoginHelper.User != null) ? LoginHelper.User.UserName : string.Empty;
                maintenanceBill.TrangThaiKhaiThac = MaintenanceRequestState.NhapMoi;
                maintenanceBill = MaintenanceRequestManager.InsertMaintenanceRequest(maintenanceBill);

                //Hạng mục bảo dưỡng
                foreach (GridViewRow row in gvMaintenanceRequest.Rows)
                {
                    HiddenField hfIDHangMuc = row.FindControl("hfIDHangMuc") as HiddenField;
                    if (hfIDHangMuc != null && !string.IsNullOrEmpty(hfIDHangMuc.Value))
                    {
                        int idHangMucBaoDuong = int.Parse(hfIDHangMuc.Value);
                        tblChiTietBaoDuong maintenanceDetail = new tblChiTietBaoDuong();
                        maintenanceDetail.IDTheBaoDuong = maintenanceBill.ID;
                        maintenanceDetail.IDHangMucBaoDuong = idHangMucBaoDuong;
                        maintenanceDetail.GhiChu = string.Empty;
                        maintenanceDetail = MaintenanceRequestManager.InsertMaintenanceDetail(maintenanceDetail);

                        //Công việc cấp 1
                        HiddenField hfIdTaskLevel1 = row.FindControl("hfIdTaskLevel1") as HiddenField;
                        HiddenField hfIdMaintenanceLevel1 = row.FindControl("hfIdMaintenanceLevel1") as HiddenField;
                        if (hfIdTaskLevel1 != null && hfIdMaintenanceLevel1 != null &&
                            !string.IsNullOrEmpty(hfIdTaskLevel1.Value) && !string.IsNullOrEmpty(hfIdMaintenanceLevel1.Value))
                        {
                            string[] strIdTaskList = hfIdTaskLevel1.Value.Split(',');
                            if (strIdTaskList.Length > 0)
                            {
                                for (int i = 0; i < strIdTaskList.Length; i++)
                                {
                                    if (strIdTaskList[i] != string.Empty)
                                    {
                                        tblChiTietBaoDuongCongViec maintenanceTask = new tblChiTietBaoDuongCongViec();
                                        maintenanceTask.IDChiTietBaoDuong = maintenanceDetail.ID;
                                        maintenanceTask.IDCongViec = Convert.ToInt32(strIdTaskList[i]);
                                        maintenanceTask.IDCapBaoDuong = int.Parse(hfIdMaintenanceLevel1.Value);
                                        MaintenanceRequestManager.InsertMaintenanceTask(maintenanceTask);
                                    }
                                }
                            }
                        }

                        //Công việc cấp 2
                        HiddenField hfIdTaskLevel2 = row.FindControl("hfIdTaskLevel2") as HiddenField;
                        HiddenField hfIdMaintenanceLevel2 = row.FindControl("hfIdMaintenanceLevel2") as HiddenField;
                        if (hfIdTaskLevel2 != null && hfIdMaintenanceLevel2 != null &&
                            !string.IsNullOrEmpty(hfIdTaskLevel2.Value) && !string.IsNullOrEmpty(hfIdMaintenanceLevel2.Value))
                        {
                            string[] strIdTaskList = hfIdTaskLevel2.Value.Split(',');
                            if (strIdTaskList.Length > 0)
                            {
                                for (int i = 0; i < strIdTaskList.Length; i++)
                                {
                                    if (strIdTaskList[i] != string.Empty)
                                    {
                                        tblChiTietBaoDuongCongViec maintenanceTask = new tblChiTietBaoDuongCongViec();
                                        maintenanceTask.IDChiTietBaoDuong = maintenanceDetail.ID;
                                        maintenanceTask.IDCongViec = Convert.ToInt32(strIdTaskList[i]);
                                        maintenanceTask.IDCapBaoDuong = int.Parse(hfIdMaintenanceLevel2.Value);
                                        MaintenanceRequestManager.InsertMaintenanceTask(maintenanceTask);
                                    }
                                }
                            }
                        }

                        //Công việc cấp 3
                        HiddenField hfIdTaskLevel3 = row.FindControl("hfIdTaskLevel3") as HiddenField;
                        HiddenField hfIdMaintenanceLevel3 = row.FindControl("hfIdMaintenanceLevel3") as HiddenField;
                        if (hfIdTaskLevel3 != null && hfIdMaintenanceLevel3 != null &&
                            !string.IsNullOrEmpty(hfIdTaskLevel3.Value) && !string.IsNullOrEmpty(hfIdMaintenanceLevel3.Value))
                        {
                            string[] strIdTaskList = hfIdTaskLevel3.Value.Split(',');
                            if (strIdTaskList.Length > 0)
                            {
                                for (int i = 0; i < strIdTaskList.Length; i++)
                                {
                                    if (strIdTaskList[i] != string.Empty)
                                    {
                                        tblChiTietBaoDuongCongViec maintenanceTask = new tblChiTietBaoDuongCongViec();
                                        maintenanceTask.IDChiTietBaoDuong = maintenanceDetail.ID;
                                        maintenanceTask.IDCongViec = Convert.ToInt32(strIdTaskList[i]);
                                        maintenanceTask.IDCapBaoDuong = int.Parse(hfIdMaintenanceLevel3.Value);
                                        MaintenanceRequestManager.InsertMaintenanceTask(maintenanceTask);
                                    }
                                }
                            }
                        }


                        //Công việc cấp 4
                        HiddenField hfIdTaskLevel4 = row.FindControl("hfIdTaskLevel4") as HiddenField;
                        HiddenField hfIdMaintenanceLevel4 = row.FindControl("hfIdMaintenanceLevel4") as HiddenField;
                        if (hfIdTaskLevel4 != null && hfIdMaintenanceLevel4 != null &&
                            !string.IsNullOrEmpty(hfIdTaskLevel4.Value) && !string.IsNullOrEmpty(hfIdMaintenanceLevel4.Value))
                        {
                            string[] strIdTaskList = hfIdTaskLevel4.Value.Split(',');
                            if (strIdTaskList.Length > 0)
                            {
                                for (int i = 0; i < strIdTaskList.Length; i++)
                                {
                                    if (strIdTaskList[i] != string.Empty)
                                    {
                                        tblChiTietBaoDuongCongViec maintenanceTask = new tblChiTietBaoDuongCongViec();
                                        maintenanceTask.IDChiTietBaoDuong = maintenanceDetail.ID;
                                        maintenanceTask.IDCongViec = Convert.ToInt32(strIdTaskList[i]);
                                        maintenanceTask.IDCapBaoDuong = int.Parse(hfIdMaintenanceLevel4.Value);
                                        MaintenanceRequestManager.InsertMaintenanceTask(maintenanceTask);
                                    }
                                }
                            }
                        }
                    }
                }

                //Cập nhật thông tin bảo dưỡng mới nhất (Ngày bảo dưỡng, Số km/ Số giờ) vào bảng tblTrangThietBi
                MaintenanceRequestManager.CapNhatThongTinBaoDuong(maintenanceBill.ID);

                Response.Redirect(string.Format("MaintenanceRequestDetail.aspx?ID={0}", maintenanceBill.ID.ToString()));

                #endregion
            }
            else
            {
                #region Update

                //Thẻ bảo dưỡng
                tblTheBaoDuong maintenanceBill = MaintenanceRequestManager.GetMaintenanceRequestById(IDMaintenanceRequest);
                if (maintenanceBill != null)
                {
                    maintenanceBill.IDTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);
                    maintenanceBill.IDHuHong = 1;
                    maintenanceBill.IDCapBaoDuong = idCapBaoDuong;
                    maintenanceBill.NgayBaoDuong = ngayLapPhieu;
                    maintenanceBill.NguoiBaoDuong = txtNguoiThucHien.Text.Trim();
                    maintenanceBill.TrangThaiKhaiThac = dlState.SelectedValue;

                    if (soGioHoacKm > 0)
                        maintenanceBill.SoGioHoacKm = soGioHoacKm;
                    else
                        maintenanceBill.SoGioHoacKm = null;

                    maintenanceBill.NgaySua = DateTime.Now;
                    maintenanceBill.NguoiSua = (LoginHelper.User != null) ? LoginHelper.User.UserName : string.Empty;
                    MaintenanceRequestManager.UpdateMaintenanceRequest(maintenanceBill);

                    //Hạng mục bảo dưỡng
                    foreach (GridViewRow row in gvMaintenanceRequest.Rows)
                    {
                        HiddenField hfChiTietBaoDuong = row.FindControl("hfChiTietBaoDuong") as HiddenField;
                        if (hfChiTietBaoDuong != null && !string.IsNullOrEmpty(hfChiTietBaoDuong.Value))
                        {
                            int idChiTietBaoDuong = int.Parse(hfChiTietBaoDuong.Value);

                            CheckBox chkIsChecked = row.FindControl("chkIsChecked") as CheckBox;
                            CheckBox chkIsRequiresRepair = row.FindControl("chkIsRequiresRepair") as CheckBox;
                            if (chkIsChecked != null && chkIsRequiresRepair != null)
                            {
                                tblChiTietBaoDuong maintenanceDetail = MaintenanceRequestManager.GetMaintenanceDetailById(idChiTietBaoDuong);
                                if (maintenanceDetail != null)
                                {
                                    maintenanceDetail.IsChecked = chkIsChecked.Checked;
                                    maintenanceDetail.IsRequiresRepair = chkIsRequiresRepair.Checked;
                                    MaintenanceRequestManager.UpdateMaintenanceDetail(maintenanceDetail);
                                }
                            }
                        }
                    }

                    //Cập nhật thông tin bảo dưỡng (Ngày bảo dưỡng, Số km/ Số giờ) vào bảng tblTrangThietBi
                    MaintenanceRequestManager.CapNhatThongTinBaoDuong(maintenanceBill.ID);
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
        string url = string.Format("RequestForm.aspx?ID={0}", IDMaintenanceRequest);
        string fullURL = "window.open('" + url + "', '_blank',);";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void btnPrintAcceptanceForm_Click(object sender, EventArgs e)
    {
        string url = string.Format("AcceptanceForm.aspx?ID={0}", IDMaintenanceRequest);
        string fullURL = "window.open('" + url + "', '_blank',);";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    [System.Web.Services.WebMethod]
    public static string GetData()
    {
        //lblMaintennanceName.Text = "Web method";
        return string.Empty;
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

                    tblTheBaoDuong maintenanceBill = MaintenanceRequestManager.GetMaintenanceRequestById(IDMaintenanceRequest);
                    if (maintenanceBill != null)
                    {
                        //Xóa file cũ
                        string oldFileName = string.Format("Upload/Maintenance/{0}", maintenanceBill.PathOfFileUpload);
                        string oldPath = Server.MapPath(oldFileName);
                        if (File.Exists(oldPath))
                            File.Delete(oldPath);

                        //Upload file mới
                        string fullPath = string.Empty;
                        fullPath = Server.MapPath("~/Upload/Maintenance/") + string.Format("{0}-{1}", maintenanceBill.MaThe, filename);
                        fileUpload.SaveAs(fullPath);

                        //Update tên file xuống database
                        maintenanceBill.PathOfFileUpload = string.Format("{0}-{1}", maintenanceBill.MaThe, filename);
                        MaintenanceRequestManager.UpdateMaintenanceRequest(maintenanceBill);
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
            tblTheBaoDuong maintenanceBill = MaintenanceRequestManager.GetMaintenanceRequestById(IDMaintenanceRequest);
            if (maintenanceBill != null)
            {
                if (!string.IsNullOrEmpty(maintenanceBill.PathOfFileUpload))
                {
                    string fileName = string.Format("Upload/Maintenance/{0}", maintenanceBill.PathOfFileUpload);
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
        tblTheBaoDuong maintenanceBill = MaintenanceRequestManager.GetMaintenanceRequestById(IDMaintenanceRequest);
        if (maintenanceBill != null)
        {
            if (!string.IsNullOrEmpty(maintenanceBill.PathOfFileUpload))
            {
                string fileName = string.Format("Upload/Maintenance/{0}", maintenanceBill.PathOfFileUpload);
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

            tblTheBaoDuong maintenanceBill = MaintenanceRequestManager.GetMaintenanceRequestById(IDMaintenanceRequest);
            if (maintenanceBill != null)
            {
                string fileName = string.Format("Upload/Maintenance/{0}", maintenanceBill.PathOfFileUpload);
                string path = Server.MapPath(fileName);
                if (File.Exists(path))
                {
                    File.Delete(path); //xóa file trong thư mục upload
                    maintenanceBill.PathOfFileUpload = string.Empty;
                    MaintenanceRequestManager.UpdateMaintenanceRequest(maintenanceBill);
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