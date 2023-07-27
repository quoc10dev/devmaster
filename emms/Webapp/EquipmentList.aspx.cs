using BusinessLogic;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EquipmentList : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Equipment_List;
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

                CompanyManager.LoadCompanyForSearch(dlFilterCompany);
                EquipmentGroupManager.LoadEquipmentGroupForSearch(dlFilterNhomTrangThietBi);
                LoadEquipmentType();

                SortExpression = "ID";
                PageIndex = 1;
                BindData();

                dlFilterCompany.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterBienSo.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterTen.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
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
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Equipment_Add); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        txtFilterBienSo.MaxLength = 50; 
        txtFilterTen.MaxLength = 300;
    }

    protected void dlFilterNhomTrangThietBi_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEquipmentType();
    }

    private void LoadEquipmentType()
    {
        try
        {
            //Load loại trang thiết bị
            int idEquipmentGroup = 0;
            if (int.TryParse(dlFilterNhomTrangThietBi.SelectedItem.Value, out idEquipmentGroup))
                EquipmentTypeManager.LoadEquipmentTypeForSearch(dlFilterLoaiTrangThietBi, idEquipmentGroup);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion

    #region gridview

    public override void BindData()
    {
        int totalRecord = 0;

        int idCongTy = 0;
        int idNhomTrangThietBi = 0;
        int idLoaiTrangThietBi = 0;
        
        if (!string.IsNullOrEmpty(dlFilterCompany.SelectedValue))
            idCongTy = int.Parse(dlFilterCompany.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
            idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
            idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

        DataTable dt = EquipmentManager.SearchEquipment(idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, txtFilterBienSo.Text.Trim(), 
                                            txtFilterTen.Text.Trim(), 
                                            PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
        gvList.DataSource = dt;
        gvList.DataBind();
        gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

        //Nếu lọc loại xe cụ thể thì ẩn cột "Nhóm xe", "Loại xe"  
        if (idLoaiTrangThietBi > 0)
        {
            gvList.Columns[1].Visible = false;
            gvList.Columns[2].Visible = false;
        }
        else
        {
            gvList.Columns[1].Visible = true;
            gvList.Columns[2].Visible = true;
        }

        //Nếu lọc nhóm xe cụ thể thì ẩn cột "Nhóm xe"  
        gvList.Columns[1].Visible = (idNhomTrangThietBi > 0) ? false : true;

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
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Equipment_Edit) ||
                            FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Equipment_View); //phân quyền sửa, xem
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                        lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Equipment_Delete); //phân quyền xóa
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
                Response.Redirect(string.Format("EquipmentDetail.aspx?ID={0}", id));
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
            EquipmentManager.DeleteEquipment(id);
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
            dlFilterCompany.SelectedIndex = 1;
            dlFilterNhomTrangThietBi.SelectedIndex = 0;
            dlFilterLoaiTrangThietBi.SelectedIndex = 0;
            txtFilterBienSo.Text = string.Empty;
            txtFilterTen.Text = string.Empty;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            int totalRecord = 0;

            int idCongTy = 0;
            int idNhomTrangThietBi = 0;
            int idLoaiTrangThietBi = 0;

            if (!string.IsNullOrEmpty(dlFilterCompany.SelectedValue))
                idCongTy = int.Parse(dlFilterCompany.SelectedValue);

            if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
                idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

            if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
                idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

            string bienSo = txtFilterBienSo.Text.Trim();
            string ten = txtFilterTen.Text.Trim();

            DataTable dt = EquipmentManager.SearchEquipment(idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, txtFilterBienSo.Text.Trim(),
                                                txtFilterTen.Text.Trim(),
                                                PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);

            string url = string.Format("EquipmentListExport.aspx?IDCongTy={0}&IDNhomTrangThietBi={1}&IDLoaiTrangThietBi={2}&BienSo={3}&Ten={4}",
                                    idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, bienSo, ten);
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}