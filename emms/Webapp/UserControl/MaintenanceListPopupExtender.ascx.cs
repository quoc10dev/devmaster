using AjaxControlToolkit;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_MaintenanceListPopupExtender : System.Web.UI.UserControl
{
    public delegate void UserControl_MaintenanceListDelegate(List<int> IDMaintenanceList);

    public event UserControl_MaintenanceListDelegate GetDataFromModal;

    public int IDHangMuc
    {
        get
        {
            if (ViewState["IDHangMuc"] != null)
                return (int)ViewState["IDHangMuc"];
            else
                return 0;
        }
        set
        {
            ViewState["IDHangMuc"] = value;
        }
    }

    public string TenHangMuc
    {
        get
        {
            if (ViewState["TenHangMuc"] != null)
                return ViewState["TenHangMuc"].ToString();
            else
                return string.Empty;
        }
        set
        {
            ViewState["TenHangMuc"] = value;
        }
    }

    public void GetMaintenanceList()
    {
        //lblMaintennanceName.Text = TenHangMuc;
        
        DataTable dt = MaintenanceManager.GetAllMaintenanceForCreateMaintenanceForm();
        gvMaintenanceList.DataSource = dt;
        gvMaintenanceList.DataBind();
        gvMaintenanceList.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IDMaintenanceList = null;
            gvMaintenanceList.DataSource = new DataTable();
            gvMaintenanceList.DataBind();
        }
        gvMaintenanceList.HeaderRow.TableSection = TableRowSection.TableHeader;
        Page.MaintainScrollPositionOnPostBack = true;
    }

    protected void gvMaintenanceList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow &&
           (e.Row.RowState == DataControlRowState.Normal ||
           e.Row.RowState == DataControlRowState.Alternate)
           )
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow1(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling1(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
        }
    }

    protected void gvMaintenanceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    Label lblSTT = e.Row.FindControl("lblSTT") as Label;
                    if (lblSTT != null)
                    {
                        lblSTT.Text = row["STT"].ToString();
                        if ((bool)row["IsBold"])
                            lblSTT.Font.Bold = true;
                    }

                    Label lblTen = e.Row.FindControl("lblTen") as Label;
                    if (lblTen != null)
                    {
                        lblTen.Text = row["Ten"].ToString();
                        if ((bool)row["IsBold"])
                            lblTen.Font.Bold = true;
                    }

                    HiddenField hfIDHangMuc = e.Row.FindControl("hfIDHangMuc") as HiddenField;
                    if (hfIDHangMuc != null)
                        hfIDHangMuc.Value = row["IDHangMuc"].ToString();

                    CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null)
                    {
                        if (row["IDHangMuc"] != DBNull.Value && !(bool)row["IsBold"])
                        {
                            if (IDMaintenanceList.Count > 0)
                            {
                                foreach (int idTask in IDMaintenanceList)
                                {
                                    if ((int)row["IDHangMuc"] == idTask)
                                        chkSelect.Checked = true;
                                }
                            }
                        }
                        else
                            chkSelect.Visible = false;
                    }
                }
            }
        }
        catch (Exception exc)
        {
            //ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    public List<int> IDMaintenanceList
    {
        get
        {
            if (Session["IDMaintenanceList"] != null)
                return Session["IDMaintenanceList"] as List<int>;
            else
                return new List<int>();
        }
        set
        {
            Session["IDMaintenanceList"] = value;
        }
    }

    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb = (CheckBox)gvMaintenanceList.Rows[selRowIndex].FindControl("chkSelect");
        if (cb.Checked)
        {
            HiddenField hf = (HiddenField)gvMaintenanceList.Rows[selRowIndex].FindControl("hfIDHangMuc");
            if (hf != null && !string.IsNullOrEmpty(hf.Value))
            {
                List<int> maintenanceList = IDMaintenanceList;
                maintenanceList.Add(int.Parse(hf.Value));
                IDMaintenanceList = maintenanceList;
            }
        }
        //ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "Pop", "openModalMaintenance();", true);
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            //Kiểm tra loại bỏ các idTask không chọn trên lưới ra khỏi danh sách IDTaskList
            int idMaintenance = 0;
            List<int> idCongViecList = new List<int>();
            foreach (GridViewRow row in gvMaintenanceList.Rows)
            {
                CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                if (chkSelect != null && !chkSelect.Checked)
                {
                    HiddenField hfIDHangMuc = row.FindControl("hfIDHangMuc") as HiddenField;
                    if (hfIDHangMuc != null && !string.IsNullOrEmpty(hfIDHangMuc.Value))
                    {
                        idMaintenance = int.Parse(hfIDHangMuc.Value);
                        for (int i = 0; i < IDMaintenanceList.Count; i++)
                        {
                            if (IDMaintenanceList[i] == idMaintenance)
                                IDMaintenanceList.RemoveAt(i);
                        }
                    }
                }
            }

            if (CurrentRowIndex != null)
            {
                string strValue = hfColumnValue.Value.ToString();
                hfCurrentRowIndex.Value = string.Empty;
                hfParentContainer.Value = string.Empty;
                hfColumnValue.Value = string.Empty;
            }
            

            if (GetDataFromModal != null)
                GetDataFromModal(IDMaintenanceList);

            //HideModal();
        }
        catch (Exception exc)
        {
            //ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        var popup = Parent.FindControl("modalPopupExt") as ModalPopupExtender;
        popup.Hide();
    }

    private object CurrentRowIndex
    {
        get
        {
            int result = 0;
            if (int.TryParse(hfCurrentRowIndex.Value, out result))
            {
                return result;
            }
            else
                return null;
        }
    }

    private void HideModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectMaintenanceDialog').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteModalScript", sb.ToString(), true);
    }

    private void OpenModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectMaintenanceDialog').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteModalScript", sb.ToString(), true);
    }
}