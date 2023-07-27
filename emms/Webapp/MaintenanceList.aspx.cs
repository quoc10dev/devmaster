using BusinessLogic;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaintenanceList : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_List;
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
                MaintenanceGroupManager.LoadMaintenanceGroupForSearch(dlMaintenanceGroup);
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
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_List_Add); //Phân quyền thêm
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

        int idNhomBaoDuong = 0;
        if (!string.IsNullOrEmpty(dlMaintenanceGroup.SelectedValue))
            idNhomBaoDuong = int.Parse(dlMaintenanceGroup.SelectedValue);

        DataTable dt = MaintenanceManager.SearchAllMaintenance(idNhomBaoDuong, txtFilterName.Text.Trim(), PageIndex, PageSize, 
                                                        SortExpression, GetSortOrder, out totalRecord);
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
                    bool isGroup = (bool)row["IsGroup"];

                    Label lblID = e.Row.FindControl("lblID") as Label;
                    if (lblID != null)
                    {
                        lblID.Text = row["RowNum"].ToString();
                        //if (isGroup)
                            //lblID.Visible = false;
                    }

                    Label lblTen = e.Row.FindControl("lblTen") as Label;
                    if (lblTen != null)
                    {
                        lblTen.Text = row["Ten"].ToString();
                        if (isGroup)
                            lblTen.Font.Bold = true;
                    }

                    Label lblTenEng = e.Row.FindControl("lblTenEng") as Label;
                    if (lblTenEng != null)
                    {
                        lblTenEng.Text = row["TenEng"].ToString();
                        if (isGroup)
                            lblTenEng.Font.Bold = true;
                    }

                    Label lblSoThuTuHienThi = e.Row.FindControl("lblSoThuTuHienThi") as Label;
                    if (lblSoThuTuHienThi != null)
                    {
                        lblSoThuTuHienThi.Text = row["SoThuTuHienThi"].ToString();
                        if (isGroup)
                            lblSoThuTuHienThi.Font.Bold = true;
                    }

                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        if (isGroup)
                            lnkEdit.Visible = false;
                        else
                        {
                            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                            lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_List_Edit) ||
                                             FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_List_View); //phân quyền sửa
                        }
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        if (isGroup)
                            lnkDelete.Visible = false;
                        else
                        {
                            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                            lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_List_Delete); //phân quyền xóa
                        }
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
                Response.Redirect(string.Format("MaintenanceDetail.aspx?ID={0}", id));
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
            MaintenanceManager.DeleteMaintenance(int.Parse(hfIdToDelete.Value));
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
            dlMaintenanceGroup.SelectedIndex = 0;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}