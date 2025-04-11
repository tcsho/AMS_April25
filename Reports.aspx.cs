using System;
using System.Data;
using System.Web.UI.WebControls;
 
using System.Drawing;
using System.Collections.Generic;
using System.Configuration;

public partial class Repoprts : System.Web.UI.Page
{
    DALBase objbase = new DALBase();
    BLLNetworkCenter objBLLNetwork = new BLLNetworkCenter();

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
                try
                {
                    ViewState["tMoodLate"] = "uncheck";
                    loadMonths();

                    loadRegions();
                    String s = Session["UserType"].ToString();


                    if (Session["UserType"].ToString() == "26")
                    {
                        div_region.Visible = true;
                        div_center.Visible = true;
                    }
                    else if (Session["UserType"].ToString() == "19")
                    {
                        div_region.Visible = true;
                        div_center.Visible = true;
                        ddlDepartment.Enabled = true;
                        TrDept.Visible = true;
                    }
                    else
                    {
                        div_region.Visible = false;
                        div_center.Visible = false;
                    }



                    if (Session["UserType"].ToString() == "22")
                    {
                        loadCenter();
                        div_center.Visible = true;
                        TrDept.Visible = true;
                    }

                    if (Session["UserType"].ToString() == "39")
                    {
                        div_region.Visible = false;
                        ddl_region.Enabled = false;
                        div_center.Visible = true;
                        ddlDepartment.Enabled = false;
                        TrDept.Visible = true;
                        ddl_region.SelectedValue = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();
                        loadCenter();

                    }
                    if (Session["UserType"].ToString() == "25")
                    {
                        TrDept.Visible = true;
                    }

                    loadCenter();
                    loadDepartments();
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
                    else if (queryStr == "S1225")
                    {
                        rbLstRpt.SelectedValue = "6";
                        ViewState["rptmode"] = "?r=S1225";

                    }
                    else if (queryStr == "T4549")
                    {
                        rbLstRpt.SelectedValue = "8";
                        ViewState["rptmode"] = "?r=T4549";

                    }
                    else if (queryStr == "T4550")
                    {
                        rbLstRpt.SelectedValue = "7";
                        ViewState["rptmode"] = "?r=T4550";

                    }
                    string [] list = ConfigurationManager.AppSettings["CafeLogViewEmp"].Split(',');
                    foreach (string item in list)
                    {
                        if(item.ToString().Trim()== Session["EmployeeCode"].ToString().Trim())
                        {
                            rblCafe.Visible = true;
                        }
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
    protected void btnCafeReport_Click(object sender, EventArgs e)
    {
        try
        {
            string repStr = "";
            if (rbMonth.Checked == true)
            {
                if (ddlMonths.SelectedIndex > 0)
                    repStr = "{vw_CAFETERIA_LOG.PMonth}= '" + ddlMonths.SelectedValue + "'";
            }
            else if (rbRange.Checked == true)
            {
                if (!String.IsNullOrEmpty(txtFrmDate.Text) && !String.IsNullOrEmpty(txtToDate.Text))
                {
                    DateTime dtfrom = Convert.ToDateTime(txtFrmDate.Text);
                    DateTime dtto = Convert.ToDateTime(txtToDate.Text);
                    repStr = repStr + " Date({vw_CAFETERIA_LOG.CafeDate})>=#" + txtFrmDate.Text + "# and Date({vw_CAFETERIA_LOG.CafeDate})<=#" + txtToDate.Text + "#";

                }
            }
            if (ddl_region.SelectedIndex > 0)
                repStr = repStr + " and {vw_CAFETERIA_LOG.Region_Id}= '" + ddl_region.SelectedValue + "'";
            if (ddlDepartment.SelectedIndex > 0)
                repStr = repStr + " and {vw_CAFETERIA_LOG.DeptCode}= '" + ddlDepartment.SelectedValue + "'";
            if (ddlEmployeecode.SelectedIndex > 0)
                repStr = repStr + " and {vw_CAFETERIA_LOG.EmployeeCode}=  " + ddlEmployeecode.SelectedValue + " ";
            if (rblCafe.SelectedValue == "0")
            {
                ViewState["rptmode"] = "";
                Session["reppath"] = "Reports\\rptCafeteriaLogEmployeeWiseSummary.rpt";
                Session["rep"] = "rptCafeteriaLogEmployeeWiseSummary.rpt";

            }
            if (rblCafe.SelectedValue == "1")
            {
                ViewState["rptmode"] = "";
                Session["reppath"] = "Reports\\rptCafeteriaLogWithLogo.rpt";
                Session["rep"] = "rptCafeteriaLogWithLogo.rpt";

            }

            Session["CriteriaRpt"] = repStr;
            Session["LastPage"] = "~/Reports.aspx" + ViewState["rptmode"].ToString();
            Response.Redirect("~/rptAllReports.aspx", false);
        }
        catch (Exception ex)
        {
            string s = ex.Message;
            //string inner = ex.InnerException.ToString();
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

            Session["reppath"] = "Reports\\rptAttendance.rpt";
            Session["rep"] = "rptAttendance.rpt";


            #region 'Attendance Report'



            if (UserLevel == 4) //Campus
            {
                if(Session["EmployeeCode"].ToString().TrimEnd() == "35970")
                {
                    repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.Center_Id} IN [" + Session["CenterID"].ToString() + ", 41901002, 42001001, 41201001]";
                }
                else
                {
                    repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.Center_Id}=" + Session["CenterID"].ToString();
                }
            }
            else if (UserLevel == 3)//Region
            {
                if (Session["UserType"].ToString() == "22")
                {
                    //As per request of SR to provide attendance report for whole region req# 2711
                    //repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
                    repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

                    if (ddl_center.SelectedIndex > 0)
                    {
                        repStr = repStr + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
                    }
                }
                else if (Session["UserType"].ToString() == "20")
                {
                   //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
                    repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

                    if (ddl_center.SelectedIndex > 0)
                    {
                        repStr = repStr + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
                    }
                }
                else
                {
                    
                    repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString()  + " and {vw_AttendanceRep.CenterId}=0 ";
                }
            }
            if (UserLevel == 5) //Network
            {


                if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
                {
                    repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString();
                    if (chkemp.Checked)
                    {

                        string _str;
                        //_str = loadEmployees();
                        _str = loadEmployeesReportTo();
                        if (_str != "")
                        {
                            repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
                        }
                        else
                        {
                            repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
                        }


                    }
                    else
                    {

                        DataTable dt = new DataTable();
                        dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
                        if (dt.Rows.Count > 0)
                        {
                            repStr = repStr + " and {vw_AttendanceRep.CenterId} IN [";
                            int i = 0;
                            foreach (DataRow row in dt.Rows)
                            {

                                string Center_ID = dt.Rows[i]["Center_ID"].ToString();
                                repStr = repStr + Center_ID + ",";
                                i++;
                            }
                            repStr = repStr.Substring(0, repStr.Length - 1);
                            repStr = repStr + " ]";

                        }
                    }
                }
                else
                {
                    repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
                }

            }
            else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            {
                //repStr = "{vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
                if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
                {

                    repStr = " 1=1 ";
                }
                else if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
                {
                    if (Session["UserType"].ToString() == "26") //According to Audit Requirements to view Attendance for whole region including schools. 
                    {
                        repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

                        if (ddl_center.SelectedIndex > 0)
                        {
                            repStr = repStr + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
                        }
                    }
                    else // For HR to view report for only Region Departments
                        repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
                }
                else
                {
                    repStr = "{vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
                }
            }

            if (rbMonth.Checked)
            {
                repStr = repStr + " AND {vw_AttendanceRep.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            }
            else if (rbRange.Checked)
            {
                //repStr = repStr + " and Date({vw_AttenLogRep.AttDate}) in Date(" + txtFrmDate.Text + ") to Date(" + txtToDate.Text + ")";

                repStr = repStr + " and Date({vw_AttendanceRep.AttDate})>=#" + txtFrmDate.Text + "# and Date({vw_AttendanceRep.AttDate})<=#" + txtToDate.Text + "#";
            }

            if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            {
                string _str;
                _str = loadEmployeesReportTo();
                if (_str != "")
                {
                    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
                }
                else
                {
                    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
                }
            }
            else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            {
                repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            }

            //if (chkemp.Checked && ddlEmployeecode.SelectedIndex > 0)
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString().Trim() + "'";
            //}
            //else if (ddlDepartment.SelectedIndex > 0 && ddlEmployeecode.SelectedIndex == 0)
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            //}
            if (chkemp.Checked && selectedEmpCount() > 0)
            {
                string _str;
                _str = loadEmployeesReportTo();
                repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
                //repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + lstEmployee.SelectedValue.ToString().Trim() + "'";
            }
            else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
            {
                repStr = repStr + " and {vw_AttendanceRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            }
            #endregion



        }
        else if (rbLstRpt.SelectedValue == "1")
        {
            Session["reppath"] = "Reports\\rptAttenLog.rpt";
            Session["rep"] = "rptAttenLog.rpt";


            #region 'Attendance Log Report'

            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_AttenLogRep");

            #endregion

        }

        else if (rbLstRpt.SelectedValue == "2")
        {
            Session["reppath"] = "Reports\\rptAbsent1.rpt";
            Session["rep"] = "rptAbsent1.rpt";

            #region 'Absent Report'

            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_AbsentRep");

            //if (UserLevel == 4) //Campus
            //{
            //    repStr = "{vw_AbsentRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AbsentRep.Center_Id}=" + Session["CenterID"].ToString();
            //}
            //else if (UserLevel == 3)//Region
            //{
            //    if (Session["UserType"].ToString() == "22")
            //    {
            //        repStr = "{vw_AbsentRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AbsentRep.CenterId}= " + ddl_center.SelectedValue;
            //    }
            //    else if (Session["UserType"].ToString() == "20")
            //    {
            //        //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
            //        repStr = "{vw_AbsentRep.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //        if (ddl_center.SelectedIndex > 0)
            //        {
            //            repStr = repStr + " and {vw_AbsentRep.CenterId}=" + ddl_center.SelectedValue;
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_AbsentRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AbsentRep.CenterId}=0 ";
            //    }
            //}
            //if (UserLevel == 5) //Network
            //{
            //    // repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;

            //    if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
            //    {
            //        repStr = "{vw_AbsentRep.Region_Id}=" + Session["RegionID"].ToString();
            //        if (chkemp.Checked)
            //        {

            //            string _str;
            //            //_str = loadEmployees();
            //            _str = loadEmployeesReportTo();
            //            if (_str != "")
            //            {
            //                repStr = repStr + " and {vw_AbsentRep.EmployeeCode} IN " + _str;
            //            }
            //            else
            //            {
            //                repStr = repStr + " and {vw_AbsentRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //            }


            //        }
            //        else
            //        {

            //            DataTable dt = new DataTable();
            //            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            //            if (dt.Rows.Count > 0)
            //            {
            //                repStr = repStr + " and {vw_AbsentRep.CenterId} IN [";
            //                int i = 0;
            //                foreach (DataRow row in dt.Rows)
            //                {

            //                    string Center_ID = dt.Rows[i]["Center_ID"].ToString();
            //                    repStr = repStr + Center_ID + ",";
            //                    i++;
            //                }
            //                repStr = repStr.Substring(0, repStr.Length - 1);
            //                repStr = repStr + " ]";

            //            }
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_AbsentRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AbsentRep.CenterId}= " + ddl_center.SelectedValue;

            //    }
            //}
            //else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //{
            //    //repStr = "{vw_AbsentRep.RegionId}=0 and {vw_AbsentRep.CenterId}=0";

            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //    {

            //        repStr = " 1=1 ";
            //    }
            //    else if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
            //    {
            //        repStr = "{vw_AbsentRep.RegionId}=" + ddl_region.SelectedValue + " and {vw_AbsentRep.CenterId}=" + ddl_center.SelectedValue;
            //    }
            //    else
            //    {
            //        repStr = "{vw_AbsentRep.RegionId}=0 and {vw_AbsentRep.CenterId}=0";
            //    }

            //}




            //if (rbMonth.Checked)
            //{
            //    repStr = repStr + " AND {vw_AbsentRep.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            //}
            //else if (rbRange.Checked)
            //{
            //    repStr = repStr + " and Date({vw_AbsentRep.AttDate})>=#" + txtFrmDate.Text + "# and Date({vw_AbsentRep.AttDate})<=#" + txtToDate.Text + "#";
            //}

            //if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    if (_str != "")
            //    {
            //        repStr = repStr + " and {vw_AbsentRep.EmployeeCode} IN " + _str;
            //    }
            //    else
            //    {
            //        repStr = repStr + " and {vw_AbsentRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //    }
            //}
            //else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //{
            //    repStr = repStr + " and {vw_AbsentRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //}

            ////if (chkemp.Checked)
            ////{
            ////    repStr = repStr + " and {vw_AbsentRep.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString() + "'";
            ////}

            //if (chkemp.Checked && selectedEmpCount() > 0)
            //{
            //    //repStr = repStr + " and {vw_AbsentRep.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString().Trim() + "'";
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    repStr = repStr + " and {vw_AbsentRep.EmployeeCode} IN " + _str;
            //}
            //else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
            //{
            //    repStr = repStr + " and {vw_AbsentRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            //}
            #endregion
        }
        else if (rbLstRpt.SelectedValue == "3")
        {
            Session["reppath"] = "Reports\\rptAbsentReds.rpt";
            Session["rep"] = "rptAbsentReds.rpt";

            #region 'Reds Report'

            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_RedsforHODApproval");

            //if (UserLevel == 4) //Campus
            //{
            //    repStr = " {vw_RedsforHODApproval.NegTypeGroup}='Red' AND {vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_RedsforHODApproval.Center_Id}=" + Session["CenterID"].ToString();
            //}
            //else if (UserLevel == 3)//Region
            //{
            //    if (Session["UserType"].ToString() == "22")
            //    {
            //        repStr = "{vw_RedsforHODApproval.NegTypeGroup}='Red' AND {vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_RedsforHODApproval.CenterId}= " + ddl_center.SelectedValue;
            //    }
            //    else if (Session["UserType"].ToString() == "20")
            //    {
            //        //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
            //        repStr = "{vw_RedsforHODApproval.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //        if (ddl_center.SelectedIndex > 0)
            //        {
            //            repStr = repStr + " and {vw_RedsforHODApproval.CenterId}=" + ddl_center.SelectedValue;
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_RedsforHODApproval.NegTypeGroup}='Red' AND {vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_RedsforHODApproval.CenterId}=0 ";
            //    }
            //}
            //if (UserLevel == 5) //Network
            //{
            //    // repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
            //    if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
            //    {
            //        repStr = "{vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString();
            //        if (chkemp.Checked)
            //        {

            //            string _str;
            //            //_str = loadEmployees();
            //            _str = loadEmployeesReportTo();
            //            if (_str != "")
            //            {
            //                repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode} IN " + _str;
            //            }
            //            else
            //            {
            //                repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //            }


            //        }
            //        else
            //        {

            //            DataTable dt = new DataTable();
            //            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            //            if (dt.Rows.Count > 0)
            //            {
            //                repStr = repStr + " and {vw_RedsforHODApproval.CenterId} IN [";
            //                int i = 0;
            //                foreach (DataRow row in dt.Rows)
            //                {

            //                    string Center_ID = dt.Rows[i]["Center_ID"].ToString();
            //                    repStr = repStr + Center_ID + ",";
            //                    i++;
            //                }
            //                repStr = repStr.Substring(0, repStr.Length - 1);
            //                repStr = repStr + " ]";

            //            }
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_RedsforHODApproval.CenterId}= " + ddl_center.SelectedValue;

