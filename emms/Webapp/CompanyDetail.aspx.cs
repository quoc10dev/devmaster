using System;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Security;
using DataAccess;

public partial class CompanyDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Company_List;
        }
    }

    private int IDCompany
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
                LoadDetail();
                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Company_Add) || 
                                    FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Company_Edit);
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtTenDayDu.MaxLength = 300;
        txtTenVietTat.MaxLength = 100;
        txtDiaChi.MaxLength = 300;
        txtDienThoai.MaxLength = 200;
        txtEmail.MaxLength = 200;
    }

    private void LoadDetail()
    {
        if (IDCompany > 0)
        {
            lblAction.Text = "Edit";
            tblCongTy item = CompanyManager.GetCompanyById(IDCompany);
            if (item != null)
            {
                txtTenDayDu.Text = item.TenDayDu;
                txtTenVietTat.Text = item.TenVietTat;
                txtDiaChi.Text = item.DiaChi;
                txtDienThoai.Text = item.DienThoai;
                txtEmail.Text = item.Email;
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDCompany > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Company_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Company_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtTenDayDu.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Full name</b>", MessageType.Message);
                return;
            }

            if (IDCompany > 0)
            {
                tblCongTy item = new tblCongTy();
                item.ID = IDCompany;
                item.TenDayDu = txtTenDayDu.Text.Trim();
                item.TenVietTat = txtTenVietTat.Text.Trim();
                item.DiaChi = txtDiaChi.Text.Trim();
                item.DienThoai = txtDienThoai.Text.Trim();
                item.Email = txtEmail.Text.Trim();
                CompanyManager.UpdateCompany(item);
            }
            else
            {
                tblCongTy newItem = new tblCongTy();
                newItem.TenDayDu = txtTenDayDu.Text.Trim();
                newItem.TenVietTat = txtTenVietTat.Text.Trim();
                newItem.DiaChi = txtDiaChi.Text.Trim();
                newItem.DienThoai = txtDienThoai.Text.Trim();
                newItem.Email = txtEmail.Text.Trim();
                CompanyManager.InsertCompany(newItem);
            }

            Response.Redirect("CompanyList.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}