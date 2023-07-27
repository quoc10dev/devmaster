using System;
using System.Linq;
using System.Net;

public partial class ViewPdf : System.Web.UI.Page
{
    private string PathOfFile
    {
        get
        {
            if (ViewState["Path"] != null)
                return ViewState["Path"].ToString();
            else if (Request.QueryString.AllKeys.Contains("Path"))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Path"]))
                {
                    ViewState["Path"] = Request.QueryString["Path"];
                    return Request.QueryString["Path"];
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string path = Server.MapPath(PathOfFile);
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(path);

            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
        }
        catch (Exception ex)
        {
            
        }
    }
}