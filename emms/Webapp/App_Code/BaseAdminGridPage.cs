using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BusinessLogic.Helper;

/// <summary>
/// Summary description for BaseAdminGridPage
/// </summary>
public class BaseAdminGridPage: BasePage
{
    public BaseAdminGridPage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int PageSize
    {
        //Số dòng trên 1 trang
        get
        {
            if (ViewState["PageSize"] == null)
                ViewState["PageSize"] = SystemSetting.PageSize;
            return (int)ViewState["PageSize"];
        }
        set
        {
            ViewState["PageSize"] = value;
        }
    }

    public int PageIndex
    {
        //Vị trí trang hiện tại
        get
        {
            if (ViewState["PageIndex"] == null)
                ViewState["PageIndex"] = 1;
            return (int)ViewState["PageIndex"];
        }
        set
        {
            ViewState["PageIndex"] = value;
        }
    }

    public string SortExpression
    {
        //Tên cột cần sắp xếp
        get
        {
            return ViewState["SortExpression"].ToString();
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }

    public SortDirection Direction
    {
        //Sắp xếp tăng dần/giảm dần: mặc định tăng dần
        get
        {
            if (ViewState["DirectionState"] == null)
                ViewState["DirectionState"] = SortDirection.Ascending;
            return (SortDirection)ViewState["DirectionState"];
        }
        set
        {
            ViewState["DirectionState"] = value;
        }
    }

    public string GetSortOrder
    {
        //Kiểm tra đang sắp xếp tăng hay giảm
        get
        {
            return (Direction == SortDirection.Ascending) ? Sort.ASC : Sort.DESC;
        }
    }

    protected virtual void GridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Gán các image tương ứng với điều kiện sắp xếp tăng/giảm của cột
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    for (int i = 0; i < e.Row.Cells.Count; i++)
        //    {
        //        DataControlFieldHeaderCell obj = (DataControlFieldHeaderCell)e.Row.Cells[i];
        //        if (!String.IsNullOrEmpty(SortExpression) && obj.ContainingField.SortExpression == SortExpression)
        //            obj.Attributes.Add("class", (Direction == SortDirection.Ascending) ? "sorting_asc" : "sorting_desc"); //style của bootstrap jquery.dataTables.css
        //        else
        //            obj.Attributes.Add("class", "sorting");
        //    }
        //}

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript: if (typeof(SelectRow) !== 'undefined') SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
        }
    }

    public virtual void GridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        //Lấy tên cột cần sắp xếp, và điều kiện sắp xếp
        if (Direction == SortDirection.Ascending)
            Direction = SortDirection.Descending;
        else
            Direction = SortDirection.Ascending;

        SortExpression = e.SortExpression;
        this.BindData();
    }

    public virtual void BindData()
    {

    }
}