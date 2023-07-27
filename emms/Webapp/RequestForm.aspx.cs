using System;
using System.Data;
using System.Linq;
using System.Text;
using BusinessLogic;
using BusinessLogic.Helper;
using DataAccess;

public partial class RequestForm : BasePage
{
    private int IDMaintenanceRequest
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
        if (IDMaintenanceRequest > 0)
        {
            tblTheBaoDuong maintenanceBill = MaintenanceRequestManager.GetMaintenanceRequestById(IDMaintenanceRequest);
            if (maintenanceBill != null)
            {
                lblMaThe.Text = maintenanceBill.MaThe;

                tblTrangThietBi equipment = maintenanceBill.tblTrangThietBi;
                if (equipment != null)
                {
                    lblEquipmentName.Text = equipment.Ten;
                    lblBienSo.Text = equipment.BienSo;
                    lblBaoDuongTheo.Text = equipment.BaoDuongTheo;

                    if (maintenanceBill.SoGioHoacKm.HasValue)
                    {
                        tblLoaiTrangThietBi equipmentType = equipment.tblLoaiTrangThietBi;
                        if (equipmentType != null)
                        {
                            if (equipmentType.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Gio))
                                lblSoGioHoacSoKm.Text = string.Format("{0} giờ", NumberHelper.ToStringNumber(maintenanceBill.SoGioHoacKm.ToString()));
                            else if (equipmentType.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Km))
                                lblSoGioHoacSoKm.Text = string.Format("{0} km", NumberHelper.ToStringNumber(maintenanceBill.SoGioHoacKm.ToString()));
                            else
                                lblSoGioHoacSoKm.Text = NumberHelper.ToStringNumber(maintenanceBill.SoGioHoacKm.ToString());
                        }
                    }
                }

