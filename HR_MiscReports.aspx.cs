using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class HR_MiscReports : System.Web.UI.Page
{
    DALBase objBase = new DALBase();
    BLLNetworkCenter objBLLNetwork = new BLLNetworkCenter();
    int UserLevel, UserType;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["EmployeeCode"] == null)
            {
                Response.Redirect("~/login.aspx", false);
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









                    rbLstRpt.SelectedValue = "1";
                    rbLstRpt_SelectedIndexChanged(this, EventArgs.Empty);




                }
                catch (Exception ex)
                {
                    Session["error"] = ex.Message;
                    Response.Redirect("ErrorPage.aspx", false);
                }

            }

        }
        else
        {
            Response.Redirect("~/login.aspx", false);

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

        int UserLevel, UserType;

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());

        string repStr = "";
        if (rbLstRpt.SelectedValue == "0")
        {

            Session["reppath"] = "Reports\\rpt_LWOP_full_half_rep.rpt";
            Session["rep"] = "rpt_LWOP_full_half_rep.rpt";


            #region 'Attendance Report'



            if (UserLevel == 4) //Campus
            {
                repStr = "{vw_LWOP_full_half_rep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_LWOP_full_half_rep.Center_Id}=" + Session["CenterID"].ToString();
            }
            else if (UserLevel == 3)//Region
            {
                repStr = "{vw_LWOP_full_half_rep.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_LWOP_full_half_rep.Center_Id}=0 ";

            }
            else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            {
                repStr = "{vw_LWOP_full_half_rep.Region_Id} = 0 and {vw_LWOP_full_half_rep.Center_Id} = 0";

            }

            //if (rbMonth.Checked)
            //{
            //    repStr = repStr + " AND {vw_LWOP_full_half_rep.PMonthDesc}='" + ddlMonths.SelectedValue.ToString() + "'";
            //}
            //else if (rbRange.Checked)
            //{
            //    repStr = repStr + "and Date({vw_LWOP_full_half_rep.AttDate})>=#" + txtFrmDate.Text + "# and Date({vw_LWOP_full_half_rep.AttDate})<=#" + txtToDate.Text + "#";
            //}



            #endregion



        }
        else if (rbLstRpt.SelectedValue == "1")
        {

            Session["reppath"] = "Reports\\rpt_Employees_Without_ReportingLine.rpt";
            Session["rep"] = "rpt_Employees_Without_ReportingLine.rpt";


            #region 'Employees_Without_ReportingLine'



            if (UserLevel == 4) //Campus
            {
                repStr = "{vw_Employees_Without_ReportingLine.Region_Id}=" + Session["RegionID"].ToString() + " and {vw_Employees_Without_ReportingLine.Center_Id}=" + Session["CenterID"].ToString();
            }
            else if (UserLevel == 3)//Region
            {
                //regional office and all campuses 
                repStr = "{vw_Employees_Without_ReportingLine.Region_Id}=" + Session["RegionID"].ToString();
                //regional office only

            }





            #endregion



        }
        else if (rbLstRpt.SelectedValue == "2")
        {
            Session["reppath"] = "Reports\\rptReportingLineDeptWise.rpt";
            Session["rep"] = "rptReportingLineDeptWise.rpt";

            string region_id = ddl_region.SelectedValue.ToString();
            string center_id = ddl_center.SelectedValue.ToString();
            string dept_id = ddlDepartment.SelectedValue.ToString();

            //repStr = "{vw_EmployeeReportingLine.Region_Id}=" + region_id + " and {vw_EmployeeReportingLine.Center_Id}=" + Session["CenterID"].ToString();
            if (region_id.Trim().Length > 0)
                repStr = "{vw_EmployeeReportingLine.Region_Id}=" + region_id;
            if (center_id != "0")
            {
                if (repStr.Trim().Length > 1)
                    repStr = repStr + " and ";
                repStr = repStr + "{vw_EmployeeReportingLine.center_id}=" + center_id;

            }
            if (ddlDepartment.SelectedIndex > 0)
            {
                if (repStr.Trim().Length > 1)
                    repStr = repStr + " and ";
                repStr = repStr + "{vw_EmployeeReportingLine.DeptCode}=" + dept_id;
            }
        }

        Session["CriteriaRpt"] = repStr;
        Session["LastPage"] = "~/HR_MiscReports.aspx"; // +ViewState["rptmode"].ToString();
        Response.Redirect("~/rptAllReports.aspx");
    }







    protected void rbLstRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (rbLstRpt.SelectedValue)
        {
            case "0":
                {
                    trMonth.Attributes.CssStyle.Add("display", "inline");
                    trMonth.Visible = true;
 

                    break;
                }
            case "2":
                {
                    trRegion.Attributes.CssStyle.Add("display", "inline");
                    break;
                }

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

}