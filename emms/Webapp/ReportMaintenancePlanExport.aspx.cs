using System;
using System.Data;
using System.Drawing;
using BusinessLogic;
using DataAccess;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class ReportMaintenancePlanExport : BasePage
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

            DataTable dt = MaintenanceTypeManager.WarningMaintenance(IDCongTy, IDNhomTrangThietBi, IDLoaiTrangThietBi, BienSo, SoNgayCanhBao);

            using (ExcelPackage exp = new ExcelPackage())
            {
                //Tạo worksheet    
                ExcelWorksheet ws = exp.Workbook.Worksheets.Add("Maintenance plan");
                ws.PrinterSettings.Orientation = eOrientation.Landscape; //Mẫu in nằm ngang
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

                ws.Cells["J2:N2"].Merge = true;
                ws.Cells["J2"].Value = string.Format("Cam Ranh, ngày {0:dd} tháng {1:MM} năm {2:yyyy}", DateTime.Now, DateTime.Now, DateTime.Now);
                ws.Cells["J2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Row(2).Height = 30;

                //Tiêu đề báo cáo
                ws.Cells["A3:N3"].Merge = true;
                ws.Cells["A3"].Value = "KẾ HOẠCH BẢO DƯỠNG";
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
                ws.Column(2).Width = 19;

                ws.Cells[string.Format("C{0}", indexTitle)].Value = "Loại xe";
                ws.Column(3).Width = 19;

                ws.Cells[string.Format("D{0}", indexTitle)].Value = "Tên xe";
                ws.Column(4).Width = 17;

                ws.Cells[string.Format("E{0}", indexTitle)].Value = "Biển số";
                ws.Column(5).Width = 10;

                ws.Cells[string.Format("F{0}", indexTitle)].Value = "Loại bảo dưỡng";
                ws.Column(6).Width = 16;

                ws.Cells[string.Format("G{0}", indexTitle)].Value = "Ngày bảo dưỡng gần nhất";
                ws.Column(7).Width = 10;

                ws.Cells[string.Format("H{0}", indexTitle)].Value = "Số km/giờ lần bảo dưỡng gần nhất";
                ws.Column(8).Width = 10;

                ws.Cells[string.Format("I{0}", indexTitle)].Value = "Ngày nhập số liệu gần nhất";
                ws.Column(9).Width = 10;

                ws.Cells[string.Format("J{0}", indexTitle)].Value = "Số km/giờ lần nhập gần nhất";
                ws.Column(10).Width = 10;

                ws.Cells[string.Format("K{0}", indexTitle)].Value = "Mốc bảo dưỡng tiếp theo";
                ws.Column(9).Width = 10;

                ws.Cells[string.Format("L{0}", indexTitle)].Value = "Tần suất hoạt động / ngày";
                ws.Column(10).Width = 10;

                ws.Cells[string.Format("M{0}", indexTitle)].Value = "Ngày bảo dưỡng dự kiến tiếp theo";
                ws.Column(11).Width = 10;

                ws.Cells[string.Format("N{0}", indexTitle)].Value = "Số ngày dự kiến còn lại";
                ws.Column(12).Width = 9;

                using (ExcelRange objRange = ws.Cells[string.Format("A{0}:N{1}", indexTitle, indexTitle)])
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

                    if (row["NhomXe"] != null)
                        ws.Cells["B" + rowIndexCurrentRecord].Value = row["NhomXe"].ToString();

                    if (row["LoaiTrangThietBi"] != null)
                        ws.Cells["C" + rowIndexCurrentRecord].Value = row["LoaiTrangThietBi"].ToString();

                    if (row["TenXe"] != null && row["TenXe"] != DBNull.Value)
                        ws.Cells["D" + rowIndexCurrentRecord].Value = row["TenXe"];

                    if (row["BienSo"] != null && row["BienSo"] != DBNull.Value)
                        ws.Cells["E" + rowIndexCurrentRecord].Value = row["BienSo"];

                    if (row["BienSo"] != null && row["BienSo"] != DBNull.Value)
                        ws.Cells["E" + rowIndexCurrentRecord].Value = row["BienSo"];

                    if (row["LoaiBaoDuong"] != null && row["LoaiBaoDuong"] != DBNull.Value)
                        ws.Cells["F" + rowIndexCurrentRecord].Value = row["LoaiBaoDuong"];

                    if (row["NgayBaoDuongGanNhat"] != null && row["NgayBaoDuongGanNhat"] != DBNull.Value)
                    {
                        ws.Cells["G" + rowIndexCurrentRecord].Value = row["NgayBaoDuongGanNhat"];
                        ws.Cells["G" + rowIndexCurrentRecord].Style.Numberformat.Format = "dd/MM/yyyy";
                        ws.Cells["G" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["G" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["SoKmGioDaChay"] != null && row["SoKmGioDaChay"] != DBNull.Value)
                    {
                        if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Gio))
                            ws.Cells["H" + rowIndexCurrentRecord].Value = string.Format("{0} giờ",row["SoKmGioDaChay"]);
                        else if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Km))
                            ws.Cells["H" + rowIndexCurrentRecord].Value = string.Format("{0} km", row["SoKmGioDaChay"]);

                        ws.Cells["H" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["H" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["NgayNhapGanNhat"] != null && row["NgayNhapGanNhat"] != DBNull.Value)
                    {
                        ws.Cells["I" + rowIndexCurrentRecord].Value = row["NgayNhapGanNhat"];
                        ws.Cells["I" + rowIndexCurrentRecord].Style.Numberformat.Format = "dd/MM/yyyy";
                        ws.Cells["I" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["I" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["SoKmGioNhapGanNhat"] != null && row["SoKmGioNhapGanNhat"] != DBNull.Value)
                    {
                        if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Gio))
                            ws.Cells["J" + rowIndexCurrentRecord].Value = string.Format("{0} giờ", row["SoKmGioNhapGanNhat"]);
                        else if (row["MaKieuBaoDuong"].ToString().Equals(KieuBaoDuong.Km))
                            ws.Cells["J" + rowIndexCurrentRecord].Value = string.Format("{0} km", row["SoKmGioNhapGanNhat"]);

                        ws.Cells["J" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["J" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["MocBaoDuongTiepTheo"] != null && row["MocBaoDuongTiepTheo"] != DBNull.Value)
                    {
                        ws.Cells["K" + rowIndexCurrentRecord].Value = row["MocBaoDuongTiepTheo"];
                        ws.Cells["K" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["K" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["TanSuatHoatDong"] != null && row["TanSuatHoatDong"] != DBNull.Value)
                    {
                        ws.Cells["L" + rowIndexCurrentRecord].Value = row["TanSuatHoatDong"];
                        ws.Cells["L" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["L" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["NgayBaoDuongTiepTheo"] != null && row["NgayBaoDuongTiepTheo"] != DBNull.Value)
                    {
                        ws.Cells["M" + rowIndexCurrentRecord].Value = row["NgayBaoDuongTiepTheo"];
                        ws.Cells["M" + rowIndexCurrentRecord].Style.Numberformat.Format = "dd/MM/yyyy";
                        ws.Cells["M" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["M" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["SoNgayConLaiChoLanBaoDuongTiepTheo"] != null && row["SoNgayConLaiChoLanBaoDuongTiepTheo"] != DBNull.Value)
                    {
                        ws.Cells["N" + rowIndexCurrentRecord].Value = row["SoNgayConLaiChoLanBaoDuongTiepTheo"];
                        ws.Cells["N" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["N" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    rowIndexCurrentRecord++;
                    index++;
                }

                //Setting Top/left,right/bottom borders.
                var cell = ws.Cells[string.Format("A{0}:N{1}", indexTitle, rowIndexCurrentRecord - 1)];
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                var border = cell.Style.Border;
                border.Bottom.Style = ExcelBorderStyle.Thin;
                border.Top.Style = ExcelBorderStyle.Thin;
                border.Left.Style = ExcelBorderStyle.Thin;
                border.Right.Style = ExcelBorderStyle.Thin;

                string fileName = "EMMS_WarningMaintenance.xlsx";
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=\"{0}\"", fileName));
                Response.BinaryWrite(exp.GetAsByteArray());
                Response.End();

                ///Định dạng số
                //ws.Cells[string.Format("D11:G{0}", rowIndexCurrentRecord)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //ws.Cells[string.Format("D11:G{0}", rowIndexCurrentRecord)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //ws.Cells[string.Format("H11:K{0}", rowIndexCurrentRecord)].Style.Numberformat.Format = "#,##0";
                //ws.Cells[string.Format("H11:K{0}", rowIndexCurrentRecord)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //ws.Cells[string.Format("H11:K{0}", rowIndexCurrentRecord)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                //Tổng tiền
                //ws.Cells["B" + rowIndexCurrentRecord + ":" + "J" + rowIndexCurrentRecord].Merge = true;
                //ws.Cells["B" + rowIndexCurrentRecord].Value = "Tổng tiền:";
                //ws.Cells["B" + rowIndexCurrentRecord].Style.Font.Bold = true;
                //ws.Cells["B" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //ws.Cells["K" + rowIndexCurrentRecord].Value = totalprice;
                //ws.Cells["K" + rowIndexCurrentRecord].Style.Font.Bold = true;
                //ws.Cells["K" + rowIndexCurrentRecord].Style.Numberformat.Format = "#,##0";

                //ws.Cells[string.Format("A{0}:K{0}", rowIndexCurrentRecord + 1)].Merge = true;
                //ws.Cells[string.Format("A{0}:K{0}", rowIndexCurrentRecord + 2)].Merge = true;
                //ws.Cells[string.Format("A{0}:K{0}", rowIndexCurrentRecord + 3)].Merge = true;
                //ws.Cells[string.Format("A{0}:K{0}", rowIndexCurrentRecord + 4)].Merge = true;
                //ws.Cells[string.Format("A{0}:K{0}", rowIndexCurrentRecord + 5)].Merge = true;
                //ws.Cells[string.Format("A{0}:K{0}", rowIndexCurrentRecord + 6)].Merge = true;
            }
        }
        catch (Exception exc)
        {

        }
    }
}