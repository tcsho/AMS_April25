using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PrintReport
/// </summary>
public class PrintReport
{
    public PrintReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void PrintReportMonthly(AMSReports aMS,string Employeecode,string PMonth,string LastPage)
    {

        string repStr = "";

        if (aMS==AMSReports.AttendanceReport)
        {
            System.Web.HttpContext.Current.Session["reppath"] = "Reports\\rptAttendance.rpt";
            System.Web.HttpContext.Current.Session["rep"] = "rptAttendance.rpt";

            repStr = repStr + "{vw_AttendanceRep.PMonth}='" + PMonth + "'";
            repStr = repStr + " and {vw_AttendanceRep.EmployeeCode}='" + Employeecode + "'";
        }
        else if (aMS==AMSReports.EmployeeLogReport)
        {
            System.Web.HttpContext.Current.Session["reppath"] = "Reports\\rptAttenLog.rpt";
            System.Web.HttpContext.Current.Session["rep"] = "rptAttenLog.rpt";
            repStr = repStr + "{vw_AttenLogRep.PMonth}='" + PMonth + "'";
            repStr = repStr + " and {vw_AttenLogRep.EmployeeCode}='" + Employeecode + "'";
        }
        if ((repStr).Length>0)
        {
            System.Web.HttpContext.Current.Session["CriteriaRpt"] = repStr;
            System.Web.HttpContext.Current.Session["LastPage"] = LastPage;
            HttpContext.Current.Response.Redirect("~/rptAllReports.aspx");
        }


    }





}