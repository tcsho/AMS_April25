using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KPISelection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeCode"] == null)
        {
            Response.Redirect("~/login.aspx");
        }
        if (Session["EmployeeCode"] != null)
        {
            int _part_Id = Convert.ToInt32(Session["UserType"].ToString());
            if (_part_Id == 19)
            {
                btnMngKPI.Visible = true;
                btnAsgnKPI.Visible = true;
                btnUpdKPI.Visible = true;
            }
            else
            {
                btnMngKPI.Visible = false;
                btnAsgnKPI.Visible = true;
                btnUpdKPI.Visible = true;
            }
        }
    }

    protected void btnMngKPI_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPITemplate_Manage.aspx", false);
    }

    protected void btnAsgnKPI_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPITemplate_Assign.aspx", false);
    }

    protected void btnUpdKPI_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPI_UpdateEmpTemplate.aspx", false);
    }
}