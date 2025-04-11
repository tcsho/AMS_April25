﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class Center_SpecialShiftTiming : System.Web.UI.Page
{
    BLLCenterShifts_SpecialCases objcenter = new BLLCenterShifts_SpecialCases();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                ViewState["tMoodLate"] = "uncheck";
                ViewState["SortDirection"] = "ASC";
                int UserLevel, UserType;

                if (Session["employeeCode"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }

                UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
                UserType = Convert.ToInt32(Session["UserType"].ToString());
                loadMonths();
                loadRegions();
               ////// loadCenters();
                ///ddlCenter.SelectedIndex = 0;
                BindGrid();
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("ErrorPage.aspx", false);
            }
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
            //ddlCenter.Items.Insert(0, new ListItem("Select Region", "0"));
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

    protected void loadCenters()
    {
        try
        {
            BLLVacationTimigs objBll = new BLLVacationTimigs();
            DataTable _dt = new DataTable();
            objBll.Region_id = Convert.ToInt32(this.ddlRegion.SelectedValue);

            if (ViewState["Centers"] == null)
                _dt = objBll.fetchCenters(objBll);
            else
                _dt = (DataTable)ViewState["Centers"];
            gvCenter.DataSource = _dt;
            gvCenter.DataBind();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void btnAddNewPlan_Click(object sender, EventArgs e)
    {
        try
        {
            loadCenters();
            ViewState["Mode"] = "Add";
            txtAttDate.Text = DateTime.Now.ToShortDateString();
            txtEnd.Text = "";
            txtMargin.Text = "";
            txtRemarks.Text = "";
            txtStart.Text = "";
            txtTchEnd.Text = "";
            txttchStart.Text = "";
            txtAbsent.Text = "";
            AddNew.Visible = true;
            gridPanel.Visible = false;
            
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
           /// ddlCenter.SelectedValue = "0";
            AddNew.Visible = false;
            gridPanel.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Mode"].ToString() == "Add")
            {
                if (!String.IsNullOrEmpty(txtAttDate.Text))
                {
                    DateTime dt = DateTime.ParseExact(txtAttDate.Text, "M/d/yyyy", null);
                    objcenter.AttDate = dt.Date;
                    objcenter.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
                    objcenter.StartTime = txtStart.Text;
                    objcenter.EndTime = txtEnd.Text;
                    objcenter.Margin = Convert.ToInt32(txtMargin.Text);
                    objcenter.AbsentTime = txtAbsent.Text;
                    objcenter.TchrSTime = txttchStart.Text;
                    objcenter.TchrETime = txtTchEnd.Text;
                    objcenter.Remarks = txtRemarks.Text;
                    objcenter.CreateBy = Convert.ToInt32(Session["User_Id"].ToString());
                    int k;
                    foreach (GridViewRow r in gvCenter.Rows)
                    {
                        CheckBox cb= (CheckBox)r.FindControl("cbAllow");
                        if (cb.Checked == true)
                        {
                            objcenter.Center_Id = Convert.ToInt32(r.Cells[2].Text);
                            k = objcenter.CenterShifts_SpecialCasesInsertDetails(objcenter);
                        }
                    }
                    AddNew.Visible = false;
                }

                else
                {
                    ImpromptuHelper.ShowPrompt("Please mention Attendance Date to Proceed");
                }
            }
            else if (ViewState["Mode"].ToString() == "Edit")
            {
                objcenter.CenterShifts_SpecialCases_ID = Convert.ToInt32(ViewState["Id"].ToString());
                objcenter.AttDate = Convert.ToDateTime(txtAttDate.Text);

                if (!String.IsNullOrEmpty(lblhidden.Text))
                    objcenter.Center_Id = Convert.ToInt32(lblhidden.Text);
                else
                    return;
                // objcenter.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
                objcenter.StartTime = txtStart.Text;
                objcenter.EndTime = txtEnd.Text;
                objcenter.Margin = Convert.ToInt32(txtMargin.Text);
                objcenter.AbsentTime = txtAbsent.Text;
                objcenter.TchrSTime = txttchStart.Text;
                objcenter.TchrETime = txtTchEnd.Text;
                objcenter.Remarks = txtRemarks.Text;
                //update store procedure 
                objcenter.CenterShifts_SpecialCasesUpdate(objcenter);

            }
            ddlCenter.SelectedValue = "0";
            ViewState["Details"] = null;
            BindGrid();
            gridPanel.Visible = true;
            AddNew.Visible = false;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvCenters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            if (e.Row.Cells[4].Text == "True")
            {
                //e.Row.BackColor = System.Drawing.Color.Khaki;
                e.Row.Enabled = false;
            }
        }
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

    protected void BindGrid()
    {
        try
        {
            objcenter.PMonth = ddlMonths.SelectedValue;
            objcenter.Region_Id = Convert.ToInt32(ddlRegion.SelectedValue);
            objcenter.Center_Id = 0;//Convert.ToInt32(ddlCenter.SelectedValue);
            DataTable dt = new DataTable();
            if (ViewState["Details"] != null)
            {
                dt = (DataTable)ViewState["Details"];
            }
            else
            {
                dt = objcenter.CenterShifts_SpecialCasesSelectAll(objcenter);
                ViewState["Details"] = dt;
            }
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
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
            ImageButton btndel = (ImageButton)sender;
            objcenter.CenterShifts_SpecialCases_ID = Convert.ToInt32(btndel.CommandArgument);
            int k = objcenter.CenterShifts_SpecialCasesDelete(objcenter);
            ViewState["Details"] = null;
            BindGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        try
        {
            gvCenter.PageIndex = e.NewPageIndex;
            loadCenters();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvCenter_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable oDataSet = (DataTable)ViewState["Centers"];
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
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    
    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        try
        {
            gvDetails.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable oDataSet = (DataTable)ViewState["Details"];
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
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            gvCenter.DataSource = null;
            gvCenter.DataBind();
            lblCenter.Visible = true;
            trNote.Visible = false;
            ViewState["Mode"] = "Edit";
            txtRemarks.Focus();
            AddNew.Visible = true;
            ImageButton btnEdit = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvDetails.SelectedIndex = gvr.RowIndex;
            objcenter.CenterShifts_SpecialCases_ID = Convert.ToInt32(gvr.Cells[0].Text);
            ViewState["Id"] = objcenter.CenterShifts_SpecialCases_ID;
            txtAttDate.Text = gvr.Cells[2].Text;
            lblhidden.Text = gvr.Cells[3].Text;
            lblCenter.Text = "&nbsp;&nbsp;&nbsp;" + lblCenter.Text + " " + gvr.Cells[3].Text + " - " + gvr.Cells[4].Text;
            txtStart.Text = gvr.Cells[6].Text;
            txtEnd.Text = gvr.Cells[7].Text;
            txtMargin.Text = gvr.Cells[8].Text;
            txtAbsent.Text = gvr.Cells[9].Text;
            txttchStart.Text = gvr.Cells[10].Text;
            txtTchEnd.Text = gvr.Cells[11].Text;
            if (gvr.Cells[12].Text == "&nbsp;")
            {
                txtRemarks.Text = "";
            }
            else
            {
                txtRemarks.Text = gvr.Cells[12].Text;
            }
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
            ViewState["Details"] = null;
            BindGrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
}