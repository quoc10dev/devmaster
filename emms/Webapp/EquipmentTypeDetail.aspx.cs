using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class EquipmentTypeDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Equipment_Type_List;
        }
    }

    private int IDEquipmentType
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
                LoadMaintenanceType();
                LoadLoaiBaoDuong();
                LoadDetail();
                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_Edit);

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
        txtName.MaxLength = 300;
        txtNumberDisplay.MaxLength = 5;
    }

    private void LoadEquipmentGroup()
    {
        List<tblNhomTrangThietBi> equipGroupList = EquipmentGroupManager.GetAllEquipmentGroup();
        tblNhomTrangThietBi emptyItem = new tblNhomTrangThietBi();
        emptyItem.ID = 0;
        emptyItem.TenNhom = "--- Chọn nhóm xe ---";
        equipGroupList.Insert(0, emptyItem);

        dlEquipGroup.DataSource = equipGroupList;
        dlEquipGroup.DataTextField = "TenNhom";
        dlEquipGroup.DataValueField = "ID";
        dlEquipGroup.DataBind();
    }

    private void LoadMaintenanceType()
    {
        dlKieuBaoDuong.DataSource = MaintenanceTypeManager.GetAllMaintenanceType();
        dlKieuBaoDuong.DataTextField = "Ten";
        dlKieuBaoDuong.DataValueField = "ID";
        dlKieuBaoDuong.DataBind();
    }

    protected void dlKieuBaoDuong_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadLoaiBaoDuong();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadLoaiBaoDuong()
    {
        dlLoaiBaoDuong.Items.Clear();
        int idKieuBaoDuong = 0;
        int.TryParse(dlKieuBaoDuong.SelectedValue, out idKieuBaoDuong);

        dlLoaiBaoDuong.DataSource = MaintenanceTypeManager.GetMaintenanceChildTypeByIdKieuBaoDuong(idKieuBaoDuong);
        dlLoaiBaoDuong.DataTextField = "Ten";
        dlLoaiBaoDuong.DataValueField = "IDLoaiBaoDuong";
        dlLoaiBaoDuong.DataBind();
    }

    private void LoadDetail()
    {
        if (IDEquipmentType > 0)
        {
            lblAction.Text = "Edit";
            
            tblLoaiTrangThietBi item = EquipmentTypeManager.GetEquipmentTypeById(IDEquipmentType);
            if (item != null)
            {
                txtName.Text = item.Ten;
                txtNameEng.Text = item.TenEng;
                txtNumberDisplay.Text = item.SoThuTuHienThi.ToString();
                txtNote.Text = item.GhiChu;

                if (!string.IsNullOrEmpty(item.DonViGhiNhanHoatDong))
                {
                    if (item.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Gio))
                        rdGio.Checked = true;
                    else
                        rdKm.Checked = true;
                }

                if (item.IDLoaiBaoDuong.HasValue)
                {
                    dlKieuBaoDuong.SelectedValue = item.tblLoaiBaoDuong.IDKieuBaoDuong.ToString();
                    LoadLoaiBaoDuong();
                    dlLoaiBaoDuong.SelectedValue = item.IDLoaiBaoDuong.Value.ToString();
                }

                if (item.IDNhomTrangThietBi.HasValue)
                    dlEquipGroup.SelectedValue = item.IDNhomTrangThietBi.Value.ToString();
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDEquipmentType > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(dlEquipGroup.SelectedValue) && dlEquipGroup.SelectedValue.Equals("0")) 
            {
                ShowMessage(this, "Please select <b>Nhóm trang thiết bị</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Equment type name (Vi)</b>", MessageType.Message);
                return;
            }
            
            if (!rdGio.Checked && !rdKm.Checked)
            {
                ShowMessage(this, "Please select <b>Đơn vị ghi nhận hoạt động</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(dlKieuBaoDuong.SelectedValue))
            {
                ShowMessage(this, "Please select <b>Loại bảo dưỡng</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(dlKieuBaoDuong.SelectedValue))
            {
                ShowMessage(this, "Please select <b>Loại bảo dưỡng</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtNumberDisplay.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Number display</b>", MessageType.Message);
                return;
            }

            short numberDisplay = 0;
            if (!NumberHelper.ConvertToShort(txtNumberDisplay.Text.Trim(), out numberDisplay))
            {
                ShowMessage(this, "Please enter <b>Number display</b> is a number", MessageType.Message);
                return;
            }

            if (numberDisplay <= 0)
            {
                ShowMessage(this, "Please enter <b>Number display</b> is greater than 0", MessageType.Message);
                return;
            }

            if (IDEquipmentType > 0)
            {
                tblLoaiTrangThietBi item = new tblLoaiTrangThietBi();
                item.ID = IDEquipmentType;
                item.Ten = txtName.Text.Trim();
                item.TenEng = txtNameEng.Text.Trim();
                item.SoThuTuHienThi = numberDisplay;
                item.GhiChu = txtNote.Text.Trim();
                if (rdGio.Checked)
                    item.DonViGhiNhanHoatDong = DonViGhiNhanHoatDong.Gio;
                else
                    item.DonViGhiNhanHoatDong = DonViGhiNhanHoatDong.Km;
                item.IDLoaiBaoDuong= int.Parse(dlLoaiBaoDuong.SelectedValue);
                item.IDNhomTrangThietBi = int.Parse(dlEquipGroup.SelectedValue);
                EquipmentTypeManager.UpdateEquipmentType(item);
            }
            else
            {
                tblLoaiTrangThietBi newItem = new tblLoaiTrangThietBi();
                newItem.Ten = txtName.Text.Trim();
                newItem.TenEng = txtNameEng.Text.Trim();
                newItem.SoThuTuHienThi = numberDisplay;
                newItem.GhiChu = txtNote.Text.Trim();
                if (rdGio.Checked)
                    newItem.DonViGhiNhanHoatDong = DonViGhiNhanHoatDong.Gio;
                else
                    newItem.DonViGhiNhanHoatDong = DonViGhiNhanHoatDong.Km;
                newItem.IDLoaiBaoDuong = int.Parse(dlLoaiBaoDuong.SelectedValue);
                newItem.IDNhomTrangThietBi = int.Parse(dlEquipGroup.SelectedValue);
                EquipmentTypeManager.InsertEquipmentType(newItem);
            }

            Response.Redirect("EquipmentTypeList.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}