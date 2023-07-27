using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportWarningMaintenance : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Warning_Maintenance;
        }
    }

    private int Setting_SoNgayCanhBaoBaoDuong
    {
        get
        {
            if (ViewState["Setting_SoNgayCanhBaoBaoDuong"] != null)
                return Convert.ToInt32(ViewState["Setting_SoNgayCanhBaoBaoDuong"]);
            else
            {
                int soNgayCanhBaoBaoDuong = 0;
                tblSystemSetting item = SystemSettingManager.GetItemByName(SystemSettingParameter.Maintenance_Management_SoNgayCanhBaoBaoDuong);
                if (item != null)
                    soNgayCanhBaoBaoDuong = Convert.ToInt32(item.Value);

                ViewState["Setting_SoNgayCanhBaoBaoDuong"] = soNgayCanhBaoBaoDuong;
                return soNgayCanhBaoBaoDuong;
            }

            return 0;
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
                BindData();

                dlFilterCompany.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterNhomTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                dlFilterLoaiTrangThietBi.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
                txtFilterBienSo.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            }
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Highlight row when click on Gridview
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "GridClick",
                "if(typeof(HighlightRow)!='undefined') HighlightRow(" + gvList.ClientID + ");", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnExport.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Warning_Maintenance_View);
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
                    int soNgayQuaHan = Convert.ToInt32(row["SoNgayQuaHan"]);
                    if (soNgayQuaHan < 0)
                    {
                        e.Row.CssClass = "table-danger";
                        e.Row.ToolTip = "Xe này đã quá hạn bảo dưỡng. Bạn cần bảo dưỡng gấp.";
                    }
                    else
                    {
                        int soNgayConLai = Convert.ToInt32(row["SoNgayConLaiChoLanBaoDuongTiepTheo"]);
                        if (soNgayConLai <= Setting_SoNgayCanhBaoBaoDuong)
                        {
                            e.Row.CssClass = "table-warning";
                            e.Row.ToolTip = "Sắp tới hạn bảo dưỡng";
                        }
                    }

                    HyperLink hpBienSo = e.Row.FindControl("hlBienSo") as HyperLink;
                    if (hpBienSo != null)
                    {
                        hpBienSo.Text = row["BienSo"].ToString();
                        hpBienSo.NavigateUrl = string.Format("../EquipmentDetail.aspx?ID={0}", Convert.ToInt32(row["IDTrangThietBi"]));
                    }

                    Label lblSoKmGioDaChay = e.Row.FindControl("lblSoKmGioDaChay") as Label;
                    if (lblSoKmGioDaChay != null && row["SoKmGioDaChay"] != DBNull.Value)
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
                        if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Gio) && row["SoKmGioNhapGanNhat"] != DBNull.Value)
                            lblSoKmGioNhapGanNhat.Text = string.Format("{0} giờ", NumberHelper.ToStringNumber(row["SoKmGioNhapGanNhat"]));
                        else if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Km) && row["SoKmGioNhapGanNhat"] != DBNull.Value)
                            lblSoKmGioNhapGanNhat.Text = string.Format("{0} km", NumberHelper.ToStringNumber(row["SoKmGioNhapGanNhat"]));
                        else
                            lblSoKmGioNhapGanNhat.Text = string.Empty;
                    }

                    Label lblTanSuatHoatDong = e.Row.FindControl("lblTanSuatHoatDong") as Label;
                    if (lblTanSuatHoatDong != null)
                    {
                        if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Gio) || row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Km))
                        {
                            if (row["TanSuatHoatDong"] != DBNull.Value)
                                lblTanSuatHoatDong.Text = NumberHelper.ToStringNumber(row["TanSuatHoatDong"]);
                            else
                                lblTanSuatHoatDong.Text = string.Empty;
                        }
                        else
                            lblTanSuatHoatDong.Text = string.Empty;
                    }

                    Label lblMocBaoDuongTiepTheo = e.Row.FindControl("lblMocBaoDuongTiepTheo") as Label;
                    if (lblMocBaoDuongTiepTheo != null && row["MocBaoDuongTiepTheo"] != DBNull.Value)
                        lblMocBaoDuongTiepTheo.Text = NumberHelper.ToStringNumber(row["MocBaoDuongTiepTheo"]);

                    Label lblNgayNhapLieuGanNhat = e.Row.FindControl("lblNgayNhapLieuGanNhat") as Label;
                    if (lblNgayNhapLieuGanNhat != null && row["NgayNhapGanNhat"] != DBNull.Value)
                    {
                        if (!row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Thang))
                            lblNgayNhapLieuGanNhat.Text = string.Format("{0: dd/MM/yyyy}", row["NgayNhapGanNhat"]);
                    }
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

        DataTable dt = MaintenanceTypeManager.WarningMaintenance(idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, txtFilterBienSo.Text.Trim(), soNgayPhamViCanhBao);
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

            string url = string.Format("ReportWarningMaintenanceExport.aspx?IDCongTy={0}&IDNhomTrangThietBi={1}&IDLoaiTrangThietBi={2}&BienSo={3}&SoNgayCanhBao={4}",
                                    idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, bienSo, soNgayPhamViCanhBao);
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