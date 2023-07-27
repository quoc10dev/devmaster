using BusinessLogic;
using BusinessLogic.Security;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaintenanceLevel : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_Level;
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

                MaintenanceTypeManager.GetAllMaintenanceTypeForSearch(dlMaintenanceType);
                if (dlMaintenanceType.Items.Count > 0)
                    LoadLoaiBaoDuong(int.Parse(dlMaintenanceType.SelectedValue));

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
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Level_Add); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        txtFilterName.MaxLength = 100;
    }

    protected void dlMaintenanceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int idKieuBaoDuong = 0;
            if (int.TryParse(dlMaintenanceType.SelectedItem.Value, out idKieuBaoDuong))
                LoadLoaiBaoDuong(idKieuBaoDuong);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadLoaiBaoDuong(int idKieuBaoDuong)
    {
        dlMaintenanceChildType.Items.Clear();

        List<tblLoaiBaoDuong> result = MaintenanceTypeManager.GetMaintenanceChildTypeByIdKieuBaoDuong(idKieuBaoDuong);

        tblLoaiBaoDuong findAllItem = new tblLoaiBaoDuong();
        findAllItem.IDLoaiBaoDuong = 0;
        findAllItem.Ten = "--- Select All ---";
        result.Insert(0, findAllItem);

        dlMaintenanceChildType.DataSource = result;
        dlMaintenanceChildType.DataTextField = "Ten";
        dlMaintenanceChildType.DataValueField = "IDLoaiBaoDuong";
        dlMaintenanceChildType.DataBind();

        dlMaintenanceChildType.SelectedIndex = 0;
    }

    #endregion

    #region gridview

    public override void BindData()
    {
        int idKieuBaoDuong = 0;
        if (!string.IsNullOrEmpty(dlMaintenanceType.SelectedValue))
            int.TryParse(dlMaintenanceType.SelectedValue, out idKieuBaoDuong);

        int idLoaiBaoDuong = 0;
        if (!string.IsNullOrEmpty(dlMaintenanceChildType.SelectedValue))
            int.TryParse(dlMaintenanceChildType.SelectedValue, out idLoaiBaoDuong);

        int totalRecord = 0;
        DataTable dt = MaintenanceLevelManager.SearchMaintenanceLevel(idKieuBaoDuong, idLoaiBaoDuong, txtFilterName.Text.Trim(), PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
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
                    Label lblDonViTinh = e.Row.FindControl("lblDonViTinh") as Label;
                    if (lblDonViTinh != null)
                        lblDonViTinh.Text = MaintenanceTypeManager.GetNameOfMaintenanceType(row["MaKieuBaoDuong"].ToString());

                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Level_Edit) ||
                            FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Level_View); //phân quyền sửa
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                        lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Level_Delete); //phân quyền xóa
                    }

                    HiddenField hfIdCompany = (HiddenField)e.Row.FindControl("hfId");
                    if (hfIdCompany != null && row["ID"] != DBNull.Value)
                        hfIdCompany.Value = row["ID"].ToString();

                    HiddenField hfName = (HiddenField)e.Row.FindControl("hfName");
                    if (hfName != null && row["TenCapBaoDuong"] != DBNull.Value)
                        hfName.Value = row["TenCapBaoDuong"].ToString();
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
                Response.Redirect(string.Format("MaintenanceLevelDetail.aspx?ID={0}", id));
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
            MaintenanceLevelManager.DeleteMaintenanceLevel(int.Parse(hfIdToDelete.Value));
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
            dlMaintenanceType.SelectedIndex = 0;
            dlMaintenanceChildType.SelectedIndex = 0;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}