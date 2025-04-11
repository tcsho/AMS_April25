using ADG.JQueryExtenders.Impromptu;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LeaveEncashment : System.Web.UI.Page
{
    string hodEmail = string.Empty;
    string hodName = string.Empty;
    string empName = string.Empty;
    string empEmail = string.Empty;
    // Connection string to your database (update with your actual connection string) 
    private string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RegionID"] == null)
        {
            Response.Redirect("~/login.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "20")
            {
                BindGridView4Hod(Convert.ToInt32(Session["EmployeeCode"]));
            }
            else if (Session["UserType"].ToString() == "19" || Session["UserType"].ToString() == "22")
            {
                BindGridView(Convert.ToInt32(Session["RegionID"]));
            }
        }
    }
    private void BindGridView4Hod(int empcode)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetLeaveEncashmentDetails4Hod", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hodEmpCode", empcode);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvLeaveEncashmentHoD.DataSource = dt;
                    gvLeaveEncashmentHoD.DataBind();
                }
            }
        }
    }
    protected void gvLeaveEncashmentHoD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //// Check if the row is a data row
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Find the Button control in the current row
            Button btnSubmit = (Button)e.Row.FindControl("btnSubmitToHR");

            // Get the value of Submit2BOD from the current row
            string EncashmentSubmitToHR = DataBinder.Eval(e.Row.DataItem, "EncashmentSubmitToHR").ToString();

            // If Submit2BOD is 1, disable the button
            if (EncashmentSubmitToHR == "Yes")
            {
                btnSubmit.Enabled = false;
            }
        }
    }
    protected void gvLeaveEncashmentHoD_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        BLLSendEmail bllemail = new BLLSendEmail();
        if (e.CommandName == "submithr")
        {
            // Get the Row Index where the button was clicked
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            // Access the data from the selected row
            string employeeCode = gvLeaveEncashmentHoD.Rows[rowIndex].Cells[2].Text;
            string employeeName = gvLeaveEncashmentHoD.Rows[rowIndex].Cells[3].Text;
            string employeeEmail = gvLeaveEncashmentHoD.Rows[rowIndex].Cells[4].Text;
            string designation = gvLeaveEncashmentHoD.Rows[rowIndex].Cells[5].Text;
            string department = gvLeaveEncashmentHoD.Rows[rowIndex].Cells[6].Text;

            GridViewRow selectedRow = gvLeaveEncashmentHoD.Rows[rowIndex];
            TextBox txtRemarks = (TextBox)selectedRow.FindControl("txtRemarks");
            string hodRemarks = txtRemarks != null ? txtRemarks.Text : string.Empty;

            // Access the HiddenField in the selected row
            HiddenField hfEmpLeaveId = (HiddenField)gvLeaveEncashmentHoD.Rows[rowIndex].FindControl("hfEmpLeaveId");

            GetHOD(employeeCode);

            if (hodRemarks == "" || hodRemarks == string.Empty)
            {
                drawMsgBox("Kindly provide your remarks before submitting the leave encashment request to HR!", 2);
                return;
            }

            string empLeaveId = hfEmpLeaveId.Value;

            string mailMsg = "Dear " + employeeName + ",<br><br>This is to inform you that your Head of Department has submitted your leave encashment request to HR for further approval processing.<br><br>";
            mailMsg += "Regards:<br><br>";
            mailMsg += department + " (HOD)";

            bllemail.SendEmailNew(employeeEmail, "approval/disapproval for leave encashment", mailMsg, "");

            string mailMsghod = "Dear " + hodName + ",<br><br>This is to inform you that your Head of Department has submitted " + employeeName + "'s leave encashment request to HR for further approval processing.<br><br>";
            mailMsghod += "Regards:<br><br>";
            mailMsghod += department + " (HOD)";

            bllemail.SendEmailNew(hodEmail, "approval/disapproval for leave encashment", mailMsghod, "");

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

            if (Session["RegionID"].ToString() == "0")
            {
                mailTo = "Ams_alert_ho_leave_encashment_process@csn.edu.pk";
            }

            string mailMsg1 = "Dear HR " + Session["RegionName"] + ",<br><br>This is to inform you that your " + Session["CenterName"] + " Head of Department has submitted leave encashment request to HR for Employee " + employeeName + " for further approval processing.<br><br>";
            mailMsg1 += "Regards:<br><br>";
            mailMsg1 += Session["CenterName"] + " (HOD)";

            bllemail.SendEmailNew(mailTo, "approval/disapproval for leave encashment", mailMsg1, "");

            UpdateSubmitToHR(Convert.ToInt32(empLeaveId), hodRemarks);
        }
    }
    private void UpdateSubmitToHR(int hfEmpLeaveId, string HodRemarks)
    {
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
                    BindGridView4Hod(Convert.ToInt32(Session["EmployeeCode"]));
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


    private void BindGridView(int regionId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetLeaveEncashmentDetails", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionId", regionId);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvLeaveEncashment.DataSource = dt;
                    gvLeaveEncashment.DataBind();
                }
            }
        }
    }
    protected void gvLeaveEncashment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check if the row is a data row
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Find the Button control in the current row
            Button btnSubmit = (Button)e.Row.FindControl("btnSubmitToBoD");

            // Get the value of Submit2BOD from the current row
            string submit2BOD = DataBinder.Eval(e.Row.DataItem, "Submit2BOD").ToString();

            // If Submit2BOD is 1, disable the button
            if (submit2BOD == "Yes")
            {
                btnSubmit.Enabled = false;
            }
        }
    }    
    protected void gvLeaveEncashment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        BLLSendEmail bllemail = new BLLSendEmail();
        if (e.CommandName == "submitbod")
        {
            // Get the Row Index where the button was clicked
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            // Access the data from the selected row
            string employeeCode = gvLeaveEncashment.Rows[rowIndex].Cells[2].Text;
            string employeeName = gvLeaveEncashment.Rows[rowIndex].Cells[3].Text;
            string designation = gvLeaveEncashment.Rows[rowIndex].Cells[4].Text;
            string department = gvLeaveEncashment.Rows[rowIndex].Cells[5].Text;
            string region = gvLeaveEncashment.Rows[rowIndex].Cells[6].Text;
            string hodRemarks = gvLeaveEncashment.Rows[rowIndex].Cells[12].Text;

            // Access the HiddenField in the selected row
            HiddenField hfEmpLeaveId = (HiddenField)gvLeaveEncashment.Rows[rowIndex].FindControl("hfEmpLeaveId");

            // Get the EmpLeaveId value
            string empLeaveId = hfEmpLeaveId.Value;

            byte[] emplvId = System.Text.ASCIIEncoding.ASCII.GetBytes(empLeaveId);
            string empLeId = Convert.ToBase64String(emplvId);

            string mailTo;
            
            //if (Session["RegionID"].ToString() == "40000000")
            //{
            //    mailTo = "aurangzeb.firoz@csn.edu.pk";
            //}
            //else if (Session["RegionID"].ToString() == "20000000")
            //{
            //    mailTo = "jfiroz@tcsidxb.ae";
            //} 
            //else
            //{
            //    mailTo = "csnho@csn.edu.pk";
            //}


            //mailTo = "rumman@csn.edu.pk";
            mailTo = "usman.riaz@csn.edu.pk";
            var baseurl = "http://attlog.thecityschool.edu.pk/";
            //var baseurl = "http://localhost:49915/";
            var urlconfirmation = baseurl + "encashconfirm.aspx?el_Id=" + empLeId;
            var urlreject = baseurl + "encashconfirm.aspx?ell_Id=" + empLeId;
            string mailMsg = "Dear BoD Member," + "<br><br>This is to inform you that " + employeeName + ", " + designation + ", who works at " + department + " at " + region + " applied for annual leaves which have been rejected/not approved by the departmental head on account of work exigencies, citing the following justifications:<br><br> " + '"' + hodRemarks + '"' + "<br><br>Please review and provide your feedback or approval/disapproval for leave encashment.<br><br>";
            mailMsg += "<span style='margin-left: 110px;'><a href='" + urlconfirmation + "' style='display: inline-block; color: #fff; text-decoration: none; padding: 10px 30px; background-color: #0C4DA2; border-radius: 10px; border: none; text-align: center; font-size: 16px; font-weight: bold; cursor: pointer;'> Confirm</a> <a href='" + urlreject + "' style='display: inline-block; color: #fff; text-decoration: none; padding: 10px 30px; background-color: red; border-radius: 10px; border: none; text-align: center; font-size: 16px; font-weight: bold; cursor: pointer;'> Reject</a><br><br>";                         
            mailMsg += "Regards:<br><br>";
            mailMsg += region + " (HR)";
             
            bllemail.SendEmailNew(mailTo, "approval/disapproval for leave encashment", mailMsg, "");

            UpdateSubmitToBOD(Convert.ToInt32(empLeaveId));
        }
        else if (e.CommandName == "resubmit")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            HiddenField hfEmpLeaveId = (HiddenField)gvLeaveEncashment.Rows[rowIndex].FindControl("hfEmpLeaveId");
            HiddenField hfhodEmail = (HiddenField)gvLeaveEncashment.Rows[rowIndex].FindControl("hfhodEmail");
            HiddenField hfEmpRegion_Id = (HiddenField)gvLeaveEncashment.Rows[rowIndex].FindControl("hfEmpRegion_Id");
            HiddenField hfEmpCenter_Id = (HiddenField)gvLeaveEncashment.Rows[rowIndex].FindControl("hfEmpCenter_Id");
            string hodName = gvLeaveEncashment.Rows[rowIndex].Cells[9].Text;
            string region = gvLeaveEncashment.Rows[rowIndex].Cells[6].Text;
            string employeeName = gvLeaveEncashment.Rows[rowIndex].Cells[3].Text;

            // Get the EmpLeaveId value
            string empLeaveId = hfEmpLeaveId.Value;
            string hodemail = hfhodEmail.Value;
            string regionid = hfEmpRegion_Id.Value;
            string centerid = hfEmpCenter_Id.Value;

            if (regionid != "" && centerid != "")
            {
                UpdateReSubmitToHOD(Convert.ToInt32(empLeaveId), "","");
            }
            else
            {
                UpdateReSubmitToHOD(Convert.ToInt32(empLeaveId), "");
            }

            string mailMsghod = "Dear " + hodName + ",<br><br>We noticed that a proper justification was not provided in the remarks for " + employeeName + "'s leave encashment request. Kindly review and resubmit the request.<br><br>";
            mailMsghod += "Regards:<br><br>";
            mailMsghod += region + " (HR)";

            bllemail.SendEmailNew(hodemail, "approval/disapproval for leave encashment", mailMsghod, ""); 
        }
        else
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            HiddenField hfEmpLeaveId = (HiddenField)gvLeaveEncashment.Rows[rowIndex].FindControl("hfEmpLeaveId");
            HiddenField hfhodEmail = (HiddenField)gvLeaveEncashment.Rows[rowIndex].FindControl("hfhodEmail");
            string hodName = gvLeaveEncashment.Rows[rowIndex].Cells[9].Text;
            string region = gvLeaveEncashment.Rows[rowIndex].Cells[6].Text;
            string employeeName = gvLeaveEncashment.Rows[rowIndex].Cells[3].Text;
            string employeeCode = gvLeaveEncashment.Rows[rowIndex].Cells[2].Text;

            // Get the EmpLeaveId value
            string empLeaveId = hfEmpLeaveId.Value;
            string hodemail = hfhodEmail.Value;

            UpdateEncashmentRejectByHR(Convert.ToInt32(empLeaveId));

            SelectEmployeeInfo(Convert.ToInt32(employeeCode));

            string mailMsg = "Dear " + employeeName + ",<br><br>Upon review your leave encashment request has been rejected by HR.<br><br>";
            mailMsg += "Regards:<br><br>";
            mailMsg += region + " (HR)";

            bllemail.SendEmailNew(empEmail, "approval/disapproval for leave encashment", mailMsg, "");
             
            string mailMsghod = "Dear " + hodName + ",<br><br>Upon review, " + employeeName + "'s leave encashment request has been rejected by HR.<br><br>";
            mailMsghod += "Regards:<br><br>";
            mailMsghod += region + " (HR)";

            bllemail.SendEmailNew(hodemail, "approval/disapproval for leave encashment", mailMsghod, "");
        }
    }
    private void UpdateReSubmitToHOD(int hfEmpLeaveId, string HodRemarks,string remarks)
    {
        string updateQuery = "UPDATE EmployeeLeaves " +
                     "SET EncashmentSubmitToHR = @EncashmentSubmitToHR, " +
                     "EncashmentHodRemarks = @EncashmentHodRemarks, " +
                     "HODRemakrs = @HODRemakrs " +
                     "WHERE EmpLeave_Id = @EmpLeaveId";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a command to execute the query
            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@EncashmentSubmitToHR", 0);
            command.Parameters.AddWithValue("@EncashmentHodRemarks", HodRemarks);
            command.Parameters.AddWithValue("@HODRemakrs", remarks);
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
                    BindGridView(Convert.ToInt32(Session["RegionID"]));
                    drawMsgBox("The leave encashment request has been successfully sent back to the HoD for resubmission!", 1);
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
    private void UpdateReSubmitToHOD(int hfEmpLeaveId, string HodRemarks)
    {
        string updateQuery = "UPDATE EmployeeLeaves " +
                     "SET EncashmentSubmitToHR = @EncashmentSubmitToHR, " +
                     "EncashmentHodRemarks = @EncashmentHodRemarks " +
                     "WHERE EmpLeave_Id = @EmpLeaveId";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a command to execute the query
            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@EncashmentSubmitToHR", 0);
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
                    BindGridView(Convert.ToInt32(Session["RegionID"]));
                    drawMsgBox("The leave encashment request has been successfully sent back to the HoD for resubmission!", 1);
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
    private void UpdateEncashmentRejectByHR(int hfEmpLeaveId)
    {
        // SQL UPDATE query
        string updateQuery = "UPDATE EmployeeLeaves SET EncashmentRejectByHR = 1 WHERE EmpLeave_Id = " + hfEmpLeaveId;

        // Create a connection to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a command to execute the query
            SqlCommand command = new SqlCommand(updateQuery, connection);

            // Add parameters to avoid SQL injection
            command.Parameters.AddWithValue("@EncashmentRejectByHR", 1);
            command.Parameters.AddWithValue("@EmpLeave_Id", hfEmpLeaveId); // Assuming you're updating the record with Id = 1

            try
            {
                // Open the connection
                connection.Open();

                // Execute the query
                int rowsAffected = command.ExecuteNonQuery();

                // Check if any rows were affected
                if (rowsAffected > 0)
                {
                    BindGridView(Convert.ToInt32(Session["RegionID"]));
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
    private void UpdateSubmitToBOD(int hfEmpLeaveId)
    {
        // SQL UPDATE query
        string updateQuery = "UPDATE EmployeeLeaves SET EncashmentSubmitToBod = 1 WHERE EmpLeave_Id = " + hfEmpLeaveId;

        // Create a connection to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a command to execute the query
            SqlCommand command = new SqlCommand(updateQuery, connection);

            // Add parameters to avoid SQL injection
            command.Parameters.AddWithValue("@EncashmentSubmitToBod", 1);
            command.Parameters.AddWithValue("@EmpLeave_Id", hfEmpLeaveId); // Assuming you're updating the record with Id = 1

            try
            {
                // Open the connection
                connection.Open();

                // Execute the query
                int rowsAffected = command.ExecuteNonQuery();

                // Check if any rows were affected
                if (rowsAffected > 0)
                {
                    BindGridView(Convert.ToInt32(Session["RegionID"]));
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
}
