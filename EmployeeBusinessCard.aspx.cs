using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class EmployeeBusinessCard : System.Web.UI.Page
{
    BLLEmployeeBusinessCard objBll = new BLLEmployeeBusinessCard();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeCode"] == null)
        {
            Response.Redirect("Login.aspx", false);
            return;
        }
        if (!IsPostBack)
        {
            string employeeCode=Session["EmployeeCode"].ToString().Trim();
            if (employeeCode == "31942" || employeeCode=="353")
                divRequestForPrinting.Attributes.CssStyle.Add("display", "inline");
            else
                divRequestForPrinting.Attributes.CssStyle.Add("display", "none");
            pan_New.Attributes.CssStyle.Add("display", "none");
            LoadEmployeeInformation();
            BindGridBCard();
            ViewState["SortDirectionBCard"] = "ASC";
        }
    }
    protected void btnAddRequest_Click(object sender, EventArgs e)
    {
        if (ValidateData())
        {
            pan_New.Attributes.CssStyle.Add("display", "inline");
        }
        else
        {
            ImpromptuHelper.ShowPrompt("Can not create new request, beacuse already having pending order requrests.");
        }
    }
    private Boolean ValidateData()
    {
        BLLEmployeeBusinessCard obj = new BLLEmployeeBusinessCard();
        obj.EmployeeCode = Session["EmployeeCode"].ToString();
        return Convert.ToBoolean(obj.EmployeeBusinessCardPendingOrderFetchByEmpCode(obj));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveBCardData();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pan_New.Attributes.CssStyle.Add("display", "none");
        ClearAll();
    }

    protected void btnReceived_Click(object sender, EventArgs e)
    {
        BLLEmployeeBusinessCard obj = new BLLEmployeeBusinessCard();
        Button btn = (Button)sender;
        obj.ApprovalTypeId = 4;//Employee received 
        obj.EmpBCard_Id = Convert.ToInt32(btn.CommandArgument);
        obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
        obj.EmpBCardStatus_Id = 5;//received
        //obj.Remarks = "";

        obj.EmployeeBusinessCardApprovalUpdate(obj);

        ViewState["BCard"] = null;
        BindGridBCard();

    }
    protected void btnRequestForPrinting_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/EmployeeBusinessCardPrinting.aspx",false);


    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string repStr = "";
        string Employeecode;
        ImageButton btn = (ImageButton)sender;
       int   EmpBCard_Id =Convert.ToInt32 (btn.CommandArgument);

        Session["reppath"] = "Reports\\rptBCard.rpt";
        Session["rep"] = "rptBCard.rpt";

        repStr = repStr + "{vw_EmployeeBusinessCard.EmpBCard_Id}=" + EmpBCard_Id;
       

        Session["CriteriaRpt"] = repStr;
        Session["LastPage"] = "~/EmployeeBusinessCard.aspx";
        Response.Redirect("~/rptAllReports.aspx");


    }

    private void LoadEmployeeInformation()
    {
        
        DataTable dt = new DataTable();

      

        objBll.EmployeeCode  = Session["EmployeeCode"].ToString();
        dt = objBll.EmployeeProfileForBCardFetchByCode(objBll);
       if (dt.Rows.Count > 0)
       {
           txtFName.Text = dt.Rows[0]["FullName"].ToString();
           txtDesignation.Text = dt.Rows[0]["DesigName"].ToString();
           txtCell.Text = dt.Rows[0]["EmpContactNumber"].ToString();
           txtEmail.Text = dt.Rows[0]["EmpEmail"].ToString();
           ddlQuantity.SelectedValue=dt.Rows[0]["Quantity"].ToString();
           txtCost.Text = dt.Rows[0]["Cost"].ToString();
           
           txtAddress.Text = dt.Rows[0]["Address"].ToString();
           txtUAN.Text = dt.Rows[0]["UAN"].ToString();         
           txtFax.Text = dt.Rows[0]["Fax"].ToString();
           txtWeb.Text = dt.Rows[0]["website"].ToString();


       }
       else
       {
           ClearAll();
       }
 
    }
    private void ClearAll()
    {
        //txtFName.Text = "";
        //txtDesignation.Text = "";
        txtCell.Text = "";
        txtEmail.Text = "";
        txtOther.Text = "";
        txtRemarks.Text = "";
        //ddlQuantity.SelectedIndex = ddlQuantity.Items.Count - 1;
        //txtAddress.Text = "";
        //txtUAN.Text = "";
        //txtFax.Text = "";
        //txtWeb.Text = "";
        
    }
   
    private void SaveBCardData()
    {
        objBll.EmployeeCode = Session["EmployeeCode"].ToString();
        objBll.ContactNumber=txtCell.Text.Trim();
        objBll.Email=txtEmail.Text.Trim();
        objBll.Quantity=Convert.ToInt32(ddlQuantity.SelectedItem.Text);
        objBll.Other =txtOther.Text.Trim();
        objBll.Remarks=txtRemarks.Text.Trim();
        objBll.EmployeeBusinessCardAdd(objBll);
        ViewState["BCard"] = null;
        ClearAll();
        BindGridBCard();
        pan_New.Attributes.CssStyle.Add("display", "none");

    }
    private void BindGridBCard()
    {
        gv.DataSource = null;
        gv.DataBind();
        DataTable dt = new DataTable();
        if (ViewState["BCard"] == null)
        {
            objBll.EmployeeCode = Session["EmployeeCode"].ToString();
            dt = objBll.EmployeeBusinessCardFetchAllByEmpCode(objBll);
            ViewState["BCard"] = dt;
        }
       else
            dt = (DataTable)ViewState["BCard"];
       
            gv.DataSource = dt;
            ViewState["BCard"] = dt;
            gv.DataBind();
    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable oDataSet = (DataTable)ViewState["BCard"];
            oDataSet.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirectionBCard"].ToString();
            if (ViewState["SortDirectionBCard"].ToString() == "ASC")
            {
                ViewState["SortDirectionBCard"] = "DESC";
            }
            else
            {
                ViewState["SortDirectionBCard"] = "ASC";
            }
            BindGridBCard();

        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv.PageIndex = e.NewPageIndex;
            BindGridBCard();
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            
            objBll.EmpBCard_Id = Convert.ToInt32( gv.Rows[e.RowIndex].Cells[0].Text);
            objBll.EmployeeBusinessCardDelete(objBll);
            ViewState["BCard"] = null;
            BindGridBCard();
            //divListOfSubordinates.Attributes.CssStyle.Add("display", "none");
            //pan_New.Attributes.CssStyle.Add("display", "none");
              //ImpromptuHelper.ShowPrompt("BCard & All his/her Subordinates Removed Successfully!");
        }
        catch (Exception oException)
        {
            throw oException;
        }
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ib = (ImageButton)e.Row.FindControl("imgbtnDelete");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this record?') ");
        }
    }
}