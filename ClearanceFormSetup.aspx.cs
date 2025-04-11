using ADG.JQueryExtenders.Impromptu;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


public partial class ClearanceFormSetup : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["tcs_invConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/login.aspx");
        }

        if (!IsPostBack)
        {
            // Add items programmatically
            ddlDepartments.Items.Add(new ListItem("Select Department", "0")); // Add a default item
            ddlDepartments.Items.Add(new ListItem("Accounts", "accounts"));
            ddlDepartments.Items.Add(new ListItem("IT", "it"));
            ddlDepartments.Items.Add(new ListItem("Human Resource", "hr"));
            ddlDepartments.Items.Add(new ListItem("Library/TRC", "lib"));
            ddlDepartments.Items.Add(new ListItem("Admin (Cafeteria, Company Car, Guest House)", "admin"));
            ddlDepartments.Items.Add(new ListItem("Laboratory", "laboratory"));
            ddlDepartments.Items.Add(new ListItem("Training", "training"));
            //ddlDepartments.Items.Add(new ListItem(" Head of Department", "hod"));

            BindEmployeeDropdown(Convert.ToInt32(Session["RegionID"]), Convert.ToInt32(Session["CenterID"]));
            BindGridView(Convert.ToInt32(Session["RegionID"]), Convert.ToInt32(Session["CenterID"]));
        }
    }
    private void BindEmployeeDropdown(int regionId, int centerId)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetClearenceFormSetupEmployees", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionId", regionId);
                cmd.Parameters.AddWithValue("@CenterId", centerId);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    ddlEmployee.Items.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        ddlEmployee.DataSource = dt;
                        ddlEmployee.DataTextField = "EmployeeInfo"; // Ensure this is correct
                        ddlEmployee.DataValueField = "EmployeeCode"; // Ensure this is correct  
                        ddlEmployee.DataBind();
                    }
                    else
                    {
                        // Optionally, add a message for no employees found
                        ddlEmployee.Items.Add(new ListItem("No employees found", "0"));
                    }
                }
            }
        }
    }
    private void BindGridView(int regionId, int centerId)
    {
        // Clear previous data
        gvEmpInfoDptWise.DataSource = null;
        gvEmpInfoDptWise.DataBind();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetClearenceFormSetupListing", conn))
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
                        gvEmpInfoDptWise.DataSource = dt;
                        gvEmpInfoDptWise.DataBind();
                    }
                    else
                    {
                        // No data, clear the previous data binding and set EmptyDataText
                        gvEmpInfoDptWise.DataSource = null;
                        gvEmpInfoDptWise.DataBind();
                    }
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlDepartments.SelectedItem.Value == "0")
        {
            drawMsgBox("Please select the department.", 1);
            return;
        }
        if (ddlEmployee.SelectedItem.Value == "0")
        {
            drawMsgBox("Please select an employee.", 1);
            return;
        }
        
        InsertEmployeeClearenceRight(ddlDepartments.SelectedItem.Text,ddlDepartments.SelectedItem.Value,ddlEmployee.SelectedItem.Value,1);
    }
    public void InsertEmployeeClearenceRight(string department, string departmentValue, string employeeCode, int statusId)
    {
        string insertQuery = @"
            INSERT INTO EmployeeClearenceRights (Department, DepartmentValue, EmployeeCode, Status_Id)
            VALUES (@Department, @DepartmentValue, @EmployeeCode, @Status_Id)";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Department", department);
            command.Parameters.AddWithValue("@DepartmentValue", departmentValue);
            command.Parameters.AddWithValue("@EmployeeCode", employeeCode);
            command.Parameters.AddWithValue("@Status_Id", statusId);

            try
            {
                connection.Open();
                command.ExecuteNonQuery(); // Execute the insert command
                drawMsgBox("Record inserted successfully.", 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while inserting: " + ex.Message);
            }
        }
    }
    protected void gvEmpInfoDptWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            // Get the row index of the clicked delete button
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvEmpInfoDptWise.DataKeys[index].Value);

            // Perform the delete operation (use your actual delete logic here)
            DeleteEmployeeClearenceRight(id);

            // Rebind the GridView to reflect the changes
            BindGridView(Convert.ToInt32(Session["RegionID"]), Convert.ToInt32(Session["CenterID"]));
        }
    }
    // Update Operation
    public void DeleteEmployeeClearenceRight(int id)
    {
        string updateQuery = @"
            UPDATE EmployeeClearenceRights
            SET Status_Id = 2
            WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Status_Id", 2);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery(); // Execute the update command
                if (rowsAffected > 0)
                {
                    drawMsgBox("Record deleted successfully.", 1);                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating: " + ex.Message);
            }
        }
    }
    //protected void gvEmpInfoDptWise_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Delete")
    //    {
    //        // Get the row index of the record to delete
    //        int index = Convert.ToInt32(e.CommandArgument);

    //        // Get the 'Id' value of the record (assuming 'Id' is the primary key)
    //        GridViewRow row = gvEmpInfoDptWise.Rows[index];
    //        string idToDelete = gvEmpInfoDptWise.DataKeys[row.RowIndex].Value.ToString();

    //        // Perform the delete operation (assuming you have a method to delete the record)
    //        DeleteEmployeeClearenceRight(Convert.ToInt32(idToDelete)); 
    //    }
    //}
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