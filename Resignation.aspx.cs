using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class Resignation : System.Web.UI.Page
{
    string hodEmail = string.Empty;
    string hodName = string.Empty;
    BLLEmployeeLeaves bllEmpLeaves = new BLLEmployeeLeaves();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    try
                    {
                        ResetControls();
                        resignationSubmDate_TextChanged(sender, e);
                        bindgridResignation();
                        RegDepartmentEmployee();
                    }
                    catch (Exception ex)
                    {
                        Session["error"] = ex.Message;
                        Response.Redirect("ErrorPage.aspx", false);
                    }
                }
            }
            //ResetControls();
            //resignationSubmDate_TextChanged(sender, e);

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    } 

    protected void resignationSubmDate_TextChanged(object sender, EventArgs e)
    { 
        DateTime dF = DateTime.ParseExact(submissionDate.Text, "dd/MM/yyyy", null);

        var test = Convert.ToDateTime(dF.AddDays(29).Date).ToString();

        policyText.Text = "As per policy last working day will be ";
        policyText.Visible = true;
        policyDate.Visible = true;
        policyDate.Text = dF.AddDays(29).ToString("dd/MM/yyyy");
        lastDayDate.Text = dF.AddDays(29).ToString("dd/MM/yyyy");
        DataValidations();
    }

    protected void lasDayDate_TextChanged(object sender, EventArgs e)
    {
        DataValidations();
    }

    private void DataValidations()
    {    
        DateTime submissionDateV = DateTime.ParseExact(submissionDate.Text, "dd/MM/yyyy", null);
        if (submissionDateV < DateTime.Now.AddDays(-7))
        {
            drawMsgBox("Invalid Date Range! 'From date' can not be greater than 7 days.", 2);

            ResetControls();
        }

        int days = CalculateDays(submissionDate.Text, lastDayDate.Text);
        if (days < 0)
        {
            drawMsgBox("Invalid Date Range! 'Last working date' can not be less than 'Resignation date'.", 2);
            noticeDays.Text = "0";
        }
        else
        {
            noticeDays.Text = days.ToString();
        }
    }

    private int CalculateDays(string _fromDate, string _toDate)
    {
        int _ret = 0;
        if (submissionDate.Text.Length > 0 && lastDayDate.Text.Length > 2)
        {

            DateTime dF = DateTime.ParseExact(_fromDate, "dd/MM/yyyy", null);

            DateTime dT = DateTime.ParseExact(_toDate, "dd/MM/yyyy", null);

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
    protected void drawMsgBox(string msg, int errType)
    {


        ImpromptuHelper.ShowPrompt(msg);

    }

    private void ResetControls()
    {

        submissionDate.Text = "";
        lastDayDate.Text = "";
        noticeDays.Text = "";
        ddlreason.SelectedValue = "-1";
        employeeComments.Text = "";
        lastDateRemarks.Text = "";
        policyText.Text = "";
        policyDate.Text = "";
        policyText.Visible = false;
        policyDate.Visible = false;

        DateTime d = DateTime.Now;
        submissionDate.Text = d.ToString("dd/MM/yyyy"); // d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls();
    }

    protected void btnSubmitResignation_Click(object sender, EventArgs e)
    {
        try
        {
            BLLEmployeeResignationTermination obj = new BLLEmployeeResignationTermination();
            BLLSendEmail bllemail = new BLLSendEmail();

            int nAlreadyIn = 0;
            DataTable dt = new DataTable();
            bool isok = true;
            string _displymsg = "";
            var userTypeId = Convert.ToInt32(Session["UserType"].ToString());
            if (submissionDate.Text.Trim() == "")
            {
                isok = false;
                _displymsg = "Submission date is empty. !";
            }
            else if (lastDayDate.Text.Trim() == "")
            {
                isok = false;
                _displymsg = "Last working date is empty. !";
            }
            else if (ddlreason.SelectedValue == "-1")
            {
                isok = false;
                _displymsg = "Select a reason !";
            }
            else if (employeeComments.Text.Trim() == "")
            {
                isok = false;
                _displymsg = "Comment is empty. !";
            }
            else if (userTypeId == 22)
            {
                if (regDepartment.SelectedValue == "0")
                {
                    isok = false;
                    _displymsg = "Select a Department !";
                }
                else if (regEmployee.SelectedValue == "0")
                {
                    isok = false;
                    _displymsg = "Select an Employee!";
                }
            }
            else if (noticeDays.Text == "0")
            {
                isok = false;
                _displymsg = "Invalid Date Range! 'Last working date' can not be less than 'Resignation date'.";
            }

            if (isok)
            {
                DateTime submissiondatecheck = DateTime.ParseExact(submissionDate.Text, "dd/MM/yyyy", null);
                DateTime lastdatecheck = DateTime.ParseExact(lastDayDate.Text, "dd/MM/yyyy", null);

                obj.EmployeeCode = Session["EmployeeCode"].ToString();

                if (obj.EmployeeCode == "")
                    obj.EmployeeCode = regEmployee.SelectedValue.Split('-')[0].Trim();

                obj.CreatedBy = Session["UserName"].ToString();

                dt = obj.SingleEmployeeDetails(obj.EmployeeCode);
                obj.Category = "Resignation";
                obj.SubmissionDate = submissiondatecheck;
                obj.LastWorkingDate = lastdatecheck;
                obj.NoticePeriod = Convert.ToInt32(noticeDays.Text);
                obj.Reason = ddlreason.SelectedItem.Text.ToString();
                obj.Comments = employeeComments.Text;
                obj.CreatedOn = DateTime.Now;
                obj.ReportTo = GetHOD(obj.EmployeeCode);
                obj.StatusId = 1;
                obj.LastWorkingDateRemarks = lastDateRemarks.Text;

                nAlreadyIn = 0;
                nAlreadyIn = obj.ResignationTerminationAdd(obj);

                if (nAlreadyIn == 0)
                {
                    //Email

                    //string mailMsg = "<html><body><p>Dear " + hodName + ",</p><p>One of your team member has been resigned, you can approve / unapprove the resign by login in to the AMS.</p><p>Kindly approve / unapprove the resign within 7 days of the resignation date else system will automatically approve the resignation. </p><br><p>Employee No: " + Session["EmployeeCode"].ToString() + "</p><p>Employee Name: " + dt.Rows[0]["FullName"].ToString() + "</p><p>Designation: " + dt.Rows[0]["Designation"].ToString() + "</p><p>Department: " + dt.Rows[0]["Department"].ToString() + "</p><p>Region: " + dt.Rows[0]["Region"].ToString() + "</p><p>Center:	" + dt.Rows[0]["Center"].ToString() + "</p><p>Resignation date: " + submissiondatecheck.ToShortDateString() + "	</p><p>Last working date: " + lastdatecheck.ToShortDateString() + " </p><p>Last working date remarks: " + lastDateRemarks.Text + " </p><p>Notice period: " + noticeDays.Text + " </p><p>Reason of resignation: " + ddlreason.SelectedItem.Text.ToString() + " </p><p>Comments by employee: " + employeeComments.Text + " </p><br><p>Regards,</p></body></html>";

                    // Retrieve employee information with explicit null checks

                    string employeeFullName = dt.Rows[0]["FullName"] != null ? dt.Rows[0]["FullName"].ToString() : "N/A";
                    string employeeDesignation = dt.Rows[0]["Designation"] != null ? dt.Rows[0]["Designation"].ToString() : "N/A";
                    string employeeDepartment = dt.Rows[0]["Department"] != null ? dt.Rows[0]["Department"].ToString() : "N/A";
                    string employeeRegion = dt.Rows[0]["Region"] != null ? dt.Rows[0]["Region"].ToString() : "N/A";
                    string employeeCenter = dt.Rows[0]["Center"] != null ? dt.Rows[0]["Center"].ToString() : "N/A";

                    // Check if the session variable is not null before converting to string
                    string employeeCode = Session["EmployeeCode"] != null ? Session["EmployeeCode"].ToString() : "N/A";

                    // Assuming submissiondatecheck and lastdatecheck are DateTime objects
                    string resignationDate = submissiondatecheck.ToShortDateString();
                    string lastWorkingDate = lastdatecheck.ToShortDateString();

                    // Assume that lastDateRemarks and noticeDays are controls that might have text properties
                    string lastWorkingDateRemarks = lastDateRemarks != null ? lastDateRemarks.Text : "N/A";
                    string noticePeriod = noticeDays != null ? noticeDays.Text : "N/A";

                    // Check if the selected item is not null before accessing its text
                    string resignationReason = ddlreason.SelectedItem != null ? ddlreason.SelectedItem.Text : "N/A";
                    string employeeCommentsText = employeeComments != null ? employeeComments.Text : "N/A";
                    string gender = dt.Rows[0]["Gender"] != DBNull.Value ? dt.Rows[0]["Gender"].ToString().Trim() : "N/A";

                    string _gender;
                    if (gender.Equals("F", StringComparison.OrdinalIgnoreCase))
                    {
                        _gender = "her";
                    }
                    else if (gender.Equals("M", StringComparison.OrdinalIgnoreCase))
                    {
                        _gender = "his";
                    }
                    else
                    {
                        _gender = "his/her";
                    }


                    string mailMsg = "<html>" +
                 "<body>" +
                 "<p>Dear " + hodName + ",</p>" +
                 "<p>This is to inform you that your team member, " + employeeFullName + ", " + employeeDesignation + ", has submitted " + _gender + " resignation today.</p>" +
                 "<p>You can approve or reject the resignation by logging into your AMS portal.</p>" +
                 "<p>Please ensure to take action within 7 days of the resignation date; otherwise, the system will automatically process the resignation.</p>" +
                 "<p>Employee No: " + employeeCode + "</p>" +
                 "<p>Employee Name: " + employeeFullName + "</p>" +
                 "<p>Designation: " + employeeDesignation + "</p>" +
                 "<p>Department: " + employeeDepartment + "</p>" +
                 "<p>Region: " + employeeRegion + "</p>" +
                 "<p>Center: " + employeeCenter + "</p>" +
                 "<p>Resignation date: " + resignationDate + "</p>" +
                 "<p>Last working date: " + lastWorkingDate + "</p>" +
                 "<p>Last working date remarks: " + lastWorkingDateRemarks + "</p>" +
                 "<p>Notice period: " + noticePeriod + "</p>" +
                 "<p>Reason for resignation: " + resignationReason + "</p>" +
                 "<p>Comments by employee: " + employeeCommentsText + "</p>" +
                 "<p>Thank you for your prompt attention to this matter.</p>" +
                 "<p>Regards,</p>" +
                 "</body>" +
                 "</html>";

                    string subject = "Resignation Notification for " + employeeFullName;
                    bllemail.SendEmailNew(hodEmail, subject, mailMsg, "");
                    obj.IsEmailSent = true;
                    int alreadyIn = obj.EmployeeResignationEmailUpdate(obj);

                    drawMsgBox("Data added successfully.", 1);
                    ResetControls();
                    resignationSubmDate_TextChanged(sender, e);
                }
                else if (nAlreadyIn == 1)
                {
                    drawMsgBox("Resignation already Submitted.", 3);
                }
            }
            else
            {
                drawMsgBox(_displymsg, 3);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    private string GetHOD(string empCode)
    {

        string HOD = "";
        BLLEmployeeReportTo objBll = new BLLEmployeeReportTo();
        DataTable _dt = new DataTable();
        objBll.Status_id = 1;
        objBll.EmployeeCode = empCode; //Session["EmployeeCode"].ToString();
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

        return HOD.Trim();
    }

    protected void bindgridResignation()
    {
        try
        {
            BLLEmployeeResignationTermination obj = new BLLEmployeeResignationTermination();
            DataTable _dt = new DataTable(); 

            obj.CreatedBy = Session["UserName"].ToString(); 

            if (ViewState["gvResignation"] == null)
                _dt = bllEmpLeaves.EmployeeResignationStatus(obj);
            else
                _dt = (DataTable)ViewState["gvResignation"];

            if (_dt.Rows.Count > 0)
            {
                gvResignation.DataSource = _dt;
                ViewState["gvResignation"] = _dt;

                //div_leaveRequests.Visible = true;
            }
            gvResignation.DataBind();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void RegDepartmentEmployee()
    {
        var userTypeId = Convert.ToInt32(Session["UserType"].ToString());
        if (userTypeId == 22 || userTypeId == 19)
        {
            txtRegDepartment.Visible = true;
            regDepartment.Visible = true;
            txtRegEmployee.Visible = true;
            regEmployee.Visible = true;
            submissionDate.Text = "";
            lastDayDate.Text = "";
            noticeDays.Text = "";
            ddlreason.SelectedValue = "-1";
            employeeComments.Text = "";
            lastDateRemarks.Text = "";
            policyDate.Text = "";
            loadDepartments();
        }
        else
        {
            txtRegDepartment.Visible = false;
            regDepartment.Visible = false;
            txtRegEmployee.Visible = false;
            regEmployee.Visible = false;
        }

    }
    protected void loadDepartments()
    {

        BLLAttendance obj = new BLLAttendance();
        DALBase objBase = new DALBase();
        DataTable dt = new DataTable();

        obj.PMonthDesc = Session["CurrentMonth"].ToString();
        obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
        obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
        dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);
        objBase.FillDropDown(dt, regDepartment, "Deptcode", "DeptName");


    }
    protected void regDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLLAttendance bllObj = new BLLAttendance();
        DataTable _dt = new DataTable();
        DALBase objBase = new DALBase();
        bllObj.PMonthDesc = Session["CurrentMonth"].ToString();
        bllObj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
        bllObj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString().Trim());
        bllObj.DeptCode = Convert.ToInt32(regDepartment.SelectedValue);
        bllObj.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
        _dt = bllObj.AttendanceFetchSummaryNew(bllObj);
        _dt.Columns.Add("EmployeeCodeAndName", typeof(string));
        foreach (DataRow row in _dt.Rows)
        {
            row["EmployeeCodeAndName"] = row["EmployeeCode"].ToString() + "-" + row["FullName"];
        }
        objBase.FillDropDown(_dt, regEmployee, "EmployeeCode", "EmployeeCodeAndName");

    }
    protected void regEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        ResetControls();
        resignationSubmDate_TextChanged(sender, e);
    }
}