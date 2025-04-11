using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;
using System.Globalization;

public partial class Termination : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }

            if (Session["EmployeeCode"] != null)
            {
                employeeDetailsDiv.Visible = false;
                if (!IsPostBack)
                {
                    try
                    {
                        ResetControls();
                        //resignationSubmDate_TextChanged(sender, e);
                        loadEmployees();

                    }
                    catch (Exception ex)
                    {
                        Session["error"] = ex.Message;
                        Response.Redirect("ErrorPage.aspx", false);
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



    protected void ddlLeavesStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void resignationSubmDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlEmp.SelectedValue == "0")
        {
            employeeDetailsDiv.Visible = false;
        }
        else
        {
            employeeDetailsDiv.Visible = true;
        }

        DateTime terminationDateV = DateTime.ParseExact(submissionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        if (terminationDateV < DateTime.Now.AddDays(-7))
        {
            drawMsgBox("Invalid Date Range! 'From date' can not be greater than 7 days.", 2);

            ResetControls();
        }

        DataValidations();

    }

    protected void lasDayDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlEmp.SelectedValue == "0")
        {
            employeeDetailsDiv.Visible = false;
        }
        else
        {
            employeeDetailsDiv.Visible = true;
        }
        DataValidations();

    }

    private void DataValidations()
    {
        int days = CalculateDays(submissionDate.Text, lastDayDate.Text);
        if (days < 0)
        {
            drawMsgBox("Invalid Date Range! 'From date' can not be greater than 'To date'.", 2);
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

    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLLEmployeeResignationTermination obj = new BLLEmployeeResignationTermination();

        DataTable dt = new DataTable();

        if (ddlEmp.SelectedValue == "0")
        {
            employeeDetailsDiv.Visible = false;
        }
        else
        {
            dt = obj.SingleEmployeeDetails(ddlEmp.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                //dt.Rows
                employeeNameText.Text = dt.Rows[0]["FullName"].ToString();
                designationText.Text = dt.Rows[0]["Designation"].ToString();
                departmentText.Text = dt.Rows[0]["Department"].ToString();
                regionText.Text = dt.Rows[0]["Region"].ToString();
                centerText.Text = dt.Rows[0]["Center"].ToString();
                employeeEmail.Text = dt.Rows[0]["Email"].ToString();

                employeeDetailsDiv.Visible = true;
            }

        }
        resignationSubmDate_TextChanged(sender, e);
    }

    protected void loadEmployees()
    {

        //BLLEmplyeeResignationTermination obj = new BLLEmplyeeResignationTermination();
        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

        DataTable dt = new DataTable();

        obj.ReportTo = Session["EmployeeCode"].ToString().Trim();

        int UserLevel;

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        obj.UserType_id = Convert.ToInt32(Session["UserType"].ToString());

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
        obj.PMonthDesc = Session["CurrentMonth"].ToString();

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


    protected void ddlreason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmp.SelectedValue == "0")
        {
            employeeDetailsDiv.Visible = false;
        }
        else
        {
            employeeDetailsDiv.Visible = true;
        }
    }

    protected void btnSubmitTermination_Click(object sender, EventArgs e)
    {
        try
        {
            BLLEmployeeResignationTermination obj = new BLLEmployeeResignationTermination();
            BLLSendEmail bllemail = new BLLSendEmail();

            int nAlreadyIn = 0;
            string mailMsg;

            bool isok = true;
            string _displymsg = "";

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
            else if (hodComments.Text.Trim() == "")
            {
                isok = false;
                _displymsg = "Comment is empty. !";
            }

            if (isok)
            {
                DateTime submissiondatecheck = DateTime.ParseExact(submissionDate.Text, "dd/MM/yyyy", null);
                DateTime lastdatecheck = DateTime.ParseExact(lastDayDate.Text, "dd/MM/yyyy", null);

                obj.EmployeeCode = ddlEmp.SelectedValue;
                obj.Category = "Termination";
                obj.SubmissionDate = submissiondatecheck.Date;
                obj.LastWorkingDate = lastdatecheck.Date;
                obj.ApprovedDate = DateTime.Now;
                obj.HODApprove = true;
                obj.NoticePeriod = Convert.ToInt32(noticeDays.Text);
                obj.Reason = ddlreason.SelectedItem.Text.ToString();
                obj.Comments = hodComments.Text;
                obj.CreatedOn = DateTime.Now;
                obj.CreatedBy = Session["EmployeeCode"].ToString();
                obj.StatusId = 1;
                obj.ReportTo = Session["EmployeeCode"].ToString();

                nAlreadyIn = 0;
                nAlreadyIn = obj.ResignationTerminationAdd(obj);

                if (nAlreadyIn == 0)
                {

                    var centerId = Convert.ToInt32(Session["CenterID"]);
                    string regionIdString = Session["RegionID"].ToString() ?? "0";

                    //var regionId = Convert.ToInt32(regionIdString);

                    //Email
                    if (ddlreason.SelectedValue == "2")
                    {
                        //mailMsg = "<html><body><div><html><body><p>Date:  " + DateTime.Now.ToShortDateString() + "</p><br><p><b>" + employeeNameText.Text + "</b></p><p>" + designationText.Text + "</p><p><b>The City School</b></p><p>" + regionText.Text + "</p><p>" + centerText.Text + " </p><br><p style='text-align: center'><u><b>DISCONTINUATION OF SERVICES - MISCONDUCT</b></u></p><br><p>" + employeeNameText.Text + ",</p><p>We regret to inform you that the company no longer requires your services as per the clause 9.1.2 (a to i) of the Service Rules of The City School. Please consider this letter as a notice for the above-mentioned subject and your last working day shall be <b>" + lastdatecheck.ToShortDateString() + ".</b></p><p>Your final settlement will be released as per the Service Rules.</p><br><p>Sincerely,</p><p>" + Session["First_Name"] + " " + Session["Middle_Name"] + "</p><p>" + Session["Designation"] + "</p></body></html><div/></body></html>";
                        mailMsg = "<html><body><div><html><body><p>Dear " + employeeNameText.Text + ",</p><p>We regret to inform you that, in accordance with clause 9.1.2 (a - i) of the Service Rules of The City School, your services will no longer be required. Please consider this email as formal notice of the termination of your employment. Your last working day will be <b>" + lastdatecheck.ToShortDateString() + ".</b></p><p>Your final settlement will be processed in line with the company’s Service Rules.</p><p>We appreciate your contributions and wish you the best in your future endeavours.</p><p>Sincerely,</p><p>" + Session["First_Name"] + " " + Session["Middle_Name"] + "</p><p>" + Session["Designation"] + "</p></body></html><div/></body></html>";
                    }
                    else
                    {
                        //mailMsg = "<html><body><div><html><body><p>Date:  " + DateTime.Now.ToShortDateString() + "</p><br><p><b>" + employeeNameText.Text + "</b></p><p>" + designationText.Text + "</p><p><b>The City School</b></p><p>" + regionText.Text + "</p><p>" + centerText.Text + " </p><br><p style='text-align: center'><u><b>Discontinuation of Services</b></u></p><br><p>" + employeeNameText.Text + ",</p><p>We regret to inform you that the company no longer requires your services as per the clause 8.1.1 of the Service Rules of The City School. Please consider this letter as a notice for the above-mentioned subject and your last working day will be <b>" + lastdatecheck.ToShortDateString() + ".</b></p><p>Your final settlement will be released as per the Service Rules.</p><br><p>Sincerely,</p><p>" + Session["First_Name"] + " " + Session["Middle_Name"] + "</p><p>" + Session["Designation"] + "</p></body></html><div/></body></html>";
                        mailMsg = "<html><body><div><html><body><p>Dear " + employeeNameText.Text + ",</p><p>We regret to inform you that, in accordance with clause 8.1.1 of the Service Rules of The City School, your services will no longer be required. Please consider this email as formal notice of the termination of your employment. Your last working day will be <b>" + lastdatecheck.ToShortDateString() + ".</b></p><p>Your final settlement will be processed in line with the company’s Service Rules.</p><p>We appreciate your contributions and wish you the best in your future endeavours.</p><p>Sincerely,</p><p>" + Session["First_Name"] + " " + Session["Middle_Name"] + "</p><p>" + Session["Designation"] + "</p></body></html><div/></body></html>";
                    }
                    //var email = "mafaz.ahamed@csn.edu.pk";

                    bllemail.SendEmailNew(employeeEmail.Text, "Employment Termination Notice", mailMsg, "");
                    obj.IsEmailSent = true;
                    int alreadyIn = obj.EmployeeResignationEmailUpdate(obj);

                    if (centerId > 0)
                    {
                        //ERP
                        string sentToERP = obj.EmployeeResignationDataToERP(obj);

                        if (sentToERP == "T")
                        {
                            obj.IsSentToERP = true;
                            obj.EmployeeResignationERPUpdate(obj);
                        }
                    }


                    drawMsgBox("Data added successfully.", 1);
                    ResetControls();
                }
                else if (nAlreadyIn == 1)
                {
                    drawMsgBox("Data already exist.", 3);
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls();
    }

    private void ResetControls()
    {

        submissionDate.Text = "";
        lastDayDate.Text = "";
        noticeDays.Text = "";
        ddlreason.SelectedValue = "-1";
        hodComments.Text = "";
        //ddlEmp.SelectedValue = "0";


        DateTime d = DateTime.Now;
        submissionDate.Text = d.ToString("dd/MM/yyyy"); // d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();
        //lastDayDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();

    }
}