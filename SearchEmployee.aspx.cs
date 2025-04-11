using System;
using System.Web.UI.WebControls;
using System.Data;

public partial class SearchEmployee : System.Web.UI.Page
{
    BLLEmployeeReportTo objBll = new BLLEmployeeReportTo();
    DALBase objBase = new DALBase();
    BLLSearchEmployee objSearch = new BLLSearchEmployee();
    _DALSearchEmployee _DALSearch = new _DALSearchEmployee();


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

        //====== End Page Access settings ======================//

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());
        // btnSearch.Focus();

        if (!IsPostBack)
        {
            pan_New.Attributes.CssStyle.Add("display", "inline");            
            loadRegions();            
            loadCenters();
            //setRightsControls();
            GetEmployeeGrade();
            GetReligionName();
            GetDepartmentName();
            GetDesignationName();            
            btnSearch.Focus();

        }

    }

    //  protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   // loadCenters();
    //   // ddlCenter_SelectedIndexChanged(sender, e);
    //}

    //protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    public void setRightsControls()
    {
        if (UserLevel == 4)
        {
            ddlRegion.Enabled = false;
            ddlCenter.Enabled = false;

            ddlRegion.SelectedValue = Session["RegionID"].ToString();
            ddlCenter.SelectedValue = Session["CenterID"].ToString();
 
        }
        else if (UserLevel == 3)
        {
            ddlRegion.Enabled = false;
            ddlCenter.Enabled = true;

            ddlRegion.SelectedValue = Session["RegionID"].ToString(); 
        }
        else if (UserLevel == 1 || UserLevel == 2)
        {
            ddlRegion.Enabled = true;
            ddlCenter.Enabled = true; 
        }
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
        ddlRegion.Items.Insert(0, new ListItem("--Select--", "-1"));
        ddlRegion.Items.Insert(1, new ListItem("Head Office", "0"));

        if (UserLevel == 4 || UserLevel == 3)
        {
            ddlRegion.Enabled = false;
            ddlRegion.SelectedValue = Session["RegionID"].ToString();
        }
        else if (UserLevel == 1 || UserLevel == 2)
        {
            ddlRegion.Enabled = true;
        } 
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCenters();
    }
    protected void loadCenters()
    {
        BLLVacationTimigs objBll = new BLLVacationTimigs();
        DataTable _dt = new DataTable();
        ddlCenter.Items.Clear();

        objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);
        _dt = objBll.fetchCenters(objBll);

        ddlCenter.DataTextField = "Center_Name";
        ddlCenter.DataValueField = "Center_ID";
        ddlCenter.DataSource = _dt;
        ddlCenter.DataBind();

        if (UserLevel == 4)
        {
            ddlCenter.Enabled = false;
            ddlCenter.SelectedValue = Session["CenterID"].ToString();
        }
        else if (UserLevel == 3)
        {
            ddlCenter.Items.Insert(0, new ListItem("--Select--", "-1"));
            ddlCenter.SelectedValue = "-1";
            ddlCenter.Enabled = true;
        }
        else if (UserLevel == 1 || UserLevel == 2)
        {
            ddlCenter.Enabled = true;

            if (ddlRegion.SelectedValue == "0")
            {
                ddlCenter.Items.Insert(0, new ListItem("Head Office", "0"));
                ddlCenter.SelectedValue = "0";
            }
            else if (ddlRegion.SelectedValue == "-1")
            {
                ddlCenter.Items.Insert(0, new ListItem("--Select--", "-1"));
                ddlCenter.SelectedValue = "-1";
            }
        }
    }

    public void GetDesignationName()
    {
        DataTable dt = new DataTable();
        BLLDesignation objBllDesg = new BLLDesignation();

        dt = objBllDesg.DesignationSelectBySearchCriteriaFetch(objBllDesg);
        objBase.FillDropDown(dt, ddlDesignation, "DesigCode", "DesigName");
    }


    public void GetDepartmentName()
    {
        DataTable dt = new DataTable();
        _DALDepartment _DALDept = new _DALDepartment();
        BLLDepartment objBllDepart = new BLLDepartment();

        dt = objBllDepart.DepartmentSelectBySearchCriteriaFetch(objBllDepart);
        objBase.FillDropDown(dt, ddlDept, "DeptCode", "DeptName");
    }

    public void GetReligionName()
    {
        DataTable dt = new DataTable();
        BLLReligion objReligion = new BLLReligion();

        dt = objReligion.ReligionSelectBySearchCriteriaFetch(objReligion);
        objBase.FillDropDown(dt, ddlReligion, "Religion_Id", "Religion_Name");
    }
    public void GetEmployeeGrade()
    {
        DataTable dt = new DataTable();
        BLLGrade objGrade = new BLLGrade();
        dt = objGrade.GradeSelectBySearchCriteriaFetch(objGrade);
        objBase.FillDropDown(dt, ddlGrade, "EmpGrade", "EmpGrade");
    }

    protected void gvSearchEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSearchEmployee.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["dtSearchEmployee"];

            gvSearchEmployee.DataSource = dt;
            gvSearchEmployee.DataBind();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }


    protected void BindSearchEmployee(BLLSearchEmployee objSearch)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objSearch.EmployeeprofileSelectBySearchCriteriasFetch(objSearch);
            if (dt.Rows.Count > 0)
            {
                divListOfSearchEmployee.Visible = true;
                gvSearchEmployee.DataSource = dt;
                gvSearchEmployee.DataBind();
            }
            ViewState["dtSearchEmployee"] = dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            ResetControls();
        }
        catch (Exception oException)
        {
            throw oException;
        }

    }

    private void ResetControls()
    {
        if (ddlRegion.SelectedIndex > 0)
            ddlRegion.SelectedIndex = 0;
        loadCenters();
        if (ddlCenter.SelectedIndex > 0)
            ddlCenter.SelectedIndex = 0;
        txtEmployeeCode.Text = string.Empty;
        txtName.Text = string.Empty;
        if (ddlDesignation.SelectedIndex > 0)
            ddlDesignation.SelectedIndex = 0;
        if (ddlDept.SelectedIndex > 0)
            ddlDept.SelectedIndex = 0;
        if (ddlGrade.SelectedIndex > 0)
            ddlGrade.SelectedIndex = 0;
        if (ddlReligion.SelectedIndex > 0)
            ddlReligion.SelectedIndex = 0;
        if (ddlIsContracual.SelectedIndex > 0)
            ddlIsContracual.SelectedIndex = 0;
        if (ddlGender.SelectedIndex > 0)
            ddlGender.SelectedIndex = 0;
        ddlInActive.SelectedIndex = 1;
        gvSearchEmployee.DataSource = null;
        gvSearchEmployee.DataBind();
    }


    public void GetCenters(string Region_Id)
    {
        try
        {
            DataTable dt = new DataTable();

            BLLCenter objCenter = new BLLCenter();
            objCenter.Region_Id = Convert.ToInt32(Region_Id);

            dt = objCenter.CenterFetch(objCenter);

            objBase.FillDropDown(dt, ddlCenter, "Center_Id", "Center_Name");
        }
        catch (Exception oException)
        {
            throw oException;
        }

    }
    protected void gvSearchEmployee_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvSearchEmployee.Rows.Count > 0)
            {
                gvSearchEmployee.UseAccessibleHeader = false;
                gvSearchEmployee.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            objSearch.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
            objSearch.Center_Id = Convert.ToInt32(ddlCenter.SelectedValue);
            if (!String.IsNullOrEmpty(txtEmployeeCode.Text))
                objSearch.EmployeeCode = txtEmployeeCode.Text;
            else
                objSearch.EmployeeCode = "-";
            if (!String.IsNullOrEmpty(txtName.Text))
                objSearch.EmployeeName = txtName.Text;
            else
                objSearch.EmployeeName = "-";
            if (ddlGrade.SelectedIndex > 0)
                objSearch.EmployeeGrade = ddlGrade.SelectedItem.Text;
            else
                objSearch.EmployeeGrade = "";
            objSearch.InActive = ddlInActive.SelectedValue;
            objSearch.IsContracual = Convert.ToInt32(ddlIsContracual.SelectedValue);
            objSearch.Gender_Id = ddlGender.SelectedValue;
            if (ddlGrade.SelectedIndex > 0)
                objSearch.EmployeeGrade = ddlGrade.SelectedItem.Text;
            else
                objSearch.EmployeeGrade = "";
            if (ddlDesignation.SelectedIndex > 0)
                objSearch.DesigName = ddlDesignation.SelectedItem.Text;
            else
                objSearch.DesigName = "";
            if (ddlReligion.SelectedIndex > 0)
                objSearch.Religion_Name = ddlReligion.SelectedItem.Text;
            else
                objSearch.Religion_Name = "";
            if (ddlDept.SelectedIndex > 0)
                objSearch.DeptName = ddlDept.SelectedItem.Text;
            else
                objSearch.DeptName = "";
            BindSearchEmployee(objSearch);
        }
        catch (Exception oException)
        {
            throw oException;
        }

    }


    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnSearch_Click(this, EventArgs.Empty);
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }
    protected void txtName_TextChanged(object sender, EventArgs e)
    {

        try
        {
            btnSearch_Click(this, EventArgs.Empty);
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }


    //protected void btnPrintAtt_Click(object sender, EventArgs e)
    //{
    //    ImageButton btn = (ImageButton)sender;
    //    string emp=btn.CommandArgument;

    //    // Assuming CurrentMonth is stored as an integer (1-12)
    //    int currentMonth = Convert.ToInt32(Session["CurrentMonth"]);

    //    // Calculate the last two months
    //    int month1 = (currentMonth - 2) < 1 ? 12 + (currentMonth - 2) : currentMonth - 2;
    //    int month2 = (currentMonth - 1) < 1 ? 12 + (currentMonth - 1) : currentMonth - 1;

    //    // Create a comma-separated string of months with proper formatting
    //    string pmonth = "'" + month1 + "', '" + month2 + "', '" + currentMonth + "'";

    //    PrintReport.PrintReportMonthly(AMSReports.AttendanceReport, emp, pmonth, "~/SearchEmployee.aspx");
    //}

    //protected void btnPrintLog_Click(object sender, EventArgs e)
    //{
    //    ImageButton btn = (ImageButton)sender;
    //    string emp = btn.CommandArgument;

    //    // Assuming CurrentMonth is stored as an integer (1-12)
    //    int currentMonth = Convert.ToInt32(Session["CurrentMonth"]);

    //    // Calculate the last two months
    //    int month1 = (currentMonth - 2) < 1 ? 12 + (currentMonth - 2) : currentMonth - 2;
    //    int month2 = (currentMonth - 1) < 1 ? 12 + (currentMonth - 1) : currentMonth - 1;

    //    // Create a comma-separated string of months with proper formatting
    //    string pmonth = "'" + month1 + "', '" + month2 + "', '" + currentMonth + "'";

    //    PrintReport.PrintReportMonthly(AMSReports.EmployeeLogReport, emp, pmonth, "~/SearchEmployee.aspx");
    //}
    protected void btnPrintAtt_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string emp = btn.CommandArgument;

        PrintReport.PrintReportMonthly(AMSReports.AttendanceReport, emp, Session["CurrentMonth"].ToString(), "~/SearchEmployee.aspx");
    }


    protected void btnPrintLog_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string emp = btn.CommandArgument;

        PrintReport.PrintReportMonthly(AMSReports.EmployeeLogReport, emp, Session["CurrentMonth"].ToString(), "~/SearchEmployee.aspx");
    }
}
