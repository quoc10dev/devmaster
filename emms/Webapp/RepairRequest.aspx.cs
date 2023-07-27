using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RepairRequest : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Repair_Request;
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
                MaintenanceTypeManager.GetAllMaintenanceTypeForSearch(dlFilterKieuBaoDuong);

                BindData();

                //Khi bấm enter trên các textbox sẽ tự động bấm nút Filter
                txtFilterNgayNhapTuNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterNgayNhapDenNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterNgayBaoDuongTuNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterNgayBaoDuongDenNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");

                dlFilterCompany.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterBienSo.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterMaThe.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            }

            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            MyGridViewPager.PopulatePager(MyGridViewPager.TotalRecord, PageIndex, PageSize);

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
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Add); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        txtFilterBienSo.MaxLength = 50;
    }

    private void SetDefaultDateValue()
    {
        txtFilterBienSo.Text = string.Empty;
        txtFilterMaThe.Text = string.Empty;

        dlFilterCompany.SelectedIndex = 1;
        dlFilterNhomTrangThietBi.SelectedIndex = 0;
        dlFilterLoaiTrangThietBi.SelectedIndex = 0;
        dlFilterKieuBaoDuong.SelectedIndex = 0;

        txtFilterNgayNhapTuNgay.Text = DateTimeHelper.FirstDayOfCurrentYear().ToString("dd/MM/yyyy");
        txtFilterNgayNhapDenNgay.Text = DateTimeHelper.LastDayOfCurrentYear().ToString("dd/MM/yyyy");
        txtFilterNgayBaoDuongTuNgay.Text = DateTimeHelper.FirstDayOfCurrentYear().ToString("dd/MM/yyyy");
        txtFilterNgayBaoDuongDenNgay.Text = DateTimeHelper.LastDayOfCurrentYear().ToString("dd/MM/yyyy");
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
        //Ngày nhập
        DateTime ngayNhapTuNgay = DateTime.MinValue;
        DateTime ngayNhapDenNgay = DateTime.MinValue;

        if (string.IsNullOrEmpty(txtFilterNgayNhapTuNgay.Text.Trim()) || txtFilterNgayNhapTuNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Ngày nhập từ ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterNgayNhapTuNgay.Text.Trim(), out ngayNhapTuNgay))
        {
            ShowMessage(this, "Error convert <b>Ngày nhập từ ngày</b> to datetime", MessageType.Message);
            return;
        }

        if (string.IsNullOrEmpty(txtFilterNgayNhapDenNgay.Text.Trim()) || txtFilterNgayNhapDenNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Ngày nhập đến ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterNgayNhapDenNgay.Text.Trim(), out ngayNhapDenNgay))
        {
            ShowMessage(this, "Error convert <b>Ngày nhập đến ngày</b> to datetime", MessageType.Message);
            return;
        }

        if (DateTime.Compare(ngayNhapTuNgay, ngayNhapDenNgay) > 0)
        {
            ShowMessage(this, "Please enter <b>Ngày nhập từ ngày</b> is earlier than <b>Ngày nhập đến ngày</b>.", MessageType.Message);
            return;
        }

        //Ngày bảo dưỡng
        DateTime ngayBaoDuongTuNgay = DateTime.MinValue;
        DateTime ngayBaoDuongDenNgay = DateTime.MinValue;

        if (string.IsNullOrEmpty(txtFilterNgayBaoDuongTuNgay.Text.Trim()) || txtFilterNgayBaoDuongTuNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Ngày bảo dưỡng từ ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterNgayBaoDuongTuNgay.Text.Trim(), out ngayBaoDuongTuNgay))
        {
            ShowMessage(this, "Error convert <b>Ngày bảo dưỡng từ ngày</b> to datetime", MessageType.Message);
            return;
        }

        if (string.IsNullOrEmpty(txtFilterNgayBaoDuongDenNgay.Text.Trim()) || txtFilterNgayBaoDuongDenNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Ngày bảo dưỡng đến ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterNgayBaoDuongDenNgay.Text.Trim(), out ngayBaoDuongDenNgay))
        {
            ShowMessage(this, "Error convert <b>Ngày bảo dưỡng đến ngày</b> to datetime", MessageType.Message);
            return;
        }

        if (DateTime.Compare(ngayBaoDuongTuNgay, ngayBaoDuongDenNgay) > 0)
        {
            ShowMessage(this, "Please enter <b>Ngày bảo dưỡng từ ngày</b> is earlier than <b>Ngày bảo dưỡng đến ngày</b>.", MessageType.Message);
            return;
        }

        int totalRecord = 0;

        int idCongTy = 0;
        int idNhomTrangThietBi = 0;
        int idLoaiTrangThietBi = 0;
        int idKieuBaoDuong = 0;

        if (!string.IsNullOrEmpty(dlFilterCompany.SelectedValue))
            idCongTy = int.Parse(dlFilterCompany.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
            idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
            idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterKieuBaoDuong.SelectedValue))
            idKieuBaoDuong = int.Parse(dlFilterKieuBaoDuong.SelectedValue);

        DataTable dt = RepairRequestManager.SearchRepairRequest(ngayNhapTuNgay, ngayNhapDenNgay, ngayBaoDuongTuNgay, ngayBaoDuongDenNgay,
                                idCongTy, idKieuBaoDuong, idNhomTrangThietBi, idLoaiTrangThietBi, 
                                txtFilterMaThe.Text.Trim(), txtFilterBienSo.Text.Trim(),  
                                PageIndex, PageSize, SortExpression, GetSortOrder, out totalRecord);
        gridView.DataSource = dt;
        gridView.DataBind();
        gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

        //Nếu lọc loại xe cụ thể thì ẩn cột "Nhóm xe", "Loại xe"  
        if (idLoaiTrangThietBi > 0)
        {
            gridView.Columns[4].Visible = false;
            gridView.Columns[5].Visible = false;
        }
        else
        {
            gridView.Columns[5].Visible = true;
            gridView.Columns[6].Visible = true;
        }

        //Nếu lọc nhóm xe cụ thể thì ẩn cột "Nhóm xe"  
        gridView.Columns[4].Visible = (idNhomTrangThietBi > 0) ? false : true;

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
                    Label lblSoGio = e.Row.FindControl("lblSoGio") as Label;
                    if (lblSoGio != null)
                        lblSoGio.Text = NumberHelper.ToStringNumber(row["SoGio"]);

                    Label lblSoKm = e.Row.FindControl("lblSoKm") as Label;
                    if (lblSoKm != null)
                        lblSoKm.Text = NumberHelper.ToStringNumber(row["SoKm"]);

                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkEdit);
                        lnkEdit.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Edit) ||
                                        FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_View); //phân quyền sửa, xem chi tiết
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lnkDelete);
                        lnkDelete.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Repair_Request_Delete); //phân quyền xóa
                    }

                    HiddenField hfId = (HiddenField)e.Row.FindControl("hfId");
                    if (hfId != null && row["ID"] != DBNull.Value)
                        hfId.Value = row["ID"].ToString();

                    HiddenField hfMaThe = (HiddenField)e.Row.FindControl("hfMaThe");
                    if (hfMaThe != null && row["MaPhieu"] != DBNull.Value)
                        hfMaThe.Value = row["MaPhieu"].ToString();
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
                Response.Redirect(string.Format("RepairRequestDetail.aspx?ID={0}", id));
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                //Lấy giá trị Id
                hfIdToDelete.Value = e.CommandArgument.ToString();

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hfMaThe = (HiddenField)row.Cells[1].FindControl("hfMaThe");
                if (hfMaThe != null)
                    ltrContentDialog.Text = string.Format("Are you sure you want to delete item <b>{0}</b> ?", hfMaThe.Value);
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
            RepairRequestManager.DeleteRepairRequest(int.Parse(hfIdToDelete.Value));
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
            SetDefaultDateValue();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}