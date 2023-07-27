using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using BusinessLogic;
using DataAccess;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class ReportActivityHistoryExport : BasePage
{
    private DateTime FromDate
    {
        get
        {
            string strFromDate = GetValueOfQueryStringTypeString("FromDate");
            DateTime fromDate = DateTime.ParseExact(strFromDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            return fromDate;
        }
    }

    private DateTime ToDate
    {
        get
        {
            string strToDate = GetValueOfQueryStringTypeString("ToDate");
            DateTime toDate = DateTime.ParseExact(strToDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            return toDate;
        }
    }

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

            DataTable dt = OperationProcessingManager.SearchActivityHistory(FromDate, ToDate, IDCongTy, IDNhomTrangThietBi, IDLoaiTrangThietBi,
                                                                        BienSo);
            using (ExcelPackage exp = new ExcelPackage())
            {
                //Tạo worksheet    
                ExcelWorksheet ws = exp.Workbook.Worksheets.Add("Activity history");
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

                ws.Cells["B2:C2"].Merge = true;
                ws.Cells["B2"].Value = "Công ty TNHH Dịch Vụ Mặt Đất Hàng Không AGS";

                ws.Cells["D2:G2"].Merge = true;
                ws.Cells["D2"].Value = string.Format("Cam Ranh, ngày {0:dd} tháng {1:MM} năm {2:yyyy}", DateTime.Now, DateTime.Now, DateTime.Now);
                ws.Cells["D2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                ws.Row(2).Height = 30;

                //Tiêu đề báo cáo
                ws.Cells["A3:H3"].Merge = true;
                ws.Cells["A3"].Value = "LỊCH SỬ HOẠT ĐỘNG TRANG THIẾT BỊ";
                ws.Cells["A3"].Style.Font.Bold = true;
                ws.Cells["A3"].Style.Font.Size = 15;
                ws.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Row(3).Height = 30;

                ws.Cells["C4:G4"].Merge = true;
                ws.Cells["C4"].Value = string.Format("Công ty: {0}", tenCongTy);
                ws.Cells["C4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["C4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["C5:G5"].Merge = true;
                ws.Cells["C5"].Value = string.Format("Nhóm xe: {0}", tenNhomTTB);
                ws.Cells["C5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["C5"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["C6:G6"].Merge = true;
                ws.Cells["C6"].Value = string.Format("Loại xe: {0}", tenLoaiTTB);
                ws.Cells["C6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["C6"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["C7:G7"].Merge = true;
                ws.Cells["C7"].Value = string.Format("Biển số: {0}", !string.IsNullOrEmpty(BienSo) ? BienSo : "Tất cả");
                ws.Cells["C7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["C7"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                ws.Cells["C8:G8"].Merge = true;
                ws.Cells["C8"].Value = string.Format("Từ ngày: {0:dd/MM/yyyy} - {1:dd/MM/yyyy}", FromDate, ToDate);
                ws.Cells["C8"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                ws.Cells["C8"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                //Tên tiêu đề cột
                int indexTitle = 10;
                ws.Cells[string.Format("A{0}", indexTitle)].Value = "STT";
                ws.Column(1).Width = 7;

                ws.Cells[string.Format("B{0}", indexTitle)].Value = "Nhóm xe";
                ws.Column(2).Width = 30;

                ws.Cells[string.Format("C{0}", indexTitle)].Value = "Loại xe";
                ws.Column(3).Width = 30;

                ws.Cells[string.Format("D{0}", indexTitle)].Value = "Biển số";
                ws.Column(4).Width = 15;

                int idxCol = 0;
                int index = 1;
                int rowIndexBeginOrders = 11;
                int rowIndexCurrentRecord = rowIndexBeginOrders;

                DateTime fromDate = FromDate;
                DateTime toDate = ToDate;
                TimeSpan days = toDate - fromDate;
                int soNgayChenhLech = Convert.ToInt32(days.TotalDays);

                foreach (DataRow row in dt.Rows)
                {
                    fromDate = FromDate;

                    if (row["STT"] != null)
                        {
                        ws.Cells[rowIndexCurrentRecord, 1].Value = row["STT"].ToString();
                        ws.Cells[rowIndexCurrentRecord, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    if (row["TenNhom"] != null)
                        ws.Cells[rowIndexCurrentRecord, 2].Value = row["TenNhom"].ToString();

                    if (row["TenLoai"] != null)
                        ws.Cells[rowIndexCurrentRecord, 3].Value = row["TenLoai"].ToString();

                    if (row["BienSo"] != null && row["BienSo"] != DBNull.Value)
                        ws.Cells[rowIndexCurrentRecord, 4].Value = row["BienSo"].ToString();

                    idxCol = 5; //vị trí cột ngày đầu tiên
                    string strDate = string.Empty;
                    for (int i = 0; i <= soNgayChenhLech; i++)
                    {
                        strDate = string.Format("{0:yyyyMMdd}", fromDate);

                        if (row[strDate] != null && row[strDate] != DBNull.Value)
                        {
                            //Tiêu đề
                            ws.Cells[indexTitle, idxCol].Value = string.Format("{0:dd/MM/yyyy}", fromDate);
                            ws.Cells[indexTitle, idxCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[rowIndexCurrentRecord, idxCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            //Giá trị
                            ws.Cells[rowIndexCurrentRecord, idxCol].Value = row[strDate];
                            ws.Cells[rowIndexCurrentRecord, idxCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[rowIndexCurrentRecord, idxCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Column(idxCol).Width = 12; 
                        }

                        //Total
                        if (row["Total"] != null && row["Total"] != DBNull.Value)
                        {
                            ws.Cells[rowIndexCurrentRecord, idxCol + 1].Value = row["Total"];
                            ws.Cells[rowIndexCurrentRecord, idxCol + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            ws.Cells[rowIndexCurrentRecord, idxCol + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        fromDate = fromDate.AddDays(1);
                        idxCol++;
                    }

                    rowIndexCurrentRecord++;
                    index++;
                }

                //Tiêu đề cột Total
                ws.Cells[indexTitle, idxCol].Value = "Total";

                //Setting Top/left,right/bottom borders.
                var cell = ws.Cells[indexTitle, 1, rowIndexCurrentRecord - 1, idxCol];
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                var border = cell.Style.Border;
                border.Bottom.Style = ExcelBorderStyle.Thin;
                border.Top.Style = ExcelBorderStyle.Thin;
                border.Left.Style = ExcelBorderStyle.Thin;
                border.Right.Style = ExcelBorderStyle.Thin;

                //Tô đậm cột
                using (ExcelRange objRange = ws.Cells[indexTitle, 1, indexTitle, idxCol])
                {
                    objRange.Style.Font.Bold = true;
                    objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                string fileName = "EMMS_ActivityHistoryReport.xlsx";
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