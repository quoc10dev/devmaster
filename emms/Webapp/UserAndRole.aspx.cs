using BusinessLogic.Security;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class UserAndRole : BaseAdminGridPage
{
    #region begin

    private int IdUser
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
                GetUserInfo();
                BindData();
            }
            gvRole.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void GetUserInfo()
    {
        tblUser user = UserManager.GetUserByIdUser(IdUser);
        if (user != null)
        {
            lblUserName.Text = user.UserName;
        }
    }

    #endregion

    #region gridview

    public override void BindData()
    {
        List<tblRole> result = RoleManager.GetAllRole(true);
        gvRole.DataSource = result;
        gvRole.DataBind();
        gvRole.HeaderRow.TableSection = TableRowSection.TableHeader;

        //Kiểm tra đánh dấu các quyền đã được gán
        List<tblUserRole> userRoleList = RoleManager.GetUserRoleByIdUser(IdUser);
        foreach (GridViewRow row in gvRole.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSelect = row.Cells[0].FindControl("chkSelect") as CheckBox;
                if (chkSelect != null)
                {
                    HiddenField hfIdRole = (HiddenField)row.Cells[0].FindControl("hfIdRole");
                    if (hfIdRole != null)
                    {
                        //Cách khác: userRoleList.Find(i => i.IDRole == int.Parse(hfIdRole.Value));
                        tblUserRole resultUserRole = userRoleList.Where(i => i.IDRole == int.Parse(hfIdRole.Value)).FirstOrDefault();
                        if (resultUserRole != null)
                            chkSelect.Checked = true;
                    }
                }
            }
        }
    }

    protected override void GridView_RowCreated(Object sender, GridViewRowEventArgs e)
    {
        try
        {
            base.GridView_RowCreated(sender, e); //gọi thực thi phương thức GridView_RowCreated ở lớp cha BaseAdminGridPage
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tblRole row = e.Row.DataItem as tblRole;
                if (row != null)
                {
                    HiddenField hfIdRole = (HiddenField)e.Row.FindControl("hfIdRole");
                    if (hfIdRole != null)
                        hfIdRole.Value = row.ID.ToString();
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tblRole row = e.Row.DataItem as tblRole;
                if (row != null)
                {
                    HiddenField hfIdRole = (HiddenField)e.Row.FindControl("hfIdRole");
                    if (hfIdRole != null)
                        hfIdRole.Value = row.ID.ToString();
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion

    #region button

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvRole.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvRole.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSelect = row.Cells[0].FindControl("chkSelect") as CheckBox;
                        HiddenField hfIdRole = row.Cells[0].FindControl("hfIdRole") as HiddenField;
                        if (chkSelect != null && hfIdRole != null)
                        {
                            int idRole = int.Parse(hfIdRole.Value);
                            if (chkSelect.Checked)
                            {
                                //Kiểm tra dưới bảng tblUserRole nếu chưa có thì thêm
                                List<tblUserRole> userRole = RoleManager.GetUserRoleByIdUserAndIdRole(IdUser, idRole);
                                if (userRole.Count == 0)
                                {
                                    tblUserRole newUserRole = new tblUserRole();
                                    newUserRole.IDRole = idRole;
                                    newUserRole.IDUser = IdUser;
                                    RoleManager.InsertUserRole(newUserRole);
                                }
                            }
                            else
                            {
                                //Kiểm tra dưới bảng tblUserRole nếu có thì xóa
                                List<tblUserRole> userRole = RoleManager.GetUserRoleByIdUserAndIdRole(IdUser, idRole);
                                if (userRole != null && userRole.Count > 0)
                                    RoleManager.DeleteRoleRoleByIdUserAndIdRole(userRole[0].IDUser, userRole[0].IDRole);
                            }
                        }
                    }
                }
                BindData();
                ShowMessage(this, "Update successfully.", MessageType.Message);
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}