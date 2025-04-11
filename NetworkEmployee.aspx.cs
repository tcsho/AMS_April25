using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class NetworkEmployee : System.Web.UI.Page
{
    BLLNetworkTeam objBllTeam = new BLLNetworkTeam();
    BLLNetworkCenter objBllCenter = new BLLNetworkCenter();
    BLLNetworkRegion objBllRegion = new BLLNetworkRegion();
    BLLEmployeeReportTo objBllReportTo = new BLLEmployeeReportTo();
    

    DALBase objBase = new DALBase();

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
        catch (Exception)
        {
        }

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

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType =  Convert.ToInt32(Session["UserType"].ToString());

        if (!IsPostBack)
        {



            Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
            pan_AssigCampus.Attributes.CssStyle.Add("display", "none");
           
            ViewState["MarkEmployee"] = "check";
            ViewState["MarkCampus"] = "check";
            ViewState["Region_Id"] = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();
          


            loadRegions();
            loadCenters();

            load_region_dept();
           

            setRightsControls();
            ///Main Grid
            BindGridNetwork();

          






        }


    }

    public void setRightsControls()
    {
        if (UserLevel == 4)
        {
            ddlRegion.Enabled = false;
            ddlRegions.Enabled = false;
            ddlCenter.Enabled = false;

            ddlRegion.SelectedValue = Session["RegionID"].ToString();
            ddlCenter.SelectedValue = Session["CenterID"].ToString();


            ddl_region_dept.Enabled = false;
            ddl_center_dept.Enabled = false;

            ddl_region_dept.SelectedValue = Session["RegionID"].ToString();
            ddl_center_dept.SelectedValue = Session["CenterID"].ToString();
        }
        else if (UserLevel == 3)
        {
            ddlRegion.Enabled = false;
            ddlRegions.Enabled = false;
            ddlCenter.Enabled = true;

            ddlRegion.SelectedValue = Session["RegionID"].ToString();

            ddl_region_dept.Enabled = false;
            ddl_center_dept.Enabled = true;
            ddl_RegionTeam.Enabled = false;
            ddl_Networks.Enabled = false;
            ddl_TeamNetwork.Enabled = false;

            ddl_region_dept.SelectedValue = Session["RegionID"].ToString();
        }
        else if (UserLevel == 1 || UserLevel == 2)
        {
            ddlRegion.Enabled = true;
            ddlCenter.Enabled = true;

            ddl_region_dept.Enabled = true;
            ddl_center_dept.Enabled = true;

            ddl_RegionTeam.Enabled = true;

        }

    }

    private void GridSelectedRow(GridView gv, ImageButton imgbtn)
    {
        GridViewRow gvr;
        gvr = (GridViewRow)imgbtn.NamingContainer;
        gv.SelectedIndex = gvr.RowIndex;
    }

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

    protected void FillHODList()
    {

        DataTable dt = new DataTable();


        if (UserLevel == 4) //Campus
        {
           objBllRegion.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objBllCenter.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
        }
        else if (UserLevel == 3)//Region
        {
            objBllRegion.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objBllCenter.Center_Id = 0;
        }
        else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
        {
            objBllRegion.Region_Id = 0;
            objBllCenter.Center_Id = 0;

        }

        //dt = objBll.UserHODSelectByUserTypeIDRegionCenter(objBll);
        //objBase.FillDropDown(dt, ddlHOD, "EmployeeCode", "CodeName");




    }

    public void loadRegions()
    {
        BLLVacationTimigs objBll = new BLLVacationTimigs();
        DataTable _dt = new DataTable();

        _dt = objBll.fetchRegions();

        ddlRegion.DataTextField = "Region_Name";
        ddlRegion.DataValueField = "Region_Id";
        ddlRegion.DataSource = _dt;
        ddlRegion.DataBind();
        ddlRegion.Items.Insert(0, new ListItem("Head Office", "0"));

       // string id = Session["RegionID"].ToString();
        ddlRegion.SelectedValue = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();


        ddlRegions.DataTextField = "Region_Name";
        ddlRegions.DataValueField = "Region_Id";
        ddlRegions.DataSource = _dt;
        ddlRegions.DataBind();
        ddlRegions.Items.Insert(0, new ListItem("Head Office", "0"));

        ddlRegions.SelectedValue = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();





        ddl_RegionTeam.DataTextField = "Region_Name";
        ddl_RegionTeam.DataValueField = "Region_Id";
        ddl_RegionTeam.DataSource = _dt;
        ddl_RegionTeam.DataBind();
        ddl_RegionTeam.Items.Insert(0, new ListItem("Head Office", "0"));

        ddl_RegionTeam.SelectedValue = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();


    }

    protected void loadCenters()
    {

        BLLVacationTimigs objBll = new BLLVacationTimigs();
        DataTable _dt = new DataTable();


        objBll.Region_id = Convert.ToInt32(ViewState["Region_Id"]);
        _dt = objBll.fetchCenters(objBll);

        ddlCenter.DataTextField = "Center_Name";
        ddlCenter.DataValueField = "Center_ID";
        ddlCenter.DataSource = _dt;
        ddlCenter.DataBind();

        if (ddlRegion.SelectedValue == "0")
        {
            ddlCenter.Items.Insert(0, new ListItem("Head Office", "0"));
            
        }
        else
        {
            ddlCenter.Items.Insert(0, new ListItem("--Regional Office--", "0"));
            
        }


        ddlCenterTeam.Items.Clear();
        ddlCenterTeam.DataTextField = "Center_Name";
        ddlCenterTeam.DataValueField = "Center_ID";
        ddlCenterTeam.DataSource = _dt;
        ddlCenterTeam.DataBind();
        

        if (ddl_RegionTeam.SelectedValue == "0")
        {
            ddlCenterTeam.Items.Insert(0, new ListItem("Head Office", "0"));

        }
        else
        {
            ddlCenterTeam.Items.Insert(0, new ListItem("--Regional Office--", "0"));
        }


       

       
    }

    protected void loadDepartments()
    {

        // BLLAttendance obj = new BLLAttendance();

        // DataTable dt = new DataTable();

        // obj.PMonthDesc = Session["CurrentMonth"].ToString();

        //obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
        // obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());

        // dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);

        // objBase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");

        BLLEmployeeReportToHOD objBll = new BLLEmployeeReportToHOD();

        DataTable dt = new DataTable();
        objBll.PMonth = "201804";
         objBll.PMonth = Session["CurrentMonth"].ToString();

        objBll.regionId = Convert.ToInt32(ViewState["Region_Id"]);
        objBll.centerId = Convert.ToInt32(ddlCenterTeam.SelectedValue);

        dt = objBll.WebDepartmentSelectByMonthRegionCenter(objBll);

        objBase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");




    }

    public void load_region_dept()
    {
        BLLVacationTimigs objBll = new BLLVacationTimigs();
        DataTable _dt = new DataTable();

        _dt = objBll.fetchRegions();

        ddl_region_dept.DataTextField = "Region_Name";
        ddl_region_dept.DataValueField = "Region_Id";
        ddl_region_dept.DataSource = _dt;
        ddl_region_dept.DataBind();

        ddl_region_dept.Items.Insert(0, new ListItem("Head Office", "0"));

        ddl_region_dept.SelectedValue = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();


        // ddl_region_dept_SelectedIndexChanged(null, null);


    }

    protected void load_center_dept()
    {

        BLLVacationTimigs objBll = new BLLVacationTimigs();
        DataTable _dt = new DataTable();
        if (ViewState["Region_Id"] != null)
        {
            objBll.Region_id = Convert.ToInt32(ViewState["Region_Id"]);
        }
        else
        {
            objBll.Region_id = Convert.ToInt32(this.ddl_region_dept.SelectedValue);
        }
        _dt = objBll.fetchCenters(objBll);

        ddl_center_dept.DataTextField = "Center_Name";
        ddl_center_dept.DataValueField = "Center_ID";
        ddl_center_dept.DataSource = _dt;
        ddl_center_dept.DataBind();

        if (ddl_region_dept.SelectedValue == "0")
        {
            ddl_center_dept.Items.Insert(0, new ListItem("Head Office", "0"));
        }
        else
        {
            ddl_center_dept.Items.Insert(0, new ListItem("Regional Office", "0"));
        }

        ddl_center_dept.SelectedValue = (Session["CenterID"].ToString() == "") ? "0" : Session["CenterID"].ToString();
        ddl_center_dept_SelectedIndexChanged(null, null);

        //ViewState["Region_Id"] = null;
        //ViewState["Region_Id"] = ddl_region_dept.SelectedValue;
        load_ddl_Networks();



        //BindGridHod();

        //ViewState["dtMain"] = null;
        //bindgrid();
        //DateTime.TryParse(
    }

    protected void load_ddl_Networks()
    {
        DataTable _dt = new DataTable();

        objBllRegion.Region_Id = Convert.ToInt32(ViewState["Region_Id"]);


        _dt = objBllRegion.NetworkRegionFetchByRegionID(objBllRegion.Region_Id);

        ddl_Networks.Items.Clear();



        ddl_Networks.DataTextField = "NetworkName";
        ddl_Networks.DataValueField = "NetworkRegion_Id";
        ddl_Networks.DataSource = _dt;
        ddl_Networks.DataBind();
        ddl_Networks.Items.Insert(0, new ListItem("-- Select Network --", "0"));
        // string id = Convert.ToString(ViewState["NetworkRegion_Id"]);
        if (objBllRegion.Region_Id == 0)
        {
            ddl_Networks.SelectedValue = "0";
        }
        else
        {
            ddl_Networks.SelectedValue = (Convert.ToString(ViewState["NetworkRegion_Id"]) == "") ? "0" : ViewState["NetworkRegion_Id"].ToString();
        }


        ddl_TeamNetwork.Items.Clear();


        ddl_TeamNetwork.DataTextField = "NetworkName";
        ddl_TeamNetwork.DataValueField = "NetworkRegion_Id";
        ddl_TeamNetwork.DataSource = _dt;
        ddl_TeamNetwork.DataBind();
        ddl_TeamNetwork.Items.Insert(0, new ListItem("-- Select Network --", "0"));
        if (objBllRegion.Region_Id == 0)
        {
            ddl_TeamNetwork.SelectedValue = "0";
        }
        else
        {
            ddl_TeamNetwork.SelectedValue = (Convert.ToString(ViewState["NetworkRegion_Id"]) == "") ? "0" : ViewState["NetworkRegion_Id"].ToString();
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCenters();
        ddlCenter_SelectedIndexChanged(sender, e);

        DataTable dt = new DataTable();

        objBllRegion.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
       // objBllRegion.NetworkRegion_Id objBllRegion.NetworkRegion_Id
        dt = objBllRegion.NetworkRegionFetch(objBllRegion);
        gvNetwork.DataSource = dt;
        gvNetwork.DataBind();

        

    }

    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["HOD"] = null;
        //BindGridHod();

        //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
    }

    protected void ddl_RegionTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = null;
        //BindGridNetwork();
        ViewState["NetworkRegion_Id"] = null;
        //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
        // (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString(); 
        ViewState["Region_Id"] = ddl_RegionTeam.SelectedValue;
        load_center_dept();
        ddl_center_dept_SelectedIndexChanged(sender, e);

        BindAssignCampus();
        loadCenters();

    }

    protected void ddlCenterTeam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = null;
        ViewState["Region_Id"] = ddl_RegionTeam.SelectedValue;
        loadDepartments();
    }

    protected void ddl_region_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = null;
        //BindGridNetwork();
        ViewState["NetworkRegion_Id"] = null;
        ViewState["Region_Id"] = ddl_region_dept.SelectedValue;
        //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
        ddl_center_dept_SelectedIndexChanged(sender, e);
       
        load_center_dept();
        
        BindAssignCampus();
        loadCenters();
        
    }

    protected void ddl_center_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ViewState["HOD"] = null;
        //BindGridHod();

        //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");

        loadDepartments();
        ddlDepartment_SelectedIndexChanged(sender, e);
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDepartment.SelectedIndex >= 0)
        {
            //loadEmployees();
            ViewState["Employees"] = null;
            BingGridEmployees();

        }
    }

    protected void gvCampuses_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            //DataTable oDataSet = (DataTable)ViewState["Subordinates"];
            //oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            //if (ViewState["SortDirection"].ToString() == "ASC")
            //{
            //    ViewState["SortDirection"] = "DESC";
            //}
            //else
            //{
            //    ViewState["SortDirection"] = "ASC";
            //}
            //BindGridSubordinates(0);

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvCampuses_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           gvCampuses.PageIndex = e.NewPageIndex;
           BindGridShowCampus();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvCampuses_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            ViewState["Region_Id"] = ddlRegion.SelectedValue;
            ViewState["NetworkRegion_Id"] = null;
            objBllCenter.NetworkCenter_Id = Convert.ToInt32(gvCampuses.Rows[e.RowIndex].Cells[0].Text);
            ViewState["NetworkRegion_Id"] = Convert.ToInt32(gvCampuses.Rows[e.RowIndex].Cells[4].Text);
            objBllCenter.NetworkCenterDelete(objBllCenter);

            BindGridNetwork();
            BindGridShowCampus();
            ImpromptuHelper.ShowPrompt("Campus Removed Sucessfully");


        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvCampuses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("ImageButton2");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?') ");
        }
    }

    protected void gvNetwork_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["HOD"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionHOD"].ToString();
            if (ViewState["SortDirectionHOD"].ToString() == "ASC")
            {
                ViewState["SortDirectionHOD"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionHOD"] = "ASC";
            }
            //BindGridHod();

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvNetwork_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvNetwork.PageIndex = e.NewPageIndex;
            //BindGridHod();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvNetwork_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int AlreadyIn = 0;
            ViewState["Region_Id"] = ddlRegion.SelectedValue;
            // BLLEmployeeReportToHOD objBll = new BLLEmployeeReportToHOD();

            objBllRegion.NetworkRegion_Id = Convert.ToInt32(gvNetwork.Rows[e.RowIndex].Cells[0].Text);


            AlreadyIn = objBllRegion.NetworkRegionDelete(objBllRegion);

            if (AlreadyIn == 0)
            {
                BindGridNetwork();

                // BindGridHod();
                //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
                pan_AssigCampus.Attributes.CssStyle.Add("display", "none");
                pan_AssignTeam.Attributes.CssStyle.Add("display", "none");
                Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
                ImpromptuHelper.ShowPrompt("Network Remove Sucessfully");
            }
            else
            {

                ImpromptuHelper.ShowPrompt("Please First Remove Assign Campuses / Team");


            }
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvNetwork_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("btnDelete");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?') ");
        }
    }

    protected void gvNetworkRegion_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            //DataTable oDataSet = (DataTable)ViewState["Employees"];
            //oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionEmployees"].ToString();
            //if (ViewState["SortDirectionEmployees"].ToString() == "ASC")
            //{
            //    ViewState["SortDirectionEmployees"] = "DESC";
            //}
            //else
            //{
            //    ViewState["SortDirectionEmployees"] = "ASC";
            //}
            //BingGridEmployees();

        }
        catch (Exception oException)
        {
            throw oException;
        }

    }

    protected void gvNetworkRegion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            //if (e.CommandName == "toggleCheck")
            //{
            //    CheckBox cb = null;
            //    string mood = ViewState["MarkEmployees"].ToString();

            //    foreach (GridViewRow gvr in gvEmployees.Rows)
            //    {
            //        cb = (CheckBox)gvr.FindControl("CheckBox1");

            //        if (mood == "" || mood == "check")
            //        {
            //            cb.Checked = true;
            //            ViewState["MarkEmployees"] = "uncheck";
            //        }
            //        else
            //        {
            //            cb.Checked = false;
            //            ViewState["MarkEmployees"] = "check";
            //        }

            //    }

            //}

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void gvAssignCampus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAssignCampus.PageIndex = e.NewPageIndex;
            BindAssignCampus();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvAssignCampus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["MarkCampus"].ToString();

                foreach (GridViewRow gvr in gvAssignCampus.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBox1");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["MarkCampus"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["MarkCampus"] = "check";
                    }

                }

            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    
    protected void gvEmployees_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["Employees"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionEmployees"].ToString();
            if (ViewState["SortDirectionEmployees"].ToString() == "ASC")
            {
                ViewState["SortDirectionEmployees"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionEmployees"] = "ASC";
            }
            BingGridEmployees();

        }
        catch (Exception oException)
        {
            throw oException;
        }

    }

    protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvEmployees.PageIndex = e.NewPageIndex;
            BingGridEmployees();
        }
        catch (Exception oException)
        {
            throw oException;
        }

    }

    protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["MarkEmployee"].ToString();

                foreach (GridViewRow gvr in gvEmployees.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBox2");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["MarkEmployee"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["MarkEmployee"] = "check";
                    }

                }

            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    
    protected void gvTeam_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            //DataTable oDataSet = (DataTable)ViewState["Subordinates"];
            //oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            //if (ViewState["SortDirection"].ToString() == "ASC")
            //{
            //    ViewState["SortDirection"] = "DESC";
            //}
            //else
            //{
            //    ViewState["SortDirection"] = "ASC";
            //}
            //BindGridSubordinates(0);

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvTeam_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvTeam.PageIndex = e.NewPageIndex;
            BindGridTeam();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvTeam_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("btnDelNetworkTeam");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?') ");
        }
    }
  
    protected void BindGridNetwork()
    {
        gvNetwork.DataSource = null;
        gvNetwork.DataBind();
        DataTable dt = new DataTable();
        objBllRegion.Region_Id = Convert.ToInt32(ViewState["Region_Id"]);
        dt = objBllRegion.NetworkRegionFetch(objBllRegion);
        gvNetwork.DataSource = dt;
        gvNetwork.DataBind();


    }

    protected void BindAssignCampus()
    {
        DataTable dt = new DataTable();
        BLLVacationTimigs objBll = new BLLVacationTimigs();
        gvAssignCampus.DataSource = null;
        gvAssignCampus.DataBind();
      

        objBll.Region_id = Convert.ToInt32(ViewState["Region_Id"]);
        dt = objBllRegion.NetworkCenterSelectByRegionID(objBll.Region_id);
        gvAssignCampus.DataSource = dt;
        gvAssignCampus.DataBind();



    }

    private void BindGridShowCampus()
    { 
          DataTable _dt = new DataTable();
          objBllCenter.NetworkRegion_Id = Convert.ToInt32(ViewState["NetworkRegion_Id"]);
        _dt =objBllCenter.NetworkCenterFetch(objBllCenter.NetworkRegion_Id);
        gvCampuses.DataSource = _dt;
        gvCampuses.DataBind();
    
    }

    private void BindGridTeam()
      {
          
          DataTable _dt = new DataTable();
          objBllTeam.NetworkRegion_Id = Convert.ToInt32(ViewState["NetworkRegion_Id"]);
          _dt = objBllTeam.NetworkTeamFetch(objBllTeam.NetworkRegion_Id);
          gvTeam.DataSource = _dt;
          gvTeam.DataBind();
          divListOfTeam.Attributes.CssStyle.Add("display", "inline");

      }

    protected void BingGridEmployees()
      {
          gvEmployees.DataSource = null;
          gvEmployees.DataBind();
          BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

          DataTable dt = new DataTable();
          UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
          UserType = Convert.ToInt32(Session["UserType"].ToString());

          if (ViewState["Employees"] == null)
          {
              if (UserLevel == 4)
              {
                  obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                  obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
              }
              else if (UserLevel == 3)
              {
                  obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                  obj.Center_id = Convert.ToInt32(ddlCenterTeam.SelectedValue);
              }
              else if (UserLevel == 1 || UserLevel == 2)
              {
                  obj.Region_id = Convert.ToInt32(ddl_RegionTeam.SelectedValue);
                  obj.Center_id = Convert.ToInt32(ddlCenterTeam.SelectedValue);
              }

              obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);


              dt = objBllTeam.NetworkTeamByRegion(obj);
              ViewState["Employees"] = dt;
          }
          else
              dt = (DataTable)ViewState["Employees"];

          gvEmployees.DataSource = dt;
          gvEmployees.DataBind();
      }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pan_AssigCampus.Attributes.CssStyle.Add("display", "none");
        Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        pan_AssignTeam.Attributes.CssStyle.Add("display", "none");
        ddlRegions.SelectedValue = Session["RegionID"].ToString() == "" ? "0" : Session["RegionID"].ToString();
        //divMainList.Attributes.CssStyle.Add("display", "inline");
        //gvCampuses.SelectedRowStyle.Reset();
    }
  

    protected void btnAddNetwork_Click(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = null;
        txtNetworkName.Text = string.Empty;
        
        Pan_NetworkName.Attributes.CssStyle.Add("display", "inline");
        pan_AssigCampus.Attributes.CssStyle.Add("display", "none");
        pan_AssignTeam.Attributes.CssStyle.Add("display", "none");
        divListOfTeam.Attributes.CssStyle.Add("display", "none");
        divListOfCampus.Attributes.CssStyle.Add("display", "none");
       
       
        

        //divMainList.Attributes.CssStyle.Add("display", "inline");
        ViewState["Region_Id"] = ddlRegion.SelectedValue;
        //BindgvNetworkRegion();

        //txtHODEmail.Text = "";
        //chkEmail.Checked = false;
        //divHODmail.Attributes.CssStyle.Add("display", "none");
        //ddlDepartment.SelectedValue = "0";
        //ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);
        //pan_New.Attributes.CssStyle.Add("display", "inline");
        //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
        //btnSaveHOD.Attributes.CssStyle.Add("display", "inline");
        //btnSaveSubordinate.Attributes.CssStyle.Add("display", "none");
        //Page.SetFocus(btnCancel);
    }

    protected void btnDelNetworkRegion_Click(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = ddlRegion.SelectedValue;
        ImageButton imgBtn = (ImageButton)sender;
        objBllRegion.NetworkRegion_Id = Int32.Parse(imgBtn.CommandArgument);

        int AlreadyIn = 0;

        AlreadyIn = objBllRegion.NetworkRegionDelete(objBllRegion);
        if (AlreadyIn == 0)
        {
            ImpromptuHelper.ShowPrompt("Netowrk Name Removed Sucessfully");
            //BindgvNetworkRegion();
            BindGridNetwork();
        }
        else
        {

            ImpromptuHelper.ShowPrompt("Please First Remove Assign Campuses");

        }

        //ViewState["NetworkRegion_Id"] = NetworkRegion_Id;

        //ViewState["HODEmpCode"] = imgBtn.CommandArgument;
        //ViewState["Subordinates"] = null;
        //btnCancel_Click(this, EventArgs.Empty);//hide add subordinate panel
        //BindGridSubordinates(employeeCode);
        //GridSelectedRow(gvHOD, imgBtn);
        //Page.SetFocus(gvCampuses);

    }

    protected void btnAddCampuses_Click(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = null;
        ImageButton imgBtn = (ImageButton)sender;
        // int NetworkRegion_Id = Int32.Parse(imgBtn.CommandArgument);
        // int NetworkRegion_Id = Int32.Parse(imgBtn.CommandArgument);
        string Rid = Convert.ToString(Session["RegionID"]);

        ViewState["Region_Id"] = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();
        ViewState["NetworkRegion_Id"] = Int32.Parse(imgBtn.CommandArgument);

        Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        pan_AssigCampus.Attributes.CssStyle.Add("display", "inline");
        pan_AssignTeam.Attributes.CssStyle.Add("display", "none");
        divListOfTeam.Attributes.CssStyle.Add("display", "none");
        divListOfCampus.Attributes.CssStyle.Add("display", "none");
        //divMainList.Attributes.CssStyle.Add("display", "none");
        //ddl_region_dept.SelectedItem
        //ddl_region_dept.SelectedValue = Convert.ToString(ViewState["Region_Id"]);
        load_region_dept();
        load_ddl_Networks();


        BindAssignCampus();
        //if (ddlRegion.SelectedValue == "0")
        //{
        //    ddl_center_dept.Visible = true;
        //}





    }

    protected void btnShowCampus_Click(object sender, EventArgs e)
    {

        //Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        //pan_New.Attributes.CssStyle.Add("display", "inline");

        ViewState["NetworkRegion_Id"] = null;
        ImageButton imgBtn = (ImageButton)sender;
        int NetworkRegion_Id = Int32.Parse(imgBtn.CommandArgument);
        ViewState["NetworkRegion_Id"] = imgBtn.CommandArgument;
        // ViewState["Subordinates"] = null;
        // btnCancel_Click(this, EventArgs.Empty);//hide add subordinate panel
        // BindGridSubordinates(employeeCode);
        GridSelectedRow(gvNetwork, imgBtn);
        // Page.SetFocus(gvCampuses);


        BindGridShowCampus();
        //pan_New.Attributes.CssStyle.Add("display", "none");
        divListOfTeam.Attributes.CssStyle.Add("display", "none");
        divListOfCampus.Attributes.CssStyle.Add("display", "block");
        Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        pan_AssignTeam.Attributes.CssStyle.Add("display", "none");
       
    }

    protected void btnAddTeam_Click(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = null;
        ViewState["Employees"] = null;
        ImageButton imgBtn = (ImageButton)sender;
       ViewState["Region_Id"]  = (Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString();
       // ViewState["Region_Id"] = ddlRegion.SelectedValue;

        ViewState["NetworkRegion_Id"] = Int32.Parse(imgBtn.CommandArgument);




        pan_AssignTeam.Attributes.CssStyle.Add("display", "block");
        Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        pan_AssigCampus.Attributes.CssStyle.Add("display", "none");
        divListOfCampus.Attributes.CssStyle.Add("display", "none");
        divListOfTeam.Attributes.CssStyle.Add("display", "none");

        loadRegions();
        load_ddl_Networks();
        loadCenters();
        loadDepartments();
        BingGridEmployees();






    }

    protected void btnShowTeam_Click(object sender, EventArgs e)
    {

        //Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        //pan_New.Attributes.CssStyle.Add("display", "inline");

        ViewState["NetworkRegion_Id"] = null;
        ImageButton imgBtn = (ImageButton)sender;
        int NetworkRegion_Id = Int32.Parse(imgBtn.CommandArgument);
        ViewState["NetworkRegion_Id"] = imgBtn.CommandArgument;
        // ViewState["Subordinates"] = null;
        // btnCancel_Click(this, EventArgs.Empty);//hide add subordinate panel
        // BindGridSubordinates(employeeCode);
        BindGridTeam();
        GridSelectedRow(gvNetwork, imgBtn);
        // Page.SetFocus(gvCampuses);



        //pan_New.Attributes.CssStyle.Add("display", "none");
        divListOfTeam.Attributes.CssStyle.Add("display", "none");
        divListOfCampus.Attributes.CssStyle.Add("display", "none");
        divListOfTeam.Attributes.CssStyle.Add("display", "inline");
        pan_AssigCampus.Attributes.CssStyle.Add("display", "none");
        Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        pan_AssignTeam.Attributes.CssStyle.Add("display", "none");



    }

    protected void btnDelNetworkTeam_Click(object sender, EventArgs e)
    {
        //ViewState["NetworkRegion_Id"] = null;
        ViewState["Region_Id"] = ddlRegion.SelectedValue;
        ImageButton imgBtn = (ImageButton)sender;
        objBllTeam.NetworkTeam_Id = Int32.Parse(imgBtn.CommandArgument);

        objBllTeam.NetworkTeamDelete(objBllTeam);
        ImpromptuHelper.ShowPrompt("Netowrk Team Member Removed Sucessfully");
        BindGridTeam();
        BindGridNetwork();

        //ViewState["NetworkRegion_Id"] = NetworkRegion_Id;

        //ViewState["HODEmpCode"] = imgBtn.CommandArgument;
        //ViewState["Subordinates"] = null;
        //btnCancel_Click(this, EventArgs.Empty);//hide add subordinate panel
        //BindGridSubordinates(employeeCode);
        //GridSelectedRow(gvHOD, imgBtn);
        //Page.SetFocus(gvCampuses);

    }

    protected void btnSaveNetWorkName_Click(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = null;
        ViewState["Region_Id"] = ddlRegion.SelectedValue;
        objBllRegion.Region_Id = Convert.ToInt32(ddlRegions.SelectedValue);
        objBllRegion.NetworkName = txtNetworkName.Text;
        int alreadyexist = 0;
        alreadyexist = objBllRegion.NetworkRegionAdd(objBllRegion);
        if (alreadyexist == 0)
        {
            ImpromptuHelper.ShowPrompt("Network Name Added Sucessfully");
            txtNetworkName.Text = string.Empty;
            Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
            //BindgvNetworkRegion();
            BindGridNetwork();
            ddlRegions.SelectedValue =  ViewState["Region_Id"].ToString();
        }
    }

    protected void btnAssignNetwork_Click(object sender, EventArgs e)
    {
        Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        pan_AssigCampus.Attributes.CssStyle.Add("display", "inline");

    }

    protected void btnSaveAssignCampus_Click(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = ddlRegion.SelectedValue;
        Boolean flag = false;
        try
        {

            foreach (GridViewRow gvr in gvAssignCampus.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("CheckBox1");

                if (cb.Checked)
                {
                    objBllCenter.NetworkRegion_Id = Convert.ToInt32(ddl_Networks.SelectedValue);
                    objBllCenter.Center_Id = Convert.ToInt32(gvAssignCampus.Rows[gvr.RowIndex].Cells[0].Text);
                    objBllCenter.NetworkCenterAdd(objBllCenter);
                    flag = true;
                }

            }
            if (flag == true)
            {
                ImpromptuHelper.ShowPrompt("Campuses Saved Sucessfully");
                pan_AssigCampus.Attributes.CssStyle.Add("display", "none");
                gvAssignCampus.DataSource = null;
                gvAssignCampus.DataBind();
                BindGridNetwork();
            }
            else
            {

                ImpromptuHelper.ShowPrompt("No Record Save");

            }

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void btnAddEmployee_Click(object sender, EventArgs e)
    {
        Pan_NetworkName.Attributes.CssStyle.Add("display", "none");
        pan_AssigCampus.Attributes.CssStyle.Add("display", "inline");

    }

    protected void btnShowEmployee_Click(object sender, EventArgs e)
    {



        ViewState["NetworkRegion_Id"] = null;
        ImageButton imgBtn = (ImageButton)sender;
        int NetworkRegion_Id = Int32.Parse(imgBtn.CommandArgument);
        ViewState["NetworkRegion_Id"] = imgBtn.CommandArgument;

        GridSelectedRow(gvNetwork, imgBtn);
        Page.SetFocus(gvCampuses);


        BindGridTeam();

        divListOfCampus.Attributes.CssStyle.Add("display", "none");
        pan_AssigCampus.Attributes.CssStyle.Add("display", "none");
        divListOfTeam.Attributes.CssStyle.Add("display", "block");
    }

    protected void btnSaveNetworkEmployee_Click(object sender, EventArgs e)
    {
        ViewState["Region_Id"] = ddlRegion.SelectedValue;
        Boolean flag = false;
        try
        {
            foreach (GridViewRow gvr in gvEmployees.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("CheckBox2");

                if (cb.Checked)
                {

                    objBllTeam.NetworkRegion_Id = Convert.ToInt32(ddl_TeamNetwork.SelectedValue);
                    objBllTeam.EmployeeCode = gvEmployees.Rows[gvr.RowIndex].Cells[0].Text;
                    CheckBox chkRow = (gvr.Cells[0].FindControl("cbIsHo") as CheckBox);
                    objBllTeam.IsHOD = false;
                    if (chkRow.Checked)
                    {
                        objBllTeam.IsHOD = true;
                       
                      
                    }

                    objBllTeam.NetworkTeamAdd(objBllTeam);
                    flag = true;
                }

            }
            if (flag == true)
            {
                ImpromptuHelper.ShowPrompt("Team Members Saved Sucessfully");

                pan_AssignTeam.Attributes.CssStyle.Add("display", "none");
                gvAssignCampus.DataSource = null;
                gvAssignCampus.DataBind();
                BindGridNetwork();
            }
            else
            {

                ImpromptuHelper.ShowPrompt("No Record Save");

            }

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }
   
   
    
    
}