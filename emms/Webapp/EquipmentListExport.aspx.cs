using System;
using System.Data;
using System.Drawing;
using BusinessLogic;
using DataAccess;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class EquipmentListExport : BasePage
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

    private string TenXe
    {
        get
        {
            return GetValueOfQueryStringTypeString("Ten");
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
            ShowMessage(this, exc.ToString(), MessageType.Error);
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

            DataTable dt = EquipmentManager.SearchEquipmentToExport(IDCongTy, IDNhomTrangThietBi, IDLoaiTrangThietBi, BienSo, TenXe);

            using (ExcelPackage exp = new ExcelPackage())
            {
                //Tạo worksheet    
                ExcelWorksheet ws = exp.Workbook.Worksheets.Add("Equipment List");
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

                ws.Cells["H2:L2"].Merge = true;
                ws.Cells["H2"].Value = string.Format("Cam Ranh, ngày {0:dd} tháng {1:MM} năm {2:yyyy}", DateTime.Now, DateTime.Now, DateTime.Now);
                ws.Cells["H2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Row(2).Height = 30;

                //Tiêu đề báo cáo
                ws.Cells["A3:L3"].Merge = true;
                ws.Cells["A3"].Value = "DANH SÁCH TRANG THIẾT BỊ";
                ws.Cells["A3"].Style.Font.Bold = true;
                ws.Cells["A3"].Style.Font.Size = 15;
                ws.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Row(3).Height = 30;

                ws.Cells["E4:H4"].Merge = true;
                ws.Cells["E4"].Value = string.Format("Công ty: {0}", tenCongTy);
                ws.Cells["E4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["E4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["E5:H5"].Merge = true;
                ws.Cells["E5"].Value = string.Format("Nhóm xe: {0}", tenNhomTTB);
                ws.Cells["E5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["E5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["E6:H6"].Merge = true;
                ws.Cells["E6"].Value = string.Format("Loại xe: {0}", tenLoaiTTB);
                ws.Cells["E6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["E6"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["E7:H7"].Merge = true;
                ws.Cells["E7"].Value = string.Format("Biển số: {0}", !string.IsNullOrEmpty(BienSo) ? BienSo : "Tất cả");
                ws.Cells["E7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["E7"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["E8:H8"].Merge = true;
                ws.Cells["E8"].Value = string.Format("Tên xe: {0}", !string.IsNullOrEmpty(TenXe) ? BienSo : "Tất cả");
                ws.Cells["E8"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["E8"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                //Tên tiêu đề cột
                int indexTitle = 10;
                ws.Cells[string.Format("A{0}", indexTitle)].Value = "STT";
                ws.Column(1).Width = 7;

                ws.Cells[string.Format("B{0}", indexTitle)].Value = "Công ty";
                ws.Column(2).Width = 7;

                ws.Cells[string.Format("C{0}", indexTitle)].Value = "Nhóm xe";
                ws.Column(3).Width = 15;

                ws.Cells[string.Format("D{0}", indexTitle)].Value = "Loại xe";
                ws.Column(4).Width = 19;

                ws.Cells[string.Format("E{0}", indexTitle)].Value = "Tên xe";
                ws.Column(5).Width = 15;

                ws.Cells[string.Format("F{0}", indexTitle)].Value = "Biển số";
                ws.Column(6).Width = 12;

                ws.Cells[string.Format("G{0}", indexTitle)].Value = "Mã tài sản";
                ws.Column(7).Width = 12;

                ws.Cells[string.Format("H{0}", indexTitle)].Value = "Số khung";
                ws.Column(8).Width = 12;

                ws.Cells[string.Format("I{0}", indexTitle)].Value = "Loại máy";
                ws.Column(9).Width = 12;

                ws.Cells[string.Format("J{0}", indexTitle)].Value = "Số máy";
                ws.Column(10).Width = 12;

                ws.Cells[string.Format("K{0}", indexTitle)].Value = "Năm sản xuất";
                ws.Column(11).Width = 9;

                ws.Cells[string.Format("L{0}", indexTitle)].Value = "Nước sản xuất";
                ws.Column(12).Width = 9;

                using (ExcelRange objRange = ws.Cells[string.Format("A{0}:L{1}", indexTitle, indexTitle)])
                {
                    objRange.Style.Font.Bold = true;
                    objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                int index = 1;
                int rowIndexBeginOrders = 11;
                int rowIndexCurrentRecord = rowIndexBeginOrders;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["RowNum"] != null)
                    {
                        ws.Cells["A" + rowIndexCurrentRecord].Value = row["RowNum"];
                        ws.Cells["A" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["CongTy"] != DBNull.Value)
                        ws.Cells["B" + rowIndexCurrentRecord].Value = row["CongTy"].ToString();

                    if (row["NhomXe"] != DBNull.Value)
                        ws.Cells["C" + rowIndexCurrentRecord].Value = row["NhomXe"].ToString();

                    if (row["LoaiXe"] != DBNull.Value)
                        ws.Cells["D" + rowIndexCurrentRecord].Value = row["LoaiXe"].ToString();

                    if (row["TenXe"] != DBNull.Value)
                        ws.Cells["E" + rowIndexCurrentRecord].Value = row["TenXe"].ToString();

                    if (row["BienSo"] != DBNull.Value)
                        ws.Cells["F" + rowIndexCurrentRecord].Value = row["BienSo"].ToString();

                    if (row["MaTaiSan"] != DBNull.Value)
                        ws.Cells["G" + rowIndexCurrentRecord].Value = row["MaTaiSan"].ToString();

                    if (row["SoKhung"] != DBNull.Value)
                        ws.Cells["H" + rowIndexCurrentRecord].Value = row["SoKhung"].ToString();

                    if (row["LoaiMay"] != DBNull.Value)
                        ws.Cells["I" + rowIndexCurrentRecord].Value = row["LoaiMay"].ToString();

                    if (row["SoMay"] != DBNull.Value)
                        ws.Cells["J" + rowIndexCurrentRecord].Value = row["SoMay"].ToString();

                    if (row["NamSanXuat"] != DBNull.Value)
                    {
                        ws.Cells["K" + rowIndexCurrentRecord].Value = row["NamSanXuat"];
                        ws.Cells["K" + rowIndexCurrentRecord].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        ws.Cells["K" + rowIndexCurrentRecord].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["NuocSanXuat"] != DBNull.Value)
                        ws.Cells["L" + rowIndexCurrentRecord].Value = row["NuocSanXuat"].ToString();

                    rowIndexCurrentRecord++;
                    index++;
                }

                //Setting Top/left,right/bottom borders.
                var cell = ws.Cells[string.Format("A{0}:L{1}", indexTitle, rowIndexCurrentRecord - 1)];
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                var border = cell.Style.Border;
                border.Bottom.Style = ExcelBorderStyle.Thin;
                border.Top.Style = ExcelBorderStyle.Thin;
                border.Left.Style = ExcelBorderStyle.Thin;
                border.Right.Style = ExcelBorderStyle.Thin;

                string fileName = "EMMS_EquipmentList.xlsx";
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