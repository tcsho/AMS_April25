using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;

public partial class _Default : System.Web.UI.Page
{

    BLLAttendance bllObj = new BLLAttendance();
    DALBase objbase = new DALBase();
    BLLEmployeeLeaves bllObjLeaves = new BLLEmployeeLeaves();

    int countLeavesLockedRows = 0;
    int countHalfDayLockedRows = 0;
    int countReservationLockedRows = 0;
    int countMissingInOutLockedRows = 0;
    int countLateArrivalLockedRows = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx",false);
                return;
            }

           

            if (!IsPostBack)
            {
                try
                {

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
                    ViewState["SortDirection"] = "ASC";
                    loadMonths();

                if (Session["UserType"] != null)
                {
                    int _partType = Convert.ToInt32(Session["UserType"].ToString());
                    if (_partType == (int)UserTypes.HO_HR || _partType == (int)UserTypes.RO_HR || _partType == (int)UserTypes.CO_HR)
                    {
                        ddlDepartment.Visible = true;
                        ddlDepartment.SelectedIndex = 1;
                        ddlDepartment_SelectedIndexChanged(sender, e);
                        btn_viewPendingApprovals.Visible = true;
                    }
                    else
                    {
                        ddlDepartment.Visible = false;
                    }

                    if (_partType == (int)UserTypes.RO_HR)
                    {
                        btn_PendingAttSummary.Visible = true;
                    }

                    if (_partType == (int)UserTypes.HO_HR)
                    {
                        btn_pendingAtttendanceRegionWise.Visible = true;
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
        ddlMonths_SelectedIndexChanged(this, EventArgs.Empty);

        bindgrid();
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

    protected void loadDepartments()
    {

        BLLAttendance obj = new BLLAttendance();

        DataTable dt = new DataTable();

        obj.PMonthDesc = ddlMonths.SelectedValue;
        obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
        obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
        dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);
        objbase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");

        ddlDepartment.SelectedIndex = 0;

        ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);
        
    }
    protected void bindgrid()
    {
        gvLib.DataSource = null;
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString().Trim();
            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString().Trim());
            bllObj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);

            int UserLevel, UserType;

            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());

            if (UserLevel == 4) //Campus
            {
                bllObj.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
                bllObj.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
            }
            else if (UserLevel == 3 || UserLevel==5)//Region
            {
                bllObj.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
                bllObj.Center_Id = 0;
            }
            else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            {
                bllObj.Region_Id = 0;
                bllObj.Center_Id = 0;

            }

            if (ViewState["dtMain"] == null)
                _dt = bllObj.AttendanceFetchSummaryNew(bllObj);
            else
                _dt = (DataTable)ViewState["dtMain"];

            if (_dt.Rows.Count > 0)
            {
                gvLib.DataSource = _dt;
                ViewState["dtMain"] = _dt;
            }
            gvLib.DataBind();

            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }

    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dtMain"] = null;
        ViewState["dtMainShow"] = null;
        loadDepartments();
 
        ScriptManager.RegisterClientScriptBlock(this,this.GetType(), "test", "TableData();",true);
    }






    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            ViewState["dtMain"] = null;
            ViewState["dtMainShow"] = null;
            bindgrid();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "test", "TableData();", true);
        }
    }
    protected void btnPassChange_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {

        ImageButton imgbtn = (ImageButton)sender;
        
        BLLUser objbll = new BLLUser();
        objbll.User_Name = imgbtn.CommandArgument;
        objbll.Password = "123456";

        int AlreadyIn = objbll.UserUpdate(objbll);

        if (AlreadyIn == 0)
        {
            // drawMsgBox("Error to reset Password!",2);
            ADG.JQueryExtenders.Impromptu.ImpromptuHelper.ShowPrompt("Error to reset Password!");

        }
        else if (AlreadyIn == 1)
        {
            drawMsgBox("Password Changed Successfully.", 1);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //string repStr = "";
        string Employeecode;
        ImageButton btn = (ImageButton)sender;
        Employeecode = (btn.CommandArgument);
        PrintReport.PrintReportMonthly(AMSReports.AttendanceReport,Employeecode, ddlMonths.SelectedValue.ToString(), "~/Default.aspx");
    }


    protected void btnApp_Click(object sender, EventArgs e)
    {
        string repStr = "";
        string Employeecode;
        ImageButton btn = (ImageButton)sender;
        Employeecode = (btn.CommandArgument);
        ViewState["EmployeeCodeView"] = Employeecode;
    }



    protected void ddlAprvReservation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlAprc = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlAprc.Parent.Parent);


        if (ddlAprc.SelectedValue == "False")
        {
            //grdrDropDownRow.Cells[14].Enabled = true;
        }
        else
        {
            //grdrDropDownRow.Cells[14].Enabled = false;
            ddlAprc.SelectedValue = grdrDropDownRow.Cells[1].Text;
        }
    }




    private void setReservationSummary()
    {
        //int reservationTotal = gvReservationUnApproved.Rows.Count;
        //int reservationSubmitted = reservationTotal - countReservationLockedRows;
        //int reservationNotSubmitted = reservationTotal - reservationSubmitted;
        //int reservationApproved = gvReservationApproved.Rows.Count;
        //int reservationNotApproved = reservationSubmitted - reservationApproved;

        //reservationNotApproved = reservationNotApproved < 0 ? 0 : reservationNotApproved;
        //lblReservationTotal.Text = reservationTotal.ToString();
        //lblReservationSubmitted.Text = reservationSubmitted.ToString();
        //lblReservationNotSubmitted.Text = reservationNotSubmitted.ToString();
        //lblReservationApproved.Text = reservationApproved.ToString();
        //lblReservationNotApproved.Text = reservationNotApproved.ToString();
    }



    #region // Late Arrivals information section


    #endregion
    protected void gvLib_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //add the thead and tbody section programatically
            e.Row.TableSection = TableRowSection.TableHeader;
        }

        if (e.Row.RowIndex != -1)
        {


            ImageButton passwordreset = (ImageButton)e.Row.FindControl("btnPassReset");
            ImageButton Approve = (ImageButton)e.Row.FindControl("btnApp");
            int userType_Id = Convert.ToInt32(Session["UserType"].ToString());

            passwordreset.Visible = false;
            Approve.Visible = false;

            if (userType_Id==17 ||userType_Id==20 || userType_Id==23 ) // if Data Locked after ERP PRocesss
            {
                passwordreset.Visible = false;
                Approve.Visible = true;
            }

            if (userType_Id == 19 || userType_Id == 22 || userType_Id == 25) // if Data Locked after ERP PRocesss
            {
                passwordreset.Visible = true;
                Approve.Visible = false;
            }

        }



    }
    protected void btn_viewPendingApprovals_Click(object sender, EventArgs e)
    {


        string repStr = "";
        

        Session["RptTitle"] = "Monthly Pending Attendance Submission & Approvals";
        Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_PendingAttendanceStatusReport.rpt");
        Session["rep"] = "TCS_RO_PendingAttendanceStatusReport.rpt";
        
        Session["PMonth"] = this.ddlMonths.SelectedValue;

        Session["CriteriaRpt"] = "";

        Session["LastPage"] = "~/Default.aspx";
        Response.Redirect("~/TssCrystalReports.aspx");
    }


    protected void btn_PendingAttSummary_Click(object sender, EventArgs e)
    {


        string repStr = "";


        Session["RptTitle"] = "Monthly Pending Attendance Summary";
        Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_PendingAttendanceSummary.rpt");
        Session["rep"] = "TCS_RO_PendingAttendanceSummary.rpt";

        Session["PMonth"] = this.ddlMonths.SelectedValue;

        Session["CriteriaRpt"] = "";

        Session["LastPage"] = "~/Default.aspx";
        Response.Redirect("~/TssCrystalReports.aspx");
    }


    protected void btn_pendingAtttendanceRegionWise_Click(object sender, EventArgs e)
    {


        string repStr = "";


        Session["RptTitle"] = "Monthly Pending Attendance Summary Region Wise";
        Session["reppath"] = Server.MapPath("~/Reports/TCS_RO_PendingAttendanceRegionSummary.rpt");
        Session["rep"] = "TCS_RO_PendingAttendanceRegionSummary.rpt";

        Session["PMonth"] = this.ddlMonths.SelectedValue;

        Session["CriteriaRpt"] = "";

        Session["LastPage"] = "~/Default.aspx";
        Response.Redirect("~/TssCrystalReports.aspx");
    }


}
