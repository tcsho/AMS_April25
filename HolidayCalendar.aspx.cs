using System;
using System.Data;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;


public partial class HolidayCalendar : System.Web.UI.Page
{
    BLLCalendar bllObj = new BLLCalendar();
    DALBase objbase = new DALBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
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

            //====== End Page Access settings ======================
            int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

            //int _result = objbase.ApplicationSettings(sRet, _part_Id);


            //if (_result == 1)
            //    {


            if (!IsPostBack)
            {
                try
                {
                    ViewState["tMoodLate"] = "uncheck";
                    ViewState["mode"] = "Add";
                    ViewState["SortDirection"] = "ASC";
                    loadMonths();
                    bindgrid();
                    ResetControls();
                    if (!String.IsNullOrEmpty(Session["UserType"].ToString()) && Session["UserType"].ToString() == "22")
                        btnShowDetail.Visible = true;
                    int c_id = 0;
                    if (Session["CenterID"].ToString() != "")
                        c_id = Convert.ToInt32(Session["CenterID"].ToString());
                    int r_id = 0;
                    if (Session["RegionID"].ToString() != "")
                        r_id = Convert.ToInt32(Session["RegionID"].ToString());
                    if (r_id > 0 && c_id == 0)
                    {
                        cbApplyCenter.Visible = true;
                        lblApplytoCenter.Visible = true;
                       // loadCenters();
                    }
                    

                }
                catch (Exception ex)
                {
                    Session["error"] = ex.Message;
                    Response.Redirect("ErrorPage.aspx", false);
                }


            }
            //  }
            //else
            //    {
            //    Session["error"] = "You Are Not Authorized To Access This Page";
            //    Response.Redirect("ErrorPage.aspx", false);
            //    }
        }
        else
        {
            Response.Redirect("~/login.aspx");

        }

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
    protected void loadCenters(object sender, EventArgs e)
    {
        try
        {
            int c_id = 0;
            if (Session["CenterID"].ToString() != "")
                c_id = Convert.ToInt32(Session["CenterID"].ToString());
            int r_id = 0;
            if (Session["RegionID"].ToString() != "")
                r_id = Convert.ToInt32(Session["RegionID"].ToString());
            if (r_id > 0 && c_id == 0)
            {
                BLLVacationTimigs objBll = new BLLVacationTimigs();
                DataTable _dt = new DataTable();

                objBll.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
                _dt = objBll.fetchCenters(objBll);
                if(cbApplyCenter.Checked==true)
                    gvCenter.DataSource = _dt;
                else
                    gvCenter.DataSource = null;
                gvCenter.DataBind();
            }

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
                string mood = ViewState["tMoodLate"].ToString();

                foreach (GridViewRow gvr in gvCenter.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("cbAllow");

                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMoodLate"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMoodLate"] = "check";
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // DateTime dtFrom = Convert.ToDateTime(this.txtFromDate.Text);
            //DateTime dtTo = Convert.ToDateTime(this.txtToDate.Text);

            //int compareDates = dtFrom.CompareTo(dtTo);
            //if (compareDates < 0)
            //{

            string mode = Convert.ToString(ViewState["mode"]);
            int id = 0;

            #region 'Common Data'


            int _reg = 0;
            int _cen = 0;
            string _censtr = "";
            int UserLevel, UserType;



            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());

            if (UserLevel == 4) //Campus
            {
                _reg = Convert.ToInt32(Session["RegionID"].ToString());
                _cen = Convert.ToInt32(Session["CenterID"].ToString());
            }
            else if (UserLevel == 3)//Region
            {
                _reg = Convert.ToInt32(Session["RegionID"].ToString());
                _cen = 0;
                foreach (GridViewRow r in gvCenter.Rows)
                {
                    CheckBox cb = (CheckBox)r.FindControl("cbAllow");
                    if (cb.Checked == true)
                    {
                        _censtr = _censtr + r.Cells[0].Text + ",";

                    }
                }
                _censtr = _censtr.TrimEnd(',');
              
            }
            else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            {
                _reg = 0;
                _cen = 0;

            }

            bllObj.Description = txtDescription.Text;
            bllObj.Region_Id = _reg;
            bllObj.Center_Id = _cen;
            bllObj.centerstring = _censtr;


            bllObj.CalenderDate = txtFromDate.Text;

            #endregion

            int nAlreadyIn = 0;
            if (mode != "Edit")
            {
                #region 'Calendar Add'


                //if(bllObj.CalendarAlreadyExistInRange(dtFrom, dtTo) )


                nAlreadyIn = bllObj.CalendarAdd(bllObj);
             
                if (nAlreadyIn == 0)
                {
                    ViewState["dtLib"] = null;
                    bindgrid();
                    ResetControls();
                    if (UserLevel == 3 && (_censtr == "" || String.IsNullOrEmpty(_censtr)))
                    {
                        drawMsgBox("No Centers selected so Holiday will be applied to Reigonal Office only.Data added successfully for Regional office.", 1);
                    }
                    else
                        drawMsgBox("Data added successfully", 1);

                }
                else if (nAlreadyIn == 1)
                {
                    drawMsgBox("Data already exist.", 3);
                }



                #endregion
            }
            else
            {
                #region 'Update'
                id = Convert.ToInt32(ViewState["EditID"]);
                bllObj.CalId = Int32.Parse(ViewState["EditID"].ToString());

                nAlreadyIn = bllObj.CalendarUpdate(bllObj);
                if (nAlreadyIn == 0)
                {
                    drawMsgBox("Data modified successfully.", 1);
                    ViewState["dtLib"] = null;
                    ResetControls();
                    bindgrid();
                }
                else if (nAlreadyIn == 1)
                {
                    drawMsgBox("Data already exist.", 3);
                }
                #endregion
            }

            //}
            //else
            //{
            //    drawMsgBox("'From Date' must be less then or equal to 'To Date'.", 3);
            //}

        }
        catch (Exception ex)
        {
            throw;
        }
        ViewState["mode"] = "Add";

    }


    private void ResetControls()
    {
        txtDescription.Text = "";
        DateTime d = DateTime.Now;
        txtFromDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();
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

    private void RetrieveEOI(int id)
    {

        bllObj.CalId = id;
        DataTable dt = bllObj.CalendarFetch(id);
        if (dt.Rows.Count > 0)
        {

            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            DateTime d = Convert.ToDateTime(dt.Rows[0]["CalenderDate"].ToString());

            txtFromDate.Text = d.Month.ToString() + '/' + d.Day.ToString() + '/' + d.Year.ToString();

        }
    }

    protected void bindgrid()
    {
        try
        {
            #region 'Fill Datagrid'

            DataTable _dt = new DataTable();

            int _reg = 0;
            int _cen = 0;


            int UserLevel, UserType;



            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());


            if (UserLevel == 4) //Campus
            {
                _reg = Convert.ToInt32(Session["RegionID"].ToString());
                _cen = Convert.ToInt32(Session["CenterID"].ToString());
            }
            else if (UserLevel == 3)//Region
            {
                _reg = Convert.ToInt32(Session["RegionID"].ToString());
                _cen = 0;
            }
            else if (UserLevel == 1 || UserLevel == 2) //Head Office + Admin
            {
                _reg = 0;
                _cen = 0;

            }

            bllObj.Region_Id = _reg;
            bllObj.Center_Id = _cen;
            bllObj.PMonth = ddlMonths.SelectedValue;


            if (ViewState["dtLib"] == null)
                _dt = bllObj.CalendarFetch(bllObj); //Problem
            else
                _dt = (DataTable)ViewState["dtLib"];

            if (_dt.Rows.Count > 0)
            {
                gvLib.DataSource = _dt;
                ViewState["dtLib"] = _dt;
            }
            gvLib.DataBind();

            #endregion

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }

    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dtLib"] = null;
        bindgrid();
    }
    protected void gvLib_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtLib"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();

            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            ViewState["dtLib"] = null;
            bindgrid();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLib_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLib.PageIndex = e.NewPageIndex;

            ViewState["dtLib"] = null;
            bindgrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLib_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            bllObj.CalId = Convert.ToInt32(gvLib.Rows[e.RowIndex].Cells[0].Text);
            int id = bllObj.CalId;

            DataTable dt = bllObj.CalendarFetch(id);
            if (dt.Rows.Count > 0)
            {

                bllObj.CalendarDelete(bllObj);
                ViewState["dtLib"] = null;
                bindgrid();
                drawMsgBox("Data Item Deleted!", 1);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvLib_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("ImageButton2");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure to delete this Item?') ");
        }
    }

    protected void gvLib_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            ViewState["mode"] = "Edit";
            ViewState["EditID"] = this.gvLib.Rows[e.NewSelectedIndex].Cells[0].Text;
            int id = Int32.Parse(ViewState["EditID"].ToString());
            RetrieveEOI(id);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {

        //string strDocNumber;
        //ImageButton btn = (ImageButton)sender;
        //strDocNumber = btn.CommandArgument;
        //Session["DocNo"] = "{Sms_vwEOIReport.Franch_Id}=" + strDocNumber;
        //Session["LastPage"] = "Default.aspx";
        //Response.Redirect("rptHandingOver.aspx");



    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls();
        bindgrid();
    }

    protected void btn_PrintAll_Click(object sender, EventArgs e)
    {
        //Session["DocNo"] = "";
        //Session["LastPage"] = "Default.aspx";
        //Response.Redirect("rptHandingOver.aspx");

    }
    protected void btnShowDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string s = Session["RegionID"].ToString();
            if (String.IsNullOrEmpty(Session["RegionID"].ToString()))
                bllObj.Region_Id = 0;
            else
                bllObj.Region_Id = Convert.ToInt32(Session["RegionID"].ToString());
            bllObj.PMonth = ddlMonths.SelectedValue;
            DataTable dt = bllObj.CalendarFetchAllDetails(bllObj);
            if (dt.Rows.Count > 0)
            {
                gvdetail.DataSource = dt;
                gvdetail.DataBind();
                divAdd.Visible = false;
                divshow.Visible = false;
                btnCancel.Visible = true;
            }
            else
            {
                ImpromptuHelper.ShowPrompt("No record to show!");
                return;
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            divAdd.Visible = true;
            divshow.Visible = true;
            gvdetail.DataSource = null;
            gvdetail.DataBind();
            btnCancel.Visible = false;

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvdetail_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvdetail.Rows.Count > 0)
            {
                gvdetail.UseAccessibleHeader = false;
                gvdetail.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
}
