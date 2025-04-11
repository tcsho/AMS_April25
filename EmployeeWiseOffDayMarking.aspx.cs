using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;
using System.Globalization;

public partial class EmployeeWiseOffDayMarking : System.Web.UI.Page
{
    int UserLevel, UserType;
    DALBase objBase = new DALBase();
    BLLEmployeeWiseOffDays objEmpOff = new BLLEmployeeWiseOffDays();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());


            if (Session["EmployeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
        }
        catch (Exception)
        {
        }
        if (!IsPostBack)
        {
            ////======== Page Access Settings ========================
            //DALBase objBase = new DALBase();
            //DataRow row = (DataRow)Session["rightsRow"];
            //string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            //System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            //string sRet = oInfo.Name;


            //DataTable _dtSettings = objBase.ApplyPageAccessSettingsTable(sRet, Convert.ToInt32(row["User_Type_Id"].ToString()));
            //this.Page.Title = _dtSettings.Rows[0]["PageTitle"].ToString();
            ////tdFrmHeading.InnerHtml = _dtSettings.Rows[0]["PageCaption"].ToString();
            //if (Convert.ToBoolean(_dtSettings.Rows[0]["isAllow"]) == false)
            //{
            //    Session.Abandon();
            //    Response.Redirect("~/login.aspx");
            //}

            //====== End Page Access settings ======================


            ViewState["Emp"] = null;
            ViewState["SortDirection"] = "ASC";
            ViewState["tMoodMIO"] = "check";
            ViewState["mode"] = "Add";
            div_WeekDays.Visible = false;
            ResetAllpanels(true);
            LoadEmployeeOffDays();
            //loadDepartments();
            LoadEmpApply();
        }
    }
    protected void ResetAllpanels(bool togle)

    {
        ShowEmployee.Visible = togle;
        ShorForm.Visible = !(togle);
        ShorForm1.Visible = !(togle);
        ShorForm2.Visible = !(togle);
    }
    protected void LoadEmployeeOffDays()
    {
        gvEmployeeOff.DataSource = null;
        gvEmployeeOff.DataBind();
        if (UserLevel == 4)
        {
            objEmpOff.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objEmpOff.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
        }
        else if (UserLevel == 3)
        {
            objEmpOff.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objEmpOff.Center_Id = 0;
        }
        else if (UserLevel == 1 || UserLevel == 2)
        {
            objEmpOff.Region_Id = 0;
            objEmpOff.Center_Id = 0;
        }
        DataTable dt = objEmpOff.EmployeeWiseOffDaysFetch(objEmpOff);

        if (dt.Rows.Count > 0)
        {
            gvEmployeeOff.DataSource = dt;
            gvEmployeeOff.DataBind();
        }


    }
    protected void LoadEmpApply()
    {
        DataTable EmpTable = new DataTable();
        EmpTable.Columns.Add("EmployeeCode", typeof(string));
        EmpTable.Columns.Add("FullName", typeof(string));
        EmpTable.Columns.Add("Department", typeof(string));
        EmpTable.Columns.Add("DesigName", typeof(string));
        ViewState["Emp"] = (DataTable)EmpTable;
    }

    protected void rbLstRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbLstOpt.SelectedValue == "0")
        {
            div_WeekDays.Visible = false;
        }
        else if (rbLstOpt.SelectedValue == "1")
        {
            div_WeekDays.Visible = true;
        }
    }

    protected void loadDepartments()
    {

        BLLAttendance obj = new BLLAttendance();

        DataTable dt = new DataTable();

        obj.PMonthDesc = Session["CurrentMonth"].ToString();

        obj.User_Id = Convert.ToInt32(Session["User_Id"].ToString().Trim());
        obj.UserTypeId = Convert.ToInt32(Session["UserType"].ToString());
        dt = obj.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(obj);
        objBase.FillDropDown(dt, ddlDepartment, "Deptcode", "DeptName");
        ddlDepartment.SelectedValue = "0";
        ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);
    }


    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDepartment.SelectedIndex > 0)
        {
            loadEmployees();
        }

    }
    protected void loadEmployees()
    {

        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

        DataTable dt = new DataTable();

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


        dt = obj.EmployeeprofileSelectByRegionCenterDeptViewonly(obj);
        gvEmployee.DataSource = dt;
        gvEmployee.DataBind();
        //objBase.FillDropDown(dt, ddlEmployeecode, "employeecode", "CodeName");
    }

    protected void but_Add_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)(ViewState["Emp"]);
        foreach (GridViewRow row in gvEmployee.Rows)
        {
            Control ctrl = row.FindControl("cbSelectEmployee");
            CheckBox cb = (CheckBox)ctrl;

            if (cb.Checked == true)
            {

                DataRow[] result = dt.Select("EmployeeCode=" + row.Cells[1].Text);
                if (result.Length < 1)
                {
                    dt.Rows.Add(row.Cells[1].Text, row.Cells[2].Text, ddlDepartment.SelectedItem.Text, row.Cells[3].Text);
                }
            }
            ViewState["Emp"] = dt;
        }
        gv_EmpApply.DataSource = (DataTable)ViewState["Emp"];
        gv_EmpApply.DataBind();

    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string repStr = "";
        string Employeecode;
        ImageButton btn = (ImageButton)sender;
        Employeecode = (btn.CommandArgument);
        DataTable dt = (DataTable)ViewState["Emp"];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            if (dr["EmployeeCode"].ToString().Trim() == Employeecode.Trim())
                dr.Delete();
        }

        ViewState["Emp"] = dt;
        gv_EmpApply.DataSource = dt;
        gv_EmpApply.DataBind();


    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string repStr = "";
        string EWO_Id;
        ImageButton btn = (ImageButton)sender;
        EWO_Id = (btn.CommandArgument);
        objEmpOff.EWO_Id = Convert.ToInt32(EWO_Id);
        objEmpOff.EmployeeWiseOffDaysDelete(objEmpOff);
        LoadEmployeeOffDays();

        //DataTable dt = (DataTable)ViewState["Emp"];
        //for (int i =0 ;  i<dt.Rows.Count ; i++)
        //{
        //    DataRow dr = dt.Rows[i];
        //    if (dr["EmployeeCode"].ToString().Trim() == Employeecode.Trim())
        //        dr.Delete();
        //}

        //ViewState["Emp"] = dt;
        //gv_EmpApply.DataSource = dt;
        //gv_EmpApply.DataBind();


    }


    protected void gvMIO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMoodMIO"].ToString();

                foreach (GridViewRow gvr in gvEmployee.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("cbSelectEmployee");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMoodMIO"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMoodMIO"] = "check";
                    }

                }

            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btn_apply_Click(object sender, EventArgs e)
    {

        CheckBox cb = null;
        int AlreadyIn = 0;
        List<BLLEmployeeWiseOffDays> listofobjEmpOff = new List<BLLEmployeeWiseOffDays>();
        try
        {
            if (gv_EmpApply.Rows.Count < 1)
            {
                ImpromptuHelper.ShowPrompt("Not Saved! Please add employee(s) in selected employee(s) list before apply.");
                return;
            }
            if (rbLstOpt.SelectedValue == "0")
            {


                foreach (GridViewRow gvr in gv_EmpApply.Rows)
                {
                    BLLEmployeeWiseOffDays empWiseOffDay = new BLLEmployeeWiseOffDays();
                    empWiseOffDay.Employeecode = gvr.Cells[1].Text.TrimEnd();
                    empWiseOffDay.FromDate = DateTime.ParseExact(txtFromDate.Text, "MM/dd/yyyy", CultureInfo.DefaultThreadCurrentCulture);
                    //empWiseOffDay.FromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
                    //empWiseOffDay.ToDate = Convert.ToDateTime(txtToDate.Text.ToString());
                    empWiseOffDay.ToDate = DateTime.ParseExact(txtToDate.Text, "MM/dd/yyyy", CultureInfo.DefaultThreadCurrentCulture);

                    empWiseOffDay.Reason = txtOffDaysReason.Text;
                    empWiseOffDay.SunOff = true;
                    empWiseOffDay.MonOff = true;
                    empWiseOffDay.TueOff = true;
                    empWiseOffDay.WedOff = true;
                    empWiseOffDay.ThuOff = true;
                    empWiseOffDay.FriOff = true;
                    empWiseOffDay.SatOff = true;
                    empWiseOffDay.Status_Id = 1;
                    empWiseOffDay.CreatedBy = Convert.ToInt32(Session["User_Id"].ToString());
                    empWiseOffDay.CreatedDate = DateTime.Now;
                    //empWiseOffDay.UpdatedDate = DateTime.Now;
                    listofobjEmpOff.Add(empWiseOffDay);
                    //AlreadyIn = objEmpOff.EmployeeWiseOffDaysAdd(objEmpOff);
                }

                foreach (var item in listofobjEmpOff)
                {
                    AlreadyIn = objEmpOff.EmployeeWiseOffDaysAdd(item);
                }

            }
            else if (rbLstOpt.SelectedValue == "1")
            {
                foreach (GridViewRow gvr in gv_EmpApply.Rows)
                {
                    objEmpOff.Employeecode = gvr.Cells[1].Text;
                    objEmpOff.FromDate = Convert.ToDateTime(txtFromDate.Text);
                    objEmpOff.ToDate = Convert.ToDateTime(txtToDate.Text);
                    objEmpOff.Reason = txtOffDaysReason.Text;
                    if (cblWeekdays.Items[0].Selected == true)
                    {
                        objEmpOff.SunOff = true;
                    }
                    else
                    {
                        objEmpOff.SunOff = false;
                    }
                    if (cblWeekdays.Items[1].Selected == true)
                    {
                        objEmpOff.MonOff = true;
                    }
                    else
                    {
                        objEmpOff.MonOff = false;

                    }
                    if (cblWeekdays.Items[2].Selected == true)
                    {
                        objEmpOff.TueOff = true;
                    }
                    else
                    {
                        objEmpOff.TueOff = false;
                    }
                    if (cblWeekdays.Items[3].Selected == true)
                    {
                        objEmpOff.WedOff = true;
                    }
                    else
                    {
                        objEmpOff.WedOff = false;
                    }
                    if (cblWeekdays.Items[4].Selected == true)
                    {
                        objEmpOff.ThuOff = true;
                    }
                    else
                    {
                        objEmpOff.ThuOff = false;
                    }
                    if (cblWeekdays.Items[5].Selected == true)
                    {
                        objEmpOff.FriOff = true;
                    }
                    else
                    {
                        objEmpOff.FriOff = false;
                    }
                    if (cblWeekdays.Items[6].Selected == true)
                    {
                        objEmpOff.SatOff = true;
                    }
                    else
                    {
                        objEmpOff.SatOff = false;
                    }
                    objEmpOff.Status_Id = 1;
                    objEmpOff.CreatedBy = Convert.ToInt32(Session["User_Id"].ToString());
                    objEmpOff.CreatedDate = DateTime.Now;
                    AlreadyIn = objEmpOff.EmployeeWiseOffDaysAdd(objEmpOff);
                }
            }
            LoadEmployeeOffDays();
            ViewState["Emp"] = null;
            LoadEmpApply();
            ResetAllpanels(true);
        }
        catch (Exception ex)
        {

            throw ex;
        }


    }



    protected void btnSingleEmpProcess_Click(object sender, EventArgs e)
    {
        ResetAllpanels(false);
        ViewState["Emp"] = null;
        gv_EmpApply.DataSource = null;
        gv_EmpApply.DataBind();

        loadDepartments();

        //ddlDepartment.SelectedValue = "0";
        //ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);
        //gvEmployee.DataSource = null;
        //gvEmployee.DataBind();
        LoadEmpApply();

    }
    protected void but_cancel_Click(object sender, EventArgs e)
    {
        ResetAllpanels(true);
    }
}