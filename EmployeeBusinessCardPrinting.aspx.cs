using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class EmployeeBusinessCardPrinting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortDirectionBCard"] = "ASC";
            ViewState["chkSelect"] = "check";
            BindGridBCard();
        }
    }
    private void BindGridBCard()
    {
        BLLEmployeeBusinessCard obj = new BLLEmployeeBusinessCard();
        gv.DataSource = null;
        gv.DataBind();
        DataTable dt = new DataTable();
        if (ViewState["BCard"] == null)
        {
            //int user_Type_Id = Convert.ToInt32(Session["UserType"].ToString().Trim());
            //obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            //switch (user_Type_Id)
            //{
            //    case 17:
            //    case 20:
            //    case 23:
            //        {
            //            if (obj.EmployeeCode == "33597") // HO-HR
            //                obj.ApprovalTypeId = 2;   //HR_RD
            //            else if (obj.EmployeeCode == "13878" || obj.EmployeeCode == "20654")  // 13878  RD-CR , 20654   RD-NR
            //                obj.ApprovalTypeId = 2;   //HR_RD
            //            else if (obj.EmployeeCode == "11054" || obj.EmployeeCode == "11055") // BOD / CEO
            //                obj.ApprovalTypeId = 3;//CEO
            //            else
            //                obj.ApprovalTypeId = 1;   //HOD
            //            break;
            //        }
            //    case 19:
            //        {
            //            obj.ApprovalTypeId = 2;   // HR
            //            break;
            //        }
            //    default:
            //        {
            //            obj.ApprovalTypeId = 4;//employee
            //            break;
            //        }
            //}

            //obj.EmpBCardStatus_Id = 1; //pending for approval
            dt = obj.EmployeeBusinessCardFetchAllForPrinting();
            ViewState["BCard"] = dt;
        }
        else
            dt = (DataTable)ViewState["BCard"];

        gv.DataSource = dt;
        gv.DataBind();

        ViewState["BCard"] = dt;
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string repStr = "";
        string Employeecode;
        ImageButton btn = (ImageButton)sender;
        int EmpBCard_Id = Convert.ToInt32(btn.CommandArgument);

        Session["reppath"] = "Reports\\rptBCard.rpt";
        Session["rep"] = "rptBCard.rpt";

        repStr = repStr + "{vw_EmployeeBusinessCard.EmpBCard_Id}=" + EmpBCard_Id;


        Session["CriteriaRpt"] = repStr;
        Session["LastPage"] = "~/EmployeeBusinessCardPrinting.aspx";
        Response.Redirect("~/rptAllReports.aspx");


    }
    protected void btnPrintAll_Click(object sender, EventArgs e)
    {
        BLLEmployeeBusinessCard obj = new BLLEmployeeBusinessCard();
        int chk = 0;
        CheckBox cb = null;
        string list_EmpBCard_Id = null;
        //List<string> BCardList = new List<string>();

        int user_Type_Id = Convert.ToInt32(Session["UserType"].ToString().Trim());
        obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();


        foreach (GridViewRow gvRow in gv.Rows)
        {
            //int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            cb = (CheckBox)gvRow.FindControl("ChkSelect");

            if (cb.Checked)
            {
                chk += 1;
                if (list_EmpBCard_Id != null)
                    list_EmpBCard_Id = list_EmpBCard_Id + ",";
                list_EmpBCard_Id=list_EmpBCard_Id + Convert.ToInt32(gvRow.Cells[0].Text.ToString());
                
            }
        }

        if (chk > 0)
        {
            string repStr = "";
            
            Session["reppath"] = "Reports\\rptBCard.rpt";
            Session["rep"] = "rptBCard.rpt";

            repStr = repStr + "({vw_EmployeeBusinessCard.EmpBCard_Id} in[" + list_EmpBCard_Id + "])" ;


            Session["CriteriaRpt"] = repStr;
            Session["LastPage"] = "~/EmployeeBusinessCardPrinting.aspx";
            Response.Redirect("~/rptAllReports.aspx");
        }
        else
        {
            drawMsgBox("Select the checkbox ,before click!", 1);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BLLEmployeeBusinessCard obj = new BLLEmployeeBusinessCard();
        int chk = 0;
        CheckBox cb = null;


        int user_Type_Id = Convert.ToInt32(Session["UserType"].ToString().Trim());
        obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();


        foreach (GridViewRow gvRow in gv.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            //txtRemarksInner = (TextBox)gvRow.FindControl("txtRemarks");

            cb = (CheckBox)gvRow.FindControl("ChkSelect");

            if (cb.Checked)
            {

                //if (txtRemarksInner.Text != string.Empty)
                {
                    int dt = 0;
                    obj.EmpBCard_Id = _id;
                    obj.Remarks = "";
                    obj.EmpBCardStatus_Id = 6;//printing process completed
                    obj.ApprovalTypeId = 5;   //printing process
                    dt = obj.EmployeeBusinessCardApprovalUpdate(obj);



                    if (dt >= 1)
                    {
                        chk = chk + 1;
                    }
                }
            }
        }

        if (chk > 0)
        {
            drawMsgBox("Record Saved Successfully!", 1);
            ViewState["BCard"] = null;
            ViewState["SortDirectionBCard"] = "ASC";
            ViewState["chkSelect"] = "check";
            BindGridBCard();
        }
        else
        {
            drawMsgBox("Not saved,Select the checkbox!", 1);
        }
         
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

    

   

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {


            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["chkSelect"].ToString();

                foreach (GridViewRow gvr in gv.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("chkSelect");

                    if (mood == "" || mood == "check")
                    {

                        cb.Checked = true;
                        ViewState["chkSelect"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["chkSelect"] = "check";
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
}