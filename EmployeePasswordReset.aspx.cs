using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class EmployeePasswordReset : System.Web.UI.Page
{
    DALBase objBase = new DALBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["SortDirection"] = "ASC";
                loadRegions();
                ddlRegion.SelectedValue = Session["RegionID"].ToString();
                loadCenters();

                if (Session["CenterID"].ToString() != "")
                {
                    int center = Convert.ToInt32(Session["CenterID"].ToString());
                    if (center > 0)
                    {
                        ddlCenter.SelectedValue = Session["CenterID"].ToString();
                        ddlCenter_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    loadDepartments();
                }
                else
                {
                    loadDepartments();
                }
                if (Session["UserType"].ToString() != "" && Session["UserType"].ToString() == "25")
                {
                    ddlDept.Enabled = true;
                    ddlCenter.Enabled = false;
                    ddlRegion.Enabled = false;
                    //gvEmployees.Columns[9].Visible = false;
                }
                else if (Session["UserType"].ToString() != "" && Session["UserType"].ToString() == "19")
                {
                    ddlDept.Enabled = true;
                    ddlCenter.Enabled = true;
                    ddlRegion.Enabled = true;
                }
                else if (Session["UserType"].ToString() != "" && Session["UserType"].ToString() == "22")
                {
                    ddlDept.Enabled = true;
                    ddlCenter.Enabled = true;
                    ddlRegion.Enabled = false;
                }
                ViewState["dtDetails"] = null;
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        try
        {
            gvEmployees.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable oDataSet = (DataTable)ViewState["dtDetails"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            BindGrid();

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
            BLLSpecialCasesTimigs objBll = new BLLSpecialCasesTimigs();
            DataTable _dt = new DataTable();

            _dt = objBll.fetchRegions();

            ddlRegion.DataTextField = "Region_Name";
            ddlRegion.DataValueField = "Region_Id";
            ddlRegion.DataSource = _dt;
            ddlRegion.DataBind();

            ddlRegion.Items.Insert(0, new ListItem("Head Office", "0"));
            ddlRegion.SelectedValue = Session["RegionID"].ToString();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void loadCenters()
    {
        try
        {
            BLLSpecialCasesTimigs objBll = new BLLSpecialCasesTimigs();
            DataTable _dt = new DataTable();

            objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);
            _dt = objBll.fetchCenters(objBll);

            ddlCenter.DataTextField = "Center_Name";
            ddlCenter.DataValueField = "Center_ID";
            ddlCenter.DataSource = _dt;
            ddlCenter.DataBind();

            if (ddlRegion.SelectedValue == "0")
            {
                ddlCenter.Items.Insert(0, new ListItem("Head Office", "0"));
            }
            else
            {
                ddlCenter.Items.Insert(0, new ListItem("Regional Office", "0"));
            }
            //ddlCenter.Items.Insert(0, new ListItem("Select Center", "0"));
            ViewState["dtMain"] = null;
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
            loadCenters();
            loadDepartments();
            if (ddlRegion.SelectedValue == "600" || ddlRegion.SelectedValue == "700")
            {
                if (ddlCenter.Items.Count > 1)
                {
                    ddlCenter.SelectedIndex = ddlCenter.Items.Count - 1;
                }
                else
                {
                    ddlCenter.SelectedIndex = 0;
                }
                ddlCenter_SelectedIndexChanged(this, EventArgs.Empty);
            }
            if (ddlRegion.SelectedValue == "800")
            {
                ddlCenter.SelectedIndex = 1;
                ddlCenter_SelectedIndexChanged(this, EventArgs.Empty);
            }

            ViewState["dtDetails"] = null;
            BindGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void loadDepartments()
    {
        try
        {

            if (ddlRegion.SelectedValue != "0")
            {
                BLLEmployeeReportToHOD objBll = new BLLEmployeeReportToHOD();

                DataTable dt = new DataTable();
                objBll.PMonth = Session["CurrentMonth"].ToString();
                objBll.regionId = Convert.ToInt32(ddlRegion.SelectedValue);
                objBll.centerId = Convert.ToInt32(ddlCenter.SelectedValue);
                dt = objBll.WebDepartmentSelectByMonthRegionCenter(objBll);
                objBase.FillDropDown(dt, ddlDept, "Deptcode", "DeptName");
            }
            else
            {
                BLLAttendance obj = new BLLAttendance();

                DataTable dt = new DataTable();
                obj.PMonthDesc = Session["CurrentMonth"].ToString();
                //obj.PMonthDesc = "201612";
                obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
                obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
                dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);
                objBase.FillDropDown(dt, ddlDept, "Deptcode", "DeptName");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void BindGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            BLLUser objuser = new BLLUser();
            objuser.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
            if (objuser.Region_Id > 0)
            {
                objuser.Center_Id = Convert.ToInt32(ddlCenter.SelectedValue);
            }
            else
            {
                objuser.Center_Id = 0;
            }
            objuser.DeptCode = Convert.ToInt32(ddlDept.SelectedValue);
            if (ViewState["dtDetails"] == null)
            {
                dt = objuser.UserSelectAll(objuser);
                ViewState["dtDetails"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["dtDetails"];
            }
            gvEmployees.DataSource = dt;
            gvEmployees.DataBind();
            string s = Session["UserType"].ToString();
            if (Session["UserType"].ToString() == "25")
            {
                gvEmployees.Columns[9].Visible = false;
                gvEmployees.DataBind();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadDepartments();
            if (ddlCenter.SelectedIndex > 0 && (ddlCenter.SelectedValue != "801" || ddlCenter.SelectedValue != "802" || ddlCenter.SelectedValue != "803"))
            {
                if (ddlDept.Items.Count > 1)
                {
                    ddlDept.SelectedIndex = ddlDept.Items.Count - 1;
                }
                else
                {
                    ddlDept.SelectedIndex = ddlDept.Items.Count - 1;

                }
            }
            if (ddlRegion.SelectedValue == "800")
            {
                ddlDept.SelectedIndex = 0;
            }
            ViewState["dtDetails"] = null;
            BindGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["dtDetails"] = null;
            BindGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            string id = imgbtn.CommandArgument;
            btnSave_Click(this, EventArgs.Empty, id);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e, string id)
    {
        try
        {
            BLLUser obj = new BLLUser();
            obj.User_Name = id;
            obj.Password = txtPassword.Text.Trim();
            int k = obj.UserPasswordUpdate(obj);
            if (k == 0)
            {
                ImpromptuHelper.ShowPrompt("Password not Updated!");
            }
            else
            {
                ImpromptuHelper.ShowPrompt("Default Password set!");
            }
            ViewState["dtDetails"] = null;
            BindGrid();
            btnCancel_Click(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pEmployee.Visible = true;
            pUpdatepassword.Visible = false;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
}