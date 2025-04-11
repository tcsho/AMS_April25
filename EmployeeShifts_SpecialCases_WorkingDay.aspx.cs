using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;

public partial class EmployeeShifts_SpecialCases_WorkingDay : System.Web.UI.Page
{
    BLLEmployeeShifts_SpecialCases_WorkingDay objbll = new BLLEmployeeShifts_SpecialCases_WorkingDay();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                loadRegions();
                if (Session["CenterID"]!=null &&!String.IsNullOrEmpty(Session["CenterID"].ToString()) && Convert.ToInt32(Session["CenterID"].ToString())>0)
                {
                    cbApplyCenter.Visible = false;
                    
                }
                ViewState["SortDirection"] = "DESC";
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }

    }
    public void loadRegions()
    {
        try
        {
            BLLVacationTimigs objBll = new BLLVacationTimigs();
            DataTable _dt = new DataTable();
            _dt = objBll.fetchRegions();
            ddlRegion.DataTextField = "Region_Name";
            ddlRegion.DataValueField = "Region_Id";
            ddlRegion.DataSource = _dt;
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, new ListItem("Head Office", "0"));
            ddlRegion.SelectedValue = Session["RegionID"].ToString();
            if (Convert.ToInt32(ddlRegion.SelectedValue) == 0)
                ddlRegion.Enabled = true;
            else
                ddlRegion.Enabled = false;
             
            ddlRegion_SelectedIndexChanged(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }

    }
    protected void loadCenters()
    {
        try
        {
            BLLVacationTimigs objBll = new BLLVacationTimigs();
            DataTable _dt = new DataTable();
            objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);
            _dt = objBll.fetchCenters(objBll);
            ddlCenter.DataTextField = "Center_Name";
            ddlCenter.DataValueField = "Center_ID";
            ddlCenter.DataSource = _dt;
            ddlCenter.DataBind();
            if (ddlRegion.SelectedValue == "0")
            {
                ddlCenter.Items.Insert(0, new ListItem("Head Office", "0"));
            }
            else
            {
                ddlCenter.Items.Insert(0, new ListItem("Regional Office", "0"));
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }

    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadCenters();
            ddlCenter_SelectedIndexChanged(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ViewState["dtDetails"] = null;
            BindgvEmployees();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void BindgvEmployees()
    {
        try
        {
            DataTable dt = new DataTable();
            objbll.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
            objbll.Center_Id = Convert.ToInt32(ddlCenter.SelectedValue);
            if (ViewState["dtDetails"] == null)
            {
                dt = objbll.EmployeeShifts_SpecialCases_WorkingDaySelectAll(objbll);
                ViewState["dtDetails"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["dtDetails"];
            }
            gvEmployees.DataSource = dt;
            gvEmployees.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }

    protected void btnAddShift_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCenter.SelectedIndex > -1)
            {
                txtRemarks.Text = "";
                ViewState["Mode"] = "Add";
                txtDate.Text = "";
                ddlGender.SelectedValue = "0";
                cb_GenderSpecific.Checked = false;
                divGender.Visible = false;
                pAddNew.Visible = true;
                btnAddShift.Visible = false;
                pEmployee.Visible = false;
                cbApplyCenter.Checked = false;
            }
            else
            {
                ImpromptuHelper.ShowPrompt("Please Select a Center");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pAddNew.Visible = false;
            btnAddShift.Visible = true;
            pEmployee.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(txtDate.Text);
            objbll.WorkingDate = dt;
            objbll.Remarks = txtRemarks.Text;
            objbll.isApplyCenters = cbApplyCenter.Checked;
            if (cb_GenderSpecific.Checked)
            {
                objbll.Gender = ddlGender.SelectedValue;
            }

            int k = -1;
            if (ViewState["Mode"].ToString() == "Add")
            {
                objbll.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
                objbll.Center_Id = Convert.ToInt32(ddlCenter.SelectedValue);
                objbll.CreatedBy = Convert.ToInt32(Session["User_Id"].ToString());
               
                k = objbll.EmployeeShifts_SpecialCases_WorkingDayAdd(objbll);
            }
            else
            {
                objbll.WorkingDay_Id = Convert.ToInt32(ViewState["WorkingDay_Id"].ToString());
                k = objbll.EmployeeShifts_SpecialCases_WorkingDayUpdate(objbll);
            }
            if (k == 0)
            {
                ImpromptuHelper.ShowPrompt("Record Updated");
            }
            else
            {
                ImpromptuHelper.ShowPrompt("The date was not in the current month");
            }
            ViewState["dtDetails"] = null;
            BindgvEmployees();
            btnCancel_Click(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            pAddNew.Visible = true;
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvEmployees.SelectedIndex = gvr.RowIndex;
            txtDate.Text = gvr.Cells[3].Text;
            if (gvr.Cells[4].Text == "B" || gvr.Cells[4].Text == "Both")
            {
                cb_GenderSpecific.Checked = false;
                divGender.Visible = false;
            }
            else
            {
                ddlGender.SelectedValue = gvr.Cells[4].Text;
                divGender.Visible = true;
                cb_GenderSpecific.Checked = true;
            }
            txtRemarks.Text = gvr.Cells[5].Text;
            ViewState["WorkingDay_Id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["Mode"] = "Edit";
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            objbll.WorkingDay_Id = Convert.ToInt32(btnDelete.CommandArgument);
            GridViewRow gvr = (GridViewRow)btnDelete.NamingContainer;
            gvEmployees.SelectedIndex = gvr.RowIndex;
            DateTime dt = Convert.ToDateTime(gvr.Cells[3].Text);
            objbll.WorkingDate = dt.Date;
            int k = objbll.EmployeeShifts_SpecialCases_WorkingDayDelete(objbll);
            if (k == 0)
            {
                ViewState["dtDetails"] = null;
                BindgvEmployees();
            }
            else
            {
                ImpromptuHelper.ShowPrompt("You cannot delete record from previous month");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }

    }
    protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        try
        {
            gvEmployees.PageIndex = e.NewPageIndex;
            BindgvEmployees();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_Sorting(object sender, GridViewSortEventArgs e)
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
            BindgvEmployees();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void cb_GenderSpecific_CheckedChanged(object sender, EventArgs e)
    {
        if (cb_GenderSpecific.Checked)
        {
            divGender.Visible = true;
            ddlGender.SelectedValue = "0";
        }
        else
        {
            divGender.Visible = false;
        }
    }
}