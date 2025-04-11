using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;
using System.Data.SqlClient;
using System.Configuration;

public partial class HODApprovals : System.Web.UI.Page
{

    DALBase objbase = new DALBase();
    BLLAttendance bllObj = new BLLAttendance();
    BLLEmployeeLeaves bllObjLeaves = new BLLEmployeeLeaves();
    
    private string empName = string.Empty;
    private string empEmail = string.Empty;
    
    int countLeavesLockedRows = 0;
    int countHalfDayLockedRows = 0;
    int countReservationLockedRows = 0;
    int countMissingInOutLockedRows = 0;
    int countLateArrivalLockedRows = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }


            #region 'Roles&Priviliges'


            if (Session["EmployeeCode"] != null)
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

                int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

                //int _result = objbase.ApplicationSettings(sRet, _part_Id);


                //if (_result == 1)
                //    {
                if (!IsPostBack)
                {
                    try
                    {

                        loadMonths();
                        loadEmployees();

                        ViewState["Leavesmode"] = "Add";
                        ViewState["LeavesSortDirection"] = "ASC";
                        ViewState["LeavesRecordChecked"] = "check";

                        ViewState["HalfDaySortDirection"] = "ASC";
                        ViewState["HalfDayRecordChecked"] = "check";

                        ViewState["ReservationSortDirection"] = "ASC";
                        ViewState["ReservationRecordChecked"] = "check";


                        ViewState["SortDirectionLateArrivals"] = "ASC";
                        ViewState["LateArrivalsRecordChecked"] = "check";
                        ViewState["MissingInOutRecordChecked"] = "check";


                        bindgridHalfDay();
                        bindgridLeaves();
                        bindgridReservation();
                        bindgridLateArrivals();
                        bindGridMissingInOut();
                        // register and execute document_ready method to reset/rebind autocomplete boxes
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "document_Ready", "document_Ready();", true);
                    }
                    catch (Exception ex)
                    {
                        Session["error"] = ex.Message;
                        Response.Redirect("ErrorPage.aspx", false);
                    }

                }
            #endregion
                //    }
                //else
                //    {
                //    Session["error"] = "You Are Not Authorized To Access This Page";
                //    Response.Redirect("ErrorPage.aspx", false);
                //    }
            }



        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    #region // Late Arrivals information section
    #region // grid events
    protected void gvLateArrivalsUnApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtApprovedLateArrivals"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLateArrivals"].ToString();

            if (ViewState["SortDirectionLateArrivals"].ToString() == "ASC")
            {
                ViewState["SortDirectionLateArrivals"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLateArrivals"] = "ASC";
            }
            ViewState["dtApprovedLateArrivals"] = null;
            bindgridLateArrivals();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLateArrivalsUnApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLateArrivalsUnApproved.PageIndex = e.NewPageIndex;

            ViewState["dtApprovedLateArrivals"] = null;
            bindgridLateArrivals();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLateArrivalsApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtUnApprovedLateArrivals"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLateArrivals"].ToString();

            if (ViewState["SortDirectionLateArrivals"].ToString() == "ASC")
            {
                ViewState["SortDirectionLateArrivals"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLateArrivals"] = "ASC";
            }
            ViewState["dtUnApprovedLateArrivals"] = null;
            bindgridLateArrivals();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLateArrivalsApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLateArrivalsApproved.PageIndex = e.NewPageIndex;

            ViewState["dtUnApprovedLateArrivals"] = null;
            bindgridLateArrivals();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLateArrivalsUnApproved_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {
            DropDownList ddlRoleTypeLateArrivals = (DropDownList)e.Row.FindControl("ddlRoleTypeLateArrivals");
            BLLEmployeeNegativeAttReason objBLL = new BLLEmployeeNegativeAttReason();
            objBLL.Status_id = 1;
            objBLL.Region_id = Convert.ToInt32((Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString());
            objBLL.Center_id = Convert.ToInt32((Session["CenterID"].ToString() == "") ? "0" : Session["CenterID"].ToString());
            DataTable objDt = new DataTable();

            objDt = objBLL.EmployeeNegativeAttReasonFetch(objBLL);
            ddlRoleTypeLateArrivals.DataSource = objDt;
            ddlRoleTypeLateArrivals.DataValueField = "NegHODApp_Id";
            ddlRoleTypeLateArrivals.DataTextField = "NegHODAppReason";
            ddlRoleTypeLateArrivals.DataBind();
            ddlRoleTypeLateArrivals.SelectedValue = e.Row.Cells[2].Text;
            ddlRoleTypeLateArrivals.Items.Insert(0, new ListItem("Select", "0"));


            DropDownList ddlAprvLateArrivals = (DropDownList)e.Row.FindControl("ddlAprvLateArrivals");
            objDt = new DataTable();


            BLLEmployeeLeaveType bll = new BLLEmployeeLeaveType();

            objDt = bll.EmployeeLeaveTypeFetch(1);
            ddlAprvLateArrivals.DataSource = objDt;
            ddlAprvLateArrivals.DataValueField = "Sat";
            ddlAprvLateArrivals.DataTextField = "Descr";
            ddlAprvLateArrivals.DataBind();

            ddlAprvLateArrivals.SelectedValue = "False";

            if (e.Row.Cells[16].Text == "True") // if Data Locked after ERP PRocesss
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
                countLateArrivalLockedRows += 1;
            }
            else
            {



                if (e.Row.Cells[1].Text == "False" && e.Row.Cells[7].Text == "&nbsp;")
                {
                    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFF0");
                    //e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");
                    e.Row.Enabled = false;
                    countLateArrivalLockedRows += 1;
                }
                else if (e.Row.Cells[1].Text == "False" && e.Row.Cells[6].Text == "&nbsp;")
                {
                    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFF0");
                    //e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");
                    e.Row.Enabled = false;
                    countLateArrivalLockedRows += 1;
                }

                else
                {
                    e.Row.Enabled = true;
                }

            }

        }
    }
    protected void gvLateArrivalsUnApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["LateArrivalsRecordChecked"].ToString();

                foreach (GridViewRow gvr in gvLateArrivalsUnApproved.Rows)
                {

                    if (gvr.Cells[9].Text == "Missing OUT" || gvr.Cells[9].Text == "Missing IN")
                    {

                        if (gvr.Cells[1].Text == "True")
                        {

                            cb = (CheckBox)gvr.FindControl("chkLateArrivals");

                            if (mood == "" || mood == "check")
                            {

                                cb.Checked = true;
                                ViewState["LateArrivalsRecordChecked"] = "uncheck";
                            }
                            else
                            {
                                cb.Checked = false;
                                ViewState["LateArrivalsRecordChecked"] = "check";
                            }

                        }


                    }
                    else
                    {

                        cb = (CheckBox)gvr.FindControl("chkLateArrivals");

                        if (mood == "" || mood == "check")
                        {

                            cb.Checked = true;
                            ViewState["LateArrivalsRecordChecked"] = "uncheck";
                        }
                        else
                        {
                            cb.Checked = false;
                            ViewState["LateArrivalsRecordChecked"] = "check";
                        }
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

    protected void btnReSubmitLateArrivals_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
        bllObj.AttendanceUpdateEmpNegReturn(bllObj);

        ViewState["dtApprovedLateArrivals"] = null;
        ViewState["dtUnApprovedLateArrivals"] = null;
        bindgridLateArrivals();

    }
    protected void btnEditLateArrivals_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEdit = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnEdit.CommandArgument);

        GridViewRow grv = (GridViewRow)btnEdit.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlAprvLateArrivals");
        DropDownList ddlApv = (DropDownList)ctl;

        ctl = (Control)grv.FindControl("ddlRoleTypeLateArrivals");
        DropDownList ddlLeave = (DropDownList)ctl;

        string _reason;
        string _leave;
        string _Apv;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;
        _Apv = ddlApv.SelectedValue;

        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlTy = null;
        TextBox txtReasonInner = null;
        foreach (GridViewRow gvRow in gvLateArrivalsUnApproved.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeLateArrivals");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlTy = (DropDownList)gvRow.FindControl("ddlAprvLateArrivals");

            cb = (CheckBox)gvRow.FindControl("chkLateArrivals");
            if (cb.Checked)
            {
                ddlLeaveInner.SelectedValue = _leave;
                txtReasonInner.Text = _reason;
                ddlTy.SelectedValue = _Apv;
                cb.Checked = false;
            }
        }
        ViewState["SortDirectionLateArrivals"] = "ASC";
        ViewState["LateArrivalsRecordChecked"] = "check";

    }
    protected void btnLateArrivalsSave_Click(object sender, EventArgs e)
    {
        int chk = 0;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAprvLateArrivals = null;
        TextBox txtReasonInner = null;
        Control ctl = null;
        foreach (GridViewRow gvRow in gvLateArrivalsUnApproved.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeLateArrivals");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlAprvLateArrivals = (DropDownList)gvRow.FindControl("ddlAprvLateArrivals");
            cb = (CheckBox)gvRow.FindControl("chkLateArrivals");

            if (cb.Checked)
            {
                int a = txtReasonInner.Text.Trim().Length;
                if (ddlLeaveInner.SelectedIndex > 0 && a > 0)
                {

                    bllObj.Att_Id = _id;

                    bllObj.HODApproval = Convert.ToBoolean(ddlAprvLateArrivals.SelectedValue);
                    bllObj.ApprovalReason = txtReasonInner.Text;

                    bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                    bllObj.ModifyDate = DateTime.Now;

                    bllObj.NegHODApp_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                    bllObj.isLock = true;

                    bllObj.AppNegBy = Session["EmployeeCode"].ToString();
                    bllObj.AppNegOn = DateTime.Now;


                    int dt = bllObj.AttendanceUpdateHODNeg(bllObj);
                    if (dt >= 1)
                    {
                        chk = chk + 1;
                    }
                }
            }
        }

        if (chk > 0)
        {
            drawMsgBox("Saved Successfully!", 1);
            ViewState["dtApprovedLateArrivals"] = null;
            ViewState["dtUnApprovedLateArrivals"] = null;
            ViewState["SortDirectionLateArrivals"] = "ASC";
            ViewState["LateArrivalsRecordChecked"] = "check";
            ddlEmp.SelectedValue = "0";
            bindgridLateArrivals();
        }
    }

    protected void ddlRoleTypeLateArrivals_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);

        TextBox txtReason = (TextBox)grdrDropDownRow.Cells[13].FindControl("txtReason");

        DropDownList ddlRoleTypeLateArrivals = (DropDownList)grdrDropDownRow.Cells[11].FindControl("ddlAprvLateArrivals"); ;

        if (ddlRoleTypeLateArrivals.SelectedValue == "True" && ddlCurrentDropDownList.SelectedValue == "4")
        {
            ddlCurrentDropDownList.SelectedValue = "0";
        }
        else if (ddlRoleTypeLateArrivals.SelectedValue == "False" && ddlCurrentDropDownList.SelectedValue != "4")
        {
            ddlCurrentDropDownList.SelectedValue = "4";

        }

        if (ddlCurrentDropDownList.SelectedValue == "4")
        {

            txtReason.Text = "Not Approved.";
        }
        else
        {
            txtReason.Text = "";

        }



    }
    protected void ddlAprvLateArrivals_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlAprc = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlAprc.Parent.Parent);

        TextBox txtReason = (TextBox)grdrDropDownRow.Cells[13].FindControl("txtReason");

        DropDownList ddlRoleTypeLateArrivals = (DropDownList)grdrDropDownRow.Cells[11].FindControl("ddlRoleTypeLateArrivals"); ;

        if (ddlAprc.SelectedValue == "True" && ddlRoleTypeLateArrivals.SelectedValue == "4")
        {
            ddlRoleTypeLateArrivals.SelectedValue = "0";
        }
        else if (ddlAprc.SelectedValue == "False" && ddlRoleTypeLateArrivals.SelectedValue != "4")
        {
            ddlRoleTypeLateArrivals.SelectedValue = "4";

        }

        //if (ddlAprc.SelectedValue == "False")
        //    {
        //    grdrDropDownRow.Cells[14].Enabled = true;
        //    }
        //else
        //    {
        //    grdrDropDownRow.Cells[14].Enabled = false;
        //    //ddlAprc.SelectedValue = grdrDropDownRow.Cells[1].Text;
        //    }
    }


    private void setLateArrivalSummary()
    {
        int Total = gvLateArrivalsUnApproved.Rows.Count;
        int Submitted = Total - countLateArrivalLockedRows;
        int NotSubmitted = Total - Submitted;
        int Approved = gvLateArrivalsApproved.Rows.Count;
        int NotApproved = Submitted - Approved;

        NotApproved = NotApproved < 0 ? 0 : NotApproved;
        lblLateArrivalsTotal.Text = Total.ToString();
        lblLateArrivalsSubmitted.Text = Submitted.ToString();
        lblLateArrivalsNotSubmitted.Text = NotSubmitted.ToString();
        lblLateArrivalsApproved.Text = Approved.ToString();
        lblLateArrivalsNotApproved.Text = NotApproved.ToString();
    }
    protected void bindgridLateArrivals()
    {
        DataTable _dt = new DataTable();

        gvLateArrivalsUnApproved.DataSource = null;
        gvLateArrivalsApproved.DataSource = null;


        bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
        bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
        bllObj.isLock = false;

        try
        {
            #region 'Fill Datagrid'

            if (ViewState["dtUnApprovedLateArrivals"] == null)
                _dt = bllObj.AttendanceFetchNegHODLate(bllObj);
            else
                _dt = (DataTable)ViewState["dtUnApprovedLateArrivals"];

            if (_dt.Rows.Count > 0)
            {
                gvLateArrivalsUnApproved.DataSource = _dt;
                ViewState["dtUnApprovedLateArrivals"] = _dt;

                div_lteUnApproved.Visible = true;
            }
            else
            {
                div_lteUnApproved.Visible = false;
            }
            gvLateArrivalsUnApproved.DataBind();

            //Show Read Only Grid 
            bllObj.isLock = true;
            if (ViewState["dtApprovedLateArrivals"] == null)
                _dt = bllObj.AttendanceFetchNegHODLate(bllObj);
            else
                _dt = (DataTable)ViewState["dtApprovedLateArrivals"];

            if (_dt.Rows.Count > 0)
            {
                gvLateArrivalsApproved.DataSource = _dt;
                ViewState["dtApprovedLateArrivals"] = _dt;

                div_lteApproved.Visible = true;
            }
            else
            {
                div_lteApproved.Visible = false;
            }

            gvLateArrivalsApproved.DataBind();

            setLateArrivalSummary();
            //=======================================


            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }
    #endregion

    #region // MissingInOut information section
    #region // grid events
    protected void gvMissingInOutUnApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtUnApprovedMissingInOut"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLateArrivals"].ToString();

            if (ViewState["SortDirectionLateArrivals"].ToString() == "ASC")
            {
                ViewState["SortDirectionLateArrivals"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLateArrivals"] = "ASC";
            }
            ViewState["dtUnApprovedMissingInOut"] = null;
            bindgridLateArrivals();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvMissingInOutUnApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMissingInOutUnApproved.PageIndex = e.NewPageIndex;

            ViewState["dtUnApprovedMissingInOut"] = null;
            bindgridLateArrivals();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvMissingInOutUnApproved_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {
            DropDownList ddlRoleTypeMissingInOut = (DropDownList)e.Row.FindControl("ddlRoleTypeMissingInOut");
            BLLEmployeeNegativeAttReason objBLL = new BLLEmployeeNegativeAttReason();
            objBLL.Status_id = 1;
            objBLL.Region_id = Convert.ToInt32((Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString());
            objBLL.Center_id = Convert.ToInt32((Session["CenterID"].ToString() == "") ? "0" : Session["CenterID"].ToString());
            DataTable objDt = new DataTable();

            objDt = objBLL.EmployeeNegativeAttReasonFetch(objBLL);
            ddlRoleTypeMissingInOut.DataSource = objDt;
            ddlRoleTypeMissingInOut.DataValueField = "NegHODApp_Id";
            ddlRoleTypeMissingInOut.DataTextField = "NegHODAppReason";
            ddlRoleTypeMissingInOut.DataBind();
            ddlRoleTypeMissingInOut.SelectedValue = e.Row.Cells[2].Text;
            ddlRoleTypeMissingInOut.Items.Insert(0, new ListItem("Select", "0"));


            //ddlRoleTypeMissingInOut.Items.Remove(ddlRoleTypeMissingInOut.Items.FindByText("Special Approval by HOD"));


            DropDownList ddlAprvMissingInOut = (DropDownList)e.Row.FindControl("ddlAprvMissingInOut");
            objDt = new DataTable();


            BLLEmployeeLeaveType bll = new BLLEmployeeLeaveType();

            objDt = bll.EmployeeLeaveTypeFetch(1);
            ddlAprvMissingInOut.DataSource = objDt;
            ddlAprvMissingInOut.DataValueField = "Sat";
            ddlAprvMissingInOut.DataTextField = "Descr";
            ddlAprvMissingInOut.DataBind();

            ddlAprvMissingInOut.SelectedValue = "False";

            if (e.Row.Cells[16].Text == "True") // if Data Locked after ERP PRocesss
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
                countMissingInOutLockedRows += 1;
            }
            else
            {

                if (e.Row.Cells[1].Text == "False" && e.Row.Cells[7].Text == "&nbsp;")
                {
                    //    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFF0");
                    //    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");
                    e.Row.Enabled = false;
                    countMissingInOutLockedRows += 1;
                }
                else if (e.Row.Cells[1].Text == "False" && e.Row.Cells[6].Text == "&nbsp;")
                {
                    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFF0");
                    //e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");
                    e.Row.Enabled = false;
                    countMissingInOutLockedRows += 1;
                }

                else
                {
                    e.Row.Enabled = true;
                }

            }

        }
    }
    protected void gvMissingInOutUnApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheckMIO")
            {
                CheckBox cb = null;
                string mood = ViewState["MissingInOutRecordChecked"].ToString();

                foreach (GridViewRow gvr in gvMissingInOutUnApproved.Rows)
                {

                    if (gvr.Cells[9].Text == "Missing OUT" || gvr.Cells[9].Text == "Missing IN")
                    {

                        if (gvr.Cells[1].Text == "True")
                        {

                            cb = (CheckBox)gvr.FindControl("chkMissingInOut");

                            if (mood == "" || mood == "check")
                            {

                                cb.Checked = true;
                                ViewState["MissingInOutRecordChecked"] = "uncheck";
                            }
                            else
                            {
                                cb.Checked = false;
                                ViewState["MissingInOutRecordChecked"] = "check";
                            }

                        }


                    }
                    else
                    {

                        cb = (CheckBox)gvr.FindControl("chkMissingInOut");

                        if (mood == "" || mood == "check")
                        {

                            cb.Checked = true;
                            ViewState["LateArrivalsRecordChecked"] = "uncheck";
                        }
                        else
                        {
                            cb.Checked = false;
                            ViewState["LateArrivalsRecordChecked"] = "check";
                        }
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

    protected void gvMissingInOutApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtApprovedMissingInOut"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionLateArrivals"].ToString();

            if (ViewState["SortDirectionLateArrivals"].ToString() == "ASC")
            {
                ViewState["SortDirectionLateArrivals"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionLateArrivals"] = "ASC";
            }
            ViewState["dtApprovedMissingInOut"] = null;
            bindgridLateArrivals();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvMissingInOutApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMissingInOutApproved.PageIndex = e.NewPageIndex;

            ViewState["dtUnApprovedLateArrivals"] = null;
            bindgridLateArrivals();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }


    #endregion

    protected void btnEditMissingInOut_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEdit = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnEdit.CommandArgument);

        GridViewRow grv = (GridViewRow)btnEdit.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlAprvMissingInOut");
        DropDownList ddlApv = (DropDownList)ctl;

        ctl = (Control)grv.FindControl("ddlRoleTypeMissingInOut");
        DropDownList ddlLeave = (DropDownList)ctl;

        string _reason;
        string _leave;
        string _Apv;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;
        _Apv = ddlApv.SelectedValue;

        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlTy = null;
        TextBox txtReasonInner = null;
        foreach (GridViewRow gvRow in gvMissingInOutUnApproved.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeMissingInOut");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlTy = (DropDownList)gvRow.FindControl("ddlAprvMissingInOut");

            cb = (CheckBox)gvRow.FindControl("chkMissingInOut");
            if (cb.Checked)
            {
                ddlLeaveInner.SelectedValue = _leave;
                txtReasonInner.Text = _reason;
                ddlTy.SelectedValue = _Apv;
                cb.Checked = false;
            }
        }
        ViewState["SortDirectionLateArrivals"] = "ASC";
        ViewState["MissingInOutRecordChecked"] = "check";

    }
    protected void btnMissingInOutSave_Click(object sender, EventArgs e)
    {

        int chk = 0;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAprvMissingInOut = null;
        TextBox txtReasonInner = null;
        Control ctl = null;
        foreach (GridViewRow gvRow in gvMissingInOutUnApproved.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleTypeMissingInOut");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlAprvMissingInOut = (DropDownList)gvRow.FindControl("ddlAprvMissingInOut");
            cb = (CheckBox)gvRow.FindControl("chkMissingInOut");

            if (cb.Checked)
            {
                int a = txtReasonInner.Text.Trim().Length;
                if (ddlLeaveInner.SelectedIndex > 0 && a > 0)
                {


                    bllObj.EmployeeCode = gvRow.Cells[3].Text.ToString();
                    bllObj.PMonthDesc =  this.ddlMonths.SelectedValue.ToString();

                    bool already_hlfday = false;

                    if (ddlLeaveInner.SelectedValue == "9000" )
                    {
                        DataTable dt_hlf_days = bllObj.GetHalfDaysUnOfficial(bllObj);

                        if (dt_hlf_days.Rows.Count > 0)
                        {
                            already_hlfday = true;
                        }
                    }

                    if (already_hlfday)
                    {
                        drawMsgBox("Employee can only avail one Half Casual Leave per month.", 0);
                    }
                    else
                    {


                        bllObj.Att_Id = _id;

                        bllObj.MIOHODAprv = Convert.ToBoolean(ddlAprvMissingInOut.SelectedValue);
                        bllObj.MIOApprovalReason = txtReasonInner.Text;

                        bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                        bllObj.ModifyDate = DateTime.Now;

                        bllObj.MIOHODApp_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                        bllObj.isLock = true;

                        bllObj.AppNegBy = Session["EmployeeCode"].ToString();
                        bllObj.AppNegOn = DateTime.Now;


                        int dt = bllObj.AttendanceUpdateHODNegMIO(bllObj);
                        if (dt >= 1)
                        {
                            chk = chk + 1;
                        }
                    }
                }
            }
        }
        if (chk > 0)
        {
            drawMsgBox("Saved Successfully!", 1);
            ViewState["dtUnApprovedMissingInOut"] = null;
            ViewState["dtApprovedMissingInOut"] = null;
            ViewState["SortDirectionLateArrivals"] = "ASC";
            ViewState["MissingInOutRecordChecked"] = "check";

            ddlEmp.SelectedValue = "0";
            //bindgridLateArrivals();//salman checking
            bindGridMissingInOut();
            

        }

    }
    protected void btnReSubmitMissingInOut_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
        bllObj.AttendanceUpdateEmpNegReturnMIO(bllObj);

        ViewState["dtUnApprovedMissingInOut"] = null;
        ViewState["dtApprovedMissingInOut"] = null;
        //bindgridLateArrivals();
        bindGridMissingInOut();

    }

    protected void ddlAprvMissingInOut_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlAprc = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlAprc.Parent.Parent);

        TextBox txtReason = (TextBox)grdrDropDownRow.Cells[13].FindControl("txtReason");

        DropDownList ddlRoleTypeMissingInOut = (DropDownList)grdrDropDownRow.Cells[11].FindControl("ddlRoleTypeMissingInOut");

        if (ddlAprc.SelectedValue == "True" && ddlRoleTypeMissingInOut.SelectedValue == "4")
        {
            ddlRoleTypeMissingInOut.SelectedValue = "0";
        }
        else if (ddlAprc.SelectedValue == "False" && ddlRoleTypeMissingInOut.SelectedValue != "4")
        {
            ddlRoleTypeMissingInOut.SelectedValue = "4";

        }

        //if (ddlAprc.SelectedValue == "False")
        //    {
        //    grdrDropDownRow.Cells[14].Enabled = true;
        //    }
        //else
        //    {
        //    grdrDropDownRow.Cells[14].Enabled = false;
        //    //ddlAprc.SelectedValue = grdrDropDownRow.Cells[1].Text;
        //    }
    }
    protected void ddlRoleTypeMissingInOut_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);

        TextBox txtReason = (TextBox)grdrDropDownRow.Cells[13].FindControl("txtReason");

        DropDownList ddlRoleTypeMissingInOut = (DropDownList)grdrDropDownRow.Cells[11].FindControl("ddlAprvMissingInOut"); ;

        if (ddlRoleTypeMissingInOut.SelectedValue == "True" && ddlCurrentDropDownList.SelectedValue == "4")
        {
            ddlCurrentDropDownList.SelectedValue = "0";
        }
        else if (ddlRoleTypeMissingInOut.SelectedValue == "False" && ddlCurrentDropDownList.SelectedValue != "4")
        {
            ddlCurrentDropDownList.SelectedValue = "4";

        }

        if (ddlCurrentDropDownList.SelectedValue == "4")
        {

            txtReason.Text = "Not Approved.";
        }
        else
        {
            txtReason.Text = "";

        }

        if (ddlCurrentDropDownList.SelectedValue == "9000" || ddlCurrentDropDownList.SelectedValue == "61")
        {
            //GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);

            double _casuals = countCasualsMissingGrid(grdrDropDownRow.Cells[3].Text);

            double _cal_bal = Convert.ToDouble(grdrDropDownRow.Cells[18].Text);

            if (_casuals > _cal_bal)
            {
                drawMsgBox("Can't exceed casual balance.", 0);

                ddlCurrentDropDownList.SelectedValue = "0";
            }

        }

    }

    private double countCasualsMissingGrid(string employeecode)
    {
        double _cl_counter = 0;

        DropDownList ddl_reason = null;

        foreach (GridViewRow ro in gvMissingInOutUnApproved.Rows)
        {
            ddl_reason = (DropDownList)ro.Cells[12].FindControl("ddlRoleTypeMissingInOut");

            if (ro.Cells[3].Text == employeecode && ddl_reason.SelectedValue == "61")
            {
                _cl_counter += 1;
            }
            else if (ro.Cells[3].Text == employeecode && ddl_reason.SelectedValue == "9000")
            {
                _cl_counter += 0.5;
            }
        }

        return _cl_counter;
    }

    private void setMissingInOutSummary()
    {
        int Total = gvMissingInOutUnApproved.Rows.Count;
        int Submitted = Total - countMissingInOutLockedRows;
        int NotSubmitted = Total - Submitted;
        int Approved = gvMissingInOutApproved.Rows.Count;
        int NotApproved = Submitted - Approved;

        NotApproved = NotApproved < 0 ? 0 : NotApproved;
        lblMissingInOutTotal.Text = Total.ToString();
        lblMissingInOutSubmitted.Text = Submitted.ToString();
        lblMissingInOutNotSubmitted.Text = NotSubmitted.ToString();
        lblMissingInOutApproved.Text = Approved.ToString();
        lblMissingInOutNotApproved.Text = NotApproved.ToString();
    }
    protected void bindGridMissingInOut()
    {

        DataTable _dt = new DataTable();

        gvMissingInOutUnApproved.DataSource = null;
        gvMissingInOutApproved.DataSource = null;


        bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
        bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
        bllObj.isLock = false;

        try
        {
            #region 'Fill Datagrid'

            bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObj.isLock = false;

            if (ViewState["dtUnApprovedMissingInOut"] == null)
                _dt = bllObj.AttendanceFetchNegHODMIO(bllObj);
            else
                _dt = (DataTable)ViewState["dtUnApprovedMissingInOut"];

            if (_dt.Rows.Count > 0)
            {
                gvMissingInOutUnApproved.DataSource = _dt;
                ViewState["dtUnApprovedMissingInOut"] = _dt;

                div_MIOUnApproved.Visible = true;
            }
            else
            {
                div_MIOUnApproved.Visible = false;
            }
            gvMissingInOutUnApproved.DataBind();


            //Show Read Only Grid 
            bllObj.isLock = true;
            if (ViewState["dtApprovedMissingInOut"] == null)
                _dt = bllObj.AttendanceFetchNegHODMIO(bllObj);
            else
                _dt = (DataTable)ViewState["dtApprovedMissingInOut"];

            if (_dt.Rows.Count > 0)
            {
                gvMissingInOutApproved.DataSource = _dt;
                ViewState["dtApprovedMissingInOut"] = _dt;

                div_MIOApproved.Visible = true;
            }
            else
            {
                div_MIOApproved.Visible = false;
            }

            gvMissingInOutApproved.DataBind();

            setMissingInOutSummary();

            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }



    }
    #endregion

    #region // Leaves information
    #region // leaves grid events
    protected void gvLeavesUnApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtUnApprovedLeaves"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["LeavesSortDirection"].ToString();

            if (ViewState["LeavesSortDirection"].ToString() == "ASC")
            {
                ViewState["LeavesSortDirection"] = "DESC";
            }
            else
            {
                ViewState["LeavesSortDirection"] = "ASC";
            }
            ViewState["dtUnApprovedLeaves"] = null;
            bindgridLeaves();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLeavesUnApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLeavesUnApproved.PageIndex = e.NewPageIndex;

            ViewState["dtUnApprovedLeaves"] = null;
            bindgridLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLeavesUnApproved_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {


            DropDownList ddlLeaveType = (DropDownList)e.Row.FindControl("ddlLeaveType");
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            objBLL.Status_id = 1;
            objBLL.Used_For = "LVS";
            DataTable objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);
            ddlLeaveType.DataSource = objDt;
            ddlLeaveType.DataValueField = "LeaveType_Id";
            ddlLeaveType.DataTextField = "LeaveType";
            ddlLeaveType.DataBind();
            ddlLeaveType.SelectedValue = e.Row.Cells[2].Text;
            DropDownList ddlApprvLeave = (DropDownList)e.Row.FindControl("ddlApprvLeave");
            objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetch(1);
            ddlApprvLeave.DataSource = objDt;
            ddlApprvLeave.DataValueField = "Sat";
            ddlApprvLeave.DataTextField = "Descr";
            ddlApprvLeave.DataBind();

            if (e.Row.Cells[17].Text == "True") // if Data Locked after ERP PRocesss
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
                countLeavesLockedRows = countLeavesLockedRows + 1;
            }
            else
            {
                if (e.Row.Cells[1].Text == "False")
                {
                    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFF0");
                    //e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");
                    e.Row.Enabled = false;
                    countLeavesLockedRows = countLeavesLockedRows + 1;
                }
                else
                {
                    //  e.Row.Cells[14].Enabled = false;
                }

            }

        }
    }
    protected void gvLeavesUnApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["LeavesRecordChecked"].ToString();

                foreach (GridViewRow gvr in gvLeavesUnApproved.Rows)
                {
                    if (gvr.Cells[1].Text == "True")
                    {

                        cb = (CheckBox)gvr.FindControl("chkLeaves");

                        if (mood == "" || mood == "check")
                        {

                            cb.Checked = true;
                            ViewState["LeavesRecordChecked"] = "uncheck";
                        }
                        else
                        {
                            cb.Checked = false;
                            ViewState["LeavesRecordChecked"] = "check";
                        }

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
    protected void gvLeavesApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtApprovedLeaves"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["LeavesSortDirection"].ToString();

            if (ViewState["LeavesSortDirection"].ToString() == "ASC")
            {
                ViewState["LeavesSortDirection"] = "DESC";
            }
            else
            {
                ViewState["LeavesSortDirection"] = "ASC";
            }
            ViewState["dtApprovedLeaves"] = null;
            bindgridLeaves();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLeavesApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLeavesApproved.PageIndex = e.NewPageIndex;

            ViewState["dtApprovedLeaves"] = null;
            bindgridLeaves();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    #endregion

    protected void btnLeavesSave_Click(object sender, EventArgs e)
    {
        int chk = 0;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlApprvLeave = null;
        TextBox txtReasonInner = null;
        Control ctl = null;
        foreach (GridViewRow gvRow in gvLeavesUnApproved.Rows)
        {
            //http://localhost:49915/Reports/rptEmpLvRunningBal.rpt
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlLeaveType");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlApprvLeave = (DropDownList)gvRow.FindControl("ddlApprvLeave");
            cb = (CheckBox)gvRow.FindControl("chkLeaves");

            if (cb.Checked)
            {

                if (txtReasonInner.Text != string.Empty)
                {

                    bllObj.Att_Id = _id;

                    bllObj.HODLvAprv = Convert.ToBoolean(ddlApprvLeave.SelectedValue);
                    bllObj.HODLvRemarks = txtReasonInner.Text;

                    bllObj.HRLvType_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                    bllObj.Submit2HR = true;

                    bllObj.AppLvBy = Session["EmployeeCode"].ToString();
                    bllObj.AppLvOn = DateTime.Now;

                    bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                    bllObj.ModifyDate = DateTime.Now;

                    int dt = bllObj.AttendanceUpdateEmpHODLvApv(bllObj);
                    if (dt >= 1)
                    {
                        chk = chk + 1;
                    }
                }
            }
        }

        if (chk > 0)
        {
            drawMsgBox("Saved Successfully!", 1);
            ViewState["dtUnApprovedLeaves"] = null;
            ViewState["dtApprovedLeaves"] = null;
            ViewState["LeavesSortDirection"] = "ASC";
            ViewState["LeavesRecordChecked"] = "check";
            ddlEmp.SelectedValue = "0";
            bindgridLeaves();
        }


    }
    protected void btnEditLeaves_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditLeaves = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnEditLeaves.CommandArgument);

        GridViewRow grv = (GridViewRow)btnEditLeaves.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlApprvLeave");
        DropDownList ddlLeave = (DropDownList)ctl;

        ctl = (Control)grv.FindControl("ddlLeaveType");
        DropDownList ddlLeaveN = (DropDownList)ctl;

        string _reason;
        string _leave;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;
        string _LeaveN = ddlLeaveN.SelectedValue;


        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlTy = null;
        TextBox txtReasonInner = null;
        foreach (GridViewRow gvRow in gvLeavesUnApproved.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlApprvLeave");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlTy = (DropDownList)gvRow.FindControl("ddlLeaveType");

            cb = (CheckBox)gvRow.FindControl("chkLeaves");
            if (cb.Checked)
            {
                if (_leave == "False")
                {
                    //gvRow.Cells[14].Enabled = true;
                }
                else
                {
                    // gvRow.Cells[14].Enabled = false;
                    ddlTy.SelectedValue = gvRow.Cells[2].Text;
                }
                ddlLeaveInner.SelectedValue = _leave;
                txtReasonInner.Text = _reason;
                //ddlTy.SelectedValue = _LeaveN;
                cb.Checked = false;
            }
        }

    }
    protected void btnLeavesResubmit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
        bllObj.AttendanceUpdateEmpReturn(bllObj);

        ViewState["dtUnApprovedLeaves"] = null;
        ViewState["dtApprovedLeaves"] = null;
        bindgridLeaves();

    }

    protected void ddlApprvLeave_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
        LeavePolicyImple(ddlCurrentDropDownList, grdrDropDownRow);
    }

    private void setLeavesSummary()
    {
        int leavesTotal = gvLeavesUnApproved.Rows.Count;
        int leavesSubmitted = leavesTotal - countLeavesLockedRows;
        int leavesNotSubmitted = leavesTotal - leavesSubmitted;
        int leavesApproved = gvLeavesApproved.Rows.Count;
        int leavesNotApproved = leavesSubmitted - leavesApproved;

        leavesNotApproved = leavesNotApproved < 0 ? 0 : leavesNotApproved;
        lblLeavesTotal.Text = leavesTotal.ToString();
        lblLeavesSubmitted.Text = leavesSubmitted.ToString();
        lblLeavesNotSubmitted.Text = leavesNotSubmitted.ToString();
        lblLeavesApproved.Text = leavesApproved.ToString();
        lblLeavesNotApproved.Text = leavesNotApproved.ToString();
    }
    protected int LeaveBalanceCounter(string _val)
    {
        int count = 0;
        DropDownList ddlLeaveInner = null;
        foreach (GridViewRow gvRow in gvLeavesUnApproved.Rows)
        {
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlLeaveType");
            if (ddlLeaveInner.SelectedValue == _val)
            {
                count = count + 1;
            }
        }
        return count;
    }
    protected void bindgridLeaves()
    {
        gvLeavesUnApproved.DataSource = null;
        gvLeavesApproved.DataSource = null;
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObj.Submit2HR = false;

            if (ViewState["dtUnApprovedLeaves"] == null)
                _dt = bllObj.AttendanceFetchHOD(bllObj);
            else
                _dt = (DataTable)ViewState["dtUnApprovedLeaves"];

            if (_dt.Rows.Count > 0)
            {
                gvLeavesUnApproved.DataSource = _dt;
                ViewState["dtUnApprovedLeaves"] = _dt;

                div_leavesUnApproved.Visible = true;
            }
            else
            {
                div_leavesUnApproved.Visible = false;
            }
            gvLeavesUnApproved.DataBind();

            //Show Read Only Grid Approved data
            bllObj.Submit2HR = true;
            if (ViewState["dtApprovedLeaves"] == null)
                _dt = bllObj.AttendanceFetchHOD(bllObj);
            else
                _dt = (DataTable)ViewState["dtApprovedLeaves"];

            if (_dt.Rows.Count > 0)
            {
                gvLeavesApproved.DataSource = _dt;
                ViewState["dtApprovedLeaves"] = _dt;

                div_leavesApproved.Visible = true;
            }
            else
            {
                div_leavesApproved.Visible = false;
            }

            gvLeavesApproved.DataBind();
            setLeavesSummary();

            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }
    #endregion

    #region  // Half Days information section
    #region //  Half Days grid events
    protected void gvHalfDayUnApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtUnApprovedHalfDays"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["HalfDaySortDirection"].ToString();

            if (ViewState["HalfDaySortDirection"].ToString() == "ASC")
            {
                ViewState["HalfDaySortDirection"] = "DESC";
            }
            else
            {
                ViewState["HalfDaySortDirection"] = "ASC";
            }
            ViewState["dtUnApprovedHalfDays"] = null;
            bindgridHalfDay();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvHalfDayUnApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvHalfDayUnApproved.PageIndex = e.NewPageIndex;

            ViewState["dtUnApprovedHalfDays"] = null;
            bindgridHalfDay();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvHalfDayUnApproved_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {
            DropDownList ddlHalfDayType = (DropDownList)e.Row.FindControl("ddlHalfDayType");
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            objBLL.Status_id = 1;
            objBLL.Used_For = "HLF";
            DataTable objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);
            ddlHalfDayType.DataSource = objDt;
            ddlHalfDayType.DataValueField = "LeaveType_Id";
            ddlHalfDayType.DataTextField = "LeaveType";
            ddlHalfDayType.DataBind();
            ddlHalfDayType.SelectedValue = e.Row.Cells[2].Text;
            DropDownList ddlAprvHalfDay = (DropDownList)e.Row.FindControl("ddlAprvHalfDay");
            objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetch(1);
            ddlAprvHalfDay.DataSource = objDt;
            ddlAprvHalfDay.DataValueField = "Sat";
            ddlAprvHalfDay.DataTextField = "Descr";
            ddlAprvHalfDay.DataBind();

            if (e.Row.Cells[17].Text == "True") // if Data Locked after ERP PRocesss
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
                countHalfDayLockedRows = countHalfDayLockedRows + 1;
            }
            else
            {
                if (e.Row.Cells[1].Text == "False")
                {
                    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFF0");
                    //e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");
                    e.Row.Enabled = false;
                    countHalfDayLockedRows = countHalfDayLockedRows + 1;
                }
                else
                {
                    //  e.Row.Cells[14].Enabled = false;
                }

            }

        }
    }
    protected void gvHalfDayUnApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["HalfDayRecordChecked"].ToString();

                foreach (GridViewRow gvr in gvHalfDayUnApproved.Rows)
                {
                    if (gvr.Cells[1].Text == "True")
                    {

                        cb = (CheckBox)gvr.FindControl("chkHalfDay");

                        if (mood == "" || mood == "check")
                        {

                            cb.Checked = true;
                            ViewState["HalfDayRecordChecked"] = "uncheck";
                        }
                        else
                        {
                            cb.Checked = false;
                            ViewState["HalfDayRecordChecked"] = "check";
                        }

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
    protected void gvHalfDayApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtApprovedHalfDays"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["HalfDaySortDirection"].ToString();

            if (ViewState["HalfDaySortDirection"].ToString() == "ASC")
            {
                ViewState["HalfDaySortDirection"] = "DESC";
            }
            else
            {
                ViewState["HalfDaySortDirection"] = "ASC";
            }
            ViewState["dtApprovedHalfDays"] = null;
            bindgridHalfDay();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvHalfDayApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvHalfDayApproved.PageIndex = e.NewPageIndex;

            ViewState["dtApprovedHalfDays"] = null;
            bindgridHalfDay();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    #endregion
    protected void btnHalfDaySave_Click(object sender, EventArgs e)
    {
        int chk = 0;
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAprvHalfDay = null;
        TextBox txtReasonInner = null;
        Control ctl = null;
        foreach (GridViewRow gvRow in gvHalfDayUnApproved.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlHalfDayType");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlAprvHalfDay = (DropDownList)gvRow.FindControl("ddlAprvHalfDay");
            cb = (CheckBox)gvRow.FindControl("chkHalfDay");

            if (cb.Checked)
            {

                if (txtReasonInner.Text != string.Empty)
                {

                    bllObj.EmployeeCode = gvRow.Cells[5].Text.ToString();
                    bllObj.PMonthDesc =  this.ddlMonths.SelectedValue.ToString();

                    bool already_hlfday = false;

                    if (ddlLeaveInner.SelectedValue == "9000" )
                    {
                        DataTable dt_hlf_days = bllObj.GetHalfDaysUnOfficial(bllObj);

                        if (dt_hlf_days.Rows.Count > 0)
                        {
                            already_hlfday = true;
                        }
                    }

                    if (already_hlfday)
                    {
                        drawMsgBox("Employee can only avail one Half Casual.", 0);
                    }
                    else
                    {

                        bllObj.Att_Id = _id;

                        bllObj.HODLvAprv = Convert.ToBoolean(ddlAprvHalfDay.SelectedValue);
                        bllObj.HODLvRemarks = txtReasonInner.Text;

                        bllObj.HRLvType_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                        bllObj.Submit2HR = true;

                        bllObj.AppLvBy = Session["EmployeeCode"].ToString();
                        bllObj.AppLvOn = DateTime.Now;

                        bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                        bllObj.ModifyDate = DateTime.Now;

                        int dt = bllObj.AttendanceUpdateEmpHODLvApv(bllObj);
                        if (dt >= 1)
                        {
                            chk = chk + 1;
                        }
                    }


                }
            }
        }

        if (chk > 0)
        {
            drawMsgBox("Saved Successfully!", 1);
            ViewState["dtUnApprovedHalfDays"] = null;
            ViewState["dtApprovedHalfDays"] = null;
            ViewState["HalfDaySortDirection"] = "ASC";
            ViewState["HalfDayRecordChecked"] = "check";
            ddlEmp.SelectedValue = "0";
            bindgridHalfDay();
        }


    }
    protected void btnEditHalfDay_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditHalfDay = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnEditHalfDay.CommandArgument);

        GridViewRow grv = (GridViewRow)btnEditHalfDay.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlAprvHalfDay");
        DropDownList ddlLeave = (DropDownList)ctl;

        ctl = (Control)grv.FindControl("ddlHalfDayType");
        DropDownList ddlLeaveN = (DropDownList)ctl;

        string _reason;
        string _leave;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;
        string _LeaveN = ddlLeaveN.SelectedValue;


        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlTy = null;
        TextBox txtReasonInner = null;
        foreach (GridViewRow gvRow in gvHalfDayUnApproved.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlAprvHalfDay");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlTy = (DropDownList)gvRow.FindControl("ddlHalfDayType");

            cb = (CheckBox)gvRow.FindControl("chkHalfDay");
            if (cb.Checked)
            {
                if (_leave == "False")
                {
                    //gvRow.Cells[14].Enabled = true;
                }
                else
                {
                    // gvRow.Cells[14].Enabled = false;
                    ddlTy.SelectedValue = gvRow.Cells[2].Text;
                }
                ddlLeaveInner.SelectedValue = _leave;
                txtReasonInner.Text = _reason;
                //ddlTy.SelectedValue = _LeaveN;
                cb.Checked = false;
            }
        }

    }
    protected void btnReSubmitHalfDay_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
        bllObj.AttendanceUpdateEmpReturn(bllObj);

        ViewState["dtUnApprovedHalfDays"] = null;
        ViewState["dtApprovedHalfDays"] = null;
        bindgridHalfDay();

    }

    protected void ddlHalfDayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
        LeavePolicyImple(ddlCurrentDropDownList, grdrDropDownRow);
    }

    protected void ddlAprvHalfDay_SelectedIndexChanged(object sender, EventArgs e)
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

    private void setHalfDaySummary()
    {
        int Total = gvHalfDayUnApproved.Rows.Count;
        int Submitted = Total - countHalfDayLockedRows;
        int NotSubmitted = Total - Submitted;
        int Approved = gvHalfDayApproved.Rows.Count;
        int NotApproved = Submitted - Approved;

        NotApproved = NotApproved < 0 ? 0 : NotApproved;
        lblHalfDaysTotal.Text = Total.ToString();
        lblHalfDaySubmitted.Text = Submitted.ToString();
        lblHalfDayNotSubmitted.Text = NotSubmitted.ToString();
        lblHalfDayApproved.Text = Approved.ToString();
        lblHalfDayNotApproved.Text = NotApproved.ToString();
    }

    protected void bindgridHalfDay()
    {

        gvHalfDayUnApproved.DataSource = null;
        gvHalfDayApproved.DataSource = null;

        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllObj.PMonthDesc = ddlMonths.SelectedValue.ToString();
            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObj.Submit2HR = false;

            //bllObj.Submit2HOD = 

            if (ViewState["dtUnApprovedHalfDays"] == null)
                _dt = bllObj.HalfDaysFetchHOD(bllObj);
            else
                _dt = (DataTable)ViewState["dtUnApprovedHalfDays"];

            if (_dt.Rows.Count > 0)
            {
                gvHalfDayUnApproved.DataSource = _dt;
                ViewState["dtUnApprovedHalfDays"] = _dt;

                div_halfDayUnApproved.Visible = true;
            }
            else
            {
                div_halfDayUnApproved.Visible = false;
            }

            gvHalfDayUnApproved.DataBind();


            //Show Read Only Grid Approved data
            bllObj.Submit2HR = true;
            if (ViewState["dtApprovedHalfDaysShow"] == null)
                _dt = bllObj.HalfDaysFetchHOD(bllObj);
            else
                _dt = (DataTable)ViewState["dtApprovedHalfDaysShow"];

            if (_dt.Rows.Count > 0)
            {
                gvHalfDayApproved.DataSource = _dt;
                ViewState["dtApprovedHalfDaysShow"] = _dt;

                div_halfDayApproved.Visible = true;
            }
            else
            {
                div_halfDayApproved.Visible = false;
            }

            gvHalfDayApproved.DataBind();
            setHalfDaySummary();

            #endregion


        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }
    #endregion

    #region // reservation information section
    #region // grid events
    protected void gvReservationUnApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtUnApprovedReservation"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["ReservationSortDirection"].ToString();

            if (ViewState["ReservationSortDirection"].ToString() == "ASC")
            {
                ViewState["ReservationSortDirection"] = "DESC";
            }
            else
            {
                ViewState["ReservationSortDirection"] = "ASC";
            }
            ViewState["dtUnApprovedReservation"] = null;
            bindgridReservation();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvReservationUnApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReservationUnApproved.PageIndex = e.NewPageIndex;

            ViewState["dtUnApprovedReservation"] = null;
            bindgridReservation();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvReservationUnApproved_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            DataTable objDt = new DataTable();

            DropDownList ddlAprvReservation = (DropDownList)e.Row.FindControl("ddlAprvReservation");
            objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetch(1);
            ddlAprvReservation.DataSource = objDt;
            ddlAprvReservation.DataValueField = "Sat";
            ddlAprvReservation.DataTextField = "Descr";
            
            ddlAprvReservation.DataBind();

            ListItem defaultItem = new ListItem("Select", "-1");
            ddlAprvReservation.Items.Insert(0, defaultItem);

            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlAprvCategory");

            if (ddl != null)
            {
                // Manually add items to the DropDownList
                ddl.Items.Clear(); // Clear any pre-existing items

                ddl.Items.Add(new ListItem("Select", "-1"));
                //ddl.Items.Add(new ListItem("Approve", "approve"));
                //ddl.Items.Add(new ListItem("Carryforward", "carryforward"));
                //ddl.Items.Add(new ListItem("Encashment", "encashment")); 
            }

            if (e.Row.Cells[2].Text == "True")
            {
                //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFF0");
                //e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC7F32");
                e.Row.Enabled = false;
                countReservationLockedRows = countReservationLockedRows + 1;
            }
            else
            {
                //  e.Row.Cells[14].Enabled = false;
            } 
        }
    }
    protected void gvReservationUnApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["ReservationRecordChecked"].ToString();

                foreach (GridViewRow gvr in gvReservationUnApproved.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("chkReservations");

                    if (mood == "" || mood == "check")
                    {

                        cb.Checked = true;
                        ViewState["ReservationRecordChecked"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["ReservationRecordChecked"] = "check";
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
    protected void gvReservationApproved_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtApprovedReservation"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["ReservationSortDirection"].ToString();

            if (ViewState["ReservationSortDirection"].ToString() == "ASC")
            {
                ViewState["ReservationSortDirection"] = "DESC";
            }
            else
            {
                ViewState["ReservationSortDirection"] = "ASC";
            }
            ViewState["dtApprovedReservation"] = null;
            bindgridReservation();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvReservationApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReservationApproved.PageIndex = e.NewPageIndex;

            ViewState["dtApprovedReservation"] = null;
            bindgridReservation();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }


    #endregion

    protected void ddlAprvCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlAprc = (DropDownList)sender;
        GridViewRow grdrDropDownRow = (GridViewRow)ddlAprc.NamingContainer; // More robust way to get GridViewRow 

        string leaveDaysValue = grdrDropDownRow.Cells[8].Text;

        if (leaveDaysValue != null) // Ensure the TextBox was found
        {
            int leaveDays;
            bool isValidNumber = int.TryParse(leaveDaysValue, out leaveDays); // Try to parse the text to an integer

            // Check if the leaveDays value is valid
            if (isValidNumber)
            {
                // Check for 'carryforward' condition
                if (ddlAprc.SelectedValue == "carryforward")
                {
                    if (leaveDays < 15)
                    {
                        drawMsgBox("Employees are allowed to take Annual Leave, but Annual Leave cannot be carried forward if the remaining balance is less than 15 days.", 0);
                    }
                }
                // Check for 'encashment' condition
                else if (ddlAprc.SelectedValue == "encashment")
                {
                    if (leaveDays < 15)
                    {
                        drawMsgBox("Employees can avail Annual Leave, but Annual Leave cannot be encashed if the remaining balance is less than 15 days.", 0);
                    }
                }
            }
            else
            {
                // Handle invalid number input (optional)                
            }
        }
        else
        {
            // Handle if TextBox was not found (optional)
        }
    }

    protected void ddlAprvReservation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlAprc = (DropDownList)sender; // The DropDownList that triggered the event
        GridViewRow grdrDropDownRow = (GridViewRow)ddlAprc.NamingContainer; // The row containing the DropDownList

        // Find the other DropDownList in the same row (replace "ddlOptions" with the actual ID of the other DropDownList)
        DropDownList ddl = (DropDownList)grdrDropDownRow.FindControl("ddlAprvCategory");

        if (ddl != null) // Ensure the DropDownList was found
        {
            ddl.Items.Clear(); // Clear any pre-existing items

            // Add items to the other DropDownList based on the selected value of ddlAprvReservation
            if (ddlAprc.SelectedValue == "False")
            {
                ddl.Items.Add(new ListItem("Select", "-1"));

                //if (DateTime.Now > new DateTime(DateTime.Now.Year, 11, 27) && DateTime.Now <= new DateTime(DateTime.Now.Year, 12, 26))
                //{
                    //ddl.Items.Add(new ListItem("Carryforward", "carryforward"));
                //}

                ddl.Items.Add(new ListItem("Rejected", "rejected"));
                //ddl.Items.Add(new ListItem("Carryforward", "carryforward"));
                //ddl.Items.Add(new ListItem("Encashment", "encashment"));
            }
            else if (ddlAprc.SelectedValue == "True")
            {
                ddl.Items.Add(new ListItem("Select", "-1"));
                ddl.Items.Add(new ListItem("Approved", "approved"));
            }
            else
            {
                return;
            }
        }
    }


    protected void btnReservationSave_Click(object sender, EventArgs e)
    {
        CheckBox cb;
        DropDownList ddlAprvReservation;
        DropDownList ddlAprvCategory;
        TextBox txtReasonInner;
        bool isSelected = false;
        int _id = 0;

        foreach (GridViewRow gvRow in gvReservationUnApproved.Rows)
        {
            cb = (CheckBox)gvRow.FindControl("chkReservations");
            if (cb.Checked)
            {
                isSelected = true;
                break;
            }
        }
        if (isSelected)
        {
            foreach (GridViewRow gvRow in gvReservationUnApproved.Rows)
            {
                _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
                txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                ddlAprvReservation = (DropDownList)gvRow.FindControl("ddlAprvReservation");
                ddlAprvCategory = (DropDownList)gvRow.FindControl("ddlAprvCategory");
                cb = (CheckBox)gvRow.FindControl("chkReservations");
                //string leaveDaysValue = gvRow.Cells[8].Text;
                bool isOk = true;

                if (cb.Checked)
                {
                    if (txtReasonInner.Text != string.Empty)
                    {
                        bllObjLeaves.EmpLeave_Id = _id;
                        bllObjLeaves.HODRemakrs = txtReasonInner.Text;

                        if (ddlAprvReservation.SelectedValue != "-1")
                        {
                            bllObjLeaves.HODApprove = Convert.ToBoolean(ddlAprvReservation.SelectedValue);

                            if (ddlAprvCategory.SelectedValue != "-1")
                            {
                                //int leaveDays = Convert.ToInt32(leaveDaysValue); 

                                //if (ddlAprvCategory.SelectedValue == "carryforward" || ddlAprvCategory.SelectedValue == "encashment")
                                //{
                                //    if (leaveDays < 15)
                                //    {
                                //        isOk = false;
                                //    }
                                //    else
                                //    {
                                //        isOk = true;
                                //    }
                                //}
                                 
                                if (isOk)
                                { 
                                    //if (ddlAprvCategory.SelectedValue == "encashment")
                                    //{
                                    //    if (Session["UserType"].ToString() == "23")
                                    //    {
                                    //        LeaveEncashmentSubmitToHR4SLT(_id, txtReasonInner.Text, Convert.ToInt32(gvRow.Cells[4].Text.Trim()));
                                    //    }
                                    //}

                                    bllObjLeaves.AprvCategory = ddlAprvCategory.SelectedValue;

                                    bllObjLeaves.isLock = true;

                                    bllObjLeaves.HODAPVBy = Convert.ToInt32(Session["EmployeeCode"].ToString());
                                    bllObjLeaves.HPDAPVOn = DateTime.Now;

                                    bllObjLeaves.ModifiedBy = Convert.ToInt32(Session["EmployeeCode"].ToString());
                                    bllObjLeaves.ModifiedOn = DateTime.Now;

                                    int dt = bllObjLeaves.EmployeeLeavesUpdateHOD(bllObjLeaves);
                                    if (dt >= 1)
                                    {
                                        bllObj.EmployeeCode = gvRow.Cells[4].Text.Trim();
                                        bllObj.PMonthDesc = ddlMonths.SelectedValue;
                                        if (gvRow.Cells[1].Text != "&nbsp;")
                                        {
                                            if (Convert.ToInt32(gvRow.Cells[1].Text) != 65 && Convert.ToInt32(gvRow.Cells[1].Text) != 66)
                                            {
                                                bllObj.AttendanceProcessReservation(bllObj);
                                            }
                                        }
                                        drawMsgBox("Saved Successfully!", 1);
                                        ViewState["dtUnApprovedReservation"] = null;
                                        ViewState["dtApprovedReservation"] = null;
                                        ddlEmp.SelectedValue = "0";
                                        bindgridReservation();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            drawMsgBox("Please select the leave(s) to approve!", 3);
        }
    }
    private void LeaveEncashmentSubmitToHR4SLT(int lv_id, string remarks,int empcode)
    {
        BLLSendEmail bllemail = new BLLSendEmail();
        SelectEmployeeInfo(empcode);
        
        string mailMsg = "Dear " + empName + ",<br><br>This is to inform you that your Head of Department has submitted your leave encashment request to HR for further approval processing.<br><br>";
        mailMsg += "Regards:<br><br>";
        mailMsg += Session["CenterName"] + " (HOD)";

        bllemail.SendEmailNew(empEmail, "approval/disapproval for leave encashment", mailMsg, "");
        string mailTo = string.Empty; 

        //CR Emails
        if (Session["RegionID"].ToString() == "40000000")
        {
            mailTo = "Ams_alert_cr_leave_encashment_process@csn.edu.pk";
        }

        //SR Emails
        if (Session["RegionID"].ToString() == "20000000") 
        {
            mailTo = "Ams_alert_sr_leave_encashment_process@csn.edu.pk";
        }

        //NR Emails
        if (Session["RegionID"].ToString() == "30000000") 
        {
            mailTo = "Ams_alert_nr_leave_encashment_process@csn.edu.pk";
        }

        string mailMsg1 = "Dear HR " + Session["RegionName"] + ",<br><br>This is to inform you that your " + Session["CenterName"] + " Head of Department has submitted leave encashment request to HR for Employee " + empName + " for further approval processing.<br><br>";
        mailMsg1 += "Regards:<br><br>";
        mailMsg1 += Session["CenterName"] + " (HOD)";

        bllemail.SendEmailNew(mailTo, "approval/disapproval for leave encashment", mailMsg1, "");
         
        UpdateSubmitToHR(lv_id, remarks);
    }
    private void UpdateSubmitToHR(int hfEmpLeaveId, string HodRemarks)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        string updateQuery = "UPDATE EmployeeLeaves " +
                         "SET EncashmentSubmitToHR = @EncashmentSubmitToHR, " +
                         "EncashmentHodRemarks = @EncashmentHodRemarks " +
                         "WHERE EmpLeave_Id = @EmpLeaveId";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a command to execute the query
            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@EncashmentSubmitToHR", 1);
            command.Parameters.AddWithValue("@EncashmentHodRemarks", HodRemarks);
            command.Parameters.AddWithValue("@EmpLeaveId", hfEmpLeaveId);

            try
            {
                // Open the connection
                connection.Open();

                // Execute the query
                int rowsAffected = command.ExecuteNonQuery();

                // Check if any rows were affected
                if (rowsAffected > 0)
                {
                    drawMsgBox("The leave encashment request has been successfully submitted to HR!", 1);
                }
                else
                {
                    //Console.WriteLine("No record found to update.");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    public void SelectEmployeeInfo(int empCode)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        string selectQuery = "SELECT FullName, Email FROM EmployeeProfile WHERE EmployeeCode = @EmployeeCode";  // Use parameterized query

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                // Add parameter to the query to prevent SQL injection
                command.Parameters.AddWithValue("@EmployeeCode", empCode);

                // Execute the query and retrieve the result
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            empName = reader["FullName"].ToString();
                            empEmail = reader["Email"].ToString();
                        }
                    }
                }
            }
        }
    }
    protected void btnEditReservation_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEditReservation = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnEditReservation.CommandArgument);

        GridViewRow grv = (GridViewRow)btnEditReservation.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlAprvReservation");
        DropDownList ddlLeave = (DropDownList)ctl;


        string _reason;
        string _leave;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;


        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlTy = null;
        TextBox txtReasonInner = null;
        foreach (GridViewRow gvRow in gvReservationUnApproved.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlAprvReservation");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");

            cb = (CheckBox)gvRow.FindControl("chkReservations");
            if (cb.Checked)
            {
                ddlLeaveInner.SelectedValue = _leave;
                txtReasonInner.Text = _reason;
                cb.Checked = false;
            }
        }

    }
    protected void btnReservationReSubmit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        bllObjLeaves.EmpLeave_Id = Convert.ToInt32(imgbtn.CommandArgument);

        bllObjLeaves.EmployeeLeavesUpdateEMPReturn(bllObjLeaves);
        drawMsgBox("Re-Submitted Successfully!", 1);
        ViewState["dtUnApprovedReservation"] = null;
        ViewState["dtApprovedReservation"] = null;
        ddlEmp.SelectedValue = "0";
        bindgridReservation();



    }
    private void setReservationSummary()
    {
        int reservationTotal = gvReservationUnApproved.Rows.Count;
        int reservationSubmitted = reservationTotal - countReservationLockedRows;
        int reservationNotSubmitted = reservationTotal - reservationSubmitted;
        int reservationApproved = gvReservationApproved.Rows.Count;
        int reservationNotApproved = reservationSubmitted - reservationApproved;

        reservationNotApproved = reservationNotApproved < 0 ? 0 : reservationNotApproved;
        lblReservationTotal.Text = reservationTotal.ToString();
        lblReservationSubmitted.Text = reservationSubmitted.ToString();
        lblReservationNotSubmitted.Text = reservationNotSubmitted.ToString();
        lblReservationApproved.Text = reservationApproved.ToString();
        lblReservationNotApproved.Text = reservationNotApproved.ToString();
    }
    protected void bindgridReservation()
    {
        gvReservationUnApproved.DataSource = null;
        gvReservationApproved.DataSource = null;
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            bllObjLeaves.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObjLeaves.HODApprove = false;

            if (ViewState["dtUnApprovedReservation"] == null)
                _dt = bllObjLeaves.EmployeeLeavesFetchHOD(bllObjLeaves);
            else
                _dt = (DataTable)ViewState["dtUnApprovedReservation"];

            if (_dt.Rows.Count > 0)
            {
                gvReservationUnApproved.DataSource = _dt;
                ViewState["dtUnApprovedReservation"] = _dt;

                div_ReservationsUnApproved.Visible = true;
            }
            else
            {
                div_ReservationsUnApproved.Visible = false;
            }
            gvReservationUnApproved.DataBind();

            //Show Read Only Grid 
            bllObjLeaves.HODApprove = true;
            if (ViewState["dtApprovedReservation"] == null)
                _dt = bllObjLeaves.EmployeeLeavesFetchHOD(bllObjLeaves);
            else
                _dt = (DataTable)ViewState["dtApprovedReservation"];

            if (_dt.Rows.Count > 0)
            {
                gvReservationApproved.DataSource = _dt;
                ViewState["dtApprovedReservation"] = _dt;

                div_reservationsApproved.Visible = true;
            }
            else
            {
                div_reservationsApproved.Visible = false;
            }
            gvReservationApproved.DataBind();

            setReservationSummary();
            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }
    #endregion



    #region  // General methods information





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
    protected void loadEmployees()
    {

        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

        DataTable dt = new DataTable();

        obj.ReportTo = Session["EmployeeCode"].ToString().Trim();
        obj.UserType_id = Convert.ToInt32(Session["UserType"].ToString());

        int UserLevel, UserType;

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());

        if (UserLevel == 4) //Campus
        {
            obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
            obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
        }
        else if (UserLevel == 3)//Region
        {
            obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
            obj.Center_id = 0;
        }
        else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
        {
            obj.Region_id = 0;
            obj.Center_id = 0;

        }


        obj.Status_id = 1;
        obj.DeptCode = 0;
        obj.PMonthDesc = ddlMonths.SelectedValue;
        dt = obj.EmplyeeReportToFetch(obj);
        if (dt.Rows.Count > 0)
        {
            //dt.Rows
            ddlEmp.DataSource = dt;
            ddlEmp.DataValueField = "code";
            ddlEmp.DataTextField = "Descr";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void AssignToAll(string _lvt, string _reas, string _cvr)
    {
        DropDownList ddlLeaveInner = new DropDownList();
        TextBox txtReasonInner = new TextBox();
        CheckBox cb = new CheckBox();
        foreach (GridViewRow gvRow in gvLeavesUnApproved.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlLeaveType");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            cb = (CheckBox)gvRow.FindControl("chkLeaves");
            if (cb.Checked)
            {
                if (gvRow.Cells[1].Text == "1")
                {
                    ddlLeaveInner.SelectedValue = "73";
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
    private void LeavePolicyImple(DropDownList ddlCurrentDropDownList, GridViewRow grdrDropDownRow)
    {
        int _valCasul = 0;
        int _valAnual = 0;
        int _valisAnl = 0;

        int _index = grdrDropDownRow.RowIndex;
        _valCasul = LeaveBalanceCounter("61");
        _valAnual = LeaveBalanceCounter("62");

        if (ddlCurrentDropDownList.SelectedValue == "61")   /*Casual Leaves*/
        {

            if (_valCasul > Convert.ToInt32(grdrDropDownRow.Cells[8].Text))
            {
                drawMsgBox("Maximum " + grdrDropDownRow.Cells[8].Text + " casual leaves are allowed.", 3);
                ddlCurrentDropDownList.SelectedValue = "67";
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == "62") /*Anual Leaves*/
        {
            _valisAnl = objbase.IsAnual(grdrDropDownRow.Cells[3].Text);
            _valAnual = LeaveBalanceCounter("62");

            if (Convert.ToInt32(grdrDropDownRow.Cells[9].Text) < 3)
            {
                if (_valCasul < Convert.ToInt32(grdrDropDownRow.Cells[8].Text))
                {
                    ddlCurrentDropDownList.SelectedValue = "61";
                }
                else
                {
                    ddlCurrentDropDownList.SelectedValue = "67";
                }
            }
            else
            {
                if (_valisAnl < 3)
                {
                    if (_valCasul < Convert.ToInt32(grdrDropDownRow.Cells[8].Text))
                    {
                        ddlCurrentDropDownList.SelectedValue = "61";
                    }
                    else
                    {
                        ddlCurrentDropDownList.SelectedValue = "67";
                    }
                }
                else
                {

                    if (_valAnual >= Convert.ToInt32(grdrDropDownRow.Cells[9].Text))
                    {
                        //Cehck for Casul Balance then Assign
                        if (_valCasul < Convert.ToInt32(grdrDropDownRow.Cells[8].Text))
                        {
                            ddlCurrentDropDownList.SelectedValue = "61";

                        }
                        else
                        {
                            ddlCurrentDropDownList.SelectedValue = "67";
                        }
                    }
                    else
                    {
                        ddlCurrentDropDownList.SelectedValue = "62";
                    }
                }

            }


        }
        else if (ddlCurrentDropDownList.SelectedValue == "73") /*Off Day*/
        {
        }
        else if (ddlCurrentDropDownList.SelectedValue == "2") /*Present*/
        {
            //        OffDaySettings(_index, "73", ddlCurrentDropDownList.SelectedItem.ToString());
        }
        else if (ddlCurrentDropDownList.SelectedValue == "68") /*Offical Tour*/
        {
            //      OffDaySettings(_index, "73", ddlCurrentDropDownList.SelectedItem.ToString());
        }
        else if (ddlCurrentDropDownList.SelectedValue == "72") /*Lieu Off*/
        {
            //    OffDaySettings(_index, "73", ddlCurrentDropDownList.SelectedItem.ToString());
        }
        else if (ddlCurrentDropDownList.SelectedValue == "66") /*Special Leave*/
        {
            //   OffDaySettings(_index, "73", ddlCurrentDropDownList.SelectedItem.ToString());
        }
        else if (ddlCurrentDropDownList.SelectedValue == "67") /*Leave Without Pay*/
        {
            //  OffDaySettings(_index, "67", ddlCurrentDropDownList.SelectedItem.ToString());
        }
    }

    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dtUnApprovedMissingInOut"] = null;
        ViewState["dtApprovedMissingInOut"] = null;


        ViewState["dtUnApprovedLateArrivals"] = null;
        ViewState["dtApprovedLateArrivals"] = null;


        ViewState["dtUnApprovedLeaves"] = null;
        ViewState["dtApprovedLeaves"] = null;

        ViewState["dtUnApprovedHalfDays"] = null;
        ViewState["dtApprovedHalfDays"] = null;



        //        ViewState["dtUnApprovedReservation"] = null;
        //      ViewState["dtApprovedReservation"] = null;
        ddlEmp.SelectedValue = "0";
        //loadEmployees();

        bindgridLeaves();
        bindgridHalfDay();
        bindgridLateArrivals();
        bindGridMissingInOut();
    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtLateArrivalsApproved = (DataTable)ViewState["dtApprovedLateArrivals"];
        DataTable dtLateArrivalsUnApproved = (DataTable)ViewState["dtUnApprovedLateArrivals"];

        DataView dvLateArrivalsApproved = new DataView();
        DataView dvLateArrivalsUnApproved = new DataView();

        DataTable dtMissingInOutApproved = (DataTable)ViewState["dtApprovedMissingInOut"];
        DataTable dtMissingInOutUnApproved = (DataTable)ViewState["dtUnApprovedMissingInOut"];

        DataView dvMissingInOutApproved = new DataView();
        DataView dvMissingInOutUnApproved = new DataView();

        DataTable dtLeavesApproved = (DataTable)ViewState["dtApprovedLeaves"];
        DataTable dtLeavesUnApproved = (DataTable)ViewState["dtUnApprovedLeaves"];

        DataView dvLeavesApproved = new DataView();
        DataView dvLeavesUnApproved = new DataView();

        DataTable dtHalfDayApproved = (DataTable)ViewState["dtApprovedHalfDays"];
        DataTable dtHalfDayUnApproved = (DataTable)ViewState["dtUnApprovedHalfDays"];

        DataView dvHalfDayApproved = new DataView();
        DataView dvHalfDayUnApproved = new DataView();

        DataTable dtReservationApproved = (DataTable)ViewState["dtApprovedReservation"];
        DataTable dtReservationUnApproved = (DataTable)ViewState["dtUnApprovedReservation"];

        DataView dvReservationApproved = new DataView();
        DataView dvReservationUnApproved = new DataView();

        string strFilter = "";

        if (ddlEmp.SelectedValue == "0")
        {
            bindgridLeaves();
            bindgridHalfDay();
            bindgridReservation();
            bindgridLateArrivals();
            bindGridMissingInOut();
        }
        else
        {
            strFilter = "EmployeeCode like '%" + ddlEmp.SelectedValue.Trim() + "%'";
            #region//filter late arrivals record
            //filter late arrivals un-approved record
            if (ViewState["dtUnApprovedLateArrivals"] != null)
            {
                dvLateArrivalsUnApproved = dtLateArrivalsUnApproved.DefaultView;
                dvLateArrivalsUnApproved.RowFilter = strFilter;
                gvLateArrivalsUnApproved.DataSource = dvLateArrivalsUnApproved;
                //ViewState["dtfilter"] = dv.ToTable();
                gvLateArrivalsUnApproved.DataBind();
            }

            //filter late arrivals approved record
            if (ViewState["dtApprovedLateArrivals"] != null)
            {
                dvLateArrivalsApproved = dtLateArrivalsApproved.DefaultView;
                dvLateArrivalsApproved.RowFilter = strFilter;
                gvLateArrivalsApproved.DataSource = dvLateArrivalsApproved;
                //ViewState["dtfiltershow"] = dvShow.ToTable();

                gvLateArrivalsApproved.DataBind();
            }

            #endregion

            #region // filter MissingInOut record
            // filter MissingInOut  un-approved record
            if (ViewState["dtUnApprovedMissingInOut"] != null)
            {
                dvMissingInOutUnApproved = dtMissingInOutUnApproved.DefaultView;
                dvMissingInOutUnApproved.RowFilter = strFilter;
                gvMissingInOutUnApproved.DataSource = dvMissingInOutUnApproved;
                //ViewState["dtfilter"] = dv.ToTable();
                gvMissingInOutUnApproved.DataBind();
            }

            // filter MissingInOut  approved record
            if (ViewState["dtApprovedMissingInOut"] != null)
            {
                dvMissingInOutApproved = dtMissingInOutApproved.DefaultView;
                dvMissingInOutApproved.RowFilter = strFilter;
                gvMissingInOutApproved.DataSource = dvMissingInOutApproved;
                //ViewState["dtfiltershow"] = dvShow.ToTable();

                gvMissingInOutApproved.DataBind();
            }

            #endregion

            #region // filter leaves record
            //filter leaves un-approved record
            if (ViewState["dtUnApprovedLeaves"] != null)
            {
                dvLeavesUnApproved = dtLeavesUnApproved.DefaultView;
                dvLeavesUnApproved.RowFilter = strFilter;
                gvLeavesUnApproved.DataSource = dvLeavesUnApproved;
                //ViewState["dtfilter"] = dv.ToTable();
                gvLeavesUnApproved.DataBind();
            }

            //filter leaves approved record
            if (ViewState["dtApprovedLeaves"] != null)
            {
                dvLeavesApproved = dtLeavesApproved.DefaultView;
                dvLeavesApproved.RowFilter = strFilter;
                gvLeavesApproved.DataSource = dvLeavesApproved;
                //ViewState["dtfiltershow"] = dvShow.ToTable();

                gvLeavesApproved.DataBind();
            }

            #endregion


            #region // filter half day record
            //filter half day un-approved record
            if (ViewState["dtUnApprovedHalfDays"] != null)
            {
                dvHalfDayUnApproved = dtHalfDayUnApproved.DefaultView;
                dvHalfDayUnApproved.RowFilter = strFilter;
                gvHalfDayUnApproved.DataSource = dvHalfDayUnApproved;
                gvHalfDayUnApproved.DataBind();
            }

            //filter half day approved record
            if (ViewState["dtApprovedHalfDays"] != null)
            {
                dvHalfDayApproved = dtHalfDayApproved.DefaultView;
                dvHalfDayApproved.RowFilter = strFilter;
                gvHalfDayApproved.DataSource = dvHalfDayApproved;
                gvHalfDayApproved.DataBind();
            }

            #endregion

            #region // filter reservation record
            //filter reservation un-approved record
            if (ViewState["dtUnApprovedReservation"] != null)
            {
                dvReservationUnApproved = dtReservationUnApproved.DefaultView;
                dvReservationUnApproved.RowFilter = strFilter;
                gvReservationUnApproved.DataSource = dvReservationUnApproved;
                gvReservationUnApproved.DataBind();
            }

            //filter reservation approved record
            if (ViewState["dtApprovedReservation"] != null)
            {
                dvReservationApproved = dtReservationApproved.DefaultView;
                dvReservationApproved.RowFilter = strFilter;
                gvReservationApproved.DataSource = dvReservationApproved;
                gvReservationApproved.DataBind();
            }

            #endregion
        }

        setMissingInOutSummary();
        setLateArrivalSummary();
        setLeavesSummary();
        setHalfDaySummary();
        setReservationSummary();

    }
    #endregion














    protected void ddlLeavesStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}