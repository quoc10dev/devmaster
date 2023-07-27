using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportFuelConsumption : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Fuel_Consumption_Report;
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
                //SetMaxLength();

                SetDefaultDateValue();
                CompanyManager.LoadCompanyForSearch(dlFilterCompany);
                EquipmentGroupManager.LoadEquipmentGroupForSearch(dlFilterNhomTrangThietBi);
                LoadEquipmentType();

                gvList.DataSource = new DataTable();
                gvList.DataBind();

                txtFilterTuNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterDenNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterCompany.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            }
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls", "Control.Build(" + formatControl.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnAddNew.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Fuel_Consumption_Report); //Phân quyền thêm
        btnExport.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Fuel_Consumption_Report); //Phân quyền thêm
    }

    private void SetMaxLength()
    {
        
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

        int idCongTy = 0;
        int idNhomTrangThietBi = 0;
        int idLoaiTrangThietBi = 0;

        if (!string.IsNullOrEmpty(dlFilterCompany.SelectedValue))
            idCongTy = int.Parse(dlFilterCompany.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
            idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
            idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

        DataTable dt = EquipmentManager.ReportTinhDinhMucNhienLieu(idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, tuNgay, denNgay);
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
                    Label lblDinhMuc = e.Row.FindControl("lblDinhMuc") as Label;
                    if (lblDinhMuc != null)
                        lblDinhMuc.Text = NumberHelper.ToStringNumber(row["DinhMuc"]);

                    Label lblDinhMucBanHanh = e.Row.FindControl("lblDinhMucBanHanh") as Label;
                    if (lblDinhMucBanHanh != null)
                        lblDinhMucBanHanh.Text = NumberHelper.ToStringNumber(row["DinhMucBanHanh"]);
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion

    #region button

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
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
            dlFilterLoaiTrangThietBi.Items.Clear();
            dlFilterNhomTrangThietBi.Items.Clear();
            EquipmentGroupManager.LoadEquipmentGroupForSearch(dlFilterNhomTrangThietBi);
            LoadEquipmentType();

            dlFilterNhomTrangThietBi.SelectedIndex = 0;
            dlFilterLoaiTrangThietBi.SelectedIndex = 0;
            dlFilterCompany.SelectedIndex = 1;
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

            int idCongTy = 0;
            int idNhomTrangThietBi = 0;
            int idLoaiTrangThietBi = 0;

            if (!string.IsNullOrEmpty(dlFilterCompany.SelectedValue))
                idCongTy = int.Parse(dlFilterCompany.SelectedValue);

            if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
                idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

            if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
                idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

            string url = string.Format("ReportFuelConsumptionExport.aspx?IDCongTy={0}&IDNhomTrangThietBi={1}&IDLoaiTrangThietBi={2}&TuNgay={3}&DenNgay={4}",
                                    idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, 
                                    string.Format("{0:yyyyMMdd}",tuNgay), string.Format("{0:yyyyMMdd}", denNgay));
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