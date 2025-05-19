using ADG.JQueryExtenders.Impromptu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KPI_UpdateEmpTemplate : System.Web.UI.Page
{
    BLLKPITemplate objbll = new BLLKPITemplate();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int nAlreadyIn = 0;
            DataTable dt = new DataTable();
            bool isok = true;
            string _displymsg = "";

            var username = Session["UserName"].ToString();
            if (lblEmpName.Text == "-")
            {
                isok = false;
                _displymsg = "Please Select Any Employee !";
            }
            else if (txtEmpID.Text == "")
            {
                isok = false;
                _displymsg = "Please Enter Any Employee Code !";
            }
            
            if (isok)
            {
                BLLKPITemplateAssignDetail obj = new BLLKPITemplateAssignDetail();

                obj.EmployeeID = txtEmpID.Text;
                obj.RemarksHR = txtRemarks.Text;
                obj.ModifiedBy = username;
                obj.ModifiedDate = DateTime.Now;
                obj.KPITemplateAssignDetailUpdate(obj);

                foreach (GridViewRow row in gvKPIInsert.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string assignDetailId = gvKPIInsert.DataKeys[row.RowIndex]["AssignDetailID"].ToString();
                        string detailId = gvKPIInsert.DataKeys[row.RowIndex].Value.ToString();
                        string grade5Max = ((TextBox)row.FindControl("txtGrade5Max")).Text;
                        string grade5Min = ((TextBox)row.FindControl("txtGrade5Min")).Text;
                        string grade4Max = ((TextBox)row.FindControl("txtGrade4Max")).Text;
                        string grade4Min = ((TextBox)row.FindControl("txtGrade4Min")).Text;
                        string grade3Max = ((TextBox)row.FindControl("txtGrade3Max")).Text;
                        string grade3Min = ((TextBox)row.FindControl("txtGrade3Min")).Text;
                        string grade2Max = ((TextBox)row.FindControl("txtGrade2Max")).Text;
                        string grade2Min = ((TextBox)row.FindControl("txtGrade2Min")).Text;
                        string grade1Max = ((TextBox)row.FindControl("txtGrade1Max")).Text;
                        string grade1Min = ((TextBox)row.FindControl("txtGrade1Min")).Text;

                        // Create object and assign values
                        BLLKPIEmployeeWiseDetail detail = new BLLKPIEmployeeWiseDetail();
                        
                        detail.AssignDetailID = Convert.ToInt32(assignDetailId);
                        detail.TemplateDetailID = Convert.ToInt32(detailId);
                        detail.Grade5_Max = grade5Max;
                        detail.Grade5_Min = grade5Min;
                        detail.Grade4_Max = grade4Max;
                        detail.Grade4_Min = grade4Min;
                        detail.Grade3_Max = grade3Max;
                        detail.Grade3_Min = grade3Min;
                        detail.Grade2_Max = grade2Max;
                        detail.Grade2_Min = grade2Min;
                        detail.Grade1_Max = grade1Max;
                        detail.Grade1_Min = grade1Min;
                        detail.ModifiedBy = username.ToString();
                        detail.ModifiedDate = DateTime.Now;
                        // Call update method
                        detail.KPITemplateAssignDetailUpdate(detail);
                    }
                }
                drawMsgBox("Data Updated successfully.", 1);
            }
            else
            {
                drawMsgBox(_displymsg, 3);

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void drawMsgBox(string msg, int errType)
    {
        ImpromptuHelper.ShowPrompt(msg);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPISelection.aspx", false);
    }
    protected void txtEmpID_TextChanged(object sender, EventArgs e)
    {
        BLLKPITemplate obj = new BLLKPITemplate();
        DataTable dt = objbll.KPIEmployeeTemplateFetchbyEmployeeCode(Convert.ToInt32(txtEmpID.Text));
        if (dt.Rows.Count> 0)
        {
            lblEmpName.Visible = true;
            lblOrganization.Visible = true;
            lblDesignation.Visible = true;
            lblRegion.Visible = true;
            lblUpdated.Visible = true;
            lblUpdatedBy.Visible = true;
            lblUpdatedOn.Visible = true;
            lblBy.Visible = true;
            lblRemarks.Visible = true;
            txtRemarks.Visible = true;


            lblEmpName.Text = dt.Rows[0]["EmployeeName"].ToString();
            lblOrganization.Text = dt.Rows[0]["Center_Name"].ToString();
            lblDesignation.Text = dt.Rows[0]["DesigName"].ToString();
            lblRegion.Text = dt.Rows[0]["Region_Name"].ToString();
            lblTemplate.Text = dt.Rows[0]["TemplateNameYear"].ToString();
            lblUpdatedOn.Text = dt.Rows[0]["ModifiedDate"].ToString();
            lblBy.Text = "by";
            lblUpdatedBy.Text = dt.Rows[0]["ModifyUser"].ToString();
            txtRemarks.Text = dt.Rows[0]["RemarksHR"].ToString();
            gvKPIInsert.DataSource = dt;
            gvKPIInsert.DataBind();
        }
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
            kpiCell1.ForeColor = System.Drawing.Color.Black;
            kpiCell1.HorizontalAlign = HorizontalAlign.Center;
            headerRow.Cells.Add(kpiCell1);
            // Grades
            string[] grades = { "Grade 5", "Grade 4", "Grade 3", "Grade 2", "Grade 1" };
            foreach (string grade in grades)
            {
                TableCell cell = new TableCell();
                cell.Text = grade;
                cell.ColumnSpan = 2;
                cell.BackColor = System.Drawing.Color.Black;
                cell.ForeColor = System.Drawing.Color.Black;
                cell.HorizontalAlign = HorizontalAlign.Center;
                headerRow.Cells.Add(cell);
            }

            gvKPIInsert.Controls[0].Controls.AddAt(0, headerRow);
        }
    }
   
}