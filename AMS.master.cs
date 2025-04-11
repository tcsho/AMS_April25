using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class AMS : System.Web.UI.MasterPage
{

    string innerHtml = "<nav>";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeCode"] == null)
        {
            Response.Redirect("~/login.aspx", false);
            return;
        }
        if (!IsPostBack)
        {


            BindMenu(0);

            innerHtml += "</nav>";

            this.div_menu.InnerHtml = innerHtml;


            lblEmp.Text = Session["EmployeeCode"].ToString();
            lblName.Text = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
            lblHOD.Text = GetHOD();
            if (Session["RegionName"].ToString() == "")
            {
                lblRegion.Text = "Head Office";
            }
            else
            {
                lblRegion.Text = Session["RegionName"].ToString();
            }
            if (Session["CenterName"].ToString() == "")
            {
                lblCenter.Text = "Head Office";
            }
            else
            {
                lblCenter.Text = Session["CenterName"].ToString();
            }
        }
    }

    private string GetHOD()
    {

        string HOD = "";
        BLLEmployeeReportTo objBll = new BLLEmployeeReportTo();
        DataTable _dt = new DataTable();
        objBll.Status_id = 1;
        objBll.EmployeeCode = Session["EmployeeCode"].ToString();
        _dt = objBll.EmployeeReportToHODSelectByEmployeeCode(objBll);

        if (_dt.Rows.Count > 0)
        {
            HOD = _dt.Rows[0]["HODEmployeeCode"].ToString() + '-' + _dt.Rows[0]["FullName"].ToString();
        }
        else
        {
            HOD = "";
        }

        return HOD;
    }

    private void BindMenu()
    {
        string innerHtml = "<nav>" +
                    "<ul>"
                        + "<li><a href='EmployeeLeavesSubmissions.aspx'>Home</a></li>";

        if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
        {
            innerHtml += "<li><a href='HODApprovals.aspx'>Approvals</a></li>";
        }

        innerHtml += "<li><a href='Reports_new.aspx?r=N4548'>Reports</a></li>"
                        + "<li><a href='#'>Change password</a></li>"
                    + "</ul>"
                + "</nav>";
        this.div_menu.InnerHtml = innerHtml;
    }
    public void BindMenu(int _PrntMenu)
    {
        BLLLmsAppMenu ObjMenu = new BLLLmsAppMenu();
        ObjMenu.User_Type_Id = Convert.ToInt32(Session["UserType"]);
        ObjMenu.PrntMenu_ID = _PrntMenu;
        ObjMenu.EmployeeCode = Session["EmployeeCode"].ToString();
        DataTable _dtmenu = ObjMenu.LmsAppMenuFetch(ObjMenu);

        innerHtml += "<ul>";

        foreach (DataRow subMenu in _dtmenu.Rows)
        {
            if (Convert.ToBoolean(subMenu["isAllow"]))
            {
                innerHtml += "<li>";



                if (Convert.ToInt32(subMenu["isChild"].ToString()) > 0)
                {
                    innerHtml += subMenu["MenuText"].ToString();

                    BindMenu(Convert.ToInt32(subMenu["Menu_ID"].ToString()));
                }
                else
                {
                    if (subMenu["Menu_ID"].ToString() == "45" && (Session["RegionID"].ToString() == "2000000" ||
                        Session["RegionID"].ToString() == "3000000" || Session["RegionID"].ToString() == "4000000"))
                        continue;
                    else

                        innerHtml += "<a href=\"" +
                        Page.ResolveUrl(subMenu["PagePath"].ToString()) + " \">" +
                        subMenu["MenuText"].ToString() + "</a>";
                }

                innerHtml += "</li>";
            }
        }
        innerHtml += "</ul>";

    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }

}
