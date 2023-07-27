using System;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class FailureDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Failure_List;
        }
    }

    private int IDFailure
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
                btnSave.Enabled = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Failure_Type_Add) || 
                                    FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Failure_Type_Edit);
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
        txtTen.MaxLength = 200;
        txtSoThuTuHienThi.MaxLength = 5;
    }

    private void LoadDetail()
    {
        if (IDFailure > 0)
        {
            lblAction.Text = "Edit";
            tblLoaiHuHong item = FailureManager.GetFailureById(IDFailure);
            if (item != null)
            {
                txtTen.Text = item.Ten;
                txtSoThuTuHienThi.Text = item.SoThuTuHienThi.ToString();
            }
        }
        else
            lblAction.Text = "Add";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtTen.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Failure name</b>", MessageType.Message);
                return;
            }
            
            if (string.IsNullOrEmpty(txtSoThuTuHienThi.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Number display</b>", MessageType.Message);
                return;
            }

            int numberDisplay = 0;
            if (!NumberHelper.ConvertToInt(txtSoThuTuHienThi.Text.Trim(), out numberDisplay))
            {
                ShowMessage(this, "Please enter <b>Number display</b> is a number", MessageType.Message);
                return;
            }

            if (numberDisplay <= 0)
            {
                ShowMessage(this, "Please enter <b>Number display</b> is greater than 0", MessageType.Message);
                return;
            }

            if (IDFailure > 0)
            {
                tblLoaiHuHong item = new tblLoaiHuHong();
                item.ID = IDFailure;
                item.Ten = txtTen.Text.Trim();
                item.SoThuTuHienThi = short.Parse(txtSoThuTuHienThi.Text);
                FailureManager.UpdateFailure(item);
            }
            else
            {
                tblLoaiHuHong newItem = new tblLoaiHuHong();
                newItem.Ten = txtTen.Text.Trim();
                newItem.SoThuTuHienThi = short.Parse(txtSoThuTuHienThi.Text);
                FailureManager.InsertFailure(newItem);
            }

            Response.Redirect("FailureType.aspx");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}