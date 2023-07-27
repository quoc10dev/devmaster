using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class FrequencyWorkingDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Frequency_Working;
        }
    }

    private int IDFrequencyWorking
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

                btnSave.Enabled = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.FrequencyWorking_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.FrequencyWorking_Edit);

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                btnSave.OnClientClick = "Control.Destroy('#" + formatControl.ClientID + "')";
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
        txtSoLuongTanSuat.MaxLength = 10;
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
            int idEquipmentType = 0;
            if (int.TryParse(dlLoaiTrangThietBi.SelectedItem.Value, out idEquipmentType))
                LoadEquipment(idEquipmentType);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
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
                        lblDonViTanSuat.Text = "Giờ / ngày";
                    else if (loaiTrangThietBi.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Km))
                        lblDonViTanSuat.Text = "Km / ngày";
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
        if (IDFrequencyWorking > 0)
        {
            lblAction.Text = "Edit";

            tblTanSuatHoatDong item = FrequencyWorkingEquiment.GetFrequencyById(IDFrequencyWorking);
            if (item != null)
            {
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
                        LoadDonViGhiNhanHoatDong();
                    }
                }

                dlTrangThietBi.SelectedValue = item.IDTrangThietBi.ToString();
                txtSoLuongTanSuat.Text = item.SoLuongTanSuat.ToString();
                txtNgayBatDauApDung.Text = string.Format("{0:dd/MM/yyyy}", item.NgayBatDauApDung);

                dlNhomXe.Enabled = false;
                dlLoaiTrangThietBi.Enabled = false;
                dlTrangThietBi.Enabled = false;
            }
        }
        else
        {
            lblAction.Text = "Add";
            LoadEquipmentType();
            if (!string.IsNullOrEmpty(dlLoaiTrangThietBi.SelectedValue))
                LoadEquipment(int.Parse(dlLoaiTrangThietBi.SelectedValue));
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

            if (string.IsNullOrEmpty(txtSoLuongTanSuat.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Số lượng tần suất</b>", MessageType.Message);
                return;
            }

            double soLuongTanSuat = 0;
            if (!NumberHelper.ConvertToDouble(txtSoLuongTanSuat.Text.Trim(), out soLuongTanSuat))
            {
                ShowMessage(this, "Please enter <b>Số lượng tần suất</b> is a number", MessageType.Message);
                return;
            }

            if (soLuongTanSuat <= 0)
            {
                ShowMessage(this, "Please enter <b>Số lượng tần suất</b> is greater than 0", MessageType.Message);
                return;
            }

            DateTime ngayBatDauApDung = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtNgayBatDauApDung.Text.Trim()) || txtNgayBatDauApDung.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Ngày bắt đầu áp dụng</b> is a datetime", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtNgayBatDauApDung.Text.Trim(), out ngayBatDauApDung))
            {
                ShowMessage(this, "Error convert <b>Ngày bắt đầu áp dụng</b> to datetime", MessageType.Message);
                return;
            }

            if (IDFrequencyWorking > 0)
            {
                tblTanSuatHoatDong item = new tblTanSuatHoatDong();
                item.ID = IDFrequencyWorking;
                item.IDTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);
                item.SoLuongTanSuat = soLuongTanSuat;
                item.NgayBatDauApDung = ngayBatDauApDung;
                FrequencyWorkingEquiment.UpdateFrequency(item);
            }
            else
            {
                tblTanSuatHoatDong newItem = new tblTanSuatHoatDong();
                newItem.IDTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);
                newItem.SoLuongTanSuat = soLuongTanSuat;
                newItem.NgayBatDauApDung = ngayBatDauApDung;
                FrequencyWorkingEquiment.InsertFrequency(newItem);
            }
            
            Response.Redirect("FrequencyWorking.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}