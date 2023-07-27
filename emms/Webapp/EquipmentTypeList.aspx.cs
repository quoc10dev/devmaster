using BusinessLogic;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EquipmentTypeList : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Equipment_Type_List;
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
                EquipmentGroupManager.LoadEquipmentGroupForSearch(dlFilterNhomTrangThietBi);
                SortExpression = "ID";
                PageIndex = 1;
                BindData();

                txtFilterName.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            }
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
            MyGridViewPager.PopulatePager(MyGridViewPager.TotalRecord, PageIndex, PageSize);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_Add); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        txtFilterName.MaxLength = 300;
    }

    #endregion

    #region gridview

    public override void BindData()
    {
        int totalRecord = 0;

        int idNhomTrangThietBi = 0;
        if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
            idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

        DataTable dt = EquipmentTypeManager.SearchEquipmentType(idNhomTrangThietBi, txtFilterName.Text.Trim(), 
                                    PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
        gvList.DataSource = dt;
        gvList.DataBind();
        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

        MyGridViewPager.PopulatePager(totalRecord, PageIndex, PageSize);
        MyGridViewPager.TotalRecord = totalRecord;
        MyGridViewPager.SetTotalRecord(totalRecord);
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_Edit) ||
                                    FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_View); //phân quyền sửa
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                        lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_Delete); //phân quyền xóa
                    }

                    Label lblDonViGhiNhanHoatDong = e.Row.FindControl("lblDonViGhiNhanHoatDong") as Label;
                    if (lblDonViGhiNhanHoatDong != null && row["DonViGhiNhanHoatDong"] != null)
                    {
                        if (row["DonViGhiNhanHoatDong"].ToString().Equals(DonViGhiNhanHoatDong.Gio))
                            lblDonViGhiNhanHoatDong.Text = "Giờ";
                        else
                            lblDonViGhiNhanHoatDong.Text = "Km";
                    }

                    HiddenField hfId = (HiddenField)e.Row.FindControl("hfId");
                    if (hfId != null && row["ID"] != DBNull.Value)
                        hfId.Value = row["ID"].ToString();

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

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("editRecord"))
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect(string.Format("EquipmentTypeDetail.aspx?ID={0}", id));
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                //Lấy giá trị Id
                hfIdToDelete.Value = e.CommandArgument.ToString();

                //Lấy giá trị username
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hfName = (HiddenField)row.Cells[1].FindControl("hfName");
                if (hfName != null)
                {
                    //Kiểm tra khóa ngoại
                    //if (CheckBeforeDelete() == false)
                    //{
                    //    ShowMessage(this, string.Format("Can't delete <b>{0}</b>. This role is permitted for user.", hfName.Value), MessageType.Message);
                    //    return;
                    //}

                    ltrContentDialog.Text = string.Format("Are you sure you want to delete <b>{0}</b> ?", hfName.Value);
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

    //private bool CheckBeforeDelete()
    //{
        
    //    int idRole = 0;
    //    int.TryParse(hfIdToDelete.Value, out idRole);
    //    List<tblUserRole> userRoleList = RoleManager.GetUserRoleByIdRole(idRole);
    //    if (userRoleList.Count > 0)
    //        return false;
    //    else
    //        return true;
    //}

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            int.TryParse(hfIdToDelete.Value, out id);
            EquipmentTypeManager.DeleteEquipmentType(id);
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
            txtFilterName.Text = string.Empty;
            dlFilterNhomTrangThietBi.SelectedIndex = 0;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}