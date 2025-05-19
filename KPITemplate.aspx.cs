using ADG.JQueryExtenders.Impromptu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KPITemplate : System.Web.UI.Page
{
    BLLKPITemplate objbll = new BLLKPITemplate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                lblHeading.Text = "Edit KPI Template";
                int templateId = Convert.ToInt32(Request.QueryString["tid"]);
           
                // Call your function to fetch KPI details by templateId
                DataTable dt = objbll.KPITemplateFetchbyID(templateId);
            

                if (dt.Rows.Count > 0)
                {
                    ViewState["KPIList"] = dt;
                    txtTemplateName.Text = dt.Rows[0]["TemplateName"].ToString();
                    txtTemplateName.ReadOnly = true; // Make it readonly

                    ddlYear.Text = dt.Rows[0]["Year"].ToString();
                    ddlYear.Enabled = false;

                    DateTime fromDate = Convert.ToDateTime(dt.Rows[0]["FromDate"]);
                    txtFromDate.Text = fromDate.ToString("dd/MM/yyyy");
                    txtFromDate.ReadOnly = true;// Make it readonly


                    DateTime toDate = Convert.ToDateTime(dt.Rows[0]["ToDate"]);
                    txtToDate.Text = fromDate.ToString("dd/MM/yyyy");
                    txtToDate.ReadOnly = true;// Make it readonly

                   

                    txtTotalWeight.Text = dt.Rows[0]["TotalWeight"].ToString();
                    txtTotalWeight.ReadOnly = true;
                }
                else
                {
                    // In case no data returned, fallback to empty structure with one row
                    dt = CreateDataTable();
                    dt.Rows.Add(dt.NewRow());
                    ViewState["KPIList"] = dt;
                }

                BindGrid(); // Bind data to gvKPIInsert
            }
            else
            {
                lblHeading.Text = "Create KPI Template";
                // No query param? Use default empty row
                DataTable dt = CreateDataTable();
                dt.Rows.Add(dt.NewRow());
                ViewState["KPIList"] = dt;
                BindGrid();
            }
        }
    }
    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("KPIName");
        dt.Columns.Add("Weight");
        dt.Columns.Add("Grade5_Max");
        dt.Columns.Add("Grade5_Min");
        dt.Columns.Add("Grade4_Max");
        dt.Columns.Add("Grade4_Min");
        dt.Columns.Add("Grade3_Max");
        dt.Columns.Add("Grade3_Min");
        dt.Columns.Add("Grade2_Max");
        dt.Columns.Add("Grade2_Min");
        dt.Columns.Add("Grade1_Max");
        dt.Columns.Add("Grade1_Min");
        return dt;
    }
    private void GetGridValues()
    {
        DataTable dt = CreateDataTable();

        foreach (GridViewRow row in gvKPIInsert.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                DataRow dr = dt.NewRow();

                TextBox txtKPIName = (TextBox)row.FindControl("txtKPIName");
                dr["KPIName"] = txtKPIName != null ? txtKPIName.Text : "";

                TextBox txtWeight = (TextBox)row.FindControl("txtWeight");
                dr["Weight"] = txtWeight != null ? txtWeight.Text : "";

                TextBox txtG5Max = (TextBox)row.FindControl("txtG5Max");
                dr["Grade5_Max"] = txtG5Max != null ? txtG5Max.Text : "";

                TextBox txtG5Min = (TextBox)row.FindControl("txtG5Min");
                dr["Grade5_Min"] = txtG5Min != null ? txtG5Min.Text : "";

                TextBox txtG4Max = (TextBox)row.FindControl("txtG4Max");
                dr["Grade4_Max"] = txtG4Max != null ? txtG4Max.Text : "";

                TextBox txtG4Min = (TextBox)row.FindControl("txtG4Min");
                dr["Grade4_Min"] = txtG4Min != null ? txtG4Min.Text : "";

                TextBox txtG3Max = (TextBox)row.FindControl("txtG3Max");
                dr["Grade3_Max"] = txtG3Max != null ? txtG3Max.Text : "";

                TextBox txtG3Min = (TextBox)row.FindControl("txtG3Min");
                dr["Grade3_Min"] = txtG3Min != null ? txtG3Min.Text : "";

                TextBox txtG2Max = (TextBox)row.FindControl("txtG2Max");
                dr["Grade2_Max"] = txtG2Max != null ? txtG2Max.Text : "";

                TextBox txtG2Min = (TextBox)row.FindControl("txtG2Min");
                dr["Grade2_Min"] = txtG2Min != null ? txtG2Min.Text : "";

                TextBox txtG1Max = (TextBox)row.FindControl("txtG1Max");
                dr["Grade1_Max"] = txtG1Max != null ? txtG1Max.Text : "";

                TextBox txtG1Min = (TextBox)row.FindControl("txtG1Min");
                dr["Grade1_Min"] = txtG1Min != null ? txtG1Min.Text : "";

                dt.Rows.Add(dr);
            }
        }

        ViewState["KPIList"] = dt;
    }
    private void BindGrid()
    {
        if (ViewState["KPIList"] != null)
        {
            gvKPIInsert.DataSource = ViewState["KPIList"] as DataTable;
            gvKPIInsert.DataBind();
        }
    }
    protected void gvKPIInsert_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "InsertRow")
        {
            GetGridValues();
            DataTable dt = ViewState["KPIList"] as DataTable;
            dt.Rows.Add(dt.NewRow());
            ViewState["KPIList"] = dt;
            BindGrid();
        }
        if (e.CommandName == "DeleteRow")
        {
            DataTable dt = ViewState["KPIList"] as DataTable;
            int index = Convert.ToInt32(((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex);
            dt.Rows.RemoveAt(index);
            ViewState["KPIList"] = dt;
            BindGrid();
        }
        //BindGrid();
    }
    protected void gvKPIInsert_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow headerRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            // KPI Name - rowspan
            TableCell kpiCell = new TableCell();
            //kpiCell.Text = "KPI Name";
            kpiCell.RowSpan = 1;
            kpiCell.BackColor = System.Drawing.Color.LightGray;
            kpiCell.ForeColor = System.Drawing.Color.White;
            kpiCell.HorizontalAlign = HorizontalAlign.Center;
            headerRow.Cells.Add(kpiCell);

            TableCell kpiCell1 = new TableCell();
            //kpiCell.Text = "KPI Name";
            kpiCell1.RowSpan = 1;
            kpiCell1.BackColor = System.Drawing.Color.LightGray;
            kpiCell1.ForeColor = System.Drawing.Color.White;
            kpiCell1.HorizontalAlign = HorizontalAlign.Center;
            headerRow.Cells.Add(kpiCell1);
            // Grades
            string[] grades = { "Grade 5", "Grade 4", "Grade 3", "Grade 2", "Grade 1" };
            foreach (string grade in grades)
            {
                TableCell cell = new TableCell();
                cell.Text = grade;
                cell.ColumnSpan = 2;
                cell.BackColor = System.Drawing.Color.LightGray;
                cell.ForeColor = System.Drawing.Color.Black;
                cell.HorizontalAlign = HorizontalAlign.Center;
                headerRow.Cells.Add(cell);
            }

            gvKPIInsert.Controls[0].Controls.AddAt(0, headerRow);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal totalWeight = 0;
        bool isok = true;
        string _displymsg = "";
        if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
        {
            foreach (GridViewRow row in gvKPIInsert.Rows)
            {
                TextBox txtWeight = (TextBox)row.FindControl("txtWeight");
                decimal weight;
                if (decimal.TryParse(txtWeight.Text, out weight)) 
                {
                    totalWeight += weight;
                }
            }

            decimal expectedTotal = decimal.Parse(txtTotalWeight.Text);
            if (totalWeight != expectedTotal)
            {
                isok = false;
                _displymsg = "Total Weight of KPI List must be equal to " + expectedTotal;
            }

            if (isok)
            {
                int templateId = Convert.ToInt32(Request.QueryString["tid"]);
                BLLKPITemplate obj = new BLLKPITemplate();
                obj.TemplateId = templateId; // this was missing before

                BLLKPITemplate objbll = new BLLKPITemplate(); // Or reuse the one you already declared
                objbll.KPITemplateDetailDelete(obj);
                foreach (GridViewRow row in gvKPIInsert.Rows)
                {
                    BLLKPITemplateDetail detail = new BLLKPITemplateDetail();
                    detail.templateId = templateId;
                    detail.kpiName = ((TextBox)row.FindControl("txtKPIName")).Text.Trim();
                    detail.weight = ((TextBox)row.FindControl("txtWeight")).Text.Trim();
                    detail.grade5_max = ((TextBox)row.FindControl("txtG5Max")).Text.Trim();
                    detail.grade5_min = ((TextBox)row.FindControl("txtG5Min")).Text.Trim();
                    detail.grade4_max = ((TextBox)row.FindControl("txtG4Max")).Text.Trim();
                    detail.grade4_min = ((TextBox)row.FindControl("txtG4Min")).Text.Trim();
                    detail.grade3_max = ((TextBox)row.FindControl("txtG3Max")).Text.Trim();
                    detail.grade3_min = ((TextBox)row.FindControl("txtG3Min")).Text.Trim();
                    detail.grade2_max = ((TextBox)row.FindControl("txtG2Max")).Text.Trim();
                    detail.grade2_min = ((TextBox)row.FindControl("txtG2Min")).Text.Trim();
                    detail.grade1_max = ((TextBox)row.FindControl("txtG1Max")).Text.Trim();
                    detail.grade1_min = ((TextBox)row.FindControl("txtG1Min")).Text.Trim();

                    detail.KPITemplateDetailAdd(detail);
                }
                drawMsgBox("Data Updated successfully.", 1);
            }
            else
            {
                drawMsgBox(_displymsg, 3);
            }
        }
        else
        {
            int nAlreadyIn = 0;
            DataTable dt = new DataTable();
            var username = Session["UserName"].ToString();
            if (txtToDate.Text.Trim() != "")
            {
                isok = true;
                DataValidations();
            }
           
            if (txtFromDate.Text.Trim() == "")
            {
                isok = false;
                _displymsg = "From date is empty. !";
            }
            else if (txtToDate.Text.Trim() == "")
            {
                isok = false;
                _displymsg = "To date is empty. !";
            }
            else if (ddlYear.SelectedValue == "0")
            {
                isok = false;
                _displymsg = "Select KPI year !";
            }
            else if (txtTemplateName.Text.Trim() == "")
            {
                isok = false;
                _displymsg = "Template Name is empty. !";
            }
            foreach (GridViewRow row in gvKPIInsert.Rows)
            {
                TextBox txtWeight = (TextBox)row.FindControl("txtWeight");
                decimal weight;
                if (decimal.TryParse(txtWeight.Text, out weight))
                {
                    totalWeight += weight;
                }
            }

            decimal expectedTotal = decimal.Parse(txtTotalWeight.Text);
            if (totalWeight != expectedTotal)
            {
                isok = false;
                _displymsg = "Total Weight of KPI List must be equal to " + expectedTotal;
            }

            if (isok)
            {
                BLLKPITemplate obj = new BLLKPITemplate();

                // Master data
                obj.templateName = txtTemplateName.Text.Trim();
                obj.year = Convert.ToInt32(ddlYear.SelectedValue);
                obj.fromdate = DateTime.ParseExact(txtFromDate.Text.Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                obj.todate = DateTime.ParseExact(txtToDate.Text.Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                obj.totalweight = Convert.ToInt32(txtTotalWeight.Text.Trim());
                obj.createdby = username;
                obj.createddate = DateTime.Now;

                // Call Master Insert
                int newTemplateId = obj.KPITemplateAdd(obj);
                 
                {
                    // Save each detail row
                    foreach (GridViewRow row in gvKPIInsert.Rows)
                    {
                        BLLKPITemplateDetail detail = new BLLKPITemplateDetail();
                        detail.templateId = newTemplateId;
                        detail.kpiName = ((TextBox)row.FindControl("txtKPIName")).Text.Trim();
                        detail.weight = ((TextBox)row.FindControl("txtWeight")).Text.Trim();
                        detail.grade5_max = ((TextBox)row.FindControl("txtG5Max")).Text.Trim();
                        detail.grade5_min = ((TextBox)row.FindControl("txtG5Min")).Text.Trim();
                        detail.grade4_max = ((TextBox)row.FindControl("txtG4Max")).Text.Trim();
                        detail.grade4_min = ((TextBox)row.FindControl("txtG4Min")).Text.Trim();
                        detail.grade3_max = ((TextBox)row.FindControl("txtG3Max")).Text.Trim();
                        detail.grade3_min = ((TextBox)row.FindControl("txtG3Min")).Text.Trim();
                        detail.grade2_max = ((TextBox)row.FindControl("txtG2Max")).Text.Trim();
                        detail.grade2_min = ((TextBox)row.FindControl("txtG2Min")).Text.Trim();
                        detail.grade1_max = ((TextBox)row.FindControl("txtG1Max")).Text.Trim();
                        detail.grade1_min = ((TextBox)row.FindControl("txtG1Min")).Text.Trim();

                        // Call detail insert
                        detail.KPITemplateDetailAdd(detail);
                    }
                    drawMsgBox("Data added successfully.", 1);
                }
            }

            else
            {
                drawMsgBox(_displymsg, 3);
            }
        }
    }
    protected void drawMsgBox(string msg, int errType)
    {
        ImpromptuHelper.ShowPrompt(msg);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPITemplate_Manage.aspx", false);
    }
    protected void gvKPIInsert_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            ((TextBox)e.Row.FindControl("txtKPIName")).Text = drv["KPIName"].ToString();
            ((TextBox)e.Row.FindControl("txtWeight")).Text = drv["Weight"].ToString();
            ((TextBox)e.Row.FindControl("txtG5Max")).Text = drv["Grade5_Max"].ToString();
            ((TextBox)e.Row.FindControl("txtG5Min")).Text = drv["Grade5_Min"].ToString();
            ((TextBox)e.Row.FindControl("txtG4Max")).Text = drv["Grade4_Max"].ToString();
            ((TextBox)e.Row.FindControl("txtG4Min")).Text = drv["Grade4_Min"].ToString();
            ((TextBox)e.Row.FindControl("txtG3Max")).Text = drv["Grade3_Max"].ToString();
            ((TextBox)e.Row.FindControl("txtG3Min")).Text = drv["Grade3_Min"].ToString();
            ((TextBox)e.Row.FindControl("txtG2Max")).Text = drv["Grade2_Max"].ToString();
            ((TextBox)e.Row.FindControl("txtG2Min")).Text = drv["Grade2_Min"].ToString();
            ((TextBox)e.Row.FindControl("txtG1Max")).Text = drv["Grade1_Max"].ToString();
            ((TextBox)e.Row.FindControl("txtG1Min")).Text = drv["Grade1_Min"].ToString();
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        DataValidations();
    }
    private void DataValidations()
    {
        int days = CalculateDays(txtFromDate.Text, txtToDate.Text);
        if (days < 0)
        {
            drawMsgBox("Invalid Date Range! 'From date' can not be greater than 'To date'.", 2);
        }
        else
        {
        }
    }
    private int CalculateDays(string _fromDate, string _toDate)
    {
        int _ret = 0;
        if (txtFromDate.Text.Length > 0 && txtToDate.Text.Length > 2)
        {
            DateTime dF = DateTime.ParseExact(_fromDate, "M/d/yyyy", null);

            DateTime dT = DateTime.ParseExact(_toDate, "M/d/yyyy", null);

            TimeSpan span = dT.Subtract(dF);
            if (span.Days >= 0)
            {
                _ret = span.Days + 1;
            }
            else
            {
                _ret = span.Days;
            }
        }
        return _ret;
    }
}