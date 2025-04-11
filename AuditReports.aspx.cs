using System;
using System.Data;

using ADG.JQueryExtenders.Impromptu;

public partial class AuditReports : System.Web.UI.Page
{
    DALBase objbase = new DALBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["EmployeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

        if (Session["EmployeeCode"] != null)
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;

            int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

            //int _result = objbase.ApplicationSettings(sRet, _part_Id);


            //if (_result == 1)
            //{
            if (!IsPostBack)
            {
                try
                {
                    loadMonths();
                    loadDepartments();

                    //string queryStr = Request.QueryString["r"];

                    rbLstRpt.SelectedValue = "0";
                    //if (queryStr == "N4548")
                    //{
                    //    rbLstRpt.SelectedValue = "0";
                    //    ViewState["rptmode"] = "?r=N4548";
                    //}
                    //else if (queryStr == "P8454")
                    //{
                    //    rbLstRpt.SelectedValue = "1";
                    //    ViewState["rptmode"] = "?r=P8454";
                    //}
                    //else if (queryStr == "N4845")
                    //{
                    //    rbLstRpt.SelectedValue = "2";
                    //    ViewState["rptmode"] = "?r=N4845";
                    //}
                    //else if (queryStr == "N5484")
                    //{
                    //    rbLstRpt.SelectedValue = "3";
                    //    ViewState["rptmode"] = "?r=N5484";
                    //}
                    //else if (queryStr == "T4548")
                    //{
                    //    rbLstRpt.SelectedValue = "4";
                    //    ViewState["rptmode"] = "?r=T4548";
                    //}
                }
                catch (Exception ex)
                {
                    Session["error"] = ex.Message;
                    Response.Redirect("ErrorPage.aspx", false);
                }
            }
            //}
            //else
            //{
            //    Session["error"] = "You Are Not Authorized To Access This Page";
            //    Response.Redirect("ErrorPage.aspx", false);
            //}
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        string repStr = "";

        Session["reppath"] = "Reports\\rptAttendance4Audit.rpt";
        Session["rep"] = "rptAttendance4Audit.rpt";

        if (rbLstRpt.SelectedValue == "0")
        {
            #region 'Attendance Report'

            if (Session["LoginFrom"].ToString() == "H")
            {
                repStr = "{vw_AttendanceRpt4Audit.RegionId}=0 and {vw_AttendanceRpt4Audit.CenterId}=0";
            }

            if (rbMonth.Checked)
            {
                repStr = repStr + " AND {vw_AttendanceRpt4Audit.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            }
            repStr = repStr + " and {vw_AttendanceRpt4Audit.REDInt}>0 and {vw_AttendanceRpt4Audit.MonthRedCounter}<=2";
            Session["RptTitle"] = "Report with 2 or lesser Reds";

            #endregion
        }
        else if (rbLstRpt.SelectedValue == "1")
        {
            #region 'Attendance Report'

            if (Session["LoginFrom"].ToString() == "H")
            {
                repStr = "{vw_AttendanceRpt4Audit.RegionId}=0 and {vw_AttendanceRpt4Audit.CenterId}=0";
            }

            if (rbMonth.Checked)
            {
                repStr = repStr + " AND {vw_AttendanceRpt4Audit.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            }
            repStr = repStr + " and {vw_AttendanceRpt4Audit.REDInt}>0 and {vw_AttendanceRpt4Audit.MonthRedCounter}>=3";
            Session["RptTitle"] = "Report 3 or more Reds";
            #endregion
        }
        else if (rbLstRpt.SelectedValue == "2")
        {
            #region 'Attendance Report'

            if (Session["LoginFrom"].ToString() == "H")
            {
                repStr = "{vw_AttendanceRpt4Audit.RegionId}=0 and {vw_AttendanceRpt4Audit.CenterId}=0";
            }

            if (rbMonth.Checked)
            {
                repStr = repStr + " AND {vw_AttendanceRpt4Audit.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            }

            repStr = repStr + " and {vw_AttendanceRpt4Audit.MissingInt}>0 and {vw_AttendanceRpt4Audit.Apv}='Y'";
            Session["RptTitle"] = "Approved Missing Entries";
            #endregion
        }
        else if (rbLstRpt.SelectedValue == "3")
        {
            #region 'Attendance Report'

            if (Session["LoginFrom"].ToString() == "H")
            {
                repStr = "{vw_AttendanceRpt4Audit.RegionId}=0 and {vw_AttendanceRpt4Audit.CenterId}=0";
            }

            if (rbMonth.Checked)
            {
                repStr = repStr + " AND {vw_AttendanceRpt4Audit.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            }

            repStr = repStr + " and {vw_AttendanceRpt4Audit.MissingInt}>0 and {vw_AttendanceRpt4Audit.Apv}='N'";
            Session["RptTitle"] = "Un-Approved Missing Entries";
            #endregion
        }
        else if (rbLstRpt.SelectedValue == "4")
        {
            #region 'Attendance Report'

            if (Session["LoginFrom"].ToString() == "H")
            {
                repStr = "{vw_AttendanceRpt4Audit.RegionId}=0 and {vw_AttendanceRpt4Audit.CenterId}=0";
            }

            if (rbMonth.Checked)
            {
                repStr = repStr + " AND {vw_AttendanceRpt4Audit.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            }

            repStr = repStr + " and {vw_AttendanceRpt4Audit.after9}>0 and {vw_AttendanceRpt4Audit.Apv}='Y'";
            Session["RptTitle"] = "Approved absences aftr 9 o clck";
            #endregion
        }
        else if (rbLstRpt.SelectedValue == "5")
        {
            #region 'Attendance Report'

            if (Session["LoginFrom"].ToString() == "H")
            {
                repStr = "{vw_AttendanceRpt4Audit.RegionId}=0 and {vw_AttendanceRpt4Audit.CenterId}=0";
            }

            if (rbMonth.Checked)
            {
                repStr = repStr + " AND {vw_AttendanceRpt4Audit.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            }

            repStr = repStr + " and {vw_AttendanceRpt4Audit.after9}>0 and {vw_AttendanceRpt4Audit.Apv}='N'";
            Session["RptTitle"] = "Un-Approved absences aftr 9";
            #endregion
        }

        Session["CriteriaRpt"] = repStr;
        Session["LastPage"] = "~/AuditReports.aspx"; //+ ViewState["rptmode"].ToString();
        Response.Redirect("~/rptAllReports.aspx");
    }

    protected void loadEmployees()
    {

        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

        DataTable dt = new DataTable();

        obj.ReportTo = Session["EmployeeCode"].ToString().Trim();
        obj.UserType_id = Convert.ToInt32(Session["UserType"].ToString());

        obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
        obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
        obj.Status_id = 2;
        obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);

        dt = obj.EmplyeeReportToFetch(obj);
        if (dt.Rows.Count > 0)
        {
            ddlEmployeecode.DataTextField = "Descr";
            ddlEmployeecode.DataValueField = "code";
            ddlEmployeecode.DataSource = dt;
            ddlEmployeecode.DataBind();
        }
    }

    protected void loadDepartments()
    {


        BLLAttendance obj = new BLLAttendance();

        DataTable dt = new DataTable();

        obj.PMonthDesc = ddlMonths.SelectedValue;
        obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
        obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
        dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);
        objbase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");
    }

    protected void loadMonths()
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


    protected string loadEmployeesReportTo()
    {

        string str = "";
        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

        DataTable dt = new DataTable();

        obj.ReportTo = Session["EmployeeCode"].ToString();
        dt = obj.EmplyeeReportToFetchList(obj);
        if (dt.Rows.Count > 0)
        {
            str = dt.Rows[0]["Codes"].ToString();
        }
        return str;

    }


    //protected void chkemp_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkemp.Checked)
    //    {
    //        trsingleemp.Visible = true;
    //        if (Session["UserType"] != null)
    //        {
    //            int _part_Id = Convert.ToInt32(Session["UserType"].ToString());
    //            if (_part_Id == 19 || _part_Id == 20)
    //            {

    //                TrDept.Visible = true;
    //                loadDepartments();
    //            }
    //            else
    //            {
    //                TrDept.Visible = false;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        trsingleemp.Visible = false;
    //        TrDept.Visible = false;
    //    }
    //}
    protected void rbMonth_CheckedChanged(object sender, EventArgs e)
    {
        if (rbMonth.Checked)
        {
            trFrmDate.Visible = false;
            trMonth.Visible = true;
            trToDate.Visible = false;
        }
    }
    protected void rbRange_CheckedChanged(object sender, EventArgs e)
    {
        if (rbRange.Checked)
        {
            trFrmDate.Visible = true;
            trMonth.Visible = false;
            trToDate.Visible = true;

            DateTime d = DateTime.Now;

            txtFrmDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();
            txtToDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex >= 0)
        {
            loadEmployees();

        }
    }
}
