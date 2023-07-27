using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class MaintenanceLevelDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_Level;
        }
    }

    private int IDCapBaoDuong
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
                LoadKieuBaoDuong();
                LoadLoaiBaoDuong();
                LoadDonViTinhHanBaoDuong();
                LoadDetail();

                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Level_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Level_Edit);

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
        txtSoLuongMoc.MaxLength = 10;
    }

    private void LoadKieuBaoDuong()
    {
        dlKieuBaoDuong.Items.Clear();

        List<tblKieuBaoDuong> kieuBaoDuongList = MaintenanceTypeManager.GetAllMaintenanceType();
        tblKieuBaoDuong emptyItem = new tblKieuBaoDuong();
        emptyItem.ID = 0;
        emptyItem.Ten = "--- Chọn kiểu bảo dưỡng ---";
        kieuBaoDuongList.Insert(0, emptyItem);

        dlKieuBaoDuong.DataSource = kieuBaoDuongList;
        dlKieuBaoDuong.DataTextField = "Ten";
        dlKieuBaoDuong.DataValueField = "ID";
        dlKieuBaoDuong.DataBind();
    }

    protected void dlKieuBaoDuong_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLoaiBaoDuong();
        LoadDonViTinhHanBaoDuong();
    }

    private void LoadLoaiBaoDuong()
    {
        dlLoaiBaoDuong.Items.Clear();
        int idKieuBaoDuong = 0;
        int.TryParse(dlKieuBaoDuong.SelectedValue, out idKieuBaoDuong);

        List<tblLoaiBaoDuong> loaiBaoDuongList = MaintenanceTypeManager.GetMaintenanceChildTypeByIdKieuBaoDuong(idKieuBaoDuong);
        tblLoaiBaoDuong emptyItem = new tblLoaiBaoDuong();
        emptyItem.IDLoaiBaoDuong = 0;
        emptyItem.Ten = "--- Chọn loại bảo dưỡng ---";
        loaiBaoDuongList.Insert(0, emptyItem);

        dlLoaiBaoDuong.DataSource = loaiBaoDuongList;
        dlLoaiBaoDuong.DataTextField = "Ten";
        dlLoaiBaoDuong.DataValueField = "IDLoaiBaoDuong";
        dlLoaiBaoDuong.DataBind();
    }

    private void LoadDonViTinhHanBaoDuong()
    {
        try
        {
            int idKieuBaoDuong = 0; 
            if (int.TryParse(dlKieuBaoDuong.SelectedItem.Value, out idKieuBaoDuong))
            {
                tblKieuBaoDuong item = MaintenanceTypeManager.GetMaintenanceTypeById(idKieuBaoDuong);
                if (item != null && !string.IsNullOrEmpty(item.Ma))
                {
                    lblDonViTinh.Text = MaintenanceTypeManager.GetNameOfMaintenanceType(item.Ma);
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
        if (IDCapBaoDuong > 0)
        {
            lblAction.Text = "Edit";
            tblCapBaoDuong item = MaintenanceLevelManager.GetMaintenanceLevelById(IDCapBaoDuong);
            if (item != null)
            {
                dlKieuBaoDuong.SelectedValue = item.tblLoaiBaoDuong.IDKieuBaoDuong.ToString();

                LoadLoaiBaoDuong();
                dlLoaiBaoDuong.SelectedValue = item.IDLoaiBaoDuong.ToString();

                txtTenCapBaoDuong.Text = item.Ten.ToString();
                txtTenVietTat_ViTri1.Text = !string.IsNullOrEmpty(item.ShowInPrintPosition1) ? item.ShowInPrintPosition1 : string.Empty;
                txtTenVietTat_ViTri2.Text = !string.IsNullOrEmpty(item.ShowInPrintPosition2) ? item.ShowInPrintPosition2 : string.Empty;
                txtSoLuongMoc.Text = item.SoLuongMoc.ToString();

                if (item.LevelInPrint.ToString().Equals("1"))
                    rdCap1.Checked = true;
                else if (item.LevelInPrint.ToString().Equals("2"))
                    rdCap2.Checked = true;
                else if (item.LevelInPrint.ToString().Equals("3"))
                    rdCap3.Checked = true;
                else if (item.LevelInPrint.ToString().Equals("4"))
                    rdCap4.Checked = true;

                LoadDonViTinhHanBaoDuong();
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDCapBaoDuong > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Level_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Level_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (string.IsNullOrEmpty(dlKieuBaoDuong.SelectedValue) || dlKieuBaoDuong.SelectedValue.Equals("0"))
            {
                ShowMessage(this, "Please enter <b>Kiểu bảo dưỡng</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(dlLoaiBaoDuong.SelectedValue) || dlLoaiBaoDuong.SelectedValue.Equals("0"))
            {
                ShowMessage(this, "Please enter <b>Loại bảo dưỡng</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtTenCapBaoDuong.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Tên cấp bảo dưỡng</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtTenVietTat_ViTri1.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Tên viết tắt 1</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtTenVietTat_ViTri2.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Tên viết tắt 2</b>", MessageType.Message);
                return;
            }

            double soLuongMocBaoDuong = 0;
            if (!NumberHelper.ConvertToDouble(txtSoLuongMoc.Text.Trim(), out soLuongMocBaoDuong))
            {
                ShowMessage(this, "Please enter <b>Mốc bảo dưỡng</b> is a number", MessageType.Message);
                return;
            }

            if (soLuongMocBaoDuong <= 0)
            {
                ShowMessage(this, "Please enter <b>Mốc bảo dưỡng</b> is greater than 0", MessageType.Message);
                return;
            }

            if (IDCapBaoDuong > 0)
            {
                tblCapBaoDuong item = new tblCapBaoDuong();
                item.ID = IDCapBaoDuong;
                item.IDLoaiBaoDuong = int.Parse(dlLoaiBaoDuong.SelectedValue);
                item.Ten = txtTenCapBaoDuong.Text.Trim();
                item.SoLuongMoc = soLuongMocBaoDuong;
                item.ShowInPrintPosition1 = txtTenVietTat_ViTri1.Text.Trim();
                item.ShowInPrintPosition2 = txtTenVietTat_ViTri2.Text.Trim();

                if (rdCap1.Checked)
                    item.LevelInPrint = 1;
                else if (rdCap2.Checked)
                    item.LevelInPrint = 2;
                else if (rdCap3.Checked)
                    item.LevelInPrint = 3;
                else if (rdCap4.Checked)
                    item.LevelInPrint = 4;

                MaintenanceLevelManager.UpdateMaintenanceLevel(item);
            }
            else
            {
                tblCapBaoDuong newItem = new tblCapBaoDuong();
                newItem.IDLoaiBaoDuong = int.Parse(dlLoaiBaoDuong.SelectedValue); 
                newItem.Ten = txtTenCapBaoDuong.Text.Trim();
                newItem.SoLuongMoc = soLuongMocBaoDuong;
                newItem.ShowInPrintPosition1 = txtTenVietTat_ViTri1.Text.Trim();
                newItem.ShowInPrintPosition2 = txtTenVietTat_ViTri2.Text.Trim();

                if (rdCap1.Checked)
                    newItem.LevelInPrint = 1;
                else if (rdCap2.Checked)
                    newItem.LevelInPrint = 2;
                else if (rdCap3.Checked)
                    newItem.LevelInPrint = 3;
                else if (rdCap4.Checked)
                    newItem.LevelInPrint = 4;

                MaintenanceLevelManager.InsertMaintenanceLevel(newItem);
            }

            Response.Redirect("MaintenanceLevel.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}