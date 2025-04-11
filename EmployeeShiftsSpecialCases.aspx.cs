using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using ADG.JQueryExtenders.Impromptu;
using System.Globalization;

public partial class EmployeeShiftsSpecialCases : System.Web.UI.Page
{
    
    BLLSpecialCasesTimigs objBll = new BLLSpecialCasesTimigs();
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
                        ViewState["tMood"] = "check";
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
    private void SaveCheckedValues()
    {
        ArrayList userdetails = new ArrayList();
        int index = -1;
        foreach (GridViewRow gvrow in gvEmployees.Rows)
        {
            index = (int)gvEmployees.DataKeys[gvrow.RowIndex].Value;
            bool result = ((CheckBox)gvrow.FindControl("cbAllow")).Checked;

            // Check in the Session
            if (Session["CHECKED_ITEMS"] != null)
                userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (result)
            {
                if (!userdetails.Contains(index))
                    userdetails.Add(index);
            }
            else
                userdetails.Remove(index);
        }
        if (userdetails != null && userdetails.Count > 0)
            ViewState["CHECKED_ITEMS"] = userdetails;
    }

    protected void cbHalfday_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbHalfday.Checked == true)
                divhalfDayTime.Visible = true;
            else
                divhalfDayTime.Visible = false;

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void rbLstRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cblWeekdays.Visible = true;
            clearForm();
            if (rbLstOpt.SelectedValue == "0" && ddlSpecialCase_Type.SelectedValue == "1")//Date Range and on manual/ approved 
            {
                bool val;
                for (int i = 0; i < cblWeekdays.Items.Count; i++)
                {
                    cblWeekdays.Items[i].Selected = true;
                    val = cblWeekdays.Items[i].Selected;
                }

                div_WeekDays.Visible = false;
                divAbsentTime.Visible = true;
                divEndTime.Visible = true;

                divStartTime.Visible = true;
                divSaturday.Visible = true;
                divFriday.Visible = true;
                divData.Visible = true;
            }
            else if (rbLstOpt.SelectedValue == "1" && ddlSpecialCase_Type.SelectedValue == "1")// weekday and on manual/approved 
            {
                divData.Visible = true;
                div_WeekDays.Visible = true;
                divAbsentTime.Visible = true;
                divEndTime.Visible = true;

                divStartTime.Visible = true;
                divSaturday.Visible = false;
                divFriday.Visible = false;
            }
            else if (rbLstOpt.SelectedValue == "1" && ddlSpecialCase_Type.SelectedValue != "0")
            {
                divData.Visible = true;
                div_WeekDays.Visible = true;
                divAbsentTime.Visible = false;
                divEndTime.Visible = false;

                divStartTime.Visible = false;
                divSaturday.Visible = false;
                divFriday.Visible = false;

            }
            else if (rbLstOpt.SelectedValue == "0" && ddlSpecialCase_Type.SelectedValue != "0")
            {
                for (int i = 0; i < cblWeekdays.Items.Count; i++)
                {
                    cblWeekdays.Items[i].Selected = true;
                }
                divData.Visible = true;
                div_WeekDays.Visible = false;
                divAbsentTime.Visible = false;
                divEndTime.Visible = false;

                divStartTime.Visible = false;
                divSaturday.Visible = false;
                divFriday.Visible = false;
            }

            else if (ddlSpecialCase_Type.SelectedValue == "0")
            {
                divData.Visible = false; //for default
                cblWeekdays.Visible = false;
            }
            DefaultCheckboxSelect();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void DefaultCheckboxSelect()
    {
        try
        {
            for (int i = 0; i < cblWeekdays.Items.Count; i++)
            {
                cblWeekdays.Items[i].Selected = false;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }
    protected void loadEmployees()
    {
        try
        {
            BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();
            DataTable dt = new DataTable();


            if (ddlRegion.SelectedValue != "0" && ddlCenter.SelectedValue != "0")
            {
                obj.Region_id = Convert.ToInt32(ddlRegion.SelectedValue);
                obj.Center_id = Convert.ToInt32(ddlCenter.SelectedValue);
            }
            else if (ddlRegion.SelectedValue != "0" && ddlCenter.SelectedValue == "0")
            {
                obj.Region_id = Convert.ToInt32(ddlRegion.SelectedValue);
                obj.Center_id = 0;
            }
            else if (ddlRegion.SelectedValue == "0")
            {
                obj.Region_id = 0;
                obj.Center_id = 0;
            }
            if (ViewState["Employees"] == null)
            {
                dt = obj.EmployeeprofileSelectByRegionCenter(obj);
            }
            // objbase.FillDropDown(dt, ddlEmp, "EmployeeCode", "CodeName");
            else
            {
                dt = (DataTable)ViewState["Employees"];
            }

            ViewState["Employees"] = dt;
            gvEmployees.DataSource = dt;
            gvEmployees.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvEmployees.Rows.Count > 0)
            {
                gvEmployees.UseAccessibleHeader = false;
                gvEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvEmployees.FooterRow.TableSection = TableRowSection.TableFooter;
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
            BLLSpecialCasesTimigs objBll = new BLLSpecialCasesTimigs();
            DataTable _dt = new DataTable();

            _dt = objBll.fetchRegions();

            ddlRegion.DataTextField = "Region_Name";
            ddlRegion.DataValueField = "Region_Id";
            ddlRegion.DataSource = _dt;
            ddlRegion.DataBind();

            ddlRegion.Items.Insert(0, new ListItem("Head Office", "0"));
            ddlRegion.SelectedValue = Session["RegionID"].ToString();
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
            BLLSpecialCasesTimigs objBll = new BLLSpecialCasesTimigs();
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

            //ddlCenter.Items.Insert(0, new ListItem("Select Center", "0"));



            ViewState["dtMain"] = null;
            bindgrid();
            loadEmployees();
            //DateTime.TryParse(
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

            DataTable _dt = (DataTable)ViewState["Employees"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();

            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            loadEmployees();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SaveCheckedValues();
            string s = ViewState["CHECKED_ITEMS"].ToString();
            gvEmployees.PageIndex = e.NewPageIndex;
            loadEmployees();

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

            bindgrid();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvShifts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvShifts.PageIndex = e.NewPageIndex;

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
        gvDetail.DataSource = null;
        gvDetail.DataBind();
        Panel_DetailGrid.Visible = false;
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
                dt = objBll.fetchSpecialCasesRegionCenter(objBll);
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btndel = (LinkButton)sender;
            objBll.SpecialCases_id = Convert.ToInt32(btndel.CommandArgument);
            objBll.PMonth = Session["CurrentMonth"].ToString();
            int k = objBll.SpecialCasesTimingsDelete(objBll);
            if (k == 0)
            {
                ImpromptuHelper.ShowPrompt("Record Deleted");
            }
            else
            {
                ImpromptuHelper.ShowPrompt("Record cannot be deleted");
            }
            ViewState["dtMain"] = null;
            bindgrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btndel = (LinkButton)sender;
            objBll.SpecialCases_id = Convert.ToInt32(btndel.CommandArgument);
            DataTable dt = objBll.EmployeeShifts_SpecialCasesSelectDetail(objBll.SpecialCases_id);
            gvDetail.DataSource = dt;
            gvDetail.DataBind();
            Panel_DetailGrid.Visible = true;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<bool>("IsSpecificDays") == false)
                {
                    gvDetail.Columns[4].Visible = true;
                    gvDetail.Columns[5].Visible = true;
                    gvDetail.Columns[6].Visible = true;
                    gvDetail.Columns[7].Visible = true;
                    gvDetail.Columns[8].Visible = true;
                    gvDetail.Columns[9].Visible = true;
                    gvDetail.Columns[10].Visible = false;
                    gvDetail.Columns[11].Visible = false;
                    gvDetail.Columns[12].Visible = false;
                    gvDetail.Columns[13].Visible = false;
                    gvDetail.Columns[14].Visible = false;
                    gvDetail.Columns[15].Visible = false;
                    gvDetail.Columns[16].Visible = false;
                }
                else
                {
                    gvDetail.Columns[4].Visible = false;
                    gvDetail.Columns[5].Visible = false;
                    gvDetail.Columns[6].Visible = false;
                    gvDetail.Columns[7].Visible = false;
                    gvDetail.Columns[8].Visible = false;
                    gvDetail.Columns[9].Visible = false;
                    gvDetail.Columns[10].Visible = true;
                    gvDetail.Columns[11].Visible = true;
                    gvDetail.Columns[12].Visible = true;
                    gvDetail.Columns[13].Visible = true;
                    gvDetail.Columns[14].Visible = true;
                    gvDetail.Columns[15].Visible = true;
                    gvDetail.Columns[16].Visible = true;
                }

            }

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
            ViewState["Employees"] = null; 
            loadEmployees();

            //if (ddlCenter.SelectedValue != "0")
            //{
            //    btnAddNewSpecialCase.Attributes.CssStyle.Add("display", "inline");
            //}
            //else
            //{
            //    btnAddNewSpecialCase.Attributes.CssStyle.Add("display", "none");
            //}
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
            //    if (e.CommandName == "toggleCheck")
            //    {
            //        CheckBox cb = null;
            //        string mood = ViewState["tMood"].ToString();

            //        foreach (GridViewRow gvr in gvShifts.Rows)
            //        {
            //            //if (gvr.Cells[1].Text == "True")
            //            //{

            //            cb = (CheckBox)gvr.FindControl("CheckBox1");
            //            if (mood == "" || mood == "check")
            //            {
            //                cb.Checked = true;
            //                ViewState["tMood"] = "uncheck";
            //            }
            //            else
            //            {
            //                cb.Checked = false;
            //                ViewState["tMood"] = "check";
            //            }
            //            //}
            //        }
            //    }
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
        try
        {
            if (cbHalfday.Checked == true && String.IsNullOrEmpty(txthfet.Text) && String.IsNullOrEmpty(txthfst.Text))
            {
                ImpromptuHelper.ShowPrompt("Please Update Half Day Timings. ");
                return;
            }


            this.objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);
            this.objBll.Center_id = Convert.ToInt32(this.ddlCenter.SelectedValue);


            this.objBll.From_date = Convert.ToDateTime(this.txtFrmDate.Text,  CultureInfo.InvariantCulture);
            this.objBll.To_date = Convert.ToDateTime(this.txtToDate.Text, CultureInfo.InvariantCulture);
            this.objBll.Reason = this.txt_Reason.Text;
            if (!String.IsNullOrEmpty(txthfet.Text))
                this.objBll.first_half_end = txthfet.Text;
            else
                this.objBll.first_half_end = txtEndTime.Text;

            if (!String.IsNullOrEmpty(txthfst.Text))
                this.objBll.second_half_start = txthfst.Text;
            else
                this.objBll.second_half_start = txtAbsentTime.Text;

            if (rbLstOpt.SelectedValue == "0")
            {
                DefaultDayTrue();
                objBll.IsSpecificDays = false;
            }
            else if (rbLstOpt.SelectedValue == "1")
            {
                if (CheckSelectedDays() == false)
                {
                    return;
                }
                objBll.IsSpecificDays = true;
            }
            this.objBll.Time_in = this.txtStartTime.Text;
            this.objBll.Absent_Time = this.txtAbsentTime.Text;
            this.objBll.Time_out = this.txtEndTime.Text;
            if (String.IsNullOrEmpty(txtMargin.Text))
            {
                objBll.Margin = 0;
            }
            else
            {
                this.objBll.Margin = Convert.ToInt32(this.txtMargin.Text);
            }
            objBll.SpecialCase_Type = Convert.ToInt32(ddlSpecialCase_Type.SelectedValue);

            this.objBll.SaT_Time_in = this.txtSatStartTime.Text;
            this.objBll.SaT_Absent_Time = this.txtSatAbsentTime.Text;
            this.objBll.SaT_Time_out = this.txtSatEndTime.Text;

            this.objBll.Fri_Time_in = this.txtFriStartTime.Text;
            this.objBll.Fri_Absent_Time = this.txtFriAbsentTime.Text;
            this.objBll.Fri_Time_out = this.txtFriEndTime.Text;

            this.objBll.Inserted_by = Session["User_Id"].ToString();
            int nAlreadyIn = 0;

            for (int i = 0; i < gvEmployees.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)gvEmployees.Rows[i].FindControl("cbAllow");
                if (cb.Checked == true)
                {
                    this.objBll.Emp_Code = gvEmployees.Rows[i].Cells[1].Text;
                    nAlreadyIn = objBll.SpecialCasesTimingsInsert(objBll);
                }

            }

            if (nAlreadyIn != -2)
            {
                drawMsgBox("Special Case timings saved successfully.", 3);
            }
            else
            {
                drawMsgBox("Special Case timings for this employee already exist in these dates.", 3);
            }


            clearForm();
            pan_New.Attributes.CssStyle.Add("display", "none");
            btnAddNewSpecialCase.Attributes.CssStyle.Add("display", "inline");

            ViewState["dtMain"] = null;
            bindgrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected bool CheckSelectedDays()
    {
        DefaultDayFalse();
        if (cblWeekdays.Items.Cast<ListItem>().Any(item => item.Selected))
        {
            if (cblWeekdays.Items.FindByText("Sunday").Selected == true)
            {
                objBll.Sunday = true;
            }
            if (cblWeekdays.Items.FindByText("Monday").Selected == true)
            {
                objBll.Monday = true;
            }
            if (cblWeekdays.Items.FindByText("Tuesday").Selected == true)
            {
                objBll.Tuesday = true;
            }
            if (cblWeekdays.Items.FindByText("Wednesday").Selected == true)
            {
                objBll.Wednesday = true;
            }
            if (cblWeekdays.Items.FindByText("Thursday").Selected == true)
            {
                objBll.Thursday = true;
            }
            if (cblWeekdays.Items.FindByText("Friday").Selected == true)
            {
                objBll.Friday = true;
            }
            if (cblWeekdays.Items.FindByText("Saturday").Selected == true)
            {
                objBll.Saturday = true;
            }
            return true;
        }
        else
        {
            drawMsgBox("Please Select atleast one day!", 0);
            return false;
        }
    }
    protected void DefaultDayFalse()
    {
        try
        {
            objBll.Sunday = false;
            objBll.Monday = false;
            objBll.Tuesday = false;
            objBll.Wednesday = false;
            objBll.Thursday = false;
            objBll.Friday = false;
            objBll.Saturday = false;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void DefaultDayTrue()
    {
        try
        {
            objBll.Sunday = true;
            objBll.Monday = true;
            objBll.Tuesday = true;
            objBll.Wednesday = true;
            objBll.Thursday = true;
            objBll.Friday = true;
            objBll.Saturday = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void clearForm()
    {
        try
        {
            this.ddlEmp.SelectedValue = "0";
            this.txtFrmDate.Text = "";
            this.txtToDate.Text = "";
            this.txt_Reason.Text = "";
            this.txtStartTime.Text = "";
            this.txtAbsentTime.Text = "";
            this.txtEndTime.Text = "";

            this.txtMargin.Text = "";

            this.txtFriStartTime.Text = "";
            this.txtFriAbsentTime.Text = "";
            this.txtFriEndTime.Text = "";

            this.txtSatStartTime.Text = "";
            this.txtSatEndTime.Text = "";
            this.txtSatAbsentTime.Text = "";
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void btnSaveSpecialCases_Click(object sender, EventArgs e)
    {
        try
        {

            if (validateControls())
            {
                Save();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected bool validateControls()
    {

        if (this.ddlEmp.SelectedValue == "0")
        {
            drawMsgBox("Employee is required.", 0);
            return false;
        }
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
        if (rbLstOpt.SelectedValue == "0" && (String.IsNullOrEmpty(txtAbsentTime.Text) || String.IsNullOrEmpty(txtEndTime.Text) || String.IsNullOrEmpty(txtStartTime.Text)))
        {
            drawMsgBox("Please Enter Complete Details .", 0);
            return false;
        }



        if (rbLstOpt.SelectedValue == "0" && (this.txtSatAbsentTime.Text == "" || txtSatEndTime.Text == "" || txtSatStartTime.Text == ""))
        {
            drawMsgBox("Saturday Timing Details are required.", 0);
            return false;
        }
        if (rbLstOpt.SelectedValue == "0" && (this.txtFriEndTime.Text == "" || txtFriAbsentTime.Text == "" || txtFriStartTime.Text == ""))
        {
            drawMsgBox("Friday Timing Details are required.", 0);
            return false;
        }
        return true;
    }


    protected void FillSpecialCaseType()
    {
        DataTable dt = objBll.SpecialCase_TypeSelectAll();
        objbase.FillDropDown(dt, ddlSpecialCase_Type, "SpecialCase_Type_Id", "Description");
    }
    protected void btnAddNewSpecialCase_Click(object sender, EventArgs e)
    {
        FillSpecialCaseType();
        loadEmployees();
        ddlSpecialCase_Type.SelectedValue = "1";
        ddlSpecialCase_Type.Visible = false;
        ddlSpecialCase_Type_SelectedIndexChanged(this, EventArgs.Empty);
        ddlEmp.Enabled = true;
        pan_New.Attributes.CssStyle.Add("display", "inline");
        btnAddNewSpecialCase.Attributes.CssStyle.Add("display", "none");
        clearForm();
        divData.Visible = false;
        divCriteria.Visible = false;
        rbLstOpt.SelectedValue = "0";
        rbLstRpt_SelectedIndexChanged(this, EventArgs.Empty);
        rbLstOpt.Visible = false;
        btnSave.Visible = true;
    }


    protected void btnCancelSpecialCases_Click(object sender, EventArgs e)
    {
        pan_New.Attributes.CssStyle.Add("display", "none");
        btnAddNewSpecialCase.Attributes.CssStyle.Add("display", "inline");
        clearForm();
        divData.Visible = false;
        divCriteria.Visible = false;
        ddlSpecialCase_Type.SelectedValue = "0";
        rbLstOpt.SelectedValue = "0";
        rbLstRpt_SelectedIndexChanged(this, EventArgs.Empty);
    }
    protected void ddlSpecialCase_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rbLstOpt.SelectedValue = "0";
            rbLstRpt_SelectedIndexChanged(this, EventArgs.Empty);

            if (ddlSpecialCase_Type.SelectedValue != "0")
            {
                divCriteria.Visible = true;
                rbLstOpt.SelectedValue = "0";
                rbLstRpt_SelectedIndexChanged(this, EventArgs.Empty);
                btnSave.Visible = true;
            }
            else if (ddlSpecialCase_Type.SelectedValue == "0")
            {
                btnSave.Visible = false;
                rbLstOpt.Visible = false;
                divData.Visible = false;
            }
            if (ddlSpecialCase_Type.SelectedValue != "1")
            {
                rbLstOpt.SelectedValue = "0";
                rbLstOpt.Visible = false;
            }
            if (ddlSpecialCase_Type.SelectedValue == "1")
            {
                rbLstOpt.SelectedValue = "0";
                rbLstOpt.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btndel = (LinkButton)sender;
            objBll.SpecialCases_id = Convert.ToInt32(btndel.CommandArgument);
            objBll.SpecialCase_Type = Convert.ToInt32(ddlSpecialCase_Type.SelectedValue);
            objBll.Emp_Code = ddlEmp.SelectedValue;
            ddlEmp.Enabled = false;


        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void cblWeekdays_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cblWeekdays.SelectedValue == "Friday")
            {
                divFriday.Visible = false;
            }
            if (cblWeekdays.SelectedValue == "Saturday")
            {
                divSaturday.Visible = false;
            }
            if (cblWeekdays.Items.FindByValue("Saturday").Selected == false)
            {
                divSaturday.Visible = false;
            }
            if (cblWeekdays.Items.FindByValue("Friday").Selected == false)
            {
                divFriday.Visible = false;
            }
            if (cblWeekdays.Items.FindByValue("Saturday").Selected == true)
            {
                divSaturday.Visible = false;
            }
            if (cblWeekdays.Items.FindByValue("Friday").Selected == true)
            {
                divFriday.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
}