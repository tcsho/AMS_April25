using System;
using System.Data;
using System.Web.UI.WebControls;


public partial class AttendanceReportsHO : System.Web.UI.Page
{

    DALBase objBase = new DALBase();

    protected void Page_Load(object sender, EventArgs e)
    {

        int UserLevel;
        int UserType;

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
            if (UserLevel == 1 || UserLevel == 2) //Head Office
            {
                ddl_MOrg.SelectedIndex = 1;
                ddl_MOrg_SelectedIndexChanged(sender, e);

                ddl_country.SelectedValue = "1";
                ddl_country_SelectedIndexChanged(sender, e);

                ddl_country.Enabled = true;
                ddl_region.Enabled = true;
                ddl_center.Enabled = true;

            }

            else if (UserLevel == 3) //Regional Officer
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
            else if (UserLevel == 4) //Campus Officer
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
            trFrmDate.Visible = true;
            divDept.Visible = false;
        }
    }

    protected void btnViewReport_Click(object sender, EventArgs e)
    {

        bool _isok = false;
        string _cri = "";

        switch (rblReportType.SelectedValue)
        {
            case "1": //Monthly Attendance Summary Campus Wise
                {
                    Session["RptTitle"] = "Monthly Attendance Summary Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_CO_MonthlyEmployeeAttendanceReportCenterWiseList.rpt");
                    Session["rep"] = "TCS_CO_MonthlyEmployeeAttendanceReportCenterWiseList.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;
                    break;
                }
            case "2"://Monthly Attendance Summary Region Wise
                {
                    Session["RptTitle"] = "Monthly Attendance Summary Region Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWiseBAR.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWiseBAR.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "3"://Monthly Presents Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Presents Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIEPresents.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIEPresents.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "4"://Monthly Absent Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Absent Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIEPIEAbsents.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIEPIEAbsents.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "5"://Monthly Late Arrival Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Late Arrival Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIELate.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIELate.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "6"://Monthly Missing In Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Missing In Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIEMissingIn.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIEMissingIn.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "7"://Monthly Missing Out Comparison Campus Wise
                {
                    Session["RptTitle"] = "Monthly Missing Out Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_MonthlyRegionComparisonReportCenterWisePIEMissingOut.rpt");
                    Session["rep"] = "TCS_RO_MonthlyRegionComparisonReportCenterWisePIEMissingOut.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "8"://Daily Attendance Summary Campus Wise
                {
                    Session["RptTitle"] = "Daily Attendance Summary Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_CO_DailyEmployeeAttendanceReportCenterWiseList.rpt");
                    Session["rep"] = "TCS_CO_DailyEmployeeAttendanceReportCenterWiseList.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "9"://Daily Attendance Summary Region Wise
                {
                    Session["RptTitle"] = "Daily Attendance Summary Region Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWiseBAR.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWiseBAR.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "10"://Daily Presents Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Presents Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIEPresents.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIEPresents.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "11"://Daily Absent Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Absent Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIEPIEAbsents.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIEPIEAbsents.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "12"://Daily Late Arrival Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Late Arrival Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIELate.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIELate.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "13"://Daily Missing In Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Missing In Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIEMissingIn.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIEMissingIn.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;

                    break;
                }
            case "14"://Daily Missing Out Comparison Campus Wise
                {
                    Session["RptTitle"] = "Daily Missing Out Comparison Campus Wise";
                    Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_DailyRegionComparisonReportCenterWisePIEMissingOut.rpt");
                    Session["rep"] = "TCS_RO_DailyRegionComparisonReportCenterWisePIEMissingOut.rpt";
                    _cri = SelectCriteria(_cri, "TCS_EmployeeAttendanceDetailDateWise");
                    _isok = true;
                    break;
                }
            case "15"://Detailed Attendance Summary Department wise
                {
                    Session["RptTitle"] = "Attendance Summary";
                    Session["reppath"] = Server.MapPath("~/Reports/rptAttendanceSummaryDetailed.rpt");
                    Session["rep"] = "rptAttendanceSummaryDetailed.rpt";
                    _cri = SelectCriteria(_cri, "vw_AttendanceRep");
                    _isok = true;
                    break;
                }
        }
        Session["LastPage"] = "~/AttendanceReportsHO.aspx";
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
            if(rblReportType.SelectedValue!="15")
                _cri = "{" + _view + ".Main_Organisation_Id}=" + ddl_MOrg.SelectedValue;
        }
        if (ddl_region.SelectedIndex > 0)
        {
            _cri = _cri + " and {" + _view + ".Region_Id}=" + ddl_region.SelectedValue;
        }
        if (ddl_center.SelectedIndex > 0)
        {
            _cri = _cri + " and {" + _view + ".Center_Id}=" + ddl_center.SelectedValue;
        }

        if (ddlMonths.SelectedIndex > 0)
        {
            if (rblReportType.SelectedValue != "15")
                _cri = _cri + " and {" + _view + ".PMonthDesc}='" + ddlMonths.SelectedValue + "'";
             if(rblReportType.SelectedValue=="15")
                _cri = _cri + "  {" + _view + ".PMonth}='" + ddlMonths.SelectedValue + "'";
        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            if (rblReportType.SelectedValue == "15")
                _cri = _cri + " and {" + _view + ".DeptCode}=" + ddlDepartment.SelectedValue + "";
        }

        if (txtFrmDate.Text.Length > 0)
        {
            _cri = _cri + " and Date({" + _view + ".AttDate})=#" + txtFrmDate.Text + "#";
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

        DALCenter oDALCenter = new DALCenter();
        DataSet oDataSet = new DataSet();
        oDataSet = null;
        int id = 0;
        id = Convert.ToInt32(ddl_region.SelectedValue.ToString());

        oDataSet = oDALCenter.get_CenterFromRegion(id);

        if (oDataSet.Tables[0].Rows.Count != 0)
        {
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
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = false;
            divDept.Visible = false;
        }

        else if (rblReportType.SelectedValue == "2") //Student Registration Class Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = false;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "3") //Unassigned Students
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = false;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "4") //Assigned Student Class Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = false;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "5") //Assigned Student Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = false;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "6") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = false;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "7") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = false;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "8") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = true;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "9") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = true;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "10") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = true;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "11") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = true;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "12") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = true;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "13") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = true;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "14") //Student Strengh Subject Wise
        {
            trRegion.Visible = true;
            trCenter.Visible = false;
            trFrmDate.Visible = true;
            divDept.Visible = false;
        }
        else if (rblReportType.SelectedValue == "15") //Attedance Summary Department Wise 
        {
            trRegion.Visible = true;
            trCenter.Visible = true;
            trFrmDate.Visible = false;
            divDept.Visible = true;
        }
    }

}
