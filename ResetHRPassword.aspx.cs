using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class ResetHRPassword : System.Web.UI.Page
{
   
    DALBase objbase = new DALBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            int UserLevel, UserType;
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
                        //loadMonths();
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

    public void loadRegions()
    {
        BLLRegion objBll = new BLLRegion();
        DataTable _dt = new DataTable();

        _dt = objBll.RegionFetch(objBll);

        ddlRegion.DataTextField = "Region_Name";
        ddlRegion.DataValueField = "Region_Id";
        ddlRegion.DataSource = _dt;
        ddlRegion.DataBind();

        if(Session["RegionID"] != null)
            ddlRegion.SelectedValue = Session["RegionID"].ToString();
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
    protected void loadCenters()
    {

        BLLCenter objBll = new BLLCenter();
        DataTable _dt = new DataTable();

        objBll.Region_Id = Convert.ToInt32(this.ddlRegion.SelectedValue);
        _dt = objBll.CenterFetch(objBll);

        ddlCenter.DataTextField = "Center_Name";
        ddlCenter.DataValueField = "Center_ID";
        ddlCenter.DataSource = _dt;
        ddlCenter.DataBind();

        ddlCenter.Items.Insert(0, new ListItem("Select Center", "0"));



        ViewState["dtMain"] = null;
        bindgrid();
        //loadEmployees();
        //DateTime.TryParse(
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCenters();
        

    }
    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dtMain"] = null;
        bindgrid();

    }
    protected void bindgrid()
    {
        BLLUser objBll = new BLLUser();
        //loadEmpleave();
        gvShifts.DataSource = null;
        //gvShow.DataSource = null;
        try
        {

            DataTable dt = new DataTable();

            objBll.User_Type_Id = 25; // HR users
            objBll.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
            objBll.Center_Id = Convert.ToInt32(ddlCenter.SelectedValue);

            if (ViewState["dtMain"] == null)
                dt = objBll.UserFetchByUserTypeId(objBll);
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


    protected void btnPassChange_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {

        ImageButton imgbtn = (ImageButton)sender;

        BLLUser objbll = new BLLUser();
        objbll.User_Name = imgbtn.CommandArgument;
        objbll.Password = "123456";
        int AlreadyIn = objbll.UserUpdate(objbll);

        if (AlreadyIn == 0)
        {
            // drawMsgBox("Error to reset Password!",2);
              ImpromptuHelper.ShowPrompt("Error to reset Password!");

        }
        else if (AlreadyIn == 1)
        {
            drawMsgBox("Password Changed Successfully.", 1);
        }
        ViewState["dtMain"] = null;
        bindgrid();
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


}