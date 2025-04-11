using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;

public partial class EmployeeReportToHODWise : System.Web.UI.Page
{
    BLLEmployeeReportTo objBll = new BLLEmployeeReportTo();
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

        ////======== Page Access Settings ========================
        //DALBase objBase = new DALBase();
        //DataRow row = (DataRow)Session["rightsRow"];
        //string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        //System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        //string sRet = oInfo.Name;


        //DataTable _dtSettings = objBase.ApplyPageAccessSettingsTable(sRet, Convert.ToInt32(row["User_Type_Id"].ToString()));
        //this.Page.Title = _dtSettings.Rows[0]["PageTitle"].ToString();
        ////tdFrmHeading.InnerHtml = _dtSettings.Rows[0]["PageCaption"].ToString();
        //if (Convert.ToBoolean(_dtSettings.Rows[0]["isAllow"]) == false)
        //{
        //    Session.Abandon();
        //    Response.Redirect("~/login.aspx");
        //}

        ////====== End Page Access settings ======================

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());

        if (!IsPostBack)
        {

            
            

            pan_New.Attributes.CssStyle.Add("display", "none");
            divListOfSubordinates.Attributes.CssStyle.Add("display","none");
            ViewState["SortDirection"] = "ASC";
            ViewState["SortDirectionHOD"] = "ASC"; 
            ViewState["SortDirectionEmployees"] = "ASC";
            ViewState["MarkEmployees"] = "check";

            //bindgrid();
            FillParentMenu();
            FillPage();
            //FillHODList();
            

            loadRegions();
            loadCenters();

            load_region_dept();
            //ddl_region_dept_SelectedIndexChanged(sender, e);
            //load_center_dept();

            setRightsControls();

            BindGridHod();

            //loadDepartments();
            //ddlDepartment_SelectedIndexChanged(sender, e);

            //FillEmployees();

            



          
        }
        

    }

    public void setRightsControls()
    {
        if (UserLevel == 4)
        {
            ddlRegion.Enabled = false;
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
            ddlCenter.Enabled = true;

            ddlRegion.SelectedValue = Session["RegionID"].ToString();

            ddl_region_dept.Enabled = false;
            ddl_center_dept.Enabled = true;

            ddl_region_dept.SelectedValue = Session["RegionID"].ToString();
        }
        else if (UserLevel == 1 || UserLevel == 2)
        {
            ddlRegion.Enabled = true;
            ddlCenter.Enabled = true;

            ddl_region_dept.Enabled = true;
            ddl_center_dept.Enabled = true;
        }
    }

    protected void BingGridEmployees() {
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
                obj.Center_id = Convert.ToInt32(ddl_center_dept.SelectedValue);
            }
            else if (UserLevel == 1 || UserLevel == 2)
            {
                obj.Region_id = Convert.ToInt32(ddl_region_dept.SelectedValue);
                obj.Center_id = Convert.ToInt32(ddl_center_dept.SelectedValue);
            }

            obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);


            dt = obj.EmployeeprofileSelectByRegionCenterDept(obj);
            ViewState["Employees"] = dt;
        }
        else
            dt = (DataTable)ViewState["Employees"];

        gvEmployees.DataSource = dt;
        gvEmployees.DataBind();
    }
    protected void BindGridHod()
    {
        gvHOD.DataSource = null;
        gvHOD.DataBind();
        DataTable dt = new DataTable();
        if (ViewState["HOD"] == null)
        {
            if (UserLevel == 4) //Campus
            {
                objBll.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
                objBll.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
            }
            else if (UserLevel == 3)//Region
            {
                objBll.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
                objBll.Center_Id = Convert.ToInt32(ddlCenter.SelectedValue);
            }
            else if (UserLevel == 1 || UserLevel == 2)//Head Office + Admin
            {
                objBll.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
                objBll.Center_Id = Convert.ToInt32(ddlCenter.SelectedValue);
            }
            dt = objBll.UserHODSelectByUserTypeIDRegionCenter(objBll);
            ViewState["HOD"] =dt;
        }

        else
            dt = (DataTable)ViewState["HOD"];


        //if (_dt.Rows.Count == 0)
        //  lab_dataStatus.Visible = true;
        //else
        {
            gvHOD.DataSource = dt;
            ViewState["HOD"] = dt;
            //lab_dataStatus.Visible = false;
        }
        gvHOD.DataBind();
        //        pan_New.Attributes.CssStyle.Add("display", "none");

    }
    protected void BindGridSubordinates(int empCode)
    {
        //objBll.Status_id = 1;
        DataTable _dt = new DataTable();
        if (ViewState["Subordinates"] == null)
        {
            //objBll.EmployeeCode = ddlHOD.SelectedValue;
            objBll.EmployeeCode = empCode.ToString();
            _dt = objBll.EmployeeReportToSelectByEmployeeCode(objBll);
            ViewState["Subordinates"] = _dt;
        }

        else
            _dt = (DataTable)ViewState["Subordinates"];


        if (_dt.Rows.Count == 0)
            lab_dataStatus.Visible = true;
        else
        {
            gvSubordinates.DataSource = _dt;
            ViewState["Subordinates"] = _dt;
            lab_dataStatus.Visible = false;
        }
        gvSubordinates.DataBind();
        //pan_New.Attributes.CssStyle.Add("display", "none");
        divListOfSubordinates.Attributes.CssStyle.Add("display", "block");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pan_New.Attributes.CssStyle.Add("display", "none");
        gvSubordinates.SelectedRowStyle.Reset();
    }
    protected void btnSaveHOD_Click(object sender, EventArgs e) {
        BLLEmployeeReportToHOD objBll = new BLLEmployeeReportToHOD();
        Boolean IsAlreadyHOD = false;
        Boolean flag = true;
        string empCodeAlreadyHOD = "";
        try
        {
            //if (ddlEmployeecode.SelectedValue == "0")
            //{
            //    ImpromptuHelper.ShowPrompt("Select an Employee to Make HOD!");
            //    return;
            //}
            foreach (GridViewRow gvr in gvEmployees.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("CheckBox1");

                if (cb.Checked)
                {
                    objBll.EmployeeCode = gvEmployees.Rows[gvr.RowIndex].Cells[1].Text;
                    if (objBll.isEmployeeHOD(objBll))
                    {
                        IsAlreadyHOD = true;
                        empCodeAlreadyHOD = empCodeAlreadyHOD + " , " + objBll.EmployeeCode;
                    }
                    else
                    {
                        objBll.MakeHODfromEmployee(objBll);
                        flag = true;
                    }
                }
            }

            if (flag && IsAlreadyHOD) { 
            ImpromptuHelper.ShowPrompt("Employees [" + empCodeAlreadyHOD + "] alerady marked as HOD, Rest Selected Employees Successfully Marked As HOD!");
            ViewState["HOD"] = null;
            btnCancel_Click(this, EventArgs.Empty);
            BindGridHod();
            }
           
            else if (flag==true & IsAlreadyHOD==false)
            {
                ViewState["HOD"] = null;
                btnCancel_Click(this, EventArgs.Empty);
                BindGridHod();
                ImpromptuHelper.ShowPrompt("Status changed as HOD Successfully!");
            }
            else if (flag == false & IsAlreadyHOD == true) 
            {
                ImpromptuHelper.ShowPrompt("Employees [" + empCodeAlreadyHOD + "] alerady marked as HOD!");
            }
            else
            {
               ImpromptuHelper.ShowPrompt("No record saved!");
            }
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }
    protected void btnSaveSubordinate_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtHODEmail.Text.Trim().Length < 1 && chkEmail.Checked)
            {
                ImpromptuHelper.ShowPrompt("Enter Email,or Uncheck Send Email to HOD!");
                return;
            }
            //if (ddlEmployeecode.SelectedValue == Convert.ToString(ViewState["HODEmpCode"]))
            //{
            //    ImpromptuHelper.ShowPrompt(ddlEmployeecode.SelectedValue + " Can't Reported Him/Her Self!");
            //    return;
            //}

            int nAlreadyIn;
            int id = 0;
            Boolean isDuplicated = false;
            Boolean flag = false;
            string empAlready = "";
            foreach (GridViewRow gvr in gvEmployees.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("CheckBox1");

                if (cb.Checked)
                {


                    objBll.EmployeeCode = gvEmployees.Rows[gvr.RowIndex].Cells[1].Text;
                    objBll.ReportTo = Convert.ToString(ViewState["HODEmpCode"]);
                    objBll.HODEmail = txtHODEmail.Text;
                    objBll.IsEmail = chkEmail.Checked;
                    objBll.Status_id = 1;

                    nAlreadyIn = objBll.EmployeeReportToInsert(objBll);

                    if (nAlreadyIn == 1)
                    {
                        isDuplicated = true;
                        empAlready = empAlready + " , " + objBll.EmployeeCode;

                    }
                    else
                        flag = true;


                }
            }
            
            if (flag && isDuplicated)
            {
                btnCancel_Click(this, EventArgs.Empty);
                ViewState["Subordinates"] = null;
                BindGridSubordinates(Convert.ToInt32(objBll.ReportTo));

                ViewState["HOD"] = null;
                BindGridHod();
                ImpromptuHelper.ShowPrompt("Employee code [" + empAlready + "] already reported To " + objBll.ReportTo + " reset all records saved!");

               
            }
            else if (flag == true && isDuplicated == false)
            {
                btnCancel_Click(this, EventArgs.Empty);
                ViewState["Subordinates"] = null;
                BindGridSubordinates(Convert.ToInt32(objBll.ReportTo));

                ViewState["HOD"] = null;
                BindGridHod(); 
                ImpromptuHelper.ShowPrompt("Subordinate Record Save Successfully!");
            }
            else if (flag == false && isDuplicated == true)
            {
                ImpromptuHelper.ShowPrompt("Employee code [" + empAlready + "] already reported To " + objBll.ReportTo );
            }   
            else
            {
                ImpromptuHelper.ShowPrompt("No record saved!");
            }

        }
        catch (Exception oException)
        {
            throw oException;
        }
    
    }

    protected void gvSubordinates_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["Subordinates"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            BindGridSubordinates(0);

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvSubordinates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSubordinates.PageIndex = e.NewPageIndex;
            BindGridSubordinates(0);
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvSubordinates_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            
            objBll.Tid = Convert.ToInt32(gvSubordinates.Rows[e.RowIndex].Cells[0].Text);
            objBll.EmployeeReportToDelete(objBll);

            ViewState["Subordinates"] = null;
            BindGridSubordinates(Convert.ToInt32(gvSubordinates.Rows[e.RowIndex].Cells[6].Text));
            ViewState["HOD"] = null;
            BindGridHod();

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvSubordinates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("ImageButton2");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?') ");
        }
    }

    protected void btnShowSubordinates_Click(object sender, EventArgs e) {
        ViewState["HODEmpCode"] = null;
        ImageButton imgBtn = (ImageButton)sender;
        int  employeeCode = Int32.Parse(imgBtn.CommandArgument);
        ViewState["HODEmpCode"] = imgBtn.CommandArgument;
        ViewState["Subordinates"] = null;
        btnCancel_Click(this, EventArgs.Empty);//hide add subordinate panel
        BindGridSubordinates(employeeCode);
        GridSelectedRow(gvHOD, imgBtn);
        Page.SetFocus(gvSubordinates);
       
    }
    protected void btnAddHOD_Click(object sender, EventArgs e)
    {
        lblAddNew.Text = "[ Assigning New HOD's ]";
      
        txtHODEmail.Text = "";
        chkEmail.Checked = false;

        divHODmail.Attributes.CssStyle.Add("display","none");

        ddlDepartment.SelectedValue = "0";
        ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);

        pan_New.Attributes.CssStyle.Add("display", "inline");
        divListOfSubordinates.Attributes.CssStyle.Add("display", "none");

        btnSaveHOD.Attributes.CssStyle.Add("display", "inline");
        btnSaveSubordinate.Attributes.CssStyle.Add("display", "none");

        Page.SetFocus(btnCancel);
    }
    protected void btnAddSubordinate_Click(object sender, EventArgs e)
    {
        ViewState["Subordinates"] = null;
        ViewState["HODEmpCode"] = null;
        ImageButton imgBtn = (ImageButton)sender;
        int employeeCode = Int32.Parse(imgBtn.CommandArgument);
        ViewState["HODEmpCode"] = imgBtn.CommandArgument;
        lblAddNew.Text = "[ Assigning New Subordinates ]";

        pan_New.Attributes.CssStyle.Add("display", "inline");
        divListOfSubordinates.Attributes.CssStyle.Add("display", "block");

        divHODmail.Attributes.CssStyle.Add("display", "inline");

        btnSaveHOD.Attributes.CssStyle.Add("display", "none");
        btnSaveSubordinate.Attributes.CssStyle.Add("display", "inline");
        
        BindGridSubordinates(employeeCode);
        GridSelectedRow(gvHOD, imgBtn);
        this.txtHODEmail.Text = this.gvHOD.Rows[gvHOD.SelectedIndex].Cells[2].Text != "&nbsp;" ? this.gvHOD.Rows[gvHOD.SelectedIndex].Cells[2].Text : "";
        this.chkEmail.Checked = this.gvHOD.Rows[gvHOD.SelectedIndex ].Cells[3].Text != "&nbsp;" ? Convert.ToBoolean(this.gvHOD.Rows[gvHOD.SelectedIndex ].Cells[3].Text) : false;

        this.ddl_region_dept.SelectedValue = (this.gvHOD.Rows[gvHOD.SelectedIndex].Cells[12].Text != "&nbsp;") ? this.gvHOD.Rows[gvHOD.SelectedIndex].Cells[12].Text : "0";
        ddl_region_dept_SelectedIndexChanged(sender, e);

        this.ddl_center_dept.SelectedValue = (this.gvHOD.Rows[gvHOD.SelectedIndex].Cells[13].Text != "&nbsp;") ? this.gvHOD.Rows[gvHOD.SelectedIndex].Cells[13].Text : "0";
        ddl_center_dept_SelectedIndexChanged(sender, e);

        this.ddlDepartment.SelectedValue = this.gvHOD.Rows[gvHOD.SelectedIndex ].Cells[4].Text != "&nbsp;" ? this.gvHOD.Rows[gvHOD.SelectedIndex].Cells[4].Text : "0";
        ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);
        Page.SetFocus(btnCancel);
        
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

    protected void FillParentMenu()
    {

        /*objBll.Status_id = 1;
        DataTable _dt = new DataTable();
        _dt = objBll.LmsAppMenuFetchByStatusId(objBll);
        objBase.FillDropDown(_dt, ddlParentMenu, "Menu_ID", "MenuText");*/

    }

    protected void FillPage()
    {

        /*DataTable dt = new DataTable();
        dt = objBll.LmsAppPageFetch();
        objBase.FillDropDown(dt, ddlPage, "Page_Id", "PageTitle");*/


    }

    protected void FillHODList()
    {

        DataTable dt = new DataTable();


        if (UserLevel == 4) //Campus
        {
            objBll.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objBll.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
        }
        else if (UserLevel == 3)//Region
        {
            objBll.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objBll.Center_Id = 0;
        }
        else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
        {
            objBll.Region_Id = 0;
            objBll.Center_Id = 0;

        }

        dt = objBll.UserHODSelectByUserTypeIDRegionCenter(objBll);
        //objBase.FillDropDown(dt, ddlHOD, "EmployeeCode", "CodeName");




    }

    //protected void ddlHOD_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ViewState["Subordinates"] = null;

    //    ViewState["HODEmpCode"] = ddlHOD.SelectedValue;
    //    BindGridSubordinates(int.Parse(ddlHOD.SelectedValue));
        
    //}

    protected void FillEmployees()
    {
        //DataTable dt = new DataTable();
        //objBll.User_Type_Id = 18;
        //dt = objBll.UserSelectByUserTypeID(objBll);
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

        ddlRegion.SelectedValue = Session["RegionID"].ToString();




    }


    protected void loadCenters()
    {

        BLLVacationTimigs objBll = new BLLVacationTimigs();
        DataTable _dt = new DataTable();

        objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);
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
            ddlCenter.Items.Insert(0, new ListItem("Regional Office", "0"));
        }


        ViewState["HOD"] = null;
        BindGridHod();

        //ViewState["dtMain"] = null;
        //bindgrid();
        //DateTime.TryParse(
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCenters();
        ddlCenter_SelectedIndexChanged(sender, e);
    }

    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["HOD"] = null;
        BindGridHod();

        divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
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

        ddl_region_dept_SelectedIndexChanged(null, null);


    }


    protected void load_center_dept()
    {

        BLLVacationTimigs objBll = new BLLVacationTimigs();
        DataTable _dt = new DataTable();

        objBll.Region_id = Convert.ToInt32(this.ddl_region_dept.SelectedValue);
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
        //ViewState["HOD"] = null;
        //BindGridHod();

        //ViewState["dtMain"] = null;
        //bindgrid();
        //DateTime.TryParse(
    }




    protected void ddl_region_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ViewState["HOD"] = null;
        //BindGridHod();

        //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
        load_center_dept();
        ddl_center_dept_SelectedIndexChanged(sender, e);
    }


    protected void ddl_center_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ViewState["HOD"] = null;
        //BindGridHod();

        //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");

        loadDepartments();
        ddlDepartment_SelectedIndexChanged(sender, e);
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
        objBll.PMonth = Session["CurrentMonth"].ToString();

        objBll.regionId = Convert.ToInt32(ddl_region_dept.SelectedValue);
        objBll.centerId = Convert.ToInt32(ddl_center_dept.SelectedValue);

        dt = objBll.WebDepartmentSelectByMonthRegionCenter(objBll);

        objBase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");


        

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

    protected void gvHOD_Sorting(object sender, GridViewSortEventArgs e)
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
            BindGridHod();

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvHOD_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvHOD.PageIndex = e.NewPageIndex;
            BindGridHod();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvHOD_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BLLEmployeeReportToHOD objBll = new BLLEmployeeReportToHOD();
            objBll.EmployeeCode = gvHOD.Rows[e.RowIndex].Cells[1].Text;
            
            objBll.MakeEmployeefromHOD(objBll);
            
           

            ViewState["HOD"] = null;
            BindGridHod ();
            divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
            pan_New.Attributes.CssStyle.Add("display", "none");
ImpromptuHelper.ShowPrompt("HOD & All his/her Subordinates Removed Successfully!");
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gvHOD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("ImageButton2");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?') ");
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
                string mood = ViewState["MarkEmployees"].ToString();

                foreach (GridViewRow gvr in gvEmployees.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBox1");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["MarkEmployees"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["MarkEmployees"] = "check";
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
}

