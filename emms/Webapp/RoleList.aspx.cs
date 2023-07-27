using BusinessLogic.Security;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RoleList : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Role_List;
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

                SortExpression = "ID";
                PageIndex = 1;
                BindData();
                Permission();
            }
            gvRole.HeaderRow.TableSection = TableRowSection.TableHeader;
            MyGridViewPager.PopulatePager(MyGridViewPager.TotalRecord, PageIndex, PageSize);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtFilterRoleName.MaxLength = 200;
        txtFilterDescription.MaxLength = 1000;
    }

    private void Permission()
    {
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Role_Add); //Phân quyền thêm
    }

    #endregion

    #region gridview

    public override void BindData()
    {
        int totalRecord = 0;
        DataTable dt = RoleManager.SearchRole(txtFilterRoleName.Text.Trim(), txtFilterDescription.Text.Trim(),
                                            PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
        gvRole.DataSource = dt;
        gvRole.DataBind();
        gvRole.HeaderRow.TableSection = TableRowSection.TableHeader;

        MyGridViewPager.PopulatePager(totalRecord, PageIndex, PageSize);
        MyGridViewPager.TotalRecord = totalRecord;
        MyGridViewPager.SetTotalRecord(totalRecord);
    }

    protected override void GridView_RowCreated(Object sender, GridViewRowEventArgs e)
    {
        try
        {
            base.GridView_RowCreated(sender, e); //gọi thực thi phương thức GridView_RowCreated ở lớp cha BaseAdminGridPage
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    Label lblActive = (Label)e.Row.FindControl("lblActive");
                    if (lblActive != null)
                        lblActive.Text = (bool)row["Status"] ? "Kích hoạt" : "Khóa";
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
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    Label lblActive = (Label)e.Row.FindControl("lblActive");
                    if (lblActive != null)
                        lblActive.Text = (bool)row["Status"] ? "Kích hoạt" : "Khóa";

                    LinkButton lnkPermission = e.Row.FindControl("lnkPermission") as LinkButton;
                    if (lnkPermission != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkPermission);
                        lnkPermission.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Permission_View) ||
                            FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Permission_Update);
                    }

                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Role_Edit) ||
                            FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Role_View);
                    }

                    LinkButton lb = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lb != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
                        lb.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Role_Delete);
                    }

                    HiddenField hfIdRole = (HiddenField)e.Row.FindControl("hfIdRole");
                    if (hfIdRole != null && row["ID"] != DBNull.Value)
                        hfIdRole.Value = row["ID"].ToString();

                    HiddenField hfRoleName = (HiddenField)e.Row.FindControl("hfRoleName");
                    if (hfRoleName != null && row["RoleName"] != DBNull.Value)
                        hfRoleName.Value = row["RoleName"].ToString();
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("permissRecord"))
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect(string.Format("PermissionRole.aspx?ID={0}", id));
            }
            else if (e.CommandName.Equals("editRecord"))
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect(string.Format("RoleDetail.aspx?ID={0}", id));
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                //Lấy giá trị Id
                hfIdToDelete.Value = e.CommandArgument.ToString();

                //Lấy giá trị username
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hfRolename = (HiddenField)row.Cells[1].FindControl("hfRolename");
                if (hfRolename != null)
                {
                    //Kiểm tra nếu role đã được gán cho user thì không cho xóa
                    if (CheckBeforeDelete() == false)
                    {
                        ShowMessage(this, string.Format("Can't delete <b>{0}</b>. This role is permitted for user.", hfRolename.Value), MessageType.Message);
                        return;
                    }

                    ltrContentDialog.Text = string.Format("Are you sure you want to delete <b>{0}</b> ?", hfRolename.Value);
                    ShowModal();
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void PageIndex_Changed(object sender, EventArgs e)
    {
        try
        {
            PageIndex = (int)sender;
            BindData();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void PageSize_Changed(object sender, EventArgs e)
    {
        try
        {
            PageSize = (int)sender;
            PageIndex = 1;
            BindData();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private bool CheckBeforeDelete()
    {

        int idRole = 0;
        int.TryParse(hfIdToDelete.Value, out idRole);
        List<tblUserRole> userRoleList = RoleManager.GetUserRoleByIdRole(idRole);
        if (userRoleList.Count > 0)
            return false;
        else
            return true;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int idRole = 0;
            int.TryParse(hfIdToDelete.Value, out idRole);

            RoleManager.DeleteRole(idRole);
            HideModal();
            BindData();
        }
        catch (Exception exc)
        {
            ShowMessage(this, "Không thể xóa. Thông tin này đang được dùng.", MessageType.Message);
        }
    }

    private void ShowModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalDelete').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
    }

    private void HideModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalDelete').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
    }
    #endregion

    #region button

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            PageIndex = 1;
            BindData();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtFilterRoleName.Text = string.Empty;
            txtFilterDescription.Text = string.Empty;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}