using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class ClearenceForm : System.Web.UI.Page
{
    string LeaveDays = string.Empty;
    string SubmissionDate = string.Empty;
    string LastWorkingDate = string.Empty;

    BLLEmployeeLeaves bllEmpLeaves = new BLLEmployeeLeaves();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/login.aspx");
        }

        if (!IsPostBack)
        {
            LoadVerifyBy();
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

            if (Session["HoDCode"] != null)
            {
                BindGridView4HoD(Convert.ToInt32(Session["HoDCode"]));
            }
            else
            {
                BindGridView(regionID, centerID);
            }            
             
            divREI.Visible = false;
            divAccounts.Visible = false;
            divIt.Visible = false;
            divHr.Visible = false;
            divLib.Visible = false;
            divAdmin.Visible = false;
            divLaboratory.Visible = false;
            divTraining.Visible = false;
            divHod.Visible = false;
        }
    }
    private void LoadVerifyBy()
    {
        spName.InnerText = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
        spEmpCode.InnerText = Session["EmployeeCode"].ToString();
        spDesg.InnerText = Session["Designation"].ToString();

        Span1.InnerText = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
        Span2.InnerText = Session["EmployeeCode"].ToString();
        Span3.InnerText = Session["Designation"].ToString();

        Span4.InnerText = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
        Span5.InnerText = Session["EmployeeCode"].ToString();
        Span6.InnerText = Session["Designation"].ToString();

        Span7.InnerText = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
        Span8.InnerText = Session["EmployeeCode"].ToString();
        Span9.InnerText = Session["Designation"].ToString();

        Span10.InnerText = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
        Span11.InnerText = Session["EmployeeCode"].ToString();
        Span12.InnerText = Session["Designation"].ToString();

        Span13.InnerText = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
        Span14.InnerText = Session["EmployeeCode"].ToString();
        Span15.InnerText = Session["Designation"].ToString();

        Span16.InnerText = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
        Span17.InnerText = Session["EmployeeCode"].ToString();
        Span18.InnerText = Session["Designation"].ToString();

        Span19.InnerText = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();
        Span20.InnerText = Session["EmployeeCode"].ToString();
        Span21.InnerText = Session["Designation"].ToString();
    }
    protected void gvResignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Clearance")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            // Get the row
            GridViewRow row = gvResignation.Rows[index];

            // Set values
            string employeeCode = row.Cells[0].Text;
            string employeeName = row.Cells[1].Text;
            string employeeDesignation = row.Cells[2].Text;

            string noticeperiod = row.Cells[6].Text;
            string subdate = row.Cells[3].Text;
            string lastdate = row.Cells[4].Text;

            lblEmployeeCode.Text = employeeCode;
            lblEmployeeName.Text = employeeName;
            lblEmployeeDesignation.Text = employeeDesignation;
            
            //divAccounts.Visible = true;
            //divIt.Visible = true;
            //divHr.Visible = true;
            //divLib.Visible = true;
            //divAdmin.Visible = true;
            //divLaboratory.Visible = true;
            //divTraining.Visible = true;
            //divHod.Visible = true;

            // Initially hide all divs
            divAccounts.Visible = false;
            divIt.Visible = false;
            divHr.Visible = false;
            divLib.Visible = false;
            divAdmin.Visible = false;
            divLaboratory.Visible = false;
            divTraining.Visible = false;
            divHod.Visible = false;

            if (Session["HoDCode"] != null)
            {
                divHod.Visible = true;
            }
            else
            { 
                List<string> dept = getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString().Trim());

                foreach (var department in dept)
                {
                    switch (department.ToLower()) // Use ToLower() to handle case insensitivity
                    {
                        case "accounts":
                            divAccounts.Visible = true;
                            break;
                        case "it":
                            divIt.Visible = true;
                            break;
                        case "hr":
                            divHr.Visible = true;
                            break;
                        case "lib":
                            divLib.Visible = true;
                            break;
                        case "admin":
                            divAdmin.Visible = true;
                            break;
                        case "laboratory":
                            divLaboratory.Visible = true;
                            break;
                        default:
                            divTraining.Visible = true;
                            break;
                    }
                }
            }

            BindFeeConcessionData(employeeCode.Trim());
            BindTrainingData(employeeCode.Trim());

            DataTable dt = GetEmployeeClearance(employeeCode.Trim());

            if (dt.Rows.Count > 0) // Check if there are any rows
            {
                btnSaveHOD.Visible = false;
                btnSaveTraining.Visible = false;
                btnSaveLaboratory.Visible = false;
                btnSaveAdmin.Visible = false;
                btnSaveLibrary.Visible = false;
                btnSaveHr.Visible = false;
                btnSaveIt.Visible = false;
                btnSaveAccount.Visible = false;

                btnUpdateHOD.Visible = true;
                btnUpdateTraining.Visible = true;
                btnUpdateLaboratory.Visible = true;
                btnUpdateAdmin.Visible = true;
                btnUpdateLibrary.Visible = true;
                btnUpdateHr.Visible = true;
                btnUpdateIt.Visible = true;                
                btnUpdateAccount.Visible = true;

                txtLoadAdvances.Text = dt.Rows[0]["accounts_LoanAdvances"].ToString();
                txtAccountAmount.Text = dt.Rows[0]["accounts_Amount"].ToString();
                txtAccountRemarks.Text = dt.Rows[0]["accounts_Remarks"].ToString();

                txtItEquipment.Text = dt.Rows[0]["it_Equipment"].ToString();
                txtItAmount.Text = dt.Rows[0]["it_Amount"].ToString();
                rblLoginEmail.SelectedValue = Convert.ToBoolean(dt.Rows[0]["it_UserLoginEmail"].ToString()) ? "Yes" : "No";
                rblLoginErp.SelectedValue = Convert.ToBoolean(dt.Rows[0]["it_ERPLogin"].ToString()) ? "Yes" : "No";
                txtItRemarks.Text = dt.Rows[0]["it_Remarks"].ToString();

                txtNoticePeriod.Text = dt.Rows[0]["hr_NoticePeriodServed"].ToString();
                txtLeavesDuringNoticePeriod.Text = dt.Rows[0]["hr_LeavesDuringNoticePeriod"].ToString();
                txtHcDeduction.Text = dt.Rows[0]["hr_HcDeduction"].ToString();
                txtHrAmount.Text = dt.Rows[0]["hr_Amount"].ToString();
                rblHrExitInterviewConducted.SelectedValue = Convert.ToBoolean(dt.Rows[0]["hr_ExitInterviewConducted"].ToString()) ? "Yes" : "No";
                rblHrStopSalaryPayment.SelectedValue = Convert.ToBoolean(dt.Rows[0]["hr_StopSalaryPayment"].ToString()) ? "Yes" : "No";
                rblHrRehireCall.SelectedValue = Convert.ToBoolean(dt.Rows[0]["hr_RehireCall"].ToString()) ? "Yes" : "No";
                txtHrReason.Text = dt.Rows[0]["hr_RehireCallNoReason"].ToString();
                txtHrRemarks.Text = dt.Rows[0]["hr_Remarks"].ToString();

                txtResourcesCheckedOut.Text = dt.Rows[0]["lib_ResourcesCheckedout"].ToString();
                txtValueOfAssets.Text = dt.Rows[0]["lib_ValueOfAssetsNotReturned"].ToString();
                txtLibAmount.Text = dt.Rows[0]["lib_Amount"].ToString();
                txtLibRemarks.Text = dt.Rows[0]["lib_Remarks"].ToString();

                txtMonth.Text = dt.Rows[0]["admin_Month"].ToString();
                txtAmountDue.Text = dt.Rows[0]["admin_Amount"].ToString();
                txtAdminRemarks.Text = dt.Rows[0]["admin_Remarks"].ToString();

                rblLabEquipHandedOver.SelectedValue = Convert.ToBoolean(dt.Rows[0]["laboratory_EquipmentHandedOver"].ToString()) ? "Yes" : "No";
                txtValueMiss.Text = dt.Rows[0]["laboratory_ValueOfMissingEquipment"].ToString();
                txtLabAmount.Text = dt.Rows[0]["laboratory_Amount"].ToString();
                txtLabRemarks.Text = dt.Rows[0]["laboratory_Remarks"].ToString();

                txtTrainingRemarks.Text = dt.Rows[0]["training_Remarks"].ToString();

                rblNoticeServed.SelectedValue = Convert.ToBoolean(dt.Rows[0]["hod_NoticePeriodServed"].ToString()) ? "Yes" : "No";
                rblRehireCallhod.SelectedValue = Convert.ToBoolean(dt.Rows[0]["hod_RehireCall"].ToString()) ? "Yes" : "No";
                rblHandingOverhod.SelectedValue = Convert.ToBoolean(dt.Rows[0]["hod_HandingOverComplete"].ToString()) ? "Yes" : "No";
                txtFromDate.Text = dt.Rows[0]["hod_NoticePeriodServedFrom"].ToString();
                txtToDate.Text = dt.Rows[0]["hod_NoticePeriodServedTo"].ToString();
                txtRehireCallhodReason.Text = dt.Rows[0]["hod_RehireCallNoReason"].ToString();
                txtLeavesDuringNotice.Text = dt.Rows[0]["hod_LeavesDuringNoticePeriod"].ToString();
                txtClBlc.Text = dt.Rows[0]["hod_CLBalance"].ToString();
                txtAlBalance.Text = dt.Rows[0]["hod_ALBalance"].ToString();
                txtHodRemarks.Text = dt.Rows[0]["hod_Remarks"].ToString();
            }
            else
            {
                getEmpLoanInfo(employeeCode.Trim());
                getEmpLeaveBalance(employeeCode.Trim());

                txtNoticePeriod.Text = noticeperiod;
                txtLeavesDuringNoticePeriod.Text = (LeaveDays == "") ? "0" : LeaveDays.ToString();
                txtFromDate.Text = subdate;
                txtToDate.Text = lastdate;
                txtLeavesDuringNotice.Text = (LeaveDays == "") ? "0" : LeaveDays.ToString(); 

                txtNoticePeriod.ReadOnly = true;
                txtLeavesDuringNoticePeriod.ReadOnly = true;
                txtFromDate.ReadOnly = true;
                txtToDate.ReadOnly = true;
                txtLeavesDuringNotice.ReadOnly = true;
                txtClBlc.ReadOnly = true;
                txtAlBalance.ReadOnly = true;
                txtLoadAdvances.ReadOnly = true;

                txtNoticePeriod.BackColor = Color.LightGray;
                txtLeavesDuringNoticePeriod.BackColor = Color.LightGray;
                txtFromDate.BackColor = Color.LightGray;
                txtToDate.BackColor = Color.LightGray;
                txtLeavesDuringNotice.BackColor = Color.LightGray;
                txtClBlc.BackColor = Color.LightGray;
                txtAlBalance.BackColor = Color.LightGray;
                txtLoadAdvances.BackColor = Color.LightGray;

                btnUpdateHOD.Visible = false;
                btnUpdateTraining.Visible = false;
                btnUpdateLaboratory.Visible = false;
                btnUpdateAdmin.Visible = false;
                btnUpdateLibrary.Visible = false;
                btnUpdateHr.Visible = false;
                btnUpdateIt.Visible = false;
                btnUpdateAccount.Visible = false;

                btnSaveHOD.Visible = true;
                btnSaveTraining.Visible = true;
                btnSaveLaboratory.Visible = true;
                btnSaveAdmin.Visible = true;
                btnSaveLibrary.Visible = true;
                btnSaveHr.Visible = true;
                btnSaveIt.Visible = true;
                btnSaveAccount.Visible = true;
            }

            divREI.Visible = true; 
        }
    }
    private void getEmpLoanInfo(string empcode)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("EmpLoanInfo4Clearence", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeCode", empcode);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Assuming you want to show a specific column value from the first row, for example:
                        // If you have a column named "LoanAmount" or something similar:
                        txtLoadAdvances.Text = dt.Rows[0]["OUTSTANDING_Balance"].ToString();
                    }
                    else
                    {
                        // No data, clear the previous data binding and set EmptyDataText (assuming this is 0 or some other string)
                        txtLoadAdvances.Text = "0";
                    }
                }
            }
        }
    }

    private void BindGridView4HoD(int hodCode)
    {
        // Clear previous data
        gvResignation.DataSource = null;
        gvResignation.DataBind();

        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetResignedEmployees2DaysPendingFromLastWorkingDay4HoD", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hodEmpCode", hodCode); 

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // If there is data, bind it
                        gvResignation.DataSource = dt;
                        gvResignation.DataBind();
                    }
                    else
                    {
                        // No data, clear the previous data binding and set EmptyDataText
                        gvResignation.DataSource = null;
                        gvResignation.DataBind();
                    }
                }
            }
        }
    }

    private void BindGridView(int regionId, int centerId)
    {
        // Clear previous data
        gvResignation.DataSource = null;
        gvResignation.DataBind();

        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetResignedEmployees2DaysPendingFromLastWorkingDay", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionId", regionId);
                cmd.Parameters.AddWithValue("@CenterId", centerId);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // If there is data, bind it
                        gvResignation.DataSource = dt;
                        gvResignation.DataBind();
                    }
                    else
                    {
                        // No data, clear the previous data binding and set EmptyDataText
                        gvResignation.DataSource = null;
                        gvResignation.DataBind();
                    }
                }
            }
        }
    }
    //private void BindFeeConcessionData(string employeecode)
    //{
    //    // Create a DataTable to hold test data
    //    DataTable testData = new DataTable();
    //    testData.Columns.Add("Name");
    //    testData.Columns.Add("RollNo");
    //    testData.Columns.Add("Branch");
    //    testData.Columns.Add("Class");

    //    // Add test rows
    //    testData.Rows.Add("", "", "", "");
    //    testData.Rows.Add("", "", "", "");
    //    testData.Rows.Add("", "", "", "");
    //    testData.Rows.Add("", "", "", "");
    //    //testData.Rows.Add("John Doe", "101", "Science", "10th");
    //    //testData.Rows.Add("Jane Smith", "102", "Commerce", "12th");
    //    //testData.Rows.Add("Robert Brown", "103", "Arts", "11th");
    //    //testData.Rows.Add("John Doe", "101", "Science", "10th");
    //    //testData.Rows.Add("Jane Smith", "102", "Commerce", "12th");

    //    // Bind the data to the GridView
    //    gvFeeConcession.DataSource = testData;
    //    gvFeeConcession.DataBind();
    //}
    //private void BindTrainingData(string employeecode)
    //{
    //    // Create a DataTable to hold test data
    //    DataTable testData = new DataTable();
    //    testData.Columns.Add("TrainingAttended");
    //    testData.Columns.Add("DeductionAgainstTraining");
    //    testData.Columns.Add("Date");

    //    // Add test rows
    //    testData.Rows.Add("", "", "");
    //    testData.Rows.Add("", "", "");
    //    testData.Rows.Add("", "", "");
    //    testData.Rows.Add("", "", "");
    //    testData.Rows.Add("", "", "");

    //    // Bind the data to the GridView
    //    gvTraining.DataSource = testData;
    //    gvTraining.DataBind();
    //}
    private void BindTrainingData(string employeecode)
    {
        // Clear previous data
        gvTraining.DataSource = null;
        gvTraining.DataBind();

        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("EmpTrainingInfo4Clearence", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeCode", employeecode); 

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // If there is data, bind it
                        gvTraining.DataSource = dt;
                        gvTraining.DataBind();
                    }
                    else
                    {
                        // No data, clear the previous data binding and set EmptyDataText
                        gvTraining.DataSource = null;
                        gvTraining.DataBind();
                    }
                }
            }
        }
    }
    private void BindFeeConcessionData(string employeecode)
    {
        // Clear previous data
        gvFeeConcession.DataSource = null;
        gvFeeConcession.DataBind();

        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("EmpChildFeeConcessionInfo4Clearence", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeCode", employeecode); 

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // If there is data, bind it
                        gvFeeConcession.DataSource = dt;
                        gvFeeConcession.DataBind();
                    }
                    else
                    {
                        // No data, clear the previous data binding and set EmptyDataText
                        gvFeeConcession.DataSource = null;
                        gvFeeConcession.DataBind();
                    }
                }
            }
        }
    }
    public void GetLeaveDays(DateTime leaveFrom, DateTime leaveTo)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString; 

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Define the stored procedure and parameters
            string storedProcedure = "EmpLeavesInfo4Clearence";

            // Create a command object
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters for the stored procedure
                command.Parameters.AddWithValue("@LeaveFrom", leaveFrom);
                command.Parameters.AddWithValue("@LeaveTo", leaveTo);

                try
                {
                    // Open the connection
                    connection.Open();

                    // Execute the command and get the data
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        { 
                            LeaveDays =   reader["LeaveDays"].ToString() ; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Error: {ex.Message}");
                }
            }
        } 
    }
    private void getEmpLeaveBalance(string empcode)
    {
        BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

        DataTable dt = new DataTable();
        obj.EmployeeCode = Session["EmployeeCode"].ToString();
        obj.Year = DateTime.Now.Year.ToString();
        dt = obj.EmployeeLeaveBalanceFetch(obj);
        if (dt.Rows.Count > 0)
        { 
            if (dt.Rows[0]["balAnnual"].ToString() == "")
                txtAlBalance.Text = "0";
            else
                txtAlBalance.Text = dt.Rows[0]["balAnnual"].ToString();

            if (dt.Rows[0]["balCasual"].ToString() == "")
                txtClBlc.Text = "0";
            else
                txtClBlc.Text = dt.Rows[0]["balCasual"].ToString();             
        }         
    }
    private List<string> getEmpIfClearenceFormAvailable(string empcode)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        string selectQuery = "SELECT distinct EmployeeCode, DepartmentValue FROM EmployeeClearenceRights WHERE Status_Id = 1 AND EmployeeCode = " + empcode;

        List<string> departmentValues = new List<string>(); // List to store department values

        // Create a connection to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a command to execute the query
            SqlCommand command = new SqlCommand(selectQuery, connection);

            try
            {
                // Open the connection
                connection.Open();

                // Execute the query and get the data
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Access the data and add each department value to the list
                        string departmentValue = reader["DepartmentValue"].ToString().Trim();
                        departmentValues.Add(departmentValue);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log or rethrow)
                //Console.WriteLine($"Error: {ex.Message}");
            }
        }

        return departmentValues; // Return the list of department values
    }    
    public DataTable GetEmployeeClearance(string employeecode)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string selectQuery = "SELECT * FROM EmployeeClearence WHERE EmployeeCode = " + employeecode;
            SqlDataAdapter da = new SqlDataAdapter(selectQuery, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public void InsertEmployeeClearance(string employeeCode, int accounts_LoanAdvances, int accounts_Amount, string accounts_Remarks, string accounts_VerifiedBy, bool accounts_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearence (EmployeeCode, accounts_LoanAdvances, accounts_Amount, accounts_Remarks, accounts_VerifiedBy, accounts_approve) " +
                           "VALUES (@EmployeeCode, @LoanAdvances, @Amount, @Remarks, @VerifiedBy, @Approve)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@LoanAdvances", accounts_LoanAdvances);
                cmd.Parameters.AddWithValue("@Amount", accounts_Amount);
                cmd.Parameters.AddWithValue("@Remarks", accounts_Remarks);
                cmd.Parameters.AddWithValue("@VerifiedBy", accounts_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", accounts_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void InsertEmployeeClearance(string employeeCode, int hr_NoticePeriodServed, int hr_LeavesDuringNoticePeriod, bool hr_ExitInterviewConducted, bool hr_RehireCall, string hr_RehireCallNoReason, bool hr_StopSalaryPayment, int hr_HcDeduction, int hr_Amount, string hr_Remarks, string hr_VerifiedBy, bool hr_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearence (employeeCode, hr_NoticePeriodServed, hr_LeavesDuringNoticePeriod, hr_ExitInterviewConducted, hr_RehireCall, hr_RehireCallNoReason, hr_StopSalaryPayment, hr_HcDeduction, hr_Amount,hr_Remarks, hr_VerifiedBy, hr_approve) " +
                           "VALUES (@EmployeeCode, @hr_NoticePeriodServed, @hr_LeavesDuringNoticePeriod, @hr_ExitInterviewConducted, @hr_RehireCall, @hr_RehireCallNoReason, @hr_StopSalaryPayment, @hr_HcDeduction, @hr_Amount, @hr_Remarks, @hr_VerifiedBy, @Approve)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@hr_NoticePeriodServed", hr_NoticePeriodServed);
                cmd.Parameters.AddWithValue("@hr_LeavesDuringNoticePeriod", hr_LeavesDuringNoticePeriod);
                cmd.Parameters.AddWithValue("@hr_ExitInterviewConducted", hr_ExitInterviewConducted);
                cmd.Parameters.AddWithValue("@hr_RehireCall", hr_RehireCall);
                cmd.Parameters.AddWithValue("@hr_RehireCallNoReason", hr_RehireCallNoReason);
                cmd.Parameters.AddWithValue("@hr_StopSalaryPayment", hr_StopSalaryPayment);
                cmd.Parameters.AddWithValue("@hr_HcDeduction", hr_HcDeduction);
                cmd.Parameters.AddWithValue("@hr_Amount", hr_Amount);
                cmd.Parameters.AddWithValue("@hr_Remarks", hr_Remarks);
                cmd.Parameters.AddWithValue("@hr_VerifiedBy", hr_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", hr_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void InsertEmployeeClearance(string employeeCode, string lib_ResourcesCheckedout, int lib_ValueOfAssetsNotReturned, int lib_Amount, string lib_Remarks, string lib_VerifiedBy, bool lib_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearence (EmployeeCode, lib_ResourcesCheckedout, lib_ValueOfAssetsNotReturned, lib_Amount, lib_Remarks, lib_VerifiedBy, lib_approve) " +
                           "VALUES (@EmployeeCode, @lib_ResourcesCheckedout, @lib_ValueOfAssetsNotReturned, @lib_Amount, @lib_Remarks, @lib_VerifiedBy, @Approve)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@lib_ResourcesCheckedout", lib_ResourcesCheckedout);
                cmd.Parameters.AddWithValue("@lib_ValueOfAssetsNotReturned", lib_ValueOfAssetsNotReturned);
                cmd.Parameters.AddWithValue("@lib_Amount", lib_Amount);
                cmd.Parameters.AddWithValue("@lib_Remarks", lib_Remarks);
                cmd.Parameters.AddWithValue("@lib_VerifiedBy", lib_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", lib_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void InsertEmployeeClearance(string employeeCode, string admin_Month, int admin_Amount, string admin_Remarks, string admin_VerifiedBy, bool admin_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearence (EmployeeCode, admin_Month, admin_Amount, admin_Remarks, admin_VerifiedBy, admin_approve) " +
                           "VALUES (@EmployeeCode, @admin_Month, @admin_Amount, @admin_Remarks, @admin_VerifiedBy, @Approve)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@admin_Month", admin_Month);
                cmd.Parameters.AddWithValue("@admin_Amount", admin_Amount);
                cmd.Parameters.AddWithValue("@admin_Remarks", admin_Remarks);
                cmd.Parameters.AddWithValue("@admin_VerifiedBy", admin_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", admin_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void InsertEmployeeClearance(string employeeCode, bool laboratory_EquipmentHandedOver, int laboratory_ValueOfMissingEquipment,int laboratory_Amount, string laboratory_Remarks, string laboratory_VerifiedBy, bool laboratory_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearence (EmployeeCode, laboratory_EquipmentHandedOver, laboratory_ValueOfMissingEquipment, laboratory_Amount, laboratory_Remarks, laboratory_VerifiedBy, laboratory_approve) " +
                           "VALUES (@EmployeeCode, @laboratory_EquipmentHandedOver, @laboratory_ValueOfMissingEquipment, @laboratory_Amount, @laboratory_Remarks, @laboratory_VerifiedBy, @Approve)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@laboratory_EquipmentHandedOver", laboratory_EquipmentHandedOver);
                cmd.Parameters.AddWithValue("@laboratory_ValueOfMissingEquipment", laboratory_ValueOfMissingEquipment);
                cmd.Parameters.AddWithValue("@laboratory_Amount", laboratory_Amount);
                cmd.Parameters.AddWithValue("@laboratory_Remarks", laboratory_Remarks);
                cmd.Parameters.AddWithValue("@laboratory_VerifiedBy", laboratory_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", laboratory_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void InsertEmployeeClearance(string employeeCode, string training_Remarks, string training_VerifiedBy, bool training_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearence (EmployeeCode, training_Remarks, training_VerifiedBy, training_approve) " +
                           "VALUES (@EmployeeCode, @training_Remarks, @training_VerifiedBy, @Approve)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@training_Remarks", training_Remarks);
                cmd.Parameters.AddWithValue("@training_VerifiedBy", training_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", training_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void InsertEmployeeClearance(string employeeCode, bool hod_NoticePeriodServed, DateTime hod_NoticePeriodServedFrom, DateTime hod_NoticePeriodServedTo, int hod_LeavesDuringNoticePeriod, bool hod_RehireCall, string hod_RehireCallNoReason, bool hod_HandingOverComplete, int hod_CLBalance, int hod_ALBalance, string hod_Remarks, string hod_VerifiedBy, bool hod_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearence (EmployeeCode, hod_NoticePeriodServed, hod_NoticePeriodServedFrom, hod_NoticePeriodServedTo, hod_LeavesDuringNoticePeriod, hod_RehireCall, hod_RehireCallNoReason, hod_HandingOverComplete, hod_CLBalance, hod_ALBalance, hod_Remarks, hod_VerifiedBy, hod_approve) " +
                           "VALUES (@EmployeeCode, @hod_NoticePeriodServed, @hod_NoticePeriodServedFrom, @hod_NoticePeriodServedTo, @hod_LeavesDuringNoticePeriod, @hod_RehireCall, @hod_RehireCallNoReason, @hod_HandingOverComplete, @hod_CLBalance, @hod_ALBalance, @hod_Remarks, @hod_VerifiedBy, @Approve)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@hod_NoticePeriodServed", hod_NoticePeriodServed);
                cmd.Parameters.AddWithValue("@hod_NoticePeriodServedFrom", hod_NoticePeriodServedFrom);
                cmd.Parameters.AddWithValue("@hod_NoticePeriodServedTo", hod_NoticePeriodServedTo);
                cmd.Parameters.AddWithValue("@hod_LeavesDuringNoticePeriod", hod_LeavesDuringNoticePeriod);
                cmd.Parameters.AddWithValue("@hod_RehireCall", hod_RehireCall);
                cmd.Parameters.AddWithValue("@hod_RehireCallNoReason", hod_RehireCallNoReason);
                cmd.Parameters.AddWithValue("@hod_HandingOverComplete", hod_HandingOverComplete);
                cmd.Parameters.AddWithValue("@hod_CLBalance", hod_CLBalance);
                cmd.Parameters.AddWithValue("@hod_ALBalance", hod_ALBalance);
                cmd.Parameters.AddWithValue("@hod_Remarks", hod_Remarks);
                cmd.Parameters.AddWithValue("@hod_VerifiedBy", hod_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", hod_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void InsertEmployeeClearance(string employeeCode, string it_Equipment, bool it_UserLoginEmail, bool it_ERPLogin, int it_Amount, string it_Remarks, string it_VerifiedBy, bool it_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearence (EmployeeCode, it_Equipment, it_UserLoginEmail, it_ERPLogin, it_Amount, it_Remarks, it_VerifiedBy, it_approve) " +
                           "VALUES (@EmployeeCode, @it_Equipment, @it_UserLoginEmail, @it_ERPLogin, @it_Amount, @it_Remarks, @it_VerifiedBy, @Approve)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@it_Equipment", it_Equipment);
                cmd.Parameters.AddWithValue("@it_UserLoginEmail", it_UserLoginEmail);
                cmd.Parameters.AddWithValue("@it_ERPLogin", it_ERPLogin);
                cmd.Parameters.AddWithValue("@it_Amount", it_Amount);
                cmd.Parameters.AddWithValue("@it_Remarks", it_Remarks);
                cmd.Parameters.AddWithValue("@it_VerifiedBy", it_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", it_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    } 
    public void UpdateEmployeeClearance(string employeeCode, int accounts_LoanAdvances, int accounts_Amount, string accounts_Remarks, string accounts_VerifiedBy, bool accounts_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        { 
            string query = "UPDATE EmployeeClearence " +
               "SET accounts_LoanAdvances = @LoanAdvances, " +
               "accounts_Amount = @Amount, " +
               "accounts_Remarks = @Remarks, " +
               "accounts_VerifiedBy = @VerifiedBy, " +
               "accounts_approve = @Approve " +
               "WHERE EmployeeCode = @EmployeeCode";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@LoanAdvances", accounts_LoanAdvances);
                cmd.Parameters.AddWithValue("@Amount", accounts_Amount);
                cmd.Parameters.AddWithValue("@Remarks", accounts_Remarks);
                cmd.Parameters.AddWithValue("@VerifiedBy", accounts_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", accounts_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeClearance(string employeeCode, int hr_NoticePeriodServed, int hr_LeavesDuringNoticePeriod, bool hr_ExitInterviewConducted, bool hr_RehireCall, string hr_RehireCallNoReason, bool hr_StopSalaryPayment, int hr_HcDeduction, int hr_Amount, string hr_Remarks, string hr_VerifiedBy, bool hr_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearence " +
               "SET hr_NoticePeriodServed = @hr_NoticePeriodServed, " +
               "hr_LeavesDuringNoticePeriod = @hr_LeavesDuringNoticePeriod, " +
               "hr_ExitInterviewConducted = @hr_ExitInterviewConducted, " +
               "hr_RehireCall = @hr_RehireCall, " +
               "hr_RehireCallNoReason = @hr_RehireCallNoReason, " +
               "hr_StopSalaryPayment = @hr_StopSalaryPayment, " +
               "hr_HcDeduction = @hr_HcDeduction, " +
               "hr_Amount = @hr_Amount, " +
               "hr_Remarks = @hr_Remarks, " +
               "hr_VerifiedBy = @hr_VerifiedBy, " +
               "hr_approve = @Approve " +
               "WHERE employeeCode = @EmployeeCode";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@hr_NoticePeriodServed", hr_NoticePeriodServed);
                cmd.Parameters.AddWithValue("@hr_LeavesDuringNoticePeriod", hr_LeavesDuringNoticePeriod);
                cmd.Parameters.AddWithValue("@hr_ExitInterviewConducted", hr_ExitInterviewConducted);
                cmd.Parameters.AddWithValue("@hr_RehireCall", hr_RehireCall);
                cmd.Parameters.AddWithValue("@hr_RehireCallNoReason", hr_RehireCallNoReason);
                cmd.Parameters.AddWithValue("@hr_StopSalaryPayment", hr_StopSalaryPayment);
                cmd.Parameters.AddWithValue("@hr_HcDeduction", hr_HcDeduction);
                cmd.Parameters.AddWithValue("@hr_Amount", hr_Amount);
                cmd.Parameters.AddWithValue("@hr_Remarks", hr_Remarks);
                cmd.Parameters.AddWithValue("@hr_VerifiedBy", hr_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", hr_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeClearance(string employeeCode, string lib_ResourcesCheckedout, int lib_ValueOfAssetsNotReturned, int lib_Amount, string lib_Remarks, string lib_VerifiedBy, bool lib_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearence " +
               "SET lib_ResourcesCheckedout = @lib_ResourcesCheckedout, " +
               "lib_ValueOfAssetsNotReturned = @lib_ValueOfAssetsNotReturned, " +
               "lib_Amount = @lib_Amount, " +
               "lib_Remarks = @lib_Remarks, " +
               "lib_VerifiedBy = @lib_VerifiedBy, " +
               "lib_approve = @Approve " +
               "WHERE EmployeeCode = @EmployeeCode";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@lib_ResourcesCheckedout", lib_ResourcesCheckedout);
                cmd.Parameters.AddWithValue("@lib_ValueOfAssetsNotReturned", lib_ValueOfAssetsNotReturned);
                cmd.Parameters.AddWithValue("@lib_Amount", lib_Amount);
                cmd.Parameters.AddWithValue("@lib_Remarks", lib_Remarks);
                cmd.Parameters.AddWithValue("@lib_VerifiedBy", lib_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", lib_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeClearance(string employeeCode, string admin_Month, int admin_Amount, string admin_Remarks, string admin_VerifiedBy, bool admin_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearence " +
               "SET admin_Month = @admin_Month, " +
               "admin_Amount = @admin_Amount, " +
               "admin_Remarks = @admin_Remarks, " +
               "admin_VerifiedBy = @admin_VerifiedBy, " +
               "admin_approve = @Approve " +
               "WHERE EmployeeCode = @EmployeeCode";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@admin_Month", admin_Month);
                cmd.Parameters.AddWithValue("@admin_Amount", admin_Amount);
                cmd.Parameters.AddWithValue("@admin_Remarks", admin_Remarks);
                cmd.Parameters.AddWithValue("@admin_VerifiedBy", admin_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", admin_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeClearance(string employeeCode, bool laboratory_EquipmentHandedOver, int laboratory_ValueOfMissingEquipment,int laboratory_Amount, string laboratory_Remarks, string laboratory_VerifiedBy, bool laboratory_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearence " +
               "SET laboratory_EquipmentHandedOver = @laboratory_EquipmentHandedOver, " +
               "laboratory_ValueOfMissingEquipment = @laboratory_ValueOfMissingEquipment, " +
               "laboratory_Amount = @laboratory_Amount, " +
               "laboratory_Remarks = @laboratory_Remarks, " +
               "laboratory_VerifiedBy = @laboratory_VerifiedBy, " +
               "laboratory_approve = @Approve " +
               "WHERE EmployeeCode = @EmployeeCode";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@laboratory_EquipmentHandedOver", laboratory_EquipmentHandedOver);
                cmd.Parameters.AddWithValue("@laboratory_ValueOfMissingEquipment", laboratory_ValueOfMissingEquipment);
                cmd.Parameters.AddWithValue("@laboratory_Amount", laboratory_Amount);
                cmd.Parameters.AddWithValue("@laboratory_Remarks", laboratory_Remarks);
                cmd.Parameters.AddWithValue("@laboratory_VerifiedBy", laboratory_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", laboratory_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeClearance(string employeeCode, string training_Remarks, string training_VerifiedBy, bool training_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearence " +
               "SET training_Remarks = @training_Remarks, " +
               "training_VerifiedBy = @training_VerifiedBy, " +
               "training_approve = @Approve " +
               "WHERE EmployeeCode = @EmployeeCode";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@training_Remarks", training_Remarks);
                cmd.Parameters.AddWithValue("@training_VerifiedBy", training_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", training_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeClearance(string employeeCode, bool hod_NoticePeriodServed, DateTime hod_NoticePeriodServedFrom, DateTime hod_NoticePeriodServedTo, int hod_LeavesDuringNoticePeriod, bool hod_RehireCall, string hod_RehireCallNoReason, bool hod_HandingOverComplete, int hod_CLBalance, int hod_ALBalance, string hod_Remarks, string hod_VerifiedBy, bool hod_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearence " +
               "SET hod_NoticePeriodServed = @hod_NoticePeriodServed, " +
               "hod_NoticePeriodServedFrom = @hod_NoticePeriodServedFrom, " +
               "hod_NoticePeriodServedTo = @hod_NoticePeriodServedTo, " +
               "hod_LeavesDuringNoticePeriod = @hod_LeavesDuringNoticePeriod, " +
               "hod_RehireCall = @hod_RehireCall, " +
               "hod_RehireCallNoReason = @hod_RehireCallNoReason, " +
               "hod_HandingOverComplete = @hod_HandingOverComplete, " +
               "hod_CLBalance = @hod_CLBalance, " +
               "hod_ALBalance = @hod_ALBalance, " +
               "hod_Remarks = @hod_Remarks, " +
               "hod_VerifiedBy = @hod_VerifiedBy, " +
               "hod_approve = @Approve " +
               "WHERE EmployeeCode = @EmployeeCode";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@hod_NoticePeriodServed", hod_NoticePeriodServed);
                cmd.Parameters.AddWithValue("@hod_NoticePeriodServedFrom", hod_NoticePeriodServedFrom);
                cmd.Parameters.AddWithValue("@hod_NoticePeriodServedTo", hod_NoticePeriodServedTo);
                cmd.Parameters.AddWithValue("@hod_LeavesDuringNoticePeriod", hod_LeavesDuringNoticePeriod);
                cmd.Parameters.AddWithValue("@hod_RehireCall", hod_RehireCall);
                cmd.Parameters.AddWithValue("@hod_RehireCallNoReason", hod_RehireCallNoReason);
                cmd.Parameters.AddWithValue("@hod_HandingOverComplete", hod_HandingOverComplete);
                cmd.Parameters.AddWithValue("@hod_CLBalance", hod_CLBalance);
                cmd.Parameters.AddWithValue("@hod_ALBalance", hod_ALBalance);
                cmd.Parameters.AddWithValue("@hod_Remarks", hod_Remarks);
                cmd.Parameters.AddWithValue("@hod_VerifiedBy", hod_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", hod_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeClearance(string employeeCode, string it_Equipment, bool it_UserLoginEmail, bool it_ERPLogin, int it_Amount, string it_Remarks, string it_VerifiedBy, bool it_approve)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearence " +
               "SET it_Equipment = @it_Equipment, " +
               "it_UserLoginEmail = @it_UserLoginEmail, " +
               "it_ERPLogin = @it_ERPLogin, " +
               "it_Amount = @it_Amount, " +
               "it_Remarks = @it_Remarks, " +
               "it_VerifiedBy = @it_VerifiedBy, " +
               "it_approve = @Approve " +
               "WHERE EmployeeCode = @EmployeeCode";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@it_Equipment", it_Equipment);
                cmd.Parameters.AddWithValue("@it_UserLoginEmail", it_UserLoginEmail);
                cmd.Parameters.AddWithValue("@it_ERPLogin", it_ERPLogin);
                cmd.Parameters.AddWithValue("@it_Amount", it_Amount);
                cmd.Parameters.AddWithValue("@it_Remarks", it_Remarks);
                cmd.Parameters.AddWithValue("@it_VerifiedBy", it_VerifiedBy);
                cmd.Parameters.AddWithValue("@Approve", it_approve);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public DataTable GetEmployeeFeeConcession()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM EmployeeClearenceFeeConcession";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public void InsertEmployeeFeeConcession(int employeeCode, string name, string rollNo, string branch, string className)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearenceFeeConcession (EmployeeCode, Name, RollNo, Branch, Class) " +
                           "VALUES (@EmployeeCode, @Name, @RollNo, @Branch, @Class)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@RollNo", rollNo);
                cmd.Parameters.AddWithValue("@Branch", branch);
                cmd.Parameters.AddWithValue("@Class", className);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeFeeConcession(int id, string name, string rollNo, string branch, string className)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearenceFeeConcession SET Name = @Name, RollNo = @RollNo, " +
                           "Branch = @Branch, Class = @Class WHERE Id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@RollNo", rollNo);
                cmd.Parameters.AddWithValue("@Branch", branch);
                cmd.Parameters.AddWithValue("@Class", className);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public DataTable GetEmployeeTraining()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM EmployeeClearenceTraining";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public void InsertEmployeeTraining(int employeeCode, string trainingAttended, DateTime trainingDate, decimal deductionAgainstTraining)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO EmployeeClearenceTraining (EmployeeCode, TrainingAttended, TrainingAttendedDate, DeductionAgainstTraining) " +
                           "VALUES (@EmployeeCode, @TrainingAttended, @TrainingAttendedDate, @Deduction)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@TrainingAttended", trainingAttended);
                cmd.Parameters.AddWithValue("@TrainingAttendedDate", trainingDate);
                cmd.Parameters.AddWithValue("@Deduction", deductionAgainstTraining);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void UpdateEmployeeTraining(int id, string trainingAttended, DateTime trainingDate, decimal deductionAgainstTraining)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE EmployeeClearenceTraining SET TrainingAttended = @TrainingAttended, " +
                           "TrainingAttendedDate = @TrainingAttendedDate, DeductionAgainstTraining = @Deduction WHERE Id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@TrainingAttended", trainingAttended);
                cmd.Parameters.AddWithValue("@TrainingAttendedDate", trainingDate);
                cmd.Parameters.AddWithValue("@Deduction", deductionAgainstTraining);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void btnSaveAccount_Click(object sender, EventArgs e)
    {
        getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString());

        DataTable dt = GetEmployeeClearance(lblEmployeeCode.Text);

        if (dt.Rows.Count > 0) // Check if there are any rows
        {
            UpdateEmployeeClearance(lblEmployeeCode.Text, Convert.ToInt32(txtLoadAdvances.Text), Convert.ToInt32(txtAccountAmount.Text), txtAccountRemarks.Text, Session["EmployeeCode"].ToString(),chkAccount.Checked);
        }
        else
        {
            InsertEmployeeClearance(lblEmployeeCode.Text, Convert.ToInt32(txtLoadAdvances.Text), Convert.ToInt32(txtAccountAmount.Text), txtAccountRemarks.Text, Session["EmployeeCode"].ToString(), chkAccount.Checked);
        }
    }

    protected void btnSaveIt_Click(object sender, EventArgs e)
    {
        bool isLoginEmail = (rblLoginEmail.SelectedItem != null && rblLoginEmail.SelectedValue == "Yes");
        bool isLoginErp = (rblLoginErp.SelectedItem != null && rblLoginErp.SelectedValue == "Yes");

        getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString());

        DataTable dt = GetEmployeeClearance(lblEmployeeCode.Text);

        if (dt.Rows.Count > 0) // Check if there are any rows
        {
            UpdateEmployeeClearance(lblEmployeeCode.Text, txtItEquipment.Text, isLoginEmail, isLoginErp, Convert.ToInt32(txtItAmount.Text), txtItRemarks.Text, Session["EmployeeCode"].ToString(),chkIt.Checked);
        }
        else
        {
            InsertEmployeeClearance(lblEmployeeCode.Text, txtItEquipment.Text, isLoginEmail, isLoginErp, Convert.ToInt32(txtItAmount.Text), txtItRemarks.Text, Session["EmployeeCode"].ToString(), chkIt.Checked);
        }
    }

    protected void btnSaveHr_Click(object sender, EventArgs e)
    {
        bool isHrExitInterviewConducted = (rblHrExitInterviewConducted.SelectedItem != null && rblHrExitInterviewConducted.SelectedValue == "Yes");
        bool isHrRehireCall = (rblHrRehireCall.SelectedItem != null && rblHrRehireCall.SelectedValue == "Yes");
        bool isHrStopSalaryPayment = (rblHrStopSalaryPayment.SelectedItem != null && rblHrStopSalaryPayment.SelectedValue == "Yes");

        getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString());

        DataTable dt = GetEmployeeClearance(lblEmployeeCode.Text);

        if (dt.Rows.Count > 0) // Check if there are any rows
        {
            UpdateEmployeeClearance(lblEmployeeCode.Text, Convert.ToInt32(txtNoticePeriod.Text), Convert.ToInt32(txtLeavesDuringNoticePeriod.Text), isHrExitInterviewConducted, isHrRehireCall, txtHrReason.Text, isHrStopSalaryPayment, Convert.ToInt32(txtHcDeduction.Text), Convert.ToInt32(txtHrAmount.Text), txtHrRemarks.Text, Session["EmployeeCode"].ToString(), chkHr.Checked);
        }
        else
        {
            InsertEmployeeClearance(lblEmployeeCode.Text, Convert.ToInt32(txtNoticePeriod.Text), Convert.ToInt32(txtLeavesDuringNoticePeriod.Text), isHrExitInterviewConducted, isHrRehireCall, txtHrReason.Text, isHrStopSalaryPayment, Convert.ToInt32(txtHcDeduction.Text), Convert.ToInt32(txtHrAmount.Text), txtHrRemarks.Text, Session["EmployeeCode"].ToString(), chkHr.Checked);
        }            
    }

    protected void btnSaveLibrary_Click(object sender, EventArgs e)
    {
        getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString());

        DataTable dt = GetEmployeeClearance(lblEmployeeCode.Text);

        if (dt.Rows.Count > 0) // Check if there are any rows
        {
            UpdateEmployeeClearance(lblEmployeeCode.Text, txtResourcesCheckedOut.Text, Convert.ToInt32(txtValueOfAssets.Text), Convert.ToInt32(txtLibAmount.Text), txtLibRemarks.Text, Session["EmployeeCode"].ToString(), chkLibrary.Checked);
        }
        else
        {
            InsertEmployeeClearance(lblEmployeeCode.Text, txtResourcesCheckedOut.Text, Convert.ToInt32(txtValueOfAssets.Text), Convert.ToInt32(txtLibAmount.Text), txtLibRemarks.Text, Session["EmployeeCode"].ToString(), chkLibrary.Checked);
        }            
    }

    protected void btnSaveAdmin_Click(object sender, EventArgs e)
    {
        getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString());

        DataTable dt = GetEmployeeClearance(lblEmployeeCode.Text);

        if (dt.Rows.Count > 0) // Check if there are any rows
        {
            UpdateEmployeeClearance(lblEmployeeCode.Text, txtMonth.Text, Convert.ToInt32(txtAmountDue.Text), txtAdminRemarks.Text, Session["EmployeeCode"].ToString(), chkAdmin.Checked);
        }
        else
        {
            InsertEmployeeClearance(lblEmployeeCode.Text, txtMonth.Text, Convert.ToInt32(txtAmountDue.Text), txtAdminRemarks.Text, Session["EmployeeCode"].ToString(), chkAdmin.Checked);
        }            
    }

    protected void btnSaveLaboratory_Click(object sender, EventArgs e)
    {
        bool isLabEquipHandedOver = (rblLabEquipHandedOver.SelectedItem != null && rblLabEquipHandedOver.SelectedValue == "Yes");

        getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString());

        DataTable dt = GetEmployeeClearance(lblEmployeeCode.Text);

        if (dt.Rows.Count > 0) // Check if there are any rows
        {
            UpdateEmployeeClearance(lblEmployeeCode.Text, isLabEquipHandedOver, Convert.ToInt32(txtValueMiss.Text), Convert.ToInt32(txtLabAmount.Text), txtLabRemarks.Text, Session["EmployeeCode"].ToString(), chkLaboratory.Checked);
        }
        else
        {
            InsertEmployeeClearance(lblEmployeeCode.Text, isLabEquipHandedOver, Convert.ToInt32(txtValueMiss.Text), Convert.ToInt32(txtLabAmount.Text), txtLabRemarks.Text, Session["EmployeeCode"].ToString(), chkLaboratory.Checked);
        }            
    }

    protected void btnSaveTraining_Click(object sender, EventArgs e)
    {
        getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString());

        DataTable dt = GetEmployeeClearance(lblEmployeeCode.Text);

        if (dt.Rows.Count > 0) // Check if there are any rows
        {
            UpdateEmployeeClearance(lblEmployeeCode.Text, txtTrainingRemarks.Text, Session["EmployeeCode"].ToString(), chkTraining.Checked);
        }
        else
        {
            InsertEmployeeClearance(lblEmployeeCode.Text, txtTrainingRemarks.Text, Session["EmployeeCode"].ToString(), chkTraining.Checked);
        }            
    }

    protected void btnSaveHOD_Click(object sender, EventArgs e)
    {
        bool isNoticeServed = (rblNoticeServed.SelectedItem != null && rblNoticeServed.SelectedValue == "Yes");
        bool isRehireCallhod = (rblRehireCallhod.SelectedItem != null && rblRehireCallhod.SelectedValue == "Yes");
        bool isHandingOverhod = (rblHandingOverhod.SelectedItem != null && rblHandingOverhod.SelectedValue == "Yes");

        getEmpIfClearenceFormAvailable(Session["EmployeeCode"].ToString());

        DataTable dt = GetEmployeeClearance(lblEmployeeCode.Text);

        if (dt.Rows.Count > 0) // Check if there are any rows
        {
            UpdateEmployeeClearance(lblEmployeeCode.Text, isNoticeServed, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(txtLeavesDuringNotice.Text), isRehireCallhod, txtRehireCallhodReason.Text, isHandingOverhod, Convert.ToInt32(txtClBlc.Text), Convert.ToInt32(txtAlBalance.Text), txtHodRemarks.Text, Session["EmployeeCode"].ToString(), chkHOD.Checked);
        }
        else
        {
            InsertEmployeeClearance(lblEmployeeCode.Text, isNoticeServed, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(txtLeavesDuringNotice.Text), isRehireCallhod, txtRehireCallhodReason.Text, isHandingOverhod, Convert.ToInt32(txtClBlc.Text), Convert.ToInt32(txtAlBalance.Text), txtHodRemarks.Text, Session["EmployeeCode"].ToString(), chkHOD.Checked);
        }            
    } 
    protected void btnUSaveAccount_Click(object sender, EventArgs e)
    {
        UpdateEmployeeClearance(lblEmployeeCode.Text, Convert.ToInt32(txtLoadAdvances.Text), Convert.ToInt32(txtAccountAmount.Text), txtAccountRemarks.Text, Session["EmployeeCode"].ToString(), chkAccount.Checked);
    }

    protected void btnUSaveIt_Click(object sender, EventArgs e)
    {
        bool isLoginEmail = (rblLoginEmail.SelectedItem != null && rblLoginEmail.SelectedValue == "Yes");
        bool isLoginErp = (rblLoginErp.SelectedItem != null && rblLoginErp.SelectedValue == "Yes");

        UpdateEmployeeClearance(lblEmployeeCode.Text, txtItEquipment.Text, isLoginEmail, isLoginErp, Convert.ToInt32(txtItAmount.Text), txtItRemarks.Text, Session["EmployeeCode"].ToString(), chkIt.Checked);
    }

    protected void btnUSaveHr_Click(object sender, EventArgs e)
    {
        bool isHrExitInterviewConducted = (rblHrExitInterviewConducted.SelectedItem != null && rblHrExitInterviewConducted.SelectedValue == "Yes");
        bool isHrRehireCall = (rblHrRehireCall.SelectedItem != null && rblHrRehireCall.SelectedValue == "Yes");
        bool isHrStopSalaryPayment = (rblHrStopSalaryPayment.SelectedItem != null && rblHrStopSalaryPayment.SelectedValue == "Yes");

        UpdateEmployeeClearance(lblEmployeeCode.Text, Convert.ToInt32(txtNoticePeriod.Text), Convert.ToInt32(txtLeavesDuringNoticePeriod.Text), isHrExitInterviewConducted, isHrRehireCall, txtHrReason.Text, isHrStopSalaryPayment, Convert.ToInt32(txtHcDeduction.Text), Convert.ToInt32(txtHrAmount.Text), txtHrRemarks.Text, Session["EmployeeCode"].ToString(), chkHr.Checked);
    }

    protected void btnUSaveLibrary_Click(object sender, EventArgs e)
    {
        UpdateEmployeeClearance(lblEmployeeCode.Text, txtResourcesCheckedOut.Text, Convert.ToInt32(txtValueOfAssets.Text), Convert.ToInt32(txtLibAmount.Text), txtLibRemarks.Text, Session["EmployeeCode"].ToString(), chkLibrary.Checked);
    }

    protected void btnUSaveAdmin_Click(object sender, EventArgs e)
    {
        UpdateEmployeeClearance(lblEmployeeCode.Text, txtMonth.Text, Convert.ToInt32(txtAmountDue.Text), txtAdminRemarks.Text, Session["EmployeeCode"].ToString(), chkAdmin.Checked);
    }

    protected void btnUSaveLaboratory_Click(object sender, EventArgs e)
    {
        bool isLabEquipHandedOver = (rblLabEquipHandedOver.SelectedItem != null && rblLabEquipHandedOver.SelectedValue == "Yes");

        UpdateEmployeeClearance(lblEmployeeCode.Text, isLabEquipHandedOver, Convert.ToInt32(txtValueMiss.Text), Convert.ToInt32(txtLabAmount.Text), txtLabRemarks.Text, Session["EmployeeCode"].ToString(), chkLaboratory.Checked);
    }

    protected void btnUSaveTraining_Click(object sender, EventArgs e)
    {
        UpdateEmployeeClearance(lblEmployeeCode.Text, txtTrainingRemarks.Text, Session["EmployeeCode"].ToString(), chkTraining.Checked);
    }

    protected void btnUSaveHOD_Click(object sender, EventArgs e)
    {
        bool isNoticeServed = (rblNoticeServed.SelectedItem != null && rblNoticeServed.SelectedValue == "Yes");
        bool isRehireCallhod = (rblRehireCallhod.SelectedItem != null && rblRehireCallhod.SelectedValue == "Yes");
        bool isHandingOverhod = (rblHandingOverhod.SelectedItem != null && rblHandingOverhod.SelectedValue == "Yes");

        UpdateEmployeeClearance(lblEmployeeCode.Text, isNoticeServed, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(txtLeavesDuringNotice.Text), isRehireCallhod, txtRehireCallhodReason.Text, isHandingOverhod, Convert.ToInt32(txtClBlc.Text), Convert.ToInt32(txtAlBalance.Text), txtHodRemarks.Text, Session["EmployeeCode"].ToString(), chkHOD.Checked);
    }
}