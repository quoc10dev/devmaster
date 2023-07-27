using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FuleProcessing : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Fule_Processing;
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

                SetDefaultDateValue();
                CompanyManager.LoadCompanyForSearch(dlFilterCompany);
                EquipmentGroupManager.LoadEquipmentGroupForSearch(dlFilterNhomTrangThietBi);
                LoadEquipmentType();
                BindData();

                txtFilterTuNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterDenNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterCompany.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterBienSo.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterTen.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            }
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            MyGridViewPager.PopulatePager(MyGridViewPager.TotalRecord, PageIndex, PageSize);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls", "Control.Build(" + formatControl.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.FuleProcessing_Add); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        txtFilterTen.MaxLength = 300;
        txtFilterBienSo.MaxLength = 50;
    }

    private void SetDefaultDateValue()
    {
        txtFilterTuNgay.Text = DateTimeHelper.FirstDayOfMonth(DateTime.Now).ToString("dd/MM/yyyy");
        txtFilterDenNgay.Text = DateTimeHelper.LastDayOfMonth(DateTime.Now).ToString("dd/MM/yyyy");
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
        DateTime tuNgay = DateTime.MinValue;
        DateTime denNgay = DateTime.MinValue;

        if (string.IsNullOrEmpty(txtFilterTuNgay.Text.Trim()) || txtFilterTuNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Từ ngày</b>", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterTuNgay.Text.Trim(), out tuNgay))
        {
            ShowMessage(this, "Error convert <b>Từ ngày</b> to datetime", MessageType.Message);
            return;
        }

        if (string.IsNullOrEmpty(txtFilterDenNgay.Text.Trim()) || txtFilterDenNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Đến ngày</b>", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterDenNgay.Text.Trim(), out denNgay))
        {
            ShowMessage(this, "Error convert <b>Đến ngày</b> to datetime", MessageType.Message);
            return;
        }

        if (DateTime.Compare(tuNgay, denNgay) > 0)
        {
            ShowMessage(this, "Please enter <b>Từ ngày</b> is earlier than <b>Đến ngày</b>.", MessageType.Message);
            return;
        }

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

        DataTable dt = FuleProcessingManager.SearchFuleProcessing(tuNgay, denNgay, idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, txtFilterBienSo.Text.Trim(),             txtFilterTen.Text.Trim(), PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
        gridView.DataSource = dt;
        gridView.DataBind();
        gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

        //Nếu lọc loại xe cụ thể thì ẩn cột "Nhóm xe", "Loại xe"  
        if (idLoaiTrangThietBi > 0)
        {
            gridView.Columns[2].Visible = false;
            gridView.Columns[3].Visible = false;
        }
        else
        {
            gridView.Columns[2].Visible = true;
            gridView.Columns[3].Visible = true;
        }

        //Nếu lọc nhóm xe cụ thể thì ẩn cột "Nhóm xe"  
        gridView.Columns[2].Visible = (idNhomTrangThietBi > 0) ? false : true;

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
                    Label lblSoLuong = e.Row.FindControl("lblSoLuong") as Label;
                    if (lblSoLuong != null)
                        lblSoLuong.Text = NumberHelper.ToStringNumber(row["SoLuong"]);

                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.FuleProcessing_Edit); //phân quyền sửa
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                        lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.FuleProcessing_View); //phân quyền xóa
                    }

                    HiddenField hfId = (HiddenField)e.Row.FindControl("hfId");
                    if (hfId != null && row["ID"] != DBNull.Value)
                        hfId.Value = row["ID"].ToString();

                    //HiddenField hfName = (HiddenField)e.Row.FindControl("hfName");
                    //if (hfName != null && row["TenVietTat"] != DBNull.Value)
                    //    hfName.Value = row["TenVietTat"].ToString();
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
                Response.Redirect(string.Format("FuleProcessingDetail.aspx?ID={0}", id));
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                //Lấy giá trị Id
                hfIdToDelete.Value = e.CommandArgument.ToString();

                //Lấy giá trị username
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hfId = (HiddenField)row.Cells[1].FindControl("hfId");
                if (hfId != null)
                    ltrContentDialog.Text = string.Format("Are you sure you want to delete item <b>{0}</b> ?", hfId.Value);
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
            FuleProcessingManager.DeleteFuleProcessing(int.Parse(hfIdToDelete.Value));
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
            txtFilterBienSo.Text = string.Empty;
            txtFilterTen.Text = string.Empty;
            dlFilterNhomTrangThietBi.SelectedIndex = 0;
            dlFilterLoaiTrangThietBi.SelectedIndex = 0;
            dlFilterCompany.SelectedIndex = 1;
            SetDefaultDateValue();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}