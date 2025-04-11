using System;
using System.Linq;

public partial class PresentationLayer_ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
        {
        try
            {
            if (Session["ContactID"] == null)
                {
                Response.Redirect("~/login.aspx");
                }
            }
        catch (Exception)
            {
            }

        lab_error.Text = Session["error"].ToString();
        }
    }