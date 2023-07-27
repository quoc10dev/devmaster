using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_SelectAnEquipment : System.Web.UI.UserControl
{
    public string IDEquipment
    {
        get
        {
            if (ViewState["IDEquipment"] != null)
                return ViewState["IDEquipment"].ToString();
            else
                return string.Empty;
        }
        set
        {
            ViewState["IDEquipment"] = value;
        }
    }

    public void GetEquipment(int idLoaiTrangThietBi)
    {
        List<tblTrangThietBi> dt = EquipmentManager.GetEquipmentByIDEquimentTypeForSelect(idLoaiTrangThietBi);
        gvList1.DataSource = dt;
        gvList1.DataBind();
        
        //if (HttpContext.Current != null)
        //{
        //    Page page = (Page)HttpContext.Current.Handler;
        //    GridView gvList1 = (GridView)page.FindControl("gvList1");
        //    gvList1.DataSource = dt;
        //    gvList1.DataBind();
        //}

        gvList1.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvList1.DataSource = new List<tblTrangThietBi>();
            gvList1.DataBind();
        }
        gvList1.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvList1_RowCreated(object sender, GridViewRowEventArgs e)
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

    private void HideModal()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalSelectEquipmentDialog').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "DeleteModalScript", sb.ToString(), true);
    }


    protected void btnSelect_Click(object sender, EventArgs e)
    {
        //if (gvList1.SelectedIndex >= 0)
        //{
        //    IDEquipment = gvList1.SelectedRow.Cells[1].Text;
        //    Label label = (this.Parent.Page).FindControl("lblTenXe") as Label;
        //    if (label != null)
        //        label.Text = IDEquipment;
        //}
        ////thay this bang UpdatePanel1
        ////ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Pop", "ClosePopup();", true);
        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Close", "$('#modalSelectEquipmentDialog').modal('hide');", true);
        ////HideModal();

        if (CurrentRowIndex != null)
        {
            string strValue = hfColumnValue.Value.ToString();
            hfCurrentRowIndex.Value = string.Empty;
            hfParentContainer.Value = string.Empty;
            hfColumnValue.Value = string.Empty;
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
}