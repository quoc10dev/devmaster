using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportMaintenancePlan : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Report_Maintenance_Plan;
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

                SetDefaultDateValue();

                //gvList.DataSource = new DataTable();
                //gvList.DataBind();

                BindData();

                dlFilterCompany.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterBienSo.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtTanSuatTuNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtTanSuatDenNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtKeHoachTuNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtKeHoachDenNgay.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            }
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Thiết lập bắt buộc nhập đúng format cho các control tìm kiếm datetime, number
            //Khởi tạo: Control.Build(el);
            //Hủy: Control.Destroy(el);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageControl",
                                                   "if(typeof(Control)!='undefined') Control.Build(" + UpdatePanel1.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetDefaultDateValue()
    {
        txtKeHoachTuNgay.Text = DateTimeHelper.FirstDayOfMonth(DateTime.Now).ToString("dd/MM/yyyy");
        txtKeHoachDenNgay.Text = DateTimeHelper.LastDayOfMonth(DateTime.Now).ToString("dd/MM/yyyy");
    }

    private void Permission()
    {
        btnExport.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Report_Maintenance_Plan_View);
    }

    private void SetMaxLength()
    {
        txtFilterBienSo.MaxLength = 50;
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

    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    HyperLink hpBienSo = e.Row.FindControl("hlBienSo") as HyperLink;
                    if (hpBienSo != null)
                    {
                        hpBienSo.Text = row["BienSo"].ToString();
                        hpBienSo.NavigateUrl = string.Format("../EquipmentDetail.aspx?ID={0}", Convert.ToInt32(row["IDTrangThietBi"]));
                    }

                    Label lblSoKmGioDaChay = e.Row.FindControl("lblSoKmGioDaChay") as Label;
                    if (lblSoKmGioDaChay != null)
                    {
                        if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Gio))
                            lblSoKmGioDaChay.Text = string.Format("{0} giờ", NumberHelper.ToStringNumber(row["SoKmGioDaChay"]));
                        else if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Km))
                            lblSoKmGioDaChay.Text = string.Format("{0} km", NumberHelper.ToStringNumber(row["SoKmGioDaChay"]));
                        else
                            lblSoKmGioDaChay.Text = string.Empty;
                    }

                    Label lblSoKmGioNhapGanNhat = e.Row.FindControl("lblSoKmGioNhapGanNhat") as Label;
                    if (lblSoKmGioNhapGanNhat != null)
                    {
                        if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Gio))
                            lblSoKmGioNhapGanNhat.Text = string.Format("{0} giờ", NumberHelper.ToStringNumber(row["SoKmGioNhapGanNhat"]));
                        else if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Km))
                            lblSoKmGioNhapGanNhat.Text = string.Format("{0} km", NumberHelper.ToStringNumber(row["SoKmGioNhapGanNhat"]));
                        else
                            lblSoKmGioNhapGanNhat.Text = string.Empty;
                    }

                    Label lblTanSuatHoatDong = e.Row.FindControl("lblTanSuatHoatDong") as Label;
                    if (lblTanSuatHoatDong != null)
                    {
                        if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Gio) || row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Km))
                            lblTanSuatHoatDong.Text = NumberHelper.ToStringNumber(row["TanSuatHoatDong"]);
                        else
                            lblTanSuatHoatDong.Text = string.Empty;
                    }

                    Label lblMocBaoDuongTiepTheo = e.Row.FindControl("lblMocBaoDuongTiepTheo") as Label;
                    if (lblMocBaoDuongTiepTheo != null)
                        lblMocBaoDuongTiepTheo.Text = NumberHelper.ToStringNumber(row["MocBaoDuongTiepTheo"]);
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    public override void BindData()
    {
        int idCongTy = 0;
        int idNhomTrangThietBi = 0;
        int idLoaiTrangThietBi = 0;

        if (!string.IsNullOrEmpty(dlFilterCompany.SelectedValue))
            idCongTy = int.Parse(dlFilterCompany.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
            idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

        if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
            idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

        int soNgayPhamViCanhBao = 0;
        tblSystemSetting setting = SystemSettingManager.GetItemByName(SystemSettingParameter.Maintenance_Management_SoNgayCanhBaoBaoDuong);
        if (setting != null)
        {
            if (!int.TryParse(setting.Value, out soNgayPhamViCanhBao) || soNgayPhamViCanhBao == 0)
            {
                ShowMessage(this, "Vui lòng nhập <b>Phạm vi hiện cảnh báo bảo dưỡng trang thiết bị</b> ở cấu hình hệ thống trước khi tính kế hoạch bảo dưỡng.",
                                MessageType.Message);
                return;
            }
        }

        int soNgayNhapLieuGanNhat = 0;
        setting = SystemSettingManager.GetItemByName(SystemSettingParameter.Maintenance_Management_SoNgayTinhTanSuat);
        if (setting != null)
        {
            if (!int.TryParse(setting.Value, out soNgayNhapLieuGanNhat) || soNgayNhapLieuGanNhat == 0)
            {
                ShowMessage(this, "Vui lòng nhập <b>Số ngày gần nhất nhập liệu để tính tần suất</b> ở cấu hình hệ thống trước khi tính kế hoạch bảo dưỡng.",
                                MessageType.Message);
                return;
            }
        }

        //Kế hoạch bảo dưỡng từ ngày - đến ngày
        DateTime keHoachTuNgay = DateTime.MinValue;
        DateTime keHoachDenNgay = DateTime.MinValue;

        if (string.IsNullOrEmpty(txtKeHoachTuNgay.Text.Trim()) || txtKeHoachTuNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Kế hoạch từ ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtKeHoachTuNgay.Text.Trim(), out keHoachTuNgay))
        {
            ShowMessage(this, "Error convert <b>Kế hoạch từ ngày</b> to datetime", MessageType.Message);
            return;
        }

        if (string.IsNullOrEmpty(txtKeHoachDenNgay.Text.Trim()) || txtKeHoachDenNgay.Text.Trim().Equals("__/__/____"))
        {
            ShowMessage(this, "Please enter <b>Kế hoạch đến ngày</b> is a datetime", MessageType.Message);
            return;
        }
        else if (!DateTimeHelper.ConvertStringToDateTime(txtKeHoachDenNgay.Text.Trim(), out keHoachDenNgay))
        {
            ShowMessage(this, "Error convert <b>Kế hoạch đến ngày</b> to datetime", MessageType.Message);
            return;
        }

        if (DateTime.Compare(keHoachTuNgay, keHoachDenNgay) > 0)
        {
            ShowMessage(this, "Please enter <b>Kế hoạch từ ngày</b> is earlier than <b>Kế hoạch đến ngày</b>.", MessageType.Message);
            return;
        }

        DataTable dt = MaintenanceTypeManager.ReportMaintenancePlan(idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi,
                                            txtFilterBienSo.Text.Trim(), soNgayPhamViCanhBao, soNgayNhapLieuGanNhat,
                                            keHoachTuNgay, keHoachDenNgay);
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

    #endregion

    #region button

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime tuNgay = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtTanSuatTuNgay.Text.Trim()) || txtTanSuatTuNgay.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Tần suất từ ngày</b> is a datetime", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtTanSuatTuNgay.Text.Trim(), out tuNgay))
            {
                ShowMessage(this, "Error convert <b>Tần suất từ ngày</b> to datetime", MessageType.Message);
                return;
            }

            //Lùi lại 1 tháng
            tuNgay = DateTimeHelper.FirstDayOfMonth(tuNgay);
            tuNgay = tuNgay.AddMonths(-1);
            txtTanSuatTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
            txtTanSuatDenNgay.Text = DateTimeHelper.LastDayOfMonth(tuNgay).ToString("dd/MM/yyyy");
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
            if (string.IsNullOrEmpty(txtTanSuatTuNgay.Text.Trim()) || txtTanSuatTuNgay.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Tần suất từ ngày</b> is a datetime", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtTanSuatTuNgay.Text.Trim(), out tuNgay))
            {
                ShowMessage(this, "Error convert <b>Tần suất từ ngày</b> to datetime", MessageType.Message);
                return;
            }

            //Tăng lên 1 tháng
            tuNgay = DateTimeHelper.FirstDayOfMonth(tuNgay);
            tuNgay = tuNgay.AddMonths(1);
            txtTanSuatTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
            txtTanSuatDenNgay.Text = DateTimeHelper.LastDayOfMonth(tuNgay).ToString("dd/MM/yyyy");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnKeHoachTuNgayPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime tuNgay = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtKeHoachTuNgay.Text.Trim()) || txtKeHoachTuNgay.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Kế hoạch từ ngày</b> is a datetime", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtKeHoachTuNgay.Text.Trim(), out tuNgay))
            {
                ShowMessage(this, "Error convert <b>Kế hoạch từ ngày</b> to datetime", MessageType.Message);
                return;
            }

            //Lùi lại 1 tháng
            tuNgay = DateTimeHelper.FirstDayOfMonth(tuNgay);
            tuNgay = tuNgay.AddMonths(-1);
            txtKeHoachTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
            txtKeHoachDenNgay.Text = DateTimeHelper.LastDayOfMonth(tuNgay).ToString("dd/MM/yyyy");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnKeHoachDenNgayNext_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime tuNgay = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtKeHoachDenNgay.Text.Trim()) || txtKeHoachDenNgay.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Kế hoạch từ ngày</b> is a datetime", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtKeHoachDenNgay.Text.Trim(), out tuNgay))
            {
                ShowMessage(this, "Error convert <b>Kế hoạch từ ngày</b> to datetime", MessageType.Message);
                return;
            }

            //Tăng lên 1 tháng
            tuNgay = DateTimeHelper.FirstDayOfMonth(tuNgay);
            tuNgay = tuNgay.AddMonths(1);
            txtKeHoachTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
            txtKeHoachDenNgay.Text = DateTimeHelper.LastDayOfMonth(tuNgay).ToString("dd/MM/yyyy");
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

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

            txtFilterBienSo.Text = string.Empty;
            dlFilterNhomTrangThietBi.SelectedIndex = 0;
            dlFilterLoaiTrangThietBi.SelectedIndex = 0;
            dlFilterCompany.SelectedIndex = 1;

            txtTanSuatTuNgay.Text = string.Empty;
            txtTanSuatDenNgay.Text = string.Empty;

            SetDefaultDateValue();
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
            int idCongTy = 0;
            int idNhomTrangThietBi = 0;
            int idLoaiTrangThietBi = 0;
            string bienSo = txtFilterBienSo.Text.Trim();

            if (!string.IsNullOrEmpty(dlFilterCompany.SelectedValue))
                idCongTy = int.Parse(dlFilterCompany.SelectedValue);

            if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
                idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

            if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
                idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

            int soNgayPhamViCanhBao = 0;
            tblSystemSetting setting = SystemSettingManager.GetItemByName(SystemSettingParameter.Maintenance_Management_SoNgayCanhBaoBaoDuong);
            if (setting != null)
            {
                if (!int.TryParse(setting.Value, out soNgayPhamViCanhBao) || soNgayPhamViCanhBao == 0)
                {
                    ShowMessage(this, "Vui lòng nhập <b>Phạm vi hiện cảnh báo bảo dưỡng trang thiết bị</b> ở cấu hình hệ thống trước khi tính kế hoạch bảo dưỡng.",
                                    MessageType.Message);
                    return;
                }
            }

            int soNgayNhapLieuGanNhat = 0;
            setting = SystemSettingManager.GetItemByName(SystemSettingParameter.Maintenance_Management_SoNgayTinhTanSuat);
            if (setting != null)
            {
                if (!int.TryParse(setting.Value, out soNgayNhapLieuGanNhat) || soNgayNhapLieuGanNhat == 0)
                {
                    ShowMessage(this, "Vui lòng nhập <b>Số ngày gần nhất nhập liệu để tính tần suất</b> ở cấu hình hệ thống trước khi tính kế hoạch bảo dưỡng.",
                                    MessageType.Message);
                    return;
                }
            }

            //Kế hoạch bảo dưỡng từ ngày - đến ngày
            DateTime keHoachTuNgay = DateTime.MinValue;
            DateTime keHoachDenNgay = DateTime.MinValue;

            if (string.IsNullOrEmpty(txtKeHoachTuNgay.Text.Trim()) || txtKeHoachTuNgay.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Kế hoạch từ ngày</b> is a datetime", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtKeHoachTuNgay.Text.Trim(), out keHoachTuNgay))
            {
                ShowMessage(this, "Error convert <b>Kế hoạch từ ngày</b> to datetime", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtKeHoachDenNgay.Text.Trim()) || txtKeHoachDenNgay.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Kế hoạch đến ngày</b> is a datetime", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtKeHoachDenNgay.Text.Trim(), out keHoachDenNgay))
            {
                ShowMessage(this, "Error convert <b>Kế hoạch đến ngày</b> to datetime", MessageType.Message);
                return;
            }

            if (DateTime.Compare(keHoachTuNgay, keHoachDenNgay) > 0)
            {
                ShowMessage(this, "Please enter <b>Kế hoạch từ ngày</b> is earlier than <b>Kế hoạch đến ngày</b>.", MessageType.Message);
                return;
            }

            string url = string.Format("ReportMaintenancePlanExport.aspx?IDCongTy={0}&IDNhomTrangThietBi={1}&IDLoaiTrangThietBi={2}&BienSo={3}",
                                    idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, bienSo);
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