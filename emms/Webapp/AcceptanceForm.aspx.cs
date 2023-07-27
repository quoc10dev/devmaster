using System;
using System.Data;
using System.Linq;
using System.Text;
using BusinessLogic;

public partial class AcceptanceForm : BasePage
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
            //LoadDetail();
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
            //tblTheBaoDuong maintenanceBill = MaintenanceRequestManager.GetMaintenanceRequestById(IDMaintenanceRequest);
            //if (maintenanceBill != null)
            //{
            //    tblLoaiTrangThietBi equipmentType = null;
            //    tblTrangThietBi equipment = maintenanceBill.tblTrangThietBi;
            //    if (equipment != null)
            //    {
            //        //lblEquipmentName.Text = equipment.Ten;
            //        //lblBienSo.Text = equipment.BienSo;

            //        //equipmentType = equipment.tblLoaiTrangThietBi;
            //        //if (equipmentType != null)
            //        //{
            //        //}
            //    }
            //    DateTime ngayBaoDuong = maintenanceBill.NgayBaoDuong;
            //    //lblNgayBaoDuong.Text = string.Format("ngày {0:dd} tháng {1:MM} năm {2:yyyy}", ngayBaoDuong, ngayBaoDuong, ngayBaoDuong);
            //}

            StringBuilder sb = new StringBuilder();
            DataSet ds = MaintenanceRequestManager.GetMaintenanceDetail(IDMaintenanceRequest);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                if ((bool)row["IsBold"])
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td class=\"center\"><b>{0}<b/></td>", row["STT"]);
                    sb.AppendFormat("<td colspan=\"8\"><b>{0}<b/></td>", row["Ten"].ToString());
                    sb.Append("</tr>");
                }
                else
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td class=\"center\">{0}</td>", row["STT"]);
                    sb.AppendFormat("<td>{0}</td>", row["Ten"].ToString());
                    sb.AppendFormat("<td>{0}</td>", row["Level1"].ToString());
                    sb.AppendFormat("<td>{0}</td>", row["Level2"].ToString());
                    sb.AppendFormat("<td>{0}</td>", row["Level3"].ToString());
                    sb.AppendFormat("<td>{0}</td>", row["Level4"].ToString());
                    sb.AppendFormat("<td class=\"checkbox\">{0}</td>", "<span>&#9633;</span>");
                    sb.AppendFormat("<td class=\"checkbox\">{0}</td>", "<span>&#9633;</span>");
                    sb.AppendFormat("<td>{0}</td>", "");
                    sb.Append("</tr>");
                }
            }
            //ltrContent.Text = sb.ToString();

            /*
            //Load tên cấp bảo dưỡng
            DataTable dtCapBaoDuong = ds.Tables[1];
            if (dtCapBaoDuong != null)
            {
                if (dtCapBaoDuong.Rows.Count == 4)
                {
                    lblLevel1_Position1.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition1"].ToString();
                    lblLevel2_Position1.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition1"].ToString();
                    lblLevel3_Position1.Text = dtCapBaoDuong.Rows[2]["ShowInPrintPosition1"].ToString();
                    lblLevel4_Position1.Text = dtCapBaoDuong.Rows[3]["ShowInPrintPosition1"].ToString();

                    lblLevel1_Position2.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition2"].ToString();
                    lblLevel2_Position2.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition2"].ToString();
                    lblLevel3_Position2.Text = dtCapBaoDuong.Rows[2]["ShowInPrintPosition2"].ToString();
                    lblLevel4_Position2.Text = dtCapBaoDuong.Rows[3]["ShowInPrintPosition2"].ToString();
                }
                else if (dtCapBaoDuong.Rows.Count == 3)
                {
                    lblLevel1_Position1.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition1"].ToString();
                    lblLevel2_Position1.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition1"].ToString();
                    lblLevel3_Position1.Text = dtCapBaoDuong.Rows[2]["ShowInPrintPosition1"].ToString();

                    lblLevel1_Position2.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition2"].ToString();
                    lblLevel2_Position2.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition2"].ToString();
                    lblLevel3_Position2.Text = dtCapBaoDuong.Rows[2]["ShowInPrintPosition2"].ToString();
                }
                else if (dtCapBaoDuong.Rows.Count == 2)
                {
                    lblLevel1_Position1.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition1"].ToString();
                    lblLevel2_Position1.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition1"].ToString();

                    lblLevel1_Position2.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition2"].ToString();
                    lblLevel2_Position2.Text = dtCapBaoDuong.Rows[1]["ShowInPrintPosition2"].ToString();
                }
                else if (dtCapBaoDuong.Rows.Count == 1)
                {
                    lblLevel1_Position1.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition1"].ToString();

                    lblLevel1_Position2.Text = dtCapBaoDuong.Rows[0]["ShowInPrintPosition2"].ToString();
                }
                
            }
            */
            //Load danh sách công việc
            //string taskNameList = MaintenanceRequestManager.GetTaskNameListForPrint();
            //ltrTaskNameList.Text = taskNameList;
        }
    }
}