            //    }
            //}
            //else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //{
            //    //repStr = " {vw_RedsforHODApproval.NegTypeGroup}='Red' AND {vw_RedsforHODApproval.RegionId}=0 and {vw_RedsforHODApproval.CenterId}=0";

            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //    {

            //        repStr = " 1=1 ";
            //    }
            //    else if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
            //    {
            //        repStr = "{vw_RedsforHODApproval.RegionId}=" + ddl_region.SelectedValue + " and {vw_RedsforHODApproval.CenterId}=" + ddl_center.SelectedValue;
            //    }
            //    else
            //    {
            //        repStr = " {vw_RedsforHODApproval.NegTypeGroup}='Red' AND {vw_RedsforHODApproval.RegionId}=0 and {vw_RedsforHODApproval.CenterId}=0";
            //    }
            //}

            //if (rbMonth.Checked)
            //{
            //    repStr = repStr + " AND {vw_RedsforHODApproval.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            //}
            //else if (rbRange.Checked)
            //{
            //    repStr = repStr + " and Date({vw_RedsforHODApproval.AttDate})>=#" + txtFrmDate.Text + "# and Date({vw_RedsforHODApproval.AttDate})<=#" + txtToDate.Text + "#";
            //}

            //if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    if (_str != "")
            //    {
            //        repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode} IN " + _str;
            //    }
            //    else
            //    {
            //        repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //    }
            //}
            //else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //{
            //    repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //}

