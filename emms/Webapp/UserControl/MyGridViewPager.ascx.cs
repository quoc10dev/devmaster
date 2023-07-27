using BusinessLogic.Helper;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class UserControl_MyGridViewPager : System.Web.UI.UserControl
{
    public int TotalRecord
    {
        get
        {
            if (ViewState["TotalRecord"] != null)
                return Convert.ToInt32(ViewState["TotalRecord"]);
            else
                return 0;
        }
        set
        {
            ViewState["TotalRecord"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            drPageSize.SelectedValue = SystemSetting.PageSize.ToString();
        }
    }

    public void PopulatePager(int recordCount, int currentPage, int pageSize)
    {
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pageSize));
        int totalPage = (int)Math.Ceiling(dblPageCount);

        lblTotalPages.Text = totalPage.ToString();

        List<ListItem> pages = new List<ListItem>();
        if (totalPage > 0)
        {
            int maxTotalButton = 5; //số index button tối đa

            //Trang điều hướng về trang đầu - First button (<<) - (disable nếu trang hiện tại bằng 1) 
            pages.Add(new ListItem("&laquo;", "1", currentPage > 1));

            //Trang điều hướng về trang trước - Previous button (<) - (disable nếu trang hiện tại là 1)
            if (currentPage != 1)
                pages.Add(new ListItem("&lsaquo;", (currentPage - 1).ToString()));

            //Trường hợp 1: tổng số trang <= maxTotalButton thì bind hết (disable trang hiện tại)
            if (totalPage <= maxTotalButton)
            {
                for (int i = 1; i <= totalPage; i++)
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            else if (currentPage < maxTotalButton)
            {
                //Trường hợp 2: maxTotalButton < currentPage < tổng số trang (disable trang hiện tại)
                //thêm dấu ... cho biết còn trang sau
                for (int i = 1; i <= maxTotalButton; i++)
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                pages.Add(new ListItem("...", (currentPage).ToString(), false));
            }
            else if (currentPage > totalPage - maxTotalButton)
            {
                //Trường hợp 3: currentPage > (totalPage - maxTotalButton) --> bind các trang còn lại
                //thêm dấu ... cho biết còn các trang trước
                pages.Add(new ListItem("...", (currentPage).ToString(), false));

                int addPageNum = totalPage - currentPage;
                for (int i = currentPage - (maxTotalButton - addPageNum - 1); i <= totalPage; i++)
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            else
            {
                //TH4: trước cũng còn trang, sau cũng còn trang thì thêm trước sau 2 trang
                pages.Add(new ListItem("...", (currentPage).ToString(), false));
                for (int i = currentPage - 2; i <= currentPage + 2; i++)
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));

                pages.Add(new ListItem("...", (currentPage).ToString(), false));
            }

            //Trang điều hướng về trang sau - Next button (>) - (disable khi trang hiện tại là cuối)
            if (currentPage != totalPage)
                pages.Add(new ListItem("&rsaquo;", (currentPage + 1).ToString()));

            //trang cuối Last button (>>) - (disable trang hiện tại bằng trang cuối) 
            pages.Add(new ListItem("&raquo;", totalPage.ToString(), currentPage < totalPage));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();

        //Ẩn thông PageSize khi không có record nào
        if (recordCount == 0)
            liPageSize.Visible = false;
        else
            liPageSize.Visible = true;
        
        //Nếu tổng số record < số lượng item quy định hiển thị trên 1 trang thì ẩn các nút phân trang
        if (recordCount <= pageSize)
        {
            rptPager.Visible = false;
            liPageSize.Visible = false;
        }
        else
        {
            rptPager.Visible = true;
            liPageSize.Visible = true;
        }
    }

    protected void rptPager_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        //Gán thuộc tính trong code cs
        LinkButton lkb = (LinkButton)e.Item.FindControl("lnkPage");
        HtmlGenericControl liPager = e.Item.FindControl("liPager") as HtmlGenericControl;
        if (lkb != null && liPager != null)
        {
            ListItem item = e.Item.DataItem as ListItem;
            if (item != null)
            {
                if (!item.Enabled)
                {
                    if (item.Text.Equals("&laquo;") || item.Text.Equals("&raquo;") || item.Text.Equals("..."))
                    {
                        lkb.Attributes.Add("class", "page-link");
                        liPager.Attributes.Add("class", "page-item");
                    }
                    else
                    {
                        liPager.Attributes.Add("class", "page-item active");
                        lkb.Attributes.Add("class", "page-link");
                    }
                }
                else
                {
                    liPager.Attributes.Add("class", "page-item");
                    lkb.Attributes.Add("class", "page-link");
                }


                //if (item.Text.Equals("&laquo;") || item.Text.Equals("&raquo;") || item.Text.Equals("..."))
                //{
                //    lkb.Attributes.Add("class", "page-link");
                //    liPager.Attributes.Add("class", "page-item");
                //}
                //else
                //{
                //    lkb.Attributes.Add("class", "page-link");
                //    liPager.Attributes.Add("class", "page-item");
                //}
            }
        }
    }

    //protected string GetItemClass(bool enable)
    //{
    //    //class='<%# GetItemClass(Convert.ToBoolean(Eval("Enabled"))) %>' thêm trong thẻ li để chạy hàm này
    //    return !enable ? "active" : string.Empty;
    //}

    //Event Handler Declaration
    public event EventHandler CreateClick;
    //Triggering the Event in User control
    public void CallParentEvent(object sender, EventArgs e)
    {
        if (CreateClick != null)
            CreateClick(sender, e);
    }
    //Calling the Event trigger after action completed in User control
    protected void Page_Changed(object sender, EventArgs e)
    {
        //your code here then trigger the event
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        CallParentEvent(pageIndex, e);
    }

    public event EventHandler SelectIndexChange;
    public void CallParentSelectIndexChangeEvent(object sender, EventArgs e)
    {
        if (SelectIndexChange != null)
            SelectIndexChange(sender, e);
    }
    protected void drPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        int pageSize = int.Parse(drPageSize.SelectedValue);
        if (SelectIndexChange != null)
            SelectIndexChange(pageSize, e);
    }

    public void SetTotalRecord(int totalRecord)
    {
        lbTotalRow.Text = totalRecord.ToString("n0");
    }
}