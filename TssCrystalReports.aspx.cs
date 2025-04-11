using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Data.SqlClient;

public partial class TssCrystalReports : System.Web.UI.Page
{
    public ReportDocument report = new ReportDocument();
    bool _isSys = false;
    bool t = false;
    int n = 0;
    int ter, sec, stu;
    string reppath, criteria;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            criteria = Session["CriteriaRpt"].ToString();
            reppath = Session["reppath"].ToString();
            LoadReport(criteria, reppath);
            rptviewer.Visible = true;
            rptviewer.ReportSource = report;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }




    }

    protected void Page_Unload(object sender, EventArgs e)
    {

        report.Close();
        report.Dispose();

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        CloseAll();
        Response.Redirect(Session["LastPage"].ToString());
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    static public void MyMethod()
    {
        //report.Close();
        //report.Dispose();
    }

    public void CloseAll()
    {
        report.Close();
        report.Dispose();
    }

    protected void reportviewer_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {

    }

    public void SetNav()
    {
        t = true;
    }

    protected void rptviewer_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        t = true;
    }

    public void LoadReport(string criteria, string reportPath)
    {
        try
        {
            string strConnect = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["tcs_invConnectionString"]);
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(strConnect);

            string _username = builder.UserID;
            string _pass = builder.Password;
            string _server = builder.DataSource;
            string _database = builder.InitialCatalog;
            if (Session["isFacialMachine"]!=null && Convert.ToBoolean(Session["isFacialMachine"].ToString()) == true)
            {
                strConnect = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["tcs_DCConnectionString"]);
                builder = new SqlConnectionStringBuilder(strConnect);
                _username = builder.UserID;
                _pass = builder.Password;
                _server = builder.DataSource;
                _database = builder.InitialCatalog;
            }


            report.Load(reportPath);

            //report.SetDatabaseLogon(_username, _pass, _server, _database);
            setDbInfo(report, _server, _database, _username, _pass);
            report.Refresh();

            if (Session["RptTitle"].ToString() == "Monthly Pending Attendance Submission & Approvals")
            {
                int r_id = Convert.ToInt32((Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString());
                int c_id = Convert.ToInt32((Session["CenterID"].ToString() == "") ? "0" : Session["CenterID"].ToString());
                report.SetParameterValue("@PMonth", Session["PMonth"]);
                report.SetParameterValue("@Region_Id", r_id);
                report.SetParameterValue("@Center_Id", c_id);
            }
            else if (Session["RptTitle"] == "Monthly Pending Attendance Summary")
            {

                int r_id = Convert.ToInt32((Session["RegionID"].ToString() == "") ? "0" : Session["RegionID"].ToString());

                report.SetParameterValue("@PMonth", Session["PMonth"]);
                report.SetParameterValue("@Region_Id", r_id);

            }
            else if (Session["RptTitle"] == "Monthly Pending Attendance Summary Region Wise")
            {

                report.SetParameterValue("@PMonth", Session["PMonth"]);

            }
            else
            {
                report.RecordSelectionFormula = criteria;
            }


            report.SummaryInfo.ReportTitle = Session["RptTitle"].ToString();
            if (Session["rptCmnt"] != null)
            {
                report.SummaryInfo.ReportComments = Session["rptCmnt"].ToString();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }

    }

    protected void setDbInfo(ReportDocument rptDoc, string Server, string dbName, string UserId, string Pwd)
    {
        TableLogOnInfo logoninfo = new TableLogOnInfo();
        // connect multiple tabel    
        foreach (CrystalDecisions.CrystalReports.Engine.Table tbl in rptDoc.Database.Tables)
        {
            logoninfo = tbl.LogOnInfo;
            logoninfo.ReportName = rptDoc.Name;
            logoninfo.ConnectionInfo.ServerName = Server;
            logoninfo.ConnectionInfo.DatabaseName = dbName;
            logoninfo.ConnectionInfo.UserID = UserId;
            logoninfo.ConnectionInfo.Password = Pwd;
            logoninfo.TableName = tbl.Name;
            tbl.ApplyLogOnInfo(logoninfo);
            tbl.Location = tbl.Name;
        }
    }

}