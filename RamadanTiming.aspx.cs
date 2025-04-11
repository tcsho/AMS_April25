using ADG.JQueryExtenders.Impromptu;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class RamadanTiming : System.Web.UI.Page
{
    BLLRamadanTiming objcenter = new BLLRamadanTiming();
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
                loadCenters();
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
            if (Session["RegionID"] != null)
            {
                ddlRegion.SelectedValue = Session["RegionID"].ToString();
            }
            else
            {
                ddlRegion.SelectedValue = "0";
            }

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
            if (Session["CenterID"] != null && !String.IsNullOrEmpty(Session["CenterID"].ToString()))
            {
                ddlCenter.SelectedValue = Session["CenterID"].ToString();
            }
            else
            {
                ddlCenter.SelectedValue = "0";
            }
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
            var date = DateTime.Now.ToString("dd/MM/yyyy");
            txtfromDate.Text = "";
            txtTodate.Text = "";
            txtStart.Text = "";
            txtEnd.Text = "";
            txtFridayETime.Text = "";
            txtFridaySTime.Text = "";
            txtRemarks.Text = "";

            txtTchEnd.Text = "";
            txttchStart.Text = "";
            txtTeacherFETime.Text = "";
            txtTeacherFSTime.Text = "";
            txtNOETime.Text = "";
            txtNOSTime.Text = "";
            txtNOFSTime.Text = "";
            txtNOFETime.Text = "";
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
    protected void gvDetails_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvDetails.Rows.Count > 0)
            {
                gvDetails.UseAccessibleHeader = false;
                gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Mode"].ToString() == "Add")
            {
                if (!String.IsNullOrEmpty(txtfromDate.Text) && !String.IsNullOrEmpty(txtTodate.Text))
                {
                    DateTime dtstart = DateTime.ParseExact(txtfromDate.Text, "d/M/yyyy", null);
                    DateTime dtend = DateTime.ParseExact(txtTodate.Text, "d/M/yyyy", null);
                    objcenter.StartDate = dtstart;
                    objcenter.EndDate = dtend;
                    objcenter.Region_ID = Convert.ToInt32(ddlRegion.SelectedValue);

                    objcenter.StartTime = txtStart.Text;
                    objcenter.EndTime = txtEnd.Text;
                    objcenter.FridayEndTime = txtFridayETime.Text;
                    objcenter.FridayStartTime = txtFridaySTime.Text;

                    objcenter.AbsentTime = txtAbsent.Text;

                    objcenter.TeacherStartTime = txttchStart.Text;
                    objcenter.TeacherEndTime = txtTchEnd.Text;
                    objcenter.TeacherFridayStartTime = txtTeacherFSTime.Text;
                    objcenter.TeacherFridayEndTime = txtTeacherFETime.Text;
                    objcenter.TeacherAbsentTime = txtteachAbsentTime.Text;
                    objcenter.NOStart_Time = txtNOSTime.Text;
                    objcenter.NOEnd_Time = txtNOETime.Text;
                    objcenter.NOFridaySTime = txtNOFSTime.Text;
                    objcenter.NOFridayETime = txtNOFETime.Text;
                    objcenter.NOAbsentTime = txtAbsent.Text;
                    objcenter.SaturdayEndTime = txtSaturdaETime.Text;
                    objcenter.SaturdayStartTime = txtSaturdaSTime.Text;
                    objcenter.Remarks = txtRemarks.Text;
                    objcenter.CreatedBy = Convert.ToInt32(Session["User_Id"].ToString());
                    int k;
                    foreach (GridViewRow r in gvCenter.Rows)
                    {
                        CheckBox cb = (CheckBox)r.FindControl("cbAllow");
                        if (cb.Checked == true)
                        {
                            objcenter.Center_ID = Convert.ToInt32(r.Cells[2].Text);
                            k = objcenter.RamadanTimingAdd(objcenter);
                        }
                    }
                    AddNew.Visible = false;
                }

                else
                {
                    ImpromptuHelper.ShowPrompt("Please mention Attendance Date to Proceed");
                }
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
            objcenter.Month = ddlMonths.SelectedValue;
            objcenter.Region_ID = Convert.ToInt32(ddlRegion.SelectedValue);
            objcenter.Center_ID = 0;// Convert.ToInt32(ddlCenter.SelectedValue);
            DataTable dt = new DataTable();
            if (ViewState["Details"] != null)
            {
                dt = (DataTable)ViewState["Details"];
            }
            else
            {
                dt = objcenter.RamadanTimingFetch(objcenter);
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
            objcenter.RT_Id = Convert.ToInt32(btndel.CommandArgument);
            int k = objcenter.RamadanTimingDelete(objcenter);
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