using System;
using System.Data;
using System.Web.UI.WebControls;
public partial class AttendanceReportsRO : System.Web.UI.Page
{
    DALBase objBase = new DALBase();
    BLLNetworkCenter objBLLNetwork = new BLLNetworkCenter();
    int UserLevel;
    int UserType;
    protected void Page_Load(object sender, EventArgs e)
    {



        try
        {

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            //Response.Redirect("~/PresentationLayer/ErrorPage.aspx", false);
        }

        if (!IsPostBack)
        {

            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());
            loadOrg(sender, e);
            loadMonths();
            loadDepartments();
            if (UserLevel == (int)UserLevels.Super_Admin || UserLevel == (int)UserLevels.Main_Organisation) //Head Office
            {
                ddl_MOrg.SelectedIndex = 1;
                ddl_MOrg_SelectedIndexChanged(sender, e);
                ddl_country.SelectedValue = "1";
                ddl_country_SelectedIndexChanged(sender, e);

                ddl_country.Enabled = true;
                ddl_region.Enabled = true;
                ddl_center.Enabled = true;

            }

            else if (UserLevel == (int)UserLevels.Region) //Regional Officer
            {
                int RId = Convert.ToInt32(Session["RegionID"].ToString());

                ddl_MOrg.SelectedIndex = 1;
                ddl_MOrg_SelectedIndexChanged(sender, e);

                ddl_country.SelectedValue = "1";
                ddl_country_SelectedIndexChanged(sender, e);

                ddl_region.SelectedValue = RId.ToString();
                ddl_Region_SelectedIndexChanged(sender, e);

                ddl_country.Enabled = false;
                ddl_region.Enabled = false;

                ddl_center.Enabled = true;

            }
            else if (UserLevel == (int)UserLevels.Center) //Campus Officer
            {
                int RId = Convert.ToInt32(Session["RegionID"].ToString());
                int CId = Convert.ToInt32(Session["CenterID"].ToString());

                ddl_MOrg.SelectedIndex = 1;
                ddl_MOrg_SelectedIndexChanged(sender, e);

                ddl_country.SelectedValue = "1";
                ddl_country_SelectedIndexChanged(sender, e);

                ddl_region.SelectedValue = RId.ToString();
                ddl_Region_SelectedIndexChanged(sender, e);

                ddl_center.SelectedValue = CId.ToString();
                ddl_center_SelectedIndexChanged(sender, e);

                ddl_country.Enabled = false;
                ddl_region.Enabled = false;

                ddl_center.Enabled = false;

            }
            else if (UserLevel == (int)UserLevels.Network) //Network
            {
                int RId = Convert.ToInt32(Session["RegionID"].ToString());

                ddl_MOrg.SelectedIndex = 1;
                ddl_MOrg_SelectedIndexChanged(sender, e);

                ddl_country.SelectedValue = "1";
                ddl_country_SelectedIndexChanged(sender, e);

                ddl_region.SelectedValue = RId.ToString();
                ddl_Region_SelectedIndexChanged(sender, e);

                ddl_country.Enabled = false;
                ddl_region.Enabled = false;

                ddl_center.Enabled = true;

            }
            trFrmDate.Visible = true;

        }
    }
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
        }
        else if (Session["UserType"].ToString() == "19")
        {
            obj.Region_Id = Convert.ToInt32(ddl_region.SelectedValue);
            obj.Center_Id = Convert.ToInt32(ddl_center.SelectedValue);

            dt = obj.AttendanceSelectDepartmentsByMonthRegionCenter(obj);

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
        DALBase objbase = new DALBase();
        objbase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");

        //loadEmployees();
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {

        bool _isok = false;
        string _cri = "";
        Session["isFacialMachine"] = false;
        if (Convert.ToInt32(Session["UserLevelID"]) == 5)
        {
            if (ddl_center.SelectedValue == "0")
            {
                DataTable dt = new DataTable();
                dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
                if (dt.Rows.Count > 0)
                {
                    _cri = _cri + "{TCS_EmployeeAttendanceDetailDateWise.Center_Id} IN [";
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {

                        string Center_ID = dt.Rows[i]["Center_ID"].ToString();
                        _cri = _cri + Center_ID + ",";
                        i++;
                    }
                    _cri = _cri.Substring(0, _cri.Length - 1);
                    _cri = _cri + " ] And ";

                }
            }
            else
            {

                _cri = "{TCS_EmployeeAttendanceDetailDateWise.Center_Id}=" + ddl_center.SelectedValue + " and ";

            }
        }

        switch (rblReportType.SelectedValue)
        {
            case "1": //Monthly Attendance Summary Campus Wise
                {
                    Session["RptTitle"] = "Monthly Attendance Summary Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_CO_MonthlyEmployeeAttendanceReportCenterWiseList.rpt");
                    Session["rep"] = "TCS_CO_MonthlyEmployeeAttendanceReportCenterWiseList.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;
                    break;
                }
            case "2"://Monthly Attendance Summary Region Wise
                {
                    Session["RptTitle"] = "Monthly Attendance Summary Region Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWiseBAR.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWiseBAR.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "3"://Monthly Presents Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Presents Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIEPresents.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIEPresents.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "4"://Monthly Absent Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Absent Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIEPIEAbsents.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIEPIEAbsents.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "5"://Monthly Late Arrival Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Late Arrival Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIELate.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIELate.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "6"://Monthly Missing In Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Missing In Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIEMissingIn.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIEMissingIn.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "7"://Monthly Missing Out Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Missing Out Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIEMissingOut.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIEMissingOut.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "8"://Daily Attendance Summary Campus Wise
                {
                    Session["RptTitle"] = "Daily Attendance Summary Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_CO_DailyEmployeeAttendanceReportCenterWiseList.rpt");
                    Session["rep"] = "TCS_CO_DailyEmployeeAttendanceReportCenterWiseList.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "9"://Daily Attendance Summary Region Wise
                {
                    Session["RptTitle"] = "Daily Attendance Summary Region Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWiseBAR.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWiseBAR.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "10"://Daily Presents Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Presents Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIEPresents.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIEPresents.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "11"://Daily Absent Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Absent Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIEPIEAbsents.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIEPIEAbsents.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "12"://Daily Late Arrival Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Late Arrival Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIELate.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIELate.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "13"://Daily Missing In Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Missing In Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIEMissingIn.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIEMissingIn.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "14"://Daily Missing Out Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Missing Out Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIEMissingOut.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIEMissingOut.rpt";
                    _cri = _cri + SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;
                    break;
                }
            //case "15"://Daily Missing Out Comparison Campus Wise
            //    {
            //        Session["RptTitle"] = "Monthly Pending Attendance Submission & Approvals";
            //        Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_PendingAttendanceStatusReport.rpt");
            //        Session["rep"] = "TCS_RO_PendingAttendanceStatusReport.rpt";
            //        _cri = SelectCriteria(_cri, "TCS_RO_PendingAttendanceStatusReport");
            //        _isok = true;
            //        break;
            //    }
            case "16"://Daily Missing Out Comparison Campus Wise
                {
                    Session["RptTitle"] = "Attendance Summary";
                    Session["reppath"] = Server.MapPath("~/Reports/rptAttendanceSummaryDetailed.rpt");
                    Session["rep"] = "rptAttendanceSummaryDetailed.rpt";
                    _cri = SelectCriteria(_cri, "vw_AttendanceRep");
                    _isok = true;
                    break;
                }
            case "17"://Facial Machine Attendance Summary
                {

                    Session["RptTitle"] = "Facial Machine Attendance Summary";
                    Session["reppath"] = Server.MapPath("~/Reports/FacialMachineReport.rpt");
                    Session["rep"] = "FacialMachineReport.rpt";
                    _cri = SelectCriteria(_cri, "vw_AttendanceReportSummary");
                    Session["isFacialMachine"] = true;
                    _isok = true;
                    break;
                }
            case "18"://Facial Machine Attendance Device Details 
                {

                    Session["RptTitle"] = "Facial Machine Device Details ";
                    Session["reppath"] = Server.MapPath("~/Reports/FacialMachineDetails.rpt");
                    Session["rep"] = "FacialMachineReport.rpt";
                    _cri = SelectCriteria(_cri, "vw_MachinesStatusReport");
                    Session["isFacialMachine"] = true;
                    _isok = true;
                    break;
                }
            case "19"://Facial Machine Attendance Device Details 
                {

                    Session["RptTitle"] = "Facial Machine Admin Summary ";
                    Session["reppath"] = Server.MapPath("~/Reports/FacialMachineAdminSummary.rpt");
                    Session["rep"] = "FacialMachineAdminSummary.rpt";
                    _cri = SelectCriteria(_cri, "vw_AdminList");
                    Session["isFacialMachine"] = true;
                    _isok = true;
                    break;
                }
            case "20"://Facial Machine Attendance Device Details 
                {

                    Session["RptTitle"] = "Facial Machine Admin List ";
                    Session["reppath"] = Server.MapPath("~/Reports/FacialMachineAdminList.rpt");
                    Session["rep"] = "FacialMachineAdminList.rpt";
                    _cri = SelectCriteria(_cri, "vw_AdminList");
                    Session["isFacialMachine"] = true;
                    _isok = true;
                    break;
                }
        }
        Session["LastPage"] = "~/AttendanceReportsRO.aspx";
        Session["CriteriaRpt"] = _cri;

        if (_isok == true)
        {
            Response.Redirect("~/TssCrystalReports.aspx");
        }

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

    protected string SelectCriteria(string _cri, string _view)
    {



        if (ddl_MOrg.SelectedIndex > 0)
        {
            if (rblReportType.SelectedValue != "16")
                _cri = "{" + _view + ".Main_Organisation_Id}=" + ddl_MOrg.SelectedValue;
        }
        if (ddl_region.SelectedIndex > 0)
        {
            if (rblReportType.SelectedValue != "16")
                _cri = _cri + " and {" + _view + ".Region_Id}=" + ddl_region.SelectedValue;
            if (rblReportType.SelectedValue == "16")
                _cri = _cri + "  {" + _view + ".Region_Id}=" + ddl_region.SelectedValue;
        }
        if (ddl_center.SelectedIndex > 0)
        {
            _cri = _cri + " and {" + _view + ".Center_Id}=" + ddl_center.SelectedValue;
        }

        if (ddlMonths.SelectedIndex > 0)
        {
            if (Convert.ToInt32(rblReportType.SelectedValue )>= 18)
            {

            }
            else
            {
                if (rblReportType.SelectedValue != "16")
                    _cri = _cri + " and {" + _view + ".PMonthDesc}='" + ddlMonths.SelectedValue.Trim() + "'";
                if (rblReportType.SelectedValue == "16")
                    _cri = _cri + " and  {" + _view + ".PMonth}='" + ddlMonths.SelectedValue.Trim() + "'";

            }

        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            if (rblReportType.SelectedValue == "16")
                _cri = _cri + " and {" + _view + ".DeptCode}=" + ddlDepartment.SelectedValue + "";
        }
        if (txtFrmDate.Text.Length > 0)
        {
            _cri = _cri + " and Date({" + _view + ".AttDate})>=#" + txtFrmDate.Text + "#";
        }

        return _cri;

    }



    protected void UpdatePanel1_PreRender(object sender, EventArgs e)
    {
        try
        {
            TreeView tempView = (TreeView)Master.FindControl("MenuLeft");
            TreeNode tn = tempView.FindNode("Create");
            if (tn != null)
            {
                tn.Expand();
                tn.ChildNodes[0].Select();
            }
            tn = tempView.FindNode("Reports");
            if (tn != null)
            {
                tn.Expand();
                //tn.ChildNodes[5].Select();
            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            //        Response.Redirect("~/PresentationLayer/ErrorPage.aspx", false);
        }
    }

    protected void ddl_Region_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCenter();
    }

    protected void ddl_MOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCountries();
    }

    protected void ddl_country_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadRegions();
    }

    protected void loadOrg(object sender, EventArgs e)
    {
        DALMainOrgnization oDALMainOrgnization = new DALMainOrgnization();
        DataSet ods = new DataSet();
        ods = null;
        ods = oDALMainOrgnization.get_MainOrgnizations();

        if (ods.Tables[0].Rows.Count != 0)
        {
            objBase.FillDropDown(ods.Tables[0], ddl_MOrg, "Main_Organisation_Id", "Main_Organisation_Name");
        }

        ddl_country.Items.Clear();
        ddl_country.Items.Add(new ListItem("Select", "0"));

        ddl_region.Items.Clear();
        ddl_region.Items.Add(new ListItem("Select", "0"));

        ddl_center.Items.Clear();
        ddl_center.Items.Add(new ListItem("Select", "0"));

    }

    protected void loadCountries()
    {
        DALMainOrgCountry oDALMainOrgCountry = new DALMainOrgCountry();
        DataSet ods = new DataSet();
        ods = null;
        int a = Convert.ToInt32(ddl_MOrg.SelectedValue.ToString());
        ods = oDALMainOrgCountry.get_CountriesForOrg(a);

        ddl_country.Items.Clear();
        ddl_country.Items.Add(new ListItem("Select", "0"));

        for (int i = 0; i <= ods.Tables[0].Rows.Count - 1; i++)
        {
            ddl_country.Items.Add(new ListItem(ods.Tables[0].Rows[i][2].ToString(), ods.Tables[0].Rows[i][0].ToString()));

        }

        ddl_region.Items.Clear();
        ddl_region.Items.Add(new ListItem("Select", "0"));

        ddl_center.Items.Clear();
        ddl_center.Items.Add(new ListItem("Select", "0"));


    }

    private void loadRegions()
    {
        DALRegion oDALRegion = new DALRegion();
        DataSet ods = new DataSet();
        ods = null;
        int id = 0;
        id = Convert.ToInt32(ddl_country.SelectedValue.ToString());
        ods = oDALRegion.get_RegionFromCountry(id);

        ddl_region.Items.Clear();
        ddl_region.Items.Add(new ListItem("Select", "0"));

        for (int i = 0; i <= ods.Tables[0].Rows.Count - 1; i++)
        {
            ddl_region.Items.Add(new ListItem(ods.Tables[0].Rows[i][0].ToString(), ods.Tables[0].Rows[i][6].ToString()));

        }

        ddl_center.Items.Clear();
        ddl_center.Items.Add(new ListItem("Select", "0"));
    }

    private void loadCenter()
    {
        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());
        DataTable dt = new DataTable();

        DALCenter oDALCenter = new DALCenter();
        DataSet oDataSet = new DataSet();
        oDataSet = null;
        int id = 0;
        id = Convert.ToInt32(ddl_region.SelectedValue.ToString());
        if (UserLevel == 5)
        {

            dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
            objBase.FillDropDown(dt, ddl_center, "Center_ID", "Center_Name");
        }
        else
        {
            oDataSet = oDALCenter.get_CenterFromRegion(id);
            objBase.FillDropDown(oDataSet.Tables[0], ddl_center, "Center_ID", "Center_Name");


        }
    }

    protected void ddl_center_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblReportType.SelectedValue == "1") //Periodic Student Registration
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = true;
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }

        else if (rblReportType.SelectedValue == "2") //Student Registration Class Wise
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "3") //Unassigned Students
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "4") //Assigned Student Class Wise
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "5") //Assigned Student Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "6") //Student Strengh Subject Wise
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "7") //Student Strengh Subject Wise
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "8") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = true;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "9") //Student Strengh Subject Wise
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = true;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "10") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = true;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "11") //Student Strengh Subject Wise
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = true;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "12") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = true;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "13") //Student Strengh Subject Wise
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = true;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "14") //Student Strengh Subject Wise
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = true;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "15") //Monthly Pending Attendance Submission & Approvals
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "16") //Monthly Pending Attendance Submission & Approvals
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = false;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = true;
        }
        else if (rblReportType.SelectedValue == "17") //Facial Machine Report 
        {
            TrMonth.Visible = true;
            trRegion.Visible = true;
            trCenter.Visible = true;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "18") //Facial Machine Report 
        {
            TrMonth.Visible = false;
            trRegion.Visible = true;
            trCenter.Visible = true;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "19") //Facial Machine Admin Summary Report 
        {
            TrMonth.Visible = false;
            trRegion.Visible = true;
            trCenter.Visible = true;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "20") //Facial Machine Admin Summary Report 
        {
            TrMonth.Visible = false;
            trRegion.Visible = true;
            trCenter.Visible = true;
            ddl_center.SelectedValue = "0";
            trFrmDate.Visible = false;
            trDept.Visible = false;
        }
    }

}
