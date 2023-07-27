using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessLogic.Security;
using DataAccess;

public partial class MaintenanceRequestFormDetail : BasePage
{
    public override string FunctionPageCode
    {
        get
        {
            return FunctionCode.Maintenance_Request_Form;
        }
    }

    private int IDEquipmentType
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

    private int IDLoaiBaoDuong
    {
        get
        {
            if (ViewState["IDLoaoBaoDuong"] != null)
                return Convert.ToInt32(ViewState["IDLoaoBaoDuong"]);
            else if (Request.QueryString.AllKeys.Contains("IDLoaoBaoDuong"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["IDLoaoBaoDuong"]))
                {
                    int result = 0;
                    int.TryParse(Request.QueryString["IDLoaoBaoDuong"], out result);
                    ViewState["IDLoaoBaoDuong"] = result;
                    return result;
                }
                else
                    return 0;
            }
            else
                return 0;
        }
        set
        {
            ViewState["IDLoaoBaoDuong"] = value;
        }
    }

    private object CongViecBaoDuong
    {
        get
        {
            if (Session["TheoBaoDuong"] != null)
                return Session["TheoBaoDuong"] as DataTable;
            else
                return null;
        }
        set
        {
            Session["TheoBaoDuong"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MasterPage.FunctionPageCode = FunctionPageCode;
                CongViecBaoDuong = null;
                maintenanceList.IDMaintenanceList = null;
                selectTaskList.IDTaskList = null;
                LoadNhomTrangThietBi();
                LoadDetail();

                btnSave.Visible = FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Add) ||
                                   FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Edit);

                // Fix lỗi nhập số thập phân >1000 sai khi PostBack
                btnSave.OnClientClick = "Control.Destroy('#" + UpdatePanel1.ClientID + "')";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PageControl", "if(typeof(Control)!='undefined')Control.Build(" + UpdatePanel1.ClientID + ")", true);

            gvMaintenanceRequest.HeaderRow.TableSection = TableRowSection.TableHeader;
            maintenanceList.GetDataFromModal += MaintenanceList_GetDataFromModal;
            selectTaskList.GetDataFromModal += SelectTask_GetDataFromModal;
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadNhomTrangThietBi()
    {
        List<tblNhomTrangThietBi> equipmentGroupList = EquipmentGroupManager.GetAllEquipmentGroup();
        tblNhomTrangThietBi findAllItem = new tblNhomTrangThietBi();
        findAllItem.ID = 0;
        findAllItem.TenNhom = "--- Chọn nhóm trang thiết bị ---";
        equipmentGroupList.Insert(0, findAllItem);

        dlNhomTrangThietBi.DataSource = equipmentGroupList;
        dlNhomTrangThietBi.DataTextField = "TenNhom";
        dlNhomTrangThietBi.DataValueField = "ID";
        dlNhomTrangThietBi.DataBind();
    }

    protected void dlNhomTrangThietBi_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLoaiTrangThietBi(int.Parse(dlNhomTrangThietBi.SelectedValue));
    }

    private void LoadLoaiTrangThietBi(int idNhomTrangThietBi)
    {
        try
        {
            List<tblLoaiTrangThietBi> equipmentTypeList = EquipmentTypeManager.GetEquipmentTypeByIDEquipmentGroup(idNhomTrangThietBi);
            tblLoaiTrangThietBi findAllItem = new tblLoaiTrangThietBi();
            findAllItem.ID = 0;
            findAllItem.Ten = "--- Chọn loại trang thiết bị ---";
            equipmentTypeList.Insert(0, findAllItem);

            dlLoaiTrangThietBi.DataSource = equipmentTypeList;
            dlLoaiTrangThietBi.DataTextField = "Ten";
            dlLoaiTrangThietBi.DataValueField = "ID";
            dlLoaiTrangThietBi.DataBind();
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private void LoadDetail()
    {
        if (IDEquipmentType > 0)
        {
            lblAction.Text = "Edit";

            //Lấy IDNhomTrangThietBi
            tblLoaiTrangThietBi equipmentType = EquipmentTypeManager.GetEquipmentTypeById(IDEquipmentType);
            if (equipmentType != null)
            {
                if (equipmentType.IDLoaiBaoDuong.HasValue)
                    IDLoaiBaoDuong = equipmentType.IDLoaiBaoDuong.Value;

                dlNhomTrangThietBi.SelectedValue = equipmentType.IDNhomTrangThietBi.Value.ToString();
                dlNhomTrangThietBi.Enabled = false;

                LoadLoaiTrangThietBi(equipmentType.IDNhomTrangThietBi.Value);
                dlLoaiTrangThietBi.SelectedValue = IDEquipmentType.ToString();
                dlLoaiTrangThietBi.Enabled = false;
            }

            //Load hạng mục
            DataTable dt = MaintenanceRequestManager.GetMaintenanceByEquipmentType(IDEquipmentType);
            gvMaintenanceRequest.DataSource = dt;
            gvMaintenanceRequest.DataBind();
            gvMaintenanceRequest.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            lblAction.Text = "Add";
            gvMaintenanceRequest.DataSource = new DataTable();
            gvMaintenanceRequest.DataBind();
            gvMaintenanceRequest.HeaderRow.TableSection = TableRowSection.TableHeader;

            gvMaintenanceRequest.Columns[2].Visible = false;
            gvMaintenanceRequest.Columns[3].Visible = false;
        }
    }

    protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = e.Row.DataItem as DataRowView;
                if (row != null)
                {
                    HiddenField hfIDHangMuc = e.Row.FindControl("hfIDHangMuc") as HiddenField;
                    if (hfIDHangMuc != null && row["IDHangMuc"] != DBNull.Value)
                        hfIDHangMuc.Value = row["IDHangMuc"].ToString();

                    Label lblSTT = e.Row.FindControl("lblSTT") as Label;
                    if (lblSTT != null && row["STT"] != DBNull.Value)
                        lblSTT.Text = row["STT"].ToString();

                    Label lblTenHangMuc = e.Row.FindControl("lblTenHangMuc") as Label;
                    if (lblTenHangMuc != null)
                        lblTenHangMuc.Text = row["Ten"].ToString();

                    if (Convert.ToBoolean(row["IsBold"]))
                    {
                        lblSTT.Font.Bold = true;
                        lblTenHangMuc.Font.Bold = true;
                    }

                    LinkButton lnkSelectTask = e.Row.FindControl("lnkSelectTask") as LinkButton;
                    if (lnkSelectTask != null && row["IDHangMuc"] == DBNull.Value)
                        lnkSelectTask.Visible = false;
                    else
                        lnkSelectTask.CommandArgument = string.Format("{0}|{1}|{2}", row["IDHangMuc"].ToString(), row["Ten"].ToString(), row["TaskIdList"].ToString());

                    HiddenField hfIDTaskList = e.Row.FindControl("hfIDTaskList") as HiddenField;
                    if (hfIDTaskList != null)
                        hfIDTaskList.Value = row["TaskIdList"].ToString();

                    HiddenField hfIDHangMucLoaiTTB = e.Row.FindControl("hfIDHangMucLoaiTTB") as HiddenField;
                    if (hfIDHangMucLoaiTTB != null && row["IDHangMucLoaiTTB"] != DBNull.Value)
                    {
                        hfIDHangMucLoaiTTB.Value = row["IDHangMucLoaiTTB"].ToString();

                        Literal ltrMoTaCongViec = e.Row.FindControl("ltrMoTaCongViec") as Literal;
                        if (ltrMoTaCongViec != null)
                            ltrMoTaCongViec.Text = GetTaskListForMaintenance((int)row["IDHangMucLoaiTTB"]);
                    }
                }
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("selectTask"))
            {
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }

                selectTaskList.IDTaskList = null;

                int idHangMuc = 0;
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hfIDHangMuc = (HiddenField)row.Cells[1].FindControl("hfIDHangMuc");
                if (hfIDHangMuc != null)
                    idHangMuc = int.Parse(hfIDHangMuc.Value);

                Label lblTenHangMuc = (Label)row.Cells[1].FindControl("lblTenHangMuc");
                if (lblTenHangMuc != null)
                    selectTaskList.TenHangMuc = lblTenHangMuc.Text;

                tblHangMucLoaiTrangThietBi hangMucLoaiTTB = MaintenanceEquipmentType.GetMaintenanceEquipmentType(idHangMuc, IDEquipmentType);
                if (hangMucLoaiTTB != null)
                {
                    selectTaskList.IDHangMuc = idHangMuc;
                    selectTaskList.IDHangMucLoaiTrangThietBi = hangMucLoaiTTB.ID;
                }

                selectTaskList.IDLoaiBaoDuong = IDLoaiBaoDuong;
                selectTaskList.LoadCapBaoDuong(IDLoaiBaoDuong);
                selectTaskList.GetTaskList();
                modalPopupExtTaskList.Show();
                //OpenModalTask(); //bỏ, dùng để mở popup sử dụng bootstrap
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }

    private string GetTaskListForMaintenance(int idHangMucLoaiTrangThietBi)
    {
        StringBuilder sb = new StringBuilder();

        List<tblCapBaoDuong> capBaoDuongList = MaintenanceLevelManager.GetMaintenanceChildByIdLoaiBaoDuong(IDLoaiBaoDuong);
        if (capBaoDuongList != null && capBaoDuongList.Count > 0)
        {
            foreach (tblCapBaoDuong item in capBaoDuongList)
            {
                string strTaskList = string.Empty;
                List<tblCapBaoDuongCongViec> taskList = MaintenanceRequestManager.GetMaintenanceLevelTask(idHangMucLoaiTrangThietBi, item.ID);
                if (taskList.Count > 0)
                {
                    foreach (tblCapBaoDuongCongViec capBaoDuongCongViec in taskList)
                    {
                        if (strTaskList == string.Empty)
                            strTaskList = capBaoDuongCongViec.tblCongViec.MaCongViec;
                        else
                            strTaskList = string.Format("{0}, {1}", strTaskList, capBaoDuongCongViec.tblCongViec.MaCongViec);
                    }
                }
                sb.Append(item.Ten + string.Format(": {0}<br/>", strTaskList));
            }
        }

        return sb.ToString();
    }

    #region Thêm các hạng mục 

    protected void btnShowPopup_Click(object sender, EventArgs e)
    {
        if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Edit))
        {
            ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
            return;
        }

        //Lấy các hạng mục đã chọn
        maintenanceList.IDMaintenanceList = null;
        List<int> idList = new List<int>();
        foreach (GridViewRow row in gvMaintenanceRequest.Rows)
        {
            HiddenField hfIDHangMuc = row.FindControl("hfIDHangMuc") as HiddenField;
            if (hfIDHangMuc != null && !string.IsNullOrEmpty(hfIDHangMuc.Value))
                idList.Add(int.Parse(hfIDHangMuc.Value));
        }
        maintenanceList.IDMaintenanceList = idList;
        maintenanceList.TenHangMuc = dlNhomTrangThietBi.Text;
        maintenanceList.GetMaintenanceList();
        modalPopupExt.Show();
    }

    private void MaintenanceList_GetDataFromModal(List<int> IDMaintenanceList)
    {
        if (IDEquipmentType == 0)
        {
            string idMaintenanceList = string.Empty;
            if (IDMaintenanceList.Count > 0)
            {
                foreach (int item in IDMaintenanceList)
                {
                    if (idMaintenanceList == string.Empty)
                        idMaintenanceList = item.ToString();
                    else
                        idMaintenanceList = string.Format("{0},{1}", idMaintenanceList, item.ToString());
                }
            }
            DataTable dt = MaintenanceRequestManager.GetMaintenanceRequestFormForCreateForm(idMaintenanceList);
            gvMaintenanceRequest.DataSource = dt;
            gvMaintenanceRequest.DataBind();
            gvMaintenanceRequest.HeaderRow.TableSection = TableRowSection.TableHeader;

            gvMaintenanceRequest.Columns[2].Visible = false; //danh sách công việc
            gvMaintenanceRequest.Columns[3].Visible = false; //chọn công việc
        }
        else
        {
            List<tblHangMucLoaiTrangThietBi> mainEquipTypeInDatabase = MaintenanceEquipmentType.GetMaintenanceEquipmentType(IDEquipmentType);

            //Kiểm tra nếu chưa có dưới database thì thêm mới
            foreach (int idMaintenance in IDMaintenanceList)
            {
                tblHangMucLoaiTrangThietBi mainEquipType = MaintenanceEquipmentType.GetMaintenanceEquipmentType(idMaintenance, IDEquipmentType);
                if (mainEquipType == null)
                {
                    tblHangMucLoaiTrangThietBi newItem = new tblHangMucLoaiTrangThietBi();
                    newItem.IDHangMuc = idMaintenance;
                    newItem.IDLoaiTrangThietBi = IDEquipmentType;
                    MaintenanceEquipmentType.InsertMaintenanceEquipmentType(newItem);
                }
            }

            //Kiểm tra nếu đã có dưới database nhưng có có trong list thì xóa khỏi database
            foreach (tblHangMucLoaiTrangThietBi item in mainEquipTypeInDatabase)
            {
                bool isExists = false;
                foreach (int idMaintenance in IDMaintenanceList)
                {
                    if (item.IDHangMuc == idMaintenance)
                    {
                        isExists = true;
                        break;
                    }
                }
                if (!isExists)
                {
                    MaintenanceRequestManager.DeleteMaintenanceRequestForm(item.IDHangMuc, item.IDLoaiTrangThietBi);
                }
            }

            LoadDetail();
        }

        UpdatePanel1.Update();
    }

    private void OpenModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectMaintenanceDialog').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenModalScript", sb.ToString(), false);
    }

    private void HideModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectMaintenanceDialog').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
    }

    #endregion

    #region Chọn công việc theo từng cấp cho các hạng mục

    private void SelectTask_GetDataFromModal(List<int> idTaskList, int idHangMuc, int idCapBaoDuong)
    {
        //Lưu các công việc đã chọn 
        tblHangMucLoaiTrangThietBi hangMucLoaiTTB = MaintenanceEquipmentType.GetMaintenanceEquipmentType(idHangMuc, IDEquipmentType);
        if (hangMucLoaiTTB != null)
        {
            List<tblCapBaoDuongCongViec> listInDatabase = MaintenanceRequestManager.GetMaintenanceLevelTask(hangMucLoaiTTB.ID, idCapBaoDuong);

            foreach (int idTask in idTaskList)
            {
                //Kiểm tra nếu chưa có dưới database thì thêm mới
                List<tblCapBaoDuongCongViec> listSelected = MaintenanceRequestManager.GetMaintenanceLevelTask(hangMucLoaiTTB.ID, idCapBaoDuong, idTask);
                if (listSelected.Count == 0)
                {
                    tblCapBaoDuongCongViec item = new tblCapBaoDuongCongViec();
                    item.IDHangMucLoaiTrangThietBi = hangMucLoaiTTB.ID;
                    item.IDCapBaoDuong = idCapBaoDuong;
                    item.IDCongViec = idTask;
                    MaintenanceRequestManager.InsertCapBaoDuongCongViec(item);
                }
            }

            //Kiểm tra nếu bỏ chọn trên lưới thì xóa dưới database
            if (listInDatabase.Count > 0)
            {
                foreach (tblCapBaoDuongCongViec item in listInDatabase)
                {
                    bool isExists = false;
                    foreach (int idTask in idTaskList)
                    {
                        if (item.IDCongViec == idTask)
                        {
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)
                        MaintenanceRequestManager.DeleteMaintenanceLevelTask(item.ID);
                }
            }
        }

        DataTable dt = null;
        if (IDEquipmentType == 0 && CongViecBaoDuong != null)
            dt = CongViecBaoDuong as DataTable;
        else
            dt = MaintenanceRequestManager.GetMaintenanceByEquipmentType(IDEquipmentType);

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["IDHangMuc"] != DBNull.Value && (int)row["IDHangMuc"] == idHangMuc)
                {
                    string taskNameList = string.Empty;
                    string strIdTaskList = string.Empty;
                    foreach (int item in idTaskList)
                    {
                        tblCongViec task = TaskManager.GetTaskById(item);
                        if (task != null)
                        {
                            if (taskNameList == string.Empty)
                            {
                                taskNameList = task.MaCongViec;
                                strIdTaskList = task.ID.ToString();
                            }
                            else
                            {
                                taskNameList = string.Format("{0}, {1}", taskNameList, task.MaCongViec);
                                strIdTaskList = string.Format("{0}, {1}", strIdTaskList, task.ID);
                            }
                        }
                    }
                    row.SetField("TaskNameList", taskNameList);
                    row.SetField("TaskIdList", strIdTaskList);
                }
            }
        }

        CongViecBaoDuong = dt;
        gvMaintenanceRequest.DataSource = dt;
        gvMaintenanceRequest.DataBind();
        gvMaintenanceRequest.HeaderRow.TableSection = TableRowSection.TableHeader;
        UpdatePanel1.Update();
    }

    private void OpenModalTask()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectTaskDialog').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenModalScript", sb.ToString(), false);
    }

    private void HideModalTask()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectTaskDialog').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IDEquipmentType > 0)
            {
                //Kiểm tra quyền sửa
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Edit))
                {
                    ShowMessage(this, "You don't have permission to Edit this item", MessageType.Message);
                    return;
                }
            }
            else
            {
                //Kiểm tra quyền thêm
                if (!FunctionManager.CheckUserHasRight(CurrentUserRight, RightCode.Maintenance_Request_Form_Add))
                {
                    ShowMessage(this, "You don't have permission to Add this item", MessageType.Message);
                    return;
                }
            }

            if (IDEquipmentType == 0)
            {
                #region Insert

                if (dlNhomTrangThietBi.SelectedValue.Equals("0"))
                {
                    ShowMessage(this, "Vui lòng chọn một nhóm trang thiết bị.", MessageType.Message);
                    return;
                }

                //Hạng mục nhóm trang thiết bị
                int idLoaiTTB = int.Parse(dlLoaiTrangThietBi.SelectedValue);

                //Kiểm tra nhóm trang thiết bị này đã được tạo mẫu in hay chưa
                if (MaintenanceEquipmentType.CheckExistsEquipmentType(idLoaiTTB))
                {
                    ShowMessage(this, "Mẫu in cho loại trang thiết bị này đã được tạo. Vui lòng chọn một loại trang thiết bị khác để tạo mẫu in.",
                                MessageType.Message);
                    return;
                }

                if (gvMaintenanceRequest.Rows.Count == 0)
                {
                    ShowMessage(this, "Vui lòng chọn các hạng mục bảo dưỡng.", MessageType.Message);
                    return;
                }

                //Hạng mục bảo dưỡng
                foreach (GridViewRow row in gvMaintenanceRequest.Rows)
                {
                    HiddenField hfIDHangMuc = row.FindControl("hfIDHangMuc") as HiddenField;
                    HiddenField hfIDTaskList = row.FindControl("hfIDTaskList") as HiddenField;
                    if (hfIDHangMuc != null && hfIDTaskList != null && !string.IsNullOrEmpty(hfIDHangMuc.Value))
                    {
                        int idHangMucBaoDuong = int.Parse(hfIDHangMuc.Value);
                        tblHangMucLoaiTrangThietBi hangMucNhomTTB = new tblHangMucLoaiTrangThietBi();
                        hangMucNhomTTB.IDLoaiTrangThietBi = idLoaiTTB;
                        hangMucNhomTTB.IDHangMuc = idHangMucBaoDuong;
                        hangMucNhomTTB = MaintenanceEquipmentType.InsertMaintenanceEquipmentType(hangMucNhomTTB);
                    }
                }
                Response.Redirect(string.Format("MaintenanceRequestFormDetail.aspx?ID={0}", idLoaiTTB.ToString()));

                #endregion
            }
            else
            {
                #region Update

                DataTable dtFromDataBase = MaintenanceRequestManager.GetMaintenanceByEquipmentType(IDEquipmentType);

                //Duyệt qua các hạng mục trên lưới, so sánh trong database nếu chưa có thì thêm vào
                List<int> idHangMucList = new List<int>();
                foreach (GridViewRow row in gvMaintenanceRequest.Rows)
                {
                    HiddenField hfIDHangMuc = row.FindControl("hfIDHangMuc") as HiddenField;
                    if (hfIDHangMuc != null && !string.IsNullOrEmpty(hfIDHangMuc.Value))
                    {
                        int idHangMucBaoDuong = int.Parse(hfIDHangMuc.Value);
                        bool isSaved = false;
                        foreach (DataRow rowInDb in dtFromDataBase.Rows)
                        {
                            if (rowInDb["IdHangMuc"] != DBNull.Value && idHangMucBaoDuong == (int)rowInDb["IdHangMuc"])
                            {
                                idHangMucList.Add(idHangMucBaoDuong);
                                isSaved = true;
                                break;
                            }
                        }
                        if (!isSaved)
                        {
                            tblHangMucLoaiTrangThietBi hangMucNhomTTB = new tblHangMucLoaiTrangThietBi();
                            hangMucNhomTTB.IDLoaiTrangThietBi = IDEquipmentType;
                            hangMucNhomTTB.IDHangMuc = idHangMucBaoDuong;
                            hangMucNhomTTB = MaintenanceEquipmentType.InsertMaintenanceEquipmentType(hangMucNhomTTB);
                        }
                    }
                }

                //tìm các hạng mục dưới database trên lưới, nếu không tìm thấy thì xóa
                foreach (DataRow rowInDb in dtFromDataBase.Rows)
                {
                    if (rowInDb["IdHangMuc"] != DBNull.Value)
                    {
                        bool isInList = false;
                        int idHangMuc = (int)rowInDb["IdHangMuc"];
                        foreach (int item in idHangMucList)
                        {
                            if (idHangMuc == item)
                            {
                                isInList = true;
                                break;
                            }
                        }
                        if (!isInList)
                        {
                            //Xóa hạng mục bảo dưỡng
                            MaintenanceEquipmentType.DeleteMaintenanceEquipmentType(idHangMuc, IDEquipmentType);
                        }
                    }
                }

                CongViecBaoDuong = null;
                LoadDetail();
                ShowMessage(this, "Your data has been successfully saved.", MessageType.Message);

                #endregion
            }
        }
        catch (Exception exc)
        {
            ShowMessage(this, exc.ToString(), MessageType.Error);
        }
    }
}