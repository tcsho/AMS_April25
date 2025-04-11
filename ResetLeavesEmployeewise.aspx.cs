using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class ResetLeavesEmployeewise : System.Web.UI.Page
{
    int UserLevel, UserType;


    BLLResetLeavesEmployeewise objBll = new BLLResetLeavesEmployeewise();
    DALBase objBase = new DALBase();
    DataTable dtHOD = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["EmployeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }

            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());

        }
        catch (Exception)
        {
        }
        if (!IsPostBack)
        {
            //======== Page Access Settings ========================
            DALBase objBase = new DALBase();
            DataRow row = (DataRow)Session["rightsRow"];
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;


            DataTable _dtSettings = objBase.ApplyPageAccessSettingsTable(sRet, Convert.ToInt32(row["User_Type_Id"].ToString()));
            this.Page.Title = _dtSettings.Rows[0]["PageTitle"].ToString();
            //tdFrmHeading.InnerHtml = _dtSettings.Rows[0]["PageCaption"].ToString();
            if (Convert.ToBoolean(_dtSettings.Rows[0]["isAllow"]) == false)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }

            //====== End Page Access settings ======================

            //pan_New.Attributes.CssStyle.Add("display", "none");

            ViewState["SortDirection"] = "ASC";
            ViewState["MissingSortDirection"] = "ASC";
            ViewState["LateSortDirection"] = "ASC";
            ViewState["HalfdayLeavesSortDirection"] = "ASC";
            ViewState["mode"] = "Add";
            //FillParentMenu();
            //FillPage();
            //FillHODList();
            loadMonths();
            loadDepartments();
            FillEmployees();

            //bindgrid();
        }
    }
    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["EmpLeaves"] = null;
            ddlDepartment.SelectedValue = "0";
            ddlEmployeecode.SelectedValue = "0";
            gv_EmpHalfdayLeaves.DataSource = null;
            gv_EmpHalfdayLeaves.DataBind();
            gv_EmpLate.DataSource = null;
            gv_EmpLate.DataBind();
            gv_EmpMissing.DataSource = null;
            gv_EmpMissing.DataBind();
            gv_EmpLeaves.DataSource = null;
            gv_EmpLeaves.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
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
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    #region 'Employee Leaves'
    protected void gv_EmpLeaves_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable oDataSet = (DataTable)ViewState["EmpLeaves"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            BindGridLeaves();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gv_EmpLeaves_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_EmpLeaves.PageIndex = e.NewPageIndex;
            BindGridLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnLeavesResubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)imgbtn.NamingContainer;
            objBll.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objBll.PMonth = ddlMonths.SelectedValue.Trim();
            objBll.EmpLeave_Id = Convert.ToInt32(imgbtn.CommandArgument);
            DateTime dtFrom = Convert.ToDateTime(gvr.Cells[5].Text);
            objBll.LeaveFrom = dtFrom;
            DateTime dtTo = Convert.ToDateTime(gvr.Cells[6].Text);
            objBll.LeaveTo = dtTo;
            objBll.ResetBy = Session["UserName"].ToString();
            objBll.ResetEmployeeLeaves(objBll);
            ViewState["EmpLeaves"] = null;
            BindGridLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void BindGridLeaves()
    {
        try
        {
            //BLLAttendance bllObj = new BLLAttendance();
            gv_EmpLeaves.DataSource = null;
            gv_EmpLeaves.DataBind();
            DataTable _dt = new DataTable();
            if (ViewState["EmpLeaves"] == null)
            {
                //bllObj.PMonthDesc = this.ddlMonths.SelectedValue;
                objBll.EmployeeCode = ddlEmployeecode.SelectedValue;
                //bllObj.Submit2HOD = true;
                objBll.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                objBll.PMonth = ddlMonths.SelectedValue;
                if (Session["RegionID"].ToString() == "")
                {
                    objBll.Region_Id = 0;
                }
                else
                {
                    objBll.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
                }
                if (Session["CenterID"].ToString() == "")
                {
                    objBll.Center_Id = 0;
                }
                else
                {
                    objBll.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
                }
                _dt = objBll.FetchLeavesEmployeewise(objBll);
            }
            else
            {
                _dt = (DataTable)ViewState["EmpLeaves"];
            }
            ViewState["EmpLeaves"] = _dt;
            gv_EmpLeaves.DataSource = _dt;
            gv_EmpLeaves.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }

    #endregion
    #region 'Employee Half Day'
    protected void gv_EmpHalfdayLeaves_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["gv_EmpHalfdayLeaves"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["HalfdayLeavesSortDirection"].ToString();
            if (ViewState["HalfdayLeavesSortDirection"].ToString() == "ASC")
            {
                ViewState["HalfdayLeavesSortDirection"] = "DESC";
            }
            else
            {
                ViewState["HalfdayLeavesSortDirection"] = "ASC";
            }
            BindGridHalfDayLeaves();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnReSubmitHalfDay_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLAttendance bllObj = new BLLAttendance();
            ImageButton imgbtn = (ImageButton)sender;
            bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
            bllObj.AttendanceUpdateEmpReturn(bllObj);
            ViewState["gv_EmpHalfdayLeaves"] = null;
            BindGridHalfDayLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gv_EmpHalfdayLeaves_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_EmpHalfdayLeaves.PageIndex = e.NewPageIndex;

            ViewState["gv_EmpHalfdayLeaves"] = null;
            BindGridHalfDayLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void BindGridHalfDayLeaves()
    {
        try
        {
            gv_EmpHalfdayLeaves.DataSource = null;
            gv_EmpHalfdayLeaves.DataBind();
            BLLAttendance bllObj = new BLLAttendance();
            DataTable dtHalf = new DataTable();
            if (ViewState["gv_EmpHalfdayLeaves"] == null)
            {
                bllObj.PMonthDesc = this.ddlMonths.SelectedValue.Trim();
                bllObj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
                bllObj.Submit2HOD = true;
                dtHalf = bllObj.HalfDaysSelect(bllObj);
            }
            else
            {
                dtHalf = (DataTable)ViewState["gv_EmpHalfdayLeaves"];
            }
            ViewState["gv_EmpHalfdayLeaves"] = dtHalf;
            gv_EmpHalfdayLeaves.DataSource = dtHalf;
            gv_EmpHalfdayLeaves.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    #endregion
    #region 'Employee Late'
    protected void gv_EmpLate_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable oDataSet = (DataTable)ViewState["gv_EmpLate"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["LateSortDirection"].ToString();
            if (ViewState["LateSortDirection"].ToString() == "ASC")
            {
                ViewState["LateSortDirection"] = "DESC";
            }
            else
            {
                ViewState["LateSortDirection"] = "ASC";
            }
            BindGridLate();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gv_EmpLate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_EmpLate.PageIndex = e.NewPageIndex;
            BindGridLate();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnReSubmitLateArrivals_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLAttendance bllObj = new BLLAttendance();
            ImageButton imgbtn = (ImageButton)sender;
            bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
            bllObj.AttendanceUpdateEmpNegReturn(bllObj);
            ViewState["gv_EmpLate"] = null;
            BindGridLate();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void BindGridLate()
    {
        try
        {
            gv_EmpLate.DataSource = null;
            gv_EmpLate.DataBind();
            BLLAttendance bllObj = new BLLAttendance();
            DataTable dtLate = new DataTable();
            if (ViewState["gv_EmpLate"] == null)
            {
                bllObj.PMonthDesc = this.ddlMonths.SelectedValue.Trim();
                bllObj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
                bllObj.NegAttSubmit2HOD = true;
                bllObj.isLock = false;
                dtLate = bllObj.AttendanceSelectNegEmpLate(bllObj);
            }
            else
            {
                dtLate = (DataTable)ViewState["gv_EmpLate"];
            }
            gv_EmpLate.DataSource = dtLate;
            gv_EmpLate.DataBind();
            ViewState["gv_EmpLate"] = dtLate;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    #endregion
    #region'gv_EmpMissing'
    protected void gv_EmpMissing_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["gv_EmpMissing"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["MissingSortDirection"].ToString();
            if (ViewState["MissingSortDirection"].ToString() == "ASC")
            {
                ViewState["MissigSortDirection"] = "DESC";
            }
            else
            {
                ViewState["MissingSortDirection"] = "ASC";
            }
            BindGridMissing();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gv_EmpMissing_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_EmpMissing.PageIndex = e.NewPageIndex;
            BindGridMissing();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnReSubmitMissingInOut_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLAttendance bllObj = new BLLAttendance();
            ImageButton imgbtn = (ImageButton)sender;
            bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
            bllObj.AttendanceUpdateEmpNegReturnMIO(bllObj);
            ViewState["gv_EmpMissing"] = null;
            BindGridMissing();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void BindGridMissing()
    {
        try
        {
            gv_EmpMissing.DataSource = null;
            gv_EmpMissing.DataBind();
            BLLAttendance bllObj = new BLLAttendance();
            DataTable dtMissing = new DataTable();
            if (ViewState["gv_EmpMissing"] == null)
            {
                bllObj.PMonthDesc = this.ddlMonths.SelectedValue.Trim();
                bllObj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
                bllObj.MIOAttSubmit2HOD = true;
                bllObj.isLock = false;
                dtMissing = bllObj.AttendanceSelectNegEmpMIO(bllObj);
            }
            else
            {
                dtMissing = (DataTable)ViewState["gv_EmpMissing"];
            }
            ViewState["gv_EmpMissing"] = dtMissing;
            gv_EmpMissing.DataSource = dtMissing;
            gv_EmpMissing.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    #endregion


    protected void drawMsgBox(string msg)
    {
        try
        {
            ImpromptuHelper.ShowPrompt(msg);

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void FillEmployees()
    {
        try
        {
            BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();
            DataTable dt = new DataTable();


            if (UserLevel == 4)
            {
                obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
            }
            else if (UserLevel == 3)
            {
                obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                obj.Center_id = 0;
            }
            else if (UserLevel == 1 || UserLevel == 2)
            {
                obj.Region_id = 0;
                obj.Center_id = 0;
            }

            dt = obj.EmployeeprofileSelectByRegionCenter(obj);
            objBase.FillDropDown(dt, ddlEmployeecode, "EmployeeCode", "CodeName");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void loadEmployees()
    {
        try
        {
            BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();
            DataTable dt = new DataTable();

            if (UserLevel == 4)
            {
                obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
            }
            else if (UserLevel == 3)
            {
                obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                obj.Center_id = 0;
            }
            else if (UserLevel == 1 || UserLevel == 2)
            {
                obj.Region_id = 0;
                obj.Center_id = 0;
            }

            obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);


            dt = obj.EmployeeprofileSelectByRegionCenterDeptViewonly(obj);
            objBase.FillDropDown(dt, ddlEmployeecode, "employeecode", "CodeName");
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
            BLLAttendance obj = new BLLAttendance();

            DataTable dt = new DataTable();

            obj.PMonthDesc = Session["CurrentMonth"].ToString();

            obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
            obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);
            objBase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                loadEmployees();
            }
            else if (ddlDepartment.SelectedIndex == 0)
            {
                FillEmployees();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void ddlEmployeecode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["EmpLeaves"] = null;
            ViewState["gv_EmpHalfdayLeaves"] = null;
            ViewState["gv_EmpLate"] = null;
            ViewState["gv_EmpMissing"] = null;
            BindGridHalfDayLeaves();
            BindGridLate();
            BindGridLeaves();
            BindGridMissing();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
}

