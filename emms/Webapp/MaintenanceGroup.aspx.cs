using BusinessLogic;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaintenanceGroup : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_Group_List;
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
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            MyGridViewPager.PopulatePager(MyGridViewPager.TotalRecord, PageIndex, PageSize);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Group_Add); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        txtFilterName.MaxLength = 200;
    }

    #endregion

    #region gridview

    public override void BindData()
    {
        int totalRecord = 0;
        //DataTable dt = MaintenanceGroupManager.SearchMaintenanceGroup(txtFilterName.Text.Trim(), PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
        DataTable dt = MaintenanceGroupManager.SearchMaintenanceGroup(txtFilterName.Text.Trim(), out totalRecord);
        gridView.DataSource = dt;
        gridView.DataBind();
        gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

        MyGridViewPager.PopulatePager(totalRecord, PageIndex, PageSize);
        MyGridViewPager.TotalRecord = totalRecord;
        MyGridViewPager.SetTotalRecord(totalRecord);
    }

    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    int cap = (int)row["Cap"];

                    Literal ltrTen = e.Row.FindControl("ltrTen") as Literal;
                    if (ltrTen != null)
                    {
                        if (cap == 1)
                            ltrTen.Text = string.Format("<b>{0}</b>", row["Ten"].ToString());
                        else if (cap == 2)
                            ltrTen.Text = string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", row["Ten"].ToString());
                        else if (cap == 3)
                            ltrTen.Text = string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", row["Ten"].ToString());
                    }

                    Literal ltrTenEng = e.Row.FindControl("ltrTenEng") as Literal;
                    if (ltrTenEng != null)
                    {
                        if (cap == 1)
                            ltrTenEng.Text = string.Format("<b>{0}</b>", row["TenEng"].ToString());
                        else if (cap == 2)
                            ltrTenEng.Text = string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", row["TenEng"].ToString());
                        else if (cap == 3)
                            ltrTenEng.Text = string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", row["TenEng"].ToString());
                    }

                    Literal ltrSTT = e.Row.FindControl("ltrSTT") as Literal;
                    if (ltrSTT != null)
                    {
                        if (cap == 1)
                            ltrSTT.Text = string.Format("<b>{0}</b>", row["SoThuTuHienThi"].ToString());
                        else if (cap == 2)
                            ltrSTT.Text = string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", row["SoThuTuHienThi"].ToString());
                        else if (cap == 3)
                            ltrSTT.Text = string.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}", row["SoThuTuHienThi"].ToString());
                    }

                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Group_Edit) ||
                                    FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Group_View); //phân quyền sửa, xem chi tiết
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                        lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Group_Delete); //phân quyền xóa
                    }

                    HiddenField hfIdCompany = (HiddenField)e.Row.FindControl("hfId");
                    if (hfIdCompany != null && row["ID"] != DBNull.Value)
                        hfIdCompany.Value = row["ID"].ToString();

                    HiddenField hfName = (HiddenField)e.Row.FindControl("hfName");
                    if (hfName != null && row["Ten"] != DBNull.Value)
                        hfName.Value = row["Ten"].ToString();
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("editRecord"))
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect(string.Format("MaintenanceGroupDetail.aspx?ID={0}", id));
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                //Lấy giá trị Id
                hfIdToDelete.Value = e.CommandArgument.ToString();

                //Lấy giá trị username
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hfName = (HiddenField)row.Cells[1].FindControl("hfName");
                if (hfName != null)
                    ltrContentDialog.Text = string.Format("Are you sure you want to delete <b>{0}</b> ?", hfName.Value);
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
            MaintenanceGroupManager.DeleteMaintenanceGroup(int.Parse(hfIdToDelete.Value));
            //MaintenanceGroupManager.DeleteMaintenanceGroupByID(int.Parse(hfIdToDelete.Value));
            HideModal();
            BindData();
        }
        catch (Exception exc)
        {
            ShowMessage(this, "Không thể xóa. Thông tin này đang được dùng.", MessageType.Message);
        }
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
            txtFilterName.Text = string.Empty;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}