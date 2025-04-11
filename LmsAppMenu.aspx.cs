using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ADG.JQueryExtenders.Impromptu;

public partial class LmsAppMenu : System.Web.UI.Page
{
    BLLLmsAppMenu objBll = new BLLLmsAppMenu();
    DALBase objBase = new DALBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
            /*DALBase objBase = new DALBase();
            isl_amso.dt_AuthenticateUserRow row = (isl_amso.dt_AuthenticateUserRow)Session["rightsRow"];
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            if (objBase.ApplyPageAccessSettings(sRet, row.User_Type_Id) == false)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }*/

            //====== End Page Access settings ======================

            pan_New.Attributes.CssStyle.Add("display", "none");

            ViewState["SortDirection"] = "ASC";
            ViewState["mode"] = "Add";



            bindgrid();
            FillParentMenu();
            FillPage();

        }

    }

    protected void bindgrid()
    {

        objBll.Status_id = 1;
        DataTable _dt = new DataTable();
        if (ViewState["countries"] == null)
            _dt = objBll.LmsAppMenuFetchByStatusId(objBll);
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
            int id = 0;

            //        DALCountry oDALCountry = new DALCountry();

            objBll.MenuName = this.txt_CName.Text;
            objBll.MenuText = this.txt_CName.Text;
            objBll.PageID = Convert.ToInt32(ddlPage.SelectedValue);
            objBll.PrntMenu_ID = Convert.ToInt32(ddlParentMenu.SelectedValue);


            int nAlreadyIn;
            if (mode != "Edit")
            {

                nAlreadyIn = objBll.LmsAppMenuAdd(objBll);
                if (nAlreadyIn != 0)
                {
                    if (nAlreadyIn == 1)
                        drawMsgBox("Menu Name already Exists.");
                    //                else if (nAlreadyIn == 2)
                    //                    drawMsgBox("Menu Code already Exists");

                }
                else
                {
                    ViewState["countries"] = null;
                    bindgrid();
                    pan_New.Attributes.CssStyle.Add("display", "none");
                    error.Visible = false;
                    drawMsgBox("Menu Sucessfully Created.");

                }
            }
            else
            {
                objBll.Menu_ID = Convert.ToInt32(ViewState["EditID"]);
                nAlreadyIn = objBll.LmsAppMenuUpdate(objBll);

                if (nAlreadyIn != 0)
                {
                    //error.Visible = true;
                    //error.Text = "Record alrady Exist.";
                    //drawMsgBox("Country already Exists.");
                    //                if (nAlreadyIn == 1)
                    //                   drawMsgBox("Menu Name already Exists.");
                    //              else if (nAlreadyIn == 2)
                    //                drawMsgBox("Country Code already Exists");

                    drawMsgBox("Menu already exists in the selected location.");

                }
                else
                {
                    ViewState["countries"] = null;
                    bindgrid();
                    pan_New.Attributes.CssStyle.Add("display", "none");
                    error.Visible = false;
                    drawMsgBox("Menu Sucessfully Updated.");
                }
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
            objBll.Menu_ID = Convert.ToInt32(dv_country.Rows[e.RowIndex].Cells[0].Text);
            objBll.LmsAppMenuDelete(objBll);

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
            "confirm('This action cannot be undone. Are you sure you want to delete this menu?') ");
        }
    }

    protected void but_new_Click1(object sender, EventArgs e)
    {
        FillParentMenu();
        pan_New.Attributes.CssStyle.Add("display", "inline");
        ViewState["mode"] = "Add";
        txt_CName.Text = "";
        ddlPage.SelectedValue = "0";
        ddlParentMenu.SelectedValue = "0";

        error.Visible = false;
        dv_country.SelectedRowStyle.Reset();
    }


    protected void dv_country_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //DataTable dt = (DataTable)dv_country.DataSource;
            //ddlPage.SelectedValue = dt.Rows[e.NewSelectedIndex]["Page_ID"].ToString();

            this.txt_CName.Text = this.dv_country.Rows[e.NewSelectedIndex].Cells[4].Text;
            this.ddlParentMenu.SelectedValue = this.dv_country.Rows[e.NewSelectedIndex].Cells[2].Text;
            if (ddlPage.Items.FindByValue(this.dv_country.Rows[e.NewSelectedIndex].Cells[1].Text) != null)
                this.ddlPage.SelectedValue = this.dv_country.Rows[e.NewSelectedIndex].Cells[1].Text;
            else
                ddlPage.SelectedValue = "0";

            ViewState["mode"] = "Edit";
            ViewState["EditID"] = this.dv_country.Rows[e.NewSelectedIndex].Cells[0].Text;
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

        objBll.Status_id = 1;
        DataTable _dt = new DataTable();
        _dt = objBll.LmsAppMenuFetchByStatusId(objBll);
        objBase.FillDropDown(_dt, ddlParentMenu, "Menu_ID", "MenuText");

    }

    protected void FillPage()
    {

        DataTable dt = new DataTable();
        dt = objBll.LmsAppPageFetch();
        objBase.FillDropDown(dt, ddlPage, "Page_Id", "PageTitle");


    }







}

