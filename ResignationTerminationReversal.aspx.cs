using ADG.JQueryExtenders.Impromptu;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ResignationTerminationReversal : System.Web.UI.Page
{
    string hodEmail = string.Empty;
    string hodName = string.Empty;
    BLLSendEmail bllemail = new BLLSendEmail();
    BLLEmployeeResignationTermination bllObj = new BLLEmployeeResignationTermination();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtUser.Text.Trim() == "")
        {
            return;
        }

        DataTable _dt = new DataTable();
        bllObj.EmployeeCode = txtUser.Text.Trim();
        bllObj.Region_Id = Convert.ToInt32(Session["RegionID"]);
        _dt = bllObj.ResignationTerminationReversalSelectEmployee(bllObj);

        if (_dt.Rows.Count == 0)
        {
            btnReverseEmployeeResignationTermination.Visible = false;
            btnUpdateLastWorkingDate.Visible = false;
        }
        else
        {
            btnReverseEmployeeResignationTermination.Visible = true;
            btnUpdateLastWorkingDate.Visible = true;
        }

        gvResignationTermination.DataSource = _dt;
        gvResignationTermination.DataBind();
    }
    protected void btnUpdateLastWorkingDate_Click(object sender, EventArgs e)
    {
        const int SubmissionDateColumnIndex = 4;
        const int ReasonColumnIndex = 5;

        try
        {
            foreach (GridViewRow row in gvResignationTermination.Rows)
            {
                // Extract values from the GridViewRow
                DateTime lastWorkingDate = Convert.ToDateTime(((TextBox)row.FindControl("txtLastWorkingDate")).Text);
                DateTime submissionDate = Convert.ToDateTime(row.Cells[SubmissionDateColumnIndex].Text);
                string empName = string.Empty;
                string empEmail = string.Empty;
                string reason = row.Cells[ReasonColumnIndex].Text;

                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                string hrRemarks = txtRemarks != null ? txtRemarks.Text : string.Empty;

                if (hrRemarks == "" || hrRemarks == string.Empty)
                {
                    drawMsgBox("Kindly provide your remarks before submitting the Last Working Date change request!", 2);
                    return;
                }

                // Update the bllObj with extracted values
                bllObj.EmployeeCode = txtUser.Text.Trim();
                bllObj.LastWorkingDate = lastWorkingDate;
                bllObj.SubmissionDate = submissionDate;
                bllObj.ModifiedBy = Session["UserName"].ToString();
                bllObj.Reason = reason;
                bllObj.HRRemarks = hrRemarks;
                GetHOD(bllObj.EmployeeCode);

                // Call ERP update methods
                string sentToERP = bllObj.ReverseEmployeeResignationTerminationInERP(bllObj);

                if (sentToERP == "T")
                {
                    string updateStatus = bllObj.UpdateEmployeeLastWorkingDateToERP(bllObj);

                    if (updateStatus == "T")
                    {
                        bllObj.UpdateEmployeeLastWorkingDate(bllObj);

                        string mailTo;
                        string ccEmails = string.Empty;
                        string regionId = Session["RegionID"].ToString();

                        //HO Emails
                        if (regionId == "0")
                        {
                            ccEmails = "Ams_alert_ho_resignation_process@csn.edu.pk;";
                        }

                        //CR Emails
                        if (regionId == "40000000")
                        {
                            ccEmails = "Ams_alert_cr_resignation_process@csn.edu.pk;";
                        }

                        //SR Emails
                        if (regionId == "20000000")
                        {
                            ccEmails = "Ams_alert_sr_resignation_process@csn.edu.pk;";
                        }

                        //NR Emails
                        if (regionId == "30000000")
                        {
                            ccEmails = "Ams_alert_nr_resignation_process@csn.edu.pk";
                        }

                        ccEmails += " " + hodEmail;
                        mailTo = empEmail;
                        string mailMsg = "Dear " + empName + ",<br><br>This is to inform you that your last working date has been changed.<br><br>";
                        mailMsg += "Regards:<br><br>";
                        mailMsg += "HR";

                        bllemail.SendEmailNew(mailTo, "Last Working Date Change", mailMsg, ccEmails);

                        drawMsgBox("Last working date has been changed successfully!", 1);
                    }
                    else
                    {
                        drawMsgBox(updateStatus.ToString(), 2);
                    }
                }
                else
                {
                    drawMsgBox(sentToERP.ToString(), 2);
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception if necessary and rethrow
            // LogException(ex); // Optional: Add your logging logic here
            throw ex;
        }

        // Refresh the search results
        btnSearch_Click(this, EventArgs.Empty);
    }
    protected void btnReverseEmployeeResignationTermination_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvResignationTermination.Rows)
            {
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                string hrRemarks = txtRemarks != null ? txtRemarks.Text : string.Empty;

                if (hrRemarks == "" || hrRemarks == string.Empty)
                {
                    drawMsgBox("Kindly provide your remarks before submitting the reversal request!", 2);
                    return;
                }

                // Set required properties
                bllObj.EmployeeCode = txtUser.Text.Trim();
                bllObj.ModifiedBy = Session["UserName"].ToString();
                bllObj.HRRemarks = hrRemarks;
                GetHOD(bllObj.EmployeeCode);

                string sentToERP = bllObj.ReverseEmployeeResignationTerminationInERP(bllObj);
                // Attempt to reverse resignation/termination in ERP
                if (sentToERP == "T")
                {
                    bllObj.ResignationTerminationReversalUpdate(bllObj);

                    string empName = string.Empty;
                    string empEmail = string.Empty;
                    foreach (GridViewRow gvRow in gvResignationTermination.Rows)
                    {
                        empName = gvRow.Cells[0].Text;
                        empEmail = gvRow.Cells[1].Text;
                    }
                    string mailTo;
                    string ccEmails = string.Empty;
                    string regionId = Session["RegionID"].ToString();

                    //HO Emails
                    if (regionId == "0")
                    {
                        ccEmails = "Ams_alert_ho_resignation_process@csn.edu.pk;";
                    }

                    //CR Emails
                    if (regionId == "40000000")
                    {
                        ccEmails = "Ams_alert_cr_resignation_process@csn.edu.pk;";
                    }

                    //SR Emails
                    if (regionId == "20000000")
                    {
                        ccEmails = "Ams_alert_sr_resignation_process@csn.edu.pk;";
                    }

                    //NR Emails
                    if (regionId == "30000000")
                    {
                        ccEmails = "Ams_alert_nr_resignation_process@csn.edu.pk";
                    }

                    ccEmails += " " + hodEmail;
                    mailTo = empEmail;
                    string mailMsg = "Dear " + empName + ",<br><br>This is to inform you that your resignation has been reversed.<br><br>";
                    mailMsg += "Regards:<br><br>";
                    mailMsg += "HR";

                    bllemail.SendEmailNew(mailTo, "Resignation/Termination Reversal", mailMsg, ccEmails);

                    drawMsgBox("Resignation/Termination has been reversed successfully!", 1);
                }
                else
                {
                    drawMsgBox(sentToERP.ToString(), 2);
                }
            }
        }
        catch (Exception ex)
        {
            // Log and rethrow exception for better traceability
            // LogException(ex); // Optional: implement your logging logic
            throw ex;
        }

        // Refresh search results
        btnSearch_Click(this, EventArgs.Empty);
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
}