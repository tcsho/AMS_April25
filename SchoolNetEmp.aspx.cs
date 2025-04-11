using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;
public partial class SchoolNetEmp : System.Web.UI.Page
{
    BLLNetworkTeam _objnetwork = new BLLNetworkTeam();
    DALBase objBase = new DALBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                try
                {

                    int UserLevel, UserType;
                    if (Session["employeeCode"] == null)
                    {
                        Response.Redirect("~/login.aspx");
                    }
                    UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
                    UserType = Convert.ToInt32(Session["UserType"].ToString());
                    loadRegions();
                    if (UserLevel == 2)
                    {
                        ddlRegion.SelectedValue = "0";
                        ddlRegion.Enabled = true;
                        ddlRegion.Visible = true;
                        ddlRegion_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    if (Session["RegionID"] != null)
                    {
                        ddlRegion.SelectedValue = Session["RegionID"].ToString();
                        ddlRegion_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    loadMonths();
                    BindGrid();
                }
                catch (Exception ex)
                {
                    Session["error"] = ex.Message;
                    Response.Redirect("ErrorPage.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    public void loadRegions()
    {
        try
        {
            BLLVacationTimigs objBll = new BLLVacationTimigs();
            DataTable _dt = new DataTable();
            _dt = objBll.fetchRegions();
            objBase.FillDropDown(_dt, ddlRegion, "Region_Id", "Region_Name");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Data"] = null;
            BindGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    public void BindGrid()
    {
        try
        {
            if (ddlRegion.SelectedIndex > 0)
                _objnetwork.NetworkRegion_Id = Convert.ToInt32(ddlRegion.SelectedValue);
            else
            {
                ImpromptuHelper.ShowPrompt("Please select a Region");
                return;
            }
            DataTable dt = _objnetwork.NetworkTeamSelectByLocation(_objnetwork);
            if (dt.Rows.Count > 0)
            {
                gvNetwork.DataSource = dt;
                gvNetwork.DataBind();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void gvNetwork_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvNetwork.Rows.Count > 0)
            {
                gvNetwork.UseAccessibleHeader = false;
                gvNetwork.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
    protected void lnkLocation_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            gvNetwork.SelectedIndex = gvr.RowIndex;
            _objnetwork.InSchool = Convert.ToBoolean(btn.CommandArgument);
            _objnetwork.EmployeeCode = gvr.Cells[1].Text;
            int k = _objnetwork.NetworkEmployeesinSchool_Insert(_objnetwork);
            ViewState["Data"] = null;
            EmployeeProcess(_objnetwork.EmployeeCode);
            BindGrid();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void loadMonths()
    {
        try
        {
            BLLPeriod obj = new BLLPeriod();

            DataTable dt = new DataTable();
            obj.InActive = "n";
            dt = obj.PeriodFetch(obj);
            if (dt.Rows.Count > 0)
            {
                ddlMonths.DataTextField = "PMonthDesc";
                ddlMonths.DataValueField = "PMonth";
                ddlMonths.DataSource = dt;
                ddlMonths.DataBind();
            }

            ddlMonths.SelectedValue = Session["CurrentMonth"].ToString();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
        
    }

    protected void EmployeeProcess(string emp)
    {
        try
        {
            BLLAttendance objatt = new BLLAttendance();
            objatt.PMonthDesc = ddlMonths.SelectedValue;
            objatt.EmployeeCode = emp;
            objatt.AttendanceProcessSingleEmployee(objatt);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
}