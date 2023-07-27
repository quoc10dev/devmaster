using System;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Helper;
using DataAccess;

public partial class RepairRequestForm : BasePage
{
    private int IDRepairRequest
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
        if (IDRepairRequest > 0)
        {
            tblPhieuSuaChua repairRequest = RepairRequestManager.GetRepairRequestById(IDRepairRequest);
            if (repairRequest != null)
            {
                lblMaPhieu.Text = repairRequest.MaPhieu;
                
                tblTrangThietBi equipment = repairRequest.tblTrangThietBi;
                if (equipment != null)
                {
                    lblEquipmentName.Text = equipment.Ten;
                    lblBienSo.Text = equipment.BienSo;

                    if (repairRequest.SoGioHoacKm.HasValue)
                    {
                        tblLoaiTrangThietBi equipmentType = equipment.tblLoaiTrangThietBi;
                        if (equipmentType != null)
                        {
                            if (equipmentType.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Gio))
                                lblSoGioHoacSoKm.Text = string.Format("{0} giờ", NumberHelper.ToStringNumber(repairRequest.SoGioHoacKm.ToString()));
                            else if (equipmentType.DonViGhiNhanHoatDong.Equals(DonViGhiNhanHoatDong.Km))
                                lblSoGioHoacSoKm.Text = string.Format("{0} km", NumberHelper.ToStringNumber(repairRequest.SoGioHoacKm.ToString())); 
                            else
                                lblSoGioHoacSoKm.Text = NumberHelper.ToStringNumber(repairRequest.SoGioHoacKm.ToString());
                        }
                    }
                }
                //DateTime ngayBaoDuong = repairRequest.NgayBaoDuong;
                //lblNgayBaoDuong.Text = string.Format("ngày {0:dd} tháng {1:MM} năm {2:yyyy}", ngayBaoDuong, ngayBaoDuong, ngayBaoDuong);
            }

            //StringBuilder sb = new StringBuilder();
            //DataSet ds = MaintenanceRequestManager.GetMaintenanceDetail(IDRepairRequest);
            //DataTable dt = ds.Tables[0];
            //foreach (DataRow row in dt.Rows)
            //{
            //    if ((bool)row["IsBold"])
            //    {
            //        sb.Append("<tr>");
            //        sb.AppendFormat("<td class=\"center\"><b>{0}<b/></td>", row["STT"]);
            //        sb.AppendFormat("<td colspan=\"8\"><b>{0}<b/></td>", row["Ten"].ToString());
            //        sb.Append("</tr>");
            //    }
            //    else
            //    {
            //        sb.Append("<tr>");
            //        sb.AppendFormat("<td class=\"center\">{0}</td>", row["STT"]);
            //        sb.AppendFormat("<td>{0}</td>", row["Ten"].ToString());
            //        sb.AppendFormat("<td>{0}</td>", row["Level1"].ToString());
            //        sb.AppendFormat("<td>{0}</td>", row["Level2"].ToString());
            //        sb.AppendFormat("<td>{0}</td>", row["Level3"].ToString());
            //        sb.AppendFormat("<td>{0}</td>", row["Level4"].ToString());

            //        if (row["IsChecked"] != DBNull.Value && (bool)row["IsChecked"])
            //            sb.AppendFormat("<td class=\"center\">{0}</td>", "<i class=\"far fa-check-square\"></i>");
            //        else
            //            sb.AppendFormat("<td class=\"center\">{0}</td>", "<i class=\"far fa-square\"></i>");

            //        if (row["IsRequiresRepair"] != DBNull.Value && (bool)row["IsRequiresRepair"])
            //            sb.AppendFormat("<td class=\"center\">{0}</td>", "<i class=\"far fa-check-square\"></i>");
            //        else
            //            sb.AppendFormat("<td class=\"center\">{0}</td>", "<i class=\"far fa-square\"></i>");

            //        sb.AppendFormat("<td>{0}</td>", "");
            //        sb.Append("</tr>");
            //    }
            //}
            //ltrContent.Text = sb.ToString();

            

            //Load danh sách công việc
            //string taskNameList = MaintenanceRequestManager.GetTaskNameListForPrint();
            //ltrTaskNameList.Text = taskNameList;
        }
    }
}