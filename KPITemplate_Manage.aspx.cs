using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KPITemplate_Manage : System.Web.UI.Page
{
    BLLKPITemplate objbll = new BLLKPITemplate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindKPIGrid();
        }
    }

    private void BindKPIGrid()
    {
        try
        {
            BLLKPITemplate obj = new BLLKPITemplate();
            DataTable dt = objbll.KPITemplateFetch(obj);

            gvKPITemplates.DataSource = dt;
            gvKPITemplates.DataBind();
        }
        catch (Exception ex)
        {
            // Optionally show error message
            // lblMessage.Text = "Error loading data: " + ex.Message;
        }
    }
    protected void gvKPITemplates_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvKPITemplates.DataKeys[e.RowIndex].Value);
        var username = Session["UserName"].ToString(); // make sure Session["UserId"] exists

        BLLKPITemplate obj = new BLLKPITemplate();
        obj.TemplateId = id; // this was missing before

        BLLKPITemplate objbll = new BLLKPITemplate(); // Or reuse the one you already declared
        objbll.KPITemplateDelete(obj, username); // call delete with correct values

        BindKPIGrid(); // refresh
    }
    protected void gvKPITemplates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditTemplate")
        {
            string id = e.CommandArgument.ToString();
            Response.Redirect("KPITemplate.aspx?tid=" + id);

        }
    }

    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPITemplate.aspx", false);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPISelection.aspx", false);
    }
}