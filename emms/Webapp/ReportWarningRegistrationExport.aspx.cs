using System;
using System.Data;
using System.Drawing;
using BusinessLogic;
using DataAccess;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class ReportWarningRegistrationExport : BasePage
{
    private int IDCongTy
    {
        get
        {
            return GetValueOfQueryStringTypeInt("IDCongTy");
        }
    }

    private int IDNhomTrangThietBi
    {
        get
        {
            return GetValueOfQueryStringTypeInt("IDNhomTrangThietBi");
        }
    }

    private int IDLoaiTrangThietBi
    {
        get
        {
            return GetValueOfQueryStringTypeInt("IDLoaiTrangThietBi");
        }
    }

    private string TenXe
    {
        get
        {
            return GetValueOfQueryStringTypeString("TenXe");
        }
    }

    private string BienSo
    {
        get
        {
            return GetValueOfQueryStringTypeString("BienSo");
        }
    }

    private int SoNgayCanhBao
    {
        get
        {
            return GetValueOfQueryStringTypeInt("SoNgayCanhBao");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoadDetail();
        }
        catch (Exception exc)
        {
            //ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadDetail()
    {
        try
        {
            string tenCongTy = "Tất cả";
            tblCongTy congTy = CompanyManager.GetCompanyById(IDCongTy);
            if (congTy != null)
                tenCongTy = congTy.TenVietTat;

            string tenNhomTTB = "Tất cả";
            tblNhomTrangThietBi nhomTTB = EquipmentGroupManager.GetEquipmentGroupById(IDNhomTrangThietBi);
            if (nhomTTB != null)
                tenNhomTTB = nhomTTB.TenNhom;

            string tenLoaiTTB = "Tất cả";
            tblLoaiTrangThietBi loaiTTB = EquipmentTypeManager.GetEquipmentTypeById(IDLoaiTrangThietBi);
            if (loaiTTB != null)
                tenLoaiTTB = loaiTTB.Ten;

            DataTable dt = EquipmentManager.ReportCanhBaoNgayDangKiemTiepTheo(IDCongTy, IDNhomTrangThietBi, IDLoaiTrangThietBi, TenXe, BienSo, SoNgayCanhBao);

            using (ExcelPackage exp = new ExcelPackage())
            {
                //Tạo worksheet    
                ExcelWorksheet ws = exp.Workbook.Worksheets.Add("Warning Registration");
                ws.PrinterSettings.Orientation = eOrientation.Portrait; //Mẫu in nằm ngang
                ws.PrinterSettings.PaperSize = ePaperSize.A4; //In trên khổ A4
                //ws.HeaderFooter.FirstFooter.RightAlignedText = "AGS"; //đặt footer khi in tại trang đầu
                //ws.HeaderFooter.EvenFooter.RightAlignedText = "AGS"; //đặt footer khi in tại các trang sau

                //Canh bằng 0 các margin
                ws.PrinterSettings.HeaderMargin = 0;
                ws.PrinterSettings.TopMargin = 0;
                ws.PrinterSettings.RightMargin = 0;
                ws.PrinterSettings.BottomMargin = 0;
                ws.PrinterSettings.LeftMargin = 0;
                ws.PrinterSettings.FooterMargin = 0;

                ws.Cells.Style.WrapText = true;
                ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells.Style.Font.SetFromFont(new Font("Times New Roman", 10));
                ws.Cells.AutoFitColumns(); //tự động điều chỉnh độ rộng tất cả các cột

                //Logo
                string path = Server.MapPath("img/logo.png");
                System.Drawing.Image logo = System.Drawing.Image.FromFile(path);
                var picture = ws.Drawings.AddPicture("LogoAGS", logo);
                picture.From.Column = 0;
                picture.From.Row = 0;
                picture.SetPosition(20, 5); //top 10px, left 0px
                picture.SetSize(36, 36); //70, 60

                ws.Cells["B2:D2"].Merge = true;
                ws.Cells["B2"].Value = "Công ty TNHH Dịch Vụ Mặt Đất Hàng Không AGS";

                ws.Cells["F2:I2"].Merge = true;
                ws.Cells["F2"].Value = string.Format("Cam Ranh, ngày {0:dd} tháng {1:MM} năm {2:yyyy}", DateTime.Now, DateTime.Now, DateTime.Now);
                ws.Cells["F2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Row(2).Height = 30;

                //Tiêu đề báo cáo
                ws.Cells["A3:I3"].Merge = true;
                ws.Cells["A3"].Value = "CẢNH BÁO NGÀY ĐĂNG KIỂM KẾ TIẾP";
                ws.Cells["A3"].Style.Font.Bold = true;
                ws.Cells["A3"].Style.Font.Size = 15;
                ws.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Row(3).Height = 30;

                ws.Cells["D4:G4"].Merge = true;
                ws.Cells["D4"].Value = string.Format("Công ty: {0}", tenCongTy);
                ws.Cells["D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["D4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["D5:G5"].Merge = true;
                ws.Cells["D5"].Value = string.Format("Nhóm xe: {0}", tenNhomTTB);
                ws.Cells["D5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["D5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["D6:G6"].Merge = true;
                ws.Cells["D6"].Value = string.Format("Loại xe: {0}", tenLoaiTTB);
                ws.Cells["D6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["D6"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["D7:G7"].Merge = true;
                ws.Cells["D7"].Value = string.Format("Biển số: {0}", !string.IsNullOrEmpty(BienSo) ? BienSo : "Tất cả");
                ws.Cells["D7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["D7"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                //Tên tiêu đề cột
                int indexTitle = 9;
                ws.Cells[string.Format("A{0}", indexTitle)].Value = "STT";
                ws.Column(1).Width = 7;

                ws.Cells[string.Format("B{0}", indexTitle)].Value = "Nhóm xe";
                ws.Column(2).Width = 14;

                ws.Cells[string.Format("C{0}", indexTitle)].Value = "Loại xe";
                ws.Column(3).Width = 17;

                ws.Cells[string.Format("D{0}", indexTitle)].Value = "Tên xe";
                ws.Column(4).Width = 16;

                ws.Cells[string.Format("E{0}", indexTitle)].Value = "Biển số";
                ws.Column(5).Width = 11;

                ws.Cells[string.Format("F{0}", indexTitle)].Value = "Ngày đăng kiểm lần đầu";
                ws.Column(6).Width = 9;

                ws.Cells[string.Format("G{0}", indexTitle)].Value = "Số tháng đăng kiểm định kỳ";
                ws.Column(7).Width = 7;

                ws.Cells[string.Format("H{0}", indexTitle)].Value = "Ngày đăng kiểm tiếp theo";
                ws.Column(8).Width = 9;

                ws.Cells[string.Format("I{0}", indexTitle)].Value = "Số ngày còn lại";
                ws.Column(9).Width = 7;

                using (ExcelRange objRange = ws.Cells[string.Format("A{0}:I{1}", indexTitle, indexTitle)])
                {
                    objRange.Style.Font.Bold = true;
                    objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                int index = 1;
                int rowIndexBeginOrders = 10;
                int rowIndexCurrentRecord = rowIndexBeginOrders;
                foreach (DataRow row in dt.Rows)
                {
                    ws.Cells["A" + rowIndexCurrentRecord].Value = index;
                    ws.Cells["A" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    if (row["TenNhom"] != null)
                        ws.Cells["B" + rowIndexCurrentRecord].Value = row["TenNhom"].ToString();

                    if (row["LoaiXe"] != null)
                        ws.Cells["C" + rowIndexCurrentRecord].Value = row["LoaiXe"].ToString();

                    if (row["TenXe"] != null && row["TenXe"] != DBNull.Value)
                        ws.Cells["D" + rowIndexCurrentRecord].Value = row["TenXe"];

                    if (row["BienSo"] != null && row["BienSo"] != DBNull.Value)
                        ws.Cells["E" + rowIndexCurrentRecord].Value = row["BienSo"];

                    if (row["NgayDangKiemLanDau"] != null && row["NgayDangKiemLanDau"] != DBNull.Value)
                    {
                        ws.Cells["F" + rowIndexCurrentRecord].Value = row["NgayDangKiemLanDau"];
                        ws.Cells["F" + rowIndexCurrentRecord].Style.Numberformat.Format = "dd/MM/yyyy";
                        ws.Cells["F" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["F" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["SoThangDangKiemDinhKy"] != null && row["SoThangDangKiemDinhKy"] != DBNull.Value)
                    {
                        ws.Cells["G" + rowIndexCurrentRecord].Value = row["SoThangDangKiemDinhKy"];
                        ws.Cells["G" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["G" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["NgayDangKiemTiepTheo"] != null && row["NgayDangKiemTiepTheo"] != DBNull.Value)
                    {
                        ws.Cells["H" + rowIndexCurrentRecord].Value = row["NgayDangKiemTiepTheo"];
                        ws.Cells["H" + rowIndexCurrentRecord].Style.Numberformat.Format = "dd/MM/yyyy";
                        ws.Cells["H" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["H" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["SoNgayConLai"] != null && row["SoNgayConLai"] != DBNull.Value)
                    {
                        ws.Cells["I" + rowIndexCurrentRecord].Value = row["SoNgayConLai"];
                        ws.Cells["I" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["I" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    rowIndexCurrentRecord++;
                    index++;
                }

                //Setting Top/left,right/bottom borders.
                var cell = ws.Cells[string.Format("A{0}:I{1}", indexTitle, rowIndexCurrentRecord - 1)];
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                var border = cell.Style.Border;
                border.Bottom.Style = ExcelBorderStyle.Thin;
                border.Top.Style = ExcelBorderStyle.Thin;
                border.Left.Style = ExcelBorderStyle.Thin;
                border.Right.Style = ExcelBorderStyle.Thin;

                string fileName = "EMMS_WarningRegistration.xlsx";
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=\"{0}\"", fileName));
                Response.BinaryWrite(exp.GetAsByteArray());
                Response.End();
            }
        }
        catch (Exception exc)
        {

        }
    }
}