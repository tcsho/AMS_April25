using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;

public partial class EmployeeBusinessCardApproval : System.Web.UI.Page
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
           int user_Type_Id = Convert.ToInt32(Session["UserType"].ToString().Trim());
           obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
            switch (user_Type_Id)
            {
                case 17:
                case 20:
                case 23:
                    {
                        if (obj.EmployeeCode == "33597") // HO-HR
                            obj.ApprovalTypeId = 2;   //HR_RD
                        else if(obj.EmployeeCode == "13878" || obj.EmployeeCode == "20654")  // 13878  RD-CR , 20654   RD-NR
                            obj.ApprovalTypeId = 2;   //HR_RD
                        else if (obj.EmployeeCode == "11054" || obj.EmployeeCode == "11055") // BOD / CEO
                            obj.ApprovalTypeId = 3;//CEO
                        else
                            obj.ApprovalTypeId = 1;   //HOD
                        break;
                    }
                case 19:       
                    {
                        obj.ApprovalTypeId = 2;   // HR
                        break;
                    }
                default:
                    {
                        obj.ApprovalTypeId = 4;//employee
                         break;
                    }
            }

            obj.EmpBCardStatus_Id = 1; //pending for approval
            dt = obj.EmployeeBusinessCardFetchAllByApprovalEmpCode(obj);
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
        Session["LastPage"] = "~/EmployeeBusinessCardApproval.aspx";
        Response.Redirect("~/rptAllReports.aspx");


    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int Id = Convert.ToInt32(btn.CommandArgument);

        GridViewRow grv = (GridViewRow)btn.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtRemarks");
        TextBox txtRemarks = (TextBox)ctl;

         
        string _remarks;
        
        _remarks = txtRemarks.Text;
         
        CheckBox cb = null;
        TextBox txtReasonInner = null;
        foreach (GridViewRow gvRow in gv.Rows)
        {
            int _index = gvRow.RowIndex;
            txtReasonInner = (TextBox)gvRow.FindControl("txtRemarks");
            
            cb = (CheckBox)gvRow.FindControl("chkSelect");
            if (cb.Checked)
            {
                txtReasonInner.Text = _remarks;
               
                cb.Checked = false;
            }
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        BLLEmployeeBusinessCard obj = new BLLEmployeeBusinessCard();
        ImageButton imgbtn = (ImageButton)sender;
        bool isHOD_HR_RD = false;
        bool isHOD_CEO = false;
        int user_Type_Id = Convert.ToInt32(Session["UserType"].ToString().Trim());
        obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
        switch (user_Type_Id)
        {
            case 17:  //HO-HOD
            case 20:  //RO-HOD
            case 23:  //CO-HOD
                {
                    if (obj.EmployeeCode == "33597") // HO-HR
                    {
                        //obj.ApprovalTypeId = 2;   //HR_RD
                        isHOD_HR_RD = true;
                    }
                    else if (obj.EmployeeCode == "13878" || obj.EmployeeCode == "20654")  // 13878  RD-CR , 20654   RD-NR
                    {
                        //obj.ApprovalTypeId = 2;   //HR_RD
                        isHOD_HR_RD = true;
                    }
                    else if (obj.EmployeeCode == "11054" || obj.EmployeeCode == "11055") // BOD / CEO
                    {
                        //obj.ApprovalTypeId = 3;//CEO
                        isHOD_CEO = true;
                    }
                    else
                    {
                        obj.ApprovalTypeId = 1;   //HOD
                    }
                    break;
                }
            case 19:
                {
                    obj.ApprovalTypeId = 2;   // HR   HR_RD

                    break;
                }
            default:
                {
                    obj.ApprovalTypeId = 4;//employee
                    break;
                }
        }

        obj.ApprovalTypeId = 1;//HOD 
        obj.EmpBCard_Id = Convert.ToInt32(imgbtn.CommandArgument);
        //obj.EmployeeCode =  Session["EmployeeCode"].ToString().Trim() ;
        obj.EmpBCardStatus_Id = 3;//rejected
        obj.Remarks = "";
        obj.EmployeeBusinessCardApprovalUpdate(obj);
        if (isHOD_CEO)    // as CEO auto approval
        {

            obj.ApprovalTypeId = 3;   //CEO
              obj.EmployeeBusinessCardApprovalUpdate(obj);

        }
        else if (isHOD_HR_RD)   //as  HR_RD auto approval 
        {
            obj.ApprovalTypeId = 2;   //HR_RD
             obj.EmployeeBusinessCardApprovalUpdate(obj);
        }

        

        

        ViewState["BCard"] = null;
        BindGridBCard();

    }

    protected void btnApproveSave_Click(object sender, EventArgs e)
    {
        BLLEmployeeBusinessCard obj = new BLLEmployeeBusinessCard();
        int chk = 0;
        CheckBox cb = null;
         
        TextBox txtRemarksInner = null;
        Control ctl = null;
        
        bool isHOD_HR_RD = false;
        bool isHOD_CEO = false;

        int user_Type_Id = Convert.ToInt32(Session["UserType"].ToString().Trim());
        obj.EmployeeCode = Session["EmployeeCode"].ToString().Trim();
        switch (user_Type_Id)
        {
            case 17:  //HO-HOD
            case 20:  //RO-HOD
            case 23:  //CO-HOD
                {
                    if (obj.EmployeeCode == "33597") // HO-HR
                    {
                        //obj.ApprovalTypeId = 2;   //HR_RD
                        isHOD_HR_RD = true;
                    }
                    else if (obj.EmployeeCode == "13878" || obj.EmployeeCode == "20654")  // 13878  RD-CR , 20654   RD-NR
                    {
                        //obj.ApprovalTypeId = 2;   //HR_RD
                        isHOD_HR_RD = true;
                    }
                    else if (obj.EmployeeCode == "11054" || obj.EmployeeCode == "11055") // BOD / CEO
                    {
                        //obj.ApprovalTypeId = 3;//CEO
                        isHOD_CEO = true;
                    }
                    else
                    {
                        obj.ApprovalTypeId = 1;   //HOD
                    }
                        break;
                }
            case 19:
                {
                    obj.ApprovalTypeId = 2;   // HR   HR_RD
                     
                    break;
                }
            default:
                {
                    obj.ApprovalTypeId = 4;//employee
                    break;
                }
        }

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
                    obj.EmpBCardStatus_Id = 2;//approved
                    obj.ApprovalTypeId = 1;   //HOD
                     dt = obj.EmployeeBusinessCardApprovalUpdate(obj);

                    //if (isHOD_CEO)    // as CEO auto approval
                    //{      
    
                    //    obj.ApprovalTypeId = 3;   //CEO
                    //    dt = obj.EmployeeBusinessCardApprovalUpdate(obj);

                    //}
                    //else if (isHOD_HR_RD)   //as  HR_RD auto approval 
                    //{                
                    //    obj.ApprovalTypeId = 2;   //HR_RD
                    //    dt = obj.EmployeeBusinessCardApprovalUpdate(obj);
                    //}
                   
                    
                    //by pass approval process if HOD approved then all approved
                    obj.ApprovalTypeId = 2;   //HR_RD
                    dt = obj.EmployeeBusinessCardApprovalUpdate(obj);
                    obj.ApprovalTypeId = 3;   //CEO
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
            drawMsgBox("Saved Successfully!", 1);
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

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BLLEmployeeBusinessCard obj = new BLLEmployeeBusinessCard();
            obj.EmpBCard_Id = Convert.ToInt32(gv.Rows[e.RowIndex].Cells[0].Text);
            obj.EmployeeBusinessCardDelete(obj);
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
            ImageButton ib = (ImageButton)e.Row.FindControl("imgbtnReject");
            ib.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to reject this record?') ");
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