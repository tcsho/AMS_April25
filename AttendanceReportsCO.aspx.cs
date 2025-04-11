using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AttendanceReportsCO : System.Web.UI.Page
{
    DALBase objBase = new DALBase();
    BLLNetworkCenter objBLLNetwork = new BLLNetworkCenter();
    int UserLevel;
    int UserType;

    BLLLmsAppReports objLmsAppReports = new BLLLmsAppReports();

    protected void Page_Load(object sender, EventArgs e)
    {



        try
        {
            


        if (!IsPostBack)
        {
            loadReprts();
            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());
            loadOrg(sender, e);
            loadMonths();
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


                //dt = null;
                //dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
                //objbase.FillDropDown(dt, ddl_center, "Center_ID", "Center_Name");
            }



                string rblselected = rblReportType.SelectedValue;
                int id = Convert.ToInt32(rblselected);
                FillReportControls(id);

            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            //Response.Redirect("~/PresentationLayer/ErrorPage.aspx", false);
        }
    }

    protected void btnViewReport_Click(object sender, EventArgs e)
    {


        try
        {
            bool _isok = false;
            string _cri = "";

            DataTable dt = (DataTable)ViewState["Reports"];

            string rblselected = rblReportType.SelectedItem.Text;

            DataRow[] row = dt.Select("rpt_Caption='" + rblselected + "'");

            Session["RptTitle"] = row[0]["rpt_Caption"].ToString();
            Session["reppath"] = Server.MapPath(row[0]["Rpt_Path"].ToString());
            Session["rep"] = row[0]["Rpt_Name"].ToString();

            _cri = SelectCriteria(_cri, row[0]["Rpt_View"].ToString());

            _isok = true;
            Session["CriteriaRpt"] = _cri;

            Session["LastPage"] = "~/AttendanceReportsCO.aspx";

            if (_isok == true)
            {
                Response.Redirect("~/TssCrystalReports.aspx", false);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
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
            _cri = "{" + _view + ".Main_Organisation_Id}=" + ddl_MOrg.SelectedValue;
        }
        else
        {
            _cri = "{" + _view + ".Main_Organisation_Id}=1";
        }
        if (ddl_region.SelectedIndex > 0)
        {
            _cri = _cri + " and {" + _view + ".Region_Id}=" + ddl_region.SelectedValue;
        }
        else
        {
            _cri = _cri + " and {" + _view + ".Region_Id}=0";
        }
        if (ddl_center.SelectedIndex > 0)
        {
            _cri = _cri + " and {" + _view + ".Center_Id}=" + ddl_center.SelectedValue;
        }
        else
        {
            _cri = _cri + " and {" + _view + ".Center_Id}=0";
        }
 
        if (ddlMonths.SelectedIndex > 0 )
        {
            _cri = _cri + " and {" + _view + ".PMonth}='" + ddlMonths.SelectedValue + "'";
        }
        else
        {
            _cri = _cri + " and {" + _view + ".PMonth}='" + Session["CurrentMonth"].ToString() + "'";
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
       // obj.ReportTo = Session["EmployeeCode"].ToString().Trim();
        //obj.UserType_id = Convert.ToInt32(Session["UserType"].ToString());

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());

        DALCenter oDALCenter = new DALCenter();
        DataSet oDataSet = new DataSet();
        DataTable dt = new DataTable();
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


        string rblselected = rblReportType.SelectedValue;
        int id = Convert.ToInt32(rblselected);
        FillReportControls(id);
       
    }


    private string NetworkSelectCriteria(string _cri)
    {

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
            

            
        }
        return _cri;
    }

    private string NetworkMonthlyTOERP(string _cri)
    {

        if (Convert.ToInt32(Session["UserLevelID"]) == 5)
        {
            if (ddl_center.SelectedValue == "0")
            {
                DataTable dt = new DataTable();
                dt = objBLLNetwork.NetworkCenterSelectByNetworkHOD(Convert.ToInt32(Session["UserName"]));
                if (dt.Rows.Count > 0)
                {
                    _cri = _cri + "{vw_GetLeavesSubmittedToERP.Center_Id} IN [";
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
            
        }

        return _cri;
    
    }


    protected void loadReprts()
    {
        try
        {
            string queryStr = Request.QueryString["id"];

            DataTable dt = new DataTable();

            //objLmsAppReports.Page_Id = Convert.ToInt32(queryStr);
            objLmsAppReports.Page_Id = 27;
            dt = objLmsAppReports.LmsAppReportsFetch(objLmsAppReports);
            ViewState["Reports"] = dt;
            rblReportType.DataSource = dt;
            rblReportType.DataValueField = "Rpt_Id";
            rblReportType.DataTextField = "Rpt_Caption";
            rblReportType.SelectedIndex = 0;
            rblReportType.DataBind();



        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }



    }

    private void FillReportControls(int id)
    {
        try
        {
            string str;
            bool isshow;
            DataTable dt1 = new DataTable();
            objLmsAppReports.Rpt_Id = id;
            dt1 = objLmsAppReports.FetchLmsAppReportsControlsbyRpt_Id(objLmsAppReports);
            System.Web.UI.HtmlControls.HtmlTableRow trrow;

            Control cnt;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("MainContent");

                if (mpContentPlaceHolder != null)
                {
                    str = dt1.Rows[i]["Name"].ToString();
                    isshow = Convert.ToBoolean(dt1.Rows[i]["isshow"].ToString());
                    cnt = (Control)mpContentPlaceHolder.FindControl(str);
                    cnt.Visible = false;
                    cnt.Visible = isshow;
                }

            }

            ClearControlGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }

    }

    protected void ClearControlGrid()
    {
        //DataTable dtclear = null;

        //UIGridRegion.SetData(dtclear);
        //UIGridCenter.SetData(dtclear);
        //UIGridSession.SetData(dtclear);
        //UIGridClass.SetData(dtclear);
        //UiGridSubject.SetData(dtclear);
        //UIGridTerm.SetData(dtclear);
        //UIGridGrade.SetData(dtclear);



    }
}
