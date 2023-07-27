using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class MaintenanceChildTypeDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Child_Maintenance_Type;
        }
    }

    private int IDLoaiBaoDuong
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
                LoadDonViTinhHanBaoDuong();
                LoadDetail();

                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.ChildMaintenanceType_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.ChildMaintenanceType_Edit);

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
        txtSoLuongBaoDuongDinhKy.MaxLength = 10;
    }

    private void LoadKieuBaoDuong()
    {
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
        LoadDonViTinhHanBaoDuong();
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
        if (IDLoaiBaoDuong > 0)
        {
            lblAction.Text = "Edit";
            tblLoaiBaoDuong item = MaintenanceTypeManager.GetMaintenanceChildTypeById(IDLoaiBaoDuong);
            if (item != null)
            {
                dlKieuBaoDuong.SelectedValue = item.IDKieuBaoDuong.ToString();
                txtTenLoaiBaoDuong.Text = item.Ten.ToString();
                txtSoLuongBaoDuongDinhKy.Text = item.SoLuongBaoDuongDinhKy.ToString();
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
            if (IDLoaiBaoDuong > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.ChildMaintenanceType_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.ChildMaintenanceType_Add))
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

            if (string.IsNullOrEmpty(txtTenLoaiBaoDuong.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Tên loại bảo dưỡng</b>", MessageType.Message);
                return;
            }
   
            double soLuongBaoDuongDinhKy = 0;
            if (!NumberHelper.ConvertToDouble(txtSoLuongBaoDuongDinhKy.Text.Trim(), out soLuongBaoDuongDinhKy))
            {
                ShowMessage(this, "Please enter <b>Số lượng bảo dưỡng định kỳ</b> is a number", MessageType.Message);
                return;
            }

            if (soLuongBaoDuongDinhKy <= 0)
            {
                ShowMessage(this, "Please enter <b>Số lượng bảo dưỡng định kỳ</b> is greater than 0", MessageType.Message);
                return;
            }

            if (IDLoaiBaoDuong > 0)
            {
                tblLoaiBaoDuong item = new tblLoaiBaoDuong();
                item.IDLoaiBaoDuong = IDLoaiBaoDuong;
                item.Ten = txtTenLoaiBaoDuong.Text.Trim();
                item.IDKieuBaoDuong = int.Parse(dlKieuBaoDuong.SelectedValue); 
                item.SoLuongBaoDuongDinhKy = soLuongBaoDuongDinhKy;
                MaintenanceTypeManager.UpdateMaintenanceChildType(item);
            }
            else
            {
                tblLoaiBaoDuong newItem = new tblLoaiBaoDuong();
                newItem.IDKieuBaoDuong = int.Parse(dlKieuBaoDuong.SelectedValue); 
                newItem.Ten = txtTenLoaiBaoDuong.Text.Trim();
                newItem.SoLuongBaoDuongDinhKy = soLuongBaoDuongDinhKy;
                MaintenanceTypeManager.InsertMaintenanceChildType(newItem);
            }

            Response.Redirect("MaintenanceChildType.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}