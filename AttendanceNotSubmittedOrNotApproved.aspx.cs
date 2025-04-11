using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AttendanceNotSubmittedOrNotApproved : System.Web.UI.Page
{

    DALBase objbase = new DALBase();
    int UserLevel, UserType;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["EmployeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }






        if (Session["EmployeeCode"] != null)
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;

            int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

            //int _result = objbase.ApplicationSettings(sRet, _part_Id);


            //if (_result == 1)
            //    {
            if (!IsPostBack)
            {
               
                try
                {
                    //loadMonths();
                    //loadDepartments();
                    // loadEmployees();

                   



                }
                catch (Exception ex)
                {
                    Session["error"] = ex.Message;
                    Response.Redirect("ErrorPage.aspx", false);
                }

            }

        }
        else
        {
            Response.Redirect("~/login.aspx");

        }
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {

        int UserLevel, UserType;

        UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
        UserType = Convert.ToInt32(Session["UserType"].ToString());

        string repStr = "";
        if (rbLstRpt.SelectedValue == "0")
        {
            Session["reppath"] = "Reports\\rpt_AttendanceNotSubmittedNotApproved.rpt";
            Session["rep"] = "rpt_AttendanceNotSubmittedNotApproved.rpt";

            #region 'HODs Morning Attendance'


         
           

            #endregion

            Session["CriteriaRpt"] = repStr;
            Session["LastPage"] = "~/AttendanceNotSubmittedOrNotApproved.aspx";// +ViewState["rptmode"].ToString();
            Response.Redirect("~/rptAllReports.aspx");

        }




    }




    protected string loadEmployeesReportTo()
    {

        string str = "";
        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();

        DataTable dt = new DataTable();

        obj.ReportTo = Session["EmployeeCode"].ToString();
        dt = obj.EmplyeeReportToFetchList(obj);
        if (dt.Rows.Count > 0)
        {
            str = dt.Rows[0]["Codes"].ToString();
        }
        return str;

    }




    protected void rbLstRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbLstRpt.SelectedValue == "0")
        {
        }
        else if (rbLstRpt.SelectedValue == "1")
        {
        }
    }
}