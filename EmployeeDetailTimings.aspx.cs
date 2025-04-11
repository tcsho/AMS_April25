using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeDetailTimings : System.Web.UI.Page
{
    BLLDetailTimings objDetail = new BLLDetailTimings();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeCode"] != null)
        {
            //Removed Page Settings for now
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
                if (Convert.ToBoolean(_dtSettings.Rows[0]["isAllow"]) == false)
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                }

                //====== End Page Access settings ======================

                DataTable dt = new DataTable();
                if (ViewState["DetailTimings"] != null)
                {
                    dt = (DataTable)ViewState["Details"];
                }
                else
                {
                    dt = objDetail.DetailTimingsFetch(objDetail);
                    ViewState["DetailTimings"] = dt;
                }
                gvDetailTimings.DataSource = dt;
                gvDetailTimings.DataBind();

                ViewState["chkAllCenters"] = "check";
                ViewState["chkAllDesignations"] = "check";
                chkDesignations.Visible = true;
                ListOfEmployeesPanel.Attributes.CssStyle.Add("display","none");
                
            }
            else
            {
                gvEmployeesAppliedTimings.DataSource = null;
                gvEmployeesAppliedTimings.DataBind();
                ListOfEmployeesPanel.Attributes.CssStyle.Add("display", "none");
                gvEmployeesAppliedTimings.Visible = false;

            }

        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void loadCenters()
    {
        try
        {
            DataTable _dt = new DataTable();

            objDetail.Region_id = Convert.ToInt32(Session["RegionID"]);
            _dt = objDetail.fetchCenters(objDetail);
            gvSpecificCenters.DataSource = _dt;
            gvSpecificCenters.DataBind();

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
        foreach (GridViewRow gvrow in gvSpecificCenters.Rows)
        {
            index = Convert.ToInt32(gvSpecificCenters.DataKeys[gvrow.RowIndex].Value);
            bool result = ((CheckBox)gvrow.FindControl("cbSelect")).Checked;
            string id = ((CheckBox)gvrow.FindControl("cbSelect")).ClientID;

            // Check in the Session
            if (Session["CHECKED_CENTERS"] != null)
                userdetails = (ArrayList)Session["CHECKED_CENTERS"];
            if (result)
            {
                if (!userdetails.Contains(index))
                    userdetails.Add(index);
            }
            else
                userdetails.Remove(index);
        }
        if (userdetails != null && userdetails.Count > 0)
            Session["CHECKED_CENTERS"] = userdetails;
    }
    private void RePopulateValues()
    {
        ArrayList userdetails = (ArrayList)Session["CHECKED_CENTERS"];
        if (userdetails != null && userdetails.Count > 0)
        {
            foreach (GridViewRow row in gvSpecificCenters.Rows)
            {
                int index = Convert.ToInt32(gvSpecificCenters.DataKeys[row.RowIndex].Value);
                if (userdetails.Contains(index))
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("cbSelect");
                    myCheckBox.Checked = true;
                }
            }
        }
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {

            string message = string.Empty;

            List<string> centers = new List<string>();
            string centersStr = null;

            List<string> designations = new List<string>();
            string designationStr = string.Empty;

            if (!rbAllCenters.Checked)
            {

                for (int i = 0; i < gvSpecificCenters.Rows.Count; i++)
                {
                    var checkBox = (CheckBox)gvSpecificCenters.Rows[i].Cells[3].FindControl("cbSelect");

                    if (checkBox != null && checkBox.Checked)
                    {
                        centers.Add(gvSpecificCenters.Rows[i].Cells[1].Text.Trim());
                    }

                    centersStr = string.Join(",", centers);

                }
            }


            for (int i = 0; i < gvDesignations.Rows.Count; i++)
            {
                var checkBox = (CheckBox)gvDesignations.Rows[i].Cells[2].FindControl("cbSelectDesig");

                if (checkBox != null && checkBox.Checked)
                {
                    designations.Add(gvDesignations.DataKeys[i].Value.ToString().Trim());
                }

                designationStr = string.Join(",", designations);

            }

         

            if (string.IsNullOrEmpty(designationStr))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select employees!')", true);
                return;
            }

            if (!String.IsNullOrEmpty(txtfromDate.Text) && !String.IsNullOrEmpty(txtTodate.Text))
            {
                objDetail.Region_id = Convert.ToInt32(Session["RegionID"]);
                objDetail.FromDate = DateTime.ParseExact(txtfromDate.Text, "d/M/yyyy", null).Date;
                objDetail.ToDate = DateTime.ParseExact(txtTodate.Text, "d/M/yyyy", null).Date;
                objDetail.Reason = txtReason.Text;
                objDetail.TimeIn = TimeSpan.Parse(txtTimeIn.Text);
                objDetail.TimeOut = TimeSpan.Parse(txtTimeOut.Text);
                objDetail.AbsentTime = TimeSpan.Parse(txtAbsentTime.Text);
                objDetail.FriStartTime = TimeSpan.Parse(txtAbsentTime.Text);
                objDetail.FriAbsentTime = TimeSpan.Parse(txtAbsentTime.Text);
                objDetail.FriEndTime = TimeSpan.Parse(txtAbsentTime.Text);
                objDetail.SatStartTime = TimeSpan.Parse(txtAbsentTime.Text);
                objDetail.SatAbsentTime = TimeSpan.Parse(txtAbsentTime.Text);
                objDetail.SatEndTime = TimeSpan.Parse(txtAbsentTime.Text);
                objDetail.Status_Id = 1;
                objDetail.CreatedBy = Convert.ToInt32(Session["User_Id"].ToString());
                objDetail.CreatedOn = DateTime.Now;
                objDetail.ModifiedBy = null;
                objDetail.ModifiedOn = null;
                if (chkLock.Checked)
                {
                    objDetail.isLocked = true;
                    objDetail.LockedOn = DateTime.Now;
                    objDetail.LockedBy = Convert.ToInt32(Session["User_Id"].ToString());
                }
                else
                {
                    objDetail.isLocked = false;
                    objDetail.LockedOn = null;
                    objDetail.LockedBy = null;
                }
                int? k = null;


                objDetail.ShiftCaseDetailList = new List<BLLDateDetail>();
                double counter = 0;



                while (objDetail.FromDate.AddDays(counter) <= objDetail.ToDate)
                {
                    BLLDateDetail obj = new BLLDateDetail()
                    {
                        AttDate = objDetail.FromDate.AddDays(counter),
                        isOff = false
                    };
                    objDetail.ShiftCaseDetailList.Add(obj);
                    counter++;
                }

                k = objDetail.DetailTimingsAdd(centersStr,designationStr);

                if (k != null)
                {
                    txtfromDate.Text = string.Empty;
                    txtTodate.Text = string.Empty;
                }

                ViewState["DetailTimings"] = null;
                BindGrid();
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
        DataTable dt = new DataTable();
        if (ViewState["DetailTimings"] != null)
        {
            dt = (DataTable)ViewState["Details"];
        }
        else
        {
            dt = objDetail.DetailTimingsFetch(objDetail);
            ViewState["DetailTimings"] = dt;
        }
        gvDetailTimings.DataSource = dt;
        gvDetailTimings.DataBind();
    }

    
    DataTable dateDetailTable = new DataTable();
    BLLDateDetail objDateDetail = new BLLDateDetail();
    protected void btnDateDetails_Click(object sender, EventArgs e)
    {
        try
        {

            DateDetailPanel.Visible = true;
            Button btnDateDetails = (Button)sender;
            objDateDetail.ShiftCaseId = Convert.ToInt32(btnDateDetails.CommandArgument);
            Session["ShiftCaseId"] = objDateDetail.ShiftCaseId;
            ShowData();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvDateDetail_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        gvDateDetail.EditIndex = e.NewEditIndex;
        ShowData();
    }
    protected void gvDateDetail_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        //Finding the controls from Gridview for the row which is going to update  

        Label lblShiftCaseDateId = gvDateDetail.Rows[e.RowIndex].FindControl("lblShiftCaseDateId") as Label;
        TextBox timeInTextBox = gvDateDetail.Rows[e.RowIndex].FindControl("txtDateDetailTimeIn") as TextBox;
        TextBox timeOutTextBox = gvDateDetail.Rows[e.RowIndex].FindControl("txtDateDetailTimeOut") as TextBox;
        TextBox absentTimeTextBox = gvDateDetail.Rows[e.RowIndex].FindControl("txtDateDetailAbsentTime") as TextBox;

        objDateDetail.ShiftCaseDateId = Convert.ToInt32(lblShiftCaseDateId.Text);
        objDateDetail.TimeIn = TimeSpan.Parse(timeInTextBox.Text);
        objDateDetail.TimeOut = TimeSpan.Parse(timeOutTextBox.Text);
        objDateDetail.AbsentTime = TimeSpan.Parse(absentTimeTextBox.Text);

        objDateDetail.DateDetailTimingsUpdate();

        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        gvDateDetail.EditIndex = -1;

        //Call ShowData method for displaying updated data  
        ShowData();
    }
    protected void gvDateDetail_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        gvDateDetail.EditIndex = -1;
        ShowData();
    }

    private void ShowData()
    {
        objDateDetail.ShiftCaseId = Convert.ToInt32(Session["ShiftCaseId"]);
        dateDetailTable = objDateDetail.DetailTimingsFetchAll(objDateDetail.ShiftCaseId);
        gvDateDetail.DataSource = dateDetailTable;
        gvDateDetail.DataBind();
        gvDateDetail.Visible = true;
    }

    protected void btnEmployeeDetails_Click(object sender, EventArgs e)
    {
        gvDateDetail.DataSource = null;
        gvDateDetail.DataBind();
        gvDateDetail.Visible = false;
        DateDetailPanel.Visible = false;
        Button btnEmployeesList = (Button)sender;
        int ShiftCaseId = Convert.ToInt32(btnEmployeesList.CommandArgument);
        Session["ShiftCaseId"] = ShiftCaseId;
        bindEmployees();
    }

    private void bindEmployees()
    {
        DataTable dt = new DataTable();
        int ShiftCaseId = Convert.ToInt32(Session["ShiftCaseId"]);
        dt = objDetail.EmployeesTimingAppliedTo(ShiftCaseId);
        gvEmployeesAppliedTimings.DataSource = dt;
        gvEmployeesAppliedTimings.DataBind();
        ListOfEmployeesPanel.Attributes.CssStyle.Add("display", "inline");
        gvEmployeesAppliedTimings.Visible = true;
    }
    protected void gvCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvEmployeesAppliedTimings.PageIndex = e.NewPageIndex;
            bindEmployees();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvCenters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string chkCenter = ViewState["chkAllCenters"].ToString();

                foreach (GridViewRow gvr in gvSpecificCenters.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("cbSelect");
                    if (chkCenter == "" || chkCenter == "check")
                    {
                        cb.Checked = true;
                        ViewState["chkAllCenters"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["chkAllCenters"] = "check";
                    }

                }
                if (cb.Checked)
                {
                    loadAllDesignations();
                }
                else
                {
                    gvDesignations.DataSource = null;
                    gvDesignations.DataBind();
                    gvDesignations.Visible = false;
                    DesignationsPanel.Attributes.CssStyle.Add("display", "none");

                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void rbSpecificCenters_CheckedChanged(object sender, EventArgs e)
    {
        chkDesignations.Checked = false;
        chkDesignations.Visible = false;
        AddNew.Visible = true;
        if (rbSpecificCenters.Checked && !chkDesignations.Checked)
        {
            gvDesignations.DataSource = null;
            gvDesignations.DataBind();
            gvDesignations.Visible = false;
            DesignationsPanel.Attributes.CssStyle.Add("display", "none");
        }
        CentersPanel.Attributes.CssStyle.Add("display", "inline");
        gvSpecificCenters.Visible = true;
        loadCenters();

    }

    protected void rbAllCenters_CheckedChanged(object sender, EventArgs e)
    {
        chkDesignations.Visible = true;
        gvDesignations.DataSource = null;
        gvDesignations.DataBind();
        CentersPanel.Attributes.CssStyle.Add("display", "none");
        gvSpecificCenters.Visible = false;
        if (rbAllCenters.Checked && chkDesignations.Checked)
        {
            loadAllDesignations();

        }
        else
        {
            gvDesignations.Visible = false;
            DesignationsPanel.Attributes.CssStyle.Add("display", "none");
        }
    }

    private void loadAllDesignations()
    {
        objDetail.Region_id = Convert.ToInt32(Session["RegionID"]);
        DesignationsPanel.Attributes.CssStyle.Add("display", "inline");
        gvDesignations.DataSource = objDetail.EmployeeAllDesignationSelectByRegionCenter();
        gvDesignations.DataBind();
        gvDesignations.Visible = true;
    }

    protected void gvDesignations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string chkDesig = ViewState["chkAllDesignations"].ToString();

                foreach (GridViewRow gvr in gvDesignations.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("cbSelectDesig");
                    if (chkDesig == "" || chkDesig == "check")
                    {
                        cb.Checked = true;
                        ViewState["chkAllDesignations"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["chkAllDesignations"] = "check";
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

    protected void cbSelect_CheckedChanged(object sender, EventArgs e)
    {
        List<string> selectedCenters = new List<string>();

        for (int index = 0; index < gvSpecificCenters.Rows.Count; index++)
        {

            CheckBox checkBox = (CheckBox)gvSpecificCenters.Rows[index].Cells[3].FindControl("cbSelect");

            if (checkBox != null && checkBox.Checked)
            {
                selectedCenters.Add(gvSpecificCenters.Rows[index].Cells[1].Text);
            }

        }

        string centers = string.Join(",", selectedCenters);
        objDetail.Region_id = Convert.ToInt32(Session["RegionID"]);

        gvDesignations.DataSource = objDetail.EmployeeDesignationsSelectByCenters(centers);
        gvDesignations.DataBind();
        if (selectedCenters.Count > 0)
        {
            DesignationsPanel.Attributes.CssStyle.Add("display", "inline");
            gvDesignations.Visible = true;
        }
        else
        {
            DesignationsPanel.Attributes.CssStyle.Add("display", "none");
            gvDesignations.Visible = false;
        }

    }


    protected void chkDesignations_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            objDetail.Region_id = Convert.ToInt32(Session["RegionID"]);
            if (rbAllCenters.Checked && chkDesignations.Checked)
            {
                DesignationsPanel.Attributes.CssStyle.Add("display", "inline");
                gvDesignations.DataSource = objDetail.EmployeeAllDesignationSelectByRegionCenter();
                gvDesignations.DataBind();
                gvDesignations.Visible = true;
                AddNew.Visible = true;
            }
            else
            {
                gvDesignations.DataSource = null;
                gvDesignations.DataBind();
                gvDesignations.Visible = false;
                DesignationsPanel.Attributes.CssStyle.Add("display", "none");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }


    protected void gvEmployeesAppliedTimings_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvEmployeesAppliedTimings.Rows.Count > 0)
            {
                gvEmployeesAppliedTimings.UseAccessibleHeader = false;
                gvEmployeesAppliedTimings.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btndel = (ImageButton)sender;
            int ShiftCaseEmpId = Convert.ToInt32(btndel.CommandArgument);
            int k = objDetail.EmployeesAppliedTimingDelete(ShiftCaseEmpId);
            bindEmployees();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
}