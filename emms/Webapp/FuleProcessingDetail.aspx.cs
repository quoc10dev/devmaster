using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class FuleProcessingDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Fule_Processing;
        }
    }

    private int IDFuleProcessing
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

                btnSave.Enabled = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.FuleProcessing_Add) ||
                                  FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.FuleProcessing_Edit);

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
    }

    private void LoadDetail()
    {
        if (IDFuleProcessing > 0)
        {
            lblAction.Text = "Edit";

            tblNapNhienLieu item = FuleProcessingManager.GetFuleProcessingById(IDFuleProcessing);
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

                txtNgayNap.Text = string.Format("{0:dd/MM/yyyy}", item.NgayNap);
                dlTrangThietBi.SelectedValue = item.IDTrangThietBi.ToString();
                txtSoLuong.Text = item.SoLuong.ToString();

                //Chỉ cho sửa số lượng nạp
                txtNgayNap.Enabled = false;
                dlNhomXe.Enabled = false;
                dlLoaiTrangThietBi.Enabled = false;
                dlTrangThietBi.Enabled = false;
            }
        }
        else
        {
            lblAction.Text = "Add";
            txtNgayNap.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            LoadEquipmentType();
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

            if (string.IsNullOrEmpty(txtSoLuong.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Số lượng</b>", MessageType.Message);
                return;
            }

            double soLuong = 0;
            if (!NumberHelper.ConvertToDouble(txtSoLuong.Text.Trim().Replace(".", string.Empty), out soLuong))
            {
                ShowMessage(this, "Please enter <b>Số giờ/ số km</b> is a number", MessageType.Message);
                return;
            }

            if (soLuong <= 0)
            {
                ShowMessage(this, "Please enter <b>Số lượng</b> is greater than 0", MessageType.Message);
                return;
            }

            DateTime ngayNap = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtNgayNap.Text.Trim()) || txtNgayNap.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Ngày nạp</b>", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtNgayNap.Text.Trim(), out ngayNap))
            {
                ShowMessage(this, "Error convert <b>Ngày nạp</b> to datetime", MessageType.Message);
                return;
            }

            int idTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);

            if (IDFuleProcessing > 0)
            {
                tblNapNhienLieu item = new tblNapNhienLieu();
                item.ID = IDFuleProcessing;
                item.IDTrangThietBi = idTrangThietBi;
                item.NgayNap = ngayNap;
                item.SoLuong = soLuong;
                FuleProcessingManager.UpdateFuleProcessing(item);
            }
            else
            {
                //Kiểm tra chỉ cho phép mỗi ngày nhập một lần
                tblNapNhienLieu item = FuleProcessingManager.GetFuleProcessingByIdEquipmentAndDate(idTrangThietBi, ngayNap);
                if (item != null)
                {
                    ShowMessage(this, "Fule processing date of this equipment is exists. Please find and edit from list.", MessageType.Message);
                    return;
                }

                tblNapNhienLieu newItem = new tblNapNhienLieu();
                newItem.IDTrangThietBi = int.Parse(dlTrangThietBi.SelectedValue);
                newItem.NgayNap = ngayNap;
                newItem.SoLuong = soLuong;
                FuleProcessingManager.InsertFuleProcessing(newItem);
            }
            
            Response.Redirect("FuleProcessing.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}