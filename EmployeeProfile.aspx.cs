using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ADG.JQueryExtenders.Impromptu;
public partial class EmployeeProfile : System.Web.UI.Page
{
    DALBase objbase = new DALBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string s = Session["RegionID"].ToString();
                if (Session["employeeCode"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }
                //if (Session["RegionID"].ToString() != "800" || Session["RegionID"].ToString() != "900" || Session["RegionID"].ToString() != "700")
                //{
                //    Response.Redirect("~/login.aspx");
                //}
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("ErrorPage.aspx", false);
            }
            if (Session["EmployeeCode"] != null)
            {
                //======== Page Access Settings ========================
                DALBase objBase = new DALBase();
                DataRow row = (DataRow)Session["rightsRow"];
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = oInfo.Name;


                DataTable _dtSettings = objBase.ApplyPageAccessSettingsTable(sRet, Convert.ToInt32(row["User_Type_Id"].ToString()));
                this.Page.Title = _dtSettings.Rows[0]["PageTitle"].ToString();
                //tdFrmHeading.InnerHtml = _dtSettings.Rows[0]["PageCaption"].ToString();
                if (Convert.ToBoolean(_dtSettings.Rows[0]["isAllow"]) == false)
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    divLeavebal.Attributes.CssStyle.Add("display", "none");
                    divProfileData.Attributes.CssStyle.Add("display", "none");
                    if (!IsPostBack)
                    {
                        try
                        {
                            ViewState["SortDirection"] = "ASC";
                            //loadMonths();
                            DateTime dateTime = DateTime.UtcNow.Date;
                            txtDOJ.Text = dateTime.ToString("d");
                            txtDOB.Text = dateTime.ToString("d");
                            BindGrid();
                            BindGridResigned();
                            Reset();
                            //loadRegions();

                        }
                        catch (Exception ex)
                        {
                            Session["error"] = ex.Message;
                            Response.Redirect("ErrorPage.aspx", false);
                        }
                    }
                }

            }
            else
            {
                Response.Redirect("~/login.aspx");
            }

        }
    }
    #region 'Resigned Employees'
    protected void BindGridResigned()
    {
        try
        {

            BLLUpdateEmployeeProfile objemp = new BLLUpdateEmployeeProfile();
            DataTable dt = new DataTable();
            if (ViewState["Resigned"] == null)
            {
                objemp.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
                objemp.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
                dt = objemp.EmployeeProfileResignedSelect_ForUpdateProfileAndLeaveBalances(objemp);
                ViewState["Resigned"] = dt;
            }
            else
            {
                dt = (DataTable)ViewState["Resigned"];
            }
            gv_ResignedEmployees.DataSource = dt;
            gv_ResignedEmployees.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void LoadOldDepartment()
    {
        BLLUpdateEmployeeProfile objemp = new BLLUpdateEmployeeProfile();
        DataTable dt = objemp.EmployeeProfileDepartmentSelectAll();
        objbase.FillDropDown(dt, ddlDepartment, "DeptCode", "DeptName");

    }
    protected void btnUpdateResigned_Click(object sender, EventArgs e)
    {
        try
        {
            LoadOldDepartment();
            ImageButton btnEdit = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gv.SelectedIndex = gvr.RowIndex;
            gvr.CssClass = "tr_select";
            txtEmpCode.Text = gvr.Cells[1].Text;
            txtFName.Text = gvr.Cells[2].Text;
            txtLName.Text = gvr.Cells[3].Text;
            txtFulName.Text = gvr.Cells[4].Text;
            ddlCenter.SelectedItem.Text = gvr.Cells[8].Text;
            if (gvr.Cells[9].Text == "&nbsp;")
            {
                txtEmail.Text = "";
            }
            else
            {
                txtEmail.Text = gvr.Cells[9].Text;
            }
            ddlDepartment.SelectedValue = gvr.Cells[10].Text;
            ddlDesignation.SelectedValue = gvr.Cells[11].Text;
            if (gvr.Cells[14].Text == "&nbsp;")
            {
                txtDOJ.Text = "";
            }
            else
            {
                txtDOJ.Text = gvr.Cells[14].Text;
            }
            if (gvr.Cells[15].Text == "&nbsp;")
            {
                txtDOB.Text = "";
            }
            else
            {
                txtDOB.Text = gvr.Cells[15].Text;
            }
            trInactive.Visible = true;
            ddlInactive.Focus();
            ddlMaritalStatus.SelectedValue = gvr.Cells[16].Text.Trim();
            ddlGender.SelectedValue = gvr.Cells[17].Text.Trim();
            ddlInactive.SelectedValue = gvr.Cells[18].Text.Trim();
            ViewState["tmode"] = "UpdateResigned";
            gv.SelectedIndex = -1;
            tdRegion.Visible = false;
            txtEmpCode.ReadOnly = true; ;
            ResignedEmployees.Visible = false;
            divView.Attributes.CssStyle.Add("display", "none");
            divProfileData.Attributes.CssStyle.Add("display", "block");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    #endregion
    protected void BindGrid()
    {
        try
        {
            BLLUpdateEmployeeProfile objemp = new BLLUpdateEmployeeProfile();
            DataTable dt = new DataTable();
            objemp.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objemp.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
            dt = objemp.EmployeeProfileSelect_ForUpdateProfileAndLeaveBalances(objemp);
            if (dt.Rows.Count > 0)
            {
                if (ViewState["dtDetails"] == null)
                {
                    ViewState["dtDetails"] = dt = objemp.EmployeeProfileSelect_ForUpdateProfileAndLeaveBalances(objemp);
                }
                else
                {
                    dt = (DataTable)ViewState["dtDetails"];
                }
            }
            gv.DataSource = dt;
            gv.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnAddEmployee_Click(object sender, EventArgs e)
    {
        try
        {
            divProfileData.Visible = true;
            panDesignation.Visible = false;
            loadDepartments();
            txtEmpCode.Text = "";
            txtEmpCode.ReadOnly = false;
            ddlInactive.SelectedValue = "n";
            trInactive.Visible = false;
            tdRegion.Visible = true;
            txtFName.Text = "";
            txtLName.Text = "";
            txtFulName.Text = "";
            txtEmail.Text = "";
            txtDOJ.Text = DateTime.Now.ToString("d");
            txtDOB.Text = DateTime.Now.ToString("d");
            ddlDepartment.SelectedValue = "0";
            ddlDesignation.SelectedValue = "0";
            ddlGender.SelectedValue = "0";
            ddlMaritalStatus.SelectedValue = "0";
            ViewState["tmode"] = "Add";
            divProfileData.Attributes.CssStyle.Add("display", "block");
            divView.Attributes.CssStyle.Add("display", "none");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnAddDesignation_Click(object sender, EventArgs e)
    {
        try
        {
            txtDesignation.Text = "";
            divProfileData.Visible = false;
            divLeavebal.Visible = false;
            panDesignation.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnCancelDesignation_Click(object sender, EventArgs e)
    {
        try
        {
            panDesignation.Visible = false;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnSaveDesignation_Click(object sender, EventArgs e)
    {
        try
        {
            BLLDesignation obj = new BLLDesignation();
            obj.DesigName = txtDesignation.Text;
            int k = obj.DesignationAdd(obj);
            if (k == 1)
                ImpromptuHelper.ShowPrompt("Designation Added!");
            loadDesignation();
            btnCancelDesignation_Click(this, EventArgs.Empty);
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
            divProfileData.Attributes.CssStyle.Add("display", "none");
            divLeavebal.Attributes.CssStyle.Add("display", "none");
            divView.Attributes.CssStyle.Add("display", "block");
            ResignedEmployees.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void loadMonths()
    {
        try
        {
            BLLPeriod obj = new BLLPeriod();
            DataTable dt = new DataTable();
            //obj.InActive = "n";
            //dt = obj.PeriodFetch(obj);
            //objbase.FillDropDown(dt,ddlMonths,"PMonth","PMonthDesc");
            //ddlMonths.SelectedValue = Session["CurrentMonth"].ToString();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void Reset()
    {
        try
        {
            divProfileData.Attributes.CssStyle.Add("display", "none");
            txtEmpCode.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtFulName.Text = "";
            loadRegions();
            loadDesignation();
            ddlMaritalStatus.SelectedValue = "0";
            ddlGender.SelectedValue = "0";
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
            DataTable dt;
            BLLCenter objCenter = new BLLCenter();
            objCenter.Region_Id = Int32.Parse(ddlRegion.SelectedValue);

            dt = objCenter.CenterFetch(objCenter);
            objbase.FillDropDown(dt, ddlCenter, "Center_id", "Center_Name");

            if (Session["CenterID"] != null)
            {
                ddlCenter.SelectedValue = Session["CenterID"].ToString();
                ddlRegion.Enabled = false;
                ddlCenter.Enabled = false;
            }
            else
            {
                ddlRegion.Enabled = true;
                ddlCenter.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void loadRegions()
    {
        try
        {
            DataTable dt;
            BLLRegion objReg = new BLLRegion();
            dt = objReg.RegionFetchAddEmployee(objReg);
            objbase.FillDropDown(dt, ddlRegion, "Region_id", "Region_Name");

            if (Session["RegionID"] != null)
            {
                ddlRegion.SelectedValue = Session["RegionID"].ToString();
                loadCenters();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void loadDesignation()
    {
        try
        {
            ddlDesignation.DataSource = null;
            ddlDesignation.DataBind();
            DataTable dt;
            BLLDesignation objDes = new BLLDesignation();
            dt = objDes.DesignationFetch(objDes);
            objbase.FillDropDown(dt, ddlDesignation, "DesigCode", "DesigName");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void EMPTRANS_ACTIVEInsert()
    {
        try
        {
            int already = 0;
            BLLAddEmployee objemp = new BLLAddEmployee();
            string _reg = "";
            string _cen = "";
            string _cenCode = "";
            if (Session["LoginFrom"].ToString() == "C")
            {
                _reg = Session["RegionName"].ToString();
                _cen = Session["CenterName"].ToString();
                _cenCode = Session["CenterID"].ToString();
            }
            else if (Session["LoginFrom"].ToString() == "R")
            {
                _reg = Session["RegionName"].ToString();
                _cen = "";
                _cenCode = "";
            }
            else if (Session["LoginFrom"].ToString() == "H")
            {
                _reg = "";
                _cen = "";
                _cenCode = "";
            }
            objemp.EmployeeCode = txtEmpCode.Text;
            objemp.FirstName = txtFName.Text;
            objemp.LastName = txtLName.Text;
            objemp.Name = txtFulName.Text;
            objemp.Region = _reg;
            //objemp.Branch = _cen;
            objemp.Branch = ddlDepartment.SelectedItem.Text;
            objemp.BranchCode = _cenCode;
            objemp.MStatus = Convert.ToString(ddlMaritalStatus.SelectedItem);
            objemp.Gender = Convert.ToString(ddlGender.SelectedItem);
            objemp.Designation = Convert.ToString(ddlDesignation.SelectedItem);
            objemp.email = txtEmail.Text;
            //objemp.Pmonthdesc = ddlMonths.SelectedValue;
            if (!String.IsNullOrEmpty(txtDOB.Text))
            {
                objemp.DateOfBirth = Convert.ToDateTime(txtDOB.Text);
            }
            else
            {
                objemp.DateOfBirth = null;
            }

            if (!String.IsNullOrEmpty(txtDOJ.Text))
            {
                objemp.DateOfJoining = Convert.ToDateTime(txtDOJ.Text);

            }
            else
            {
                objemp.DateOfJoining = null;
            }
            already = objemp.EMPTRANS_ACTIVEInsert(objemp);
            if (already == 1)
            {
                drawMsgBox("Record already exists", 3);
            }
            else
            {
                //objemp.sp_empfromTemp_Trans();
                ImpromptuHelper.ShowPrompt("Record Inserted Successfully");
                ViewState["dtDetails"] = null;
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void loadDepartments()
    {
        try
        {
            BLLAttendance obj = new BLLAttendance();
            DataTable dt = new DataTable();
            obj.PMonthDesc = Session["CurrentMonth"].ToString();
            //obj.PMonthDesc = "201609";
            obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
            obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
            dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);
            objbase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");
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
            if (ViewState["tmode"].ToString() == "Add")
            {
                EMPTRANS_ACTIVEInsert();

            }
            else if (ViewState["tmode"].ToString() == "UpdateProfile" || ViewState["tmode"].ToString() == "UpdateResigned")
            {
                BLLUpdateEmployeeProfile emp = new BLLUpdateEmployeeProfile();
                string _reg = "";
                string _regCode = "";
                string _cen = "";
                string _cenCode = "";

                if (Session["LoginFrom"].ToString() == "C")
                {
                    _reg = Session["RegionName"].ToString();
                    _regCode = Session["RegionID"].ToString();
                    _cen = Session["CenterName"].ToString();
                    _cenCode = Session["CenterID"].ToString();
                }
                else if (Session["LoginFrom"].ToString() == "R")
                {
                    _reg = Session["RegionName"].ToString();
                    _regCode = Session["RegionID"].ToString();
                    _cen = "";
                    _cenCode = "";
                }
                else if (Session["LoginFrom"].ToString() == "H")
                {
                    _reg = Session["RegionName"].ToString();
                    _regCode = Session["RegionID"].ToString();
                    _cen = "";
                    _cenCode = "";
                }

                emp.EmployeeCode = txtEmpCode.Text;
                emp.FirstName = txtFName.Text;
                emp.LastName = txtLName.Text;
                emp.FullName = txtFulName.Text;
                emp.Region_Id = Convert.ToInt32(_regCode);

                emp.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
                emp.Center_Id = Convert.ToInt32(_cenCode);

                emp.MaritalSts = Convert.ToString(ddlMaritalStatus.SelectedValue);
                emp.Gender = Convert.ToString(ddlGender.SelectedValue);
                emp.DesigCode = Convert.ToInt32(ddlDesignation.SelectedValue);
                emp.Inactive = ddlInactive.SelectedValue;
                emp.Email = txtEmail.Text;

                if (!String.IsNullOrEmpty(txtDOB.Text))
                {
                    DateTime dt = Convert.ToDateTime(txtDOB.Text);
                    emp.DOB = dt.Date;
                }
                else
                {
                    emp.DOB = null;
                }
                if (!String.IsNullOrEmpty(txtDOJ.Text))
                {
                    DateTime dt = Convert.ToDateTime(txtDOJ.Text);
                    emp.DOJ = dt.Date;
                }
                else
                {
                    emp.DOJ = null;
                }
                if (!String.IsNullOrEmpty(txtResignDate.Text))
                {
                    DateTime dt = Convert.ToDateTime(txtResignDate.Text);
                    emp.ResignDate = dt.Date;
                }
                else
                {
                    emp.ResignDate = null;
                }
                emp.EmployeeProfileUpdate(emp);
                trResign.Visible = false;

            }
            else if (ViewState["tmode"].ToString() == "UpdateLeave")
            {
                BLLEmployeeLeaveBalance objlv = new BLLEmployeeLeaveBalance();

                objlv.EmployeeCode = txtEmpCode.Text;
                objlv.AnnulaLeave = Convert.ToInt32(txtannual.Text);
                objlv.CasualLeave = Convert.ToDecimal(txtcasual.Text);
                objlv.EmployeeLeaveBalanceUpdate(objlv);
            }
            ViewState["Resigned"] = null;
            ViewState["dtDetails"] = null;
            BindGrid();
            BindGridResigned();
            btnCancel_Click(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void ddlresigned_click(object sender, EventArgs e)
    {
        try
        {
            if (ddlInactive.SelectedValue == "y" && ViewState["tmode"].ToString() == "UpdateProfile")
            {
                txtResignDate.Text = "";
                trResign.Visible = true;

            }
            if (ddlInactive.SelectedValue == "n" && ViewState["tmode"].ToString() == "UpdateProfile")
            {
                txtResignDate.Text = "";
                trResign.Visible = false;

            }
            else
            {
                trResign.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
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
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadCenters();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            loadDepartments();
            ImageButton btnEdit = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gv.SelectedIndex = gvr.RowIndex;
            gvr.CssClass = "tr_select";
            txtEmpCode.Text = gvr.Cells[1].Text;
            txtFName.Text = gvr.Cells[2].Text;
            txtLName.Text = gvr.Cells[3].Text;
            txtFulName.Text = gvr.Cells[4].Text;
            ddlCenter.SelectedItem.Text = gvr.Cells[8].Text;
            if (gvr.Cells[9].Text == "&nbsp;")
            {
                txtEmail.Text = "";
            }
            else
            {
                txtEmail.Text = gvr.Cells[9].Text;
            }
            ddlDepartment.SelectedValue = gvr.Cells[10].Text;
            ddlDesignation.SelectedValue = gvr.Cells[11].Text;
            if (gvr.Cells[14].Text == "&nbsp;")
            {
                txtDOJ.Text = "";
            }
            else
            {
                txtDOJ.Text = gvr.Cells[14].Text;
            }
            if (gvr.Cells[15].Text == "&nbsp;")
            {
                txtDOB.Text = "";
            }
            else
            {
                txtDOB.Text = gvr.Cells[15].Text;
            }
            ddlMaritalStatus.SelectedValue = gvr.Cells[16].Text.Trim();
            ddlGender.SelectedValue = gvr.Cells[17].Text.Trim();
            ddlInactive.SelectedValue = gvr.Cells[18].Text.Trim();
            ViewState["tmode"] = "UpdateProfile";
            txtFName.Focus();
            gv_ResignedEmployees.SelectedIndex = -1;
            tdRegion.Visible = false;
            txtEmpCode.ReadOnly = true; ;
            ResignedEmployees.Visible = false;
            trInactive.Visible = true;
            divView.Attributes.CssStyle.Add("display", "none");
            divLeavebal.Attributes.CssStyle.Add("display", "none");
            divProfileData.Attributes.CssStyle.Add("display", "block");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void btnUpdateLeave_Click(object sender, EventArgs e)
    {
        try
        {
            ResignedEmployees.Visible = false;
            divView.Attributes.CssStyle.Add("display", "block");
            ImageButton btnEdit = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gv.SelectedIndex = gvr.RowIndex;
            txtEmpCode.Text = gvr.Cells[1].Text;
            txtcasual.Text = gvr.Cells[19].Text;
            txtannual.Text = gvr.Cells[20].Text;
            txtRemarks.Text = "";
            divLeavebal.Attributes.CssStyle.Add("display", "block");
            txtRemarks.Focus();
            ViewState["tmode"] = "UpdateLeave";
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
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
            BindGrid();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv.PageIndex = e.NewPageIndex;
            BindGrid();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }

    protected void gv_ResignedEmployees_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["Resigned"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            BindGridResigned();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
    protected void gv_ResignedEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_ResignedEmployees.PageIndex = e.NewPageIndex;
            BindGridResigned();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }
}
