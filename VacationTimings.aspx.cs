using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;
using System.Globalization;


public partial class VacationTimings : System.Web.UI.Page
{
    BLLVacationTimigs objBll = new BLLVacationTimigs();
    DALBase objbase = new DALBase();

    int UserLevel, UserType;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }

            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());
            ViewState["tMoodLate"] = "uncheck";


            #region 'Roles&Priviliges'


            if (Session["EmployeeCode"] != null)
            {
                //string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                //System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                //string sRet = oInfo.Name;

                //int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

                //int _result = objbase.ApplicationSettings(sRet, _part_Id);


                //if (_result == 1)
                //{
                if (!IsPostBack)
                {
                    try
                    {
                        ViewState["tMood"] = "check";
                        //======== Page Access Settings ========================
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
                        ViewState["mode"] = "Add";
                        ViewState["SortDirection"] = "ASC";

                        loadMonths();
                        loadRegions();

                        loadCenters();
                        //bindgrid();

                    }
                    catch (Exception ex)
                    {
                        Session["error"] = ex.Message;
                        Response.Redirect("ErrorPage.aspx", false);
                    }

                }
            #endregion
                //}
                /*else
                    {
                    Session["error"] = "You Are Not Authorized To Access This Page";
                    Response.Redirect("ErrorPage.aspx", false);
                    }
                }*/



            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvShifts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMood"].ToString();

                foreach (GridViewRow gvr in gvShifts.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("CheckBox1");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMood"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMood"] = "check";
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }

    }
    protected void loadMonths()
    {
        try
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
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
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
            ddlRegion.SelectedValue = Session["RegionID"].ToString();
        }

        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void DeleteVacation()
    {
        try
        {
            objBll.From_date = Convert.ToDateTime(ddlFrom.SelectedValue);
            objBll.To_date = Convert.ToDateTime(ddlToDate.SelectedValue);
            objBll.Region_id = Convert.ToInt32(ddlRegion.SelectedValue);
            int k = objBll.VacationTimingDeleteRegionWise(objBll);
            if (k == 0)
            {
                ImpromptuHelper.ShowPrompt("Cannot Delete Vacations!");
                return;
            }
            ImpromptuHelper.ShowPrompt("Vacations Deleted!");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
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
            gvCenter.DataSource = _dt;
            gvCenter.DataBind();

            ddlCenter.Items.Insert(0, new ListItem("Select Center", "0"));
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }


        ViewState["dtMain"] = null;
        bindgrid();
        ////DateTime.TryParse(
    }


    protected void gvCenters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMood"].ToString();

                foreach (GridViewRow gvr in gvCenter.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("cbAllow");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMood"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMood"] = "check";
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


    protected void gvShifts_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtMain"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();

            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            ViewState["dtMain"] = null;
            bindgrid();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvShifts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvShifts.PageIndex = e.NewPageIndex;

            ViewState["dtMain"] = null;
            bindgrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }



    protected void gvShifts_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        /*if (e.Row.RowIndex != -1)
        {
            DropDownList ddlRoleType = (DropDownList)e.Row.FindControl("ddlRoleType");
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            objBLL.Status_id = 1;
            objBLL.Used_For = "LVS";
            DataTable objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);
            ddlRoleType.DataSource = objDt;
            ddlRoleType.DataValueField = "LeaveType_Id";
            ddlRoleType.DataTextField = "LeaveType";
            ddlRoleType.DataBind();
            ddlRoleType.SelectedValue = e.Row.Cells[2].Text;
            DropDownList ddlAprv = (DropDownList)e.Row.FindControl("ddlAprv");
            objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetch(1);
            ddlAprv.DataSource = objDt;
            ddlAprv.DataValueField = "Sat";
            ddlAprv.DataTextField = "Descr";
            ddlAprv.DataBind();
        
        }*/

        //    string str=e.Row.Cells[3].Text.Substring(1, 3);


        //if ( str== "Fri") // if Data Locked after ERP PRocesss
        //    {
        //    e.Row.BackColor = System.Drawing.Color.Beige;
        //    e.Row.Enabled = false;
        //    }
    }


    protected void drawMsgBox(string msg, int errType)
    {
        try
        {
            ADG.JQueryExtenders.Impromptu.ImpromptuHelper.ShowPrompt(msg);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void bindgrid()
    {
        //loadEmpleave();
        gvShifts.DataSource = null;
        //gvShow.DataSource = null;
        try
        {

            DataTable dt = new DataTable();

            objBll.PMonth = ddlMonths.SelectedValue.ToString();
            objBll.Region_id = Convert.ToInt32(ddlRegion.SelectedValue);
            objBll.Center_id = Convert.ToInt32(ddlCenter.SelectedValue);

            if (ViewState["dtMain"] == null)
                dt = objBll.fetchVacationsRegionCenter(objBll);
            else
                dt = (DataTable)ViewState["dtMain"];

            gvShifts.DataSource = dt;
            gvShifts.DataBind();
            ViewState["dtMain"] = dt;
        }

        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }

    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["dtMain"] = null;
            ViewState["dtMainShow"] = null;
            bindgrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["dtMain"] = null;
            bindgrid();

            //if (ddlCenter.SelectedValue != "0")
            //{
            //    btnAddNewVacation.Attributes.CssStyle.Add("display", "inline");
            //}
            //else
            //{
            //    btnAddNewVacation.Attributes.CssStyle.Add("display", "none");
            //}
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }





    //==================================================================================================================================
    protected void Save()
    {

        this.objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);
        //this.objBll.Center_id = Convert.ToInt32(this.ddlCenter.SelectedValue);

        this.objBll.strFrom_date = txtFrmDate.Text;

        this.objBll.strTo_date = txtToDate.Text;
        this.objBll.Reason = this.txt_Reason.Text;
        this.objBll.Time_in = this.txtStartTime.Text;
        this.objBll.Absent_Time = this.txtAbsentTime.Text;
        this.objBll.Time_out = this.txtEndTime.Text;
        if (cbIsoffTeacher.Checked)
        {
            this.objBll.IsOffteacher = true;
        }
        else
        {
            this.objBll.IsOffteacher = false;
        }
        int nAlreadyIn = 0;
        if (ViewState["Mode"].ToString() == "Add")
        {
            try
            {
                objBll.strCenter_id = "";
                this.objBll.Inserted_by = Session["RegionID"].ToString();
                foreach (GridViewRow r in gvCenter.Rows)
                {
                    CheckBox cb = (CheckBox)r.FindControl("cbAllow");
                    if (cb.Checked == true)
                    {
                        objBll.strCenter_id = objBll.strCenter_id + r.Cells[0].Text + ",";

                    }
                }
                objBll.strCenter_id = objBll.strCenter_id.TrimEnd(',');
                if (objBll.strCenter_id == "")
                {
                    drawMsgBox("Please select one or more Center(s) to continue", 3);
                    return;
                }
                nAlreadyIn = objBll.VacationTimingsInsert(objBll);
                if (nAlreadyIn != -2)
                {
                    drawMsgBox("Vacation timings saved successfully.Please run the process to see the effect on Employee Shift Timings", 3);
                }
                else
                {
                    drawMsgBox("Vacation timings already exist in these dates.", 3);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        else if (ViewState["Mode"].ToString() == "Edit")
        {
            objBll.From_date = Convert.ToDateTime(objBll.strFrom_date);
            objBll.To_date = Convert.ToDateTime(objBll.strTo_date);

            objBll.VacationTimings_id = Convert.ToInt32(ViewState["VacationTimings_id"].ToString());
            this.objBll.Last_updated_by = Session["RegionID"].ToString();
            nAlreadyIn = objBll.VacationTimingsUpdate(objBll);
            if (nAlreadyIn == 0)
            {
                drawMsgBox("Vacation timings updated successfully.", 3);
            }
            else
            {
                drawMsgBox("Vacation timings do not  exist in these dates.", 3);
            }
        }
        clearForm();
        pan_New.Attributes.CssStyle.Add("display", "none");
        btnAddNewVacation.Attributes.CssStyle.Add("display", "inline");

        ViewState["dtMain"] = null;
        bindgrid();
        ddlCenter.Visible = true;
    }
    protected void clearForm()
    {
        this.txtFrmDate.Text = "";
        this.txtToDate.Text = "";
        this.txt_Reason.Text = "";
        this.txtStartTime.Text = "";
        this.txtAbsentTime.Text = "";
        this.txtEndTime.Text = "";
        this.cb_StartTime.Checked = false;
        this.cb_EndTime.Checked = false;

        this.txtStartTime.Enabled = false;
        this.txtAbsentTime.Enabled = false;
        this.txtEndTime.Enabled = false;
    }

    protected void btnSaveVacation_Click(object sender, EventArgs e)
    {
        if (validateControls())
        {

            Save();
        }

        //btnAddNewVacation.Attributes.CssStyle.Add("display", "inline");
    }

    protected bool validateControls()
    {
        if (this.txtFrmDate.Text == "")
        {
            drawMsgBox("From Date is required.", 0);
            return false;
        }

        if (this.txtToDate.Text == "")
        {
            drawMsgBox("To Date is required.", 0);
            return false;
        }

        if (this.txt_Reason.Text == "")
        {
            drawMsgBox("Reason is required.", 0);
            return false;
        }

        //if (!cb_StartTime.Checked && !cb_EndTime.Checked)
        //{
        //    drawMsgBox("Start Time or End Time is required.", 0);
        //    return false;
        //}
        //else
        //{
        //    if (cb_StartTime.Checked && (this.txtStartTime.Text == "" || this.txtAbsentTime.Text == ""))
        //    {
        //        drawMsgBox("Start Time and Absent Time is required.", 0);
        //        return false;
        //    }
        //    if (cb_EndTime.Checked && this.txtEndTime.Text == "")
        //    {
        //        drawMsgBox("End Time is required.", 0);
        //        return false;
        //    }
        //}

        return true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnAddNewVacation_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Mode"] = "Add";
            pan_New.Attributes.CssStyle.Add("display", "inline");
            pan_Delete.Attributes.CssStyle.Add("display", "none");
            btnDeleteVacy.Attributes.CssStyle.Add("display", "inline");
            btnAddNewVacation.Attributes.CssStyle.Add("display", "none");
            txt_Reason.Text = "";
            txtFrmDate.Text = "";
            txtToDate.Text = "";
            txtStartTime.Text = "09:00";
            txtEndTime.Text = "14:00";
            txtAbsentTime.Text = "09:30";
            ddlCenter.Visible = false;
            loadCenters();
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void btnCancelVacation_Click(object sender, EventArgs e)
    {
        try
        {
            pan_New.Attributes.CssStyle.Add("display", "none");
            btnAddNewVacation.Attributes.CssStyle.Add("display", "inline");
            btnDeleteVacy.Attributes.CssStyle.Add("display", "inline");
            ddlCenter.Visible = true;
        }
        catch (Exception ex)
        {
            throw;
        }

    }





    protected void cb_StartTime_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.cb_StartTime.Checked)
            {
                this.txtStartTime.Enabled = true;
                this.txtAbsentTime.Enabled = true;
            }
            else
            {

                this.txtStartTime.Text = "";
                this.txtAbsentTime.Text = "";

                this.txtStartTime.Enabled = false;
                this.txtAbsentTime.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void cb_EndTime_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.cb_EndTime.Checked)
            {
                this.txtEndTime.Enabled = true;
            }
            else
            {
                this.txtEndTime.Text = "";

                this.txtEndTime.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Mode"] = "Edit";
            pan_New.Attributes.CssStyle.Add("display", "inline");
            btnAddNewVacation.Attributes.CssStyle.Add("display", "none");
            ImageButton btnEdit = (ImageButton)sender;
            objBll.VacationTimings_id = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["VacationTimings_id"] = objBll.VacationTimings_id;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            txtFrmDate.Text = gvr.Cells[2].Text;
            txtToDate.Text = gvr.Cells[3].Text;
            txt_Reason.Text = gvr.Cells[4].Text;
            txtAbsentTime.Text = gvr.Cells[6].Text;
            txtAbsentTime.Enabled = true;
            txtEndTime.Enabled = true;
            txtEndTime.Text = gvr.Cells[7].Text;
            txtStartTime.Text = gvr.Cells[5].Text;
            txtStartTime.Enabled = true;
            if (gvr.Cells[8].Text == "True")
            {
                cbIsoffTeacher.Checked = true;
            }
            else
            {
                cbIsoffTeacher.Checked = false;
            }
            gvCenter.DataSource = null;
            gvCenter.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = (ImageButton)sender;
            objBll.VacationTimings_id = Convert.ToInt32(btnDelete.CommandArgument);
            int k = objBll.DeleteVacationTiming(objBll.VacationTimings_id);
            if (k == 0)
            {
                ImpromptuHelper.ShowPrompt("Cannot Delete vacations as ERP Process has been run for this date range.");
            }
            ViewState["dtMain"] = null;
            bindgrid();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnDeleteVacy_Click(object sender, EventArgs e)
    {
        try
        {
            pan_New.Attributes.CssStyle.Add("display", "none");
            pan_Delete.Attributes.CssStyle.Add("display", "inline");
            btnDeleteVacy.Attributes.CssStyle.Add("display", "none");
            objBll.PMonth = ddlMonths.SelectedValue;
            if (!String.IsNullOrEmpty(Session["RegionID"].ToString()))
                objBll.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
            DataTable dtfrom = objBll.VacationTimingFillDate(objBll);
            objbase.FillDropDown(dtfrom, ddlFrom, "From_date", "From_date");
            objbase.FillDropDown(dtfrom, ddlToDate, "To_date", "To_date");
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void btnDeleteSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFrom.SelectedIndex <= 0)
            {
                ImpromptuHelper.ShowPrompt("Please Select dates!");
                return;
            }
            if (ddlToDate.SelectedIndex <= 0)
            {
                ImpromptuHelper.ShowPrompt("Please Select dates!");
                return;
            }
            else
            {
                DeleteVacation();
                btnCancelDelete_Click(this, EventArgs.Empty);

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancelDelete_Click(object sender, EventArgs e)
    {
        try
        {
            pan_Delete.Attributes.CssStyle.Add("display", "none");
            btnDeleteVacy.Attributes.CssStyle.Add("display", "inline");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlToDate.Items.Count > 0)
                ddlToDate.SelectedIndex = ddlFrom.SelectedIndex;

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}