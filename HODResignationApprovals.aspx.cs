using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class HODResignationApprovals : System.Web.UI.Page
{

    BLLEmployeeResignationTermination bllObj = new BLLEmployeeResignationTermination();

    int countReservationLockedRows = 0;
    DropDownList ddlApproveResignation;
    DropDownList ddlApproveTermination;



    string hodEmail = string.Empty;
    string hodName = string.Empty;

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


                //====== End Page Access settings ======================

                int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

                if (!IsPostBack)
                {
                    try
                    {

                        loadMonths();
                        loadEmployees();


                        bindgridResignation();
                    }
                    catch (Exception ex)
                    {
                        Session["error"] = ex.Message;
                        Response.Redirect("ErrorPage.aspx", false);
                    }

                }
                #endregion
            }



        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }




    #region // reservation information section
    #region // grid events
    protected void gvResignationUnApproved_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            DataTable objDt = new DataTable();

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
    protected void gvResignationUnApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["ResignationRecordChecked"].ToString();

                foreach (GridViewRow gvr in gvResignationUnApproved.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("chkResignations");

                    if (mood == "" || mood == "check")
                    {

                        cb.Checked = true;
                        ViewState["ResignationRecordChecked"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["ResignationRecordChecked"] = "check";
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

    protected void btnResignationSave_Click(object sender, EventArgs e)
    {
        BLLSendEmail bllemail = new BLLSendEmail();
        CheckBox cb;
        TextBox txtReasonInner, txtNoticePeriod, txthodProposeLastDate;
        bool isSelected = false;
        string mailMsg;

        try
        {
            foreach (GridViewRow gvRow in gvResignationUnApproved.Rows)
            {
                cb = (CheckBox)gvRow.FindControl("chkResignations");
                if (cb.Checked)
                {
                    isSelected = true;
                    break;
                }
            }
            if (isSelected)
            {
                bool isOK = false;
                foreach (GridViewRow gvRow in gvResignationUnApproved.Rows)
                {
                    txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                    txthodProposeLastDate = (TextBox)gvRow.FindControl("hodProposeLastDate");
                    txtNoticePeriod = (TextBox)gvRow.FindControl("NoticePeriod");
                    ddlApproveResignation = (DropDownList)gvRow.FindControl("ddlApproveResignation");
                    cb = (CheckBox)gvRow.FindControl("chkResignations");

                    if (cb.Checked)
                    {
                        if (txtReasonInner.Text != string.Empty)
                        {
                            if (ddlApproveResignation.SelectedValue != "-1")
                            {
                                isOK = true;
                                DateTime lastdatecheck;
                                DateTime proposeDateCheck;

                                lastdatecheck = DateTime.ParseExact(gvRow.Cells[5].Text, "MM/dd/yyyy", null);
                                proposeDateCheck = DateTime.ParseExact(txthodProposeLastDate.Text, "MM/dd/yyyy", null);
                                bllObj.EmployeeCode = gvRow.Cells[1].Text.ToString().Trim();
                                bllObj.HODRemarks = txtReasonInner.Text;
                                bllObj.Reason = gvRow.Cells[7].Text.ToString();
                                bllObj.ModifiedBy = Session["EmployeeCode"].ToString().Trim();
                                bllObj.ModifiedOn = DateTime.Now;
                                bllObj.ApprovedDate = DateTime.Now;
                                bllObj.HODApprove = Convert.ToBoolean(ddlApproveResignation.SelectedValue);
                                bllObj.StatusId = bllObj.HODApprove == true ? 1 : 2;
                                bllObj.LastWorkingDate = proposeDateCheck.Date;
                                bllObj.HODProposeDate = proposeDateCheck.Date;
                                bllObj.NoticePeriod = Convert.ToInt32(txtNoticePeriod.Text);

                                bool isApprove = bllObj.HODApprove;

                                var regionId = gvRow.Cells[10].Text.ToString();
                                var centerId = gvRow.Cells[11].Text.ToString();
                                int intCenterId = Convert.ToInt32(centerId);

                                DateTime submittionDate = DateTime.ParseExact(gvRow.Cells[4].Text, "MM/dd/yyyy", null);
                                bllObj.LastWorkingDate = proposeDateCheck.Date;
                                bllObj.SubmissionDate = submittionDate;

                                //Send data to ERP - start

                                string sentToERP = "";

                                if (isApprove)
                                {
                                    sentToERP = bllObj.EmployeeResignationDataToERP(bllObj); // Approval case returns "T"
                                }
                                else
                                {
                                    sentToERP = "T"; // Rejection case
                                }

                                //Send data to ERP - end

                                if (sentToERP == "T")
                                {
                                    int dt = bllObj.EmployeeResignationUpdateHOD(bllObj);

                                    if (dt >= 1)
                                    {
                                        string ccEmails = string.Empty;

                                        //HO Emails
                                        if (regionId == "0" && centerId == "0")
                                        {
                                            ccEmails = "Ams_alert_ho_resignation_process@csn.edu.pk";
                                        }

                                        //CR Emails
                                        if (regionId == "40000000" && centerId == "0")
                                        {
                                            ccEmails = "Ams_alert_cr_resignation_process@csn.edu.pk";
                                        }

                                        //SR Emails
                                        if (regionId == "20000000" && centerId == "0")
                                        {
                                            ccEmails = "Ams_alert_sr_resignation_process@csn.edu.pk";
                                        }

                                        //NR Emails
                                        if (regionId == "30000000" && centerId == "0")
                                        {
                                            ccEmails = "Ams_alert_nr_resignation_process@csn.edu.pk";
                                        }

                                        //Email
                                        string subject = "";

                                        if (isApprove)
                                        {
                                            subject = "Resignation Approval Confirmation";
                                            mailMsg = "<html>" +
                                                "<body>" +
                                                "<p>Dear " + gvRow.Cells[2].Text.ToString() + ",</p>" +
                                                "<p>This is to confirm that your resignation has been approved. Your last working day will be " + proposeDateCheck.ToShortDateString() + ". Please find the details of your resignation below:</p>" +
                                                "<p>Employee No: " + gvRow.Cells[1].Text.ToString() + "</p>" +
                                                "<p>Employee Name: " + gvRow.Cells[2].Text.ToString() + "</p>" +
                                                "<p>Resignation date: " + gvRow.Cells[4].Text.ToString() + "</p>" +
                                                "<p>Last working date: " + gvRow.Cells[5].Text.ToString() + "</p>" +
                                                "<p>Last working date remarks: " + gvRow.Cells[9].Text.ToString() + "</p>" +
                                                "<p>Notice period: " + gvRow.Cells[6].Text.ToString() + "</p>" +
                                                "<p>Reason for resignation: " + gvRow.Cells[7].Text.ToString() + "</p>" +
                                                "<p>Comments by employee: " + gvRow.Cells[8].Text.ToString() + "</p>" +
                                                "<p>Thank you for your contributions, and we wish you the best in your future endeavours.</p>" +
                                                "</body>" +
                                                "</html>";
                                        }
                                        else
                                        {
                                            subject = "Resignation Request Update";
                                            mailMsg = "<html>" +
                                                "<body>" +
                                                "<p>Dear " + gvRow.Cells[2].Text.ToString() + ",</p>" +
                                                "<p>This is to inform you that your resignation request has been reviewed and rejected by your line manager.</p>" +
                                                "<p>If you require further clarification or wish to discuss this matter, please feel free to reach out.</p>" +
                                                "</body>" +
                                                "</html>";
                                        }

                                        var email = gvRow.Cells[3].Text.ToString();

                                        bllemail.SendEmailNew(email, subject, mailMsg, ccEmails);

                                        bllObj.IsEmailSent = true;
                                        int alreadyIn = bllObj.EmployeeResignationEmailUpdate(bllObj);

                                        if (isApprove)
                                        {
                                            bllObj.IsSentToERP = true;
                                            bllObj.EmployeeResignationERPUpdate(bllObj);
                                        }
                                        else
                                        {
                                            bllObj.IsSentToERP = false;
                                            bllObj.EmployeeResignationERPUpdate(bllObj);
                                        }
                                    }
                                }
                                else
                                {
                                    isOK = false;
                                }
                            }
                        }
                    }
                }
                if (isOK)
                {
                    drawMsgBox("Saved Successfully!", 1);
                    ViewState["dtUnApprovedResignation"] = null;
                    ViewState["dtApprovedResignation"] = null;
                    ddlEmp.SelectedValue = "0";
                    bindgridResignation();
                }
                else
                {
                    drawMsgBox("An error has occurred. Please contact the IT department for assistance!", 2);
                    ViewState["dtUnApprovedResignation"] = null;
                    ViewState["dtApprovedResignation"] = null;
                    ddlEmp.SelectedValue = "0";
                    bindgridResignation();

                }
            }
            else
            {
                drawMsgBox("Please select the resignation(s) to approve!", 3);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnTerminationSave_Click(object sender, EventArgs e)
    {
        BLLSendEmail bllemail = new BLLSendEmail();
        CheckBox cb;
        TextBox txtReasonInner, txtNoticePeriod, txthodSupervisorApprovedDate;
        bool isSelected = false;
        string mailMsg;

        try
        {
            foreach (GridViewRow gvRow in gvTerminationUnApproved.Rows)
            {
                cb = (CheckBox)gvRow.FindControl("chkTerminations");
                if (cb.Checked)
                {
                    isSelected = true;
                    break;
                }
            }
            if (isSelected)
            {
                foreach (GridViewRow gvRow in gvTerminationUnApproved.Rows)
                {
                    txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
                    txthodSupervisorApprovedDate = (TextBox)gvRow.FindControl("hodSupervisorApprovedDate");
                    txtNoticePeriod = (TextBox)gvRow.FindControl("NoticePeriod");
                    ddlApproveTermination = (DropDownList)gvRow.FindControl("ddlApproveTermination");
                    cb = (CheckBox)gvRow.FindControl("chkTerminations");

                    if (cb.Checked)
                    {
                        if (txtReasonInner.Text != string.Empty)
                        {
                            DateTime lastdatecheck;
                            DateTime proposeDateCheck;

                            lastdatecheck = DateTime.ParseExact(gvRow.Cells[5].Text, "MM/dd/yyyy", null);
                            proposeDateCheck = DateTime.ParseExact(txthodSupervisorApprovedDate.Text, "MM/dd/yyyy", null);
                            bllObj.EmployeeCode = gvRow.Cells[1].Text.ToString();
                            bllObj.HODSupervisorRemarks = txtReasonInner.Text;
                            bllObj.Reason = gvRow.Cells[7].Text.ToString();
                            bllObj.Category = "Termination";
                            bllObj.ModifiedBy = Session["EmployeeCode"].ToString();
                            bllObj.ModifiedOn = DateTime.Now;
                            bllObj.HODSupervisorApprove = Convert.ToBoolean(ddlApproveTermination.SelectedValue);
                            bllObj.StatusId = bllObj.HODSupervisorApprove == true ? 1 : 2;
                            bllObj.LastWorkingDate = proposeDateCheck.Date;
                            bllObj.HODSupervisorApprovedDate = proposeDateCheck.Date;
                            bllObj.NoticePeriod = Convert.ToInt32(txtNoticePeriod.Text);

                            var regionId = gvRow.Cells[10].Text.ToString();
                            var centerId = gvRow.Cells[11].Text.ToString();
                            var ccEmails = "";

                            int dt = bllObj.EmployeeTerminationUpdateHODSupervisor(bllObj);

                            DataTable dtEmployee = new DataTable();
                            dtEmployee = bllObj.SingleEmployeeDetails(gvRow.Cells[1].Text.ToString());

                            if (dt >= 1)
                            {
                                if (dtEmployee.Rows.Count > 0)
                                {
                                    //dt.Rows
                                    var employeeNameText = dtEmployee.Rows[0]["FullName"].ToString();
                                    var designationText = dtEmployee.Rows[0]["Designation"].ToString();
                                    var departmentText = dtEmployee.Rows[0]["Department"].ToString();
                                    var regionText = dtEmployee.Rows[0]["Region"].ToString();
                                    var centerText = dtEmployee.Rows[0]["Center"].ToString();
                                    var employeeEmail = dtEmployee.Rows[0]["Email"].ToString();

                                    //Email

                                    if (gvRow.Cells[7].Text.ToString() == "Gross misconduct")
                                    {
                                        //mailMsg = "<html><body><div><html><body><p>Date:  " + DateTime.Now.ToShortDateString() + "</p><br><p><b>" + gvRow.Cells[2].Text.ToString() + "</b></p><p>" + designationText + "</p><p><b>The City School</b></p><p>" + regionText + "</p><p>" + centerText + " </p><br><p style='text-align: center'><u><b>DISCONTINUATION OF SERVICES - MISCONDUCT</b></u></p><br><p>" + gvRow.Cells[2].Text.ToString() + ",</p><p>We regret to inform you that the company no longer requires your services as per the clause 9.1.2 (a to i) of the Service Rules of The City School. Please consider this letter as a notice for the above-mentioned subject and your last working day shall be <b>" + lastdatecheck.ToShortDateString() + ".</b></p><p>Your final settlement will be released as per the Service Rules.</p><br><p>Sincerely,</p><p>" + Session["First_Name"] + " " + Session["Middle_Name"] + "</p><p>" + Session["Designation"] + "</p></body></html><div/></body></html>";
                                        mailMsg = "<html><body><div><html><body><p>Dear " + gvRow.Cells[2].Text.ToString() + ",</p><p>We regret to inform you that, in accordance with clause 9.1.2 (a - i) of the Service Rules of The City School, your services will no longer be required. Please consider this email as formal notice of the termination of your employment. Your last working day will be <b>" + lastdatecheck.ToShortDateString() + ".</b></p><p>Your final settlement will be processed in line with the company’s Service Rules.</p><p>We appreciate your contributions and wish you the best in your future endeavours.</p><p>Sincerely,</p><p>" + Session["First_Name"] + " " + Session["Middle_Name"] + "</p><p>" + Session["Designation"] + "</p></body></html><div/></body></html>";
                                    }
                                    else
                                    {
                                        //mailMsg = "<html><body><div><html><body><p>Date:  " + DateTime.Now.ToShortDateString() + "</p><br><p><b>" + gvRow.Cells[2].Text.ToString() + "</b></p><p>" + designationText + "</p><p><b>The City School</b></p><p>" + regionText + "</p><p>" + centerText + " </p><br><p style='text-align: center'><u><b>Discontinuation of Services</b></u></p><br><p>" + gvRow.Cells[2].Text.ToString() + ",</p><p>We regret to inform you that the company no longer requires your services as per the clause 8.1.1 of the Service Rules of The City School. Please consider this letter as a notice for the above-mentioned subject and your last working day will be <b>" + lastdatecheck.ToShortDateString() + ".</b></p><p>Your final settlement will be released as per the Service Rules.</p><br><p>Sincerely,</p><p>" + Session["First_Name"] + " " + Session["Middle_Name"] + "</p><p>" + Session["Designation"] + "</p></body></html><div/></body></html>";
                                        mailMsg = "<html><body><div><html><body><p>Dear " + gvRow.Cells[2].Text.ToString() + ",</p><p>We regret to inform you that, in accordance with clause 8.1.1 of the Service Rules of The City School, your services will no longer be required. Please consider this email as formal notice of the termination of your employment. Your last working day will be <b>" + lastdatecheck.ToShortDateString() + ".</b></p><p>Your final settlement will be processed in line with the company’s Service Rules.</p><p>We appreciate your contributions and wish you the best in your future endeavours.</p><p>Sincerely,</p><p>" + Session["First_Name"] + " " + Session["Middle_Name"] + "</p><p>" + Session["Designation"] + "</p></body></html><div/></body></html>";
                                    }

                                    var email = gvRow.Cells[3].Text.ToString();

                                    //HO Emails
                                    if (regionId == "0" && centerId == "0")
                                    {
                                        ccEmails = "";
                                        ccEmails = "Ams_alert_ho_resignation_process@csn.edu.pk";
                                    }

                                    //CR Emails
                                    if (regionId == "40000000" && centerId == "0")
                                    {
                                        ccEmails = "";
                                        ccEmails = "Ams_alert_cr_resignation_process@csn.edu.pk";
                                        //ccEmails = "Samra.ali1@csn.edu.pk,Tariq.rashid@csn.edu.pk,Raheel.shahzad@csn.edu.pk,Usama.syed@csn.edu.pk";
                                    }

                                    //SR Emails
                                    if (regionId == "20000000" && centerId == "0")
                                    {
                                        ccEmails = "";
                                        ccEmails = "Ams_alert_sr_resignation_process@csn.edu.pk";
                                        //ccEmails = "Charlotte.dsouza@csn.edu.pk,Swera.saleem@csn.edu.pk,Farooq.masood@csn.edu.pk";
                                    }

                                    //NR Emails
                                    if (regionId == "30000000" && centerId == "0")
                                    {
                                        ccEmails = "";
                                        ccEmails = "Ams_alert_nr_resignation_process@csn.edu.pk";
                                    }

                                    bllemail.SendEmailNew(email, "Employment Termination Notice", mailMsg, ccEmails);
                                    bllObj.IsEmailSent = true;
                                    int alreadyIn = bllObj.EmployeeResignationEmailUpdate(bllObj);

                                    bllObj.LastWorkingDate = proposeDateCheck.Date;
                                    DateTime submittionDate = DateTime.ParseExact(gvRow.Cells[4].Text, "MM/dd/yyyy", null);
                                    bllObj.LastWorkingDate = proposeDateCheck.Date;
                                    bllObj.SubmissionDate = submittionDate;

                                    //ERP
                                    string sentToERP = bllObj.EmployeeResignationDataToERP(bllObj);

                                    if (sentToERP == "T")
                                    {
                                        bllObj.IsSentToERP = true;
                                        bllObj.EmployeeResignationERPUpdate(bllObj);

                                        drawMsgBox("Saved Successfully!", 1);
                                    }
                                    else
                                    {
                                        drawMsgBox("Sav Failed!", 2);
                                    }
                                    ViewState["dtUnApprovedTermination"] = null;
                                    ViewState["dtApprovedTermination"] = null;
                                    ddlEmp.SelectedValue = "0";
                                    bindgridResignation();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                drawMsgBox("Please select the resignation(s) to approve!", 3);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void bindgridResignation()
    {
        gvResignationUnApproved.DataSource = null;
        gvResignationApproved.DataSource = null;
        gvTerminationUnApproved.DataSource = null;
        gvTerminationApproved.DataSource = null;
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();
            DataTable _dtTermination = new DataTable();

            bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            bllObj.HODApprove = false;

            if (ViewState["dtUnApprovedResignation"] == null)
                _dt = bllObj.ResignationSelectHOD(bllObj);
            else
                _dt = (DataTable)ViewState["dtUnApprovedResignation"];

            if (_dt.Rows.Count > 0)
            {
                gvResignationUnApproved.DataSource = _dt;
                ViewState["dtUnApprovedResignation"] = _dt;

                div_ResignationsUnApproved.Visible = true;
            }
            else
            {
                div_ResignationsUnApproved.Visible = false;
            }
            gvResignationUnApproved.DataBind();

            //Show Read Only Grid 
            bllObj.HODApprove = true;
            if (ViewState["dtApprovedResignation"] == null)
                _dt = bllObj.ResignationSelectHOD(bllObj);
            else
                _dt = (DataTable)ViewState["dtApprovedResignation"];

            if (_dt.Rows.Count > 0)
            {
                gvResignationApproved.DataSource = _dt;
                ViewState["dtApprovedResignation"] = _dt;

                div_resignationApproved.Visible = true;
            }
            else
            {
                div_resignationApproved.Visible = false;
            }
            gvResignationApproved.DataBind();


            //Termination
            bllObj.HODSupervisorApprove = false;
            if (ViewState["dtUnApprovedTermination"] == null)
                _dtTermination = bllObj.ResignationSelectHODSupervisor(bllObj);
            else
                _dtTermination = (DataTable)ViewState["dtUnApprovedTermination"];

            if (_dtTermination.Rows.Count > 0)
            {
                gvTerminationUnApproved.DataSource = _dtTermination;
                ViewState["dtUnApprovedTermination"] = _dtTermination;

                div_TerminationsUnApproved.Visible = true;
            }
            else
            {
                div_TerminationsUnApproved.Visible = false;
            }
            gvTerminationUnApproved.DataBind();

            //Approved termination
            bllObj.HODSupervisorApprove = true;
            if (ViewState["dtApprovedTermination"] == null)
                _dtTermination = bllObj.ResignationSelectHODSupervisor(bllObj);
            else
                _dtTermination = (DataTable)ViewState["dtApprovedTermination"];

            if (_dtTermination.Rows.Count > 0)
            {
                gvTerminationApproved.DataSource = _dtTermination;
                ViewState["dtApprovedTermination"] = _dtTermination;

                div_terminationsApproved.Visible = true;
            }
            else
            {
                div_terminationsApproved.Visible = false;
            }
            gvTerminationApproved.DataBind();

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

    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dtUnApprovedLeaves"] = null;
        ViewState["dtApprovedLeaves"] = null;

        ddlEmp.SelectedValue = "0";
    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtResignationApproved = (DataTable)ViewState["dtApprovedResignation"];
        DataTable dtResignationUnApproved = (DataTable)ViewState["dtUnApprovedResignation"];
        DataTable dtTerminationUnApproved = (DataTable)ViewState["dtUnApprovedTermination"];
        DataTable dtTerminationApproved = (DataTable)ViewState["dtApprovedTermination"];

        DataView dvResignationApproved = new DataView();
        DataView dvResignationUnApproved = new DataView();
        DataView dvTerminationUnApproved = new DataView();
        DataView dvTerminationApproved = new DataView();

        string strFilter = "";

        if (ddlEmp.SelectedValue == "0")
        {
            bindgridResignation();
        }
        else
        {
            strFilter = "EmployeeCode like '%" + ddlEmp.SelectedValue.Trim() + "%'";


            #region // filter reservation record
            //filter reservation un-approved record
            if (ViewState["dtUnApprovedResignation"] != null)
            {
                dvResignationUnApproved = dtResignationUnApproved.DefaultView;
                dvResignationUnApproved.RowFilter = strFilter;
                gvResignationUnApproved.DataSource = dvResignationUnApproved;
                gvResignationUnApproved.DataBind();
            }

            //filter reservation approved record
            if (ViewState["dtApprovedResignation"] != null)
            {
                dvResignationApproved = dtResignationApproved.DefaultView;
                dvResignationApproved.RowFilter = strFilter;
                gvResignationApproved.DataSource = dvResignationApproved;
                gvResignationApproved.DataBind();
            }

            //filter reservation un-approved record
            if (ViewState["dtUnApprovedTermination"] != null)
            {
                dvTerminationUnApproved = dtTerminationUnApproved.DefaultView;
                dvTerminationUnApproved.RowFilter = strFilter;
                gvTerminationUnApproved.DataSource = dvTerminationUnApproved;
                gvTerminationUnApproved.DataBind();
            }

            //filter reservation approved termination record
            if (ViewState["dtApprovedTermination"] != null)
            {
                dvTerminationApproved = dtTerminationApproved.DefaultView;
                dvTerminationApproved.RowFilter = strFilter;
                gvTerminationApproved.DataSource = dvTerminationApproved;
                gvTerminationApproved.DataBind();
            }

            #endregion
        }

    }
    #endregion

    protected void ddlApproveResignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvRow in gvResignationUnApproved.Rows)
        {
            var ddlApproveResignation = (DropDownList)gvRow.FindControl("ddlApproveResignation");
            var hodProposeLastDate = (TextBox)gvRow.FindControl("hodProposeLastDate");

            if (ddlApproveResignation != null && hodProposeLastDate != null)
            {
                // Check for both "True" and "true"
                if (ddlApproveResignation != null && hodProposeLastDate != null)
                {
                    // Check for both "True" and "true"
                    if (ddlApproveResignation.SelectedValue == "1")
                    {
                        hodProposeLastDate.BackColor = System.Drawing.Color.Chartreuse;
                        hodProposeLastDate.Enabled = true; // Set Enabled to true when Visible is true
                    }
                    else
                    {
                        hodProposeLastDate.BackColor = System.Drawing.Color.LightGray;
                        hodProposeLastDate.Enabled = false; // Set Enabled to false when Visible is false
                    }
                }
            }
        }
    }

    protected void ddlApproveTermination_SelectedIndexChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow gvRow in gvTerminationUnApproved.Rows)
        {
            var ddlApproveTermination = (DropDownList)gvRow.FindControl("ddlApproveTermination");
            var hodSupervisorProposeLastDate = (TextBox)gvRow.FindControl("hodSupervisorApprovedDate");

            if (ddlApproveTermination != null && hodSupervisorProposeLastDate != null)
            {
                // Check for both "True" and "true"
                if (ddlApproveTermination != null && hodSupervisorProposeLastDate != null)
                {
                    // Check for both "True" and "true"
                    if (ddlApproveTermination.SelectedValue == "True" || ddlApproveTermination.SelectedValue == "true")
                    {
                        hodSupervisorProposeLastDate.Visible = true;
                        hodSupervisorProposeLastDate.Enabled = true; // Set Enabled to true when Visible is true
                    }
                    else
                    {
                        hodSupervisorProposeLastDate.Enabled = false; // Set Enabled to false when Visible is false
                    }
                }
            }
        }

        //if (ddlApproveResignation.SelectedValue == true)
        //{
        //    hodProposeLastDate.
        //}
    }

    protected void LastWorkingDate_TextChanged(object sender, EventArgs e)
    {
        DataValidations();
    }

    protected void TerminationLastWorkingDate_TextChanged(object sender, EventArgs e)
    {
        TerminationDataValidations();
    }

    private void DataValidations()
    {
        TextBox txtHODProposeLastDate, noticePeriod;

        foreach (GridViewRow gvRow in gvResignationUnApproved.Rows)
        {
            txtHODProposeLastDate = (TextBox)gvRow.FindControl("hodProposeLastDate");
            noticePeriod = (TextBox)gvRow.FindControl("NoticePeriod");

            int days = CalculateDays(gvRow.Cells[4].Text.ToString(), txtHODProposeLastDate.Text);
            if (days < 0)
            {
                drawMsgBox("Invalid Date Range! 'From date' can not be greater than 'To date'.", 2);
                noticePeriod.Text = "0";
            }
            else
            {
                noticePeriod.Text = days.ToString();
            }
        }

    }

    private void TerminationDataValidations()
    {
        TextBox txtHODSupervisorApprovedDate, noticePeriod;

        foreach (GridViewRow gvRow in gvTerminationUnApproved.Rows)
        {
            txtHODSupervisorApprovedDate = (TextBox)gvRow.FindControl("hodSupervisorApprovedDate");
            noticePeriod = (TextBox)gvRow.FindControl("NoticePeriod");

            int days = CalculateDays(gvRow.Cells[4].Text.ToString(), txtHODSupervisorApprovedDate.Text);
            if (days < 0)
            {
                drawMsgBox("Invalid Date Range! 'From date' can not be greater than 'To date'.", 2);
                noticePeriod.Text = "0";
            }
            else
            {
                noticePeriod.Text = days.ToString();
            }
        }

    }

    private int CalculateDays(string _fromDate, string _toDate)
    {
        int _ret = 0;
        TextBox txtHODProposeLastDate;

        foreach (GridViewRow gvRow in gvResignationUnApproved.Rows)
        {
            txtHODProposeLastDate = (TextBox)gvRow.FindControl("hodProposeLastDate");

            if (gvRow.Cells[4].Text.Length > 0 && txtHODProposeLastDate.Text.Length > 2)
            {

                DateTime dF = DateTime.ParseExact(_fromDate, "MM/dd/yyyy", null);

                DateTime dT = DateTime.ParseExact(_toDate, "MM/dd/yyyy", null);

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
        }
        return _ret;

    }

    private string GetHOD()
    {

        string HOD = "";
        BLLEmployeeReportTo objBll = new BLLEmployeeReportTo();
        DataTable _dt = new DataTable();
        objBll.Status_id = 1;
        objBll.EmployeeCode = Session["EmployeeCode"].ToString();
        _dt = objBll.EmployeeReportToHODSelectByEmployeeCode(objBll);

        if (_dt.Rows.Count > 0)
        {
            HOD = _dt.Rows[0]["HODEmployeeCode"].ToString();
            hodEmail = _dt.Rows[0]["HODEmail"].ToString();
            hodName = _dt.Rows[0]["FullName"].ToString();
        }
        else
        {
            HOD = "";
        }

        return HOD;
    }

    protected void gvTerminationUnApproved_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex != -1)
        {
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            DataTable objDt = new DataTable();

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

    protected void gvTerminationUnApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["TerminationRecordChecked"].ToString();

                foreach (GridViewRow gvr in gvTerminationUnApproved.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("chkResignations");

                    if (mood == "" || mood == "check")
                    {

                        cb.Checked = true;
                        ViewState["TerminationRecordChecked"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["TerminationRecordChecked"] = "check";
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