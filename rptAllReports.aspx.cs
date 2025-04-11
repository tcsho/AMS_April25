using System;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.Shared;

public partial class rptAllReports : System.Web.UI.Page
{

    public ReportDocument report = new ReportDocument();
    DALBase objBase = new DALBase();

    string criteria;
    string reppath, rep;
    string strDocNo = "";



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["EmployeeCode"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

        string report_path = Session["rep"].ToString();

        if (report_path == "rptHODsMorningAttendance.rpt")
        {
            showReportHODsMorningAttendance();
        }
        else if (report_path == "rptLunchBreakInOutReport.rpt")
        {
            showLunchBrakeInOutReport();
        }
        //else if (report_path == "rpt_AttendanceNotSubmittedNotApproved.rpt")
        //{
        //    showAttendanceNotSubmittedNotApproved();
        //}
        else
        {
            Session["rptDoc"] = null;
            if (Session["CriteriaRpt"] != null)
            {
                criteria = Session["CriteriaRpt"].ToString();
            }
            else
            {
                criteria = "";
            }
            btnPrint_Click(null, null);

        }
        rptviewer.Visible = true;
        rptviewer.ReportSource = report;


    }
    public void showReportHODsMorningAttendance()
    {
        string attenDate = Session["Att_Date"].ToString();
        string strConnect = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["tcs_invConnectionString"]);
        using (SqlConnection con = new SqlConnection(strConnect))
        {
            using (SqlCommand cmd = new SqlCommand("sp_GET_HODs_Morning_Attendance", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                string[] att_split = attenDate.Split('/');
                DateTime dt_s = new DateTime(Convert.ToInt32(att_split[2]), Convert.ToInt32(att_split[0]), Convert.ToInt32(att_split[1]), 0, 0, 0, 0);
                DateTime dt_e = new DateTime(Convert.ToInt32(att_split[2]), Convert.ToInt32(att_split[0]), Convert.ToInt32(att_split[1]), 23, 59,59, 999);

                cmd.Parameters.Add("@Atten_date_s", SqlDbType.DateTime).Value = dt_s;//attenDate;//txtFirstName.Text;
                cmd.Parameters.Add("@Atten_date_e", SqlDbType.DateTime).Value = dt_e;//attenDate;//txtFirstName.Text;

                con.Open();

                //SqlDataAdapter myAdapter = new SqlDataAdapter();
                //DataTable dt = new DataTable();

                //myAdapter.SelectCommand = cmd;
                //myAdapter.Fill(dt);




                SqlDataReader data = cmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(data);

                DS_DataForHODsMorningAttendance objDsReport = new DS_DataForHODsMorningAttendance();

                int srNo = 1;

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow rnew = objDsReport.Tables["TBL_HODsMorningAttendance"].NewRow();

                        rnew[0] = srNo++;
                        rnew[1] = row[0].ToString();
                        rnew[2] = row[1].ToString();
                        //rnew[3] = row[3]==""?DateTime.Now:Convert.ToDateTime(row[3].ToString());

                        if (!String.IsNullOrEmpty(row[3].ToString()))
                        {
                            //rnew[3] = DateTime.ParseExact(row[3].ToString(), "MM/dd/yyyy HH:mm:ss tt", null);
                            rnew[3] = row[3].ToString();
                        }
                        rnew[4] = row[4] == null ? "" : row[4].ToString();
                        rnew[5] = row[5] == null ? "" : row[5].ToString(); 
                        rnew[6] = dt_s;//data.IsDBNull(4) ? "" : data.GetString(4);
                        rnew[7] = row[2] == null ? "" : row[2].ToString(); 
                        objDsReport.Tables["TBL_HODsMorningAttendance"].Rows.Add(rnew);

                    }
                }


                //if (data.HasRows)
                //{

                //    int srNo = 1;
                //    while (data.Read())
                //    {
                //        DataRow rnew = objDsReport.Tables["TBL_HODsMorningAttendance"].NewRow();

                //        rnew[0] = srNo++;
                //        rnew[1] = data.GetString(0);
                //        rnew[2] = data.GetString(1);
                //        if (!data.IsDBNull(2))
                //        {
                //            rnew[3] = data.GetDateTime(2);
                //        }
                //        rnew[4] = data.IsDBNull(3) ? "" : data.GetString(3);
                //        rnew[5] = data.IsDBNull(4) ? "" : data.GetString(4);
                //        rnew[6] = dt_s;//data.IsDBNull(4) ? "" : data.GetString(4);

                //        objDsReport.Tables["TBL_HODsMorningAttendance"].Rows.Add(rnew);
                //    }
                //}

                //rptHODsMorningAttendance objrptHODsMorningAttendance = new rptHODsMorningAttendance();

                string strRpt = Session["reppath"].ToString();

                //reppath = Server.MapPath("Reports\\rptAttendance.rpt");
                reppath = Server.MapPath(strRpt);
                
                report.Load(reppath);
                
                report.SetDataSource(objDsReport);

            }
        }

    }
    public void showLunchBrakeInOutReport()
    {
        string emp_code = Session["EmployeeCode"].ToString();

        string _query = "select * from vw_LunchBreakData where 1=1 ";



        string strConnect = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["tcs_invConnectionString"]);
        using (SqlConnection con = new SqlConnection(strConnect))
        {
            using (SqlCommand cmd = new SqlCommand(_query+Session["CriteriaRpt"], con))
            {
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.Text;

                //cmd.Parameters.Add("@emp_No", SqlDbType.Int).Value = Convert.ToInt32(emp_code);
                
                con.Open();
                SqlDataReader data = cmd.ExecuteReader();

                //DS_DataForHODsMorningAttendance objDsReport = new DS_DataForHODsMorningAttendance();
                DS_LunchBreakTimmings obj_DS_LunchBreakTimmings = new DS_LunchBreakTimmings();

                if (data.HasRows)
                {

                    int srNo = 1;
                    while (data.Read())
                    {
                        DataRow rnew = obj_DS_LunchBreakTimmings.Tables["tbl_LunchBreakTimings"].NewRow();

                        rnew[0] = srNo++;
                        rnew[1] = data.GetString(0);
                        rnew[2] = data.GetString(1);
                        rnew[3] = data.GetString(2);
                        rnew[4] = data.GetString(3);
                        rnew[5] = data.GetDateTime(4);
                        if (!data.IsDBNull(5))
                        {
                            rnew[6] = data.GetDateTime(5);
                        }
                        if (!data.IsDBNull(6))
                        {
                            rnew[7] = data.GetDateTime(6);
                        }

                        obj_DS_LunchBreakTimmings.Tables["tbl_LunchBreakTimings"].Rows.Add(rnew);
                    }
                }

                string strRpt = Session["reppath"].ToString();

                reppath = Server.MapPath(strRpt);

                report.Load(reppath);

                report.SetDataSource(obj_DS_LunchBreakTimmings);

            }
        }

        //DS_LunchBreakTimmings obj_DS_LunchBreakTimmings = new DS_LunchBreakTimmings();
        //        string strRpt = Session["reppath"].ToString();
       
        //        reppath = Server.MapPath(strRpt);

        //        report.Load(reppath);

        //        report.SetDataSource(obj_DS_LunchBreakTimmings);


    }

    public void BindReport()
    {
        try
        {
            string strConnect = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["tcs_invConnectionString"]);
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(strConnect);

            string _username = builder.UserID;
            string _pass = builder.Password;
            string _server = builder.DataSource;
            string _database = builder.InitialCatalog;

            string strRpt = Session["reppath"].ToString();

            //reppath = Server.MapPath("Reports\\rptAttendance.rpt");
            reppath = Server.MapPath(strRpt);

            report.Load(reppath);

           // report.SetDatabaseLogon(_username, _pass, _server, _database);
            setDbInfo(report, _server, _database, _username, _pass);
               
  

            report.RecordSelectionFormula = criteria;

            report.Refresh();

            Session["rptDoc"] = report;

            report.SummaryInfo.ReportTitle = Session["RptTitle"].ToString();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;

        }
        finally
        {

        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        report.Close();
        report.Dispose();
    }

    protected void lnkBtn_Click(object sender, EventArgs e)
    {
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        BindReport();
    }

    protected void rptviewer_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
    {
        BindReport();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        report.Close();
        report.Dispose();
        if (Session["LastPage"] != null)
        {
            Response.Redirect(Session["LastPage"].ToString());
        }
        else
        {
            Response.Redirect("Login.aspx");
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
