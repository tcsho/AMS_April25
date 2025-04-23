using ADG.JQueryExtenders.Impromptu;
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

            BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue));
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

        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue));
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

        BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue));
    }
    private void BindGridView(int regionId, int centerId, string employeecode, int LeaveTypeid)
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

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // If there is data, bind it
                        gvLeaveAdjustment.DataSource = dt;
                        gvLeaveAdjustment.DataBind();
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

            if (ddlLeaveType != null)
            {
                string leaveType = rowView["HR_LeaveType_Id"].ToString();
                ListItem selected = ddlLeaveType.Items.FindByValue(leaveType);
                if (selected != null) ddlLeaveType.SelectedValue = selected.Value;
            }

            if (txtLeaveFrom != null && rowView["HR_LeaveFrom"] != DBNull.Value)
            {
                txtLeaveFrom.Text = Convert.ToDateTime(rowView["HR_LeaveFrom"]).ToString("MM/dd/yyyy");
            }

            if (txtLeaveTo != null && rowView["HR_LeaveTo"] != DBNull.Value)
            {
                txtLeaveTo.Text = Convert.ToDateTime(rowView["HR_LeaveTo"]).ToString("MM/dd/yyyy");
            }

            if (txtRemarks != null && rowView["HRComment"] != DBNull.Value)
            {
                txtRemarks.Text = rowView["HRComment"].ToString();
            }
        }
    }
    protected void gvLeaveAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "submitbyhr")
        {
            // Get the Row Index where the button was clicked
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            // Access the data from the selected row
            string employeeCode = gvLeaveAdjustment.Rows[rowIndex].Cells[2].Text;

            GridViewRow selectedRow = gvLeaveAdjustment.Rows[rowIndex];
            TextBox txtRemarks = (TextBox)selectedRow.FindControl("txtRemarks");
            string hodRemarks = txtRemarks != null ? txtRemarks.Text : string.Empty;

            if (hodRemarks == string.Empty)
            {
                drawMsgBox("Please enter valid 'HR Remarks'.", 1);
                return;
            }

            // Get HR Leave Type (from DropDownList)
            DropDownList ddlLeaveType = (DropDownList)selectedRow.FindControl("ddlLeaveType");
            int hrLeaveType = ddlLeaveType != null ? Convert.ToInt32(ddlLeaveType.SelectedValue) : -1;

            if (ddlLeaveType == null || hrLeaveType == -1)
            {
                // Show error message or return early 
                drawMsgBox("Please select a valid 'Leave Type'.", 1);
                return; // or set a flag for validation failure
            }

            // Get Leave From Date
            TextBox txtLeaveFrom = (TextBox)selectedRow.FindControl("HR_LeaveFrom");
            DateTime leaveFrom;
            DateTime? leaveFromDate = null;

            if (txtLeaveFrom != null && DateTime.TryParseExact(
        txtLeaveFrom.Text.Trim(),
        "MM/dd/yyyy",  // or "dd/MM/yyyy" depending on your format
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
            TextBox txtLeaveTo = (TextBox)selectedRow.FindControl("HR_LeaveTo");
            DateTime leaveTo;
            DateTime? leaveToDate = null;

            if (txtLeaveTo != null && DateTime.TryParseExact(
        txtLeaveTo.Text.Trim(),
        "MM/dd/yyyy",  // or "dd/MM/yyyy" depending on your format
        System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None,
        out leaveTo))
            {
                leaveToDate = leaveTo;
            }
            else
            {
                drawMsgBox("A valid 'From Date' must be provided.", 1);
            }

            // Access the HiddenField in the selected row
            HiddenField hfFinalID = (HiddenField)gvLeaveAdjustment.Rows[rowIndex].FindControl("hfFinalID");
            if (hfFinalID != null)
            {
                int finalID = Convert.ToInt32(hfFinalID.Value);

                // Call UpdateByHr with the new parameters
                UpdateByHr(finalID, hodRemarks, hrLeaveType, leaveFromDate, leaveToDate);
            }
        }
    }
    private void UpdateByHr(int hfFinalID, string Remarks, int hrLeaveType, DateTime? leaveFromDate, DateTime? leaveToDate)
    {
        // Update query with all parameters
        string updateQuery = "UPDATE LeavesUploadERP " +
                             "SET HR_LeaveType_Id = @HR_LeaveType_Id, " +
                             "HR_LeaveFrom = @HR_LeaveFrom, " +
                             "HR_LeaveTo = @HR_LeaveTo, " +
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
                    BindGridView(regionID, centerID, txtUser.Text.Trim(), Convert.ToInt32(rblLeaveType.SelectedValue));

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