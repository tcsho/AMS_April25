using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class encashconfirm : System.Web.UI.Page
{
    int EmployeeCode;
    DateTime fromdate;
    DateTime todate;
    int noofdays;

    // Connection string to your database (update with your actual connection string) 
    private string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        string empl_Id = Request["el_Id"];
        string empll_Id = Request["ell_Id"];

        if (empl_Id == null)
        {
            byte[] bd = Convert.FromBase64String(empll_Id);
            string decrypted_empl_Id = System.Text.ASCIIEncoding.ASCII.GetString(bd);
            UpdateBODReject(Convert.ToInt32(decrypted_empl_Id));
            getLvRecord(Convert.ToInt32(decrypted_empl_Id));
        }
        else
        {
            byte[] bd = Convert.FromBase64String(empl_Id);
            string decrypted_empl_Id = System.Text.ASCIIEncoding.ASCII.GetString(bd);
            UpdateBODApprove(Convert.ToInt32(decrypted_empl_Id));
            getLvRecord(Convert.ToInt32(decrypted_empl_Id));
            EmployeeLeaveEncashSendToERP();
        } 
    }
    private void getLvRecord(int id)
    {
        string selectQuery = "SELECT * FROM EmployeeLeaves WHERE EmpLeave_Id = " + id;

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
                    if (reader.Read())
                    {
                        // Access the data (replace "ColumnName" with actual column names)
                        EmployeeCode = Convert.ToInt32(reader["EmployeeCode"]);
                        fromdate = Convert.ToDateTime(reader["LeaveFrom"]);
                        todate = Convert.ToDateTime(reader["LeaveTo"]);
                        noofdays = Convert.ToInt32(reader["LeaveDays"]);
                    }
                    else
                    {
                        //Console.WriteLine("No record found with the specified primary key.");
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    private void UpdateBODReject(int hfEmpLeaveId)
    {
        // SQL UPDATE query
        string updateQuery = "UPDATE EmployeeLeaves SET EncashmentBodApproval = 2 WHERE EmpLeave_Id = " + hfEmpLeaveId;

        // Create a connection to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a command to execute the query
            SqlCommand command = new SqlCommand(updateQuery, connection);

            // Add parameters to avoid SQL injection
            command.Parameters.AddWithValue("@EncashmentBodApproval", 2);
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
                    msg.InnerHtml = "Dear BoD Member,<br><br>you have rejected the leave encashment request.";
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
    private void UpdateBODApprove(int hfEmpLeaveId)
    {
        // SQL UPDATE query
        string updateQuery = "UPDATE EmployeeLeaves SET EncashmentBodApproval = 1 WHERE EmpLeave_Id = " + hfEmpLeaveId;

        // Create a connection to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Create a command to execute the query
            SqlCommand command = new SqlCommand(updateQuery, connection);

            // Add parameters to avoid SQL injection
            command.Parameters.AddWithValue("@EncashmentBodApproval", 1);
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
                    msg.InnerHtml = "Dear BoD Member,<br><br>Thanks for approving the leave encashment request.";
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
    private void EmployeeLeaveEncashSendToERP()
    {
        string conn = ConfigurationManager.ConnectionStrings["ERPDB"].ConnectionString;

        using (OracleConnection cnx = new OracleConnection(conn))
        {
            OracleCommand commProc = new OracleCommand
            {
                Connection = cnx,
                CommandText = "APPS.CUST_LEAVES_PRO",
                CommandType = System.Data.CommandType.StoredProcedure
            };

            // Add parameters with the correct values and types
            commProc.Parameters.Add(new OracleParameter("EMP_NO", OracleDbType.Int32)
            {
                Value = EmployeeCode
            });

            commProc.Parameters.Add(new OracleParameter("P_START", OracleDbType.Date)
            {
                Value = fromdate
            });

            commProc.Parameters.Add(new OracleParameter("P_END", OracleDbType.Date)  // Changed p_end to P_END to match the expected parameter
            {
                Value = todate
            });

            commProc.Parameters.Add(new OracleParameter("P_NO_DAY", OracleDbType.Int32)
            {
                Value = noofdays
            });

            try
            {
                // Open the connection
                cnx.Open();

                // Execute the stored procedure
                commProc.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as necessary
                // For example, logging the error message
                //Console.WriteLine($"Error executing stored procedure: {ex.Message}");

                // Rethrow the exception with the original stack trace
                throw;
            }
        }
    }
}