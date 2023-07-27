using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class OperationProcessingDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Equipment_Operation_Processing;
        }
    }

    private int IDOperationProcessing
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


    private string DonViHoatDong
    {
        get
        {
            if (ViewState["DonViHoatDong"] != null)
                return ViewState["DonViHoatDong"].ToString();
            else
                return string.Empty;
        }
        set
        {
            ViewState["DonViHoatDong"] = value;
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
                lblDonViGhiNhanHoatDong.Text = "Số giờ / Số km";
                LoadEquipmentGroup();
                LoadDetail();

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                btnSave.OnClientClick = "Control.Destroy('#" + formatControl.ClientID + "')";

                btnSave.Enabled = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.OperationProcessing_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.OperationProcessing_Edit);
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls", "Control.Build(" + formatControl.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtSoLuong.MaxLength = 10;
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
        int idEquipmentType = 0;
        if (int.TryParse(dlLoaiTrangThietBi.SelectedItem.Value, out idEquipmentType))
            LoadEquipment(idEquipmentType);
    }

    private void LoadEquipment(int idEquipmentType)
    {
        List<tblTrangThietBi> equipList = EquipmentManager.GetEquipmentByIDEquimentType(idEquipmentType);
        tblTrangThietBi emptyItem = new tblTrangThietBi();
        emptyItem.ID = 0;
        emptyItem.Ten = "--- Chọn xe ---";
        equipList.Insert(0, emptyItem);

        dlTrangThietBi.DataSource = equipList;
        dlTrangThietBi.DataTextField = "Ten";
        dlTrangThietBi.DataValueField = "ID";
        dlTrangThietBi.DataBind();

        LoadDonViGhiNhanHoatDong();
    }

    protected void dlTrangThietBi_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDonViGhiNhanHoatDong();
    }

    private void LoadDonViGhiNhanHoatDong()
    {
        try
        {
            int idEquipmentType = 0;
            if (int.TryParse(dlLoaiTrangThietBi.SelectedItem.Value, out idEquipmentType))
            {
                tblLoaiTrangThietBi loaiTrangThietBi = EquipmentTypeManager.GetEquipmentTypeById(idEquipmentType);
                if (loaiTrangThietBi != null && !string.IsNullOrEmpty(loaiTrangThietBi.DonViGhiNhanHoatDong))
                {
                    if (loaiTrangThietBi.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Gio))
                    {
                        lblDonViGhiNhanHoatDong.Text = "Số giờ";
                        DonViHoatDong = DonViGhiNhanHoatDong.Gio;
                    }
                    else if (loaiTrangThietBi.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Km))
                    {
                        lblDonViGhiNhanHoatDong.Text = "Số km";
                        DonViHoatDong = DonViGhiNhanHoatDong.Km;
                    }
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadDetail()
    {
        if (IDOperationProcessing > 0)
        {
            lblAction.Text = "Edit";

            tblNhatKyHoatDong item = OperationProcessingManager.GetOperationProcessingById(IDOperationProcessing);
            if (item != null)
            {
                dlIdTitle.Visible = true;
                ddIdTitle.Visible = true;
                lblID.Text = item.ID.ToString();

                tblTrangThietBi equip = item.tblTrangThietBi;
                if (equip != null)
                {
                    tblLoaiTrangThietBi equipType = equip.tblLoaiTrangThietBi;
                    if (equipType != null && equipType.IDNhomTrangThietBi.HasValue)
                    {
                        dlNhomXe.SelectedValue = equipType.IDNhomTrangThietBi.Value.ToString();
                        LoadEquipmentType();
                        dlLoaiTrangThietBi.SelectedValue = equip.IDLoaiTrangThietBi.ToString();
                        LoadEquipment(equip.IDLoaiTrangThietBi);
                    }
                }

                txtNgayHoatDong.Text = string.Format("{0:dd/MM/yyyy}", item.NgayHoatDong);
                dlTrangThietBi.SelectedValue = item.IDTrangThietBi.ToString();
                if (item.tblTrangThietBi.tblLoaiTrangThietBi.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Gio))
                    txtSoLuong.Text = item.SoGio.ToString();
                else if (item.tblTrangThietBi.tblLoaiTrangThietBi.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Km))
                    txtSoLuong.Text = item.SoKm.ToString();
                txtGhiChu.Text = item.GhiChu;

                LoadDonViGhiNhanHoatDong();

                //Chỉ cho sửa số lượng
                txtNgayHoatDong.Enabled = false;
                dlNhomXe.Enabled = false;
                dlLoaiTrangThietBi.Enabled = false;
                dlTrangThietBi.Enabled = false;
            }
        }
        else
        {
            lblAction.Text = "Add";
            txtNgayHoatDong.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            LoadEquipmentType();
            LoadDonViGhiNhanHoatDong();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
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

            if (dlTrangThietBi.SelectedValue.Equals("0")) 
            {
                ShowMessage(this, "Please select <b>Tên xe</b>", MessageType.Message);
                return;
            }

            DateTime ngayHoatDong = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtNgayHoatDong.Text.Trim()) || txtNgayHoatDong.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Ngày hoạt động</b>", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtNgayHoatDong.Text.Trim(), out ngayHoatDong))
            {
                ShowMessage(this, "Error convert <b>Ngày hoạt động</b> to datetime", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtSoLuong.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Số giờ/ số km</b>", MessageType.Message);
                return;
            }

            double soLuong = 0;
            if (!NumberHelper.ConvertToDouble(txtSoLuong.Text.Trim(), out soLuong))
            {
                ShowMessage(this, "Please enter <b>Số giờ/ số km</b> is a number", MessageType.Message);
                return;
            }

            if (soLuong <= 0)
            {
                ShowMessage(this, "Please enter <b>Số giờ/ số km</b> is greater than 0", MessageType.Message);
                return;
            }

            int idTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);

            if (IDOperationProcessing > 0)
            {
                tblNhatKyHoatDong item = new tblNhatKyHoatDong();
                item.ID = IDOperationProcessing;
                item.IDTrangThietBi = idTrangThietBi;
                item.NgayHoatDong = ngayHoatDong;
                if (DonViHoatDong.Equals(DonViGhiNhanHoatDong.Gio))
                {
                    item.SoGio = soLuong;
                    item.SoKm = null;
                }
                else if (DonViHoatDong.Equals(DonViGhiNhanHoatDong.Km))
                {
                    item.SoGio = null;
                    item.SoKm = soLuong;
                }
                item.GhiChu = txtGhiChu.Text.Trim();
                OperationProcessingManager.UpdateOperationProcessing(item);
            }
            else
            {
                //Kiểm tra chỉ cho phép mỗi ngày nhập một lần
                tblNhatKyHoatDong item = OperationProcessingManager.GetOperationProcessingByIdEquipmentAndDate(idTrangThietBi, ngayHoatDong);
                if (item != null)
                {
                    ShowMessage(this, "Operation processing date of this equipment is exists. Please find and edit from list.", MessageType.Message);
                    return;
                }

                tblNhatKyHoatDong newItem = new tblNhatKyHoatDong();
                newItem.IDTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);
                newItem.NgayHoatDong = ngayHoatDong;
                if (DonViHoatDong.Equals(DonViGhiNhanHoatDong.Gio))
                {
                    newItem.SoGio = soLuong;
                    newItem.SoKm = null;
                }
                else if (DonViHoatDong.Equals(DonViGhiNhanHoatDong.Km))
                {
                    newItem.SoGio = null;
                    newItem.SoKm = soLuong;
                }
                newItem.GhiChu = txtGhiChu.Text.Trim();
                OperationProcessingManager.InsertOperationProcessing(newItem);
            }
            
            Response.Redirect("OperationProcessing.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}