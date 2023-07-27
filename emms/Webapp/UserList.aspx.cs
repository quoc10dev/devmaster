using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserList : BaseAdminGridPage
{
    #region begin
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.User_List;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MasterPage.FunctionPageCode = FunctionPageCode;

                Permission();
                SetMaxLength();

                SortExpression = "ID";
                PageIndex = 1;
                BindData();
            }
            gvUser.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.User_Add); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        txtFilterFullname.MaxLength = 100;
        txtFilterUserName.MaxLength = 100;
    }

    //private void BindEmptyData()
    //{
    //    gvUser.DataSource = new DataTable();
    //    gvUser.DataBind();
    //    gvUser.UseAccessibleHeader = true;
    //    gvUser.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    int totalRecord = 0;
    //    MyGridViewPager.PopulatePager(totalRecord, PageIndex, PageSize);
    //    ViewState["TotalRecord"] = totalRecord;
    //    MyGridViewPager.SetTotalRecord(totalRecord);

    //    //lblMessage.Visible = true;
    //}

    #endregion

    #region gridview

    public override void BindData()
    {
        int totalRecord = 0;
        DataTable dt = UserManager.SearchUser(txtFilterUserName.Text.Trim(), txtFilterFullname.Text.Trim(), 
                                            PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
        gvUser.DataSource = dt;
        gvUser.DataBind();
        gvUser.HeaderRow.TableSection = TableRowSection.TableHeader;

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
                        lblActive.Text = (bool)row["IsActive"] ? "Kích hoạt" : "Khóa";
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        lblActive.Text = (bool)row["IsActive"] ? "Kích hoạt" : "Khóa";

                    LinkButton lnkPermission = e.Row.FindControl("lnkPermission") as LinkButton;
                    if (lnkPermission != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkPermission);
                        lnkPermission.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Permission_View);
                    }

                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.User_Edit) ||
                            FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.User_View); //phân quyền sửa, xem
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                        lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.User_Delete); //phân quyền xóa
                    }

                    HiddenField hfIdUser = (HiddenField)e.Row.FindControl("hfIdUser");
                    if (hfIdUser != null && row["ID"] != DBNull.Value)
                        hfIdUser.Value = row["ID"].ToString();

                    HiddenField hfFullname = (HiddenField)e.Row.FindControl("hfFullname");
                    if (hfFullname != null && row["Fullname"] != DBNull.Value)
                        hfFullname.Value = row["Fullname"].ToString();
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("permissRecord"))
            {
                int idUser = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect(string.Format("UserAndRole.aspx?ID={0}", idUser));
            }
            else if (e.CommandName.Equals("editRecord"))
            {
                int idUser = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect(string.Format("UserDetail.aspx?ID={0}", idUser));
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                //Lấy giá trị IdUser
                int idUser = Convert.ToInt32(e.CommandArgument);
                hfIdUserToDelete.Value = e.CommandArgument.ToString();

                //Lấy giá trị username
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hfFullname = (HiddenField)row.Cells[1].FindControl("hfFullname");
                if (hfFullname != null)
                {
                    ltrContentDialog.Text = string.Format("Are you sure you want to delete <b>{0}</b> ?", hfFullname.Value);
                    ShowModal();
                }

                //Load lại thanh phân trang
                //MyGridViewPager.PopulatePager(MyGridViewPager.TotalRecord, PageIndex, PageSize);
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            UserManager.DeleteUser(int.Parse(hfIdUserToDelete.Value));
            HideModal();
            BindData();
            upUserList.Update();
            ShowMessage(this, "Deleted successfully", MessageType.Message);
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserDetail.aspx");
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            //int milliseconds = 5000;
            //System.Threading.Thread.Sleep(milliseconds);

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
            txtFilterFullname.Text = string.Empty;
            txtFilterUserName.Text = string.Empty;
            MyGridViewPager.PopulatePager(MyGridViewPager.TotalRecord, PageIndex, PageSize);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}