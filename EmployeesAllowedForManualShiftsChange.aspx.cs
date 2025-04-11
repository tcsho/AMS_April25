using System;
using System.Data;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;
 

public partial class EmployeesAllowedForManualShiftsChange : System.Web.UI.Page
{
    BLLEmployeeOnManualShiftDetail obj = new BLLEmployeeOnManualShiftDetail();
    DALBase objBase = new DALBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
           
              
            ViewState["SortDirection"] = "ASC";
            EmployeeShiftDetail.Visible = false;
            String s = Session["RegionID"].ToString();
            if(string.IsNullOrEmpty( Session["RegionID"].ToString()) && string.IsNullOrEmpty( Session["CenterID"].ToString()))
            {
                Session["RegionID"] = 0;
                Session["CenterID"] = 0;
            }
            else if (string.IsNullOrEmpty(Session["CenterID"].ToString() ) &&  Session["RegionID"].ToString()!=null)
            {
                Session["RegionID"] = Session["RegionID"].ToString();
                Session["CenterID"] = 0;
            }
            
             
               BindMainGrid();
                 
           //lblregion.Text= Session["RegionName"].ToString();
           //lblcenter.Text=Session["CenterName"].ToString();
              //  Session["EmployeeCode"] 
        }
    }
    private void BindMainGrid()
    {
        
        gvSubordinates.DataSource = null;
        gvSubordinates.DataBind();
        long region = Convert.ToInt64(Session["RegionID"]);
        long center = Convert.ToInt64(Session["CenterID"]);
        try
        {
            DataTable dtsub = new DataTable();
            
            if (ViewState["dtDetails"] == null)
            {
                dtsub = (DataTable)obj.EmployeeprofileSelectByRegionCenter(region,center);
                ViewState["dtDetails"] = dtsub;
            }
            else
            {
                dtsub = (DataTable)ViewState["dtDetails"];
            }

            if (dtsub.Rows.Count > 0)
            {
                if (region == 0 && center == 0)
                {
                    gvSubordinates.Columns[6].Visible = false;
                    gvSubordinates.Columns[4].Visible = false;
                }
                if (region != 0 && center == 0)
                {
                     gvSubordinates.Columns[4].Visible = false;
                    gvSubordinates.Columns[6].Visible = false;
                }
                else if (region != 0 && center != 0)
                {
                    gvSubordinates.Columns[6].Visible = true;
                    gvSubordinates.Columns[4].Visible = false;
                }
                gvSubordinates.DataSource = dtsub;
                gvSubordinates.DataBind();


            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }

    }
    protected void gvSubordinates_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

           obj.Employeecode = gvSubordinates.Rows[e.RowIndex].Cells[0].Text;
           int k =obj.EmployeeOnManualShiftDetailDelete(obj)  ;
           ViewState["dtDetails"] = null;
           BindMainGrid();   

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }
    protected void gvSubordinates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("ImageButton2");
            ib.Visible = false;
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?') ");
        }
    }

    protected void gvSubordinates_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["dtDetails"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
           BindMainGrid();

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }
    protected void gvSubordinates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSubordinates.PageIndex = e.NewPageIndex;
            BindMainGrid();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }
    protected void btnSaveHOD_Click(object sender, EventArgs e)
    {
        if ( ddlDepartment.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0 && !string.IsNullOrWhiteSpace(txtreason.Text))
        {
            if (txtreason.Text.Length > 500 )
            {
                ImpromptuHelper.ShowPrompt("Please enter a reason within the range of 500 characters");
            }
            else
            {
                obj.Employeecode = ddlEmployee.SelectedValue.ToString();
                obj.Reason = txtreason.Text;
                obj.CreatedBy = Convert.ToInt32(Session["User_Id"].ToString());
                int k = obj.EmployeeOnManualShiftDetailAdd(obj);
                if (k == 0)
                {
                    ImpromptuHelper.ShowPrompt("Employee Added!");
                    ViewState["dtDetails"] = null;
                    EmployeeShiftDetail.Visible = false;
                }
                else
                {
                    ImpromptuHelper.ShowPrompt("Employee Already Exists in the list below!");
                }
            }
        }
        else if (ddlDepartment.SelectedItem.Text == "Select")
        {
            ImpromptuHelper.ShowPrompt("Please Select a Department!");
        }
        else if (ddlEmployee.SelectedIndex == -1 || ddlEmployee.SelectedItem.Text == "Select")
        {
            ImpromptuHelper.ShowPrompt("Please Select an Employee!");
        }
        else if (string.IsNullOrWhiteSpace(txtreason.Text))
        {
            ImpromptuHelper.ShowPrompt("Please add a reason!");
        }
        BindMainGrid();
        
    }
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
    protected void btnAddHOD_Click(object sender, EventArgs e)
    {
        EmployeeShiftDetail.Visible = true;
        txtreason.Text = "";
         loadMonths();
        BindDepartment();
        ddlDepartment.SelectedIndex=0;
        ddlEmployee.Items.Clear();
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EmployeeShiftDetail.Visible = false;
    }
    protected void BindDepartment()
    {
        BLLEmployeeReportToHOD objBll = new BLLEmployeeReportToHOD();

        DataTable dt = new DataTable();
        objBll.PMonth =  ddlMonths.SelectedValue;
        objBll.regionId = Convert.ToInt32(Session["RegionID"]);
        objBll.centerId = Convert.ToInt32(Session["CenterID"]);
        

        dt = objBll.WebDepartmentSelectByMonthRegionCenter(objBll);
        DataTable temp =(DataTable)ViewState["dtDetails"];

        //check in dtdetails if person exists

        objBase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bind employee drop down
        loadEmployees();
        ddlEmployee.SelectedIndex =0 ;
        

    }
    protected void loadEmployees()
    {
        
        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

        DataTable dt = new DataTable();
        int UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        if (UserLevel == 4)
        {
            obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
            obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
        }
        else if (UserLevel == 3)
        {
            obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
            obj.Center_id = 0;
        }
        else if (UserLevel == 1 || UserLevel == 2)
        {
            obj.Region_id = 0;
            obj.Center_id = 0;
        }

        obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);


        dt = obj.EmployeeprofileSelectByRegionCenterDept(obj);
        objBase.FillDropDown(dt, ddlEmployee, "employeecode", "CodeName");
    }
}