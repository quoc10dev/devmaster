using AjaxControlToolkit;
using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_TaskListPopupExtender : System.Web.UI.UserControl
{
    public delegate void UserControl_TaskListDelegate(List<int> idTaskList, int idHangMuc, int idCapBaoDuong);

    public event UserControl_TaskListDelegate GetDataFromModal;

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

    public int IDHangMucLoaiTrangThietBi
    {
        get
        {
            if (ViewState["IDHangMucLoaiTrangThietBi"] != null)
                return (int)ViewState["IDHangMucLoaiTrangThietBi"];
            else
                return 0;
        }
        set
        {
            ViewState["IDHangMucLoaiTrangThietBi"] = value;
        }
    }

    public int IDLoaiBaoDuong
    {
        get
        {
            if (ViewState["IDLoaiBaoDuong"] != null)
                return (int)ViewState["IDLoaiBaoDuong"];
            else
                return 0;
        }
        set
        {
            ViewState["IDLoaiBaoDuong"] = value;
        }
    }

    public List<int> IDTaskList
    {
        get
        {
            if (Session["IDTaskList"] != null)
                return Session["IDTaskList"] as List<int>;
            else
                return new List<int>();
        }
        set
        {
            Session["IDTaskList"] = value;
        }
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IDTaskList = null;
            gvTaskList.DataSource = new List<tblCongViec>(); 
            gvTaskList.DataBind();
        }
        gvTaskList.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    public void LoadCapBaoDuong(int idLoaiBaoDuong)
    {
        dlCapBaoDuong.DataSource = MaintenanceLevelManager.GetMaintenanceChildByIdLoaiBaoDuong(idLoaiBaoDuong);
        dlCapBaoDuong.DataTextField = "Ten";
        dlCapBaoDuong.DataValueField = "ID";
        dlCapBaoDuong.DataBind();
    }

    private void LoadTaskList()
    {
        List<tblCapBaoDuongCongViec> capBaoDuongCongViecList = MaintenanceRequestManager.GetMaintenanceLevelTask(IDHangMucLoaiTrangThietBi, 
                                                                                                                int.Parse(dlCapBaoDuong.SelectedValue));
        List<int> idTaskList = new List<int>();
        foreach (tblCapBaoDuongCongViec item in capBaoDuongCongViecList)
            idTaskList.Add(item.IDCongViec);
        IDTaskList = idTaskList;
    }

    public void GetTaskList()
    {
        lblMaintennanceName.Text = TenHangMuc;
        LoadTaskList();
        List<tblCongViec> dt = TaskManager.GetAllTask();
        gvTaskList.DataSource = dt;
        gvTaskList.DataBind();
        gvTaskList.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void dlCapBaoDuong_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTaskList();
        UpdatePanel2.Update();
    }

    protected void gvTaskList_RowCreated(object sender, GridViewRowEventArgs e)
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

            tblCongViec row = e.Row.DataItem as tblCongViec;
            if (row != null)
            {
                HiddenField hfIDCongViec = e.Row.FindControl("hfIDCongViec") as HiddenField;
                if (hfIDCongViec != null)
                    hfIDCongViec.Value = row.ID.ToString();

                CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
                if (chkSelect != null && IDTaskList.Count > 0)
                {
                    foreach (int idTask in IDTaskList)
                    {
                        if (row.ID == idTask)
                            chkSelect.Checked = true;
                    }
                }
            }
        }
    }

    protected void gvTaskList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tblCongViec row = e.Row.DataItem as tblCongViec;
                if (row != null)
                {
                    HiddenField hfIDCongViec = e.Row.FindControl("hfIDCongViec") as HiddenField;
                    if (hfIDCongViec != null)
                        hfIDCongViec.Value = row.ID.ToString();

                    CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null && IDTaskList.Count > 0)
                    {
                        foreach (int idTask in IDTaskList)
                        {
                            if (row.ID == idTask)
                                chkSelect.Checked = true;
                        }
                    }
                }
            }
        }
        catch (Exception exc)
        {
            //ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox cb = (CheckBox)gvTaskList.Rows[selRowIndex].FindControl("chkSelect");
        if (cb.Checked)
        {
            HiddenField hf = (HiddenField)gvTaskList.Rows[selRowIndex].FindControl("hfIDCongViec");
            if (hf != null && !string.IsNullOrEmpty(hf.Value))
            {
                List<int> taskList = IDTaskList;
                taskList.Add(int.Parse(hf.Value));
                IDTaskList = taskList;
            }
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(dlCapBaoDuong.SelectedValue))
            {
                return;
            }

            int idCapBaoDuong = int.Parse(dlCapBaoDuong.SelectedValue);

            //Kiểm tra loại bỏ các idTask không chọn trên lưới ra khỏi danh sách IDTaskList
            int idTask = 0;
            List<int> idCongViecList = new List<int>();
            foreach (GridViewRow row in gvTaskList.Rows)
            {
                CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                if (chkSelect != null && !chkSelect.Checked)
                {
                    HiddenField hfIDCongViec = row.FindControl("hfIDCongViec") as HiddenField;
                    if (hfIDCongViec != null)
                    {
                        idTask = int.Parse(hfIDCongViec.Value);
                        for (int i = 0; i < IDTaskList.Count; i++)
                        {
                            if (IDTaskList[i] == idTask)
                                IDTaskList.RemoveAt(i);
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

            //Đóng popup
            var popup = Parent.FindControl("modalPopupExtTaskList") as ModalPopupExtender;
            popup.Hide();

            if (GetDataFromModal != null)
                GetDataFromModal(IDTaskList, IDHangMuc, idCapBaoDuong);
        }
        catch (Exception exc)
        {
            //ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        var popup = Parent.FindControl("modalPopupExtTaskList") as ModalPopupExtender;
        popup.Hide();
    }

    private void HideModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectTaskDialog').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteModalScript", sb.ToString(), true);
    }

    private void OpenModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectTaskDialog').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteModalScript", sb.ToString(), true);
    }
}