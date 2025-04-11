using System;
using System.Data;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;

public partial class EmployeeReportTo : System.Web.UI.Page
{
    int UserLevel, UserType;


    BLLEmployeeReportTo objBll = new BLLEmployeeReportTo();
    DALBase objBase = new DALBase();
    DataTable dtHOD = new DataTable();
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

            //====== End Page Access settings ======================

            pan_New.Attributes.CssStyle.Add("display", "none");

            ViewState["SortDirection"] = "ASC";
            ViewState["mode"] = "Add";
            FillParentMenu();
            FillPage();
            FillHODList();
            loadDepartments();
            FillEmployees();
            if (UserType == 18 || UserType == 21 || UserType == 24 || UserType == 17 || UserType == 20 || UserType == 23 || UserType == 26)
            {
                ddlDepartment.Visible = false;
                ddlEmployeecode.Visible = false;
                ddlDepartment.SelectedValue = Session["DepartID"].ToString();
                ddlDepartment_SelectedIndexChanged(this, EventArgs.Empty);
                ddlEmployeecode.SelectedValue = Session["EmployeeCode"].ToString();
                ddlEmployeecode_SelectedIndexChanged(this, EventArgs.Empty);
                //bindgrid();
                
            }

        }



    }

    protected void bindgrid()
    {
        objBll.Status_id = 1;
        DataTable _dt = new DataTable();
        if (ViewState["countries"] == null)
        {
            /*objBll.EmployeeCode = ddlHOD.SelectedValue;
            _dt = objBll.EmployeeReportToSelectByEmployeeCode(objBll);*/

            objBll.EmployeeCode = ddlEmployeecode.SelectedValue;
            _dt = objBll.EmployeeReportToHODSelectByEmployeeCode(objBll);
        }

        else
            _dt = (DataTable)ViewState["countries"];


        if (_dt.Rows.Count == 0)
            lab_dataStatus.Visible = true;
        else
        {
            dv_country.DataSource = _dt;
            ViewState["countries"] = _dt;
            lab_dataStatus.Visible = false;
        }
        dv_country.DataBind();
        pan_New.Attributes.CssStyle.Add("display", "none");
        //if (UserType == 18 || UserType == 21 || UserType == 24 || UserType == 17 || UserType == 20 || UserType == 23 || UserType == 26)
        //{
            dv_country.Columns[8].Visible = false;
       // }
    }

    protected void but_cancel_Click(object sender, EventArgs e)
    {
        pan_New.Attributes.CssStyle.Add("display", "none");
        dv_country.SelectedRowStyle.Reset();
    }

    protected void but_save_Click(object sender, EventArgs e)
    {
        try
        {
            string mode = Convert.ToString(ViewState["mode"]);
            
            objBll.EmployeeCode = ddlEmployeecode.SelectedValue;
            objBll.ReportTo = ddlHOD.SelectedValue;
            objBll.HODEmail = txtHODEmail.Text;
            objBll.IsEmail = chkEmail.Checked;
            //objBll.Status_id = 1;
            objBll.Status_id = Convert.ToInt32(ddlReportingType.SelectedValue);



            int nAlreadyIn;
            if (mode != "Edit")
            {

                nAlreadyIn = objBll.EmployeeReportToInsert(objBll);
                /*if (nAlreadyIn != 0)
                {
                    if (nAlreadyIn == 1)
                        drawMsgBox("Menu Name already Exists.");
                    //                else if (nAlreadyIn == 2)
                    //                    drawMsgBox("Menu Code already Exists");

                }
                else
                {*/
                ViewState["countries"] = null;
                bindgrid();
                pan_New.Attributes.CssStyle.Add("display", "none");
                error.Visible = false;
                drawMsgBox("Added Successfully");

                //}
            }
            else
            {

                //objBll.EmployeeCode = ViewState["EditID"].ToString();//Convert.ToInt32(ViewState["EditID"]);
                objBll.Tid = Convert.ToInt32(ViewState["EditID"]);
                //objBll.Status_id = 1;
                nAlreadyIn = objBll.EmployeeReportToUpdate(objBll);



                ViewState["countries"] = null;
                bindgrid();
                pan_New.Attributes.CssStyle.Add("display", "none");
                error.Visible = false;
                drawMsgBox("Updated sucessfully.");

            }

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void dv_country_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["countries"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();
            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            bindgrid();

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void dv_country_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dv_country.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void dv_country_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //objBll.EmployeeCode = Convert.ToInt32(dv_country.Rows[e.RowIndex].Cells[0].Text);
            objBll.Tid = Convert.ToInt32(dv_country.Rows[e.RowIndex].Cells[1].Text);
            objBll.EmployeeReportToDelete(objBll);

            ViewState["countries"] = null;
            bindgrid();

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void dv_country_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("ImageButton2");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?') ");
        }
    }

    protected void but_new_Click1(object sender, EventArgs e)
    {

        pan_New.Attributes.CssStyle.Add("display", "inline");
        /*trEmployee.Attributes.CssStyle.Add("display", "inline");
        trDepartment.Attributes.CssStyle.Add("display", "inline");*/
        //trDepartment.Visible = true;
        //trEmployee.Visible = true;
        ViewState["mode"] = "Add";
        txtHODEmail.Text = "";
        //ddlEmployeecode.SelectedValue = "0";
        chkEmail.Checked = false;
        ddlHOD.SelectedValue = "0";

        error.Visible = false;
        dv_country.SelectedRowStyle.Reset();
    }


    protected void dv_country_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {


            /*trEmployee.Attributes.CssStyle.Add("display", "none");
            trDepartment.Attributes.CssStyle.Add("display", "none");*/
            //trDepartment.Visible = false;
            //trEmployee.Visible = false;
            this.txtHODEmail.Text = this.dv_country.Rows[e.NewSelectedIndex].Cells[2].Text != "&nbsp;" ? this.dv_country.Rows[e.NewSelectedIndex].Cells[2].Text : "";

            this.chkEmail.Checked = this.dv_country.Rows[e.NewSelectedIndex].Cells[3].Text != "&nbsp;" ? Convert.ToBoolean(this.dv_country.Rows[e.NewSelectedIndex].Cells[3].Text) : false;

            this.ddlHOD.SelectedValue = this.dv_country.Rows[e.NewSelectedIndex].Cells[5].Text != "&nbsp;" ? this.dv_country.Rows[e.NewSelectedIndex].Cells[5].Text : "0";
            this.ddlReportingType.SelectedValue = this.dv_country.Rows[e.NewSelectedIndex].Cells[7].Text != "&nbsp;" ? this.dv_country.Rows[e.NewSelectedIndex].Cells[7].Text : "0";

            ViewState["mode"] = "Edit";
            ViewState["EditID"] = this.dv_country.Rows[e.NewSelectedIndex].Cells[1].Text;
            error.Visible = false;
            pan_New.Attributes.CssStyle.Add("display", "inline");
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void drawMsgBox(string msg)
    {
        try
        {
            ImpromptuHelper.ShowPrompt(msg);

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }

    protected void FillParentMenu()
    {

        /*objBll.Status_id = 1;
        DataTable _dt = new DataTable();
        _dt = objBll.LmsAppMenuFetchByStatusId(objBll);
        objBase.FillDropDown(_dt, ddlParentMenu, "Menu_ID", "MenuText");*/

    }

    protected void FillPage()
    {

        /*DataTable dt = new DataTable();
        dt = objBll.LmsAppPageFetch();
        objBase.FillDropDown(dt, ddlPage, "Page_Id", "PageTitle");*/


    }

    protected void FillHODList()
    {

        DataTable dt = new DataTable();


        if (UserLevel == 4) //Campus
        {
            objBll.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objBll.Center_Id = Convert.ToInt32(Session["CenterID"].ToString());
        }
        else if (UserLevel == 3)//Region
        {
            objBll.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            objBll.Center_Id = 0;
        }
        else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
        {
            objBll.Region_Id = 0;
            objBll.Center_Id = 0;

        }

        dt = objBll.UserHODSelectByUserTypeIDRegionCenter(objBll);
        objBase.FillDropDown(dt, ddlHOD, "EmployeeCode", "CodeName");
        ViewState["dtHOD"] = dt;


    }


    protected void ddlHOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*ViewState["countries"] = null;
        bindgrid();*/

        if (ViewState["dtHOD"] != null)
        {
            dtHOD = (DataTable)ViewState["dtHOD"];
            DataView dv = dtHOD.DefaultView;
            dv.RowFilter = "EmployeeCode = " + ddlHOD.SelectedValue;
            dtHOD = dv.ToTable();
            if (dtHOD.Rows.Count > 0)
            {
                txtHODEmail.Text = dtHOD.Rows[0]["HODEmail"] != DBNull.Value ? dtHOD.Rows[0]["HODEmail"].ToString() : "";
            }


        }

    }

    protected void FillEmployees()
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

        dt = obj.EmployeeprofileSelectByRegionCenter(obj);


        objBase.FillDropDown(dt, ddlEmployeecode, "EmployeeCode", "CodeName");
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
        objBase.FillDropDown(dt, ddlEmployeecode, "employeecode", "CodeName");
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            loadEmployees();
        }
        else if (ddlDepartment.SelectedIndex == 0)
        {
            FillEmployees();
        }
        ViewState["countries"] = null;
        dv_country.DataSource = null;
        dv_country.DataBind();
        pan_New.Attributes.CssStyle.Add("display", "none");
    }


    protected void ddlEmployeecode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["countries"] = null;
        bindgrid();
    }
}

