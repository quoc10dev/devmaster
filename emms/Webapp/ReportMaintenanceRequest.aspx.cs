using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportMaintenanceRequest : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Report_Maintenance_Request;
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
                LoadStateOfMaintenanceRequest();
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
                dlFilterTrangThai.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            }
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

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
        //btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Add); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        txtFilterBienSo.MaxLength = 50;
    }

    private void SetDefaultDateValue()
    {
        txtFilterBienSo.Text = string.Empty;
        dlFilterCompany.SelectedIndex = 1;
        dlFilterNhomTrangThietBi.SelectedIndex = 0;
        dlFilterLoaiTrangThietBi.SelectedIndex = 0;
        dlFilterKieuBaoDuong.SelectedIndex = 0;
        dlFilterTrangThai.SelectedIndex = 0;

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

    private void LoadStateOfMaintenanceRequest()
    {
        dlFilterTrangThai.Items.Clear();
        dlFilterTrangThai.Items.Add(new ListItem("--- Select All ---", string.Empty));
        dlFilterTrangThai.Items.Add(new ListItem(MaintenanceRequestValue.NhapMoi, MaintenanceRequestState.NhapMoi));
        dlFilterTrangThai.Items.Add(new ListItem(MaintenanceRequestValue.HoanThanh, MaintenanceRequestState.HoanThanh));
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

        DataTable dt = MaintenanceRequestManager.ReportMaintenanceRequest(ngayNhapTuNgay, ngayNhapDenNgay, ngayBaoDuongTuNgay, ngayBaoDuongDenNgay,
                                idCongTy, idKieuBaoDuong, idNhomTrangThietBi, idLoaiTrangThietBi,
                                txtFilterBienSo.Text.Trim(), dlFilterTrangThai.SelectedValue);
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
    }

    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "50px");

                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    HyperLink hpTheBaoDuong = e.Row.FindControl("hpTheBaoDuong") as HyperLink;
                    if (hpTheBaoDuong != null)
                    {
                        hpTheBaoDuong.Text = row["MaThe"].ToString();
                        hpTheBaoDuong.NavigateUrl = string.Format("../MaintenanceRequestDetail.aspx?ID={0}", Convert.ToInt32(row["IDTheBaoDuong"]));
                    }

                    HyperLink hpBienSo = e.Row.FindControl("hlBienSo") as HyperLink;
                    if (hpBienSo != null)
                    {
                        hpBienSo.Text = row["BienSo"].ToString();
                        hpBienSo.NavigateUrl = string.Format("../EquipmentDetail.aspx?ID={0}", Convert.ToInt32(row["IDTrangThietBi"]));
                    }

                    Label lblTrangThai = e.Row.FindControl("lblTrangThai") as Label;
                    if (lblTrangThai != null)
                        lblTrangThai.Text = MaintenanceRequestManager.GetStateOfMaintenanceRequest(row["TrangThaiKhaiThac"].ToString());
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected override void GridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
        }
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