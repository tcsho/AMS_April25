using ADG.JQueryExtenders.Impromptu;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeLeavesSubmissions : System.Web.UI.Page
{
    BLLAttendance bllObj = new BLLAttendance();
    DALBase objbase = new DALBase();
    int Curryear = Convert.ToInt32(DateTime.Now.Year);
    int Prevyear = Convert.ToInt32(DateTime.Now.Year) - 1;

    BLLEmployeeLeaves bllEmpLeaves = new BLLEmployeeLeaves();

    public int show_BDays = 0;
    public int show_PwExpiry = 0;

    protected void Page_Load(object sender, EventArgs e)
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
            if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }

            //Session["CurrentMonth"] = DateTime.Now

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

        if (Session["EmployeeCode"] != null)
        {
            if (Session["DOB"] != DBNull.Value && Session["DOB"] != "")
            {
                if (Convert.ToDateTime(Session["DOB"]).Month == DateTime.Today.Month && Convert.ToDateTime(Session["DOB"]).Day == DateTime.Today.Day)
                {
                    this.show_BDays = 1;
                }
            }

            if (Session["Password_Expiry_Date"] != DBNull.Value && Session["Password_Expiry_Date"] != "")
            {
                if (DateTime.Now >= Convert.ToDateTime(Session["Password_Expiry_Date"]))
                {
                    Console.WriteLine("30 days have passed since today.");
                    this.show_PwExpiry = 1;
                }
            }

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;

            int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

            if (_part_Id == 18 || _part_Id == 21 || _part_Id == 24)
            {
                TrEMP.Visible = true;                 
            }
            else
            {
                TrEMP.Visible = false; 
            }
             

            //int _result = objbase.ApplicationSettings(sRet, _part_Id);


            //if (_result == 1)
            //    {
            if (!IsPostBack)
            {
                try
                { 

                    //ViewState["mode"] = "Add";
                    ViewState["SortDirectionLate"] = "ASC";
                    ViewState["tMoodLate"] = "check";
                    ViewState["tMoodMIO"] = "check";

                    ViewState["tMoodLeaves"] = "check";

                    ViewState["HalfDaySortDirection"] = "ASC";
                    ViewState["HalfDaytMood"] = "check";

                    ViewState["mode"] = "Add";
                    ViewState["ReservationSortDirection"] = "ASC";



                    loadMonths();

                    bindgridLateArrivals();
                    bindgridMIO();
                    bindgridLeaves();
                    bindgridHalfDay();

                    bindLeavesPerYear();
                    //loadMonths();


                    //leavetypeOFT();
                    //leavetype();
                    bindgridReservation();
                    ResetControls();
                    lvdata.Visible = false;
                    lvbtn.Visible = false;



                    if (gvLateArrivals.Rows.Count <= 0)
                    {
                        //this.div_lteNotSubmitted.Attributes.Remove("class");


                    }

                    bllObj.EmployeeCode = Session["EmployeeCode"].ToString();
                    DataTable dtTimeIn = bllObj.GetTodaysTimeIn(bllObj);
                    lblTimeIn.Text = "Today's Time In:  " + dtTimeIn.Rows[0][0].ToString();



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



    #region Late-Missing


    protected void gvLateArrivals_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtLateArrivalsNotSubmitted"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLate"].ToString();

            if (ViewState["SortDirectionLate"].ToString() == "ASC")
            {
                ViewState["SortDirectionLate"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLate"] = "ASC";
            }
            ViewState["dtLateArrivalsNotSubmitted"] = null;
            bindgridLateArrivals();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLateArrivals_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLateArrivals.PageIndex = e.NewPageIndex;

            ViewState["dtLateArrivalsNotSubmitted"] = null;
            bindgridLateArrivals();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLateArrivalsSubmitted_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtLateArrivalsSubmitted"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLate"].ToString();

            if (ViewState["SortDirectionLate"].ToString() == "ASC")
            {
                ViewState["SortDirectionLate"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLate"] = "ASC";
            }
            ViewState["dtLateArrivalsSubmitted"] = null;
            bindgridLateArrivals();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLateArrivalsSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLateArrivalsSubmitted.PageIndex = e.NewPageIndex;

            ViewState["dtLateArrivalsSubmitted"] = null;
            bindgridLateArrivals();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLateArrivals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            if (e.Row.Cells[9].Text == "True")
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
            }

            DropDownList ddlAttendanceTypeLateArrival = (DropDownList)e.Row.FindControl("ddlAttendanceTypeLateArrival");

            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();

            DataTable objDt = new DataTable();

            objDt = objBLL.EmployeeAttendanceType();

            if (objDt.Rows.Count > 0)
            {
                ddlAttendanceTypeLateArrival.DataSource = objDt;
                ddlAttendanceTypeLateArrival.DataValueField = "AttendanceTypeId";
                ddlAttendanceTypeLateArrival.DataTextField = "AttendanceType";
                ddlAttendanceTypeLateArrival.DataBind();
                ddlAttendanceTypeLateArrival.Items.Insert(0, new ListItem("Select", "0"));
            }

        }

    }
    protected void gvLateArrivals_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMoodLate"].ToString();

                foreach (GridViewRow gvr in gvLateArrivals.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBox1");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMoodLate"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMoodLate"] = "check";
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



    protected void btnCopyLateArrivals_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnCopyLateArrivals = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnCopyLateArrivals.CommandArgument);

        GridViewRow grv = (GridViewRow)btnCopyLateArrivals.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        string _reason;
        _reason = txtReason.Text;


        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        TextBox txtReasonInner = null;

        foreach (GridViewRow gvRow in gvLateArrivals.Rows)
        {
            int _index = gvRow.RowIndex;
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");
            if (cb.Checked)
            {
                txtReasonInner.Text = _reason;
                cb.Checked = false;
            }
        }
        ViewState["SortDirectionLate"] = "ASC";
        ViewState["tMoodLate"] = "check";
    }

    protected void btnCopyMIO_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnCopyMIO = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnCopyMIO.CommandArgument);

        GridViewRow grv = (GridViewRow)btnCopyMIO.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        string _reason;
        _reason = txtReason.Text;


        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        TextBox txtReasonInner = null;

        foreach (GridViewRow gvRow in gvMIO.Rows)
        {
            int _index = gvRow.RowIndex;
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");
            if (cb.Checked)
            {
                txtReasonInner.Text = _reason;
                cb.Checked = false;
            }
        }
        ViewState["SortDirectionLate"] = "ASC";
        ViewState["tMoodMIO"] = "check";
    }
    protected void drawMsgBox(string msg, int errType)
    {


        ImpromptuHelper.ShowPrompt(msg);

    }
    protected void bindgridLateArrivals()
    {
        int total = 0;
        int not_submitted = 0;
        int submitted = 0;

        gvLateArrivals.DataSource = null;
        gvLateArrivalsSubmitted.DataSource = null;
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObj.NegAttSubmit2HOD = false;
            bllObj.isLock = false;

            if (ViewState["dtLateArrivalsNotSubmitted"] == null)
                _dt = bllObj.AttendanceSelectNegEmpLate(bllObj);
            else
                _dt = (DataTable)ViewState["dtLateArrivalsNotSubmitted"];

            if (_dt.Rows.Count > 0)
            {
                gvLateArrivals.DataSource = _dt;
                ViewState["dtLateArrivalsNotSubmitted"] = _dt;
                div_lteNotSubmitted.Visible = true;
            }
            else
            {
                div_lteNotSubmitted.Visible = false;
            }

            gvLateArrivals.DataBind();

            lblLateArrivalsNotSubmitted.Text = gvLateArrivals.Rows.Count.ToString();

            //Show Read Only Grid 
            bllObj.NegAttSubmit2HOD = true;
            bllObj.isLock = true;
            if (ViewState["dtLateArrivalsSubmitted"] == null)
                _dt = bllObj.AttendanceSelectNegEmpLate(bllObj);
            else
                _dt = (DataTable)ViewState["dtLateArrivalsSubmitted"];

            if (_dt.Rows.Count > 0)
            {
                gvLateArrivalsSubmitted.DataSource = _dt;
                ViewState["dtLateArrivalsSubmitted"] = _dt;

                div_lteSubmitted.Visible = true;
            }
            else
            {
                div_lteSubmitted.Visible = false;
            }
            gvLateArrivalsSubmitted.DataBind();

            lblLateArrivalsSubmitted.Text = gvLateArrivalsSubmitted.Rows.Count.ToString();

            lblLateArrivalsTotal.Text = (gvLateArrivalsSubmitted.Rows.Count + gvLateArrivals.Rows.Count).ToString();


            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }

    protected void ResetAllGrids()
    {

        ViewState["gvReservations"] = null;
        bindgridReservation();

        ViewState["dtLeavesSubmitted"] = null;
        ViewState["dtLeavesNotSubmitted"] = null;
        bindgridLeaves();


        ViewState["dtLateArrivalsNotSubmitted"] = null;
        ViewState["dtLateArrivalsSubmitted"] = null;
        bindgridLateArrivals();

        ViewState["dtMIOSubmitted"] = null;
        ViewState["dtMIONotSubmitted"] = null;
        bindgridMIO();


        ViewState["dtHalfDaysNotSubmitted"] = null;
        ViewState["dtHalfDaysSubmitted"] = null;
        bindgridHalfDay();

    }

    protected void bindgridMIO()
    {
        gvMIO.DataSource = null;
        gvMIOSubmitted.DataSource = null;
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();
            bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();

            bllObj.MIOAttSubmit2HOD = false;
            bllObj.isLock = false;
            if (ViewState["dtMIONotSubmitted"] == null)
                _dt = bllObj.AttendanceSelectNegEmpMIO(bllObj);
            else
                _dt = (DataTable)ViewState["dtMIONotSubmitted"];

            if (_dt.Rows.Count > 0)
            {
                gvMIO.DataSource = _dt;
                ViewState["dtMIONotSubmitted"] = _dt;

                div_missingNotSubmitted.Visible = true;
            }
            else
            {
                div_missingNotSubmitted.Visible = false;
            }

            gvMIO.DataBind();

            lblMissingInOutnotSubmitted.Text = gvMIO.Rows.Count.ToString();

            bllObj.MIOAttSubmit2HOD = true;
            bllObj.isLock = false;
            if (ViewState["dtMIOSubmitted"] == null)
                _dt = bllObj.AttendanceSelectNegEmpMIO(bllObj);
            else
                _dt = (DataTable)ViewState["dtMIOSubmitted"];

            if (_dt.Rows.Count > 0)
            {
                gvMIOSubmitted.DataSource = _dt;
                ViewState["dtMIOSubmitted"] = _dt;

                div_missingSubmitted.Visible = true;
            }
            else
            {
                div_missingSubmitted.Visible = false;
            }
            gvMIOSubmitted.DataBind();

            lblMissingInOutSubmitted.Text = gvMIOSubmitted.Rows.Count.ToString();


            lblMissingInOutTotal.Text = (gvMIO.Rows.Count + gvMIOSubmitted.Rows.Count).ToString();
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
        ViewState["dtLateArrivalsNotSubmitted"] = null;
        ViewState["dtLateArrivalsSubmitted"] = null;

        ViewState["dtMIONotSubmitted"] = null;
        ViewState["dtMIOSubmitted"] = null;

        ViewState["dtLeavesNotSubmitted"] = null;
        ViewState["dtLeavesSubmitted"] = null;


        bindgridLeaves();
        bindgridLateArrivals();
        bindgridMIO();


        ViewState["dtHalfDaysNotSubmitted"] = null;
        ViewState["dtHalfDaysSubmitted"] = null;
        bindgridHalfDay();

        ViewState["gvReservations"] = null;
        bindgridReservation();
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{

    //    ImageButton btnEdit = (ImageButton)sender;
    //    int _id = Convert.ToInt32(btnEdit.CommandArgument);

    //    GridViewRow grv = (GridViewRow)btnEdit.NamingContainer;

    //    Control ctl = (Control)grv.FindControl("txtReason");
    //    TextBox txtReason = (TextBox)ctl;



    //    string _reason;

    //    _reason = txtReason.Text;

    //    if (_reason != string.Empty)
    //    {

    //        bllObj.Att_Id = _id;
    //        bllObj.NegEmpReason = txtReason.Text;
    //        bllObj.ModifyBy = Session["EmployeeCode"].ToString();
    //        bllObj.ModifyDate = DateTime.Now;
    //        bllObj.EmpLvSubDate = DateTime.Now;
    //        bllObj.NegAttSubmit2HOD = true;

    //        int dt = bllObj.AttendanceUpdateEmpNeg(bllObj);
    //        if (dt >= 1)
    //        {
    //            //drawMsgBox("Leave Submitted to HOD Successfully!");
    //            ViewState["dtLateArrivalsNotSubmitted"] = null;
    //            ViewState["dtLateArrivalsSubmitted"] = null;
    //            bindgridLateArrivals();
    //            bindgridMIO();
    //        }
    //    }
    //    else
    //    {
    //        drawMsgBox("Please Must Enter Leave Reason", 3);
    //    }

    //}
    protected void btnLateArrivalsSubmit_Click(object sender, EventArgs e)
    {

        int chk = 0;
        CheckBox cb = null;
        TextBox txtReasonInner = null;
        DropDownList ddlAttendanceTypeLateArrival = null;
        foreach (GridViewRow gvRow in gvLateArrivals.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlAttendanceTypeLateArrival = (DropDownList)gvRow.FindControl("ddlAttendanceTypeLateArrival");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");

            if (cb.Checked)
            {

                if (txtReasonInner.Text != string.Empty)
                {

                    bllObj.Att_Id = _id;
                    bllObj.NegEmpReason = txtReasonInner.Text;
                    bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                    bllObj.ModifyDate = DateTime.Now;
                    bllObj.EmpLvSubDate = DateTime.Now;
                    bllObj.NegAttSubmit2HOD = true;
                    bllObj.AttendanceTypeId = Convert.ToInt32(ddlAttendanceTypeLateArrival.SelectedValue);
                    int dt = bllObj.AttendanceUpdateEmpNeg(bllObj);
                    if (dt > 0)
                    {
                        chk = chk + 1;
                    }
                }
            }
        }

        if (chk > 0)
        {
            //  drawMsgBox("Data Saved!", 3);
            ViewState["dtLateArrivalsNotSubmitted"] = null;
            ViewState["dtLateArrivalsSubmitted"] = null;
            ViewState["SortDirectionLate"] = "ASC";
            ViewState["tMoodLate"] = "check";
            bindgridLateArrivals();
            bindgridMIO();
        }
    }

    protected void btnMissingInOutSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int chk = 0;
        CheckBox cb = null;
        TextBox txtReasonInner = null;
        DropDownList ddlAttendanceMIO = null;

        foreach (GridViewRow gvRow in gvMIO.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlAttendanceMIO = (DropDownList)gvRow.FindControl("ddlAttendanceTypeMissingInOut");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");

            if (cb.Checked)
            {

                if (txtReasonInner.Text != string.Empty && ddlAttendanceMIO.SelectedValue != "0")
                {

                    bllObj.Att_Id = _id;
                    bllObj.MIOEmpReason = txtReasonInner.Text;
                    bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                    bllObj.ModifyDate = DateTime.Now;
                    bllObj.EmpLvSubDate = DateTime.Now;
                    bllObj.MIOAttSubmit2HOD = true;
                    bllObj.AttendanceTypeId = Convert.ToInt32(ddlAttendanceMIO.SelectedValue);
                    int dt = bllObj.AttendanceUpdateEmpNegMIO(bllObj);
                    if (dt > 0)
                    {
                        chk = chk + 1;
                    }
                }
                else
                {
                    drawMsgBox("Attendance Type and Reason are mandatory fields.", 3);
                }
            }
        }

        if (chk >= 1)
        {
            drawMsgBox("Saved Successfully!", 3);


            ViewState["dtMIONotSubmitted"] = null;
            ViewState["dtMIOSubmitted"] = null;
            ViewState["SortDirectionLate"] = "ASC";
            ViewState["tMoodMIO"] = "check";
            bindgridMIO();
        }
    }catch(Exception ex)
    {
            // Log the exception or handle it appropriately
            throw new Exception("An error occurred: " + ex.Message);
        }

    }




    //protected void ddlRoleType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList ddlCurrentDropDownList = (DropDownList)sender;
    //    GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
    //}



    //-----------------
    protected void gvMIO_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtMIONotSubmitted"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLate"].ToString();

            if (ViewState["SortDirectionLate"].ToString() == "ASC")
            {
                ViewState["SortDirectionLate"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLate"] = "ASC";
            }
            ViewState["dtMIONotSubmitted"] = null;
            bindgridLateArrivals();
            bindgridMIO();
        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvMIO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMIO.PageIndex = e.NewPageIndex;

            ViewState["dtMIONotSubmitted"] = null;
            bindgridMIO();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvMIOSubmitted_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtMIOSubmitted"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLate"].ToString();

            if (ViewState["SortDirectionLate"].ToString() == "ASC")
            {
                ViewState["SortDirectionLate"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLate"] = "ASC";
            }
            ViewState["dtMIOSubmitted"] = null;
            bindgridMIO();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvMIOSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLateArrivalsSubmitted.PageIndex = e.NewPageIndex;

            ViewState["dtMIOSubmitted"] = null;
            bindgridMIO();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvMIO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            if (e.Row.Cells[9].Text == "True")
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
            }

            DropDownList ddlAttendanceTypeLateArrival = (DropDownList)e.Row.FindControl("ddlAttendanceTypeMissingInOut");

            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();

            DataTable objDt = new DataTable();

            objDt = objBLL.EmployeeAttendanceType();

            if (objDt.Rows.Count > 0)
            {
                ddlAttendanceTypeLateArrival.DataSource = objDt;
                ddlAttendanceTypeLateArrival.DataValueField = "AttendanceTypeId";
                ddlAttendanceTypeLateArrival.DataTextField = "AttendanceType";
                ddlAttendanceTypeLateArrival.DataBind();
                ddlAttendanceTypeLateArrival.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
    }
    protected void gvMIO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMoodMIO"].ToString();

                foreach (GridViewRow gvr in gvMIO.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBox1");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMoodMIO"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMoodMIO"] = "check";
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

    #endregion

    #region Leaves Submission
    protected void gvLeavesNotSubmitted_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtLeavesNotSubmitted"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLeaves"].ToString();

            if (ViewState["SortDirectionLeaves"].ToString() == "ASC")
            {
                ViewState["SortDirectionLeaves"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLeaves"] = "ASC";
            }
            ViewState["dtLeavesNotSubmitted"] = null;
            bindgridLeaves();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLeavesNotSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLeavesNotSubmitted.PageIndex = e.NewPageIndex;

            ViewState["dtLeavesNotSubmitted"] = null;
            bindgridLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void gvLeavesSubmitted_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtLeavesSubmitted"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLeaves"].ToString();

            if (ViewState["SortDirectionLeaves"].ToString() == "ASC")
            {
                ViewState["SortDirectionLeaves"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLeaves"] = "ASC";
            }
            ViewState["dtLeavesSubmitted"] = null;
            bindgridLeaves();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLeavesSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLeavesSubmitted.PageIndex = e.NewPageIndex;

            ViewState["dtLeavesSubmitted"] = null;
            bindgridLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void gvLeavesNotSubmitted_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {
            DropDownList ddlRoleType = (DropDownList)e.Row.FindControl("ddlRoleType");
            //DropDownList ddlAttendanceType = (DropDownList)e.Row.FindControl("ddlAttendanceType");
            TextBox txtReason = (TextBox)e.Row.FindControl("txtReason");
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            objBLL.Status_id = 1;
            objBLL.Used_For = "LVS";
            DataTable objDt = new DataTable();
            //DataTable attendanceTypeDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);
            ddlRoleType.DataSource = objDt;
            ddlRoleType.DataValueField = "LeaveType_Id";
            ddlRoleType.DataTextField = "LeaveType";
            ddlRoleType.DataBind();
            ddlRoleType.Items.Insert(0, new ListItem("Select", "0"));

            //attendanceTypeDt = objBLL.EmployeeAttendanceType();
            //ddlAttendanceType.DataSource = attendanceTypeDt;
            //ddlAttendanceType.DataValueField = "AttendanceTypeId";
            //ddlAttendanceType.DataTextField = "AttendanceType";
            //ddlAttendanceType.DataBind();
            //ddlAttendanceType.Items.Insert(0, new ListItem("Select", "0"));


            //CheckBox ch = (CheckBox)e.Row.FindControl("CheckBox1");
            //ch.Checked = false;

            if (e.Row.Cells[13].Text == "True") // if Data Locked after ERP PRocesss
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
            }
            else
            {
                if (e.Row.Cells[11].Text != "&nbsp;")
                {
                    ddlRoleType.SelectedValue = e.Row.Cells[11].Text;
                    //ddlAttendanceType.SelectedValue = e.Row.Cells[12].Text;
                    txtReason.Text = e.Row.Cells[12].Text;
                }
                else
                {
                    ddlRoleType.SelectedValue = "0";
                    //ddlAttendanceType.SelectedValue = "0";
                }

                int R = Convert.ToInt32(e.Row.Cells[14].Text);
                int G = Convert.ToInt32(e.Row.Cells[15].Text);
                int B = Convert.ToInt32(e.Row.Cells[16].Text);

                ////string htmlString = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(R, G, B));


                ////e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(htmlString);
                //                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");





                if (e.Row.Cells[1].Text == "1") //Off Day
                {
                    e.Row.BackColor = System.Drawing.Color.Wheat;
                    e.Row.Cells[7].Enabled = false;
                    e.Row.Cells[8].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                }


            }



            //       e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");


        }
    }

    protected void btnCopyleaves_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnCopyleaves = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnCopyleaves.CommandArgument);

        GridViewRow grv = (GridViewRow)btnCopyleaves.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlRoleType");
        DropDownList ddlLeave = (DropDownList)ctl;


        string _reason;
        string _leave;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;


        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        TextBox txtReasonInner = null;

        if (_leave == ((int)LeaveTypes.CasualLeaves).ToString())   /*Casual Leaves*/
        {


            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                int _index = gvRow.RowIndex;
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                if (cb.Checked)
                {
                    //Check here for balances
                    float _valCasul = 0;
                    _valCasul = LeaveBalanceCounter(((int)LeaveTypes.CasualLeaves).ToString());
                    //if (_valCasul >= Convert.ToInt32(lblCas.Text))
                    //{
                    //    ddlLeaveInner.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                    //    txtReasonInner.Text = _reason;
                    //    cb.Checked = false;
                    //}
                    //else
                    {
                        ddlLeaveInner.SelectedValue = _leave;
                        txtReasonInner.Text = _reason;
                        cb.Checked = false;
                    }
                }
            }
        }
        else if (_leave == ((int)LeaveTypes.AnnualLeaves).ToString()) /*Anual Leaves*/
        {
            drawMsgBox("Copy Not Allowed for this Leave Type!", 3);
            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                cb.Checked = false;
            }
            #region 'Policy'
            /* foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
                {
                int _index = gvRow.RowIndex;
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                if (cb.Checked)
                    {
                    int _valCasul = 0;
                    int _valAnual = 0;
                    int _valisAnl = 0;
                    _valisAnl = objbase.IsAnual(gvRow.Cells[2].Text);
                    _valAnual = LeaveBalanceCounter(((int)LeaveTypes.AnnualLeaves).ToString());

                    if (Convert.ToInt32(lblCas.Text) < 3)
                        {
                        if (_valCasul < Convert.ToInt32(lblCas.Text))
                            {
                            ddlLeaveInner.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                            cb.Checked = false;
                            }
                        else
                            {
                            ddlLeaveInner.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                            cb.Checked = false;
                            }
                        }
                    else
                        {
                        if (_valisAnl < 3)
                            {
                            if (_valCasul < Convert.ToInt32(lblCas.Text))
                                {
                                ddlLeaveInner.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                                cb.Checked = false;
                                }
                            else
                                {
                                ddlLeaveInner.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                                cb.Checked = false;
                                }
                            }
                        else
                            {

                            if (_valAnual >= Convert.ToInt32(lblAnu.Text))
                                {
                                //Cehck for Casul Balance then Assign
                                if (_valCasul < Convert.ToInt32(lblCas.Text))
                                    {
                                    ddlLeaveInner.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                                    cb.Checked = false;
                                    }
                                else
                                    {
                                    ddlLeaveInner.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                                    cb.Checked = false;
                                    }
                                }
                            else
                                {
                                ddlLeaveInner.SelectedValue = _leave;
                                txtReasonInner.Text = _reason;
                                cb.Checked = false;
                                }
                            }

                        }
                    } //end of check box if 

                } // end of for loop
*/
            #endregion
        }
        else if (_leave == ((int)LeaveTypes.OffDay).ToString()) /*Off Day*/
        {
            drawMsgBox("Copy Not Allowed for this Leave Type!", 3);
            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                cb.Checked = false;
            }

        }
        else if (_leave == ((int)LeaveTypes.Present).ToString()) /*Present*/
        {
            AssignToAll(_leave, _reason, ddlLeave.SelectedItem.ToString());
        }
        else if (_leave == ((int)LeaveTypes.OfficialTourTask).ToString()) /*Offical Tour*/
        {
            AssignToAll(_leave, _reason, ddlLeave.SelectedItem.ToString());
        }
        else if (_leave == ((int)LeaveTypes.ManualAttendancePunchCard).ToString()) /*Manual Punch Card*/
        {
            AssignToAll(_leave, _reason, ddlLeave.SelectedItem.ToString());
        }
        else if (_leave == ((int)LeaveTypes.Lieuof).ToString()) /*Lieu Off*/
        {
            drawMsgBox("Copy Not Allowed for this Leave Type!", 3);
            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                cb.Checked = false;
            }

        }

        else if (_leave == "0") /*Select*/
        {
            drawMsgBox("Copy Not Allowed for this Leave Type!", 3);
            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                cb.Checked = false;
            }

        }

        else if (_leave == ((int)LeaveTypes.SpecialLeaves).ToString()) /*Special Leave*/
        {
            AssignToAll(_leave, _reason, ddlLeave.SelectedItem.ToString());
        }
        else if (_leave == ((int)LeaveTypes.LeaveWithoutPay).ToString()) /*Leave Without Pay*/
        {
            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                int _index = gvRow.RowIndex;
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                if (cb.Checked)
                {
                    ddlLeaveInner.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                    txtReasonInner.Text = _reason;
                    cb.Checked = false;
                }
            }
        }
        else if (_leave == ((int)LeaveTypes.FurloughLeave).ToString())   /*Furlough Leave*/
        {
            int counter = 1;

            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                int _index = gvRow.RowIndex;
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                if (cb.Checked)
                {
                    //Check here for balances
                    float Flbalance = 0;
                    Flbalance = CheckFurloughLeaveBalance();
                    if (Flbalance < counter)
                    {
                        ddlLeaveInner.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                        counter += 1;
                    }
                    else
                    {
                        counter += 1;
                        ddlLeaveInner.SelectedValue = _leave;
                    }
                    txtReasonInner.Text = _reason;
                    cb.Checked = false;
                }
            }
        }
        ViewState["SortDirectionLeaves"] = "ASC";
        ViewState["tMoodLeaves"] = "check";

    }
    protected void AssignToAll(string _lvt, string _reas, string _cvr)
    {
        DropDownList ddlLeaveInner = new DropDownList();
        TextBox txtReasonInner = new TextBox();
        CheckBox cb = new CheckBox();
        foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");
            if (cb.Checked)
            {
                if (gvRow.Cells[1].Text == "1")
                {
                    ddlLeaveInner.SelectedValue = ((int)LeaveTypes.OffDay).ToString();
                    txtReasonInner.Text = "System attachement void, after submission of " + _cvr + ".";
                    cb.Checked = false;
                }
                else
                {
                    ddlLeaveInner.SelectedValue = _lvt;
                    txtReasonInner.Text = _reas;
                    cb.Checked = false;
                }
            }
        }

    }
    protected void bindgridLeaves()
    {
        loadEmpleave();
        gvLeavesNotSubmitted.DataSource = null;
        gvLeavesSubmitted.DataSource = null;
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObj.Submit2HOD = false;

            if (ViewState["dtLeavesNotSubmitted"] == null)
                _dt = bllObj.AttendanceFetch(bllObj);
            else
                _dt = (DataTable)ViewState["dtLeavesNotSubmitted"];

            if (_dt.Rows.Count > 0)
            {
                gvLeavesNotSubmitted.DataSource = _dt;
                ViewState["dtLeavesNotSubmitted"] = _dt;

                div_leavesNotSubmitted.Visible = true;
            }
            else
            {
                div_leavesNotSubmitted.Visible = false;
            }
            gvLeavesNotSubmitted.DataBind();



            //Show Read Only Grid 
            bllObj.Submit2HOD = true;
            if (ViewState["dtLeavesSubmitted"] == null)
                _dt = bllObj.AttendanceFetch(bllObj);
            else
                _dt = (DataTable)ViewState["dtLeavesSubmitted"];

            if (_dt.Rows.Count > 0)
            {
                gvLeavesSubmitted.DataSource = _dt;
                ViewState["dtLeavesSubmitted"] = _dt;

                div_leavesSubmitted.Visible = true;
            }
            else
            {
                div_leavesSubmitted.Visible = false;
            }
            gvLeavesSubmitted.DataBind();

            lblLeavesNotSubmitted.Text = gvLeavesNotSubmitted.Rows.Count.ToString();
            lblLeavesSubmitted.Text = gvLeavesSubmitted.Rows.Count.ToString();
            lblLeavesTotal.Text = (gvLeavesNotSubmitted.Rows.Count + gvLeavesSubmitted.Rows.Count).ToString();


            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }

    protected void bindLeavesPerYear()
    {
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            _dt = bllObj.EmployeeAvailedAndBalanceLeaveForTheYear(bllObj);

            //Show Read Only Grid 
            if (_dt.Rows.Count > 0)
            {
                availedLeaves.Text = _dt.Rows[0]["Availed"].ToString();
                balanceLeaves.Text = _dt.Rows[0]["BalanceLeaves"].ToString();
            }

            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }
    protected void btnLeavesSave_Click(object sender, EventArgs e)
    {
        CasualLeavesCriteria();
        //FurloughLeavesCriteria();

    }
    private void FurloughLeavesCriteria()
    {
        string maildts = "";

        BLLSendEmail bllemail = new BLLSendEmail();

        string _gp;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAttendanceType = null;
        TextBox txtReasonInner = null;
        Control ctl = null;

        int counter = 0;
        int Flcounter = 0;
        int errorNumber = -1;
        int checkedCount = 0;


        float Flbalance = 0;
        Flbalance = CheckFurloughLeaveBalance();

        foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");

            if (cb.Checked)
            {
                checkedCount += 1;
                if (txtReasonInner.Text != string.Empty && ddlLeaveInner.SelectedValue != "0")
                {
                    if (txtReasonInner.Text != string.Empty && ddlLeaveInner.SelectedValue == ((int)LeaveTypes.FurloughLeave).ToString())
                    {
                        Flcounter += 1;
                    }
                    counter += 1;
                }
            }
        }

        if (checkedCount > Flbalance)
        {
            errorNumber = 1;
        }

        if (counter == 0)
        {
            errorNumber = -1;
        }
        else if (counter > 0 && Flcounter <= Flbalance) //Submit Leaves if something to submit and furlough leaves allowed
        {
            errorNumber = 0;
        }
        else if (counter > 0 && Flbalance < 0)
        {
            errorNumber = 0;
        }
        else
        {
            errorNumber = 1;
        }




        if (errorNumber == 0)
        {
            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
                ddlAttendanceType = (DropDownList)gvRow.FindControl("ddlAttendanceType");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");

                if (cb.Checked)
                {
                    if (txtReasonInner.Text != string.Empty && ddlLeaveInner.SelectedValue != "0")
                    {

                        bllObj.Att_Id = _id;
                        bllObj.EmpLvType_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                        //bllObj.AttTypeId = Convert.ToInt32(ddlAttendanceType.SelectedValue);
                        bllObj.EmpLvReason = txtReasonInner.Text;
                        bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                        bllObj.ModifyDate = DateTime.Now;
                        bllObj.EmpLvSubDate = DateTime.Now;
                        bllObj.Submit2HOD = true;

                        int dt = bllObj.AttendanceUpdateEmpLeave(bllObj);
                    }
                }
            }

            drawMsgBox("Successfully Submitted to HOD.", 1);
            ViewState["dtLeavesNotSubmitted"] = null;
            ViewState["dtLeavesSubmitted"] = null;
            bindgridLeaves();
        }
        else if (errorNumber == -1)
        {
            drawMsgBox("No Action Performed.", -1);

        }
        else if (errorNumber == 1)
        {
            drawMsgBox("your Furlough leaves entitlement exceeds.", 1);
        }


    }

    private void CasualLeavesCriteria()
    {
        string maildts = "";

        BLLSendEmail bllemail = new BLLSendEmail();

        string _gp;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAttendanceType = null;
        TextBox txtReasonInner = null;
        Control ctl = null;

        int counter = 0;
        int errorNumber = -1;





        if (checkLastYearsCasualBalance(((int)LeaveTypes.CasualLeaves)))
        {


            foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
            {
                int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
                ddlAttendanceType = (DropDownList)gvRow.FindControl("ddlAttendanceType");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");

                if (cb.Checked)
                {

                    if (txtReasonInner.Text != string.Empty && ddlLeaveInner.SelectedValue != "0")
                    {
                        /*is clear group*/
                        _gp = gvRow.Cells[2].Text;

                        bool _bl = true;//isClearGroup(_gp);

                        if (_bl == true)
                        {
                            bllObj.Att_Id = _id;
                            bllObj.EmpLvType_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                            //bllObj.AttTypeId = Convert.ToInt32(ddlAttendanceType.SelectedValue);
                            bllObj.EmpLvReason = txtReasonInner.Text;
                            bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                            bllObj.ModifyDate = DateTime.Now;
                            bllObj.EmpLvSubDate = DateTime.Now;
                            bllObj.Submit2HOD = true;

                            int dt = bllObj.AttendanceUpdateEmpLeave(bllObj);
                            errorNumber = 0;
                            if (dt >= 1)
                            {
                                //

                                BLLEmployeeLeaveType bllempty = new BLLEmployeeLeaveType();
                                bllempty.LeaveType_Id = bllObj.EmpLvType_Id;
                                //bllempty.AttendanceTypeId = bllObj.AttTypeId;
                                DataTable _dtlt = bllempty.EmployeeLeaveTypeFetchByID(bllempty);
                                if (_dtlt.Rows.Count > 0)
                                {
                                    maildts = maildts + "<br>     [" + gvRow.Cells[4].Text + "]---[" + _dtlt.Rows[0]["LeaveType"].ToString() + "]---[" + txtReasonInner.Text + "]";
                                }
                                counter = counter + 1;

                            }
                        }
                        else
                        {
                            cb.Checked = false;
                            errorNumber = 1;
                            //drawMsgBox("Please submit All leaves at Once.", 3);
                        }
                    }
                    else
                    {
                        cb.Checked = false;
                        errorNumber = 2;
                        //drawMsgBox("leave Type and Reason are mandatory fields.", 3);
                    }
                }
            }
            if (counter > 0)
            {
                DataTable _dtemp = new DataTable();
                _dtemp = (DataTable)Session["EmailTable"];
                if (_dtemp.Rows.Count > 0)
                {
                    foreach (DataRow var in _dtemp.Rows)
                    {
                        string mailMsg = "Dear " + var["HODName"].ToString() + ", <br><br> Employee # : " + Session["EmployeeCode"].ToString() + ".<br> Name :" + Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString() + "<br> Submitted leaves for HOD approval. You can find data in HOD Leave Approval.<br><br>      [Date]---[EmplyeeLeave]---[Reason]";
                        mailMsg = mailMsg + maildts;
                        bllemail.SendEmail(var["HODEmail"].ToString(), "Attendance [Leave(s) Approval]", mailMsg);
                    }

                }



            }

            if (errorNumber == 0)
            {
                drawMsgBox("Successfully Submitted to HOD.", 1);
                ViewState["dtLeavesNotSubmitted"] = null;
                ViewState["dtLeavesSubmitted"] = null;
                bindgridLeaves();
            }
            else if (errorNumber == -1)
            {
                drawMsgBox("No Action Performed.", 3);

            }
            else if (errorNumber == 1)
            {
                drawMsgBox("Please submit All leaves at Once.", 3);
            }
            else if (errorNumber == 2)
            {
                drawMsgBox("leave Type and Reason are mandatory fields.", 3);

            }
        }
    }

    private bool checkLastYearsCasualBalance(int leaveTypeId)
    {
        BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

        DataTable dt = new DataTable();
        obj.EmployeeCode = Session["EmployeeCode"].ToString();
        obj.Year = Prevyear.ToString();

        dt = obj.EmployeeLeaveBalanceFetch_LastYears(obj);

        if (dt.Rows.Count > 0)
        {
            DropDownList ddlLeaveInner = null;
            CheckBox cb = null;

            if (leaveTypeId == ((int)LeaveTypes.CasualLeaves))
            {
                if (!String.IsNullOrEmpty(txtDays.Text) && !String.IsNullOrEmpty(txtFromDate.Text) && !String.IsNullOrEmpty(txtToDate.Text))
                {
                    if ((Convert.ToDateTime(txtToDate.Text) <= new DateTime(Prevyear, 12, 31)))
                    {
                        if (Convert.ToDouble(dt.Rows[0]["TCasualLeave"].ToString()) <= Convert.ToDouble(txtDays.Text))
                        {
                            return false;
                        }
                    }
                }
                int count = 0;

                foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
                {
                    cb = (CheckBox)gvRow.FindControl("CheckBox1");

                    if (cb.Checked)
                    {

                        DateTime dtAttDate = DateTime.ParseExact(gvRow.Cells[4].Text.ToString(), "dddd dd/MM/yyyy", CultureInfo.InvariantCulture);

                        if (dtAttDate >= new DateTime(Prevyear, 12, 26) && dtAttDate <= new DateTime(Prevyear, 12, 31))
                        {

                            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
                            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");

                            if (ddlLeaveInner.SelectedValue == ((int)LeaveTypes.CasualLeaves).ToString())
                            {
                                count++;
                            }
                        }
                    }
                }
                string i = dt.Rows[0]["TCasualLeave"].ToString();
                if (Convert.ToDouble(dt.Rows[0]["TCasualLeave"]) < count && count != 0)
                {
                    //drawMsgBox("Cann't exceed Casual balance (i.e. " + dt.Rows[0]["TCasualLeave"] + ") between dates '26th December 2014' and '31st December 2014'. you can submit Leave Without pay.", 0);
                    drawMsgBox("you have no Casual balance till '31st December " + Prevyear.ToString() + "'. you can submit Leave Without pay.", 0);
                    return false;
                }
            }
            else if (leaveTypeId == ((int)LeaveTypes.HalfCasualLeave))
            {

                float count = 0;

                foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
                {
                    cb = (CheckBox)gvRow.FindControl("CheckBox1");

                    if (cb.Checked)
                    {
                        DateTime dtAttDate = DateTime.ParseExact(gvRow.Cells[4].Text.ToString(), "dddd dd/MM/yyyy", CultureInfo.InvariantCulture);

                        if (dtAttDate >= new DateTime(Prevyear, 12, 26) && dtAttDate <= new DateTime(Prevyear, 12, 31))
                        {

                            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
                            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeHalfDays");

                            if (ddlLeaveInner.SelectedValue == ((int)LeaveTypes.HalfCasualLeave).ToString())
                            {
                                count++;
                            }
                        }
                    }
                }

                float hlf = (count / 2);

                if (Convert.ToDouble(dt.Rows[0]["TCasualLeave"]) < hlf && hlf != 0)
                {
                    drawMsgBox("you have no Casual balance till '31st December " + Prevyear.ToString() + "'. you can submit Half Leave Without pay.", 0);
                    return false;
                }
            }
        }

        return true;
    }


    private bool checkLastYearsAnnualBalance(int leaveTypeId, DateTime dtFrom, DateTime dtTo)
    {
        BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

        DataTable dt = new DataTable();
        obj.EmployeeCode = Session["EmployeeCode"].ToString();
        obj.Year = Prevyear.ToString();

        dt = obj.EmployeeLeaveBalanceFetch_LastYears(obj);

        if (dt.Rows.Count > 0)
        {
            if (leaveTypeId == 62)
            {


                int count = 0;

                for (DateTime day = dtFrom.Date; day.Date <= dtTo.Date; day = day.AddDays(1))
                {
                    if (day >= new DateTime(Prevyear, 12, 26) && day <= new DateTime(Prevyear, 12, 31))
                    {
                        count++;
                    }
                }

                if (Convert.ToDouble(dt.Rows[0]["TAnnulaLeave"]) < count && count != 0)
                {
                    return false;
                }
            }
        }

        return true;
    }


    protected bool isClearGroup(string _group)
    {
        bool _clr = true;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAttendanceTypeInner = null;
        TextBox txtReasonInner = null;
        Control ctl = null;
        foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
            ddlAttendanceTypeInner = (DropDownList)gvRow.FindControl("ddlAttendanceType");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            string _leaveGroup = gvRow.Cells[2].Text;
            cb = (CheckBox)gvRow.FindControl("CheckBox1");

            if (_leaveGroup == _group)
            {
                if (txtReasonInner.Text == string.Empty || ddlLeaveInner.SelectedValue == "0" || cb.Checked != true)
                {
                    _clr = false;
                }
            }
        }
        return _clr;
    }


    protected void gvLeavesNotSubmitted_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMoodLeaves"].ToString();

                foreach (GridViewRow gvr in gvLeavesNotSubmitted.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBox1");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMoodLeaves"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMoodLeaves"] = "check";
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

    protected void loadEmpleave()
    {

        BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

        DataTable dt = new DataTable();
        obj.EmployeeCode = Session["EmployeeCode"].ToString();
        obj.Year = DateTime.Now.Year.ToString();
        dt = obj.EmployeeLeaveBalanceFetch(obj);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["ActAnnual"].ToString() == "")
                lblActAnu.Text = "0";
            else
                lblActAnu.Text = dt.Rows[0]["ActAnnual"].ToString();

            if (dt.Rows[0]["ActCasual"].ToString() == "")
                lblActCas.Text = "0";
            else
                lblActCas.Text = dt.Rows[0]["ActCasual"].ToString();

            if (dt.Rows[0]["NonApvAnnual"].ToString() == "")
                lblNonApvAnu.Text = "0";
            else
                lblNonApvAnu.Text = dt.Rows[0]["NonApvAnnual"].ToString();

            if (dt.Rows[0]["NonApvCasual"].ToString() == "")
                lblNonApvCas.Text = "0";
            else
                lblNonApvCas.Text = dt.Rows[0]["NonApvCasual"].ToString();

            if (dt.Rows[0]["balCasual"].ToString() == "")
                lblCas.Text = "0";
            else
                lblCas.Text = dt.Rows[0]["balCasual"].ToString();

            if (dt.Rows[0]["balAnnual"].ToString() == "")
                lblAnu.Text = "0";
            else
                lblAnu.Text = dt.Rows[0]["balAnnual"].ToString();
        }
        else
        {
            lblActAnu.Text = "0";
            lblActCas.Text = "0";

            lblNonApvAnu.Text = "0";
            lblNonApvCas.Text = "0";

            lblCas.Text = "0";
            lblAnu.Text = "0";
        }
    }

    protected void ddlRoleTypeHalfDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
        LeavePolicyImpleHalfDays(ddlCurrentDropDownList, grdrDropDownRow);
    }

    protected void ddlAttendanceTypeHalfDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
        //LeavePolicyImpleHalfDays(ddlCurrentDropDownList, grdrDropDownRow);
    }
    private void LeavePolicyImpleHalfDays(DropDownList ddlCurrentDropDownList, GridViewRow grdrDropDownRow)
    {
        int _valCasul = 0;

        int _index = grdrDropDownRow.RowIndex;

        TextBox txtReason = (TextBox)grdrDropDownRow.Cells[7].FindControl("txtReason");



        //_valAnual = LeaveBalanceCounter(((int)LeaveTypes.AnnualLeaves).ToString());

        if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.HalfCasualLeave).ToString())   /*Half Casual Leaves*/
        {
            _valCasul = LeaveBalanceCounter(((int)LeaveTypes.CasualLeaves).ToString());
            if ((Convert.ToDouble(_valCasul) / 2) > Convert.ToDouble(lblCas.Text))
            {
                drawMsgBox("Casual Leave Balance is less then '0.5'. You can submit 'Half Leave Without Pay'.", 3);
                ddlCurrentDropDownList.SelectedValue = "1069";
                //OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");

            }
            else if (Convert.ToDouble(lblCas.Text) <= 0)
            {
                drawMsgBox("Casual Leave Balance is less then '0.5'. You can submit 'Half Leave Without Pay'.", 3);
                ddlCurrentDropDownList.SelectedValue = "1069";
            }
            else
            {
                //OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.HalfFurloughLeave).ToString())   /*Half Furlough Leaves*/
        {
            float Flbalance = 0;
            Flbalance = CheckFurloughLeaveBalance();

            if (Flbalance < 0.5)
            {
                drawMsgBox("Your Furlough Leave quota has been exceeded for this month. You can submit 'Half Leave Without Pay'.", 3);
                ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.HalfLeaveWithoutPay).ToString();
            }
        }

        else if (ddlCurrentDropDownList.SelectedValue == "0") /*Select*/
        {
            //OffDaySettings(_index, "0", "");
        }
    }



    private void LeavePolicyImple(DropDownList ddlCurrentDropDownList, GridViewRow grdrDropDownRow)
    {
        float _valCasul = 0;
        int _valAnual = 0;
        int _valisAnl = 0;

        int _index = grdrDropDownRow.RowIndex;

        TextBox txtReason = (TextBox)grdrDropDownRow.Cells[7].FindControl("txtReason");


        _valCasul = LeaveBalanceCounter(((int)LeaveTypes.CasualLeaves).ToString());
        _valAnual = LeaveBalanceCounter(((int)LeaveTypes.AnnualLeaves).ToString());

        if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.CasualLeaves).ToString())   /*Casual Leaves*/
        {

            if (_valCasul > Convert.ToSingle(lblCas.Text) || Convert.ToSingle(lblCas.Text) <= 0)
            {
                drawMsgBox("Casual Leave Balance is less then '1'. You can submit 'Leave Without Pay'.", 3);
                ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");

            }
            else
            {
                OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.AnnualLeaves).ToString()) /*Anual Leaves*/
        {

            _valisAnl = objbase.IsAnual(grdrDropDownRow.Cells[2].Text);
            _valAnual = LeaveBalanceCounter(((int)LeaveTypes.AnnualLeaves).ToString());

            if (Convert.ToInt32(lblAnu.Text) < 3)
            {
                if (_valCasul < Convert.ToSingle(lblCas.Text))
                {
                    ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                    OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
                }
                else
                {
                    ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                    OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");
                }
            }
            else
            {
                if (_valisAnl < 3)
                {
                    if (_valCasul < Convert.ToSingle(lblCas.Text))
                    {
                        ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                        OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
                    }
                    else
                    {
                        ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                        OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");
                    }
                }
                else
                {

                    if (_valAnual >= Convert.ToInt32(lblAnu.Text))
                    {
                        //Cehck for Casul Balance then Assign
                        if (_valCasul < Convert.ToSingle(lblCas.Text))
                        {
                            ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                            OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
                        }
                        else
                        {
                            ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                            OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");
                        }
                    }
                    else
                    {
                        ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.AnnualLeaves).ToString();
                        OffDaySettingsCA(_index, ((int)LeaveTypes.AnnualLeaves).ToString(), txtReason.Text);
                    }
                }
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.OffDay).ToString()) /*Off Day*/
        {
            if (grdrDropDownRow.Cells[1].Text != "1")
            {
                drawMsgBox("Working day can not be defined as calendar off day!", 3);
                ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.OfficialTourTask).ToString();
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.Present).ToString()) /*Present*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.ManualAttendancePunchCard).ToString()) /*Manual Punch*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.OfficialTourTask).ToString()) /*Offical Tour*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.Lieuof).ToString()) /*Lieu Off*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.SpecialLeaves).ToString()) /*Special Leave*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");

        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.LeaveWithoutPay).ToString()) /*Leave Without Pay*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == "0") /*Select*/
        {
            OffDaySettings(_index, "0", "");
        }
    }

    private void LeavePolicyImpleFurlough(DropDownList ddlCurrentDropDownList, GridViewRow grdrDropDownRow)
    {
        float _valCasul = 0;
        int _valAnual = 0;
        int _valisAnl = 0;

        int _index = grdrDropDownRow.RowIndex;

        TextBox txtReason = (TextBox)grdrDropDownRow.Cells[7].FindControl("txtReason");


        _valCasul = LeaveBalanceCounter(((int)LeaveTypes.CasualLeaves).ToString());
        _valAnual = LeaveBalanceCounter(((int)LeaveTypes.AnnualLeaves).ToString());

        if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.CasualLeaves).ToString())   /*Casual Leaves*/
        {
             
            if (_valCasul > Convert.ToSingle(lblCas.Text) || Convert.ToSingle(lblCas.Text) <= 0)
            {
                drawMsgBox("Casual Leave Balance is less then '1'. You can submit 'Leave Without Pay'.", 3);
                ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");

            }
            else
            {
                OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.AnnualLeaves).ToString()) /*Anual Leaves*/
        {

            _valisAnl = objbase.IsAnual(grdrDropDownRow.Cells[2].Text);
            _valAnual = LeaveBalanceCounter(((int)LeaveTypes.AnnualLeaves).ToString());

            if (Convert.ToInt32(lblAnu.Text) < 3)
            {
                if (_valCasul < Convert.ToSingle(lblCas.Text))
                {
                    ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                    OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
                }
                else
                {
                    ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                    OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");
                }
            }
            else
            {
                if (_valisAnl < 3)
                {
                    if (_valCasul < Convert.ToSingle(lblCas.Text))
                    {
                        ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                        OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
                    }
                    else
                    {
                        ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                        OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");
                    }
                }
                else
                {

                    if (_valAnual >= Convert.ToInt32(lblAnu.Text))
                    {
                        //Cehck for Casul Balance then Assign
                        if (_valCasul < Convert.ToSingle(lblCas.Text))
                        {
                            ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.CasualLeaves).ToString();
                            OffDaySettingsCA(_index, ((int)LeaveTypes.CasualLeaves).ToString(), txtReason.Text);
                        }
                        else
                        {
                            ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                            OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");
                        }
                    }
                    else
                    {
                        ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.AnnualLeaves).ToString();
                        OffDaySettingsCA(_index, ((int)LeaveTypes.AnnualLeaves).ToString(), txtReason.Text);
                    }
                }
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.OffDay).ToString()) /*Off Day*/
        {
            if (grdrDropDownRow.Cells[1].Text != "1")
            {
                drawMsgBox("Working day can not be defined as calendar off day!", 3);
                ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.OfficialTourTask).ToString();
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.Present).ToString()) /*Present*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.ManualAttendancePunchCard).ToString()) /*Manual Punch*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.OfficialTourTask).ToString()) /*Offical Tour*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.Lieuof).ToString()) /*Lieu Off*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.SpecialLeaves).ToString()) /*Special Leave*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.OffDay).ToString(), "System attachement void, after submission of " + ddlCurrentDropDownList.SelectedItem.ToString() + ".");

        }
        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.LeaveWithoutPay).ToString()) /*Leave Without Pay*/
        {
            OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), ddlCurrentDropDownList.SelectedItem.ToString() + ".");
        }

        else if (ddlCurrentDropDownList.SelectedValue == ((int)LeaveTypes.FurloughLeave).ToString()) /*Furlough Leave*/
        {
            if (Convert.ToSingle(lblAnu.Text) + Convert.ToSingle(lblCas.Text) <= 0)
            {
                drawMsgBox("Leave Balance is less then '1'. You can submit 'Leave Without Pay'.", 3);
                ddlCurrentDropDownList.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                OffDaySettings(_index, ((int)LeaveTypes.LeaveWithoutPay).ToString(), "");

            }
            else
            {
                OffDaySettings(_index, ((int)LeaveTypes.FurloughLeave).ToString(), txtReason.Text);
            }
        }

        else if (ddlCurrentDropDownList.SelectedValue == "0") /*Select*/
        {
            OffDaySettings(_index, "0", "");
        }
    }
    private void OffDaySettingsCA(int _index, string _lv, string _cvr)
    {
        string _lvgroup = gvLeavesNotSubmitted.Rows[_index].Cells[2].Text;
        string _lvOff = gvLeavesNotSubmitted.Rows[_index].Cells[1].Text;
        float _valCasul = 0;
        int _valAnual = 0;


        //forward

        int _indextemp = _index;

        foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
        {
            if (gvRow.Cells[2].Text == _lvgroup && _indextemp + 1 == gvRow.RowIndex)
            {
                if (gvRow.Cells[1].Text == "1")
                {
                    DropDownList ddlI = (DropDownList)gvRow.Cells[7].FindControl("ddlRoleType");
                    TextBox txtI = (TextBox)gvRow.Cells[8].FindControl("txtReason");

                    if (_lv == ((int)LeaveTypes.CasualLeaves).ToString())
                    {
                        _valCasul = LeaveBalanceCounter(((int)LeaveTypes.CasualLeaves).ToString());

                        if (_valCasul >= Convert.ToSingle(lblCas.Text))
                        {
                            ddlI.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                            txtI.Text = "Without Pay";
                            _indextemp = _indextemp + 1;
                        }
                        else
                        {
                            ddlI.SelectedValue = _lv;
                            txtI.Text = (_cvr == "") ? "Casual Leave" : _cvr;
                            _indextemp = _indextemp + 1;
                        }
                    }
                    else if (_lv == ((int)LeaveTypes.AnnualLeaves).ToString())
                    {
                        _valAnual = LeaveBalanceCounter(((int)LeaveTypes.AnnualLeaves).ToString());

                        if (_valAnual >= Convert.ToInt32(lblAnu.Text))
                        {
                            ddlI.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                            txtI.Text = "Without Pay";
                            _indextemp = _indextemp + 1;

                        }
                        else
                        {
                            ddlI.SelectedValue = _lv;
                            txtI.Text = _cvr;
                            _indextemp = _indextemp + 1;

                        }
                    }
                    else
                    {
                        ddlI.SelectedValue = _lv;
                        txtI.Text = _cvr;
                        _indextemp = _indextemp + 1;

                    }
                }
            }
        }


        //backward
        _indextemp = _index;


        int limit = gvLeavesNotSubmitted.Rows.Count - 1;

        for (int i = limit; i >= 0; i--)
        {

            if (gvLeavesNotSubmitted.Rows[i].Cells[2].Text == _lvgroup && _indextemp - 1 == gvLeavesNotSubmitted.Rows[i].RowIndex)
            {
                if (gvLeavesNotSubmitted.Rows[i].Cells[1].Text == "1")
                {
                    DropDownList ddlI = (DropDownList)gvLeavesNotSubmitted.Rows[i].Cells[7].FindControl("ddlRoleType");
                    TextBox txtI = (TextBox)gvLeavesNotSubmitted.Rows[i].Cells[8].FindControl("txtReason");
                    if (_lv == ((int)LeaveTypes.CasualLeaves).ToString())
                    {
                        _valCasul = LeaveBalanceCounter(((int)LeaveTypes.CasualLeaves).ToString());

                        if (_valCasul >= Convert.ToSingle(lblCas.Text))
                        {
                            ddlI.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                            txtI.Text = "Without Pay";
                            _indextemp = _indextemp - 1;

                        }
                        else
                        {
                            ddlI.SelectedValue = _lv;
                            txtI.Text = _cvr;
                            _indextemp = _indextemp - 1;

                        }
                    }
                    else if (_lv == ((int)LeaveTypes.AnnualLeaves).ToString())
                    {
                        _valAnual = LeaveBalanceCounter(((int)LeaveTypes.AnnualLeaves).ToString());

                        if (_valAnual >= Convert.ToInt32(lblAnu.Text))
                        {
                            ddlI.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
                            txtI.Text = "Without Pay";
                            _indextemp = _indextemp - 1;

                        }
                        else
                        {
                            ddlI.SelectedValue = _lv;
                            txtI.Text = _cvr;
                            _indextemp = _indextemp - 1;

                        }
                    }
                    else
                    {
                        ddlI.SelectedValue = _lv;
                        txtI.Text = _cvr;
                        _indextemp = _indextemp - 1;

                    }
                }
            }


        }

    }
    private void OffDaySettings(int _index, string _lv, string _cvr)
    {
        string _lvgroup = gvLeavesNotSubmitted.Rows[_index].Cells[2].Text;
        string _lvOff = gvLeavesNotSubmitted.Rows[_index].Cells[1].Text;
        int _indextemp = _index;
        //backward
        int limit = gvLeavesNotSubmitted.Rows.Count - 1;

        for (int i = limit; i >= 0; i--)
        {
            if (gvLeavesNotSubmitted.Rows[i].Cells[2].Text == _lvgroup && _indextemp - 1 == gvLeavesNotSubmitted.Rows[i].RowIndex)
            {
                if (gvLeavesNotSubmitted.Rows[i].Cells[1].Text == "1")
                {
                    DropDownList ddlI = (DropDownList)gvLeavesNotSubmitted.Rows[i].Cells[7].FindControl("ddlRoleType");
                    TextBox txtI = (TextBox)gvLeavesNotSubmitted.Rows[i].Cells[8].FindControl("txtReason");

                    ddlI.SelectedValue = _lv;
                    txtI.Text = _cvr;
                    _indextemp = _indextemp - 1;
                }
            }

        }


        _indextemp = _index;

        foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
        {
            if (gvRow.Cells[2].Text == _lvgroup && _indextemp + 1 == gvRow.RowIndex)
            {

                if (gvRow.Cells[1].Text == "1")
                {
                    DropDownList ddlI = (DropDownList)gvRow.Cells[7].FindControl("ddlRoleType");
                    TextBox txtI = (TextBox)gvRow.Cells[8].FindControl("txtReason");
                    txtI.Text = _cvr;
                    ddlI.SelectedValue = _lv;
                    _indextemp = _indextemp + 1;
                }
            }
        }
    }
    protected int LeaveBalanceCounter(string _val)
    {
        int count = 0;
        DropDownList ddlLeaveInner = null;
        foreach (GridViewRow gvRow in gvLeavesNotSubmitted.Rows)
        {
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
            if (ddlLeaveInner.SelectedValue == _val)
            {
                count = count + 1;
            }
        }
        return count;
    }

    protected int LeaveBalanceCounterHalfDays(string _val)
    {
        int count = 0;
        DropDownList ddlLeaveInner = null;
        foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
        {
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeHalfDays");
            if (ddlLeaveInner.SelectedValue == _val)
            {
                count = count + 1;
            }
        }
        return count;
    }

    #endregion Leaves Submission

    #region half Days


    protected void gvHalgDayNonSubmitted_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtHalfDaysNotSubmitted"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["HalfDaySortDirection"].ToString();

            if (ViewState["HalfDaySortDirection"].ToString() == "ASC")
            {
                ViewState["HalfDaySortDirection"] = "DESC";
            }
            else
            {
                ViewState["HalfDaySortDirection"] = "ASC";
            }
            ViewState["dtHalfDaysNotSubmitted"] = null;
            bindgridHalfDay();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvHalgDayNonSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvHalgDayNonSubmitted.PageIndex = e.NewPageIndex;

            ViewState["dtHalfDaysNotSubmitted"] = null;
            bindgridHalfDay();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void gvHalfDaysSubmitted_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtHalfDaysSubmitted"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["HalfDaySortDirection"].ToString();

            if (ViewState["HalfDaySortDirection"].ToString() == "ASC")
            {
                ViewState["HalfDaySortDirection"] = "DESC";
            }
            else
            {
                ViewState["HalfDaySortDirection"] = "ASC";
            }
            ViewState["dtHalfDaysSubmitted"] = null;
            bindgridHalfDay();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvHalfDaysSubmitted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvHalfDaysSubmitted.PageIndex = e.NewPageIndex;

            ViewState["dtHalfDaysSubmitted"] = null;
            bindgridHalfDay();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void gvHalgDayNonSubmitted_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            DropDownList ddlRoleType = (DropDownList)e.Row.FindControl("ddlRoleTypeHalfDays");
            DropDownList ddlAttendanceType = (DropDownList)e.Row.FindControl("ddlAttendanceTypeHalfDays");
            TextBox txtReason = (TextBox)e.Row.FindControl("txtReason");
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            objBLL.Status_id = 1;
            objBLL.Used_For = "HLF";
            DataTable objDt = new DataTable();
            DataTable attendanceTypeDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);
            attendanceTypeDt = objBLL.EmployeeAttendanceType();

            ddlRoleType.DataSource = objDt;
            ddlRoleType.DataValueField = "LeaveType_Id";
            ddlRoleType.DataTextField = "LeaveType";
            ddlRoleType.DataBind();
            ddlRoleType.Items.Insert(0, new ListItem("Select", "0"));

            //ddlAttendanceType.DataSource = attendanceTypeDt;
            //ddlAttendanceType.DataValueField = "AttendanceTypeId";
            //ddlAttendanceType.DataTextField = "AttendanceType";
            //ddlAttendanceType.DataBind();
            //ddlAttendanceType.Items.Insert(0, new ListItem("Select", "0"));


            //CheckBox ch = (CheckBox)e.Row.FindControl("CheckBox1");
            //ch.Checked = false;

            if (e.Row.Cells[13].Text == "True") // if Data Locked after ERP PRocesss
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
            }
            else
            {
                if (e.Row.Cells[11].Text != "&nbsp;")
                {
                    ddlRoleType.SelectedValue = e.Row.Cells[11].Text;
                    //ddlAttendanceType.SelectedValue = e.Row.Cells[12].Text;
                    txtReason.Text = e.Row.Cells[12].Text == "&nbsp;" ? "" : e.Row.Cells[12].Text;
                }
                else
                {
                    ddlRoleType.SelectedValue = "0";
                }

                int R = Convert.ToInt32(e.Row.Cells[14].Text);
                int G = Convert.ToInt32(e.Row.Cells[15].Text);
                int B = Convert.ToInt32(e.Row.Cells[16].Text);

                string htmlString = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(R, G, B));


                //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(htmlString);
                //                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");





                if (e.Row.Cells[1].Text == "1") //Off Day
                {
                    //e.Row.BackColor = System.Drawing.Color.Wheat;
                    e.Row.Cells[7].Enabled = false;
                    e.Row.Cells[8].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                }


            }



            //       e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");


        }
    }



    protected void btnCopyhalfDays_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnCopyhalfDays = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnCopyhalfDays.CommandArgument);

        GridViewRow grv = (GridViewRow)btnCopyhalfDays.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlRoleTypeHalfDays");
        DropDownList ddlLeave = (DropDownList)ctl;


        string _reason;
        string _leave;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;


        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        TextBox txtReasonInner = null;

        if (_leave == ((int)LeaveTypes.HalfCasualLeave).ToString())   /*Casual Leaves*/
        {


            foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
            {
                int _index = gvRow.RowIndex;
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeHalfDays");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                if (cb.Checked)
                {
                    //Check here for balances
                    float _valCasul = 0;
                    _valCasul = LeaveBalanceCounterHalfDays(((int)LeaveTypes.HalfCasualLeave).ToString());
                    if (_valCasul >= Convert.ToSingle(lblCas.Text))
                    {
                        ddlLeaveInner.SelectedValue = ((int)LeaveTypes.HalfLeaveWithoutPay).ToString();
                        txtReasonInner.Text = _reason;
                        cb.Checked = false;
                    }
                    else
                    {
                        ddlLeaveInner.SelectedValue = _leave;
                        txtReasonInner.Text = _reason;
                        cb.Checked = false;
                    }
                }
            }
        }
        else if (_leave == ((int)LeaveTypes.HalfFurloughLeave).ToString())
        {
            //Check here for balances
            float Flbalance = 0;
            Flbalance = CheckFurloughLeaveBalance();

            foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
            {
                int _index = gvRow.RowIndex;
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeHalfDays");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                if (cb.Checked)
                {


                    if (Flbalance < 0.5)
                    {
                        ddlLeaveInner.SelectedValue = ((int)LeaveTypes.HalfLeaveWithoutPay).ToString();
                        txtReasonInner.Text = _reason;
                        cb.Checked = false;
                    }
                    else
                    {
                        ddlLeaveInner.SelectedValue = _leave;
                        txtReasonInner.Text = _reason;
                        cb.Checked = false;
                        Flbalance = Flbalance - float.Parse("0.5");
                    }
                }
            }



        }
        //else if (_leave == ((int)LeaveTypes.AnnualLeaves).ToString()) /*Anual Leaves*/
        //{
        //    drawMsgBox("Copy Not Allowed for this Leave Type!", 3);
        //    foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
        //    {
        //        cb = (CheckBox)gvRow.FindControl("CheckBox1");
        //        cb.Checked = false;
        //    }
        //    #region 'Policy'

        //    #endregion
        //}
        //else if (_leave == ((int)LeaveTypes.OffDay).ToString()) /*Off Day*/
        //{
        //    drawMsgBox("Copy Not Allowed for this Leave Type!", 3);
        //    foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
        //    {
        //        cb = (CheckBox)gvRow.FindControl("CheckBox1");
        //        cb.Checked = false;
        //    }

        //}
        //else if (_leave == ((int)LeaveTypes.Present).ToString()) /*Present*/
        //{
        //    AssignToAll(_leave, _reason, ddlLeave.SelectedItem.ToString());
        //}
        //else if (_leave == ((int)LeaveTypes.OfficialTourTask).ToString()) /*Offical Tour*/
        //{
        //    AssignToAll(_leave, _reason, ddlLeave.SelectedItem.ToString());
        //}
        //else if (_leave == ((int)LeaveTypes.ManualAttendancePunchCard).ToString()) /*Manual Punch Card*/
        //{
        //    AssignToAll(_leave, _reason, ddlLeave.SelectedItem.ToString());
        //}
        //else if (_leave == ((int)LeaveTypes.Lieuof).ToString()) /*Lieu Off*/
        //{
        //    drawMsgBox("Copy Not Allowed for this Leave Type!", 3);
        //    foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
        //    {
        //        cb = (CheckBox)gvRow.FindControl("CheckBox1");
        //        cb.Checked = false;
        //    }

        //}

        else if (_leave == "0") /*Select*/
        {
            drawMsgBox("Copy Not Allowed for this Leave Type!", 3);
            foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
            {
                cb = (CheckBox)gvRow.FindControl("CheckBox1");
                cb.Checked = false;
            }

        }


        //else if (_leave == ((int)LeaveTypes.SpecialLeaves).ToString()) /*Special Leave*/
        //{
        //    AssignToAll(_leave, _reason, ddlLeave.SelectedItem.ToString());
        //}
        //else if (_leave == ((int)LeaveTypes.LeaveWithoutPay).ToString()) /*Leave Without Pay*/
        //{
        //    foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
        //    {
        //        int _index = gvRow.RowIndex;
        //        ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
        //        txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
        //        cb = (CheckBox)gvRow.FindControl("CheckBox1");
        //        if (cb.Checked)
        //        {
        //            ddlLeaveInner.SelectedValue = ((int)LeaveTypes.LeaveWithoutPay).ToString();
        //            txtReasonInner.Text = _reason;
        //            cb.Checked = false;
        //        }
        //    }
        //}
        ViewState["HalfDaySortDirection"] = "ASC";
        ViewState["HalfDaytMood"] = "check";

    }


    protected void bindgridHalfDay()
    {
        loadEmpleave();
        gvHalgDayNonSubmitted.DataSource = null;
        gvHalfDaysSubmitted.DataSource = null;
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObj.Submit2HOD = false;

            if (ViewState["dtHalfDaysNotSubmitted"] == null)
                _dt = bllObj.HalfDaysSelect(bllObj);
            else
                _dt = (DataTable)ViewState["dtHalfDaysNotSubmitted"];

            if (_dt.Rows.Count > 0)
            {
                gvHalgDayNonSubmitted.DataSource = _dt;
                ViewState["dtHalfDaysNotSubmitted"] = _dt;

                div_halfDayNotSubmitted.Visible = true;
            }
            else
            {
                div_halfDayNotSubmitted.Visible = false;
            }
            gvHalgDayNonSubmitted.DataBind();

            //Show Read Only Grid 
            bllObj.Submit2HOD = true;
            if (ViewState["dtHalfDaysSubmitted"] == null)
                _dt = bllObj.HalfDaysSelect(bllObj);
            else
                _dt = (DataTable)ViewState["dtHalfDaysSubmitted"];

            if (_dt.Rows.Count > 0)
            {
                gvHalfDaysSubmitted.DataSource = _dt;
                ViewState["dtHalfDaysSubmitted"] = _dt;

                div_halfDaySubmitted.Visible = true;
            }
            else
            {
                div_halfDaySubmitted.Visible = false;
            }
            gvHalfDaysSubmitted.DataBind();

            lblHalfDayNotSubmitted.Text = gvHalgDayNonSubmitted.Rows.Count.ToString();
            lblHalfDaySubmitted.Text = gvHalfDaysSubmitted.Rows.Count.ToString();
            lblHalfDaysTotal.Text = (gvHalgDayNonSubmitted.Rows.Count + gvHalfDaysSubmitted.Rows.Count).ToString();

            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }


    protected void btnHalfDaySave_Click(object sender, EventArgs e)
    {
        HalfCasualLeaveSubmit();
        //HalfFurloughLeaveSubmit();

    }

    private void HalfCasualLeaveSubmit()
    {
        string maildts = "";

        BLLSendEmail bllemail = new BLLSendEmail();

        string _gp;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAttendanceType = null;
        DropDownList ddlAttendanceTypeHalfDays = null;
        TextBox txtReasonInner = null;
        Control ctl = null;

        int counter = 0;
        int errorNumber = 0;


        if (checkLastYearsCasualBalance(((int)LeaveTypes.HalfCasualLeave)))
        {

            foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
            {
                int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeHalfDays");
                ddlAttendanceTypeHalfDays = (DropDownList)gvRow.FindControl("ddlAttendanceTypeHalfDays");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");

                if (cb.Checked)
                {

                    if (txtReasonInner.Text != string.Empty && ddlLeaveInner.SelectedValue != "0")
                    {
                        /*is clear group*/
                        _gp = gvRow.Cells[2].Text;

                        bool _bl = true;//isClearGroup(_gp);

                        if (_bl == true)
                        {


                            bllObj.EmployeeCode = Session["EmployeeCode"].ToString();
                            bllObj.PMonthDesc = this.ddlMonths.SelectedValue.ToString();

                            bool already_hlfday = false;

                            if (ddlLeaveInner.SelectedValue == ((int)LeaveTypes.HalfCasualLeave).ToString()) /*If already Halfday availed*/
                            {
                                DataTable dt_hlf_days = bllObj.GetHalfDaysUnOfficial_Emp(bllObj);

                                if (dt_hlf_days.Rows.Count > 0)
                                {
                                    already_hlfday = true;
                                    string date = dt_hlf_days.Rows[0]["AttDate"].ToString();
                                    ImpromptuHelper.ShowPrompt("You have already availed a half day leave on " + date + ". ");
                                }
                            }

                            if (already_hlfday)
                            {
                                cb.Checked = false;
                                errorNumber = 3;
                            }
                            else /*Save data in Database*/
                            {
                                bllObj.Att_Id = _id;
                                bllObj.EmpLvType_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                                //bllObj.AttTypeId = Convert.ToInt32(ddlAttendanceTypeHalfDays.SelectedValue);
                                bllObj.EmpLvReason = txtReasonInner.Text;
                                bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                                bllObj.ModifyDate = DateTime.Now;
                                bllObj.EmpLvSubDate = DateTime.Now;
                                bllObj.Submit2HOD = true;
                                //bllObj.AttendanceTypeId = Convert.ToInt32(ddlAttendanceTypeHalfDays.SelectedValue);

                                int dt = bllObj.AttendanceUpdateEmpHalfDays(bllObj);
                                if (dt >= 1)
                                {
                                    //

                                    BLLEmployeeLeaveType bllempty = new BLLEmployeeLeaveType();
                                    bllempty.LeaveType_Id = bllObj.EmpLvType_Id;
                                    //bllempty.AttendanceTypeId = bllObj.AttTypeId;
                                    DataTable _dtlt = bllempty.EmployeeLeaveTypeFetchByID(bllempty);
                                    if (_dtlt.Rows.Count > 0)
                                    {
                                        maildts = maildts + "<br>     [" + gvRow.Cells[4].Text + "]---[" + _dtlt.Rows[0]["LeaveType"].ToString() + "]---[" + txtReasonInner.Text + "]";
                                    }
                                    counter = counter + 1;

                                }
                            }
                        }
                        else
                        {
                            cb.Checked = false;
                            errorNumber = 1;
                        }
                    }
                    else
                    {
                        cb.Checked = false;
                        errorNumber = 2;
                    }
                }
            }
            if (counter > 0)
            {

                #region Send Email to HOD

                DataTable _dtemp = new DataTable();
                _dtemp = (DataTable)Session["EmailTable"];
                if (_dtemp.Rows.Count > 0)
                {
                    foreach (DataRow var in _dtemp.Rows)
                    {
                        string mailMsg = "Dear " + var["HODName"].ToString() + ", <br><br> Employee # : " + Session["EmployeeCode"].ToString() + ".<br> Name :" + Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString() + "<br> Submitted leaves for HOD approval. You can find data in HOD Leave Approval.<br><br>      [Date]---[EmplyeeLeave]---[Reason]";
                        mailMsg = mailMsg + maildts;
                        bllemail.SendEmail(var["HODEmail"].ToString(), "Attendance [Leave(s) Approval]", mailMsg);
                    }
                }
                #endregion

                #region Display Notification Screen Message 
                if (errorNumber == 0)
                {
                    drawMsgBox("Successfully Submitted to HOD.", 1);
                    ViewState["dtHalfDaysNotSubmitted"] = null;
                    ViewState["dtHalfDaysSubmitted"] = null;
                    bindgridHalfDay();
                }
                else if (errorNumber == 1)
                {
                    drawMsgBox("Please submit All leaves at Once.", 3);
                }
                else if (errorNumber == 2)
                {
                    drawMsgBox("leave Type and Reason are mandatory fields.", 3);

                }
                else if (errorNumber == 3)
                {
                    drawMsgBox("Employee can only avail one Half Casual Leave.", 3);

                    ViewState["dtHalfDaysNotSubmitted"] = null;
                    ViewState["dtHalfDaysSubmitted"] = null;
                    bindgridHalfDay();

                }
                #endregion

            }
        }
    }

    private void HalfFurloughLeaveSubmit()
    {


        string _gp;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAttendanceTypeHalfDays = null;
        TextBox txtReasonInner = null;
        Control ctl = null;

        float counter = 0;
        int errorNumber = 0;
        int checkedCount = 0;

        float Flbalance = 0;
        Flbalance = CheckFurloughLeaveBalance();

        foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeHalfDays");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");


            if (cb.Checked)
            {
                checkedCount += 1;
                if (txtReasonInner.Text != string.Empty && ddlLeaveInner.SelectedValue == ((int)LeaveTypes.HalfFurloughLeave).ToString())
                {
                    counter = counter + float.Parse("0.5");
                }
            }
        }

        if (checkedCount > Flbalance)
        {
            errorNumber = 1;
        }

        if (counter > Flbalance)
        {
            errorNumber = 1;
        }
        else /*Save data in Database*/
        {

            foreach (GridViewRow gvRow in gvHalgDayNonSubmitted.Rows)
            {
                int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
                ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeHalfDays");
                ddlAttendanceTypeHalfDays = (DropDownList)gvRow.FindControl("ddlAttendanceTypeHalfDays");
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                cb = (CheckBox)gvRow.FindControl("CheckBox1");

                if (cb.Checked)
                {
                    if (txtReasonInner.Text != string.Empty && ddlLeaveInner.SelectedValue != "0")
                    {
                        bllObj.Att_Id = _id;
                        bllObj.EmpLvType_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                        //bllObj.AttTypeId = Convert.ToInt32(ddlAttendanceTypeHalfDays.SelectedValue);
                        bllObj.EmpLvReason = txtReasonInner.Text;
                        bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                        bllObj.ModifyDate = DateTime.Now;
                        bllObj.EmpLvSubDate = DateTime.Now;
                        bllObj.Submit2HOD = true;

                        int dt = bllObj.AttendanceUpdateEmpHalfDays(bllObj);
                        if (dt >= 1)
                        {
                            errorNumber = 2;
                        }
                    }
                }
            }

        }

        #region Display Notification Screen Message 
        if (errorNumber == 0)
        {
            drawMsgBox("No Action Performed.", 0);
            ViewState["dtHalfDaysNotSubmitted"] = null;
            ViewState["dtHalfDaysSubmitted"] = null;
            bindgridHalfDay();
        }

        else if (errorNumber == 1)
        {
            drawMsgBox("Submitted Furlough Leaves are exceeding from your entitled leave quota of this month.", 1);

            ViewState["dtHalfDaysNotSubmitted"] = null;
            ViewState["dtHalfDaysSubmitted"] = null;
            bindgridHalfDay();

        }
        else if (errorNumber == 2)
        {
            drawMsgBox("Successfully Submitted to HOD.", 2);
            ViewState["dtHalfDaysNotSubmitted"] = null;
            ViewState["dtHalfDaysSubmitted"] = null;
            bindgridHalfDay();
        }
        #endregion

    }


    protected void gvHalgDayNonSubmitted_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["HalfDaytMood"].ToString();

                foreach (GridViewRow gvr in gvHalgDayNonSubmitted.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBox1");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["HalfDaytMood"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["HalfDaytMood"] = "check";
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



    protected void ddlRoleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
        //LeavePolicyImple(ddlCurrentDropDownList, grdrDropDownRow);
        LeavePolicyImpleFurlough(ddlCurrentDropDownList, grdrDropDownRow);
    }

    protected void ddlAttendanceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
        LeavePolicyImple(ddlCurrentDropDownList, grdrDropDownRow);
        //LeavePolicyImpleFurlough(ddlCurrentDropDownList, grdrDropDownRow);
    }



    #endregion half Days

    #region Reservation



    protected void leavetype()
    {
        BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
        objBLL.Status_id = 1;
        objBLL.Used_For = "RES";
        DataTable objDt = new DataTable();

        objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);

        if (objDt.Rows.Count > 0)
        {
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataSource = objDt;
            ddlLeaveType.DataValueField = "LeaveType_Id";
            ddlLeaveType.DataTextField = "LeaveType";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void leavetypeOFT()
    {
        BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
        objBLL.Status_id = 1;
        objBLL.Used_For = "OFT";
        DataTable objDt = new DataTable();

        objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);

        if (objDt.Rows.Count > 0)
        {
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataSource = objDt;
            ddlLeaveType.DataValueField = "LeaveType_Id";
            ddlLeaveType.DataTextField = "LeaveType";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BLLSendEmail bllemail = new BLLSendEmail();
        BLLPeriod obj = new BLLPeriod();
        BLLAttendance objAttendance = new BLLAttendance();
        obj.PMonth = ddlMonths.SelectedValue;
        DataTable dtPeriod = obj.PeriodFetchMonthRangeDates(obj);

        DateTime dtMinDate = Convert.ToDateTime(dtPeriod.Rows[0]["mindate"].ToString());
        try
        {
            DateTime datecheck = DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null);
            //Convert.ToDateTime(txtFromDate.Text);
            objAttendance.InsertDate = datecheck.Date;
            if (!String.IsNullOrEmpty(Session["CenterID"].ToString()))
                objAttendance.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
            else
                objAttendance.Center_Id = 0;
            DataTable dtbl = objAttendance.ERP_Final_Process_HistorySelectByCenter(objAttendance);
            if (dtbl.Rows.Count > 0)
            {
                //ImpromptuHelper.ShowPrompt("ERP Process has been run for the month for which you are applying Leaves!");
                //btnReset_Click(this, EventArgs.Empty);
                //return;
            }
            string mode = Convert.ToString(ViewState["mode"]);
            int id = 0;
            bool isok = true;
            string _displymsg = "";
            string _leave = ddlLeaveType.SelectedValue;

            //DateTime _StrDay = Convert.ToDateTime(txtFromDate.Text.Trim().Replace("'", ""));
            //string Monday = _StrDay.ToString("dddd");

            //DateTime _EndDay = Convert.ToDateTime(txtToDate.Text.Trim().Replace("'", ""));
            //string Saturday = _EndDay.ToString("dddd");

            //bool isEx = Convert.ToBoolean(Session["isExe"].ToString());

            //if (Monday == "Monday" && isEx == false && _leave != ((int)LeaveTypes.OfficialTourTask).ToString()) //if (Monday == "Monday")// && isEx==false)
            //{

            //    isok = false;
            //}
            //else if (Saturday == "Saturday" && isEx == false && _leave != ((int)LeaveTypes.OfficialTourTask).ToString()) //else if (Saturday == "Saturday")// && isEx == false)
            //{

            //    isok = false;
            //}
            //else
            //{
            //    isok = true;
            //}

            int _HOD;
            BLLEmplyeeReportTo BllEmpR = new BLLEmplyeeReportTo();
            BllEmpR.ReportTo = (Session["EmployeeCode"].ToString());

            DataTable dt = BllEmpR.EmplyeeReportToHOD(BllEmpR);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    _HOD = Convert.ToInt32(dr["ReportTo"].ToString());
                    if (_HOD == 17101)
                    {
                        isok = true;
                        break;
                    }
                }
            }

            if (isok == true)
            {

                if (txtFromDate.Text.Trim() == "")
                {
                    isok = false;
                    _displymsg = "Start Date is empty. !";
                }
                else if (txtToDate.Text.Trim() == "")
                {
                    isok = false;
                    _displymsg = "End Date is empty. !";
                }

                if (isok)
                {
                    if (_leave == ((int)LeaveTypes.CasualLeaves).ToString())   /*Casual Leaves*/
                    {
                        //check balance and reservations
                        isok = true;
                        DateTime dtFrom = Convert.ToDateTime(txtFromDate.Text);
                        if (checkLastYearsCasualBalance(((int)LeaveTypes.CasualLeaves)) == false)
                        {
                            isok = false;
                            _displymsg = ("You have no Annual balance till '31st December " + Prevyear + "'. you can submit Leave Without pay.");
                        }
                        if (Convert.ToInt32(txtDays.Text) > Convert.ToSingle(lblCas.Text))
                        {
                            isok = false;
                            _displymsg = ("Can't exceed from current Casual (" + lblCas.Text + ") Leave balance!.");
                        }
                        if (dtFrom < dtMinDate)
                        {
                            isok = false;
                            _displymsg = ("Can't submit Casual leaves in last month.");
                        }
                    }
                    else if (_leave == ((int)LeaveTypes.AnnualLeaves).ToString()) /*Anual Leaves*/
                    {

                        DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null);
                        // Convert.ToDateTime(txtFromDate.Text);
                        DateTime dtTo = DateTime.ParseExact(txtToDate.Text, "M/d/yyyy", null);
                        // Convert.ToDateTime(txtToDate.Text);


                        isok = true;

                        if (Convert.ToInt32(txtDays.Text) < 3)
                        {
                            isok = false;
                            _displymsg = ("Atleast 3 anual leaves are allowed.");
                        }
                        else if (dtFrom < dtMinDate)
                        {
                            isok = false;
                            _displymsg = ("Can't submit Annual leaves in last month.");
                        }
                        else if (Convert.ToInt32(txtDays.Text) > Convert.ToInt32(lblAnu.Text))
                        {
                            isok = false;
                            _displymsg = ("Can't exceed from current Annual (" + lblAnu.Text + ") Leave balance!.");
                        }
                        else if (Convert.ToInt32(txtDays.Text) > 30)
                        {
                            isok = false;
                            _displymsg = "Maximum of '30' Annual leaves can be availed at one time in a year'.";
                        }

                        if (checkLastYearsAnnualBalance(62, DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null), DateTime.ParseExact(txtToDate.Text, "M/d/yyyy", null)) == false)
                        {
                            isok = false;
                            _displymsg = ("You have no Annual balance till '31st December " + Prevyear + "'. you can submit Leave Without pay.");
                        } 

                        if (Convert.ToInt32(txtDays.Text.ToString()) > Convert.ToInt32(balanceLeaves.Text.ToString()))
                        {
                            isok = false;
                            _displymsg = ("You cannot apply more than 60 leaves in a year'.");
                        }                         
                    }
                    else if (_leave == ((int)LeaveTypes.MaternityLeaves).ToString())
                    {
                        isok = true;

                        bllEmpLeaves.EmployeeCode = Session["EmployeeCode"].ToString();
                        DataTable dtb_mat = bllEmpLeaves.Select_MaternityLeavesEmp(bllEmpLeaves);
                        DataTable dtb_mateligible = bllEmpLeaves.SelectMaternityLeavesEMPEligible(bllEmpLeaves);

                        if (dtb_mateligible.Rows.Count > 0)
                        {
                            if (Convert.ToInt16(dtb_mateligible.Rows[0]["isAllowed"].ToString()) == 0)
                            {
                                isok = false;
                                _displymsg = "You will be entitled to maternity leaves after 12 months of service.";
                            }
                        }

                        if (Convert.ToString(Session["Gender"]).ToUpper().Trim() == "M")
                        {
                            isok = false;
                            _displymsg = "Only female employees can avail Maternity leaves.";
                        }
                        else if (Convert.ToInt32(txtDays.Text) > 35)
                        {
                            isok = false;
                            _displymsg = "Maximum of '35' maternity leaves can be availed.";
                        }
                        else if (dtb_mat.Rows.Count > 1)
                        {
                            isok = false;
                            _displymsg = "Maternity leaves can't be availed more then twice in service.";
                        }
                        else if (dtb_mat.Rows.Count > 0)
                        {
                            DateTime dt_last_mat = Convert.ToDateTime(dtb_mat.Rows[dtb_mat.Rows.Count - 1][1]);

                            DateTime dt_now = Convert.ToDateTime(txtFromDate.Text.Trim().Replace("'", "")); //DateTime.Now.AddYears(-3);
                            dt_now = dt_now.AddYears(-3);


                            if (dt_now.CompareTo(dt_last_mat) < 0)
                            {
                                isok = false;
                                _displymsg = "Maternity leaves can only be availed once in 3 years.";
                            }
                        }
                    }
                    else if (_leave == ((int)LeaveTypes.OffDay).ToString() || _leave == ((int)LeaveTypes.Present).ToString() || _leave == ((int)LeaveTypes.ManualAttendancePunchCard).ToString() || _leave == ((int)LeaveTypes.Lieuof).ToString()) /*Off Day - Present- Manual punch- lieu off*/
                    {
                        isok = false;
                        _displymsg = ("This Reservation Type is not Allowed.");

                    }
                    else if (_leave == ((int)LeaveTypes.OfficialTourTask).ToString() || _leave == ((int)LeaveTypes.SpecialLeaves).ToString() || _leave == ((int)LeaveTypes.LeaveWithoutPay).ToString()) /*Offical Tour - Special Leave - Leave withoutpay*/
                    {
                        isok = true;
                    }
                    else if (_leave == "0") /*Select*/
                    {
                        _displymsg = ("Must Select a Reservation type");
                        isok = false;
                    }
                    else if (_leave == ((int)LeaveTypes.FurloughLeave).ToString())
                    {

                        DateTime dtFrom = DateTime.ParseExact(txtFromDate.Text, "M/d/yyyy", null);

                        DateTime dtTo = DateTime.ParseExact(txtToDate.Text, "M/d/yyyy", null);


                        isok = true;
                        float Flbalance = 0;
                        Flbalance = CheckFurloughLeaveBalance();

                        if (dtFrom < dtMinDate)
                        {
                            isok = false;
                            _displymsg = ("Can't submit Furlough leaves in last month.");

                        }
                        else if (Convert.ToInt32(txtDays.Text) > (Flbalance))
                        {
                            isok = false;
                            _displymsg = ("Can't exceed from current Furlough (Annual + Casual) Leave balance!.");

                        }

                    }
                }

                if (isok == true)
                {
                     
                    #region 'Common Data'

                    bllEmpLeaves.EmployeeCode = Session["EmployeeCode"].ToString();
                    bllEmpLeaves.LeaveType_Id = Convert.ToInt32(ddlLeaveType.SelectedValue);
                    bllEmpLeaves.LeaveDays = Convert.ToInt32(txtDays.Text);
                    bllEmpLeaves.LeaveFrom = (txtFromDate.Text);
                    bllEmpLeaves.LeaveTo = (txtToDate.Text);
                    bllEmpLeaves.LeaveReason = txtReason.Text;

                    bllEmpLeaves.PMonth = ddlMonths.SelectedValue;


                    #endregion

                    int nAlreadyIn = 0;
                    if (mode != "Edit")
                    {
                        int existCount = IsExist();
                        if (existCount == 0)
                        {


                            #region 'Reservation Record Add'
                            bllEmpLeaves.CreateBy = Convert.ToInt32(Session["EmployeeCode"].ToString());
                            bllEmpLeaves.CreatedOn = DateTime.Now;
                            if (Convert.ToInt32(txtDays.Text) > 0)
                            {

                                nAlreadyIn = bllEmpLeaves.EmployeeLeavesAdd(bllEmpLeaves);

                                if (nAlreadyIn == 0)
                                {
                                    ViewState["gvReservations"] = null;
                                    bindgridReservation();

                                    ViewState["dtLeavesSubmitted"] = null;
                                    ViewState["dtLeavesNotSubmitted"] = null;

                                    bindgridLeaves();

                                    drawMsgBox("Data added successfully.", 1);

                                    DataTable _dtemp = new DataTable();
                                    _dtemp = (DataTable)Session["EmailTable"];
                                    if (_dtemp.Rows.Count > 0)
                                    {

                                        foreach (DataRow var in _dtemp.Rows)
                                        {
                                            string mailMsg = "Dear " + var["HODName"].ToString() + ", <br><br> Employee # : " + Session["EmployeeCode"].ToString() + ".<br> Name :" + Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString() + "<br> Has submitted leaves from Employee Reservation for HOD approval. You can find data in HOD Reservation.";
                                            mailMsg = mailMsg + "<br> LeaveFrom :" + txtFromDate.Text + ".<br> LeaveTo :" + txtToDate.Text + "<br> No. of Days:" + txtDays.Text + "<br> Reason:" + txtReason.Text;
                                            bllemail.SendEmail(var["HODEmail"].ToString(), "Attendance [Leave(s) Approval]", mailMsg);
                                        }

                                    }
                                    ResetControls();
                                }
                                else if (nAlreadyIn == 1)
                                {
                                    drawMsgBox("Data already exist.", 3);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            ViewState["gvReservations"] = null;
                            bindgridReservation();

                            ViewState["dtLeavesSubmitted"] = null;
                            ViewState["dtLeavesNotSubmitted"] = null;

                            bindgridLeaves();
                            drawMsgBox("Invalid Date Range! There are already some leaves reserved for the selected date range.", 2);
                        }
                    }
                    else
                    {
                        #region 'Update'
                        id = Convert.ToInt32(ViewState["EditID"]);
                        bllEmpLeaves.EmpLeave_Id = Int32.Parse(ViewState["EditID"].ToString());

                        bllEmpLeaves.ModifiedBy = Convert.ToInt32(Session["EmployeeCode"].ToString());
                        bllEmpLeaves.ModifiedOn = DateTime.Now;


                        nAlreadyIn = bllEmpLeaves.EmployeeLeavesUpdateEMP(bllEmpLeaves);
                        if (nAlreadyIn == 0)
                        {
                            drawMsgBox("Data modified successfully.", 1);
                            ViewState["gvReservations"] = null;
                            ResetControls();
                            bindgridReservation();
                        }
                        else if (nAlreadyIn == 1)
                        {
                            drawMsgBox("Data already exist.", 3);
                        }
                        #endregion
                    }


                    ///*Refresh Page*/
                    //bindgridLateArrivals();
                    //bindgridLeaves();
                    //bindgridMIO();
                    //bindgridReservation();

                }
                else
                {
                    drawMsgBox(_displymsg, 3);
                }
                lvdata.Visible = false;
                lvbtn.Visible = false;
            }
            else
            {
                drawMsgBox("Please Consider Sunday attatchment while submitting annual leaves", 3);

            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    private float CheckFurloughLeaveBalance()
    {
        float Flbalance;
        BLLEmployeeLeaveBalance leaveBalance = new BLLEmployeeLeaveBalance();

        leaveBalance.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
        leaveBalance.PMonth = Session["CurrentMonth"].ToString();

        DataTable dtFl = leaveBalance.EmployeeLeaveBalanceFurlough(leaveBalance);


        if (dtFl.Rows.Count > 0)
        {
            Flbalance = float.Parse(dtFl.Rows[0][0].ToString());
        }
        else
        {
            Flbalance = 0;
        }

        return Flbalance;
    }

    private void ResetControls()
    {

        txtDays.Text = "0";
        txtReason.Text = "";
        txtToDate.Text = "";

        DateTime d = DateTime.Now;
        txtFromDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();
        //   txtToDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();

        ddlLeaveType.SelectedValue = "0";
    }
    //protected void drawMsgBox(string msg, int errType)
    //{
    //    try
    //    {
    //        if (errType == 1)
    //        {
    //            uscMsgBox1.AddMessage(msg, MessageBoxUsc_uscMsgBox.enmMessageType.Success);
    //        }
    //        else if (errType == 2)
    //        {
    //            uscMsgBox1.AddMessage(msg, MessageBoxUsc_uscMsgBox.enmMessageType.Error);
    //        }
    //        else if (errType == 3)
    //        {
    //            uscMsgBox1.AddMessage(msg, MessageBoxUsc_uscMsgBox.enmMessageType.Attention);
    //        }
    //        else if (errType == 4)
    //        {
    //            uscMsgBox1.AddMessage(msg, MessageBoxUsc_uscMsgBox.enmMessageType.Info);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Session["error"] = ex.Message;
    //        Response.Redirect("ErrorPage.aspx", false);
    //    }
    //}

    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {

        string _leave = ddlLeaveType.SelectedValue;

        if (_leave == ((int)LeaveTypes.CasualLeaves).ToString())   /*Casual Leaves*/
        {

        }
        else if (_leave == ((int)LeaveTypes.AnnualLeaves).ToString()) /*Anual Leaves*/
        {
            if (Convert.ToInt32(txtDays.Text.ToString()) > Convert.ToInt32(balanceLeaves.Text.ToString()))
            {
                drawMsgBox("You cannot apply more than 60 leaves in a year'.", 0);
                ddlLeaveType.SelectedValue = "0";
            }

            //if (Session["probrationDate"] != DBNull.Value && Session["probrationDate"] != null)
            if (Session["probrationDate"].ToString().Trim() != "")
            {
                DateTime dt = Convert.ToDateTime(Session["probrationDate"]);

                if (dt.CompareTo(DateTime.Today) > 0)
                {
                    drawMsgBox("Can't Avail Annual Leaves in Probation.", 3);
                    ddlLeaveType.SelectedValue = "0";
                }
            }
            else
            {
                drawMsgBox("Probation end date is not correct", 0);
            }
        }
        else if (_leave == ((int)LeaveTypes.OffDay).ToString()) /*Off Day*/
        {
            drawMsgBox("Reservations is not allowed for this type", 3);
            ddlLeaveType.SelectedValue = "0";
        }
        else if (_leave == ((int)LeaveTypes.Present).ToString()) /*Present*/
        {
            drawMsgBox("Reservations is not allowed for this type", 3);
            ddlLeaveType.SelectedValue = "0";
        }
        else if (_leave == ((int)LeaveTypes.OfficialTourTask).ToString()) /*Offical Tour*/
        {
        }
        else if (_leave == ((int)LeaveTypes.ManualAttendancePunchCard).ToString()) /*Manual Punch Card*/
        {
            drawMsgBox("Reservations is not allowed for this type", 3);
            ddlLeaveType.SelectedValue = "0";
        }
        else if (_leave == ((int)LeaveTypes.Lieuof).ToString()) /*Lieu Off*/
        {
            drawMsgBox("Reservations is not allowed for this type", 3);
            ddlLeaveType.SelectedValue = "0";
        }
        else if (_leave == "0") /*Select*/
        {
            //            drawMsgBox("Reservations is not allowed for this type");
        }
        else if (_leave == ((int)LeaveTypes.LongLeaves).ToString()) /*Long Leave*/
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "confirm('Long Leaves will be submitted for approval to Higher Management after approval of HOD! If you dont want to proceed then change the leave type.');", true);
            //drawMsgBox("Long Leaves will be entered after approval of HOD and Higher Management!", 3);
        }
        else if (_leave == ((int)LeaveTypes.SpecialLeaves).ToString()) /*Special Leave*/
        {
            //drawMsgBox("Special Leaves will be entered after approval of HOD and Higher Management!",3);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "confirm('Special Leaves will be submitted for approval to Higher Management after approval of HOD! If you dont want to proceed then change the leave type.');", true);
        }
        else if (_leave == ((int)LeaveTypes.LeaveWithoutPay).ToString()) /*Leave Without Pay*/
        {
        }
        else if (_leave == ((int)LeaveTypes.FurloughLeave).ToString())
        {
            int LeaveBal = Convert.ToInt32(lblCas.Text) + Convert.ToInt32(lblAnu.Text);

            if (LeaveBal <= 0)
            {
                drawMsgBox("Can't Avail Furlough Leaves due to insufficent leave balance.", 3);
                ddlLeaveType.SelectedValue = "0";
            }
        }
    }

    //protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ViewState["gvReservations"] = null;
    //    bindgridReservation();
    //}

    private void RetrieveByID(int id)
    {
        leavetype();
        bllEmpLeaves.EmpLeave_Id = id;
        DataTable dt = bllEmpLeaves.EmployeeLeavesFetch(id);
        if (dt.Rows.Count > 0)
        {
            ddlLeaveType.SelectedValue = dt.Rows[0]["LeaveType_Id"].ToString();
            txtDays.Text = dt.Rows[0]["LeaveDays"].ToString();
            txtReason.Text = dt.Rows[0]["LeaveReason"].ToString();

            DateTime dF = Convert.ToDateTime(dt.Rows[0]["LeaveFrom"].ToString());
            DateTime dT = Convert.ToDateTime(dt.Rows[0]["LeaveTo"].ToString());

            txtFromDate.Text = dF.Month.ToString() + '/' + dF.Day.ToString() + '/' + dF.Year.ToString();
            txtToDate.Text = dT.Month.ToString() + '/' + dT.Day.ToString() + '/' + dT.Year.ToString();
        }
    }

    //protected void loadEmpleave()
    //{

    //    BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

    //    DataTable dt = new DataTable();
    //    obj.EmployeeCode = Session["EmployeeCode"].ToString();
    //    obj.Year = DateTime.Now.Year.ToString();
    //    dt = obj.EmployeeLeaveBalanceFetch(obj);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblActAnu.Text = dt.Rows[0]["ActAnnual"].ToString();
    //        lblActCas.Text = dt.Rows[0]["ActCasual"].ToString();

    //        lblNonApvAnu.Text = dt.Rows[0]["NonApvAnnual"].ToString();
    //        lblNonApvCas.Text = dt.Rows[0]["NonApvCasual"].ToString();

    //        lblCas.Text = dt.Rows[0]["balCasual"].ToString();
    //        lblAnu.Text = dt.Rows[0]["balAnnual"].ToString();
    //    }
    //}
    protected void bindgridReservation()
    {
        try
        {
            loadEmpleave();
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllEmpLeaves.EmployeeCode = Session["EmployeeCode"].ToString();
            //bllEmpLeaves.PMonth = ddlMonths.SelectedValue;

            if (ViewState["gvReservations"] == null)
                _dt = bllEmpLeaves.EmployeeLeavesFetchEMP(bllEmpLeaves);
            else
                _dt = (DataTable)ViewState["gvReservations"];

            if (_dt.Rows.Count > 0)
            {
                gvReservations.DataSource = _dt;
                ViewState["gvReservations"] = _dt;

                //div_leaveRequests.Visible = true;
            }
            //else
            //{
            //    div_leaveRequests.Visible = false;
            //}
            gvReservations.DataBind();

            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }


    protected void gvReservations_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["gvReservations"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["ReservationSortDirection"].ToString();

            if (ViewState["ReservationSortDirection"].ToString() == "ASC")
            {
                ViewState["ReservationSortDirection"] = "DESC";
            }
            else
            {
                ViewState["ReservationSortDirection"] = "ASC";
            }
            ViewState["gvReservations"] = null;
            bindgridReservation();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvReservations_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReservations.PageIndex = e.NewPageIndex;

            ViewState["gvReservations"] = null;
            bindgridReservation();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvReservations_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            bllEmpLeaves.EmpLeave_Id = Convert.ToInt32(gvReservations.Rows[e.RowIndex].Cells[0].Text);
            int id = bllEmpLeaves.EmpLeave_Id;

            DataTable dt = bllEmpLeaves.EmployeeLeavesFetch(id);
            if (dt.Rows.Count > 0)
            {

                bllEmpLeaves.EmployeeLeavesDelete(bllEmpLeaves);
                ViewState["gvReservations"] = null;
                bindgridReservation();
                drawMsgBox("Data Item Deleted!", 1);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvReservations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("ImageButton2");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure to delete the this Item?') ");
        }

        if (e.Row.RowIndex != -1)
        {

            if (e.Row.Cells[13].Text == "True")
            {
                if (e.Row.Cells[1].Text == "True")
                {
                    //e.Row.BackColor = System.Drawing.Color.Wheat;
                    e.Row.Cells[11].Enabled = false;
                    e.Row.Cells[12].Enabled = false;
                }
                else
                {
                    e.Row.Cells[11].Enabled = true;
                    e.Row.Cells[12].Enabled = true;

                }
                //e.Row.BackColor = System.Drawing.Color.Wheat;
                e.Row.Enabled = false;
            }
            else
            {
                e.Row.Enabled = true;

            }

        }
    }

    protected void gvReservations_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            ViewState["mode"] = "Edit";
            ViewState["EditID"] = this.gvReservations.Rows[e.NewSelectedIndex].Cells[0].Text;
            int id = Int32.Parse(ViewState["EditID"].ToString());
            lvdata.Visible = true;
            lvbtn.Visible = true;

            RetrieveByID(id);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls();
        bindgridReservation();
        lvdata.Visible = false;
        lvbtn.Visible = false;
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = "Add";
        lvdata.Visible = true;
        lvbtn.Visible = true;
        lvbtn.Focus();
        leavetype();
        ResetControls();
    }

    protected void lnkAddOFT_Click(object sender, EventArgs e)
    {

        ViewState["mode"] = "Add";
        lvdata.Visible = true;
        lvbtn.Visible = true;
        lvbtn.Focus();
        leavetypeOFT();
        ResetControls();
    }

    private int CalculateDays(string _fromDate, string _toDate)
    {
        int _ret = 0;
        if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 2)
        {

            DateTime dF = DateTime.ParseExact(_fromDate, "M/d/yyyy", null);

            DateTime dT = DateTime.ParseExact(_toDate, "M/d/yyyy", null);

            TimeSpan span = dT.Subtract(dF);
            if (span.Days >= 0)
            {
                _ret = span.Days + 1;
            }
            else
            {
                _ret = span.Days;
            }
        }
        return _ret;

    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {

        DataValidations();

    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DataValidations();
    }
    private void DataValidations()
    {
        int days = CalculateDays(txtFromDate.Text, txtToDate.Text);
        if (days < 0)
        {
            drawMsgBox("Invalid Date Range! 'From date' can not be greater than 'To date'.", 2);
            txtDays.Text = "0";
        }
        else
        {
            txtDays.Text = days.ToString();
        }
    }

    private int IsExist()
    {
        int t = 0;

        bllEmpLeaves.EmployeeCode = Session["EmployeeCode"].ToString();
        bllEmpLeaves.LeaveFrom = (txtFromDate.Text);
        bllEmpLeaves.LeaveTo = (txtToDate.Text);


        DataTable dt = bllEmpLeaves.EmployeeLeavesFetchRangeExist(bllEmpLeaves);
        if (dt.Rows.Count > 0)
        {
            t = Convert.ToInt32(dt.Rows[0]["counter"].ToString());
        }

        return t;
    }






    #endregion




    protected void btnViewAttReport_Click(object sender, EventArgs e)
    {
        PrintReport.PrintReportMonthly(AMSReports.AttendanceReport, Session["EmployeeCode"].ToString(), ddlMonths.SelectedValue.ToString(), "~/EmployeeLeavesSubmissions.aspx");
    }


    protected void btnViewLogReport_Click(object sender, EventArgs e)
    {
        PrintReport.PrintReportMonthly(AMSReports.EmployeeLogReport, Session["EmployeeCode"].ToString(), ddlMonths.SelectedValue.ToString(), "~/EmployeeLeavesSubmissions.aspx");
    }

    protected void btnUpdateAtt_Click(object sender, EventArgs e)
    {

        BLLAttendance bllobj = new BLLAttendance();

        bllobj.PMonthDesc = ddlMonths.SelectedValue;
        bllobj.EmployeeCode = Session["EmployeeCode"].ToString();

        int AlreadyIn = 0;

        AlreadyIn = bllobj.AttendanceProcessSingleEmployee(bllobj);

        if (AlreadyIn > 0)
        {
            drawMsgBox("Attendance process successfully completed.", 1);
            //ResetAllGrids();
        }
        else
            drawMsgBox("Error in attendance process!", 2);
    }

    //protected void AttendanceType()
    //{
    //    BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();

    //    DataTable objDt = new DataTable();

    //    objDt = objBLL.EmployeeAttendanceType();

    //    if (objDt.Rows.Count > 0)
    //    {
    //        ddlAttendanceType.DataSource = objDt;
    //        ddlAttendanceType.DataValueField = "AttendanceTypeId";
    //        ddlAttendanceType.DataTextField = "AttendanceType";
    //        ddlAttendanceType.DataBind();
    //        ddlAttendanceType.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //}

}