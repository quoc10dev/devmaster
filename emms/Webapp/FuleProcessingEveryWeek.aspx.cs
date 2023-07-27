using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FuleProcessingEveryWeek : BaseAdminGridPage
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

                showGrid.Visible = true;
                showGridImport.Visible = false;

                SetDefaultDateValue();
                CompanyManager.LoadCompanyForSearch(dlFilterCompany);
                EquipmentGroupManager.LoadEquipmentGroupForSearch(dlFilterNhomTrangThietBi);
                LoadEquipmentType();
                BindData();

                //Khi bấm enter trên các textbox sẽ tự động bấm nút Filter
                txtFilterTuNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterDenNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterCompany.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterBienSo.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");

                // Xử lý lưu dữ liệu và PostBack
                btnInsert.OnClientClick = "Control.Destroy('#" + UpdatePanel1.ClientID + "')";
                btnSave.OnClientClick = "Control.Destroy('#" + UpdatePanel1.ClientID + "')";

                Session["fileUpload_FuleProcessingEveryWeek"] = null;
                Page.Form.Attributes.Add("enctype", "multipart/form-data"); //có lệnh này để tránh thông báo HasFile = false khi bấm nút Read Data lần đầu
            }
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Thiết lập bắt buộc nhập đúng format cho các control tìm kiếm datetime, number
            //Khởi tạo: Control.Build(el);
            //Hủy: Control.Destroy(el);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageControl", "Control.Build(" + UpdatePanel1.ClientID + ")", true);


            //LẤY TÊN FILE EXCEL ĐÃ CHỌN - KHI CÓ POSTBACK
            //Ban đầu khi load trang thì không nhảy vào lệnh này do fileUpload.HasFile = false
            if (Session["fileUpload_FuleProcessingEveryWeek"] == null && fileUpload.HasFile)
            {
                //Khi bấm nút Read Data lần đầu (chọn file lần đầu)
                Session["fileUpload_FuleProcessingEveryWeek"] = fileUpload;
            }
            else if (Session["fileUpload_FuleProcessingEveryWeek"] != null && (!fileUpload.HasFile))
            {
                //Không chọn file nhưng bấm các nút khác khiển PostBack
                fileUpload = (FileUpload)Session["fileUpload_FuleProcessingEveryWeek"];
            }
            else if (fileUpload.HasFile)
            {
                //Thay đổi file khác
                Session["fileUpload_FuleProcessingEveryWeek"] = fileUpload;
            }
            ltrFileName.Text = string.Format("<b>File đang đọc:</b> {0}", fileUpload.FileName);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnSave.Enabled = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.OperationProcessing_Add) ||
                          FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.OperationProcessing_Edit);
    }

    private void SetMaxLength()
    {
        txtFilterBienSo.MaxLength = 50;
    }

    private void SetDefaultDateValue()
    {
        txtFilterTuNgay.Text = DateTime.Now.AddDays(-6).ToString("dd/MM/yyyy");
        txtFilterDenNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

    public DateTime FromDate
    {
        get
        {
            if (ViewState["FromDate"] != null)
            {
                return Convert.ToDateTime(ViewState["FromDate"]);
            }
            return DateTime.MinValue;
        }
    }

    public DateTime ToDate
    {
        get
        {
            if (ViewState["ToDate"] != null)
            {
                return Convert.ToDateTime(ViewState["ToDate"]);
            }
            return DateTime.MinValue;
        }
    }

    public override void BindData()
    {
        DateTime tuNgay = DateTime.MinValue;
        DateTime denNgay = DateTime.MinValue;

        if (string.IsNullOrEmpty(txtFilterTuNgay.Text.Trim()) || txtFilterTuNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Từ ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterTuNgay.Text.Trim(), out tuNgay))
        {
            ShowMessage(this, "Error convert <b>Từ ngày</b> to datetime", MessageType.Message);
            return;
        }

        denNgay = tuNgay.AddDays(6); //chỉ cho tìm kiếm trong phạm vi 1 tuần
        txtFilterDenNgay.Text = denNgay.ToString("dd/MM/yyyy");

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

        ViewState["FromDate"] = tuNgay;
        ViewState["ToDate"] = denNgay;

        DataTable dt = FuleProcessingManager.SearchFuleProcessingEveryWeek(tuNgay, denNgay, idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, txtFilterBienSo.Text.Trim());
        gridView.DataSource = dt;
        gridView.DataBind();
        gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

        //Nếu lọc loại xe cụ thể thì ẩn cột "Nhóm xe", "Loại xe"  
        if (idLoaiTrangThietBi > 0)
        {
            gridView.Columns[1].Visible = false;
            gridView.Columns[2].Visible = false;
        }
        else
        {
            gridView.Columns[1].Visible = true;
            gridView.Columns[2].Visible = true;
        }

        //Nếu lọc nhóm xe cụ thể thì ẩn cột "Nhóm xe"  
        gridView.Columns[1].Visible = (idNhomTrangThietBi > 0) ? false : true;
    }

    private string ReturnFormatColumnName(DateTime date)
    {
        return string.Format("{0:dd}/{1:MM}/{2:yyyy}", date, date, date);
    }

    private string ReturnFormatDataTableColumnName(DateTime date)
    {
        return string.Format("{0:yyyy}{1:MM}{2:dd}", date, date, date);
    }

    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DateTime tuNgay = FromDate;

                Label lblDate = e.Row.FindControl("lblDate1") as Label;
                if (lblDate != null)
                    lblDate.Text = ReturnFormatColumnName(tuNgay);

                tuNgay = tuNgay.AddDays(1);
                lblDate = e.Row.FindControl("lblDate2") as Label;
                if (lblDate != null)
                    lblDate.Text = ReturnFormatColumnName(tuNgay);

                tuNgay = tuNgay.AddDays(1);
                lblDate = e.Row.FindControl("lblDate3") as Label;
                if (lblDate != null)
                    lblDate.Text = ReturnFormatColumnName(tuNgay);

                tuNgay = tuNgay.AddDays(1);
                lblDate = e.Row.FindControl("lblDate4") as Label;
                if (lblDate != null)
                    lblDate.Text = ReturnFormatColumnName(tuNgay);

                tuNgay = tuNgay.AddDays(1);
                lblDate = e.Row.FindControl("lblDate5") as Label;
                if (lblDate != null)
                    lblDate.Text = ReturnFormatColumnName(tuNgay);

                tuNgay = tuNgay.AddDays(1);
                lblDate = e.Row.FindControl("lblDate6") as Label;
                if (lblDate != null)
                    lblDate.Text = ReturnFormatColumnName(tuNgay);

                tuNgay = tuNgay.AddDays(1);
                lblDate = e.Row.FindControl("lblDate7") as Label;
                if (lblDate != null)
                    lblDate.Text = ReturnFormatColumnName(tuNgay);
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    DateTime fromDate = FromDate;

                    HiddenField hfId = (HiddenField)e.Row.FindControl("hfId");
                    if (hfId != null && row["IDTrangThietBi"] != DBNull.Value)
                        hfId.Value = row["IDTrangThietBi"].ToString();

                    Label lblSTT = e.Row.FindControl("lblSTT") as Label;
                    if (lblSTT != null)
                        lblSTT.Text = (e.Row.RowIndex + 1).ToString();

                    HiddenField hfIDTrangThietBi = e.Row.FindControl("hfIDTrangThietBi") as HiddenField;
                    if (hfIDTrangThietBi != null)
                        hfIDTrangThietBi.Value = row["IDTrangThietBi"].ToString();

                    Label lblBienSo = e.Row.FindControl("lblBienSo") as Label;
                    if (lblBienSo != null)
                        lblBienSo.Text = row["BienSo"].ToString();

                    //Ngày 1
                    string columnName = ReturnFormatDataTableColumnName(fromDate);
                    TextBox txtDate = e.Row.FindControl("txtDate1") as TextBox;
                    if (txtDate != null)
                        txtDate.Text = row[columnName].ToString();

                    HiddenField hfDate = e.Row.FindControl("hfDate1") as HiddenField;
                    if (hfDate != null)
                        hfDate.Value = columnName;

                    //Ngày 2
                    fromDate = fromDate.Date.AddDays(1);
                    columnName = ReturnFormatDataTableColumnName(fromDate);
                    txtDate = e.Row.FindControl("txtDate2") as TextBox;
                    if (txtDate != null)
                        txtDate.Text = row[columnName].ToString();

                    hfDate = e.Row.FindControl("hfDate2") as HiddenField;
                    if (hfDate != null)
                        hfDate.Value = columnName;

                    //Ngày 3
                    fromDate = fromDate.Date.AddDays(1);
                    columnName = ReturnFormatDataTableColumnName(fromDate);
                    txtDate = e.Row.FindControl("txtDate3") as TextBox;
                    if (txtDate != null)
                        txtDate.Text = row[columnName].ToString();

                    hfDate = e.Row.FindControl("hfDate3") as HiddenField;
                    if (hfDate != null)
                        hfDate.Value = columnName;

                    //Ngày 4
                    fromDate = fromDate.Date.AddDays(1);
                    columnName = ReturnFormatDataTableColumnName(fromDate);
                    txtDate = e.Row.FindControl("txtDate4") as TextBox;
                    if (txtDate != null)
                        txtDate.Text = row[columnName].ToString();

                    hfDate = e.Row.FindControl("hfDate4") as HiddenField;
                    if (hfDate != null)
                        hfDate.Value = columnName;

                    //Ngày 5
                    fromDate = fromDate.Date.AddDays(1);
                    columnName = ReturnFormatDataTableColumnName(fromDate);
                    txtDate = e.Row.FindControl("txtDate5") as TextBox;
                    if (txtDate != null)
                        txtDate.Text = row[columnName].ToString();

                    hfDate = e.Row.FindControl("hfDate5") as HiddenField;
                    if (hfDate != null)
                        hfDate.Value = columnName;

                    //Ngày 6
                    fromDate = fromDate.Date.AddDays(1);
                    columnName = ReturnFormatDataTableColumnName(fromDate);
                    txtDate = e.Row.FindControl("txtDate6") as TextBox;
                    if (txtDate != null)
                        txtDate.Text = row[columnName].ToString();

                    hfDate = e.Row.FindControl("hfDate6") as HiddenField;
                    if (hfDate != null)
                        hfDate.Value = columnName;

                    //Ngày 7
                    fromDate = fromDate.Date.AddDays(1);
                    columnName = ReturnFormatDataTableColumnName(fromDate);
                    txtDate = e.Row.FindControl("txtDate7") as TextBox;
                    if (txtDate != null)
                        txtDate.Text = row[columnName].ToString();

                    hfDate = e.Row.FindControl("hfDate7") as HiddenField;
                    if (hfDate != null)
                        hfDate.Value = columnName;
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
            PageIndex = 1;
            BindData();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        DateTime tuNgay = DateTime.MinValue;
        if (string.IsNullOrEmpty(txtFilterTuNgay.Text.Trim()) || txtFilterTuNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Từ ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterTuNgay.Text.Trim(), out tuNgay))
        {
            ShowMessage(this, "Error convert <b>Từ ngày</b> to datetime", MessageType.Message);
            return;
        }

        tuNgay = tuNgay.AddDays(6);
        txtFilterTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
        txtFilterDenNgay.Text = tuNgay.AddDays(6).ToString("dd/MM/yyyy");
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        DateTime tuNgay = DateTime.MinValue;
        if (string.IsNullOrEmpty(txtFilterTuNgay.Text.Trim()) || txtFilterTuNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Từ ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtFilterTuNgay.Text.Trim(), out tuNgay))
        {
            ShowMessage(this, "Error convert <b>Từ ngày</b> to datetime", MessageType.Message);
            return;
        }

        tuNgay = tuNgay.AddDays(-6);
        txtFilterTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
        txtFilterDenNgay.Text = tuNgay.AddDays(6).ToString("dd/MM/yyyy");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtFilterBienSo.Text = string.Empty;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Kiểm tra không cho phép nhập số âm
            double soLuong = 0;
            foreach (GridViewRow row in gridView.Rows)
            {
                //Ngày 1
                TextBox txtSoGio_SoKm = row.FindControl("txtDate1") as TextBox;
                if (txtSoGio_SoKm != null && !string.IsNullOrEmpty(txtSoGio_SoKm.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong))
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is a number", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                    if (soLuong <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is greater than 0", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                }

                //Ngày 2
                txtSoGio_SoKm = row.FindControl("txtDate2") as TextBox;
                if (txtSoGio_SoKm != null && !string.IsNullOrEmpty(txtSoGio_SoKm.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong))
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is a number", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                    if (soLuong <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is greater than 0", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                }

                //Ngày 3
                txtSoGio_SoKm = row.FindControl("txtDate3") as TextBox;
                if (txtSoGio_SoKm != null && !string.IsNullOrEmpty(txtSoGio_SoKm.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong))
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is a number", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                    if (soLuong <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is greater than 0", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                }

                //Ngày 4
                txtSoGio_SoKm = row.FindControl("txtDate4") as TextBox;
                if (txtSoGio_SoKm != null && !string.IsNullOrEmpty(txtSoGio_SoKm.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong))
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is a number", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                    if (soLuong <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is greater than 0", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                }

                //Ngày 5
                txtSoGio_SoKm = row.FindControl("txtDate5") as TextBox;
                if (txtSoGio_SoKm != null && !string.IsNullOrEmpty(txtSoGio_SoKm.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong))
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is a number", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                    if (soLuong <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is greater than 0", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                }

                //Ngày 6
                txtSoGio_SoKm = row.FindControl("txtDate6") as TextBox;
                if (txtSoGio_SoKm != null && !string.IsNullOrEmpty(txtSoGio_SoKm.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong))
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is a number", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                    if (soLuong <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is greater than 0", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                }

                //Ngày 7
                txtSoGio_SoKm = row.FindControl("txtDate7") as TextBox;
                if (txtSoGio_SoKm != null && !string.IsNullOrEmpty(txtSoGio_SoKm.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong))
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is a number", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                    if (soLuong <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số lượng (lít)</b> is greater than 0", MessageType.Message);
                        txtSoGio_SoKm.Focus();
                        return;
                    }
                }
            }

            //Cập nhật số km/ số giờ xuống database
            foreach (GridViewRow row in gridView.Rows)
            {
                //Ngày ở cột 1
                HiddenField hfIDTrangThietBi = row.FindControl("hfIDTrangThietBi") as HiddenField;
                HiddenField hfDate = row.FindControl("hfDate1") as HiddenField;
                TextBox txtSoGio_SoKm = row.FindControl("txtDate1") as TextBox;
                if (txtSoGio_SoKm != null && hfDate != null && hfIDTrangThietBi != null)
                {
                    if (!string.IsNullOrEmpty(txtSoGio_SoKm.Text.Trim()))
                    {
                        NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong);
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, soLuong);
                    }
                    else
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, null);
                }

                //Ngày ở cột 2
                hfDate = row.FindControl("hfDate2") as HiddenField;
                txtSoGio_SoKm = row.FindControl("txtDate2") as TextBox;
                if (txtSoGio_SoKm != null && hfDate != null && hfIDTrangThietBi != null)
                {
                    if (!string.IsNullOrEmpty(txtSoGio_SoKm.Text.Trim()))
                    {
                        NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong);
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, soLuong);
                    }
                    else
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, null);
                }

                //Ngày ở cột 3
                hfDate = row.FindControl("hfDate3") as HiddenField;
                txtSoGio_SoKm = row.FindControl("txtDate3") as TextBox;
                if (txtSoGio_SoKm != null && hfDate != null && hfIDTrangThietBi != null)
                {
                    if (!string.IsNullOrEmpty(txtSoGio_SoKm.Text.Trim()))
                    {
                        NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong);
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, soLuong);
                    }
                    else
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, null);
                }

                //Ngày ở cột 4
                hfDate = row.FindControl("hfDate4") as HiddenField;
                txtSoGio_SoKm = row.FindControl("txtDate4") as TextBox;
                if (txtSoGio_SoKm != null && hfDate != null && hfIDTrangThietBi != null)
                {
                    if (!string.IsNullOrEmpty(txtSoGio_SoKm.Text.Trim()))
                    {
                        NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong);
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, soLuong);
                    }
                    else
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, null);
                }

                //Ngày ở cột 5
                hfDate = row.FindControl("hfDate5") as HiddenField;
                txtSoGio_SoKm = row.FindControl("txtDate5") as TextBox;
                if (txtSoGio_SoKm != null && hfDate != null && hfIDTrangThietBi != null)
                {
                    if (!string.IsNullOrEmpty(txtSoGio_SoKm.Text.Trim()))
                    {
                        NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong);
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, soLuong);
                    }
                    else
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, null);
                }

                //Ngày ở cột 6
                hfDate = row.FindControl("hfDate6") as HiddenField;
                txtSoGio_SoKm = row.FindControl("txtDate6") as TextBox;
                if (txtSoGio_SoKm != null && hfDate != null && hfIDTrangThietBi != null)
                {
                    if (!string.IsNullOrEmpty(txtSoGio_SoKm.Text.Trim()))
                    {
                        NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong);
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, soLuong);
                    }
                    else
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, null);
                }

                //Ngày ở cột 7
                hfDate = row.FindControl("hfDate7") as HiddenField;
                txtSoGio_SoKm = row.FindControl("txtDate7") as TextBox;
                if (txtSoGio_SoKm != null && hfDate != null && hfIDTrangThietBi != null)
                {
                    if (!string.IsNullOrEmpty(txtSoGio_SoKm.Text.Trim()))
                    {
                        NumberHelper.ConvertToDouble(txtSoGio_SoKm.Text.Trim(), out soLuong);
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, soLuong);
                    }
                    else
                        FuleProcessingManager.UpdateNapNhienLieu(int.Parse(hfIDTrangThietBi.Value), hfDate.Value, null);
                }
            }
            this.BindData();
            ShowMessage(this, "Update successfully", MessageType.Message);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion

    #region Import from excel

    protected void chkSelectImport_CheckedChanged(Object sender, EventArgs args)
    {
        if (chkSelectImport.Checked)
        {
            divFilter.Visible = false;
            showGrid.Visible = false;
            showGridImport.Visible = true;
            divUpload.Visible = true;
        }
        else
        {
            divFilter.Visible = true;
            showGrid.Visible = true;
            showGridImport.Visible = false;
            divUpload.Visible = false;
        }
    }

    protected void btnImportFromExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (!fileUpload.HasFile)
                ShowMessage(this, "Please select a file to upload", MessageType.Message);
            else
            {
                string filename = Path.GetFileName(fileUpload.FileName);
                if (Path.GetExtension(fileUpload.FileName).ToLower().Equals(".xlsx") ||
                    Path.GetExtension(fileUpload.FileName).ToLower().Equals(".xls"))
                {
                    string fullPath = string.Empty;
                    fullPath = Server.MapPath("~/Upload/") + filename;
                    fileUpload.SaveAs(fullPath);

                    DataTable dt = ReadFromExcelFile(fullPath);
                    gridExcelData.DataSource = dt;
                    gridExcelData.DataBind();
                }
                else
                    ShowMessage(this, "Please upload excel format file.", MessageType.Message);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(this, ex.Message, MessageType.Message);
        }
    }

    private List<string> ColumnList
    {
        get
        {
            if (ViewState["colList"] != null)
            {
                List<string> result = ViewState["colList"] as List<string>;
                return result;
            }

            return new List<string>();
        }
    }

    private DataTable ReadFromExcelFile(string path)
    {
        List<string> colList = new List<string>();

        // Khởi tạo data table
        DataTable dt = new DataTable();

        // Load file excel và các setting ban đầu
        using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
        {
            //Kiểm tra không có sheet nào tồn tại
            if (package.Workbook.Worksheets.Count == 0)
                return null;

            var workbook = package.Workbook;
            var workSheet = workbook.Worksheets.FirstOrDefault();

            int totalRows = workSheet.Dimension.End.Row;
            int totalCols = workSheet.Dimension.End.Column;

            // Đọc tất cả các header --> tạo cột cho datatable
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, totalCols])
            {
                DateTime outputDateTimeValue;
                if (DateTime.TryParseExact(firstRowCell.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out outputDateTimeValue))
                {
                    dt.Columns.Add(outputDateTimeValue.ToString("dd/MM/yyyy"));
                    colList.Add(outputDateTimeValue.ToString("yyyyMMdd"));
                }
                else
                {
                    // Handle the fact that parse did not succeed
                    dt.Columns.Add(firstRowCell.Text);
                    colList.Add(firstRowCell.Text);
                }
            }

            // Đọc tất cả data bắt đầu từ row thứ 2
            for (var rowNumber = 2; rowNumber <= totalRows; rowNumber++)
            {
                // Lấy 1 row trong excel để truy vấn
                var row = workSheet.Cells[rowNumber, 1, rowNumber, totalCols];

                // tạo 1 row trong data table
                var newRow = dt.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(newRow);
            }
        }

        ViewState["colList"] = colList;
        return dt;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            bool hasError = true;
            int numberCells = 0;
            double soLit = 0;
            string bienSo = string.Empty;

            StringBuilder totalError = new StringBuilder();
            string errorDate = string.Empty;
            string errorText = string.Empty;

            foreach (GridViewRow row in gridExcelData.Rows)
            {
                numberCells = row.Cells.Count;

                //Kiểm tra biển số, lấy IDTrangThietBi
                bienSo = row.Cells[1].Text;
                if (!string.IsNullOrEmpty(bienSo))
                {
                    tblTrangThietBi equipment = EquipmentManager.GetEquipmentByLicensePlate(bienSo);
                    if (equipment != null)
                    {
                        for (int i = 2; i < numberCells; i++)
                        {
                            errorText = row.Cells[i].Text.Trim();

                            if (!string.IsNullOrEmpty(row.Cells[i].Text) && !errorText.Equals("&nbsp;"))
                            {
                                if (NumberHelper.ConvertToDouble(row.Cells[i].Text, out soLit))
                                    FuleProcessingManager.UpdateNapNhienLieu(equipment.ID, ColumnList[i], soLit);
                                else
                                {
                                    totalError.AppendFormat("Biển số: {0} - Ngày: {1} - Giá trị lỗi: {2}<br/>", bienSo, errorDate, errorText);
                                    totalError.AppendLine();
                                    hasError = false;
                                }
                            }
                        }
                    }
                }
            }
            if (hasError)
                ShowMessage(this, "Lưu thành công", MessageType.Message);
            else
                ShowMessage(this, string.Format("Có lỗi trên các dòng:<br/> {0}", totalError.ToString()), MessageType.Message);
        }
        catch (Exception ex)
        {
            ShowMessage(this, ex.Message, MessageType.Message);
        }
    }

    #endregion
}