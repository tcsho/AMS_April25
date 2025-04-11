using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;

public partial class HODs_for_daily_report : System.Web.UI.Page
{
    BLLHODs_for_daily_report objHOD = new BLLHODs_for_daily_report();
    DALBase objBase = new DALBase();
    int UserLevel, UserType;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                load_region_dept();
                BindMainGrid();
                if (ddl_region_dept.SelectedValue == "0")
                {
                    ddl_region_dept_SelectedIndexChanged(this, e);
                }
                ViewState["SortDirection"] = "DESC";
                ViewState["tMood"] = "";
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    public void load_region_dept()
    {
        try 
        {
            BLLVacationTimigs objBll = new BLLVacationTimigs();
            DataTable _dt = new DataTable();

            _dt = objBll.fetchRegions();

            ddl_region_dept.DataTextField = "Region_Name";
            ddl_region_dept.DataValueField = "Region_Id";
            ddl_region_dept.DataSource = _dt;
            ddl_region_dept.DataBind();
            ddl_region_dept.Items.Insert(0, new ListItem("Head Office", "0"));
            ddl_region_dept.SelectedValue = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void loadDepartments()
    {
        try
        {
            BLLEmployeeReportToHOD objBll = new BLLEmployeeReportToHOD();
            DataTable dt = new DataTable();
            //objBll.PMonth = "201609";
            objBll.PMonth = Session["CurrentMonth"].ToString();
            objBll.regionId = Convert.ToInt32(ddl_region_dept.SelectedValue);
            objBll.centerId = Convert.ToInt32(ddl_center_dept.SelectedValue);
            dt = objBll.WebDepartmentSelectByMonthRegionCenter(objBll);
            objBase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void BingGridEmployees()
    {
        try
        {
            gvEmployees.DataSource = null;
            gvEmployees.DataBind();
            BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();
            DataTable dt = new DataTable();
            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());
            if (ViewState["Employees"] == null)
            {
                if (UserLevel == 4)
                {
                    obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                    obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
                }
                else if (UserLevel == 3)
                {
                    obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                    obj.Center_id = Convert.ToInt32(ddl_center_dept.SelectedValue);
                }
                else if (UserLevel == 1 || UserLevel == 2)
                {
                    obj.Region_id = Convert.ToInt32(ddl_region_dept.SelectedValue);
                    obj.Center_id = Convert.ToInt32(ddl_center_dept.SelectedValue);
                }
                obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
                dt = obj.EmployeeprofileSelectByRegionCenterDeptViewonly(obj);
                ViewState["Employees"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Employees"];
                ViewState["Employees"] = dt;
            }

            gvEmployees.DataSource = dt;
            gvEmployees.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void load_center_dept()
    {
        try
        {
            BLLVacationTimigs objBll = new BLLVacationTimigs();
            DataTable _dt = new DataTable();
            objBll.Region_id = Convert.ToInt32(this.ddl_region_dept.SelectedValue);
            _dt = objBll.fetchCenters(objBll);
            ddl_center_dept.DataTextField = "Center_Name";
            ddl_center_dept.DataValueField = "Center_ID";
            ddl_center_dept.DataSource = _dt;
            ddl_center_dept.DataBind();
            if (ddl_region_dept.SelectedValue == "0")
            {
                ddl_center_dept.Items.Insert(0, new ListItem("Head Office", "0"));
            }
            else
            {
                ddl_center_dept.Items.Insert(0, new ListItem("Regional Office", "0"));
            }

            ddl_center_dept.SelectedValue = (Session["CenterID"].ToString() == "") ? "0" : Session["CenterID"].ToString();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btn_AddHOD_Click(object sender, EventArgs e)
    {
        try
        {
            PanelHOD.Visible = true;
            gridMainView.Visible = false;
            ddlDepartment.SelectedValue = "0";
            AddHOD.Visible = false;
            gvEmployees.DataSource = null;
            gvEmployees.DataBind();
            gridView.Visible = true;
            int rowCount = gvEmployees.Rows.Count;
            if (rowCount > 0)
            {
                btnSave.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void ddl_region_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_region_dept.SelectedValue == "0")
            {
                load_center_dept();
                loadDepartments();
            }
            else
            {
                load_center_dept();
                ddl_center_dept_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void ddl_center_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadDepartments();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void ddl_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = true;
            ViewState["Employees"] = null;
            BingGridEmployees();
            gridView.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objHOD.CreatedBy = Convert.ToInt32(Session["User_Id"].ToString());
            foreach (GridViewRow gvr in gvEmployees.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("CheckBoxHOD");
                if (cb.Checked)
                {
                    objHOD.EmployeeCode = gvEmployees.Rows[gvr.RowIndex].Cells[1].Text;
                    objHOD.HODs_for_daily_reportInsert(objHOD);
                }
            }
            ViewState["Data"] = null;
            BindMainGrid();
            Reset();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void BindMainGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            if (ViewState["Data"] != null)
            {
                dt = (DataTable)ViewState["Data"];
            }
            else
            {
                dt = objHOD.HODs_for_daily_reportSelectAll();
                ViewState["Data"] = dt;
            }
            gvMainHODData.DataSource = dt;
            gvMainHODData.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btndel = (ImageButton)sender;
            objHOD.HODs_for_daily_report_id = Convert.ToInt32(btndel.CommandArgument);
            objHOD.HODs_for_daily_reportDelete(objHOD);
            ViewState["Data"] = null;
            BindMainGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvEmployees.PageIndex = e.NewPageIndex;
            BingGridEmployees() ;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gvMainHODData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMainHODData.PageIndex = e.NewPageIndex;
            BindMainGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gvMainHODData_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable oDataSet = (DataTable)ViewState["Data"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            BindMainGrid();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (ViewState["Employees"] != null)
            {
                DataTable oDataSet = (DataTable)ViewState["Employees"];
                oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
                if (ViewState["SortDirection"].ToString() == "ASC")
                {
                    ViewState["SortDirection"] = "DESC";
                }
                else
                {
                    ViewState["SortDirection"] = "ASC";
                }
                BingGridEmployees();
            }
            
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMood"].ToString();

                foreach (GridViewRow gvr in gvEmployees.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBoxHOD");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMood"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMood"] = "check";
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Reset();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void Reset()
    {
        try
        {
            AddHOD.Visible = true;
            gridMainView.Visible = true;
            PanelHOD.Visible = false;
            gridView.Visible = false;
            ddlDepartment.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }

    }
}

 