            ////if (chkemp.Checked)
            ////{
            ////    repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString() + "'";
            ////}
            ////if (chkemp.Checked && ddlEmployeecode.SelectedIndex > 0)
            ////{
            ////    repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString().Trim() + "'";
            ////}
            ////else if (ddlDepartment.SelectedIndex > 0 && ddlEmployeecode.SelectedIndex == 0)
            ////{
            ////    repStr = repStr + " and {vw_RedsforHODApproval.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            ////}
            //if (chkemp.Checked && selectedEmpCount() > 0)
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode} IN " + _str;
            //    //repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + lstEmployee.SelectedValue.ToString().Trim() + "'";
            //}
            //else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
            //{
            //    repStr = repStr + " and {vw_RedsforHODApproval.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            //}

            #endregion

        }
        else if (rbLstRpt.SelectedValue == "4")
        {
            Session["reppath"] = "Reports\\rptEmpStrLvBal.rpt";
            Session["rep"] = "rptEmpStrLvBal.rpt";

            #region 'Leave Balance Report'

            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_ERP_employeeLeaveBalance");

            //if (UserLevel == 4) //Campus
            //{
            //    repStr = "{employeeprofile.Region_Id}=" + Session["RegionID"].ToString() + " and {employeeprofile.Center_Id}=" + Session["CenterID"].ToString();
            //}
            //else if (UserLevel == 3)//Region
            //{
            //    //repStr = "{employeeprofile.Region_Id}=" + Session["RegionID"].ToString() + " and ISNULL({employeeprofile.Center_Id}) ";
            //    if (ddl_center.SelectedValue == "0")
            //    {
            //        repStr = "{employeeprofile.Region_Id}=" + Session["RegionID"].ToString() + " and ISNULL({employeeprofile.Center_Id}) ";
            //    }
            //    else if (Session["UserType"].ToString() == "20")
            //    {
            //        //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
            //        repStr = "{employeeprofile.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //        if (ddl_center.SelectedIndex > 0)
            //        {
            //            repStr = repStr + " and {employeeprofile.CenterId}=" + ddl_center.SelectedValue;
            //        }
            //    }
            //    else if (Convert.ToInt32(ddl_center.SelectedValue) > 0)
            //    {
            //        repStr = "{employeeprofile.Region_Id}=" + Session["RegionID"].ToString() + " and {employeeprofile.Center_Id}=" + ddl_center.SelectedValue;

            //    }
            //    //else if (Convert.ToInt32(ddl_region.SelectedValue) > 0 && Convert.ToInt32(ddl_center.SelectedValue) > 0)
            //    //{
            //    //    repStr = "{employeeprofile.Region_Id}=" + ddl_region.SelectedValue + " and {employeeprofile.Center_Id}=" + ddl_center.SelectedValue;


            //    //}

            //}
            //if (UserLevel == 5) //Network
            //{
            //    // repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
            //    if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
            //    {
            //        repStr = "{employeeprofile.Region_Id}=" + Session["RegionID"].ToString();
            //        if (chkemp.Checked)
            //        {

            //            string _str;
            //            //_str = loadEmployees();
            //            _str = loadEmployeesReportTo();
            //            if (_str != "")
            //            {
            //                repStr = repStr + " and {vw_ERP_employeeLeaveBalance.EmployeeCode} IN " + _str;
            //            }
            //            else
            //            {
            //                repStr = repStr + " and {vw_ERP_employeeLeaveBalance.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //            }


            //        }
            //        else
            //        {

            //            DataTable dt = new DataTable();
            //            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            //            if (dt.Rows.Count > 0)
            //            {
            //                repStr = repStr + " and {employeeprofile.Center_Id} IN [";
            //                int i = 0;
            //                foreach (DataRow row in dt.Rows)
            //                {

            //                    string Center_ID = dt.Rows[i]["Center_ID"].ToString();
            //                    repStr = repStr + Center_ID + ",";
            //                    i++;
            //                }
            //                repStr = repStr.Substring(0, repStr.Length - 1);
            //                repStr = repStr + " ]";

            //            }
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{employeeprofile.Region_Id}=" + Session["RegionID"].ToString() + " and {employeeprofile.Center_Id}= " + ddl_center.SelectedValue;

            //    }
            //}
            //else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //{
            //    // repStr = "ISNULL({employeeprofile.Region_Id}) and ISNULL({employeeprofile.Center_Id})";



            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //    {

            //        repStr = " 1=1 ";
            //    }
            //    else if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
            //    {
            //        // repStr = "{employeeprofile.Region_Id} " + ((ddl_region.SelectedValue == "0") ? " is null " : " = " + ddl_region.SelectedValue) + " and {employeeprofile.Center_Id} " + ((ddl_center.SelectedValue == "0") ? " is null " : " = " + ddl_center.SelectedValue);
            //        if (ddl_region.SelectedValue == "0" && ddl_center.SelectedValue == "0")
            //        {
            //            repStr = "ISNULL({employeeprofile.Region_Id}) and ISNULL({employeeprofile.Center_Id})";
            //        }
            //        else if (Convert.ToInt32(ddl_region.SelectedValue) > 0 && ddl_center.SelectedValue == "0")
            //        {
            //            repStr = "{employeeprofile.Region_Id}=" + ddl_region.SelectedValue + " and ISNULL({employeeprofile.Center_Id}) ";

            //        }
            //        else if (ddl_region.SelectedValue == "0" && Convert.ToInt32(ddl_center.SelectedValue) > 0)
            //        {
            //            repStr = "ISNULL({employeeprofile.Region_Id})" + " and {employeeprofile.Center_Id}=" + ddl_center.SelectedValue;

            //        }
            //        else if (Convert.ToInt32(ddl_region.SelectedValue) > 0 && Convert.ToInt32(ddl_center.SelectedValue) > 0)
            //        {
            //            repStr = "{employeeprofile.Region_Id}=" + ddl_region.SelectedValue + " and {employeeprofile.Center_Id}=" + ddl_center.SelectedValue;


            //        }
            //    }
            //    else
            //    {
            //        repStr = "ISNULL({employeeprofile.Region_Id}) and ISNULL({employeeprofile.Center_Id})";
            //    }
            //}

            //if (rbMonth.Checked)
            //{
            //    string _nstr = ddlMonths.SelectedValue.ToString();

            //    repStr = repStr + " AND {vw_ERP_employeeLeaveBalance.Year}=" + _nstr.Substring(0, 4) + "";
            //}
            //else if (rbRange.Checked)
            //{
            //    string _nstr = txtFrmDate.Text;
            //    repStr = repStr + " AND {vw_ERP_employeeLeaveBalance.Year}=" + _nstr.Substring(5) + "";
            //}

            //if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    if (_str != "")
            //    {
            //        repStr = repStr + " and {vw_ERP_employeeLeaveBalance.EmployeeCode} IN " + _str;
            //    }
            //    else
            //    {
            //        repStr = repStr + " and {vw_ERP_employeeLeaveBalance.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //    }
            //}
            //else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //{
            //    repStr = repStr + " and {vw_ERP_employeeLeaveBalance.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //}

            ////if (chkemp.Checked)
            ////{
            ////    repStr = repStr + " and {vw_ERP_employeeLeaveBalance.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString() + "'";
            ////}
            //if (chkemp.Checked && selectedEmpCount() > 0)
            //{
            //    //repStr = repStr + " and {vw_ERP_employeeLeaveBalance.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString().Trim() + "'";
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    repStr = repStr + " and {vw_ERP_employeeLeaveBalance.EmployeeCode} IN " + _str;
            //}
            //else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
            //{
            //    repStr = repStr + " and {EmployeeProfile.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            //}

            #endregion

        }
        else if (rbLstRpt.SelectedValue == "5")
        {
            Session["reppath"] = "Reports\\rptAbsentMISS.rpt";
            Session["rep"] = "rptAbsentReds.rpt";

            #region 'MIO Report'

            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_RedsforHODApproval");

            //if (UserLevel == 4) //Campus
            //{
            //    repStr = " {vw_RedsforHODApproval.NegTypeGroup}='M' AND {vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_RedsforHODApproval.Center_Id}=" + Session["CenterID"].ToString();
            //}
            //else if (UserLevel == 3 || UserLevel == 5)//Region                
            //{
            //    if (Session["UserType"].ToString() == "22")
            //    {
            //        repStr = "{vw_RedsforHODApproval.NegTypeGroup}='M' AND {vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_RedsforHODApproval.CenterId}=" + ddl_center.SelectedValue;
            //    }
            //    else if (Session["UserType"].ToString() == "20")
            //    {
            //        //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
            //        repStr = "{vw_RedsforHODApproval.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //        if (ddl_center.SelectedIndex > 0)
            //        {
            //            repStr = repStr + " and {vw_RedsforHODApproval.CenterId}=" + ddl_center.SelectedValue;
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_RedsforHODApproval.NegTypeGroup}='M' AND {vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_RedsforHODApproval.CenterId}=0 ";
            //    }
            //}
            //if (UserLevel == 5) //Network
            //{
            //    // repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
            //    if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
            //    {
            //        repStr = "{vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString();
            //        if (chkemp.Checked)
            //        {

            //            string _str;
            //            //_str = loadEmployees();
            //            _str = loadEmployeesReportTo();
            //            if (_str != "")
            //            {
            //                repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode} IN " + _str;
            //            }
            //            else
            //            {
            //                repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //            }


            //        }
            //        else
            //        {

            //            DataTable dt = new DataTable();
            //            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            //            if (dt.Rows.Count > 0)
            //            {
            //                repStr = repStr + " and {vw_RedsforHODApproval.CenterId} IN [";
            //                int i = 0;
            //                foreach (DataRow row in dt.Rows)
            //                {

            //                    string Center_ID = dt.Rows[i]["Center_ID"].ToString();
            //                    repStr = repStr + Center_ID + ",";
            //                    i++;
            //                }
            //                repStr = repStr.Substring(0, repStr.Length - 1);
            //                repStr = repStr + " ]";

            //            }
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_RedsforHODApproval.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_RedsforHODApproval.CenterId}= " + ddl_center.SelectedValue;

            //    }
            //}
            //else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //{
            //    //repStr = " {vw_RedsforHODApproval.NegTypeGroup}='M' AND {vw_RedsforHODApproval.RegionId}=0 and {vw_RedsforHODApproval.CenterId}=0";

            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //    {
            //        repStr = " 1=1 ";
            //    }
            //    else if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
            //    {
            //        repStr = " {vw_RedsforHODApproval.NegTypeGroup}='M' AND {vw_RedsforHODApproval.RegionId} = " + ddl_region.SelectedValue + " and {vw_RedsforHODApproval.CenterId} = " + ddl_center.SelectedValue;
            //    }
            //    else
            //    {
            //        repStr = " {vw_RedsforHODApproval.NegTypeGroup}='M' AND {vw_RedsforHODApproval.RegionId}=0 and {vw_RedsforHODApproval.CenterId}=0";
            //    }
            //}

            //if (rbMonth.Checked)
            //{
            //    repStr = repStr + " AND {vw_RedsforHODApproval.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            //}
            //else if (rbRange.Checked)
            //{
            //    repStr = repStr + " and Date({vw_RedsforHODApproval.AttDate})>=#" + txtFrmDate.Text + "# and Date({vw_RedsforHODApproval.AttDate})<=#" + txtToDate.Text + "#";
            //}

            //if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    if (_str != "")
            //    {
            //        repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode} IN " + _str;
            //    }
            //    else
            //    {
            //        repStr = repStr + " and  {vw_RedsforHODApproval.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //    }
            //}
            //else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //{
            //    repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //}

            ////if (chkemp.Checked)
            ////{
            ////    repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString() + "'";
            ////}
            ////if (chkemp.Checked && ddlEmployeecode.SelectedIndex > 0)
            ////{
            ////    repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString().Trim() + "'";
            ////}
            ////else if (ddlDepartment.SelectedIndex > 0 && ddlEmployeecode.SelectedIndex == 0)
            ////{
            ////    repStr = repStr + " and {vw_RedsforHODApproval.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            ////}
            //if (chkemp.Checked && selectedEmpCount() > 0)
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    repStr = repStr + " and {vw_RedsforHODApproval.EmployeeCode} IN " + _str;
            //    //repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + lstEmployee.SelectedValue.ToString().Trim() + "'";
            //}
            //else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            //}
            #endregion

        }
        else if (rbLstRpt.SelectedValue == "6")
        {
            Session["reppath"] = "Reports\\rptLunchBreakInOutReport.rpt";
            Session["rep"] = "rptLunchBreakInOutReport.rpt";



            #region 'Lunch Break In Out Report'
            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_RedsforHODApproval");

            //if (UserLevel == 4) //Campus
            //{
            //    repStr = " and Region_Id = " + Session["RegionID"].ToString() + " and Center_Id = " + Session["CenterID"].ToString();
            //}
            //else if (UserLevel == 3)//Region
            //{
            //    if (Session["UserType"].ToString() == "20")
            //    {
            //        //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
            //        repStr = "{vw_RedsforHODApproval.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //        if (ddl_center.SelectedIndex > 0)
            //        {
            //            repStr = repStr + " and {vw_RedsforHODApproval.CenterId}=" + ddl_center.SelectedValue;
            //        }
            //    }
            //    else
            //    {
            //        repStr = " and Region_Id = " + Session["RegionID"].ToString() + " and Center_Id is null ";
            //    }

            //}
            //if (UserLevel == 5) //Network
            //{
            //    repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;

            //}
            //else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //{
            //    //repStr = " and Region_Id is null and Center_Id is null ";

            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //    {
            //        repStr = " 1=1 ";
            //    }
            //    else if (Session["UserType"].ToString() == "26")
            //    {
            //        repStr = " and Region_Id = " + ddl_region.SelectedValue + " and Center_Id = " + ddl_center.SelectedValue;
            //    }
            //    else
            //    {
            //        repStr = " and Region_Id is null and Center_Id is null ";
            //    }
            //}

            //if (rbMonth.Checked)
            //{
            //    repStr = repStr + " and PMonth = '" + ddlMonths.SelectedValue.ToString() + "'";
            //}
            //else if (rbRange.Checked)
            //{
            //    string[] attFromDate = txtFrmDate.Text.Split('/');
            //    string[] attToDate = txtToDate.Text.Split('/');

            //    repStr = repStr + " and AttDate between '" + attFromDate[2] + "-" + attFromDate[0] + "-" + attFromDate[1] + "' and '" + attToDate[2] + "-" + attToDate[0] + "-" + attToDate[1] + "' ";
            //}

            //if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    _str = _str.Replace('\"', '\'');
            //    _str = _str.Replace('[', '(');
            //    _str = _str.Replace(']', ')');
            //    repStr = repStr + " and EmployeeCode IN " + _str;
            //}
            //else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //{
            //    repStr = repStr + " and EmployeeCode = '" + Session["EmployeeCode"].ToString() + "' ";
            //}

            //if (chkemp.Checked)
            //{
            //    repStr = repStr + " and EmployeeCode = '" + ddlEmployeecode.SelectedValue.ToString() + "' ";
            //}

            #endregion



        }

        else if (rbLstRpt.SelectedValue == "7")
        {
            Session["reppath"] = "Reports\\rptAttendanceSummaryDetailed.rpt";
            Session["rep"] = "rptAttendanceSummaryDetailed.rpt";


            #region 'Attendance Report Summary'

            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_AttendanceRep");

            //repStr = repStr + "  {vw_AttendanceRep.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";



            ////repStr = repStr + " and Date({vw_AttenLogRep.AttDate}) in Date(" + txtFrmDate.Text + ") to Date(" + txtToDate.Text + ")";
            //if (!String.IsNullOrEmpty(txtFrmDate.Text))
            //    repStr = repStr + " and Date({vw_AttendanceRep.AttDate})>=#" + txtFrmDate.Text;
            //if (!String.IsNullOrEmpty(txtToDate.Text))
            //    repStr = repStr + "# and Date({vw_AttendanceRep.AttDate})<=#" + txtToDate.Text + "#";

            //if (UserLevel == 4) //Campus
            //{
            //    repStr = repStr + " and  {vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.Center_Id}=" + Session["CenterID"].ToString();
            //}
            //else if (UserLevel == 3)//Region
            //{
            //    if (Session["UserType"].ToString() == "22")
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
            //    }
            //   else if (Session["UserType"].ToString() == "20")
            //    {
            //        //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
            //        repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //        if (ddl_center.SelectedIndex > 0)
            //        {
            //            repStr = repStr + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
            //        }
            //    }
            //    else
            //    {
            //        repStr = repStr + "and {vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}=0 ";
            //    }
            //}
            //if (UserLevel == 5) //Network
            //{


            //    if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
            //    {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString();
            //        if (chkemp.Checked)
            //        {

            //            string _str;
            //            //_str = loadEmployees();
            //            _str = loadEmployeesReportTo();
            //            if (_str != "")
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //            }
            //            else
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //            }


            //        }
            //        else
            //        {

            //            DataTable dt = new DataTable();
            //            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            //            if (dt.Rows.Count > 0)
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.CenterId} IN [";
            //                int i = 0;
            //                foreach (DataRow row in dt.Rows)
            //                {

            //                    string Center_ID = dt.Rows[i]["Center_ID"].ToString();
            //                    repStr = repStr + Center_ID + ",";
            //                    i++;
            //                }
            //                repStr = repStr.Substring(0, repStr.Length - 1);
            //                repStr = repStr + " ]";

            //            }
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;

            //    }

            //}
            //else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //{
            //    //repStr = "{vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //    {

            //        //repStr = " 1=1 ";
            //    }
            //    else if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
            //    {
            //        repStr = repStr+ " and {vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
            //    }
            //    else
            //    {
            //        if(ddl_region.SelectedIndex<=0 )
            //        repStr = repStr+ " and {vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
            //    }
            //}



            //if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    if (_str != "")
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //    }
            //    else
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //    }
            //}
            //else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //}

            ////if (chkemp.Checked && ddlEmployeecode.SelectedIndex > 0)
            ////{
            ////    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString().Trim() + "'";
            ////}
            ////else if (ddlDepartment.SelectedIndex > 0 && ddlEmployeecode.SelectedIndex == 0)
            ////{
            ////    repStr = repStr + " and {vw_AttendanceRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();

            ////}
            //if (chkemp.Checked && selectedEmpCount() > 0)
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //    //repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + lstEmployee.SelectedValue.ToString().Trim() + "'";
            //}
            //else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            //}
            #endregion

        }


        else if (rbLstRpt.SelectedValue == "8")
        {

            Session["reppath"] = "Reports\\rptAttendance_Analysis.rpt";
            Session["rep"] = "rptAttendance_Analysis.rpt";


            #region 'Attendance Analysis Report'

            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_AttendanceRep");
            repStr = repStr + "and {vw_AttendanceRep.OffDay}<>'Y'";


            //if (UserLevel == 4) //Campus
            //{
            //    repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.Center_Id}=" + Session["CenterID"].ToString();
            //}
            //else if (UserLevel == 3)//Region
            //{
            //    if (Session["UserType"].ToString() == "22")
            //    {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
            //    }
            //    else if (Session["UserType"].ToString() == "20")
            //    {
            //        //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
            //        repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //        if (ddl_center.SelectedIndex > 0)
            //        {
            //            repStr = repStr + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}=0 ";
            //    }
            //}
            //if (UserLevel == 5) //Network
            //{


            //    if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
            //    {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString();
            //        if (chkemp.Checked)
            //        {

            //            string _str;
            //            //_str = loadEmployees();
            //            _str = loadEmployeesReportTo();
            //            if (_str != "")
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //            }
            //            else
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //            }


            //        }
            //        else
            //        {

            //            DataTable dt = new DataTable();
            //            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            //            if (dt.Rows.Count > 0)
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.CenterId} IN [";
            //                int i = 0;
            //                foreach (DataRow row in dt.Rows)
            //                {

            //                    string Center_ID = dt.Rows[i]["Center_ID"].ToString();
            //                    repStr = repStr + Center_ID + ",";
            //                    i++;
            //                }
            //                repStr = repStr.Substring(0, repStr.Length - 1);
            //                repStr = repStr + " ]";

            //            }
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
            //    }

            //}
            //else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //{
            //    //repStr = "{vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //    {

            //        repStr = " 1=1 ";
            //    }
            //    else if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
            //    {
            //        if (Session["UserType"].ToString() == "26") //According to Audit Requirements to view Attendance for whole region including schools. 
            //        {
            //            repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //            if (ddl_center.SelectedIndex > 0)
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
            //            }
            //        }
            //        else // For HR to view report for only Region Departments
            //            repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
            //    }
            //    else
            //    {
            //        repStr = "{vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
            //    }
            //}

            //if (rbMonth.Checked)
            //{
            //    repStr = repStr + " AND {vw_AttendanceRep.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            //}
            //else if (rbRange.Checked)
            //{
            //    //repStr = repStr + " and Date({vw_AttenLogRep.AttDate}) in Date(" + txtFrmDate.Text + ") to Date(" + txtToDate.Text + ")";

            //    repStr = repStr + " and Date({vw_AttendanceRep.AttDate})>=#" + txtFrmDate.Text + "# and Date({vw_AttendanceRep.AttDate})<=#" + txtToDate.Text + "#";
            //}

            //if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //{
            //    string _str = loadEmployeesReportTo();

            //    if (_str != "")
            //    {

            //        repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //    }
            //    else
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString().Trim() + "'";
            //    }
            //}
            //else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //}

            //if (chkemp.Checked && selectedEmpCount() > 0)
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //    //repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + lstEmployee.SelectedValue.ToString().Trim() + "'";
            //}
            //else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            //}

            #endregion



        }
        else if (rbLstRpt.SelectedValue == "9")
        {
            Session["reppath"] = "Reports\\rptAttendanceSummaryAll.rpt";
            Session["rep"] = "rptAttendanceSummaryAll.rpt";


            #region 'Attendance Report Summary'

            repStr = SelectionCriteria((UserLevels)UserLevel, repStr, "vw_AttendanceRep");

            //repStr = repStr + "  {vw_AttendanceRep.PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
            ////repStr = repStr + " and Date({vw_AttenLogRep.AttDate}) in Date(" + txtFrmDate.Text + ") to Date(" + txtToDate.Text + ")";
            //if (!String.IsNullOrEmpty(txtFrmDate.Text))
            //    repStr = repStr + " and Date({vw_AttendanceRep.AttDate})>=#" + txtFrmDate.Text;
            //if (!String.IsNullOrEmpty(txtToDate.Text))
            //    repStr = repStr + "# and Date({vw_AttendanceRep.AttDate})<=#" + txtToDate.Text + "#";

            //if (UserLevel == 4) //Campus
            //{
            //    repStr = repStr + " and  {vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.Center_Id}=" + Session["CenterID"].ToString();
            //}
            //else if (UserLevel == 3)//Region
            //{
            //    if (Session["UserType"].ToString() == "22")
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;
            //    }
            //    else if (Session["UserType"].ToString() == "20")
            //    {
            //        //As per req# 2975 to allow view RO HOD to view all the attendance of sub ordinates in schools 
            //        repStr = "{vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue; //+ " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;

            //        if (ddl_center.SelectedIndex > 0)
            //        {
            //            repStr = repStr + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
            //        }
            //    }
            //    else
            //    {
            //        repStr = repStr + "and {vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}=0 ";
            //    }
            //}
            //if (UserLevel == 5) //Network
            //{


            //    if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
            //    {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString();
            //        if (chkemp.Checked)
            //        {

            //            string _str;
            //            //_str = loadEmployees();
            //            _str = loadEmployeesReportTo();
            //            if (_str != "")
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //            }
            //            else
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //            }


            //        }
            //        else
            //        {

            //            DataTable dt = new DataTable();
            //            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            //            if (dt.Rows.Count > 0)
            //            {
            //                repStr = repStr + " and {vw_AttendanceRep.CenterId} IN [";
            //                int i = 0;
            //                foreach (DataRow row in dt.Rows)
            //                {

            //                    string Center_ID = dt.Rows[i]["Center_ID"].ToString();
            //                    repStr = repStr + Center_ID + ",";
            //                    i++;
            //                }
            //                repStr = repStr.Substring(0, repStr.Length - 1);
            //                repStr = repStr + " ]";

            //            }
            //        }
            //    }
            //    else
            //    {
            //        repStr = "{vw_AttendanceRep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_AttendanceRep.CenterId}= " + ddl_center.SelectedValue;

            //    }

            //}
            //else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            //{
            //    //repStr = "{vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
            //    if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //    {

            //        //repStr = " 1=1 ";
            //    }
            //    else if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.RegionId}=" + ddl_region.SelectedValue + " and {vw_AttendanceRep.CenterId}=" + ddl_center.SelectedValue;
            //    }
            //    else
            //    {
            //        if (ddl_region.SelectedIndex <= 0)
            //            repStr = repStr + " and {vw_AttendanceRep.RegionId}=0 and {vw_AttendanceRep.CenterId}=0";
            //    }
            //}



            //if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23")
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    if (_str != "")
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //    }
            //    else
            //    {
            //        repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //    }
            //}
            //else if (Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "24")
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            //}

            ////if (chkemp.Checked && ddlEmployeecode.SelectedIndex > 0)
            ////{
            ////    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + ddlEmployeecode.SelectedValue.ToString().Trim() + "'";
            ////}
            ////else if (ddlDepartment.SelectedIndex > 0 && ddlEmployeecode.SelectedIndex == 0)
            ////{
            ////    repStr = repStr + " and {vw_AttendanceRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();

            ////}
            //if (chkemp.Checked && selectedEmpCount() > 0)
            //{
            //    string _str;
            //    _str = loadEmployeesReportTo();
            //    repStr = repStr + " and {vw_AttendanceRep.EmployeeCode} IN " + _str;
            //    //repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + lstEmployee.SelectedValue.ToString().Trim() + "'";
            //}
            //else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
            //{
            //    repStr = repStr + " and {vw_AttendanceRep.DeptCode}=" + ddlDepartment.SelectedValue.ToString();
            //}
            #endregion

        }


        Session["CriteriaRpt"] = repStr;
        Session["LastPage"] = "~/Reports.aspx" + ViewState["rptmode"].ToString();
        Response.Redirect("~/rptAllReports.aspx");
    }

    private string SelectionCriteria(UserLevels UserLevel, string repStr,string view)
    {


      if ( UserLevel == UserLevels.Super_Admin) // Admin
        {
            if (Session["UserType"].ToString() == ((int)UserTypes.Audit).ToString() || Session["UserType"].ToString() == ((int)UserTypes.HO_HR).ToString())
            {
                if (Session["UserType"].ToString() == ((int)UserTypes.Audit).ToString()) //According to Audit Requirements to view Attendance Log for whole region including schools. 
                {
                    repStr = "{" + view + ".RegionId}=" + ddl_region.SelectedValue;

                    if (ddl_center.SelectedIndex > 0)
                    {
                        repStr = repStr + " and {" + view + ".CenterId}=" + ddl_center.SelectedValue;
                    }
                }
                else // For HR to view report for only Region Departments
                    repStr = "{" + view + ".RegionId}=" + ddl_region.SelectedValue + " and {" + view + ".CenterId}=" + ddl_center.SelectedValue;
            }
            else
            {
                repStr = "{" + view + ".RegionId}=0 and {" + view + ".CenterId}=0";
            }
        }

        else if (UserLevel == UserLevels.Main_Organisation ) //Head Office  
        {

            if (ddl_region.SelectedIndex > 0 && ddl_center.SelectedIndex > 0)
            {
                repStr = "{" + view + ".Region_Id}=" + ddl_region.SelectedValue + " and {" + view + ".Center_Id}=" + ddl_center.SelectedValue;

            }
            else if (ddl_region.SelectedIndex > 0 && ddl_center.SelectedIndex == 0)
            {
                repStr = "{" + view + ".Region_Id}=" + ddl_region.SelectedValue;
            }
            else
            {
                repStr = "{" + view + ".RegionId}=0 and {" + view + ".CenterId}=0";
            }
            
            if (Session["UserType"].ToString() == ((int)UserTypes.HO_HOD).ToString())
            {
                string _str;
                _str = loadEmployeesReportTo();
                if (_str != "")
                {
                    repStr = repStr + " and {" + view + ".EmployeeCode} IN " + _str;
                }
                else
                {
                    repStr = repStr + " and {" + view + ".EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
                }
            }
            else if (Session["UserType"].ToString() == ((int)UserTypes.HO_EMP).ToString())
            {
                repStr = repStr + " and {" + view + ".EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
            }

        }
        else if (UserLevel == UserLevels.Region)//Region
        {
            repStr = "{" + view + ".RegionId}=" + ddl_region.SelectedValue;

            if (Session["UserType"].ToString() == ((int)UserTypes.RO_HR).ToString())
            {
                if (ddl_center.SelectedIndex>0)
                {
                    repStr = repStr + " and {" + view + ".CenterId}= " + ddl_center.SelectedValue;
                }
            }
            else if (Session["UserType"].ToString() == ((int)UserTypes.RO_HOD).ToString())
            {
                string _str;
                _str = loadEmployeesReportTo();
                if (_str != "")
                {
                    repStr = repStr + " and {" + view + ".EmployeeCode} IN " + _str;
                }
                else
                {
                    repStr = repStr + " and {" + view + ".EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
                }

            }

            else if (Session["UserType"].ToString() == ((int)UserTypes.RO_EMP).ToString())
            {
                repStr = repStr + " and {" + view + ".EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";

            }

            else
            {
                repStr = repStr+ " and {" + view + ".CenterId}=0 ";
            }
        }
        else if (UserLevel == UserLevels.Center) //Campus
        {
            repStr = "{" + view + ".Region_Id}=" + Session["RegionID"].ToString() + " and {" + view + ".Center_Id}=" + Session["CenterID"].ToString();

             if (Session["UserType"].ToString() == ((int)UserTypes.CO_HOD).ToString())
            {
                string _str;
                _str = loadEmployeesReportTo();
                if (_str != "")
                {
                    repStr = repStr + " and {" + view + ".EmployeeCode} IN " + _str;
                }
                else
                {
                    repStr = repStr + " and {" + view + ".EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
                }

            }

            else if (Session["UserType"].ToString() == ((int)UserTypes.CO_EMP).ToString())
            {
                repStr = repStr + " and {" + view + ".EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";

            }

            else
            {
                repStr = repStr +" and {" + view + ".Center_Id}=" + Session["CenterID"].ToString();
            }


        }
        if (UserLevel == UserLevels.Network) //Network
        {
            if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
            {
                repStr = "{" + view + ".Region_Id}=" + Session["RegionID"].ToString();
                if (chkemp.Checked)
                {

                    string _str;
                    //_str = loadEmployees();
                    _str = loadEmployeesReportTo();
                    if (_str != "")
                    {
                        repStr = repStr + " and {" + view + ".EmployeeCode} IN " + _str;
                    }
                    else
                    {
                        repStr = repStr + " and {" + view + ".EmployeeCode}='" + Session["EmployeeCode"].ToString() + "'";
                    }


                }
                else
                {

                    DataTable dt = new DataTable();
                    dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
                    if (dt.Rows.Count > 0)
                    {
                        repStr = repStr + " and {" + view + ".CenterId} IN [";
                        int i = 0;
                        foreach (DataRow row in dt.Rows)
                        {

                            string Center_ID = dt.Rows[i]["Center_ID"].ToString();
                            repStr = repStr + Center_ID + ",";
                            i++;
                        }
                        repStr = repStr.Substring(0, repStr.Length - 1);
                        repStr = repStr + " ]";

                    }
                }
            }
            else
            {
                repStr = "{" + view + ".Region_Id}=" + Session["RegionID"].ToString() + " and {" + view + ".CenterId}= " + ddl_center.SelectedValue;

            }
        }



        if (rbMonth.Checked)
        {
            repStr = repStr + " AND {" + view + ".PMonth}='" + ddlMonths.SelectedValue.ToString() + "'";
        }
        else if (rbRange.Checked)
        {
            //repStr = repStr + " and {vw_AttenLogRep.AttDate} in " + txtFrmDate.Text + " to " + txtToDate.Text;
            repStr = repStr + " and Date({" + view + ".AttDate})>=#" + txtFrmDate.Text + "# and Date({" + view + ".AttDate})<=#" + txtToDate.Text + "#";
        }




     
        if (chkemp.Checked && selectedEmpCount() > 0)
        {
            string _str;
            _str = loadEmployeesReportTo();
            repStr = repStr + " and {" + view + ".EmployeeCode} IN " + _str;
            //repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + lstEmployee.SelectedValue.ToString().Trim() + "'";
        }
        else if (ddlDepartment.SelectedIndex > 0 && selectedEmpCount() == 0)
        {
            repStr = repStr + " and {" + view + ".DeptCode}=" + ddlDepartment.SelectedValue.ToString();
        }

        return repStr;
    }

    protected int selectedEmpCount()
    {
        int count = 0;
        try
        {
            foreach (ListItem item in lstEmployee.Items)
            {
                if (item.Selected)
                {
                    count = count + 1;
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
        return count;
    }
    protected string loadEmployeesReportTo()
    {
        string str = "";
        if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "23" ||
            Session["UserType"].ToString() == "19" || Session["UserType"].ToString() == "22" || Session["UserType"].ToString() == "25" || Session["UserType"].ToString() == "26")
        {

            str = "[";
            string _strAll = "[";

            bool isChecked = false;
            foreach (ListItem item in lstEmployee.Items)
            {

                _strAll = _strAll + '"' + item.Value.Trim() + '"' + ",";
                if (item.Selected)
                {
                    str = str + '"' + item.Value.Trim() + '"' + ",";
                    isChecked = true;
                }
            }
            str = str.TrimEnd(',');
            str = str + "]";
            _strAll = _strAll.TrimEnd(',');
            _strAll = _strAll + "]";
            if (isChecked == false) //if no specific employee selected then select all
                str = _strAll;
        }
        else
        {

            BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

            DataTable dt = new DataTable();

            obj.ReportTo = Session["EmployeeCode"].ToString();
            dt = obj.EmplyeeReportToFetchList(obj);
            if (dt.Rows.Count > 0)
            {
                str = dt.Rows[0]["Codes"].ToString();
            }
        }
        return str;

    }


    protected void gvCenters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMoodLate"].ToString();

                //foreach (GridViewRow gvr in gvMultiEmp.Rows)
                //{
                //    cb = (CheckBox)gvr.FindControl("cbAllow");

                //    if (mood == "" || mood == "check")
                //    {
                //        cb.Checked = true;
                //        ViewState["tMoodLate"] = "uncheck";
                //    }
                //    else
                //    {
                //        cb.Checked = false;
                //        ViewState["tMoodLate"] = "check";
                //    }

                //}

            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    private void loadRegions()
    {
        DALRegion oDALRegion = new DALRegion();
        DataSet ods = new DataSet();
        ods = null;
        //int id = 0;
        //id = Convert.ToInt32(ddl_country.SelectedValue.ToString());
        ods = oDALRegion.get_RegionFromCountry(1);

        ddl_region.Items.Clear();
        ddl_region.Items.Add(new ListItem("Select", "0"));

        for (int i = 0; i <= ods.Tables[0].Rows.Count - 1; i++)
        {
            ddl_region.Items.Add(new ListItem(ods.Tables[0].Rows[i][0].ToString(), ods.Tables[0].Rows[i][6].ToString()));
        }

        ddl_center.Items.Clear();
        ddl_center.Items.Add(new ListItem("Select", "0"));
    }


    protected void ddl_region_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["RegionID"]= ddl_region.SelectedValue;
        loadCenter();
    }


    protected void ddl_center_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDepartments();

        if (Session["UserType"].ToString() == "39")
        {
            TrDept.Visible = true;
            if (Convert.ToInt32(ddl_center.SelectedValue) > 0)
            {
                ddlDepartment.Enabled = true;



            }
            //else
            //{
            //    //ddlDepartment.SelectedValue = "0";

            //    ////ddlDepartment.SelectedValue = (Session["DepartID"].ToString() == "") ? "0" : Session["DepartID"].ToString();
            //    //ddlDepartment.Enabled = false;
            //    //ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);

            //}

        }
        else if (Session["UserType"].ToString() == "19")
        {
            ddlDepartment.Enabled = true;

        }


    }

    private void loadCenter()
    {

        DALCenter oDALCenter = new DALCenter();
        DataSet oDataSet = new DataSet();
        DataTable dt = new DataTable();
        oDataSet = null;
        int id = 0;
        if(!String.IsNullOrEmpty(Session["RegionID"].ToString()))
            ddl_region.SelectedValue = Session["RegionID"].ToString();
        else
            ddl_region.SelectedValue = "0";
        if (Session["UserType"].ToString() == "22" || Session["UserType"].ToString() == "19")
        {
          
            
            if (Convert.ToString(Session["RegionID"]) != "")
            {
                id = Convert.ToInt32(Session["RegionID"].ToString());
            }
            else
            {
                id = Convert.ToInt32(ddl_region.SelectedValue.ToString());

            }

        }
        else
        {
            id = Convert.ToInt32(ddl_region.SelectedValue.ToString());
        }
        oDataSet = oDALCenter.get_CenterFromRegion(id);

        if (oDataSet.Tables[0].Rows.Count != 0)
        {
            objbase.FillDropDown(oDataSet.Tables[0], ddl_center, "Center_ID", "Center_Name");
        }
        else
        {
            ddl_center.Items.Clear();
            ddl_center.Items.Add(new ListItem("Select", "0"));
        }
        if (Session["UserType"].ToString() == "39")
        {
            dt = null;
            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            objbase.FillDropDown(dt, ddl_center, "Center_ID", "Center_Name");
        }
        loadDepartments();

    }



    protected void loadEmployees()
    {

        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

        DataTable dt = new DataTable();

        obj.ReportTo = Session["EmployeeCode"].ToString().Trim();
        obj.UserType_id = Convert.ToInt32(Session["UserType"].ToString());
        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());
        if (UserLevel == 4)
        {
            obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
            obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
        }
        else if (UserLevel == 3)
        {
            obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());

            if (Session["UserType"].ToString() == "22")
            {
                obj.Center_id = Convert.ToInt32(this.ddl_center.SelectedValue);
            }
            else
            {
                obj.Center_id = 0;
            }
        }
        if (UserLevel == 5)
        {
            obj.Region_id = Convert.ToInt32(ddl_region.SelectedValue);
            obj.Center_id = Convert.ToInt32(ddl_center.SelectedValue);
        }


        else if (UserLevel == 1 || UserLevel == 2)
        {
            if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "19")
            {
                obj.Region_id = Convert.ToInt32(this.ddl_region.SelectedValue);
                obj.Center_id = Convert.ToInt32(this.ddl_center.SelectedValue);
            }
            else
            {
                if (Convert.ToInt32(ddlDepartment.SelectedValue) > 0 && Convert.ToInt32(ddl_center.SelectedValue) > 0)
                {
                    obj.Region_id = Convert.ToInt32(this.ddl_region.SelectedValue);
                    obj.Center_id = Convert.ToInt32(this.ddl_center.SelectedValue);
                }
                else
                {
                    obj.Region_id = 0;
                    obj.Center_id = 0;
                }
            }
        }


        obj.Status_id = 2;

        if (ddlDepartment.SelectedValue == "0" && Convert.ToInt32(ddl_center.SelectedValue) > 0)
        {
            objbase.FillDropDown(dt, ddlEmployeecode, "code", "Descr");
        }
        else
        {
            obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj.PMonthDesc = ddlMonths.SelectedValue;
            dt = obj.EmplyeeReportToFetch(obj);
            objbase.FillDropDown(dt, ddlEmployeecode, "code", "Descr");
            lstEmployee.DataSource = dt;
            lstEmployee.DataTextField = "Descr";
            lstEmployee.DataValueField = "code";
            lstEmployee.DataBind();
            
        }

        //if (dt.Rows.Count > 0)
        //{
        //    ddlEmployeecode.DataTextField = "Descr";
        //    ddlEmployeecode.DataValueField = "code";
        //    ddlEmployeecode.DataSource = dt;
        //    ddlEmployeecode.DataBind();
        //}
        //else
        //{
        //    ddlEmployeecode.Items.Clear();
        //}
    }


    //protected void loadDepartments()
    //{

    //    BLLAttendance obj = new BLLAttendance();

    //    DataTable dt = new DataTable();

    //    obj.PMonthDesc = ddlMonths.SelectedValue;
    //    obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
    //    obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

    //    dt = obj.AttendanceFetchDepartments(obj);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlDepartment.DataTextField = "DeptName";
    //        ddlDepartment.DataValueField = "Deptcode";
    //        ddlDepartment.DataSource = dt;
    //        ddlDepartment.DataBind();
    //        loadEmployees();
    //    }
    //}

    protected void loadDepartments()
    {

        BLLAttendance obj = new BLLAttendance();

        DataTable dt = new DataTable();

        obj.PMonthDesc = ddlMonths.SelectedValue;

        if (Session["UserType"].ToString() == "26" || Session["UserType"].ToString() == "39")
        {
            obj.Region_Id = Convert.ToInt32(ddl_region.SelectedValue);
            obj.Center_Id = Convert.ToInt32(ddl_center.SelectedValue);

            dt = obj.AttendanceSelectDepartmentsByMonthRegionCenter(obj);
            if (ddl_center.SelectedValue == "0")
            {
                ddlDepartment.Enabled = false;
            }
        }
        else if (Session["UserType"].ToString() == "19")
        {
            obj.Region_Id = Convert.ToInt32(ddl_region.SelectedValue);
            obj.Center_Id = Convert.ToInt32(ddl_center.SelectedValue);

            dt = obj.AttendanceSelectDepartmentsByMonthRegionCenter(obj);

            ddlDepartment.Enabled = true;

        }
        else if (Session["UserType"].ToString() == "22")
        {
            obj.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            obj.Center_Id = Convert.ToInt32(ddl_center.SelectedValue);

            dt = obj.AttendanceSelectDepartmentsByMonthRegionCenter(obj);
        }
        else
        {
            obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
            obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

            dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);
            //objbase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");
        }
        objbase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");
        ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);
        //loadEmployees();
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





    protected void chkemp_CheckedChanged(object sender, EventArgs e)
    {
        if (chkemp.Checked)
        {

            if (Session["UserType"] != null)
            {
                int _part_Id = Convert.ToInt32(Session["UserType"].ToString());
                if (_part_Id == 19 || _part_Id == 22 || _part_Id == 25 || _part_Id == 26)
                {
                    TrDept.Visible = true;
                    trsingleemp.Visible = true;
                    trMultiEmp.Visible = true;
                    ddlEmployeecode.Visible = false;
                    //  loadDepartments();
                    //if (_part_Id == 19 && Convert.ToInt32(ddl_center.SelectedValue) > 0)
                    //{
                    ddlDepartment.Enabled = true;
                    // }

                }
                else if (_part_Id == 17 || _part_Id == 23 || _part_Id == 20) //HODs
                {
                    trMultiEmp.Visible = true;
                }
                else if (_part_Id == 39)
                {
                    TrDept.Visible = true;
                    if (Convert.ToInt32(ddl_center.SelectedValue) > 0)
                    {
                        ddlDepartment.Enabled = true;

                        trsingleemp.Visible = true;
                        // loadDepartments();
                    }
                    else
                    {

                        // ddlDepartment.SelectedValue = Session["DepartID"].ToString();
                        // ddlDepartment.Enabled = false;
                        // ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);

                    }


                }

                else
                {
                    trsingleemp.Visible = true;
                    TrDept.Visible = false;
                }
            }
            //loadEmployees();
        }
        else
        {
            trsingleemp.Visible = false;
            //TrDept.Visible = true;
        }
    }

    protected void rbLstRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadEmployees();
        rblCafe.SelectedIndex = -1;
        if (Convert.ToInt32(Session["UserType"].ToString()) == 17)
        {
            TrDept.Visible = false;
            chkemp.Visible = true;
            ddlDepartment.Enabled = true;
        }
        if (Convert.ToInt32(rbLstRpt.SelectedValue) >= 0)
        {
            btnViewReport.Visible = true;
            btnCafeReport.Visible = false;
        }

    }
    protected void rblCafe_SelectedIndexChanged(object sender, EventArgs e)
    {
        rbLstRpt.SelectedIndex = -1;
        chkemp.Visible = false;
        TrDept.Visible = false;
        if (rblCafe.SelectedValue == "0")
        {
            loadCafeEmployees();
            trsingleemp.Visible = true;
        }
        if (rblCafe.SelectedValue == "1")
        {
            loadEmployees();
            trsingleemp.Visible = false;

        }

        if (rblCafe.SelectedValue == "0" || rblCafe.SelectedValue == "1")
        {
            btnViewReport.Visible = false;
            btnCafeReport.Visible = true;
        }

    }
    public void loadCafeEmployees()
    {
        //get all HO ,SMART , CR,Shalimar  Employees 
        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();
        DataTable dt = obj.SelectEmployeeListforCafe(obj);
        objbase.FillDropDown(dt, ddlEmployeecode, "EmployeeCode", "FullName");

    }
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
        else
        {

            // ddlEmployeecode.Items.Clear();
            //ddlEmployeecode.Items.Add(new ListItem("Select", "0"));
        }
    }
    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDepartments();
        //if (Convert.ToInt32(ddl_center.SelectedValue) == 0)
        //{
        //    ddlDepartment.SelectedValue = (Session["DepartID"].ToString() == "") ? "0" : Session["DepartID"].ToString();
        //}

    }


    protected void lstEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < lstEmployee.Items.Count; i++)
            {
                if (lstEmployee.Items[i].Selected)
                {
                    lstEmployee.Items[i].Attributes.Add("style", "background-color: BLUE");
                }
                
            }
        }
        catch (Exception ex)
        {
        }
    }
}
