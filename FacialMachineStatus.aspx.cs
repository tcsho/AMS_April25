using ADG.JQueryExtenders.Impromptu;
using Splash;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.UI.WebControls;

public partial class FacialMachineStatus : System.Web.UI.Page
{
    DALBase objBase = new DALBase();
    FaceId obj = new FaceId();
    BLLFacialMachineStatus facial = new BLLFacialMachineStatus();
    private const Int32 DeviceCodePage = 65001;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                try
                {
                    //FaceId fid = new FaceId("",1);
                    ViewState["SortDirection"] = "ASC";
                    int UserLevel, UserType;

                    if (Session["employeeCode"] == null)
                    {
                        Response.Redirect("~/login.aspx");
                    }
                    UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
                    UserType = Convert.ToInt32(Session["UserType"].ToString());
                    loadRegions();
                    if (Session["RegionID"] == null && Session["CenterID"] == null)//HO
                    {
                        ddlRegion.SelectedValue = "0";
                        ddlRegion.Enabled = true;
                        ddlRegion.Visible = true;
                        ddlRegion_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    if (Session["RegionID"] != null && (Session["CenterID"] ==null || String.IsNullOrEmpty(Session["CenterID"].ToString())))//RO
                    {
                        ddlRegion.SelectedValue = Session["RegionID"].ToString();
                        ddlRegion_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    else if (Session["RegionID"] != null && Session["CenterID"] != null) //CO
                    {
                        divddl.Visible = false;
                        divButtons.Visible = false;
                        ddlRegion.SelectedValue = Session["RegionID"].ToString();
                        ddlRegion_SelectedIndexChanged(this, EventArgs.Empty);
                    }

                }
                catch (Exception ex)
                {
                    Session["error"] = ex.Message;
                    Response.Redirect("ErrorPage.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    public void loadRegions()
    {
        try
        {
            BLLVacationTimigs objBll = new BLLVacationTimigs();
            DataTable _dt = new DataTable();
            _dt = objBll.fetchRegions();
            objBase.FillDropDown(_dt, ddlRegion, "Region_Id", "Region_Name");
            DevicePort();
            //ddlRegion.DataTextField = "Region_Name";
            //ddlRegion.DataValueField = "Region_Id";
            //ddlRegion.DataSource = _dt;
            //ddlRegion.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void btnCancelSearch_Click(object sender, EventArgs e)
    {
        try
        {
            gridPanel.Visible = true;

            pan_Search.Visible = false;
            gvCenter.DataSource = null;
            gvCenter.DataBind();
            BindGrid();
            btnActive_Click(this, EventArgs.Empty);
            pan_operations.Visible = false;
            pan_Employee.Visible = false;
            gvOperations.DataSource = null;
            gvEmployees.DataSource = null;
            gvOperations.DataBind();
            gvEmployees.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
    protected void DevicePort()
    {
        DataTable dt = facial.DEVICEPORT();
        objBase.FillDropDown(dt, ddlPort, "DevicePort", "DevicePort");
        //ddlPort.DataSource = dt;
        //ddlPort.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            btnCancelOPClick(this, EventArgs.Empty);
            gridPanel.Visible = false;
            pan_Search.Visible = true;
            txtEmployeeCode.Text = "";
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Data"] = null;
            BindGrid();
            btnActive_Click(this, EventArgs.Empty);


        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void gvCenter_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvCenter.Rows.Count > 0)
            {
                gvCenter.UseAccessibleHeader = false;
                gvCenter.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
    protected void gvOperations_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvOperations.Rows.Count > 0)
            {
                gvOperations.UseAccessibleHeader = false;
                gvOperations.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
    protected void BindGrid()
    {
        try
        {
            int mode = -1;
            BLLFacialMachineStatus obj = new BLLFacialMachineStatus();
            if (Session["RegionID"] != null &&!string.IsNullOrEmpty(Session["RegionID"].ToString()))
            {
                obj.RegionID = Convert.ToInt32(Session["RegionID"].ToString());
            }
            else
            {
                obj.RegionID = 0;
            }
            if (Session["CenterID"] != null && !String.IsNullOrEmpty(Session["CenterID"].ToString()))
            {
                obj.BranchCode = Session["CenterID"].ToString();
            }
            else
            {
                obj.BranchCode = "0";
            }
            if (ViewState["DeviceMode"] != null)
                mode = Convert.ToInt32(ViewState["DeviceMode"].ToString());

            DataTable dt = new DataTable();
            if (ViewState["Data"] == null)
            {
                dt = obj.FacialMachineStatusFetch(obj, mode);
                ViewState["Data"] = dt;
            }
            else
                dt = (DataTable)ViewState["Data"];
            if (dt.Rows.Count > 0)
                gvCenter.DataSource = dt;
            // update code by fahad to show edit btn
            if (Session["CenterID"] != null && Session["CenterID"].ToString() != "0" && !String.IsNullOrEmpty(Session["CenterID"].ToString()))
            {
                gvCenter.DataBind();
                gvCenter.Columns[12].Visible = false;
            }
            gvCenter.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }


    protected void btnPull_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvCenter.SelectedIndex = gvr.RowIndex;
            facial.DeviceIP = gvr.Cells[0].Text;
            facial.DevicePort = Convert.ToInt32(gvr.Cells[1].Text);
            facial.DeviceID = gvr.Cells[2].Text;
            string command = "GetRecord()";
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {
                if (ViewState["Output"].ToString() == "The Device is not connected")
                {
                    lblPull.Text = "<br/>" + ViewState["Output"].ToString();
                    return;
                }
                InsertAttendance(ViewState["Output"].ToString(), facial.DeviceID);
                lblPull.Text = "<br/>Data Successfully Pulled. Please check Log Report to Verify.<br/>";
            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnPullRange_Click(object sender, EventArgs e)
    {
        try
        {
            txtFrom.Text = "";
            txtFrom.Focus();
            //txtTO.Text = DateTime.Now.ToString("d");            //txtTO.Text = DateTime.Now.ToString("d");
            divPull.Visible = true;
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvCenter.SelectedIndex = gvr.RowIndex;
            facial.DeviceIP = gvr.Cells[0].Text;
            facial.DevicePort = Convert.ToInt32(gvr.Cells[1].Text);
            facial.DeviceID = gvr.Cells[2].Text;
            lblPullIP.Text = facial.DeviceIP;
            lblPullPort.Text = facial.DevicePort.ToString();
            lblPullID.Text = facial.DeviceID;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(lblPullIP.Text))
                return;
            if (String.IsNullOrEmpty(lblPullPort.Text))
                return;
            if (String.IsNullOrEmpty(txtFrom.Text))
            {
                ImpromptuHelper.ShowPrompt("Please mention an start time ");
                return;
            }
            if (String.IsNullOrEmpty(txtTO.Text))
            {
                txtFrom.Text = "";
                txtTO.Text = "";
                ImpromptuHelper.ShowPrompt("Please mention an end time ");
                return;
            }

            divPull.Visible = false;
            facial.DeviceIP = lblPullIP.Text;
            facial.DevicePort = Convert.ToInt32(lblPullPort.Text);
            facial.DeviceID = lblPullID.Text;

            DateTime from = Convert.ToDateTime(txtFrom.Text);
            DateTime to = Convert.ToDateTime(txtTO.Text);

            if (Session["CenterID"] != null && !String.IsNullOrEmpty(Session["CenterID"].ToString()))
            {


                if (from < to)
                {
                    int diff = to.Day - from.Day;
                    if (diff>2)
                    {
                        ImpromptuHelper.ShowPrompt("Branch Can not export data more than 3 days! ");
                        return;

                    }
                }

            }


            if (from > to)
            {
                ImpromptuHelper.ShowPrompt("Please mention correct Date Range! ");
                return;
            }
            DataTable dtpull = new DataTable();
            facial.User_Id = Convert.ToInt32(Session["User_Id"].ToString());
            facial.From_Date = from;
            facial.To_Date = to;
            dtpull = facial.FacialMachineStatusSelectPullStatus(facial);
            if (dtpull.Rows.Count > 0)
            {
                ImpromptuHelper.ShowPrompt("<br/>" + dtpull.Rows[0]["PullStatus"].ToString() + "<br/>");
                //lblPull.Text = ;
                return;
            }
            string command = "GetRecord(start_time=\"" + from.ToString("yyyy-MM-dd HH:mm:ss") + "\" end_time=\"" + to.ToString("yyyy-MM-dd") + " 24:00:00\")";
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {
                if (ViewState["Output"].ToString() == "The Device is not connected")
                {
                    lblPull.Text = "<br/>" + ViewState["Output"].ToString();
                    return;
                }

                InsertAttendance(ViewState["Output"].ToString(), facial.DeviceID);
                lblPull.Text = "<br/>Data Successfully Pulled. Please check Log Report to Verify.<br/>";
            }
            txtFrom.Text = "";
            txtTO.Text = "";
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void InsertAttendance(string s, string device)
    {
        try
        {
            facial.DeviceID = device;
            string source = s;
            source = source.Remove(0, 61);
            string output;
            string final = "";

            for (int i = 0; final != "\r\n)"; i++)
            {
                final = source.Split('"')[i];
                if (final == " authority=")
                {
                    i++;
                    facial.FacialMachineStatusInsertRecord(facial);
                    continue;
                }

                if (final == " workcode=")
                {
                    facial.WorkCode = Convert.ToInt32(source.Split('"')[++i]);
                    continue;
                }
                if (final == " check_type=")
                {
                    facial.AttendanceMethod = source.Split('"')[++i];
                    continue;
                }
                if (final == " status=")
                {
                    facial.StatusID = Convert.ToInt32(source.Split('"')[++i]);
                    continue;
                }
                if (CheckDate(final) == true)
                {
                    facial.Attendance = Convert.ToDateTime(final);
                    continue;
                }
                output = new string(final.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
                if (!String.IsNullOrEmpty(output) && Convert.ToInt32(output) > 1)
                {
                    facial.EmployeeCode = Convert.ToInt32(output);

                    continue;
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }
    private bool CheckDate(String date)
    {
        try
        {
            DateTime dt = DateTime.Parse(date);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void btnCancelPull_Click(object sender, EventArgs e)
    {
        try
        {
            divPull.Visible = false;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnCancelOPClick(object sender, EventArgs e)
    {
        try
        {
            pan_Employee.Visible = false;
            pan_operations.Visible = false;
            gvCenter.DataSource = null;
            gvCenter.DataBind();
            gvEmployees.DataSource = null;
            gvEmployees.DataBind();
            BindGrid();
            btnActive_Click(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    public void GetMachineRecord(string answer, string deviceid)
    {
        try
        {
            BLLFacialMachineStatus facial = new BLLFacialMachineStatus();
            facial.DeviceID = deviceid;
            facial.ConnectionStatus = "Connected";
            string s = answer;
            s = s.Replace('"', '|');
            string final = "", output;
            for (int i = 0; !final.Contains(")"); i++)
            {
                try
                {
                    final = s.Split(' ')[i];
                    output = final.Substring(final.LastIndexOf('=') + 1);
                    output = output.Replace('|', ' ');
                    output = output.Replace(')', ' ');
                    if (final.Contains("dev_id="))
                    {
                        facial.DeviceSerialNo = output;
                    }
                    if (final.Contains("ip="))
                    {
                        facial.DeviceIP = IPAddress.Parse(ConverttoIP(output)).ToString();
                    }
                    if (final.Contains("gateway="))
                    {
                        var a = IPAddress.Parse("103.53.44.97");
                        facial.Gateway = IPAddress.Parse(ConverttoIP(output)).ToString(); ;
                    }
                    if (final.Contains("mac="))
                    {
                        facial.MacAddress = output;
                    }
                    if (final.Contains("edition") && facial.Firmware == null)
                    {
                        facial.Firmware = output;
                    }
                    if (final.Contains("type"))
                    {
                        facial.Model = output;
                    }
                    if (final.Contains("alg_edition"))
                    {
                        facial.Algorithm = output;
                    }
                    if (final.Contains("netmask="))
                    {
                        facial.NetMask = IPAddress.Parse(ConverttoIP(output)).ToString();
                    }
                    if (final.Contains("max_facerecord="))
                    {
                        facial.TotalAttendance = Convert.ToInt32(output);
                    }
                    if (final.Contains("real_facerecord="))
                    {
                        facial.AttendanceRecords = Convert.ToInt32(output);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            int k = facial.FacialMachineStatusDeviceDetails(facial);
        }
        catch (Exception ex)
        {
            string message = ex.Message;
            //Console.WriteLine("Device Not Connected!"); 
        }
    }
    public static string ConverttoIP(string ip)
    {
        string finalip = "";
        int count = 0;
        ip = ip.TrimStart();
        ip = ip.TrimEnd();
        foreach (char c in ip)
        {
            count++;
            finalip = finalip + c;
            if (count == 3)
            {
                finalip = finalip + ".";
                count = 0;
            }
        }
        finalip = finalip.TrimEnd('.');
        finalip=finalip.Replace(".0", ".");
        return finalip;
    }
    protected void btnInfo_Click(object sender, EventArgs e)
    {
        try
        {
            gvDeviceDetails.DataSource = null;
            gvDeviceDetails.DataBind();
            LinkButton btnEdit = (LinkButton)sender;
            if (btnEdit.CommandArgument.Contains("Info"))
            {
                gvOperations.DataSource = null;
                gvOperations.DataBind();
                lblAdmin.Text = "";
            }
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvCenter.SelectedIndex = gvr.RowIndex;
            int DeviceID = Convert.ToInt32(gvr.Cells[2].Text);
            DataTable dts = facial.FacialMachineStatusSelectById(DeviceID);
            if (dts.Rows.Count > 0)
            {
                dts.Columns.Add("StoragePercentage");
                foreach (DataRow r in dts.Rows)
                {
                    double denominator = Convert.ToInt32(r["TotalAttendance"].ToString());
                    double neumerator = Convert.ToInt32(r["AttendanceRecord"].ToString());
                    double answer = (neumerator / denominator) * 100;
                    if (denominator == 1)
                        r["StoragePercentage"] = "-";
                    else
                        r["StoragePercentage"] = answer.ToString() + "%";
                }
                gvDeviceDetails.DataSource = dts;
                gvDeviceDetails.DataBind();
                ResetFilter();
                ApplyFilter(3, DeviceID.ToString());
                pan_operations.Visible = true;
                if (Session["CenterID"] != null && Session["CenterID"].ToString() != "0" && !String.IsNullOrEmpty(Session["CenterID"].ToString()))
                {
                    gvDeviceDetails.Columns[13].Visible = false;
                    gvDeviceDetails.DataBind();
                }
            }
            else
                ImpromptuHelper.ShowPrompt("No information found!");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }
    protected void btnConnect_Click(object sender, EventArgs e)
    {
        try
        {
            gvOperations.Visible = true;
            gvEmployees.DataSource = null;
            gvEmployees.DataBind();
            gvOperations.DataSource = null;
            gvOperations.DataBind();
            lblAdmin.Text = "";
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvCenter.SelectedIndex = gvr.RowIndex;
            facial.DeviceIP = gvr.Cells[5].Text;
            facial.DevicePort = Convert.ToInt32(gvr.Cells[6].Text);
            facial.DeviceID = gvr.Cells[2].Text;
            ViewState["IP"] = facial.DeviceIP;
            ViewState["Port"] = facial.DevicePort;
            ViewState["DeviceId"] = facial.DeviceID;
            ResetFilter();
            ApplyFilter(3, facial.DeviceID);
            pan_operations.Visible = true;
            string command = "GetDeviceInfo()";
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            lblPull.Text = "";
            if (ViewState["Output"].ToString() != "The Device is not Connected")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("DeviceIP");
                dt.Columns.Add("DevicePort");
                dt.Columns.Add("DeviceID");
                dt.Rows.Add(new Object[] { facial.DeviceIP, facial.DevicePort, facial.DeviceID });

                string s = ViewState["Output"].ToString();
                if (s == "The Device is not connected")
                {
                    gvOperations.DataSource = null;
                    gvOperations.DataBind();
                    lblAdmin.Text = s;
                    btnInfo_Click(sender, EventArgs.Empty);
                    return;
                }
                GetMachineRecord(s, facial.DeviceID);
                gvOperations.DataSource = dt;
                gvOperations.DataBind();
                s = s.Remove(0, 208);
                s = s.Substring(0, s.IndexOf("max_other"));
                s = s + "+";
                string final = "", output;
                bool flag = true;
                for (int i = 0; final != "+"; i++)
                {
                    final = s.Split('"')[i];
                    output = new string(final.ToCharArray().Where(c => char.IsDigit(c)).ToArray());

                    if (final == " max_managernum=")
                    {
                        lblAdmin.Text = lblAdmin.Text + "Total Admins: ";
                        flag = true;
                    }
                    if (final == " managernum=")
                    {
                        flag = true;
                        lblAdmin.Text = lblAdmin.Text + "&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp Admins on Device: ";
                    }
                    if (final == " max_faceregist=")
                        flag = false;
                    //lblAdmin.Text = lblAdmin.Text +"<br/>Total Face Registerations: ";
                    if (final == " real_faceregist=")
                        flag = false;
                    //lblAdmin.Text = lblAdmin.Text +"<br/>Face Registerations on Device: ";
                    if (final == " max_facerecord=")
                    {
                        flag = true;
                        lblAdmin.Text = lblAdmin.Text + "<br/>Total Attendance: ";
                    }
                    if (final == " real_facerecord=")
                    {
                        flag = true;
                        lblAdmin.Text = lblAdmin.Text + "&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp Attendance: ";
                    }
                    if (final == " max_employee=")
                    {
                        flag = true;
                        lblAdmin.Text = lblAdmin.Text + "<br/> Total Users: ";
                    }
                    if (final == " real_employee=")
                    {
                        flag = true;
                        lblAdmin.Text = lblAdmin.Text + "&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp Registered Users: ";
                    }
                    if (final == " +")
                    {
                        lblAdmin.Text = lblAdmin.Text + " <br/>";
                        break;
                    }
                    if (flag == true)
                    {
                        lblAdmin.Text = lblAdmin.Text + output + " ";
                    }

                }
                gvOperations.Visible = true;
                btnInfo_Click(sender, EventArgs.Empty);
                int device = Convert.ToInt32(facial.DeviceID);
                AddLog(device, "Check Connectivity");
                string a = Session["CenterID"].ToString();
                if ( Session["CenterID"] != null && Session["CenterID"].ToString() !="0"  && !String.IsNullOrEmpty(Session["CenterID"].ToString()))
                {
                    //gvOperations.DataSource = null;
                    //gvOperations.DataBind();

                    gvOperations.HeaderRow.Cells[4].Visible = false;
                    gvOperations.HeaderRow.Cells[5].Visible = false;
                    gvOperations.HeaderRow.Cells[6].Visible = false;
                    gvOperations.HeaderRow.Cells[8].Visible = false;


                    gvOperations.Rows[0].Cells[4].Visible = false;
                    gvOperations.Rows[0].Cells[5].Visible = false;
                    gvOperations.Rows[0].Cells[6].Visible = false;
                    gvOperations.Rows[0].Cells[8].Visible = false;



                }

            }
            else
            {
                lblAdmin.Text = ViewState["Output"].ToString();
                gvOperations.DataSource = null;
                gvOperations.DataBind();
                return;
            }

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnTimeClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvCenter.SelectedIndex = gvr.RowIndex;
            facial.DeviceIP = gvr.Cells[0].Text;
            facial.DevicePort = Convert.ToInt32(gvr.Cells[1].Text);
            string command = btnEdit.CommandArgument;
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {
                Label lblTime = (Label)gvr.FindControl("lblTimeSync");
                if (ViewState["Output"].ToString() == "The Device is not connected")
                {
                    lblTime.Text = "<br/>" + ViewState["Output"].ToString();
                    return;
                }
                string newString = ViewState["Output"].ToString().Substring(23, 35);
                lblTime.Text = "<br/>" + newString + "<br/>";
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnSyncTimeClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvOperations.SelectedIndex = gvr.RowIndex;
            facial.DeviceID = gvr.Cells[2].Text;
            facial.DeviceIP = gvr.Cells[0].Text;
            facial.DevicePort = Convert.ToInt32(gvr.Cells[1].Text);
            string Time = "\"" + DateTime.Now.ToString("HH:mm:ss") + "\"";
            string date = "\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\"";
            string command = "SetDateTime(date=" + date + "time=" + Time + ")";
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {
                Label lblTime = (Label)gvr.FindControl("lblTimeSync");
                if (ViewState["Output"].ToString() == "The Device is not connected")
                {
                    lblTime.Text = "<br/>" + ViewState["Output"].ToString();
                    return;
                }
                lblTime.Text = "<br/>Time Synced Successfully";
            }
            int device = Convert.ToInt32(facial.DeviceID);
            AddLog(device, "Sync Machine Time");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnStatusClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvCenter.SelectedIndex = gvr.RowIndex;
            facial.DeviceIP = gvr.Cells[0].Text;
            facial.DevicePort = Convert.ToInt32(gvr.Cells[1].Text);
            string command = "GetClientStatus()";
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {
                Label lbl = (Label)gvr.FindControl("lblStatus");
                if (ViewState["Output"].ToString() == "The Device is not connected")
                {
                    lbl.Text = "<br/>" + ViewState["Output"].ToString();
                    return;
                }
                string newString = ViewState["Output"].ToString().Substring(23, 31);
                lbl.Text = "<br/>" + newString;// +ViewState["Output"].ToString();
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    private void ExecuteCommand(string ip, int port, string command)
    {
        String Answer;
        try
        {

            using (FaceId Client = new FaceId(facial.DeviceIP, facial.DevicePort))
            {

                FaceId_ErrorCode ErrorCode = Client.Execute(command, out Answer, DeviceCodePage);
                if (ErrorCode == FaceId_ErrorCode.Success)
                {
                    ViewState["Output"] = Answer;
                }
                else if (ErrorCode == FaceId_ErrorCode.Failed)
                {
                    ImpromptuHelper.ShowPrompt("Error Code: " + ErrorCode.ToString());
                    ViewState["Output"] = "Error Code: " + ErrorCode.ToString();
                }


            }
        }
        catch (Exception ex)
        {
            // ImpromptuHelper.ShowPrompt( ex.Message+"The Device is not connected!");
            Answer = ex.Message + "The Device is not connected";
            ViewState["Output"] = "The Device is not connected";
        }

    }
    protected void btnSaveDevice_Click(object sender, EventArgs e)
    {
        try
        {
            BLLFacialMachineStatus obj = new BLLFacialMachineStatus();
            int k = -1;
            obj.DeviceID = lblDeviceId.Text;
            obj.DeviceName = txtDeviceName.Text;

            obj.BranchCode = txtBranchCode.Text;
            obj.DeviceStatus = ddlDeviceStatus.SelectedValue;
            obj.DeviceSerialNo = txtSerialNum.Text;
            if (String.IsNullOrEmpty(Session["RegionID"].ToString()) || Session["RegionID"] == null)
                obj.RegionID = 0;
            else
                obj.RegionID = Convert.ToInt32(Session["RegionID"].ToString());
            obj.DeviceIP = txtIP.Text;
            obj.DevicePort = Convert.ToInt32(ddlPort.SelectedValue);
            obj.DeviceSerialNo = txtSerialNum.Text;

            if (ViewState["Mode"].ToString() == "Add")
            {
                k = obj.FacialMachineStatusAdd(obj);
                int device = Convert.ToInt32(obj.DeviceID);
                AddLog(device, "Add New Machine");
            }
            else if (ViewState["Mode"].ToString() == "Edit")
            {
                k = obj.FacialMachineStatusUpdate(obj);
                int device = Convert.ToInt32(obj.DeviceID);
                AddLog(device, "Update Machine");
            }
            if (k == 0)
                ImpromptuHelper.ShowPrompt("Record Updated");
            else if (k == 1)
            {
                ImpromptuHelper.ShowPrompt("Please check your Port and Device IP again!");
                return;
            }
            ViewState["Data"] = null;
            ddlDStatus.SelectedValue = "1";
            BindGrid();
            btnActive_Click(this, EventArgs.Empty);
            btnCancel_Click(this, EventArgs.Empty);

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    public void AddLog(int device, string action)
    {
        try
        {
            int user = Convert.ToInt32(Session["User_Id"].ToString());
            facial.FacialMachineStatusInsertLog(device, action, user);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pan_AddNew.Visible = false;
            gridPanel.Visible = true;
            ResetControls();
            BindGrid();
            btnActive_Click(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void ResetControls()
    {
        try
        {
            txtBranchCode.Text = "";
            txtDeviceName.Text = "";
            txtIP.Text = "";
            //  ddlPort.SelectedValue = "0";
            txtSerialNum.Text = "";
            //ddlCity.SelectedValue = "0";
            ddlDeviceStatus.SelectedValue = "0";
            gvDeviceDetails.DataSource = null;
            gvDeviceDetails.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDStatus.SelectedIndex = 0;
            ViewState["Mode"] = "Add";
            ResetControls();
            ddlDeviceStatus.SelectedValue = "Active";
            if (gvCenter.Rows.Count > 0)
            {
                lblDeviceId.Text = (Convert.ToInt32(gvCenter.Rows[0].Cells[1].Text) + 1).ToString();
            }
            pan_AddNew.Visible = true;
            gvCenter.DataSource = null;
            gvCenter.DataBind();
            gridPanel.Visible = false;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void ApplyFilter(int _FilterCondition, string value)
    {
        try
        {

            if (ViewState["Data"] != null)
            {
                DataTable dt = (DataTable)ViewState["Data"];
                DataView dv;
                dv = dt.DefaultView;
                string strFilter = "";
                switch (_FilterCondition)
                {
                    case 1:  //Active
                        {

                            strFilter = " Convert([DeviceStatus], 'System.String')='Active'";
                            break;
                        }

                    case 2: // Deactive
                        {
                            strFilter = " Convert([DeviceStatus], 'System.String')='Deactive'";
                            break;
                        }
                    case 3:
                        {
                            strFilter = " Convert([DeviceID], 'System.String')= " + value;
                            break;
                        }


                }
                dv.RowFilter = strFilter;
                gvCenter.DataSource = dv;
                gvCenter.DataBind();
                gvCenter.SelectedIndex = -1;


            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }

    }
    private void ResetFilter()
    {
        try
        {
            BindGrid();
            gvCenter.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }

    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            ResetFilter();
            ApplyFilter(Convert.ToInt32(ddlDStatus.SelectedValue), "");
            pan_AddNew.Visible = false;
            gridPanel.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            pan_operations.Visible = false;
            ViewState["Mode"] = "Edit";
            gridPanel.Visible = false;
            pan_AddNew.Visible = true;
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvCenter.SelectedIndex = gvr.RowIndex;
            txtSerialNum.Text = gvr.Cells[7].Text;
            lblDeviceId.Text = gvr.Cells[2].Text;
            txtBranchCode.Text = gvr.Cells[3].Text;
            txtDeviceName.Text = gvr.Cells[4].Text;
            txtIP.Text = gvr.Cells[5].Text;
            ddlPort.SelectedValue = gvr.Cells[6].Text;
            ddlDeviceStatus.SelectedValue = gvr.Cells[11].Text;

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnDead_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDStatus.SelectedValue = "1";
            //btnActive_Click(this, EventArgs.Empty);
            ViewState["DeviceMode"] = 2;
            ViewState["Data"] = null;
            BindGrid();
            btnActive_Click(this, EventArgs.Empty);
            pan_AddNew.Visible = false;
            gridPanel.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnICU_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDStatus.SelectedValue = "1";

            ViewState["DeviceMode"] = 1;
            ViewState["Data"] = null;
            BindGrid();
            pan_AddNew.Visible = false;
            gridPanel.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    private void SetEmptyGrid(GridView gv)
    {
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add("MaxDeviceId");
            dt.Columns.Add("DeviceSerialNo");
            dt.Columns.Add("DeviceID");
            dt.Columns.Add("BranchCode");
            dt.Columns.Add("DeviceName");
            dt.Columns.Add("RegionID");
            dt.Columns.Add("CityID");
            dt.Columns.Add("CityName");
            dt.Columns.Add("PushLastConnection");
            dt.Columns.Add("DeviceIP");
            dt.Columns.Add("DevicePort");
            dt.Columns.Add("DeviceStatus");
            dt.Columns.Add("CommPassword");
            dt.Columns.Add("ConfrmPassword");
            dt.Rows.Add(dt.NewRow());
            gv.DataSource = dt;
            gv.DataBind();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }

    }
    protected void btnAll_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["DeviceMode"] = null;
            ViewState["Data"] = null;
            BindGrid();
            ddlDStatus.SelectedValue = "1";
            btnActive_Click(this, EventArgs.Empty);
            pan_AddNew.Visible = false;
            gridPanel.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnAlive_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["DeviceMode"] = 3;
            ViewState["Data"] = null;
            BindGrid();
            ddlDStatus.SelectedValue = "1";
            btnActive_Click(this, EventArgs.Empty);
            pan_AddNew.Visible = false;
            gridPanel.Visible = true;
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void btnPullUser_Click(object sender, EventArgs e)
    {
        try
        {
            pan_Employee.Visible = true;
            ViewState["EmpType"] = "Employee";
            LinkButton btnEdit = (LinkButton)sender;


            if (ViewState["Action"] == null)
            {
                GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
                gvCenter.SelectedIndex = gvr.RowIndex;
                facial.DeviceIP = gvr.Cells[0].Text;
                facial.DevicePort = Convert.ToInt32(gvr.Cells[1].Text);
            }
            else if (ViewState["Action"].ToString() == "Delete")
            {
                facial.DeviceIP = ViewState["IP"].ToString();
                facial.DevicePort = Convert.ToInt32(ViewState["Port"].ToString());
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("User_Name");
            string command = btnEdit.CommandArgument;
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            string Answer = ViewState["Output"].ToString();
            string output;
            string s = "";
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {

                if (ViewState["Output"].ToString() == "The Device is not connected")
                {
                    lblTime.Text = "The Device is not connected!";
                    return;
                }
                for (int i = 0; s != ")"; i++)
                {
                    s = Answer.Split('"')[i];
                    output = new string(s.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
                    if (!String.IsNullOrEmpty(output))
                    {
                        DataRow dataRow = dt.NewRow();
                        dataRow["User_Name"] = output;
                        dt.Rows.Add(dataRow);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    BindGrid(dt);
                }
                else
                    ImpromptuHelper.ShowPrompt("Device not connected");
            }
        }
        catch (Exception ex)
        {
            ImpromptuHelper.ShowPrompt(ex.Message);
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void setManager(object sender, EventArgs e)
    {
        try
        {

            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvEmployees.SelectedIndex = gvr.RowIndex;
            facial.DeviceIP = ViewState["IP"].ToString();
            facial.DevicePort = Convert.ToInt32(ViewState["Port"].ToString());
            string command = "SetManager(id=\"" + btnEdit.CommandArgument.Trim() + "\" name=\"" + gvr.Cells[1].Text +
                " \" pass_word=\"" + btnEdit.CommandArgument.Trim() + "\")";
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {

                if (ViewState["Output"].ToString() == "The Device is not connected")
                {

                    return;
                }
                string lblDetail = ViewState["Output"].ToString();
                if (lblDetail.Contains("success"))
                    ImpromptuHelper.ShowPrompt("Employee Deleted!");
                else
                    ImpromptuHelper.ShowPrompt("Machine Not Connected!");
                Button b = new Button();
                b.CommandArgument = "GetEmployeeID()";
                btnPullUser_Click(b, EventArgs.Empty);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnGetManagerClick(object sender, EventArgs e)
    {
        try
        {
            pan_Employee.Visible = true;
            ViewState["EmpType"] = "Manager";
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvCenter.SelectedIndex = gvr.RowIndex;
            facial.DeviceIP = gvr.Cells[0].Text;
            facial.DevicePort = Convert.ToInt32(gvr.Cells[1].Text);
            string command = btnEdit.CommandArgument;
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {

                if (ViewState["Output"].ToString() == "The Device is not connected")
                {
                    lblTime.Text = "The Device is not connected!";
                    return;
                }
                string Answer = "";
                Answer = ViewState["Output"].ToString();
                string output;
                DataTable dt = new DataTable();
                dt.Columns.Add("User_Name");
                string s = "";
                for (int i = 0; s != ")"; i++)
                {
                    s = Answer.Split('"')[i];
                    output = new string(s.ToCharArray().Where(c => char.IsDigit(c)).ToArray());
                    if (!String.IsNullOrEmpty(output))
                    {
                        DataRow dataRow = dt.NewRow();
                        dataRow["User_Name"] = output;
                        dt.Rows.Add(dataRow);
                    }
                }
                BindGrid(dt);
                
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void BindGrid(DataTable dt)
    {
        try
        {
            gvEmployees.DataSource = null;
            gvEmployees.DataBind();
            string code = "";
            DataTable dtDetail = new DataTable();
            int last = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.IndexOf(r) > 0)
                    {
                        code = code + r["User_Name"].ToString() + ",";
                    }
                }
                code = code.TrimEnd(',');
                dtDetail = facial.WebEmployeeProfileSelectByMachineID(code);

            }
            if (dtDetail.Rows.Count > 0)
            {
                gvEmployees.DataSource = dtDetail;
                gvEmployees.DataBind();
                lblGridStatus.Text = "";
                if (ViewState["EmpType"].ToString()== "Manager")
                {
                    gvEmployees.Columns[8].Visible = false;
                    gvEmployees.DataBind();
                }
                else
                {
                    gvEmployees.Columns[8].Visible = true;
                    gvEmployees.DataBind();
                }
            }
            else
            {
                if (ViewState["EmpType"].ToString() == "Manager")
                    lblGridStatus.Text = "No Admin found on the Device!";

                if (ViewState["EmpType"].ToString() == "Employee")
                    lblGridStatus.Text = "No Users found on the Device!";
            }
        }
        catch (Exception ex)
        {
            ImpromptuHelper.ShowPrompt(ex.Message);
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void btnDeleteClick(object sender, EventArgs e)
    {
        try
        {
            ViewState["Action"] = "Delete";
            facial.DeviceIP = ViewState["IP"].ToString();
            facial.DevicePort = Convert.ToInt32(ViewState["Port"].ToString());
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvEmployees.SelectedIndex = gvr.RowIndex;
            string command = "";
            LinkButton b = new LinkButton();
            if (ViewState["EmpType"].ToString() == "Employee")
            {
                b = btnUser;
                command = "DeleteEmployee(id=\"" + btnEdit.CommandArgument.Trim() + "\")";
                b.CommandArgument = "GetEmployeeID()";

            }
            if (ViewState["EmpType"].ToString() == "Manager")
            {
                b = btnGetManager;
                command = "DeleteManager(id=\"" + btnEdit.CommandArgument.Trim() + "\")";
                b.CommandArgument = "GetManagerId()";
            }
            ExecuteCommand(facial.DeviceIP, facial.DevicePort, command);
            if (ViewState["Output"] != null || !String.IsNullOrEmpty(ViewState["Output"].ToString()))
            {
                if (ViewState["Output"].ToString() == "The Device is not connected")
                {
                    return;
                }
                string lblDetail = ViewState["Output"].ToString();
                if (lblDetail.Contains("success"))
                {
                   // btnPullUser_Click(this, EventArgs.Empty);
                    ImpromptuHelper.ShowPrompt("Employee Deleted!");
                }
                    
                else
                    ImpromptuHelper.ShowPrompt("Machine Not Connected!");
                btnPullUser_Click(b, EventArgs.Empty);
            }
            int device = Convert.ToInt32(ViewState["DeviceId"].ToString());
            AddLog(device, "Delete Employee " + btnEdit.CommandArgument.Trim());
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvEmployees_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (gvEmployees.Rows.Count > 0)
            {
                gvEmployees.UseAccessibleHeader = false;
                gvEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/presentationlayer/ErrorPage.aspx", false);
        }
    }
    protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                facial.EmployeeCode = Convert.ToInt32(txtEmployeeCode.Text);
                DataTable dt = facial.FacialMachineStatusFetchDeviceByEmployeeCode(facial);
                if (dt.Rows.Count > 0)
                {
                    gvCenter.DataSource = dt;
                    gvCenter.DataBind();
                    if (gvCenter.Rows.Count > 0)
                    {
                        gvCenter.Columns[12].Visible = false;
                        gvCenter.Columns[13].Visible = false;
                    }
                    gridPanel.Visible = true;
                    pan_Search.Visible = true;
                }
                else
                {
                    ImpromptuHelper.ShowPrompt("Please verify Employee Code again!");
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        try
        {
            string region = "0";
            if (Session["RegionID"] != null)
            {
                region = Session["RegionID"].ToString();
            }
            string repStr = "{vw_AttendanceReportSummary.PMonth}='" + Session["CurrentMonth"].ToString().Trim() + "'";
            repStr = repStr + " and {vw_AttendanceReportSummary.SchoolRegion_ID}=" + region;
            Session["reppath"] = "Reports\\FacialMachineReport.rpt";
            Session["rep"] = "FacialMachineReport.rpt";
            Session["CriteriaRpt"] = repStr;
            Session["LastPage"] = "~/FacialMachineStatus.aspx";
            Response.Redirect("~/rptAllReports.aspx", false);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void txtComments_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox btnEdit = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)btnEdit.NamingContainer;
            gvDeviceDetails.SelectedIndex = gvr.RowIndex;

            facial.DeviceID = gvr.Cells[2].Text;


            TextBox txt = gvr.FindControl("txtComments") as TextBox;
            facial.Comments = txt.Text;
            facial.FacialMachineStatusCommentsUpdate(facial);
            btnCancel.Focus();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }

    protected void btnCheckAll_Click(object sender, EventArgs e)
    {
        try
        {
            string Answer, output;
            if (Session["RegionID"] == null)
                facial.RegionID = 0;
            else
                facial.RegionID = Convert.ToInt32(Session["RegionID"].ToString());
            DataTable dt = facial.FacialMachineStatusFetch(facial, 0);
            foreach (DataRow drr in dt.Rows)
            {
                facial.DeviceIP = drr["DeviceIP"].ToString();
                facial.DevicePort = Convert.ToInt32(drr["DevicePort"].ToString());
                string Time = "\"" + DateTime.Now.ToString("HH:mm:ss") + "\"";
                string date = "\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\"";
                string command = "GetDeviceInfo()";
                try
                {
                    using (FaceId Client = new FaceId(facial.DeviceIP, facial.DevicePort))
                    {
                        FaceId_ErrorCode ErrorCode = Client.Execute(command, out Answer, DeviceCodePage);
                        if (ErrorCode == FaceId_ErrorCode.Success)
                        {
                            output = Answer;
                            Console.WriteLine(String.Format("\n\nDevice Id: " + drr["DeviceID"].ToString() + "  " + output));
                            facial.DeviceID = drr["DeviceID"].ToString();
                            facial.ConnectionStatus = "Connected";
                        }
                        else
                        {
                            output = "Device Not Connected!";
                            Console.WriteLine(String.Format("\n\nDevice Id: " + drr["DeviceID"].ToString() + "  " + output));
                            facial.DeviceID = drr["DeviceID"].ToString();
                            facial.ConnectionStatus = "Not Connected";
                            facial.FacialMachineStatusDeviceStatus(facial);
                        }
                    }
                    GetMachineRecord(Answer, facial.DeviceID);
                }
                catch
                {
                    output = "Device Not Connected!";
                    Console.WriteLine(String.Format("\n\nDevice Id: " + drr["DeviceID"].ToString() + "  " + output));
                    facial.DeviceID = drr["DeviceID"].ToString();
                    facial.ConnectionStatus = "Not Connected";
                    facial.FacialMachineStatusDeviceStatus(facial);
                    continue;
                }
            }
            ImpromptuHelper.ShowPrompt("Please go to Attendance Reports(RO)-> 'Facial Machine Details' to check updated details.");
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
}