using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BusinessLogic;
using BusinessLogic.Helper;
using BusinessLogic.Security;
using DataAccess;

public partial class EquipmentDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Equipment_List;
        }
    }

    private int IDEquipment
    {
        get
        {
            if (ViewState["ID"] != null)
                return Convert.ToInt32(ViewState["ID"]);
            else if (Request.QueryString.AllKeys.Contains("ID"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    int result = 0;
                    int.TryParse(Request.QueryString["ID"], out result);
                    ViewState["ID"] = result;
                    return result;
                }
                else
                    return 0;
            }
            else
                return 0;
        }
    }

    private string KieuBaoDuongDangChon
    {
        get
        {
            if (ViewState["KieuBaoDuongDangChon"] != null)
                return ViewState["KieuBaoDuongDangChon"].ToString();
            else
                return string.Empty;
        }
        set
        {
            ViewState["KieuBaoDuongDangChon"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MasterPage.FunctionPageCode = FunctionPageCode;
                SetMaxLength();
                LoadCompany();
                LoadEquipmentGroup();
                MaintenanceProcedure.LoadAllMaintenanceProcedure(dlQuyTrinhBaoDuong);
                LoadDetail();
                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Equipment_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Equipment_Edit);

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                btnSave.OnClientClick = "Control.Destroy('#" + formatControl.ClientID + "')";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetFormatControls",
                                                   "if(typeof(Control)!='undefined') Control.Build(" + formatControl.ClientID + ")", true);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void SetMaxLength()
    {
        txtMaTaiSan.MaxLength = 50;
        txtTen.MaxLength = 300;
        txtNuocSanXuat.MaxLength = 100;
        txtSoThangDangKiemDinhKy.MaxLength = 4;
        txtBienSo.MaxLength = 50;
        txtSoKhung.MaxLength = 200;
        txtLoaiMay.MaxLength = 200;
        txtSoMay.MaxLength = 200;
        txtSoKmBaoDuongGanNhat.MaxLength = 12;
        txtSoGioBaoDuongGanNhat.MaxLength = 12;
        txtBaoDuongTheo.MaxLength = 1000;
    }

    private void LoadCompany()
    {
        dlCompany.DataSource = CompanyManager.GetAllCompany();
        dlCompany.DataTextField = "TenVietTat";
        dlCompany.DataValueField = "ID";
        dlCompany.DataBind();
    }

    private void LoadEquipmentGroup()
    {
        List<tblNhomTrangThietBi> equipGroupList = EquipmentGroupManager.GetAllEquipmentGroup();
        tblNhomTrangThietBi emptyItem = new tblNhomTrangThietBi();
        emptyItem.ID = 0;
        emptyItem.TenNhom = "--- Chọn nhóm xe ---";
        equipGroupList.Insert(0, emptyItem);

        dlNhomXe.DataSource = equipGroupList;
        dlNhomXe.DataTextField = "TenNhom";
        dlNhomXe.DataValueField = "ID";
        dlNhomXe.DataBind();
    }

    protected void dlNhomXe_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEquipmentType();
    }

    private void LoadEquipmentType()
    {
        try
        { 
            //Load loại trang thiết bị
            int idEquipmentGroup = 0; 
            if (int.TryParse(dlNhomXe.SelectedItem.Value, out idEquipmentGroup))
                EquipmentTypeManager.LoadEquipmentType(dlLoaiTrangThietBi, idEquipmentGroup);
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void dlLoaiTrangThietBi_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadThongTinBaoDuongGanNhat();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadThongTinBaoDuongGanNhat()
    {
        if (!string.IsNullOrEmpty(dlLoaiTrangThietBi.SelectedValue))
        {
            dlQuyTrinhBaoDuong.Items.Clear();
            tblLoaiTrangThietBi equimentType = EquipmentTypeManager.GetEquipmentTypeById(int.Parse(dlLoaiTrangThietBi.SelectedValue));
            if (equimentType != null && equimentType.IDLoaiBaoDuong.HasValue)
            {
                //Lấy kiểu bảo dưỡng để quy định bắt buộc nhập số giờ hay số km
                tblLoaiBaoDuong loaiBaoDuong = equimentType.tblLoaiBaoDuong;
                if (loaiBaoDuong != null)
                {
                    tblKieuBaoDuong kieuBaoDuong = loaiBaoDuong.tblKieuBaoDuong;
                    if (kieuBaoDuong != null)
                    {
                        if (kieuBaoDuong.Ma.Equals(KieuBaoDuong.Km))
                        {
                            txtSoGioBaoDuongGanNhat.Text = string.Empty;
                            txtSoGioBaoDuongGanNhat.Enabled = false;
                            txtSoKmBaoDuongGanNhat.Enabled = true;
                            warningKm.Visible = true;
                            warningGio.Visible = false;
                            KieuBaoDuongDangChon = KieuBaoDuong.Km;
                        }
                        else if (kieuBaoDuong.Ma.Equals(KieuBaoDuong.Gio))
                        {
                            txtSoKmBaoDuongGanNhat.Text = string.Empty;
                            txtSoKmBaoDuongGanNhat.Enabled = false;
                            txtSoGioBaoDuongGanNhat.Enabled = true;
                            warningKm.Visible = false;
                            warningGio.Visible = true;
                            KieuBaoDuongDangChon = KieuBaoDuong.Gio;
                        }
                        else if (kieuBaoDuong.Ma.Equals(KieuBaoDuong.Thang))
                        {
                            txtSoGioBaoDuongGanNhat.Enabled = true;
                            txtSoKmBaoDuongGanNhat.Enabled = true;
                            warningKm.Visible = false;
                            warningGio.Visible = false;
                            KieuBaoDuongDangChon = KieuBaoDuong.Thang;
                        }
                    }
                }

                //Lấy quy trình bảo dưỡng gần nhất
                List<tblQuyTrinhBaoDuong> quyTrinhBaoDuongList = MaintenanceProcedure.GetMaintenanceProcedureByIDLoaiBaoDuong(equimentType.IDLoaiBaoDuong.Value);
                if (quyTrinhBaoDuongList != null)
                {
                    tblQuyTrinhBaoDuong emptyItem = new tblQuyTrinhBaoDuong();
                    emptyItem.ID = 0;
                    emptyItem.MaQuyTrinh = "Trống";
                    quyTrinhBaoDuongList.Insert(0, emptyItem);

                    dlQuyTrinhBaoDuong.DataSource = quyTrinhBaoDuongList;
                    dlQuyTrinhBaoDuong.DataTextField = "MaQuyTrinh";
                    dlQuyTrinhBaoDuong.DataValueField = "ID";
                    dlQuyTrinhBaoDuong.DataBind();
                }
                else
                {
                    tblQuyTrinhBaoDuong emptyItem = new tblQuyTrinhBaoDuong();
                    emptyItem.ID = 0;
                    emptyItem.MaQuyTrinh = "Trống";
                    quyTrinhBaoDuongList.Insert(0, emptyItem);

                    dlQuyTrinhBaoDuong.DataSource = new List<tblQuyTrinhBaoDuong>();
                    dlQuyTrinhBaoDuong.DataTextField = "MaQuyTrinh";
                    dlQuyTrinhBaoDuong.DataValueField = "ID";
                    dlQuyTrinhBaoDuong.DataBind();
                }
            }
        }
    }

    private void LoadDetail()
    {
        if (IDEquipment > 0)
        {
            lblAction.Text = "Edit";

            tblTrangThietBi item = EquipmentManager.GetEquipmentById(IDEquipment);
            if (item != null)
            {
                dlIdTitle.Visible = true;
                ddIdTitle.Visible = true;
                lblID.Text = item.ID.ToString();

                tblLoaiTrangThietBi equipType = item.tblLoaiTrangThietBi;
                if (equipType != null && equipType.IDNhomTrangThietBi.HasValue)
                {
                    dlNhomXe.SelectedValue = equipType.IDNhomTrangThietBi.Value.ToString();
                    LoadEquipmentType();
                    dlLoaiTrangThietBi.SelectedValue = item.IDLoaiTrangThietBi.ToString();
                }

                dlCompany.SelectedValue = item.IDCongTy.ToString();

                txtTen.Text = item.Ten;
                txtMaTaiSan.Text = item.MaTaiSan;
                if (item.NamSanXuat.HasValue)
                    txtNamSanXuat.Text = item.NamSanXuat.Value.ToString();
                txtNuocSanXuat.Text = item.NuocSanXuat;

                if (item.NgayDangKiemLanDau.HasValue)
                    txtNgayDangKiemLanDau.Text = item.NgayDangKiemLanDau.Value.ToString("dd/MM/yyyy");
                else
                    txtNgayDangKiemLanDau.Text = string.Empty;

                if (item.SoThangDangKiemDinhKy.HasValue)
                    txtSoThangDangKiemDinhKy.Text = item.SoThangDangKiemDinhKy.Value.ToString();
                else
                    txtSoThangDangKiemDinhKy.Text = string.Empty;

                txtBienSo.Text = item.BienSo;
                txtSoKhung.Text = item.SoKhung;
                txtLoaiMay.Text = item.LoaiMay;
                txtSoMay.Text = item.SoMay;
                txtBaoDuongTheo.Text = item.BaoDuongTheo;
                txtNote.Text = item.GhiChu;
                if (item.NgayBaoDuongGanNhat.HasValue)
                    txtNgayBaoDuongGanNhat.Text = item.NgayBaoDuongGanNhat.Value.ToString("dd/MM/yyyy");
                if (item.SoKmBaoDuongGanNhat.HasValue)
                    txtSoKmBaoDuongGanNhat.Text = item.SoKmBaoDuongGanNhat.Value.ToString();
                if (item.SoGioBaoDuongGanNhat.HasValue)
                    txtSoGioBaoDuongGanNhat.Text = item.SoGioBaoDuongGanNhat.Value.ToString();

                if (item.CanhBaoDangKiem.HasValue)
                    chkCanhBaoDangKiem.Checked = item.CanhBaoDangKiem.Value;
                else
                    chkCanhBaoDangKiem.Checked = false;

                LoadThongTinBaoDuongGanNhat();

                if (item.tblLoaiTrangThietBi.IDLoaiBaoDuong.HasValue)
                {
                    //Quy trình bảo dưỡng
                    if (item.IDQuyTrinhBaoDuong.HasValue)
                    {
                        string idQuyTrinhBaoDuong = item.IDQuyTrinhBaoDuong.Value.ToString();
                        if (dlQuyTrinhBaoDuong.Items.FindByValue(idQuyTrinhBaoDuong) != null)
                            dlQuyTrinhBaoDuong.SelectedValue = idQuyTrinhBaoDuong;
                        else
                            dlQuyTrinhBaoDuong.SelectedValue = "0";
                    }
                }
            }
        }
        else
        {
            lblAction.Text = "Add";
            LoadEquipmentType();
            LoadThongTinBaoDuongGanNhat();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDEquipment > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Equipment_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Equipment_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(dlNhomXe.SelectedValue) && dlNhomXe.SelectedValue.Equals("0"))
            {
                ShowMessage(this, "Please select <b>Nhóm xe</b>", MessageType.Message);
                return;
            }

            if (!string.IsNullOrEmpty(dlLoaiTrangThietBi.SelectedValue) && dlLoaiTrangThietBi.SelectedValue.Equals("0"))
            {
                ShowMessage(this, "Please select <b>Loại xe</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtTen.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Tên xe</b>", MessageType.Message);
                return;
            }

            if (string.IsNullOrEmpty(txtMaTaiSan.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Mã tài sản</b>", MessageType.Message);
                return;
            }
            else
            {
                //Kiểm tra cảnh báo mã tài sản nếu bị trùng với mã tài sản TTB khác
                tblTrangThietBi item = null;
                if (IDEquipment == 0)
                    item = EquipmentManager.GetEquipmentByAssetCode(txtMaTaiSan.Text.Trim());
                else
                    item = EquipmentManager.GetEquipmentByAssetCode(IDEquipment, txtMaTaiSan.Text.Trim());

                if (item != null)
                {
                    ShowMessage(this, "Please enter other value of <b>Mã tài sản</b>. This value exists in the list.", MessageType.Message);
                    return;
                }
            }

            //Năm sản xuất
            int namSanXuat = 0;
            if (!string.IsNullOrEmpty(txtNamSanXuat.Text.Trim()))
            {
                if (!NumberHelper.ConvertToInt(txtNamSanXuat.Text.Trim(), out namSanXuat))
                {
                    ShowMessage(this, "Please enter <b>Năm sản xuất</b> have to a number", MessageType.Message);
                    return;
                }
            }

            //Ngày đăng kiểm gần nhất
            DateTime ngayDangKiemLanDau = DateTime.MinValue;
            if (!string.IsNullOrEmpty(txtNgayDangKiemLanDau.Text.Trim()) && !txtNgayDangKiemLanDau.Text.Trim().Equals("__/__/____"))
            {
                if (!DateTimeHelper.ConvertStringToDateTime(txtNgayDangKiemLanDau.Text.Trim(), out ngayDangKiemLanDau))
                {
                    ShowMessage(this, "Error convert <b>Ngày đăng kiểm lần đầu</b> to datetime", MessageType.Message);
                    return;
                }
            }

            //Số tháng đăng kiểm định kỳ
            int soThangDangKiemDinhKy = 0;
            if (!string.IsNullOrEmpty(txtSoThangDangKiemDinhKy.Text.Trim()))
            {
                if (!NumberHelper.ConvertToInt(txtSoThangDangKiemDinhKy.Text.Trim(), out soThangDangKiemDinhKy))
                {
                    ShowMessage(this, "Please enter <b>Số tháng đăng kiểm định kỳ</b> have to a number", MessageType.Message);
                    return;
                }
            }

            //Biển số
            if (string.IsNullOrEmpty(txtBienSo.Text.Trim()))
            {
                ShowMessage(this, "Please enter <b>Biển sổ</b>", MessageType.Message);
                return;
            }
            else
            {
                //Kiểm tra cảnh báo biển số nếu bị trùng với biển số TTB khác
                tblTrangThietBi item = null;
                if (IDEquipment == 0)
                    item = EquipmentManager.GetEquipmentByLicensePlate(txtBienSo.Text.Trim());
                else
                    item = EquipmentManager.GetEquipmentByLicensePlate(IDEquipment, txtBienSo.Text.Trim());

                if (item != null)
                {
                    ShowMessage(this, "Please enter other value of <b>Biển số</b>. This value exists in the list.", MessageType.Message);
                    return;
                }
            }

            //Ngày bảo dưỡng gần nhất
            DateTime ngayBaoDuongGanNhat = DateTime.MinValue;
            if (string.IsNullOrEmpty(txtNgayBaoDuongGanNhat.Text.Trim()) || txtNgayBaoDuongGanNhat.Text.Trim().Equals("__/__/____"))
            {
                ShowMessage(this, "Please enter <b>Ngày bảo dưỡng gần nhất</b>", MessageType.Message);
                return;
            }
            else if (!DateTimeHelper.ConvertStringToDateTime(txtNgayBaoDuongGanNhat.Text.Trim(), out ngayBaoDuongGanNhat))
            {
                ShowMessage(this, "Error convert <b>Ngày bảo dưỡng gần nhất</b> to datetime", MessageType.Message);
                return;
            }

            double soKmBaoDuongGanNhat = 0;
            double soGioBaoDuongGanNhat = 0;
            if (KieuBaoDuongDangChon.Equals(KieuBaoDuong.Km))
            {
                //Bắt buộc "Số Km bảo dưỡng gần nhất"
                if (string.IsNullOrEmpty(txtSoKmBaoDuongGanNhat.Text))
                {
                    ShowMessage(this, "Please enter <b>Số km bảo dưỡng gần nhất</b> is a number", MessageType.Message);
                    return;
                }
                else
                {
                    if (!NumberHelper.ConvertToDouble(txtSoKmBaoDuongGanNhat.Text.Trim(), out soKmBaoDuongGanNhat))
                    {
                        ShowMessage(this, "Please enter <b>Số km bảo dưỡng gần nhất</b> is a number", MessageType.Message);
                        return;
                    }
                    if (soKmBaoDuongGanNhat <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số km bảo dưỡng gần nhất</b> is greater than 0", MessageType.Message);
                        return;
                    }
                }
            }
            else if (KieuBaoDuongDangChon.Equals(KieuBaoDuong.Gio))
            {
                //Bắt buộc "Số Giờ bảo dưỡng gần nhất"
                if (string.IsNullOrEmpty(txtSoGioBaoDuongGanNhat.Text))
                {
                    ShowMessage(this, "Please enter <b>Số giờ bảo dưỡng gần nhất</b> is a number", MessageType.Message);
                    return;
                }
                else
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGioBaoDuongGanNhat.Text.Trim(), out soGioBaoDuongGanNhat))
                    {
                        ShowMessage(this, "Please enter <b>Số giờ bảo dưỡng gần nhất</b> is a number", MessageType.Message);
                        return;
                    }
                    if (soGioBaoDuongGanNhat <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số giờ bảo dưỡng gần nhất</b> is greater than 0", MessageType.Message);
                        return;
                    }
                }
            }
            else if (KieuBaoDuongDangChon.Equals(KieuBaoDuong.Thang))
            {
                //Kiểm tra hợp lệ "Số Km bảo dưỡng gần nhất" nếu có nhập
                if (!string.IsNullOrEmpty(txtSoKmBaoDuongGanNhat.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoKmBaoDuongGanNhat.Text.Trim(), out soKmBaoDuongGanNhat))
                    {
                        ShowMessage(this, "Please enter <b>Số km bảo dưỡng gần nhất</b> is a number", MessageType.Message);
                        return;
                    }
                    if (soKmBaoDuongGanNhat <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số km bảo dưỡng gần nhất</b> is greater than 0", MessageType.Message);
                        return;
                    }
                }

                //Kiểm tra hợp lệ "Số Giờ bảo dưỡng gần nhất" nếu có nhập
                if (!string.IsNullOrEmpty(txtSoGioBaoDuongGanNhat.Text))
                {
                    if (!NumberHelper.ConvertToDouble(txtSoGioBaoDuongGanNhat.Text.Trim(), out soGioBaoDuongGanNhat))
                    {
                        ShowMessage(this, "Please enter <b>Số giờ bảo dưỡng gần nhất</b> is a number", MessageType.Message);
                        return;
                    }
                    if (soGioBaoDuongGanNhat <= 0)
                    {
                        ShowMessage(this, "Please enter <b>Số giờ bảo dưỡng gần nhất</b> is greater than 0", MessageType.Message);
                        return;
                    }
                }
            }

            if (IDEquipment > 0)
            {
                tblTrangThietBi item = new tblTrangThietBi();
                item.ID = IDEquipment;
                item.MaTaiSan = txtMaTaiSan.Text.Trim();
                item.Ten = txtTen.Text.Trim();
                if (namSanXuat > 0)
                    item.NamSanXuat = namSanXuat;
                item.NuocSanXuat = txtNuocSanXuat.Text.Trim();

                if (ngayDangKiemLanDau != DateTime.MinValue)
                    item.NgayDangKiemLanDau = ngayDangKiemLanDau;
                else
                    item.NgayDangKiemLanDau = null;

                if (soThangDangKiemDinhKy > 0)
                    item.SoThangDangKiemDinhKy = soThangDangKiemDinhKy;
                else
                    item.SoThangDangKiemDinhKy = null;

                item.BienSo = txtBienSo.Text.Trim();
                item.SoKhung = txtSoKhung.Text.Trim();
                item.LoaiMay = txtLoaiMay.Text.Trim();
                item.SoMay = txtSoMay.Text.Trim();
                item.GhiChu = txtNote.Text.Trim();
                item.BaoDuongTheo = txtBaoDuongTheo.Text.Trim();

                item.IDCongTy = int.Parse(dlCompany.SelectedValue);
                item.IDLoaiTrangThietBi = int.Parse(dlLoaiTrangThietBi.SelectedValue);
                item.NgayBaoDuongGanNhat = ngayBaoDuongGanNhat;
                item.SoKmBaoDuongGanNhat = soKmBaoDuongGanNhat;
                item.SoGioBaoDuongGanNhat = soGioBaoDuongGanNhat;
                item.CanhBaoDangKiem = chkCanhBaoDangKiem.Checked;

                if (!string.IsNullOrEmpty(dlQuyTrinhBaoDuong.SelectedValue))
                {
                    if (dlQuyTrinhBaoDuong.SelectedValue.Equals("0"))
                        item.IDQuyTrinhBaoDuong = null;
                    else
                        item.IDQuyTrinhBaoDuong = int.Parse(dlQuyTrinhBaoDuong.SelectedValue);
                }

                EquipmentManager.UpdateEquipment(item);

                LoadDetail();
                ShowMessage(this, "Update successfully", MessageType.Message);
            }
            else
            {
                tblTrangThietBi newItem = new tblTrangThietBi();
                newItem.MaTaiSan = txtMaTaiSan.Text.Trim();
                newItem.Ten = txtTen.Text.Trim();
                if (namSanXuat > 0)
                    newItem.NamSanXuat = namSanXuat;
                newItem.NuocSanXuat = txtNuocSanXuat.Text.Trim();

                if (ngayDangKiemLanDau != DateTime.MinValue)
                    newItem.NgayDangKiemLanDau = ngayDangKiemLanDau;
                else
                    newItem.NgayDangKiemLanDau = null;

                if (soThangDangKiemDinhKy > 0)
                    newItem.SoThangDangKiemDinhKy = soThangDangKiemDinhKy;
                else
                    newItem.SoThangDangKiemDinhKy = null;

                newItem.BienSo = txtBienSo.Text.Trim();
                newItem.SoKhung = txtSoKhung.Text.Trim();
                newItem.LoaiMay = txtLoaiMay.Text.Trim();
                newItem.SoMay = txtSoMay.Text.Trim();
                newItem.BaoDuongTheo = txtBaoDuongTheo.Text.Trim();
                newItem.GhiChu = txtNote.Text.Trim();

                newItem.IDCongTy = int.Parse(dlCompany.SelectedValue);
                newItem.IDLoaiTrangThietBi = int.Parse(dlLoaiTrangThietBi.SelectedValue);
                newItem.NgayBaoDuongGanNhat = ngayBaoDuongGanNhat;
                newItem.SoKmBaoDuongGanNhat = soKmBaoDuongGanNhat;
                newItem.SoGioBaoDuongGanNhat = soGioBaoDuongGanNhat;
                newItem.CanhBaoDangKiem = chkCanhBaoDangKiem.Checked;

                if (!string.IsNullOrEmpty(dlQuyTrinhBaoDuong.SelectedValue))
                {
                    if (dlQuyTrinhBaoDuong.SelectedValue.Equals("0"))
                        newItem.IDQuyTrinhBaoDuong = null;
                    else
                        newItem.IDQuyTrinhBaoDuong = int.Parse(dlQuyTrinhBaoDuong.SelectedValue);
                }

                EquipmentManager.InsertEquipment(newItem);
                Response.Redirect("EquipmentList.aspx");
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}