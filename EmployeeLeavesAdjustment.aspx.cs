using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;
using System.Globalization;

public partial class EmployeeLeavesAdjustment : System.Web.UI.Page
{
    int UserLevel, UserType;
    int Curryear = Convert.ToInt32(DateTime.Now.Year);
    int Prevyear = Convert.ToInt32(DateTime.Now.Year) - 1;

    BLLEmployeeLeavesAdjustment objBll = new BLLEmployeeLeavesAdjustment();
    DALBase objBase = new DALBase();
    DataTable dtHOD = new DataTable();
    BLLEmployeeLeaves bllEmpLeaves = new BLLEmployeeLeaves();
    string selectedEmployeeCode = string.Empty;
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
            employeeLvSubmitionDiv.Visible = false;
            laveBalanceDiv.Visible = false;
            leavetype();
            ResetControls();
        }
    }
    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["EmpLeaves"] = null;
            ViewState["gv_LWP"] = null;
            ViewState["gv_EmpHalfdayLeaves"] = null;
            ViewState["gv_EmpMissing"] = null;
            //ViewState["gv_EmpLate"] = null;
            //ddlDepartment.SelectedValue = "0";
            ddlEmployeecode.SelectedValue = "0";

            gv_EmpHalfdayLeaves.DataSource = null;
            gv_EmpHalfdayLeaves.DataBind();
            //gv_EmpLate.DataSource = null;
            //gv_EmpLate.DataBind();
            gv_EmpMissing.DataSource = null;
            gv_EmpMissing.DataBind();

            gv_EmpLeaves.DataSource = null;
            gv_EmpLeaves.DataBind();

            gv_LWP.DataSource = null;
            gv_LWP.DataBind();

            loadDepartments();
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
            //ddlMonths.SelectedValue = Session["CurrentMonth"].ToString();
            ddlMonths.SelectedValue = "202403    ";
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
    protected void btnLeavesEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLEmployeeLeaveBalance objEmpl = new BLLEmployeeLeaveBalance();
            DataTable dtbal = new DataTable();
            objEmpl.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objEmpl.Year = DateTime.Now.Year.ToString();
            dtbal = objEmpl.EmployeeLeaveBalanceFetch(objEmpl);

            employeeLvSubmitionDiv.Visible = true;
            laveBalanceDiv.Visible = true;
            leavetype();

            ImageButton imgbtn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)imgbtn.NamingContainer;
            objBll.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objBll.PMonth = ddlMonths.SelectedValue.Trim();
            objBll.EmpLeave_Id = Convert.ToInt32(imgbtn.CommandArgument);

            CultureInfo culture = new CultureInfo("en-US");
            var str = gvr.Cells[4].Text.ToString();
            string dateString = str.Substring(str.IndexOf(' ') + 1);
            DateTime dtFrom = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture);

            //DateTime dtFrom = Convert.ToDateTime(gvr.Cells[4].Text);
            objBll.LeaveFrom = dtFrom;

            var strto = gvr.Cells[5].Text.ToString();
            string dateStringTo = strto.Substring(strto.IndexOf(' ') + 1);
            DateTime dtTo = DateTime.ParseExact(dateStringTo, "dd/MM/yyyy", culture);

            //DateTime dtTo = Convert.ToDateTime(gvr.Cells[5].Text);
            objBll.LeaveTo = dtTo;
            objBll.ResetBy = Session["UserName"].ToString();
            //objBll.ResetEmployeeLeaves(objBll);
            //ViewState["EmpLeaves"] = null;
            //BindGridLeaves();

            txtFromDate.Text = dtFrom.Month.ToString() + '/' + dtFrom.Day.ToString() + '/' + dtFrom.Year.ToString();
            txtToDate.Text = dtTo.Month.ToString() + '/' + dtTo.Day.ToString() + '/' + dtTo.Year.ToString();
            empLeaveId.Text = gvr.Cells[0].Text;
            int days = CalculateDays(txtFromDate.Text, txtToDate.Text);
            txtDays.Text = days.ToString();

            typeGrid.Text = "RES";

            ddlLeaveType.SelectedValue = gvr.Cells[6].Text;
            empRemarks.Text = gvr.Cells[7].Text;

            if (dtbal.Rows.Count > 0)
            {
                casualLeaves.Text = dtbal.Rows[0]["balCasual"].ToString();
                annualLeaves.Text = dtbal.Rows[0]["balAnnual"].ToString();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }

    protected void btnLWPEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLEmployeeLeaveBalance objEmpl = new BLLEmployeeLeaveBalance();
            DataTable dtbal = new DataTable();
            objEmpl.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objEmpl.Year = DateTime.Now.Year.ToString();
            dtbal = objEmpl.EmployeeLeaveBalanceFetch(objEmpl);

            employeeLvSubmitionDiv.Visible = true;
            laveBalanceDiv.Visible = true;
            leavetype();

            ImageButton imgbtn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)imgbtn.NamingContainer;
            objBll.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objBll.PMonth = ddlMonths.SelectedValue.Trim();
            objBll.EmpLeave_Id = Convert.ToInt32(imgbtn.CommandArgument);
            CultureInfo culture = new CultureInfo("en-US");
            var str = gvr.Cells[2].Text.ToString();
            string dateString = str.Substring(str.IndexOf(' ') + 1);
            DateTime dtFrom = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture);

            objBll.LeaveFrom = dtFrom;
            DateTime dtTo = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture);
            objBll.LeaveTo = dtTo;
            objBll.ResetBy = Session["UserName"].ToString();
            //objBll.ResetEmployeeLeaves(objBll);
            //ViewState["EmpLeaves"] = null;
            //BindGridLeaves();

            txtFromDate.Text = dtFrom.Month.ToString() + '/' + dtFrom.Day.ToString() + '/' + dtFrom.Year.ToString();
            txtToDate.Text = dtTo.Month.ToString() + '/' + dtTo.Day.ToString() + '/' + dtTo.Year.ToString();
            attId.Text = gvr.Cells[0].Text;
            int days = CalculateDays(txtFromDate.Text, txtToDate.Text);
            txtDays.Text = days.ToString();

            ddlLeaveType.SelectedValue = gvr.Cells[4].Text;
            empRemarks.Text = gvr.Cells[3].Text;

            typeGrid.Text = "LWP";

            if (dtbal.Rows.Count > 0)
            {
                casualLeaves.Text = dtbal.Rows[0]["balCasual"].ToString();
                annualLeaves.Text = dtbal.Rows[0]["balAnnual"].ToString();
            }
            
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

    protected void btnHalfDayLeavesEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLEmployeeLeaveBalance objEmpl = new BLLEmployeeLeaveBalance();
            DataTable dtbal = new DataTable();
            objEmpl.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objEmpl.Year = DateTime.Now.Year.ToString();
            dtbal = objEmpl.EmployeeLeaveBalanceFetch(objEmpl);

            employeeLvSubmitionDiv.Visible = true;
            laveBalanceDiv.Visible = true;
            leavetype();

            ImageButton imgbtn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)imgbtn.NamingContainer;
            objBll.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objBll.PMonth = ddlMonths.SelectedValue.Trim();
            objBll.EmpLeave_Id = Convert.ToInt32(imgbtn.CommandArgument);
            CultureInfo culture = new CultureInfo("en-US");
            var str = gvr.Cells[2].Text.ToString();
            string dateString = str.Substring(str.IndexOf(' ') + 1);
            DateTime dtFrom = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture);

            objBll.LeaveFrom = dtFrom;
            DateTime dtTo = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture);
            objBll.LeaveTo = dtTo;
            objBll.ResetBy = Session["UserName"].ToString();
            //objBll.ResetEmployeeLeaves(objBll);
            //ViewState["EmpLeaves"] = null;
            //BindGridLeaves();

            txtFromDate.Text = dtFrom.Month.ToString() + '/' + dtFrom.Day.ToString() + '/' + dtFrom.Year.ToString();
            txtToDate.Text = dtTo.Month.ToString() + '/' + dtTo.Day.ToString() + '/' + dtTo.Year.ToString();
            attId.Text = gvr.Cells[0].Text;
            int days = CalculateDays(txtFromDate.Text, txtToDate.Text);
            txtDays.Text = days.ToString();

            ddlLeaveType.SelectedValue = gvr.Cells[3].Text;
            empRemarks.Text = gvr.Cells[5].Text;

            typeGrid.Text = "HDAY";

            if (dtbal.Rows.Count > 0)
            {
                casualLeaves.Text = dtbal.Rows[0]["balCasual"].ToString();
                annualLeaves.Text = dtbal.Rows[0]["balAnnual"].ToString();
            }

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
    protected void btnMissingLeavesEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLEmployeeLeaveBalance objEmpl = new BLLEmployeeLeaveBalance();
            DataTable dtbal = new DataTable();
            objEmpl.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objEmpl.Year = DateTime.Now.Year.ToString();
            dtbal = objEmpl.EmployeeLeaveBalanceFetch(objEmpl);

            employeeLvSubmitionDiv.Visible = true;
            laveBalanceDiv.Visible = true;
            leavetype();

            ImageButton imgbtn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)imgbtn.NamingContainer;
            objBll.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objBll.PMonth = ddlMonths.SelectedValue.Trim();
            objBll.EmpLeave_Id = Convert.ToInt32(imgbtn.CommandArgument);

            CultureInfo culture = new CultureInfo("en-US");
            var str = gvr.Cells[2].Text.ToString();
            string dateString = str.Substring(str.IndexOf(' ') + 1); 
            DateTime dtFrom = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture);
            
            objBll.LeaveFrom = dtFrom;
            DateTime dtTo = DateTime.ParseExact(dateString, "dd/MM/yyyy", culture);

            objBll.LeaveTo = dtTo;
            objBll.ResetBy = Session["UserName"].ToString();
            //objBll.ResetEmployeeLeaves(objBll);
            //ViewState["EmpLeaves"] = null;
            //BindGridLeaves();

            txtFromDate.Text = dtFrom.Month.ToString() + '/' + dtFrom.Day.ToString() + '/' + dtFrom.Year.ToString();
            txtToDate.Text = dtTo.Month.ToString() + '/' + dtTo.Day.ToString() + '/' + dtTo.Year.ToString();
            attId.Text = gvr.Cells[0].Text;
            int days = CalculateDays(txtFromDate.Text, txtToDate.Text);
            txtDays.Text = days.ToString();

            ddlLeaveType.SelectedValue = gvr.Cells[7].Text;
            empRemarks.Text = gvr.Cells[6].Text;

            typeGrid.Text = "MIO";

            if (dtbal.Rows.Count > 0)
            {
                casualLeaves.Text = dtbal.Rows[0]["balCasual"].ToString();
                annualLeaves.Text = dtbal.Rows[0]["balAnnual"].ToString();
            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }

    //protected void gv_EmpLate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        gv_EmpLate.PageIndex = e.NewPageIndex;
    //        BindGridLate();
    //    }
    //    catch (Exception ex)
    //    {
    //        Session["error"] = ex.Message;
    //        Response.Redirect("~/ErrorPage.aspx", false);
    //    }
    //}
    //protected void btnEmpLateEdit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        BLLEmployeeLeaveBalance objEmpl = new BLLEmployeeLeaveBalance();
    //        DataTable dtbal = new DataTable();
    //        objEmpl.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
    //        objEmpl.Year = DateTime.Now.Year.ToString();
    //        dtbal = objEmpl.EmployeeLeaveBalanceFetch(objEmpl);

    //        employeeLvSubmitionDiv.Visible = true;
    //        laveBalanceDiv.Visible = true;
    //        leavetype();

    //        ImageButton imgbtn = (ImageButton)sender;
    //        GridViewRow gvr = (GridViewRow)imgbtn.NamingContainer;
    //        objBll.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
    //        objBll.PMonth = ddlMonths.SelectedValue.Trim();
    //        objBll.EmpLeave_Id = Convert.ToInt32(imgbtn.CommandArgument);
    //        DateTime dtFrom = Convert.ToDateTime(gvr.Cells[2].Text);
    //        objBll.LeaveFrom = dtFrom;
    //        DateTime dtTo = Convert.ToDateTime(gvr.Cells[2].Text);
    //        objBll.LeaveTo = dtTo;
    //        objBll.ResetBy = Session["UserName"].ToString();
    //        //objBll.ResetEmployeeLeaves(objBll);
    //        //ViewState["EmpLeaves"] = null;
    //        //BindGridLeaves();

    //        txtFromDate.Text = dtFrom.Month.ToString() + '/' + dtFrom.Day.ToString() + '/' + dtFrom.Year.ToString();
    //        txtToDate.Text = dtTo.Month.ToString() + '/' + dtTo.Day.ToString() + '/' + dtTo.Year.ToString();
    //        attId.Text = gvr.Cells[0].Text;
    //        int days = CalculateDays(txtFromDate.Text, txtToDate.Text);
    //        txtDays.Text = days.ToString();

    //        ddlLeaveType.SelectedValue = gvr.Cells[5].Text == "Late Arrival" ? "0" : gvr.Cells[5].Text;
    //        empRemarks.Text = gvr.Cells[6].Text == "&nbsp;" ? "" : gvr.Cells[6].Text;

    //        typeGrid.Text = "LATE";

    //        if (dtbal.Rows.Count > 0)
    //        {
    //            casualLeaves.Text = dtbal.Rows[0]["balCasual"].ToString();
    //            annualLeaves.Text = dtbal.Rows[0]["balAnnual"].ToString();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Session["error"] = ex.Message;
    //        Response.Redirect("~/ErrorPage.aspx", false);
    //    }
    //}

    //protected void btnReSubmitLateArrivals_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        BLLAttendance bllObj = new BLLAttendance();
    //        ImageButton imgbtn = (ImageButton)sender;
    //        bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
    //        bllObj.AttendanceUpdateEmpNegReturnHR(bllObj);
    //        ViewState["gv_EmpLate"] = null;
    //        BindGridLate();
    //    }
    //    catch (Exception ex)
    //    {
    //        Session["error"] = ex.Message;
    //        Response.Redirect("~/ErrorPage.aspx", false);
    //    }
    //}
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
            bllObj.AttendanceUpdateEmpHDReturnHR(bllObj);
            ViewState["gv_EmpHalfdayLeaves"] = null;
            BindGridHalfDayLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    

    protected void btnReSubmitLWP_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLAttendance bllObj = new BLLAttendance();
            ImageButton imgbtn = (ImageButton)sender;
            bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
            bllObj.AttendanceUpdateEmpLWPReturn(bllObj);
            ViewState["gv_LWP"] = null;
            BindGridLeaveWithoutPay();
            
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
            bllObj.AttendanceUpdateEmpNegReturnMIOHR(bllObj);
            ViewState["gv_EmpMissing"] = null;
            BindGridMissing();
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
                dtHalf = bllObj.HalfDaysSelectHR(bllObj);
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

    //protected void gv_EmpLate_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    try
    //    {
    //        DataTable oDataSet = (DataTable)ViewState["gv_EmpLate"];
    //        oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["LateSortDirection"].ToString();
    //        if (ViewState["LateSortDirection"].ToString() == "ASC")
    //        {
    //            ViewState["LateSortDirection"] = "DESC";
    //        }
    //        else
    //        {
    //            ViewState["LateSortDirection"] = "ASC";
    //        }
    //        BindGridLate();

    //    }
    //    catch (Exception ex)
    //    {
    //        Session["error"] = ex.Message;
    //        Response.Redirect("~/ErrorPage.aspx", false);
    //    }
    //}

    //protected void BindGridLate()
    //{
    //    try
    //    {
    //        gv_EmpLate.DataSource = null;
    //        gv_EmpLate.DataBind();
    //        BLLAttendance bllObj = new BLLAttendance();
    //        DataTable dtLate = new DataTable();
    //        if (ViewState["gv_EmpLate"] == null)
    //        {
    //            bllObj.PMonthDesc = this.ddlMonths.SelectedValue.Trim();
    //            bllObj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
    //            bllObj.NegAttSubmit2HOD = true;
    //            bllObj.isLock = false;
    //            dtLate = bllObj.AttendanceSelectNegEmpLate(bllObj);
    //        }
    //        else
    //        {
    //            dtLate = (DataTable)ViewState["gv_EmpLate"];
    //        }
    //        gv_EmpLate.DataSource = dtLate;
    //        gv_EmpLate.DataBind();
    //        ViewState["gv_EmpLate"] = dtLate;
    //    }
    //    catch (Exception ex)
    //    {
    //        Session["error"] = ex.Message;
    //        Response.Redirect("~/ErrorPage.aspx", false);
    //    }
    //}

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
                dtMissing = bllObj.AttendanceSelectNegEmpMIOHR(bllObj);
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

    protected void BindGridLeaveWithoutPay()
    {
        try
        {
            gv_LWP.DataSource = null;
            gv_LWP.DataBind();
            BLLAttendance bllObj = new BLLAttendance();
            DataTable dtLWP = new DataTable();
            if (ViewState["gv_LWP"] == null)
            {
                bllObj.PMonthDesc = this.ddlMonths.SelectedValue.Trim();
                bllObj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
                bllObj.Submit2HOD = true;
                dtLWP = bllObj.LWPSelect(bllObj);
            }
            else
            {
                dtLWP = (DataTable)ViewState["gv_LWP"];
            }
            ViewState["gv_LWP"] = dtLWP;
            gv_LWP.DataSource = dtLWP;
            gv_LWP.DataBind();
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

            obj.PMonthDesc = ddlMonths.SelectedValue;

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
            ViewState["gv_LWP"] = null;
            ViewState["gv_EmpHalfdayLeaves"] = null;
            ViewState["gv_EmpMissing"] = null;
            //ViewState["gv_EmpLate"] = null;
            //BindGridHalfDayLeaves();
            BindGridLeaves();
            BindGridLeaveWithoutPay();
            BindGridHalfDayLeaves();
            //BindGridLate();
            BindGridMissing();

            ResetControls();
            employeeLvSubmitionDiv.Visible = false;
            laveBalanceDiv.Visible = false;


            //if (ddlEmployeecode.SelectedValue == "0")
            //{
            //    employeeLvSubmitionDiv.Visible = false;
            //}
            //else
            //{
            //    employeeLvSubmitionDiv.Visible = true;
            //    selectedEmployeeCode = ddlEmployeecode.SelectedValue;
            //}

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {

        string _leave = ddlLeaveType.SelectedValue;

        BLLAttendance bllObj = new BLLAttendance();
        DataTable _dt = new DataTable();

        bllObj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
        _dt = bllObj.EmployeeAvailedAndBalanceLeaveForTheYear(bllObj);

     
        BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

        DataTable dtbal = new DataTable();
        obj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
        obj.Year = DateTime.Now.Year.ToString();

        dtbal = obj.EmployeeLeaveBalanceFetch(obj);

        if (_leave == ((int)LeaveTypes.CasualLeaves).ToString())   /*Casual Leaves*/
        {

        }
        else if (_leave == ((int)LeaveTypes.AnnualLeaves).ToString()) /*Anual Leaves*/
        {
            if (Convert.ToInt32(txtDays.Text.ToString()) > Convert.ToInt32(_dt.Rows[0]["BalanceLeaves"].ToString()))
            {
                drawMsgBox("You cannot apply more than 45 leaves in a year'.", 0);
                ddlLeaveType.SelectedValue = "0";
            }

            //if (Session["probrationDate"] != DBNull.Value && Session["probrationDate"] != null)
            if (Session["probrationDate"].ToString().Trim() != "")
            {
                DateTime dt = Convert.ToDateTime(Session["probrationDate"]);

                if (dt.CompareTo(DateTime.Today) > 0)
                {
                    drawMsgBox("Can't Avail Annual Leaves in Probation.", 3);
                    ddlLeaveType.SelectedValue = "0";
                }
            }
            else
            {
                drawMsgBox("Probation end date is not correct", 0);
            }
        }
        else if (_leave == ((int)LeaveTypes.OffDay).ToString()) /*Off Day*/
        {
            drawMsgBox("Reservations is not allowed for this type", 3);
            ddlLeaveType.SelectedValue = "0";
        }
        else if (_leave == ((int)LeaveTypes.Present).ToString()) /*Present*/
        {
            drawMsgBox("Reservations is not allowed for this type", 3);
            ddlLeaveType.SelectedValue = "0";
        }
        else if (_leave == ((int)LeaveTypes.OfficialTourTask).ToString()) /*Offical Tour*/
        {
        }
        else if (_leave == ((int)LeaveTypes.ManualAttendancePunchCard).ToString()) /*Manual Punch Card*/
        {
            drawMsgBox("Reservations is not allowed for this type", 3);
            ddlLeaveType.SelectedValue = "0";
        }
        else if (_leave == ((int)LeaveTypes.Lieuof).ToString()) /*Lieu Off*/
        {
            drawMsgBox("Reservations is not allowed for this type", 3);
            ddlLeaveType.SelectedValue = "0";
        }
        else if (_leave == "0") /*Select*/
        {
            //            drawMsgBox("Reservations is not allowed for this type");
        }
        else if (_leave == ((int)LeaveTypes.LongLeaves).ToString()) /*Long Leave*/
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "confirm('Long Leaves will be submitted for approval to Higher Management after approval of HOD! If you dont want to proceed then change the leave type.');", true);
            //drawMsgBox("Long Leaves will be entered after approval of HOD and Higher Management!", 3);
        }
        else if (_leave == ((int)LeaveTypes.SpecialLeaves).ToString()) /*Special Leave*/
        {
            //drawMsgBox("Special Leaves will be entered after approval of HOD and Higher Management!",3);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "confirm('Special Leaves will be submitted for approval to Higher Management after approval of HOD! If you dont want to proceed then change the leave type.');", true);
        }
        else if (_leave == ((int)LeaveTypes.LeaveWithoutPay).ToString()) /*Leave Without Pay*/
        {
        }
        else if (_leave == ((int)LeaveTypes.FurloughLeave).ToString())
        {
            int LeaveBal = Convert.ToInt32(dtbal.Rows[0]["balCasual"].ToString()) + Convert.ToInt32(dtbal.Rows[0]["balAnnual"].ToString());

            if (LeaveBal <= 0)
            {
                drawMsgBox("Can't Avail Furlough Leaves due to insufficent leave balance.", 3);
                ddlLeaveType.SelectedValue = "0";
            }
        }
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {

        DataValidations();

    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DataValidations();
    }
    private void DataValidations()
    {
        int days = CalculateDays(txtFromDate.Text, txtToDate.Text);
        if (days < 0)
        {
            drawMsgBox("Invalid Date Range! 'From date' can not be greater than 'To date'.", 2);
            txtDays.Text = "0";
        }
        else
        {
            txtDays.Text = days.ToString();
        }
    }
    private int CalculateDays(string _fromDate, string _toDate)
    {
        int _ret = 0;
        if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 2)
        {

            DateTime dF = DateTime.ParseExact(_fromDate, "M/d/yyyy", null);

            DateTime dT = DateTime.ParseExact(_toDate, "M/d/yyyy", null);

            TimeSpan span = dT.Subtract(dF);
            if (span.Days >= 0)
            {
                _ret = span.Days + 1;
            }
            else
            {
                _ret = span.Days;
            }
        }
        return _ret;

    }

    protected void drawMsgBox(string msg, int errType)
    {


        ImpromptuHelper.ShowPrompt(msg);

    }

    protected void leavetype()
    {
        BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
        objBLL.Status_id = 1;
        objBLL.Used_For = "HR";
        DataTable objDt = new DataTable();

        objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);

        if (objDt.Rows.Count > 0)
        {
            ddlLeaveType.DataSource = objDt;
            ddlLeaveType.DataValueField = "LeaveType_Id";
            ddlLeaveType.DataTextField = "LeaveType";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BLLSendEmail bllemail = new BLLSendEmail();
        BLLPeriod obj = new BLLPeriod();
        BLLAttendance objAttendance = new BLLAttendance();
        obj.PMonth = ddlMonths.SelectedValue;
        DataTable dtPeriod = obj.PeriodFetchMonthRangeDates(obj);

        DateTime dtMinDate = Convert.ToDateTime(dtPeriod.Rows[0]["mindate"].ToString());
        try
        {

            BLLEmployeeLeaveBalance objEmpl = new BLLEmployeeLeaveBalance();

            DataTable dtbal = new DataTable();
            objEmpl.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            objEmpl.Year = DateTime.Now.Year.ToString();

            dtbal = objEmpl.EmployeeLeaveBalanceFetch(objEmpl);


            //Check last year leaves
            DataTable _dt = new DataTable();
            BLLAttendance bllObjAtt = new BLLAttendance();
            bllObjAtt.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
            _dt = bllObjAtt.EmployeeAvailedAndBalanceLeaveForTheYear(bllObjAtt);

            //availedLeaves.Text = _dt.Rows[0]["Availed"].ToString();
            //balanceLeaves.Text = _dt.Rows[0]["BalanceLeaves"].ToString();

            DateTime datecheck = DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null);
            //Convert.ToDateTime(txtFromDate.Text);
            objAttendance.InsertDate = datecheck.Date;
            if (!String.IsNullOrEmpty(Session["CenterID"].ToString()))
                objAttendance.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
            else
                objAttendance.Center_Id = 0;
            DataTable dtbl = objAttendance.ERP_Final_Process_HistorySelectByCenter(objAttendance);
            if (dtbl.Rows.Count > 0)
            {
                //ImpromptuHelper.ShowPrompt("ERP Process has been run for the month for which you are applying Leaves!");
                //btnReset_Click(this, EventArgs.Empty);
                //return;
            }
            string mode = Convert.ToString(ViewState["mode"]);
            int id = 0;
            bool isok = true;
            string _displymsg = "";
            string _leave = ddlLeaveType.SelectedValue;

            //DateTime _StrDay = Convert.ToDateTime(txtFromDate.Text.Trim().Replace("'", ""));
            //string Monday = _StrDay.ToString("dddd");

            //DateTime _EndDay = Convert.ToDateTime(txtToDate.Text.Trim().Replace("'", ""));
            //string Saturday = _EndDay.ToString("dddd");

            //bool isEx = Convert.ToBoolean(Session["isExe"].ToString());

            //if (Monday == "Monday" && isEx == false && _leave != ((int)LeaveTypes.OfficialTourTask).ToString()) //if (Monday == "Monday")// && isEx==false)
            //{

            //    isok = false;
            //}
            //else if (Saturday == "Saturday" && isEx == false && _leave != ((int)LeaveTypes.OfficialTourTask).ToString()) //else if (Saturday == "Saturday")// && isEx == false)
            //{

            //    isok = false;
            //}
            //else
            //{
            //    isok = true;
            //}

            int _HOD;
            BLLEmplyeeReportTo BllEmpR = new BLLEmplyeeReportTo();
            BllEmpR.ReportTo = (ddlEmployeecode.SelectedValue.Trim());

            DataTable dt = BllEmpR.EmplyeeReportToHOD(BllEmpR);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    _HOD = Convert.ToInt32(dr["ReportTo"].ToString());
                    if (_HOD == 17101)
                    {
                        isok = true;
                        break;
                    }
                }
            }


            if (isok == true)
            {


                if (txtFromDate.Text.Trim() == "")
                {
                    isok = false;
                    _displymsg = "Start Date is empty. !";
                }
                else if (txtToDate.Text.Trim() == "")
                {
                    isok = false;
                    _displymsg = "End Date is empty. !";
                }

                if (isok)
                {
                    if (_leave == ((int)LeaveTypes.CasualLeaves).ToString())   /*Casual Leaves*/
                    {
                        //check balance and reservations
                        isok = true;
                        DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null);
                        if (checkLastYearsCasualBalance(((int)LeaveTypes.CasualLeaves)) == false)
                        {
                            isok = false;
                            _displymsg = ("You have no Annual balance till '31st December " + Prevyear + "'. you can submit Leave Without pay.");
                        }
                        if (Convert.ToInt32(txtDays.Text) > Convert.ToSingle(dtbal.Rows[0]["balCasual"].ToString()))
                        {
                            isok = false;
                            _displymsg = ("Can't exceed from current Casual (" + dtbal.Rows[0]["balCasual"].ToString() + ") Leave balance!.");
                        }
                        if (dtFrom < dtMinDate)
                        {
                            isok = false;
                            _displymsg = ("Can't submit Casual leaves in last month.");

                        }

                    }
                    else if (_leave == ((int)LeaveTypes.AnnualLeaves).ToString()) /*Anual Leaves*/
                    {

                        DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null);
                        // Convert.ToDateTime(txtFromDate.Text);
                        DateTime dtTo = DateTime.ParseExact(txtToDate.Text, "M/d/yyyy", null);
                        // Convert.ToDateTime(txtToDate.Text);


                        isok = true;

                        if (Convert.ToInt32(txtDays.Text) < 3)
                        {
                            isok = false;
                            _displymsg = ("Atleast 3 anual leaves are allowed.");
                        }
                        else if (dtFrom < dtMinDate)
                        {
                            isok = false;
                            _displymsg = ("Can't submit Annual leaves in last month.");

                        }
                        else if (Convert.ToInt32(txtDays.Text) > Convert.ToInt32(dtbal.Rows[0]["balAnnual"].ToString()))
                        {
                            isok = false;
                            _displymsg = ("Can't exceed from current Annual (" + dtbal.Rows[0]["balAnnual"].ToString() + ") Leave balance!.");

                        }
                        if (checkLastYearsAnnualBalance(62, DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null), DateTime.ParseExact(txtToDate.Text, "M/d/yyyy", null)) == false)
                        {
                            isok = false;
                            _displymsg = ("You have no Annual balance till '31st December " + Prevyear + "'. you can submit Leave Without pay.");
                        }
                        //
                        if (Convert.ToInt32(txtDays.Text.ToString()) > Convert.ToInt32(_dt.Rows[0]["BalanceLeaves"].ToString()))
                        {
                            isok = false;
                            _displymsg = ("You cannot apply more than 45 leaves in a year'.");
                        }




                    }
                    else if (_leave == ((int)LeaveTypes.MaternityLeaves).ToString())
                    {
                        isok = true;

                        bllEmpLeaves.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
                        DataTable dtb_mat = bllEmpLeaves.Select_MaternityLeavesEmp(bllEmpLeaves);
                        DataTable dtb_mateligible = bllEmpLeaves.SelectMaternityLeavesEMPEligible(bllEmpLeaves);

                        if (dtb_mateligible.Rows.Count > 0)
                        {
                            if (Convert.ToInt16(dtb_mateligible.Rows[0]["isAllowed"].ToString()) <= 12)
                            {
                                isok = false;
                                _displymsg = "You will be entitled to maternity leaves after 12 months of service.";
                            }
                        }

                        if (Convert.ToString(Session["Gender"]).ToUpper().Trim() == "M")
                        {
                            isok = false;
                            _displymsg = "Only female employees can avail Maternity leaves.";
                        }
                        else if (Convert.ToInt32(txtDays.Text) > 35)
                        {
                            isok = false;
                            _displymsg = "Maximum of '35' maternity leaves can be availed.";
                        }
                        else if (dtb_mat.Rows.Count > 1)
                        {
                            isok = false;
                            _displymsg = "Maternity leaves can't be availed more then twice in service.";
                        }
                        else if (dtb_mat.Rows.Count > 0)
                        {
                            DateTime dt_last_mat = Convert.ToDateTime(dtb_mat.Rows[dtb_mat.Rows.Count - 1][1]);

                            DateTime dt_now = Convert.ToDateTime(txtFromDate.Text.Trim().Replace("'", "")); //DateTime.Now.AddYears(-3);
                            dt_now = dt_now.AddYears(-3);


                            if (dt_now.CompareTo(dt_last_mat) < 0)
                            {
                                isok = false;
                                _displymsg = "Maternity leaves can only be availed once in 3 years.";
                            }
                        }
                    }
                    else if (_leave == ((int)LeaveTypes.OffDay).ToString() || _leave == ((int)LeaveTypes.Present).ToString() || _leave == ((int)LeaveTypes.ManualAttendancePunchCard).ToString() || _leave == ((int)LeaveTypes.Lieuof).ToString()) /*Off Day - Present- Manual punch- lieu off*/
                    {
                        isok = false;
                        _displymsg = ("This Reservation Type is not Allowed.");

                    }
                    else if (_leave == ((int)LeaveTypes.OfficialTourTask).ToString() || _leave == ((int)LeaveTypes.SpecialLeaves).ToString() || _leave == ((int)LeaveTypes.LeaveWithoutPay).ToString()) /*Offical Tour - Special Leave - Leave withoutpay*/
                    {
                        isok = true;
                    }
                    else if (_leave == "0") /*Select*/
                    {
                        _displymsg = ("Must Select a Reservation type");
                        isok = false;
                    }
                    else if (_leave == ((int)LeaveTypes.FurloughLeave).ToString())
                    {

                        DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null);

                        DateTime dtTo = DateTime.ParseExact(txtToDate.Text, "M/d/yyyy", null);


                        isok = true;
                        float Flbalance = 0;
                        Flbalance = CheckFurloughLeaveBalance();

                        if (dtFrom < dtMinDate)
                        {
                            isok = false;
                            _displymsg = ("Can't submit Furlough leaves in last month.");

                        }
                        else if (Convert.ToInt32(txtDays.Text) > (Flbalance))
                        {
                            isok = false;
                            _displymsg = ("Can't exceed from current Furlough (Annual + Casual) Leave balance!.");

                        }

                    }
                }

                if (isok == true)
                {
                    BLLAttendance bllAttendanceObj = new BLLAttendance();

                    bllAttendanceObj.Att_Id = !string.IsNullOrEmpty(attId.Text) ? Convert.ToInt32(attId.Text) : 0;
                    bllAttendanceObj.EmpLvType_Id = Convert.ToInt32(ddlLeaveType.SelectedValue);
                    bllAttendanceObj.EmpLvReason = txtReason.Text;
                    bllAttendanceObj.ModifyBy = ddlEmployeecode.SelectedValue.Trim();
                    bllAttendanceObj.ModifyDate = DateTime.Now;
                    bllAttendanceObj.EmpLvSubDate = DateTime.Now;
                    bllAttendanceObj.Submit2HOD = true;
                    bllAttendanceObj.AttendanceTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);

                    #region 'Common Data'

                    bllEmpLeaves.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
                    bllEmpLeaves.LeaveType_Id = Convert.ToInt32(ddlLeaveType.SelectedValue);
                    bllEmpLeaves.LeaveDays = Convert.ToInt32(txtDays.Text);
                    bllEmpLeaves.LeaveFrom = (txtFromDate.Text);
                    bllEmpLeaves.LeaveTo = (txtToDate.Text);
                    bllEmpLeaves.LeaveReason = txtReason.Text;

                    bllEmpLeaves.PMonth = ddlMonths.SelectedValue;

                    bllEmpLeaves.EmpLeave_Id = !string.IsNullOrEmpty(empLeaveId.Text) ? Convert.ToInt32(empLeaveId.Text) : 0;
                    bllEmpLeaves.Att_Id = !string.IsNullOrEmpty(attId.Text) ? Convert.ToInt32(attId.Text) : 0;

                    #endregion

                    int nAlreadyIn = 0;
                    if (mode != "Edit")
                    {
                        //int existCount = IsExist();
                        int existCount = 0;
                        if (existCount == 0)
                        {


                            #region 'Reservation Record Add'
                            bllEmpLeaves.ModifiedBy = Convert.ToInt32(ddlEmployeecode.SelectedValue.Trim());
                            bllEmpLeaves.ModifiedOn = DateTime.Now;

                            if (Convert.ToInt32(txtDays.Text) > 0)
                            {
                                nAlreadyIn = 0;

                                if (typeGrid.Text == "RES")
                                {
                                    //nAlreadyIn = bllEmpLeaves.EmployeeLeavesModifyHR(bllEmpLeaves);
                                }
                                else if (typeGrid.Text == "LWP")
                                {
                                    //nAlreadyIn = bllEmpLeaves.EmployeeLWPModifyHR(bllEmpLeaves);
                                }
                                else if (typeGrid.Text == "HDAY")
                                {
                                    nAlreadyIn = bllAttendanceObj.AttendanceUpdateEmpHalfDaysHR(bllAttendanceObj);
                                }
                                else if (typeGrid.Text == "LATE")
                                {
                                    nAlreadyIn = bllAttendanceObj.AttendanceUpdateEmpLateHR(bllAttendanceObj);
                                }
                                else if (typeGrid.Text == "MIO")
                                {
                                    nAlreadyIn = bllAttendanceObj.AttendanceUpdateEmpMIOHR(bllAttendanceObj);
                                }

                                //if (bllEmpLeaves.Att_Id > 0)
                                //{
                                //    //
                                //    nAlreadyIn = bllEmpLeaves.EmployeeLWPModifyHR(bllEmpLeaves);

                                //}
                                //else if(bllEmpLeaves.EmpLeave_Id > 0)
                                //{
                                //    nAlreadyIn = bllEmpLeaves.EmployeeLeavesModifyHR(bllEmpLeaves);

                                //}

                                if (nAlreadyIn > 0)
                                {
                                    //send email

                                    ViewState["EmpLeaves"] = null;
                                    ViewState["gv_LWP"] = null;
                                    ViewState["gv_EmpHalfdayLeaves"] = null;
                                    ViewState["gv_EmpMissing"] = null;
                                    //ViewState["gv_EmpLate"] = null;

                                    BindGridLeaves();
                                    BindGridLeaveWithoutPay();
                                    BindGridHalfDayLeaves();
                                    //BindGridLate();
                                    BindGridMissing();
                                    employeeLvSubmitionDiv.Visible = false;
                                    laveBalanceDiv.Visible = false;

                                    //bindgridLeaves();

                                    drawMsgBox("Data updated successfully.", 1);

                                    DataTable _dtemp = new DataTable();
                                    _dtemp = (DataTable)Session["EmailTable"];
                                    if (_dtemp.Rows.Count > 0)
                                    {

                                        foreach (DataRow var in _dtemp.Rows)
                                        {
                                            //string mailMsg = "Dear " + var["HODName"].ToString() + ", <br><br> Employee # : " + selectedEmployeeCode + ".<br> Name :" + Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString() + "<br> Has submitted leaves from Employee Reservation for HOD approval. You can find data in HOD Reservation.";
                                            //mailMsg = mailMsg + "<br> LeaveFrom :" + txtFromDate.Text + ".<br> LeaveTo :" + txtToDate.Text + "<br> No. of Days:" + txtDays.Text + "<br> Reason:" + txtReason.Text;
                                            //bllemail.SendEmail(var["HODEmail"].ToString(), "Attendance [Leave(s) Approval]", mailMsg);
                                        }

                                    }
                                    ResetControls();
                                }
                                else if (nAlreadyIn == 1)
                                {
                                    drawMsgBox("Data already exist.", 3);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            ViewState["EmpLeaves"] = null;
                            ViewState["gv_LWP"] = null;
                            ViewState["gv_EmpHalfdayLeaves"] = null;
                            ViewState["gv_EmpMissing"] = null;
                            //ViewState["gv_EmpLate"] = null;

                            BindGridLeaves();
                            BindGridLeaveWithoutPay();
                            BindGridHalfDayLeaves();
                            //BindGridLate();
                            BindGridMissing();

                            drawMsgBox("Invalid Date Range! There are already some leaves reserved for the selected date range.", 2);
                        }
                    }
                    else
                    {
                        #region 'Update'
                        id = Convert.ToInt32(ViewState["EditID"]);
                        bllEmpLeaves.EmpLeave_Id = Int32.Parse(ViewState["EditID"].ToString());

                        bllEmpLeaves.ModifiedBy = Convert.ToInt32(ddlEmployeecode.SelectedValue.Trim());
                        bllEmpLeaves.ModifiedOn = DateTime.Now;


                        //nAlreadyIn = bllEmpLeaves.EmployeeLeavesUpdateEMP(bllEmpLeaves);
                        nAlreadyIn = 0;
                        if (nAlreadyIn == 0)
                        {
                            drawMsgBox("Data modified successfully.", 1);

                            ViewState["EmpLeaves"] = null;
                            ViewState["gv_LWP"] = null;
                            ViewState["gv_EmpHalfdayLeaves"] = null;
                            ViewState["gv_EmpMissing"] = null;
                            //ViewState["gv_EmpLate"] = null;

                            BindGridLeaves();
                            BindGridLeaveWithoutPay();
                            BindGridHalfDayLeaves();
                            //BindGridLate();
                            BindGridMissing();
                        }
                        else if (nAlreadyIn == 1)
                        {
                            drawMsgBox("Data already exist.", 3);
                        }
                        #endregion
                    }


                    ///*Refresh Page*/
                    //bindgridLateArrivals();
                    //bindgridLeaves();
                    //bindgridMIO();
                    //bindgridReservation();

                }
                else
                {
                    drawMsgBox(_displymsg, 3);
                }
                lvdata.Visible = false;
                lvbtn.Visible = false;
            }
            else
            {
                drawMsgBox("Please Consider Sunday attatchment while submitting annual leaves", 3);

            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    private void ResetControls()
    {

        txtDays.Text = "0";
        txtReason.Text = "";
        txtToDate.Text = "";
        empRemarks.Text = "";
        txtReason.Text = "";

        empLeaveId.Text = "";
        attId.Text = "";

        DateTime d = DateTime.Now;
        txtFromDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();
        //   txtToDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();

        ddlLeaveType.SelectedValue = "0";
employeeLvSubmitionDiv.Visible = false;
        laveBalanceDiv.Visible = false;
    }

    private bool checkLastYearsAnnualBalance(int leaveTypeId, DateTime dtFrom, DateTime dtTo)
    {
        BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

        DataTable dt = new DataTable();
        obj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
        obj.Year = Prevyear.ToString();

        dt = obj.EmployeeLeaveBalanceFetch_LastYears(obj);

        if (dt.Rows.Count > 0)
        {
            if (leaveTypeId == 62)
            {


                int count = 0;

                for (DateTime day = dtFrom.Date; day.Date <= dtTo.Date; day = day.AddDays(1))
                {
                    if (day >= new DateTime(Prevyear, 12, 26) && day <= new DateTime(Prevyear, 12, 31))
                    {
                        count++;
                    }
                }

                if (Convert.ToDouble(dt.Rows[0]["TAnnulaLeave"]) < count && count != 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private float CheckFurloughLeaveBalance()
    {
        float Flbalance;
        BLLEmployeeLeaveBalance leaveBalance = new BLLEmployeeLeaveBalance();

        leaveBalance.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
        leaveBalance.PMonth = Session["CurrentMonth"].ToString();

        DataTable dtFl = leaveBalance.EmployeeLeaveBalanceFurlough(leaveBalance);


        if (dtFl.Rows.Count > 0)
        {
            Flbalance = float.Parse(dtFl.Rows[0][0].ToString());
        }
        else
        {
            Flbalance = 0;
        }

        return Flbalance;
    }

    private bool checkLastYearsCasualBalance(int leaveTypeId)
    {
        BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

        DataTable dt = new DataTable();
        obj.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
        obj.Year = Prevyear.ToString();

        dt = obj.EmployeeLeaveBalanceFetch_LastYears(obj);

        if (dt.Rows.Count > 0)
        {
            DropDownList ddlLeaveInner = null;
            CheckBox cb = null;

            if (leaveTypeId == ((int)LeaveTypes.CasualLeaves))
            {
                if (!String.IsNullOrEmpty(txtDays.Text) && !String.IsNullOrEmpty(txtFromDate.Text) && !String.IsNullOrEmpty(txtToDate.Text))
                {
                    if ((DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null) <= new DateTime(Prevyear, 12, 31)))
                    {
                        if (Convert.ToDouble(dt.Rows[0]["TCasualLeave"].ToString()) <= Convert.ToDouble(txtDays.Text))
                        {
                            return false;
                        }
                    }
                }
                int count = 0;

                
                string i = dt.Rows[0]["TCasualLeave"].ToString();
                if (Convert.ToDouble(dt.Rows[0]["TCasualLeave"]) < count && count != 0)
                {
                    //drawMsgBox("Cann't exceed Casual balance (i.e. " + dt.Rows[0]["TCasualLeave"] + ") between dates '26th December 2014' and '31st December 2014'. you can submit Leave Without pay.", 0);
                    drawMsgBox("you have no Casual balance till '31st December " + Prevyear.ToString() + "'. you can submit Leave Without pay.", 0);
                    return false;
                }
            }
            else if (leaveTypeId == ((int)LeaveTypes.HalfCasualLeave))
            {

                float count = 0;


                float hlf = (count
                    / 2);

                if (Convert.ToDouble(dt.Rows[0]["TCasualLeave"]) < hlf && hlf != 0)
                {
                    drawMsgBox("you have no Casual balance till '31st December " + Prevyear.ToString() + "'. you can submit Half Leave Without pay.", 0);
                    return false;
                }
            }
        }

        return true;
    }

    private int IsExist()
    {
        int t = 0;

        bllEmpLeaves.EmployeeCode = ddlEmployeecode.SelectedValue.Trim();
        bllEmpLeaves.LeaveFrom = (txtFromDate.Text);
        bllEmpLeaves.LeaveTo = (txtToDate.Text);


        DataTable dt = bllEmpLeaves.EmployeeLeavesFetchRangeExist(bllEmpLeaves);
        if (dt.Rows.Count > 0)
        {
            t = Convert.ToInt32(dt.Rows[0]["counter"].ToString());
        }

        return t;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls();
        //bindgridReservation();
        //lvdata.Visible = false;
        //lvbtn.Visible = false;

        BindGridLeaveWithoutPay();
        BindGridLeaves();
        BindGridHalfDayLeaves();
        //BindGridLate();
        BindGridMissing();

        employeeLvSubmitionDiv.Visible = false;
        laveBalanceDiv.Visible = false;

    }

}

