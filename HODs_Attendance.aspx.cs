using System;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class HODs_Attendance : System.Web.UI.Page
{
    DALBase objbase = new DALBase();
    int UserLevel, UserType;

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
            //    {
            if (!IsPostBack)
            {
                trFrmDate.Visible = true;

                DateTime d = DateTime.Now;
                txtFrmDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();

                try
                {
                    //loadMonths();
                    //loadDepartments();
                    // loadEmployees();

                    string queryStr = Request.QueryString["r"];

                    if (queryStr == "N4548")
                    {
                        rbLstRpt.SelectedValue = "0";
                        ViewState["rptmode"] = "?r=N4548";
                    }
                    else if (queryStr == "P8454")
                    {
                        rbLstRpt.SelectedValue = "1";
                        ViewState["rptmode"] = "?r=P8454";
                    }
                    else if (queryStr == "N4845")
                    {
                        rbLstRpt.SelectedValue = "2";
                        ViewState["rptmode"] = "?r=N4845";
                    }
                    else if (queryStr == "N5484")
                    {
                        rbLstRpt.SelectedValue = "3";
                        ViewState["rptmode"] = "?r=N5484";
                    }
                    else if (queryStr == "T4548")
                    {
                        rbLstRpt.SelectedValue = "4";
                        ViewState["rptmode"] = "?r=T4548";

                    }



                }
                catch (Exception ex)
                {
                    Session["error"] = ex.Message;
                    Response.Redirect("ErrorPage.aspx", false);
                }

            }
            //    }
            //else
            //    {
            //    Session["error"] = "You Are Not Authorized To Access This Page";
            //    Response.Redirect("ErrorPage.aspx", false);
            //    }
        }
        else
        {
            Response.Redirect("~/login.aspx");

        }
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {

        int UserLevel, UserType;

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());

        string repStr = "";
        if (rbLstRpt.SelectedValue == "0")
        {
            Session["reppath"] = "Reports\\rptHODsMorningAttendance.rpt";
            Session["rep"] = "rptHODsMorningAttendance.rpt";

            #region 'HODs Morning Attendance'


            Session["Att_Date"] = this.txtFrmDate.Text;

            //    if (UserLevel == 4) //Campus
            //        {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.Center_Id}=" + Session["CenterID"].ToString();
            //        }
            //    else if (UserLevel == 3)//Region
            //        {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}=0 ";

            //        }
            //    else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //        {
            //        repStr = "{vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
            //        }

            //        if (rbMonth.Checked)
            //        {
            //             repStr = repStr + " AND {vw_AttendanceRep.PMonth}='"+ddlMonths.SelectedValue.ToString()+"'" ;
            //        }
            //        else if (rbRange.Checked)
            //        {
            //            repStr = repStr + "and Date({vw_AttendanceRep.AttDate})>=#" + txtFrmDate.Text + "# and Date({vw_AttendanceRep.AttDate})<=#" + txtToDate.Text + "#";
            //        }

            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //        {
            //            string _str ;
            //           _str=loadEmployeesReportTo();
            //           repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //        }
            //    else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //    }

            //if (chkemp.Checked)
            //    {
            //    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString() + "'";
            //    }

            #endregion

            Session["CriteriaRpt"] = repStr;
            Session["LastPage"] = "~/HODs_Attendance.aspx";// +ViewState["rptmode"].ToString();
            Response.Redirect("~/rptAllReports.aspx");

        }




    }



    //protected void loadEmployees()
    //    {

    //    BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

    //    DataTable dt = new DataTable();

    //    obj.ReportTo =  Session["EmployeeCode"].ToString().Trim();
    //    obj.UserType_id = Convert.ToInt32(Session["UserType"].ToString());




    //    UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
    //    UserType = Convert.ToInt32(Session["UserType"].ToString());


    //    if (UserLevel == 4)
    //        {
    //        obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
    //        obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
    //        }
    //    else if (UserLevel == 3)
    //        {
    //        obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
    //        obj.Center_id = 0;
    //        }
    //    else if (UserLevel == 1 || UserLevel == 2)
    //        {
    //        obj.Region_id = 0;
    //        obj.Center_id = 0;
    //        }


    //    obj.Status_id = 2;
    //    obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);

    //    dt = obj.EmplyeeReportToFetch(obj);
    //    if (dt.Rows.Count > 0)
    //        {
    //        ddlEmployeecode.DataTextField = "Descr";
    //        ddlEmployeecode.DataValueField = "code";
    //        ddlEmployeecode.DataSource = dt;
    //        ddlEmployeecode.DataBind();
    //        }
    //    }

    //protected void loadDepartments()
    //    {

    //    BLLAttendance obj = new BLLAttendance();

    //    DataTable dt = new DataTable();

    //    obj.PMonthDesc = ddlMonths.SelectedValue;
    //    obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
    //    obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

    //    dt = obj.AttendanceFetchDepartments(obj);
    //    if (dt.Rows.Count > 0)
    //        {
    //        ddlDepartment.DataTextField = "DeptName";
    //        ddlDepartment.DataValueField = "Deptcode";
    //        ddlDepartment.DataSource = dt;
    //        ddlDepartment.DataBind();
    //        loadEmployees();
    //        }
    //    }

    //protected void loadMonths()
    //    {

    //    BLLPeriod obj = new BLLPeriod();

    //    DataTable dt = new DataTable();
    //    obj.InActive = "n";
    //    dt = obj.PeriodFetch(obj);
    //    if (dt.Rows.Count > 0)
    //        {
    //        ddlMonths.DataTextField = "PMonthDesc";
    //        ddlMonths.DataValueField = "PMonth";
    //        ddlMonths.DataSource = dt;
    //        ddlMonths.DataBind();
    //        }
    //    ddlMonths.SelectedValue = Session["CurrentMonth"].ToString();
    //    }


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
    //    {
    //    if (chkemp.Checked)
    //        {
    //            trsingleemp.Visible = true;
    //            if (Session["UserType"] != null)
    //                {
    //                int _part_Id = Convert.ToInt32(Session["UserType"].ToString());
    //                if (_part_Id == 19 || _part_Id == 22 || _part_Id == 25 )
    //                    {
    //                    TrDept.Visible = true;
    //                    loadDepartments();
    //                    }
    //                else
    //                    {
    //                    TrDept.Visible = false;
    //                    }
    //                }
    //        }
    //    else
    //        {
    //            trsingleemp.Visible = false;
    //            TrDept.Visible = false;
    //        }
    //    }

    protected void rbLstRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbLstRpt.SelectedValue == "0")
        {
        }
        else if (rbLstRpt.SelectedValue == "1")
        {
        }
    }

    //protected void rbMonth_CheckedChanged(object sender, EventArgs e)
    //    {
    //    if (rbMonth.Checked)
    //        {
    //        trFrmDate.Visible = false;
    //        trMonth.Visible = true;
    //        trToDate.Visible = false;
    //        }
    //    }
    //protected void rbRange_CheckedChanged(object sender, EventArgs e)
    //    {
    //    if (rbRange.Checked)
    //        {
    //        trFrmDate.Visible = true;
    //        trMonth.Visible = false;
    //        trToDate.Visible = true;

    //        DateTime d = DateTime.Now;

    //        txtFrmDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();
    //        txtToDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();
    //        }
    //    }
    //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //    if (ddlDepartment.SelectedIndex>=0)
    //        {
    //        loadEmployees();

    //        }
    //    }
}
