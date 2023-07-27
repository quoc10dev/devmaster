using BusinessLogic;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaintenanceRequestForm : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_Request_Form;
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
                SortExpression = "ID";
                PageIndex = 1;

                EquipmentGroupManager.LoadEquipmentGroupForSearch(dlFilterNhomTrangThietBi);
                LoadEquipmentType();

                BindData();
            }
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            MyGridViewPager.PopulatePager(MyGridViewPager.TotalRecord, PageIndex, PageSize);

            dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");

            //Thiết lập bắt buộc nhập đúng format cho các control tìm kiếm datetime, number
            //Khởi tạo: Control.Build(el);
            //Hủy: Control.Destroy(el);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls",
                                                   "if(typeof(Control)!='undefined') Control.Build(" + divFilter.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Add); //Phân quyền thêm
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
        int idNhomTrangThietBi = 0;
        int idLoaiTrangThietBi = 0;

        if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
            idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
            idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

        DataTable dt = MaintenanceEquipmentType.SearchMaintenanceEquipmentType(idNhomTrangThietBi, idLoaiTrangThietBi,
                                PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
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
                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Edit) ||
                                        FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_View); //phân quyền sửa, xem
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                        lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Delete); //phân quyền xóa
                    }

                    HiddenField hfId = (HiddenField)e.Row.FindControl("hfId");
                    if (hfId != null && row["ID"] != DBNull.Value)
                        hfId.Value = row["ID"].ToString();

                    HiddenField hfName = (HiddenField)e.Row.FindControl("hfTen");
                    if (hfName != null && row["TenLoai"] != DBNull.Value)
                        hfName.Value = row["TenLoai"].ToString();
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
                Response.Redirect(string.Format("MaintenanceRequestFormDetail.aspx?ID={0}", id));
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                //Lấy giá trị Id
                hfIdToDelete.Value = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hfId = (HiddenField)row.Cells[1].FindControl("hfId");
                HiddenField hfTen = (HiddenField)row.Cells[1].FindControl("hfTen");
                if (hfId != null && hfTen != null)
                    ltrContentDialog.Text = string.Format("Are you sure you want to delete item <b>{0} - {1}</b> ?", hfId.Value, hfTen.Value);
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
            MaintenanceRequestManager.DeleteMaintenanceRequestForm(int.Parse(hfIdToDelete.Value));
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
            dlFilterNhomTrangThietBi.SelectedIndex = 0;
            dlFilterLoaiTrangThietBi.SelectedIndex = 0;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}