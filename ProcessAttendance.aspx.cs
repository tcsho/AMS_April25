using System;
using System.Data;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;

public partial class ProcessAttendance : System.Web.UI.Page
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
            //======== Page  Access Settings ========================
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

            int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

            //int _result = objbase.ApplicationSettings(sRet, _part_Id);


            //if (_result == 1)
            //    {
            if (!IsPostBack)
            {
                try
                {
                    this.ddlMonths.Enabled = false;
                    loadMonths();
                    loadRegions();


                    String s = Session["UserType"].ToString();


                    if (Session["UserType"].ToString() == ((int)UserTypes.Audit).ToString() || Session["UserType"].ToString() == ((int)UserTypes.HO_HR).ToString())
                    {
                        ddl_region.Enabled = true;
                        ddl_center.Enabled = true;

                    }
                    else if (Session["UserType"].ToString() == ((int)UserTypes.RO_HR).ToString())
                    {
                        ddl_region.Enabled = false;
                        ddl_region.SelectedValue = Session["RegionID"].ToString();
                        ddl_region_SelectedIndexChanged(sender,e);

                        ddl_center.Enabled = true;


                    }
                    else if (Session["UserType"].ToString() == ((int)UserTypes.CO_HR).ToString())
                    {
                        ddl_region.Enabled = false;
                        ddl_region.SelectedValue = Session["RegionID"].ToString();
                        ddl_region_SelectedIndexChanged(sender, e);

                        ddl_center.Enabled = false;
                        ddl_center.SelectedValue = Session["CenterID"].ToString();
   

                    }
                    else
                    {
                        ddl_region.Enabled = false;
                        ddl_center.Enabled = false;
                    }
                    if (Session["UserType"].ToString() == "39")
                    {
                        div_region.Visible = false;
                        ddl_region.Enabled = false;
                        div_center.Visible = true;
                     
                        ddl_region.SelectedValue = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();
                        loadCenter();

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
    private void loadRegions()
    {
        DALRegion oDALRegion = new DALRegion();
        DataSet ods = new DataSet();
        ods = null;
 
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
    protected void startProcess()
    {
        lblStatus.Text = "";

        BLLAttendance bllobj = new BLLAttendance();

        bllobj.PMonthDesc = ddlMonths.SelectedValue;



        int UserLevel;
        int UserType;

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());


        if (UserLevel == ((int)UserLevels.Center)) //Campus
        {
            if (ddl_region.SelectedIndex > 0 && ddl_center.SelectedIndex > 0)
            {
                bllobj.Region_Id = Convert.ToInt32(ddl_region.SelectedValue);
                bllobj.Center_Id = Convert.ToInt32(ddl_center.SelectedValue);
            }
        }
        else if (UserLevel == ((int)UserLevels.Region))//Region
        {
            if (ddl_region.SelectedIndex > 0 && ddl_center.SelectedIndex > 0)
            {
                bllobj.Region_Id = Convert.ToInt32(ddl_region.SelectedValue);
                bllobj.Center_Id = Convert.ToInt32(ddl_center.SelectedValue);
            }
            else
            {
                bllobj.Region_Id = Convert.ToInt32(ddl_region.SelectedValue);
                bllobj.Center_Id = 0;
            }
        }
        else if (UserLevel == ((int)UserLevels.Super_Admin) || UserLevel == (int)UserLevels.Main_Organisation) //Head Office + Admin
        {
            if (ddl_region.SelectedIndex>0 && ddl_center.SelectedIndex>0)
            {
                bllobj.Region_Id = Convert.ToInt32(ddl_region.SelectedValue);
                bllobj.Center_Id = Convert.ToInt32(ddl_center.SelectedValue);
            }
            else if (ddl_region.SelectedIndex>0 && ddl_center.SelectedIndex<=0)
            {
                bllobj.Region_Id = Convert.ToInt32(ddl_region.SelectedValue);
                bllobj.Center_Id = 0;
            }
            else
            {
                bllobj.Region_Id = 0;
                bllobj.Center_Id = 0;
            }


        }

        BLLAttendance objAttendance = new BLLAttendance();
        int AlreadyIn = 0;

        objAttendance.Region_Id = bllobj.Region_Id;
        objAttendance.Center_Id = bllobj.Center_Id;

        objAttendance.PMonthDesc = ddlMonths.SelectedValue;

        DataTable dtbl = objAttendance.ERP_Final_Process_HistorySelectMonth(objAttendance);


        if (dtbl.Rows.Count > 0)
        {
            ImpromptuHelper.ShowPrompt("ERP Process has been run for the month so you can not run the process!");
            
            return;
        }

        AlreadyIn = bllobj.AttendanceProcess(bllobj);

        lblStatus.Text = "Attendance process successfully completed.";
        if (AlreadyIn > 0)
        {
            drawMsgBox("Attendance process successfully completed.", 1);
            bllobj.AttendanceProccessLogAdd(bllobj); 
            ViewState["dtLib"] = null;
        }
        else if (AlreadyIn == 0)
        {
            drawMsgBox("Error in attendance process!", 2);
        }

    }
    protected void drawMsgBox(string msg, int errType)
    {
        try
        {
            ImpromptuHelper.ShowPrompt(msg);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        startProcess();
    }


    protected void ddl_region_SelectedIndexChanged(object sender, EventArgs e)
    {
 
        loadCenter();
    }
    private void loadCenter()
    {

        DALCenter oDALCenter = new DALCenter();
        DataSet oDataSet = new DataSet();
        DataTable dt = new DataTable();
        oDataSet = null;
        int id = 0;
 
        if (Session["UserType"].ToString() == ((int)UserTypes.RO_HR).ToString() || Session["UserType"].ToString() == ((int)UserTypes.HO_HR).ToString())
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
       
    }

}