                DateTime ngayBaoDuong = maintenanceBill.NgayBaoDuong;
                lblNgayBaoDuong.Text = string.Format("ngày {0:dd} tháng {1:MM} năm {2:yyyy}", ngayBaoDuong, ngayBaoDuong, ngayBaoDuong);
            }

            //Lấy cấp bảo dưỡng
            int levelInPrint = 0;
            tblCapBaoDuong capBaoDuong = maintenanceBill.tblCapBaoDuong;
            if (capBaoDuong != null && capBaoDuong.LevelInPrint.HasValue)
                levelInPrint = capBaoDuong.LevelInPrint.Value;

            StringBuilder sb = new StringBuilder();
            DataSet ds = MaintenanceRequestManager.GetMaintenanceDetail(IDMaintenanceRequest);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                if ((bool)row["IsBold"])
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td class=\"center\"><b>{0}<b/></td>", row["STT"]);
                    sb.AppendFormat("<td colspan=\"10\"><b>{0}<b/></td>", row["Ten"].ToString());
                    sb.Append("</tr>");
                }
                else
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td width=\"60\" class=\"center\">{0}</td>", row["STT"]);
                    sb.AppendFormat("<td>{0}</td>", row["Ten"].ToString());
                    sb.AppendFormat("<td class=\"center\" width=\"60\">{0}</td>", row["Level1"].ToString());

                    //Cấp 1
                    if (levelInPrint == 1)
                    {
                        if (row["IsChecked"] != DBNull.Value && (bool)row["IsChecked"])
                            sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-check-square\"></i>");
                        else
                            sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-square\"></i>");
                    }
                    else
                        sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-square\"></i>");

                    sb.AppendFormat("<td class=\"center\" width=\"60\">{0}</td>", row["Level2"].ToString());

                    //Cấp 2
                    if (levelInPrint == 2)
                    {
                        if (row["IsChecked"] != DBNull.Value && (bool)row["IsChecked"])
                            sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-check-square\"></i>");
                        else
                            sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-square\"></i>");
                    }
                    else
                        sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-square\"></i>");

                    sb.AppendFormat("<td class=\"center\" width=\"60\">{0}</td>", row["Level3"].ToString());

                    //Cấp 3
                    if (levelInPrint == 3)
                    {
                        if (row["IsChecked"] != DBNull.Value && (bool)row["IsChecked"])
                            sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-check-square\"></i>");
                        else
                            sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-square\"></i>");
                    }
                    else
                        sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-square\"></i>");

                    sb.AppendFormat("<td class=\"center\" width=\"60\">{0}</td>", row["Level4"].ToString());

                    //Cấp 4
                    if (levelInPrint == 4)
                    {
                        if (row["IsChecked"] != DBNull.Value && (bool)row["IsChecked"])
                            sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-check-square\"></i>");
                        else
                            sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-square\"></i>");
                    }
                    else
                        sb.AppendFormat("<td width=\"50\" class=\"checkbox\">{0}</td>", "<i class=\"far fa-square\"></i>");

                    /*
                    if (row["IsChecked"] != DBNull.Value && (bool)row["IsChecked"])
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<i class=\"far fa-check-square\"></i>");
                    else
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<i class=\"far fa-square\"></i>");

                    if (row["IsRequiresRepair"] != DBNull.Value && (bool)row["IsRequiresRepair"])
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<i class=\"far fa-check-square\"></i>");
                    else
                        sb.AppendFormat("<td class=\"center\">{0}</td>", "<i class=\"far fa-square\"></i>");
                    */

                    sb.AppendFormat("<td>{0}</td>", "");
                    sb.Append("</tr>");
                }
            }
            ltrContent.Text = sb.ToString();

            //Load tên cấp bảo dưỡng
            DataTable dtCapBaoDuong = ds.Tables[1];
            if (dtCapBaoDuong != null)
            {
                if (dtCapBaoDuong.Rows.Count == 4)
                {
                    lblLevel1_Position1.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition1"].ToString().Trim();
                    lblLevel2_Position1.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition1"].ToString().Trim();
                    lblLevel3_Position1.Text = dtCapBaoDuong.Rows[2]["ShowInPrintPosition1"].ToString().Trim();
                    lblLevel4_Position1.Text = dtCapBaoDuong.Rows[3]["ShowInPrintPosition1"].ToString().Trim();

                    lblLevel1_Position2.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition2"].ToString().Trim();
                    lblLevel2_Position2.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition2"].ToString().Trim();
                    lblLevel3_Position2.Text = dtCapBaoDuong.Rows[2]["ShowInPrintPosition2"].ToString().Trim();
                    lblLevel4_Position2.Text = dtCapBaoDuong.Rows[3]["ShowInPrintPosition2"].ToString().Trim();

                    //Cấp 1
                    int idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[0]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel1.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel1.Text = "<i class=\"far fa-square\"></i>";

                    //Cấp 2
                    idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[1]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel2.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel2.Text = "<i class=\"far fa-square\"></i>";

                    //Cấp 3
                    idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[2]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel3.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel3.Text = "<i class=\"far fa-square\"></i>";

                    //Cấp 4
                    idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[3]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel4.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel4.Text = "<i class=\"far fa-square\"></i>";
                }
                else if (dtCapBaoDuong.Rows.Count == 3)
                {
                    lblLevel1_Position1.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition1"].ToString();
                    lblLevel2_Position1.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition1"].ToString();
                    lblLevel3_Position1.Text = dtCapBaoDuong.Rows[2]["ShowInPrintPosition1"].ToString();

                    lblLevel1_Position2.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition2"].ToString();
                    lblLevel2_Position2.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition2"].ToString();
                    lblLevel3_Position2.Text = dtCapBaoDuong.Rows[2]["ShowInPrintPosition2"].ToString();

                    //Cấp 1
                    int idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[0]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel1.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel1.Text = "<i class=\"far fa-square\"></i>";

                    //Cấp 2
                    idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[1]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel2.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel2.Text = "<i class=\"far fa-square\"></i>";

                    //Cấp 3
                    idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[2]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel3.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel3.Text = "<i class=\"far fa-square\"></i>";
                }
                else if (dtCapBaoDuong.Rows.Count == 2)
                {
                    lblLevel1_Position1.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition1"].ToString();
                    lblLevel2_Position1.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition1"].ToString();

                    lblLevel1_Position2.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition2"].ToString();
                    lblLevel2_Position2.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition2"].ToString();

                    //Cấp 1
                    int idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[0]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel1.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel1.Text = "<i class=\"far fa-square\"></i>";

                    //Cấp 2
                    idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[1]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel2.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel2.Text = "<i class=\"far fa-square\"></i>";
                }
                else if (dtCapBaoDuong.Rows.Count == 1)
                {
                    lblLevel1_Position1.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition1"].ToString();
                    lblLevel1_Position2.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition2"].ToString();

                    //Cấp 1
                    int idCapBaoDuong = Convert.ToInt32(dtCapBaoDuong.Rows[0]["ID"]);
                    if (maintenanceBill.IDCapBaoDuong == idCapBaoDuong)
                        ltrLevel1.Text = "<i class=\"far fa-check-square\"></i>";
                    else
                        ltrLevel1.Text = "<i class=\"far fa-square\"></i>";
                }
            }

            //Load danh sách công việc
            string taskNameList = MaintenanceRequestManager.GetTaskNameListForPrint();
            ltrTaskNameList.Text = taskNameList;
        }
    }
}