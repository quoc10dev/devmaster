using BusinessLogic;
using BusinessLogic.Security;
using DataAccess;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportWarningRegistration : BaseAdminGridPage
{
    #region begin

    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Warning_Registration;
        }
    }

    private int Setting_SoNgayCanhBaoDangKiem
    {
        get
        {
            if (ViewState["Setting_SoNgayCanhBaoDangKiem"] != null)
                return Convert.ToInt32(ViewState["Setting_SoNgayCanhBaoDangKiem"]);
            else
            {
                int soNgayCanhBaoDangKiem = 0;
                tblSystemSetting item = SystemSettingManager.GetItemByName(SystemSettingParameter.Equipment_Management_SoNgayCanhBaoDangKiem);
                if (item != null)
                    soNgayCanhBaoDangKiem = Convert.ToInt32(item.Value);

                ViewState["Setting_SoNgayCanhBaoDangKiem"] = soNgayCanhBaoDangKiem;
                return soNgayCanhBaoDangKiem;
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
                txtFilterTenXe.Attributes.Add("onKeyPress", "DoClick('" + btnFilter.ClientID + "',event)");
            }
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void Permission()
    {
        btnExport.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.EquipmentType_View); //Phân quyền xem báo cáo
    }

    private void SetMaxLength()
    {
        txtFilterTenXe.MaxLength = 300;
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
                    int soNgayConLai = Convert.ToInt32(row["SoNgayConLai"]);
                    if (soNgayConLai < 0)
                    {
                        e.Row.CssClass = "table-danger";
                        e.Row.ToolTip = "Xe này đã quá hạn đăng kiểm. Bạn cần đăng kiểm gấp.";
                    }
                    else if (soNgayConLai <= Setting_SoNgayCanhBaoDangKiem)
                    {
                        e.Row.CssClass = "table-warning";
                        e.Row.ToolTip = "Sắp tới hạn đăng kiểm";
                    }

                    HyperLink hpBienSo = e.Row.FindControl("hlBienSo") as HyperLink;
                    if (hpBienSo != null)
                    {
                        hpBienSo.Text = row["BienSo"].ToString();
                        hpBienSo.NavigateUrl = string.Format("../EquipmentDetail.aspx?ID={0}", Convert.ToInt32(row["ID"])); 
                    }

                    Label lblNgayDangKiemGanNhat = e.Row.FindControl("lblNgayDangKiemGanNhat") as Label;
                    if (lblNgayDangKiemGanNhat != null && row["NgayDangKiemLanDau"] != DBNull.Value)
                        lblNgayDangKiemGanNhat.Text = string.Format("{0:dd/MM/yyyy}", row["NgayDangKiemLanDau"]);

                    Label lblSoThangDangKiemDinhKy = e.Row.FindControl("lblSoThangDangKiemDinhKy") as Label;
                    if (lblSoThangDangKiemDinhKy != null && row["SoThangDangKiemDinhKy"] != DBNull.Value)
                        lblSoThangDangKiemDinhKy.Text = row["SoThangDangKiemDinhKy"].ToString();
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

        int soNgayPhamViThongBaoDangkiem = 0; 
        tblSystemSetting setting = SystemSettingManager.GetItemByName(SystemSettingParameter.Equipment_Management_SoNgayCanhBaoDangKiem);
        if (setting != null)
        {
            if (!int.TryParse(setting.Value, out soNgayPhamViThongBaoDangkiem) || soNgayPhamViThongBaoDangkiem == 0)
            {
                ShowMessage(this, "Vui lòng nhập <b>Phạm vi hiện cảnh báo đăng kiểm trang thiết bị </b> ở cấu hình hệ thống trước khi xem báo cáo.",
                                MessageType.Message);
                return;
            }
        }

        DataTable dt = EquipmentManager.ReportCanhBaoNgayDangKiemTiepTheo(idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, txtFilterBienSo.Text.Trim(), txtFilterTenXe.Text.Trim(), soNgayPhamViThongBaoDangkiem);
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

            txtFilterTenXe.Text = string.Empty;
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

            if (!string.IsNullOrEmpty(dlFilterCompany.SelectedValue))
                idCongTy = int.Parse(dlFilterCompany.SelectedValue);

            if (!string.IsNullOrEmpty(dlFilterNhomTrangThietBi.SelectedValue))
                idNhomTrangThietBi = int.Parse(dlFilterNhomTrangThietBi.SelectedValue);

            if (!string.IsNullOrEmpty(dlFilterLoaiTrangThietBi.SelectedValue))
                idLoaiTrangThietBi = int.Parse(dlFilterLoaiTrangThietBi.SelectedValue);

            string tenXe = txtFilterTenXe.Text.Trim();
            string bienSo = txtFilterBienSo.Text.Trim();

            int soNgayPhamViThongBaoDangkiem = 0; 
            tblSystemSetting setting = SystemSettingManager.GetItemByName(SystemSettingParameter.Equipment_Management_SoNgayCanhBaoDangKiem);
            if (setting != null)
            {
                if (!int.TryParse(setting.Value, out soNgayPhamViThongBaoDangkiem) || soNgayPhamViThongBaoDangkiem == 0)
                {
                    ShowMessage(this, "Vui lòng nhập <b>Phạm vi hiện cảnh báo đăng kiểm trang thiết bị </b> ở cấu hình hệ thống trước khi xem báo cáo.",
                                    MessageType.Message);
                    return;
                }
            }

            string url = string.Format("ReportWarningRegistrationExport.aspx?IDCongTy={0}&IDNhomTrangThietBi={1}&IDLoaiTrangThietBi={2}&TenXe={3}&BienSo={4}&SoNgayCanhBao={5}", idCongTy, idNhomTrangThietBi, idLoaiTrangThietBi, tenXe, bienSo, soNgayPhamViThongBaoDangkiem);
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