using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportOperationProcessing : BaseAdminGridPage
{
    public class GridViewTemplate : ITemplate
    {
        private DataControlRowType templateType;
        private string columnName;
        private string columnNameBinding;
        private string controlType;
        private string ID;
        private string TextAlign;
        private int Width;

        public GridViewTemplate(DataControlRowType type, string colname, string colNameBinding, string ctlType, string IDControl, string textAlign, int width)
        {
            templateType = type;
            columnName = colname;
            columnNameBinding = colNameBinding;
            controlType = ctlType;
            ID = IDControl;
            TextAlign = textAlign;
            Width = width;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    Literal lc = new Literal();
                    lc.Text = columnName;
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow:
                    if (controlType == "Label")
                    {
                        Label lb = new Label();
                        lb.ID = ID;
                        lb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        lb.Width = Width;
                        //lb.Width = Unit.Pixel(100);
                        if (TextAlign.Equals("left"))
                            lb.Style.Add("text-align", "left");
                        else if (TextAlign.Equals("right"))
                            lb.Style.Add("text-align", "right");
                        else if (TextAlign.Equals("center"))
                            lb.Style.Add("text-align", "center");
                        container.Controls.Add(lb);
                    }
                    else if (controlType == "TextBox")
                    {
                        TextBox tb = new TextBox();
                        //tb.ID = "tb" + columnNameBinding;
                        tb.ID = ID;
                        tb.MaxLength = 10;
                        tb.Width = Unit.Pixel(70);
                        tb.Style.Add("text-align", "center");

                        tb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        container.Controls.Add(tb);
                    }
                    else if (controlType == "CheckBox")
                    {
                        CheckBox cb = new CheckBox();
                        cb.ID = "cb1";
                        cb.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        container.Controls.Add(cb);
                    }
                    else if (controlType == "HyperLink")
                    {
                        HyperLink hl = new HyperLink();
                        hl.ID = "hl1";
                        hl.DataBinding += new EventHandler(this.ctl_OnDataBinding);
                        container.Controls.Add(hl);
                    }
                    break;
                default:
                    break;
            }
        }

        public void ctl_OnDataBinding(object sender, EventArgs e)
        {
            if (sender.GetType().Name == "Label")
            {
                Label lb = (Label)sender;
                GridViewRow container = (GridViewRow)lb.NamingContainer;
                lb.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
            }
            else if (sender.GetType().Name == "TextBox")
            {
                TextBox tb = (TextBox)sender;
                GridViewRow container = (GridViewRow)tb.NamingContainer;
                tb.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
            }
            else if (sender.GetType().Name == "CheckBox")
            {
                CheckBox cb = (CheckBox)sender;
                GridViewRow container = (GridViewRow)cb.NamingContainer;
                cb.Checked = Convert.ToBoolean(((DataRowView)container.DataItem)[columnNameBinding].ToString());
            }
            else if (sender.GetType().Name == "HyperLink")
            {
                HyperLink hl = (HyperLink)sender;
                GridViewRow container = (GridViewRow)hl.NamingContainer;
                hl.Text = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
                hl.NavigateUrl = ((DataRowView)container.DataItem)[columnNameBinding].ToString();
            }
        }
    }

    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Report_Operation_Processing;
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

                //Khi bấm enter trên các textbox sẽ tự động bấm nút Filter
                txtFilterTuNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterDenNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterCompany.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterBienSo.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                //btnSave.OnClientClick = "Control.Destroy('#" + showGrid.ClientID + "')";
            }
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Thiết lập bắt buộc nhập đúng format cho các control tìm kiếm datetime, number
            //Khởi tạo: Control.Build(el);
            //Hủy: Control.Destroy(el);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls",
                                                   "if(typeof(Control)!='undefined') Control.Build(" + UpdatePanel1.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        //btnSave.Enabled = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.OperationProcessing_Add) ||
        //                  FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.OperationProcessing_Edit);
    }

    private void SetMaxLength()
    {
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
        BindData();
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

        if (string.IsNullOrEmpty(txtFilterDenNgay.Text.Trim()) || txtFilterDenNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Đến ngày</b> is a datetime", MessageType.Message);
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

        ViewState["FromDate"] = tuNgay;
        ViewState["ToDate"] = denNgay;

        DataTable dt = OperationProcessingManager.SearchOperationProcessingTest(tuNgay, denNgay, idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, txtFilterBienSo.Text.Trim());

        gridView.Columns.Clear();

        TemplateField field = new TemplateField();
        field.ShowHeader = true;
        field.HeaderTemplate = new GridViewTemplate(DataControlRowType.Header, "Nhóm xe", "", "", "lblIDTrangThietBi", "center", 200);
        field.ItemTemplate = new GridViewTemplate(DataControlRowType.DataRow, "", "TenNhom", "Label", "", "left", 200);
        gridView.Columns.Add(field);

        field = new TemplateField();
        field.ShowHeader = true;
        field.HeaderTemplate = new GridViewTemplate(DataControlRowType.Header, "Loại xe", "", "", "lblBienSo", "center", 150);
        field.ItemTemplate = new GridViewTemplate(DataControlRowType.DataRow, "", "TenLoai", "Label", "", "left", 150);
        gridView.Columns.Add(field);

        field = new TemplateField();
        field.ShowHeader = true;
        field.HeaderTemplate = new GridViewTemplate(DataControlRowType.Header, "Biển số", "", "", "lblBienSo", "center", 90);
        field.ItemTemplate = new GridViewTemplate(DataControlRowType.DataRow, "", "BienSo", "Label", "", "left", 90);
        gridView.Columns.Add(field);

        List<string> colList = new List<string>();

        for (int i = 6; i <= dt.Columns.Count - 1; i++)
        {
            string columnName = dt.Columns[i].ColumnName;
            colList.Add(columnName);

            string headerName = string.Format("{0}/{1}/{2}", columnName.Substring(6, 2), columnName.Substring(4, 2), columnName.Substring(0, 4));

            field = new TemplateField();
            field.HeaderStyle.Width = Unit.Pixel(80);
            field.ShowHeader = true;
            field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            field.HeaderTemplate = new GridViewTemplate(DataControlRowType.Header, headerName, "", "", string.Format("{0}{1}", "lbl", columnName), "center", 70);
            field.ItemTemplate = new GridViewTemplate(DataControlRowType.DataRow, "", columnName, "Label", "", "center", 70);
            gridView.Columns.Add(field);
        }

        gridView.DataSource = dt;
        gridView.DataBind();
        gridView.HeaderRow.TableSection = TableRowSection.TableHeader;

        //Nếu lọc loại xe cụ thể thì ẩn cột "Nhóm xe", "Loại xe"  
        if (idLoaiTrangThietBi > 0)
        {
            gridView.Columns[0].Visible = false;
            gridView.Columns[1].Visible = false;
        }
        else
        {
            gridView.Columns[0].Visible = true;
            gridView.Columns[1].Visible = true;
        }

        //Nếu lọc nhóm xe cụ thể thì ẩn cột "Nhóm xe"  
        gridView.Columns[0].Visible = (idNhomTrangThietBi > 0) ? false : true;
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
        set
        {
            ColumnList = value;
        }
    }

    private string ReturnFormatColumnName(DateTime date)
    {
        return string.Format("{0:dd}/{1:MM}/{2:yyyy}", date, date, date);
    }

    private string ReturnFormatDataTableColumnName(DateTime date)
    {
        return string.Format("{0:yyyy}{1:MM}{2:dd}", date, date, date);
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
        try
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

            //Tăng lên 1 tháng
            tuNgay = DateTimeHelper.FirstDayOfMonth(tuNgay);
            tuNgay = tuNgay.AddMonths(1);
            txtFilterTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
            txtFilterDenNgay.Text = DateTimeHelper.LastDayOfMonth(tuNgay).ToString("dd/MM/yyyy");

            BindData();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
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

            //Lùi lại 1 tháng
            tuNgay = DateTimeHelper.FirstDayOfMonth(tuNgay);
            tuNgay = tuNgay.AddMonths(-1);
            txtFilterTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
            txtFilterDenNgay.Text = DateTimeHelper.LastDayOfMonth(tuNgay).ToString("dd/MM/yyyy");

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

            txtFilterBienSo.Text = string.Empty;
            dlFilterNhomTrangThietBi.SelectedIndex = 0;
            dlFilterLoaiTrangThietBi.SelectedIndex = 0;
            dlFilterCompany.SelectedIndex = 1;
            SetDefaultDateValue();

            BindData();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    #endregion
}