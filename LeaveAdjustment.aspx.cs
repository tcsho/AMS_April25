﻿using ADG.JQueryExtenders.Impromptu;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class LeaveAdjustment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/login.aspx");
        }

        if (!IsPostBack)
        {
            singleemployee.Visible = false;
            rblLeaveType.SelectedIndex = 0;
            rblEmpType.SelectedIndex = 0;
            rblStaffType.SelectedIndex = 0;
            rblattended.SelectedIndex = 0;
            int regionID = 0;  // Default value if the session is invalid
            int centerID = 0;  // Default value if the session is invalid

            // Check if RegionID is available and a valid integer
            if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
            {
                regionID = Convert.ToInt32(Session["RegionID"]);
            }

            // Check if CenterID is available and a valid integer
            if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
            {
                centerID = Convert.ToInt32(Session["CenterID"]);
            }

            BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));
        }
    }
    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get the selected value from the DropDownList
        DropDownList ddlLeaveType = (DropDownList)sender;
        string selectedValue = ddlLeaveType.SelectedValue;

        // You can now use the selectedValue for whatever logic you need
        if (selectedValue == "1") // "All Employees"
        {
            singleemployee.Visible = false;
        }
        else if (selectedValue == "0") // "Single Employee"
        {
            singleemployee.Visible = true;
        }
    }
    protected void rblLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regionID = 0;  // Default value if the session is invalid
        int centerID = 0;  // Default value if the session is invalid

        // Check if RegionID is available and a valid integer
        if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
        {
            regionID = Convert.ToInt32(Session["RegionID"]);
        }

        // Check if CenterID is available and a valid integer
        if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
        {
            centerID = Convert.ToInt32(Session["CenterID"]);
        }

        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));
    }
    protected void rblEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regionID = 0;  // Default value if the session is invalid
        int centerID = 0;  // Default value if the session is invalid

        // Check if RegionID is available and a valid integer
        if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
        {
            regionID = Convert.ToInt32(Session["RegionID"]);
        }

        // Check if CenterID is available and a valid integer
        if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
        {
            centerID = Convert.ToInt32(Session["CenterID"]);
        }

        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));
    }
    protected void rblStaffType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regionID = 0;  // Default value if the session is invalid
        int centerID = 0;  // Default value if the session is invalid

        // Check if RegionID is available and a valid integer
        if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
        {
            regionID = Convert.ToInt32(Session["RegionID"]);
        }

        // Check if CenterID is available and a valid integer
        if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
        {
            centerID = Convert.ToInt32(Session["CenterID"]);
        }

        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));
    }
    protected void rblattended_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regionID = 0;  // Default value if the session is invalid
        int centerID = 0;  // Default value if the session is invalid

        // Check if RegionID is available and a valid integer
        if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
        {
            regionID = Convert.ToInt32(Session["RegionID"]);
        }

        // Check if CenterID is available and a valid integer
        if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
        {
            centerID = Convert.ToInt32(Session["CenterID"]);
        }

        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));
    }
    protected void txtUser_TextChanged(object sender, EventArgs e)
    {
        int regionID = 0;  // Default value if the session is invalid
        int centerID = 0;  // Default value if the session is invalid

        // Check if RegionID is available and a valid integer
        if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
        {
            regionID = Convert.ToInt32(Session["RegionID"]);
        }

        // Check if CenterID is available and a valid integer
        if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
        {
            centerID = Convert.ToInt32(Session["CenterID"]);
        }

        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));
    }
    private void BindGridView(int regionId, int centerId, string employeecode, int LeaveTypeid, int empType, int staffType, int isAttended)
    {
        // Clear previous data
        gvLeaveAdjustment.DataSource = null;
        gvLeaveAdjustment.DataBind();

        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetEmployeeLeaves4Adjustment", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionId", regionId);
                cmd.Parameters.AddWithValue("@CenterId", centerId);
                if (string.IsNullOrEmpty(employeecode))
                {
                    cmd.Parameters.AddWithValue("@EmployeeCode", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EmployeeCode", employeecode);
                }
                cmd.Parameters.AddWithValue("@LeaveType_Id", LeaveTypeid);
                cmd.Parameters.AddWithValue("@empType", empType);
                cmd.Parameters.AddWithValue("@staffType", staffType);
                cmd.Parameters.AddWithValue("@isAttended", isAttended);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // If there is data, bind it
                        gvLeaveAdjustment.DataSource = dt;
                        gvLeaveAdjustment.DataBind();

                        lblRecordCount.Text = "Total Records: " + dt.Rows.Count.ToString();
                        lblRecordCount.Visible = true;
                    }
                    else
                    {
                        // No data, clear the previous data binding and set EmptyDataText
                        gvLeaveAdjustment.DataSource = null;
                        gvLeaveAdjustment.DataBind();
                    }
                }
            }
        }
    }
    protected void gvLeaveAdjustment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var rowView = (DataRowView)e.Row.DataItem;

            DropDownList ddlLeaveType = (DropDownList)e.Row.FindControl("ddlLeaveType");
            TextBox txtLeaveFrom = (TextBox)e.Row.FindControl("HR_LeaveFrom");
            TextBox txtLeaveTo = (TextBox)e.Row.FindControl("HR_LeaveTo");
            TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

            bool allConditionsMet = true;

            ddlLeaveType.BackColor = System.Drawing.Color.Chartreuse;
            txtLeaveFrom.BackColor = System.Drawing.Color.Chartreuse;
            txtLeaveTo.BackColor = System.Drawing.Color.Chartreuse;
            txtRemarks.BackColor = System.Drawing.Color.Chartreuse;

            if (ddlLeaveType != null)
            {
                string leaveType = rowView["HR_LeaveType_Id"].ToString();
                ListItem selected = ddlLeaveType.Items.FindByValue(leaveType);
                if (selected != null)
                {
                    ddlLeaveType.SelectedValue = selected.Value;
                }
                else
                {
                    allConditionsMet = false;
                }
            }

            if (txtLeaveFrom != null && rowView["HR_LeaveFrom"] != DBNull.Value)
            {
                txtLeaveFrom.Text = Convert.ToDateTime(rowView["HR_LeaveFrom"]).ToString("dd/MM/yyyy");
            }
            else
            {
                allConditionsMet = false;
            }

            if (txtLeaveTo != null && rowView["HR_LeaveTo"] != DBNull.Value)
            {
                txtLeaveTo.Text = Convert.ToDateTime(rowView["HR_LeaveTo"]).ToString("dd/MM/yyyy");
            }
            else
            {
                allConditionsMet = false;
            }

            if (txtRemarks != null && rowView["HRComment"] != DBNull.Value)
            {
                txtRemarks.Text = rowView["HRComment"].ToString();
            }
            else
            {
                allConditionsMet = false;
            }

            if (allConditionsMet)
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;  // You can change to any color
            }
        }
    }
    protected void btnSubmitSelected_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvLeaveAdjustment.Rows)
        {
            // Find the checkbox in the current row
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");

            // Check if the checkbox is selected
            if (chkSelect != null && chkSelect.Checked)
            {
                // Get the necessary controls and data from the selected row
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                DropDownList ddlLeaveType = (DropDownList)row.FindControl("ddlLeaveType");
                TextBox txtLeaveFrom = (TextBox)row.FindControl("HR_LeaveFrom");
                TextBox txtLeaveTo = (TextBox)row.FindControl("HR_LeaveTo");
                Label lblBalCasual = (Label)row.FindControl("lblBalCasual");
                Label lblBalAnnual = (Label)row.FindControl("lblBalAnnual");
                HiddenField hfFinalID = (HiddenField)row.FindControl("hfFinalID");

                // Perform necessary validation and data processing
                if (txtRemarks != null && ddlLeaveType != null && txtLeaveFrom != null && txtLeaveTo != null && hfFinalID != null)
                {
                    string hodRemarks = txtRemarks.Text;
                    if (string.IsNullOrEmpty(hodRemarks))
                    {
                        drawMsgBox("Please enter valid 'HR Remarks'.", 1);
                        return;
                    }

                    // Get HR Leave Type (from DropDownList)
                    int hrLeaveType = Convert.ToInt32(ddlLeaveType.SelectedValue);
                     
                    if (hrLeaveType == -1)
                    {
                        // Show error message or return early 
                        drawMsgBox("Please select a valid 'Leave Type'.", 1);
                        return; // or set a flag for validation failure
                    }

                    // Get Leave From Date
                    DateTime leaveFrom;
                    DateTime? leaveFromDate = null;

                    if (DateTime.TryParseExact(txtLeaveFrom.Text.Trim(), "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out leaveFrom))
                    {
                        leaveFromDate = leaveFrom;
                    }
                    else
                    {
                        drawMsgBox("A valid 'From Date' must be provided.", 1);
                        return;
                    }

                    // Get Leave To Date
                    DateTime leaveTo;
                    DateTime? leaveToDate = null;

                    if (DateTime.TryParseExact(txtLeaveTo.Text.Trim(), "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out leaveTo))
                    {
                        leaveToDate = leaveTo;
                    }
                    else
                    {
                        drawMsgBox("A valid 'To Date' must be provided.", 1);
                        return;
                    }

                    // Calculate the number of days
                    decimal numberOfDays = 0;
                    if (leaveFromDate.HasValue && leaveToDate.HasValue)
                    {
                        TimeSpan difference = leaveToDate.Value - leaveFromDate.Value;
                        numberOfDays = difference.Days + 1; // Adding 1 to include both start and end dates
                    }

                    // Check if labels are found, then extract values
                    decimal balCasual = lblBalCasual != null ? Convert.ToDecimal(lblBalCasual.Text) : 0;
                    decimal balAnnual = lblBalAnnual != null ? Convert.ToDecimal(lblBalAnnual.Text) : 0;

                    string currentYearMonth = DateTime.Now.ToString("yyyyMM"); 
                    int currentYear = DateTime.Now.Year;    // Returns the 4-digit year

                    // Check Leave Type and Apply Logic
                    if (hrLeaveType == 6072 || hrLeaveType == 9000) // Casual Leave (CL) OR Half Casual Leave (CL)
                    {
                        if (balCasual == 0)
                        {
                            drawMsgBox("Casual leave cannot be taken if the casual leave balance is zero.", 1);
                            return;
                        }

                        if (numberOfDays != 1)
                        {
                            drawMsgBox("One leave can be taken as Casual leave.", 1);
                            return;
                        }

                        if (hrLeaveType == 9000)
                        {
                            numberOfDays = 0.5m;
                        }

                        // Update Casual Leave Balance
                        UpdateLeaveBalanceCL(int.Parse(row.Cells[2].Text), currentYear, currentYearMonth, numberOfDays);
                    }
                    else if (hrLeaveType == 6071) // Annual Leave (AL)
                    {
                        if (balAnnual == 0)
                        {
                            drawMsgBox("Annual leave cannot be taken if the Annual leave balance is zero.", 1);
                            return;
                        }

                        if (numberOfDays < 3)
                        {
                            drawMsgBox("Annual leave must be taken consecutively and last for more than 3 days.", 1);
                            return;
                        }

                        // Update Annual Leave Balance
                        UpdateLeaveBalanceAL(int.Parse(row.Cells[2].Text), currentYear, currentYearMonth, numberOfDays);
                    }

                    // Access the HiddenField in the selected row
                    int finalID = Convert.ToInt32(hfFinalID.Value);

                    // Call UpdateByHr with the new parameters
                    UpdateByHr(finalID, hodRemarks, hrLeaveType, leaveFromDate, leaveToDate, numberOfDays);
                }
            }
        }
    }
    //protected void gvLeaveAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "submitbyhr")
    //    {
    //        // Get the Row Index where the button was clicked
    //        int rowIndex = Convert.ToInt32(e.CommandArgument);

    //        // Access the data from the selected row
    //        string employeeCode = gvLeaveAdjustment.Rows[rowIndex].Cells[2].Text;

    //        GridViewRow selectedRow = gvLeaveAdjustment.Rows[rowIndex];
    //        TextBox txtRemarks = (TextBox)selectedRow.FindControl("txtRemarks");
    //        string hodRemarks = txtRemarks != null ? txtRemarks.Text : string.Empty;

    //        if (hodRemarks == string.Empty)
    //        {
    //            drawMsgBox("Please enter valid 'HR Remarks'.", 1);
    //            return;
    //        }

    //        // Get HR Leave Type (from DropDownList)
    //        DropDownList ddlLeaveType = (DropDownList)selectedRow.FindControl("ddlLeaveType");
    //        int hrLeaveType = ddlLeaveType != null ? Convert.ToInt32(ddlLeaveType.SelectedValue) : -1;

    //        if (ddlLeaveType == null || hrLeaveType == -1)
    //        {
    //            // Show error message or return early 
    //            drawMsgBox("Please select a valid 'Leave Type'.", 1);
    //            return; // or set a flag for validation failure
    //        }

    //        // Get Leave From Date
    //        TextBox txtLeaveFrom = (TextBox)selectedRow.FindControl("HR_LeaveFrom");
    //        DateTime leaveFrom;
    //        DateTime? leaveFromDate = null;

    //        if (txtLeaveFrom != null && DateTime.TryParseExact(
    //    txtLeaveFrom.Text.Trim(),
    //    "MM/dd/yyyy",  // or "dd/MM/yyyy" depending on your format
    //    System.Globalization.CultureInfo.InvariantCulture,
    //    System.Globalization.DateTimeStyles.None,
    //    out leaveFrom))
    //        {
    //            leaveFromDate = leaveFrom;
    //        }
    //        else
    //        {
    //            drawMsgBox("A valid 'From Date' must be provided.", 1);
    //            return;
    //        }

    //        // Get Leave To Date
    //        TextBox txtLeaveTo = (TextBox)selectedRow.FindControl("HR_LeaveTo");
    //        DateTime leaveTo;
    //        DateTime? leaveToDate = null;

    //        if (txtLeaveTo != null && DateTime.TryParseExact(
    //    txtLeaveTo.Text.Trim(),
    //    "MM/dd/yyyy",  // or "dd/MM/yyyy" depending on your format
    //    System.Globalization.CultureInfo.InvariantCulture,
    //    System.Globalization.DateTimeStyles.None,
    //    out leaveTo))
    //        {
    //            leaveToDate = leaveTo;
    //        }
    //        else
    //        {
    //            drawMsgBox("A valid 'From Date' must be provided.", 1);
    //            return;
    //        }

    //        int numberOfDays = 0;
    //        if (leaveFromDate.HasValue && leaveToDate.HasValue)
    //        {
    //            TimeSpan difference = leaveToDate.Value - leaveFromDate.Value;
    //            numberOfDays = difference.Days + 1; // Adding 1 to include both start and end dates
    //        }

    //        Label lblBalCasual = (Label)selectedRow.FindControl("lblBalCasual");
    //        Label lblBalAnnual = (Label)selectedRow.FindControl("lblBalAnnual");

    //        // Check if labels are found, then extract values
    //        decimal balCasual = lblBalCasual != null ? Convert.ToDecimal(lblBalCasual.Text) : 0;
    //        decimal balAnnual = lblBalAnnual != null ? Convert.ToDecimal(lblBalAnnual.Text) : 0;

    //        int currentMonth = DateTime.Now.Month;  // Returns an integer from 1 to 12
    //        int currentYear = DateTime.Now.Year;    // Returns the 4-digit year

    //        if (hrLeaveType == 6072)//CL
    //        {
    //            if (balCasual == 0)
    //            {
    //                drawMsgBox("Casual leave cannot be taken if the casual leave balance is zero.", 1);
    //                return;
    //            }

    //            if (numberOfDays != 1)
    //            {
    //                drawMsgBox("One leave can be taken as Casual leave.", 1);
    //                return;
    //            }

    //            //UpdateLeaveBalanceCL(int.Parse(employeeCode), currentYear, currentMonth.ToString(), numberOfDays);
    //            UpdateLeaveBalanceCL(int.Parse(employeeCode), currentYear, "202504", numberOfDays);
    //        }
    //        else if (hrLeaveType == 6071)//AL
    //        {
    //            if (balAnnual == 0)
    //            {
    //                drawMsgBox("Annual leave cannot be taken if the Annual leave balance is zero.", 1);
    //                return;
    //            }

    //            if (numberOfDays < 3)
    //            {
    //                drawMsgBox("Annual leave must be taken consecutively and last for more than 3 days.", 1);
    //                return;
    //            }

    //            //UpdateLeaveBalanceAL(int.Parse(employeeCode), currentYear, currentMonth.ToString(), numberOfDays);
    //            UpdateLeaveBalanceAL(int.Parse(employeeCode), currentYear, "202504", numberOfDays);

    //        }

    //        // Access the HiddenField in the selected row
    //        HiddenField hfFinalID = (HiddenField)gvLeaveAdjustment.Rows[rowIndex].FindControl("hfFinalID");
    //        if (hfFinalID != null)
    //        {
    //            int finalID = Convert.ToInt32(hfFinalID.Value);

    //            // Call UpdateByHr with the new parameters
    //            UpdateByHr(finalID, hodRemarks, hrLeaveType, leaveFromDate, leaveToDate);
    //        }
    //    }
    //}
    public void UpdateLeaveBalanceAL(int employeeCode, int year, string pMonth, decimal tAnnualLeave)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        // Query to get the current TAnnulaLeave balance
        string selectQuery = @"
        SELECT TAnnulaLeave
        FROM EmpLeaveBalance4LeaveAdjustment
        WHERE year = @Year AND pmonth = @PMonth AND employeecode = @EmployeeCode";

        decimal currentAnnualLeave = 0;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@PMonth", pMonth);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                try
                {
                    conn.Open();
                    // Execute the query and get the current TAnnulaLeave
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        currentAnnualLeave = Convert.ToDecimal(result);
                    }
                    else
                    {
                        // Handle the case where the record doesn't exist or TAnnulaLeave is null
                        currentAnnualLeave = 0;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    // You can log the exception message here
                }
            }
        }

        // Calculate the new annual leave balance by subtracting the tAnnualLeave from the current balance
        decimal newAnnualLeaveBalance = currentAnnualLeave - tAnnualLeave;

        // Query to update the TAnnulaLeave balance
        string updateQuery = @"
        UPDATE EmpLeaveBalance4LeaveAdjustment
        SET TAnnulaLeave = @TAnnulaLeave
        WHERE year = @Year AND pmonth = @PMonth AND employeecode = @EmployeeCode";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@TAnnulaLeave", newAnnualLeaveBalance);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@PMonth", pMonth);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        int regionID = 0;
                        int centerID = 0;

                        // Check if RegionID is available and a valid integer
                        if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
                        {
                            regionID = Convert.ToInt32(Session["RegionID"]);
                        }

                        // Check if CenterID is available and a valid integer
                        if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
                        {
                            centerID = Convert.ToInt32(Session["CenterID"]);
                        }

                        // Bind the GridView again after the update
                        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));
                    }
                    else
                    {
                        // Handle case where no rows were affected
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    // You can log the exception message here
                }
            }
        }
    }

    public void UpdateLeaveBalanceCL(int employeeCode, int year, string pMonth, decimal tCasualLeave)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        // Query to get the current TCasualLeave balance
        string selectQuery = @"
        SELECT TCasualLeave
        FROM EmpLeaveBalance4LeaveAdjustment
        WHERE year = @Year AND pmonth = @PMonth AND employeecode = @EmployeeCode";

        decimal currentCasualLeave = 0;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@PMonth", pMonth);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                try
                {
                    conn.Open();
                    // Execute the query and get the current TCasualLeave
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        currentCasualLeave = Convert.ToDecimal(result);
                    }
                    else
                    {
                        // Handle the case where the record doesn't exist or TCasualLeave is null
                        currentCasualLeave = 0;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    // You can log the exception message here
                }
            }
        }

        // Calculate the new casual leave balance by subtracting the tCasualLeave from the current balance
        decimal newCasualLeaveBalance = currentCasualLeave - tCasualLeave;

        // Query to update the TCasualLeave balance
        string updateQuery = @"
        UPDATE EmpLeaveBalance4LeaveAdjustment
        SET TCasualLeave = @TCasualLeave
        WHERE year = @Year AND pmonth = @PMonth AND employeecode = @EmployeeCode";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@TCasualLeave", newCasualLeaveBalance);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@PMonth", pMonth);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        int regionID = 0;
                        int centerID = 0;

                        // Check if RegionID is available and a valid integer
                        if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
                        {
                            regionID = Convert.ToInt32(Session["RegionID"]);
                        }

                        // Check if CenterID is available and a valid integer
                        if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
                        {
                            centerID = Convert.ToInt32(Session["CenterID"]);
                        }

                        // Bind the GridView again after the update
                        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));
                    }
                    else
                    {
                        // Handle case where no rows were affected
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    // You can log the exception message here
                }
            }
        }
    }

    private void UpdateByHr(int hfFinalID, string Remarks, int hrLeaveType, DateTime? leaveFromDate, DateTime? leaveToDate, decimal LeaveDays)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/login.aspx");
        }

        // Update query with all parameters
        string updateQuery = "UPDATE LeavesUploadERP " +
                             "SET HR_LeaveType_Id = @HR_LeaveType_Id, " +
                             "HR_LeaveFrom = @HR_LeaveFrom, " +
                             "HR_LeaveTo = @HR_LeaveTo, " +
                             "HR_LeaveDays = @HR_LeaveDays, " +
                             "HRComment = @HRComment, " +
                             "ModifiedBy = @ModifiedBy " +
                             "WHERE FinalID = @FinalID";  // Ensure FinalID is part of the WHERE clause

        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create the command to execute the query
            SqlCommand command = new SqlCommand(updateQuery, connection);

            // Add parameters with proper handling for nulls
            command.Parameters.AddWithValue("@HR_LeaveType_Id", hrLeaveType);
            command.Parameters.AddWithValue("@HR_LeaveFrom", (object)leaveFromDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@HR_LeaveTo", (object)leaveToDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@HR_LeaveDays", (object)LeaveDays ?? DBNull.Value);
            command.Parameters.AddWithValue("@HRComment", (object)Remarks ?? DBNull.Value); // Ensure Remarks is never null
            command.Parameters.AddWithValue("@ModifiedBy", Session["UserName"]);  // Assuming EmployeeCode is stored in Session
            command.Parameters.AddWithValue("@FinalID", hfFinalID);  // Correctly adding FinalID

            try
            {
                // Open the connection
                connection.Open();

                // Execute the query
                int rowsAffected = command.ExecuteNonQuery();

                // Check if any rows were affected
                if (rowsAffected > 0)
                {
                    // Default regionID and centerID values
                    int regionID = 0;
                    int centerID = 0;

                    // Check if RegionID is available and a valid integer
                    if (Session["RegionID"] != null && int.TryParse(Session["RegionID"].ToString(), out regionID))
                    {
                        regionID = Convert.ToInt32(Session["RegionID"]);
                    }

                    // Check if CenterID is available and a valid integer
                    if (Session["CenterID"] != null && int.TryParse(Session["CenterID"].ToString(), out centerID))
                    {
                        centerID = Convert.ToInt32(Session["CenterID"]);
                    }

                    // Bind the GridView again after the update
                    BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue), Convert.ToInt32(rblEmpType.SelectedValue), Convert.ToInt32(rblStaffType.SelectedValue), Convert.ToInt32(rblattended.SelectedValue));

                    // Show success message
                    drawMsgBox("The leave has been successfully updated!", 1); // Message updated to reflect success
                }
                else
                {
                    // If no record was updated, show a message
                    drawMsgBox("No record was updated.", 2);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here (log or show a message)
                //drawMsgBox($"Error: {ex.Message}", 2);
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
}