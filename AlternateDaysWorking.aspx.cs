using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AlternateDaysWorking : System.Web.UI.Page
{
    BLLAlternateDaysWorking objBll = new BLLAlternateDaysWorking();
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

    public void loadRegions()
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


    protected void loadCenters()
    {

        BLLVacationTimigs objBll = new BLLVacationTimigs();
        DataTable _dt = new DataTable();

        objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);
        _dt = objBll.fetchCenters(objBll);

        ddlCenter.DataTextField = "Center_Name";
        ddlCenter.DataValueField = "Center_ID";
        ddlCenter.DataSource = _dt;
        ddlCenter.DataBind();

        ddlCenter.Items.Insert(0, new ListItem("Select Center", "0"));



        ViewState["dtMain"] = null;
        bindgrid();
        //DateTime.TryParse(
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
                dt = objBll.fetchAlternateDaysWorking(objBll);
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
        ViewState["dtMain"] = null;
        ViewState["dtMainShow"] = null;
        bindgrid();
    }

    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dtMain"] = null;
        bindgrid();

        //if (ddlCenter.SelectedValue != "0")
        //{
        //    btnAddNewAlternateDaysWorking.Attributes.CssStyle.Add("display", "inline");
        //}
        //else
        //{
        //    btnAddNewAlternateDaysWorking.Attributes.CssStyle.Add("display", "none");
        //}
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
        this.objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);
        this.objBll.Center_id = Convert.ToInt32(this.ddlCenter.SelectedValue);

        this.objBll.Off_day = Convert.ToDateTime(this.txtOffDay.Text);
        this.objBll.Alternate_working_day = Convert.ToDateTime(this.txtAlternateWorkingDay.Text);
        this.objBll.Reason = this.txt_Reason.Text;
        
        this.objBll.Inserted_by = Session["RegionID"].ToString();


        int nAlreadyIn = 0;
        try
        {
            nAlreadyIn = objBll.AlternateDaysWorkingInsert(objBll);

            if (nAlreadyIn != -2)
            {
                drawMsgBox("Data saved successfully.", 3);
            }
            else
            {
                drawMsgBox("Data already exist in these dates.", 3);
            }


        }
        catch (Exception ex)
        {
            throw;
        }

        clearForm();
        pan_New.Attributes.CssStyle.Add("display", "none");
        btnAddNewAlternateDaysWorking.Attributes.CssStyle.Add("display", "inline");

        ViewState["dtMain"] = null;
        bindgrid();
    }
    protected void clearForm()
    {
        this.txtOffDay.Text = "";
        this.txtAlternateWorkingDay.Text = "";
        this.txt_Reason.Text = "";

    }

    protected void btnSaveVacation_Click(object sender, EventArgs e)
    {
        if (validateControls())
        {

            Save();
        }

        //btnAddNewAlternateDaysWorking.Attributes.CssStyle.Add("display", "inline");
    }

    protected bool validateControls()
    {
        if (this.txtOffDay.Text == "")
        {
            drawMsgBox("Off Day Date is required.", 0);
            return false;
        }

        if (this.txtAlternateWorkingDay.Text == "")
        {
            drawMsgBox("Alternate Working Day's Date is required.", 0);
            return false;
        }

        if (this.txtOffDay.Text == this.txtAlternateWorkingDay.Text)
        {
            drawMsgBox("Off 'Day Date' and 'Alternate Working Day's Date' can't be the same.", 0);
            return false;
        }


        if (this.txt_Reason.Text == "")
        {
            drawMsgBox("Reason is required.", 0);
            return false;
        }

       

        return true;
    }




    protected void btnAddNewAlternateDaysWorking_Click(object sender, EventArgs e)
    {

        pan_New.Attributes.CssStyle.Add("display", "inline");
        btnAddNewAlternateDaysWorking.Attributes.CssStyle.Add("display", "none");

    }


    protected void btnCancelVacation_Click(object sender, EventArgs e)
    {

        pan_New.Attributes.CssStyle.Add("display", "none");
        btnAddNewAlternateDaysWorking.Attributes.CssStyle.Add("display", "inline");
        clearForm();
    }




}