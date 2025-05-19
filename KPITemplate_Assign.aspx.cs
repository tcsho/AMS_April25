using ADG.JQueryExtenders.Impromptu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class KPITemplate_Assign : System.Web.UI.Page
{
    BLLKPITemplate objbll = new BLLKPITemplate();
    BLLKPITemplateAssign objbll1 = new BLLKPITemplateAssign();
    public int AssignMasterID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindKPITemplate();
        }
    }
    public void BindKPITemplate()
    {
        DALBase objBase = new DALBase();
        DALBase oDALBase = new DALBase();
        DataSet ods = new DataSet();
        ods = null;

        ods = oDALBase.get_KPITemplate();

        DataTable dt = ods.Tables[0];

        objBase.FillDropDown(dt, ddlTemplate, "TemplateId", "TemplateName_Year");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("KPISelection.aspx", false);
    }

    protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = objbll.KPITemplateFetchbyID(Convert.ToInt32(ddlTemplate.SelectedValue));
        
        if (dt.Rows.Count > 0)
        {
            lblYear.Text = dt.Rows[0]["Year"].ToString();
            DateTime fromDate = Convert.ToDateTime(dt.Rows[0]["FromDate"]);
            lblFromDate.Text = fromDate.ToString("dd/MM/yyyy");
            DateTime ToDate = Convert.ToDateTime(dt.Rows[0]["ToDate"]);
            lblToDate.Text = ToDate.ToString("dd/MM/yyyy");
        }
    }
    private void BindEmployeeGrid()
    {
        try
        {
            BLLKPITemplate obj = new BLLKPITemplate();
            DataTable dt = objbll.KPIEmployeeFetchbyCatRegion(ddlEmpCat.Text,ddlRegion.SelectedItem.Text);

            gvEmployeeList.DataSource = dt;
            gvEmployeeList.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    private void BindSingleEmployeeGrid()
    {
        try
        {
            BLLKPITemplate obj = new BLLKPITemplate();
            DataTable dt = objbll.KPIEmployeeFetchbyEmployeeCode(Convert.ToInt32(txtEmpId.Text));

            gvEmployeeList.DataSource = dt;
            gvEmployeeList.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegion.Text != "")
        {
            BindEmployeeGrid();
            gvEmployeeList.Visible = true;
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            int nAlreadyIn = 0;
            DataTable dt = new DataTable();
            bool isok = true;
            string _displymsg = "";

            var username = Session["UserName"].ToString();
            if (ddlTemplate.SelectedValue == "0")
            {
                isok = false;
                _displymsg = "Please Select KPI Template !";
            }
            else if (ddlEmpCat.SelectedValue == "0")
            {
                isok = false;
                _displymsg = "Please Select Employee Category !";
            }
            //else if (ddlRegion.SelectedValue == "0")
            //{
            //    isok = false;
            //    _displymsg = "Please Select Region !";
            //}
            if (isok)
            {
                BLLKPITemplateAssign objMaster = new BLLKPITemplateAssign();
                
                objMaster.TemplateId = Convert.ToInt32(ddlTemplate.SelectedValue);
                objMaster.EmpCategory = ddlEmpCat.SelectedValue;
                objMaster.RegionID = Convert.ToInt32(ddlRegion.SelectedValue);
                objMaster.CreatedBy = username;
                objMaster.CreatedDate = DateTime.Now;
                int newTemplateId = objMaster.KPITemplateAssignAdd(objMaster);
                if (newTemplateId > 0)
                {
                    foreach (GridViewRow row in gvEmployeeList.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect != null && chkSelect.Checked)
                        {
                            string empId = gvEmployeeList.DataKeys[row.RowIndex]["EmployeeCode"].ToString();

                            ListBox lstKPI = (ListBox)row.FindControl("lstKPI");
                            ListBox lstKPI1 = (ListBox)row.FindControl("lstKPI1");
                            ListBox lstKPI2 = (ListBox)row.FindControl("lstKPI2");

                            string assignCenter = string.Join(",", lstKPI.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
                            string assignKeyStage = string.Join(",", lstKPI1.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
                            string assignClass = string.Join(",", lstKPI2.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));

                            // 1. Add into Assign Detail
                            BLLKPITemplateAssignDetail detail = new BLLKPITemplateAssignDetail();
                            detail.AssignMasterID = newTemplateId;
                            detail.EmployeeID = empId;
                            detail.AssignCenters = assignCenter;
                            detail.AssignSIQAKS = assignKeyStage;
                            detail.AssignClass = assignClass;
                            detail.IsAssigned = true;
                            detail.AssignedDate = DateTime.Now;

                            int assignDetailId = detail.KPITemplateAssignDetailAddWithReturn(detail); // You should return ID from DB

                            BLLKPIEmployeeWiseDetail objD = new BLLKPIEmployeeWiseDetail();
                            DataTable dtdetail = objD.KPITemplateFetchbyID(Convert.ToInt32(ddlTemplate.SelectedValue)); // Assume this returns your DataTable
                            //List<BLLKPITemplateDetail> kpiDetails = new List<BLLKPITemplateDetail>();

                            foreach (DataRow rows in dtdetail.Rows)
                            {
                                objD.AssignDetailID = Convert.ToInt32(assignDetailId);
                                objD.TemplateDetailID = Convert.ToInt32(rows["Id"]);
                                objD.KPIName = rows["KPIName"].ToString();
                                objD.Weight =  rows["Weight"].ToString();
                                objD.Grade5_Max = rows["Grade5_Max"].ToString();
                                objD.Grade5_Min = rows["Grade5_Min"].ToString();
                                objD.Grade4_Max = rows["Grade4_Max"].ToString();
                                objD.Grade4_Min = rows["Grade4_Min"].ToString();
                                objD.Grade3_Max = rows["Grade3_Max"].ToString();
                                objD.Grade3_Min = rows["Grade3_Min"].ToString();
                                objD.Grade2_Max = rows["Grade2_Max"].ToString();
                                objD.Grade2_Min = rows["Grade2_Min"].ToString();
                                objD.Grade1_Max = rows["Grade1_Max"].ToString();
                                objD.Grade1_Min = rows["Grade1_Min"].ToString();

                                objD.KPITemplateDetailAdd(objD);
                            }

                        }
                    }
                    drawMsgBox("Data added successfully.", 1);
                    //BindEmployeeGrid();
                }
                else if (ddlTemplate.SelectedValue != "0")
                {
                    BLLKPITemplate obj1 = new BLLKPITemplate();
                    DataTable dtmain = obj1.KPIEmployeeFetchbyCatRegion(ddlEmpCat.Text, ddlRegion.SelectedItem.Text);
                    int AssignMasterId = Convert.ToInt32(dtmain.Rows[0]["AssignMasterID"].ToString());
                    BLLKPITemplateAssignDetail obj = new BLLKPITemplateAssignDetail();
                    obj.AssignMasterID = AssignMasterId;
                    BLLKPITemplateAssignDetail objbll = new BLLKPITemplateAssignDetail();
                    objbll.KPITemplateAssignDetailDelete(obj);
                    
                    foreach (GridViewRow row in gvEmployeeList.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect != null && chkSelect.Checked)
                        {
                            string empId = gvEmployeeList.DataKeys[row.RowIndex]["EmployeeCode"].ToString();

                            // Get ListBox controls
                            ListBox lstKPI = (ListBox)row.FindControl("lstKPI");
                            ListBox lstKPI1 = (ListBox)row.FindControl("lstKPI1");
                            ListBox lstKPI2 = (ListBox)row.FindControl("lstKPI2");

                            string assignCenter = string.Join(",", lstKPI.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
                            string assignKeyStage = string.Join(",", lstKPI1.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
                            string assignClass = string.Join(",", lstKPI2.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));

                            // Add detail
                            BLLKPITemplateAssignDetail detail = new BLLKPITemplateAssignDetail();
                            detail.AssignMasterID = AssignMasterId;
                            detail.EmployeeID = empId;
                            detail.AssignCenters = assignCenter;
                            detail.AssignSIQAKS = assignKeyStage;
                            detail.AssignClass = assignClass;
                            detail.IsAssigned = true;
                            detail.AssignedDate = DateTime.Now;
                            // Call detail insert
                            detail.KPITemplateAssignDetailAddWithReturn(detail);
                        }
                    }
                    drawMsgBox("Data Updated Successfully.", 1);
                    BindEmployeeGrid();
                }
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
    protected void gvEmployeeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeList.PageIndex = e.NewPageIndex;
        BindEmployeeGrid();
    }
    protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            // --- Bind and Select Key Stages ---
            ListBox lstKPI = (ListBox)e.Row.FindControl("lstKPI");
            if (ddlEmpCat.SelectedItem.Text == "NP")
            {
                
                if (lstKPI != null)
                {
                    string Centers = drv["AssignCenters"] != DBNull.Value ? drv["AssignCenters"].ToString() : string.Empty;

                    DataTable dtCenters = GetCenterData(ddlRegion.SelectedValue);
                    lstKPI.DataSource = dtCenters;
                    lstKPI.DataTextField = "Center_Name";
                    lstKPI.DataValueField = "Center_ID";
                    lstKPI.DataBind();

                    string[] selectedCenters = Centers.Split(',');
                    foreach (ListItem item in lstKPI.Items)
                    {
                        if (selectedCenters.Contains(item.Value))
                            item.Selected = true;
                    }
                    gvEmployeeList.Columns[3].Visible = true;
                    gvEmployeeList.Columns[4].Visible = false;
                    gvEmployeeList.Columns[5].Visible = false;
                }
            // --- Bind and Select Key Stages ---
           
            }
            else
            {
                ListBox lstKPI1 = (ListBox)e.Row.FindControl("lstKPI1");
                if (lstKPI1 != null)
                {
                    string siqaKS = drv["AssignSIQAKS"] != DBNull.Value ? drv["AssignSIQAKS"].ToString() : string.Empty;

                    DataTable dtKeyStages = GetKeyStageData();
                    lstKPI1.DataSource = dtKeyStages;
                    lstKPI1.DataTextField = "Group_Name";
                    lstKPI1.DataValueField = "Group_ID";
                    lstKPI1.DataBind();

                    string[] selectedKS = siqaKS.Split(',');
                    foreach (ListItem item in lstKPI1.Items)
                    {
                        if (selectedKS.Contains(item.Value))
                            item.Selected = true;
                    }
                    gvEmployeeList.Columns[4].Visible = true;
                }

                // --- Bind and Select Classes ---
                ListBox lstKPI2 = (ListBox)e.Row.FindControl("lstKPI2");
                if (lstKPI2 != null)
                {
                    string assignClass = drv["AssignClass"] != DBNull.Value ? drv["AssignClass"].ToString() : string.Empty;
                    //if (!string.IsNullOrEmpty(assignClass))
                    //{
                    DataTable dtClasses = GetClassData();
                    lstKPI2.DataSource = dtClasses;
                    lstKPI2.DataTextField = "Class_Name";
                    lstKPI2.DataValueField = "Class_Id";
                    lstKPI2.DataBind();

                    string[] selectedCls = assignClass.Split(',');
                    foreach (ListItem item in lstKPI2.Items)
                    {
                        if (selectedCls.Contains(item.Value))
                            item.Selected = true;
                    }
                    gvEmployeeList.Columns[5].Visible = true;
                    //}
                }
                gvEmployeeList.Columns[3].Visible = false;
                gvEmployeeList.Columns[4].Visible = true;
                gvEmployeeList.Columns[5].Visible = true;
            }
        }


    }

    private DataTable GetKeyStageData()
    {
        // Dummy data or DB call
        DataTable dt = new DataTable();
        dt.Columns.Add("Group_ID");
        dt.Columns.Add("Group_Name");
        dt.Rows.Add("1", "EYFS");
        dt.Rows.Add("2", "KS1");
        dt.Rows.Add("3", "KS2");
        dt.Rows.Add("4", "KS3");
        dt.Rows.Add("5", "KS4");
        dt.Rows.Add("6", "KS5");
        dt.Rows.Add("7", "MATRIC");
        return dt;
    }

    private DataTable GetClassData()
    {
        // Dummy data or DB call
        DataTable dt = new DataTable();
        dt.Columns.Add("Class_Id");
        dt.Columns.Add("Class_Name");
        dt.Rows.Add("2", "Playgroup");
        dt.Rows.Add("3", "Nursery");
        dt.Rows.Add("4", "Kindergarten");
        dt.Rows.Add("5", "Class 1");
        dt.Rows.Add("6", "Class 2");
        dt.Rows.Add("7", "Class 3");
        dt.Rows.Add("8", "Class 4");
        dt.Rows.Add("9", "Class 5");
        dt.Rows.Add("10", "Class 6");
        dt.Rows.Add("11", "Class 7");
        dt.Rows.Add("12", "Class 8");
        dt.Rows.Add("13", "Class 9 (O Level)");
        dt.Rows.Add("14", "Class 10 (O Level)");
        dt.Rows.Add("15", "Class 11 (O Level)");
        dt.Rows.Add("17", "9 M(Matric)");
        dt.Rows.Add("18", "10 M(Matric)");
        dt.Rows.Add("19", "A-1 (A Level)");
        dt.Rows.Add("20", "A-2 (A Level)");
        dt.Rows.Add("89", "XI(Intermediate)");
        dt.Rows.Add("90", "XII(Intermediate)");
        dt.Rows.Add("91", "Class 9 (IGCSE)");
        dt.Rows.Add("92", "Class 10 (IGCSE)");
        dt.Rows.Add("93", "Class 11 (IGCSE)");
        
        return dt;
    }

    private DataTable GetCenterData(string regionid)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Center_ID");
        dt.Columns.Add("Center_Name");
        if (regionid == "20000000")
        {
            dt.Rows.Add("20101002", "CEPD");
            dt.Rows.Add("20101003", "Malir Project");
            dt.Rows.Add("20101004", "FB Area Project");
            dt.Rows.Add("20102001", "Guest House - Karachi Guest House");
            dt.Rows.Add("20102002", "Guest House-Bath Island");
            dt.Rows.Add("20102003", "GH-Jehanzab Farm House-SR");
            dt.Rows.Add("20102004", "Guest House-Quetta");
            dt.Rows.Add("20103001", "S001- (TCS) Clifton Karachi");
            dt.Rows.Add("20103002", "S002- (TCS) Defence Karachi");
            dt.Rows.Add("20103003", "S003- (TCS) Defence Jr Karachi");
            dt.Rows.Add("20103004", "S004-(TCS) Darakhshan Campus Karachi");
            dt.Rows.Add("20104001", "S012- (TCS) Gulshan B Campus Karachi");
            dt.Rows.Add("20104002", "S013- (TCS) Gulshan A Boys Campus Kar");
            dt.Rows.Add("20104003", "S014- (TCS) Gulshan I Karachi");
            dt.Rows.Add("20104004", "S016- (TCS) Gulshan A Jr Karachi");
            dt.Rows.Add("20104005", "S020- (TCS) Gulshan Jr Karachi");
            dt.Rows.Add("20104006", "S021- (TCS) Gulshan F Jr Karachi");
            dt.Rows.Add("20104007", "S022- (TCS) Gulshan C Girls Campus Karachi");
            dt.Rows.Add("20104008", "S023- (TCS) Gulshan Baitul Mukaram Karachi");
            dt.Rows.Add("20104009", "S024- (TCS) Gulshan A Level Karachi");
            dt.Rows.Add("20105001", "S025- (TCS) NN I Karachi");
            dt.Rows.Add("20105002", "S026- (TCS) NN A Jr Karachi");
            dt.Rows.Add("20105003", "S027- (TCS) NN B Jr Karachi");
            dt.Rows.Add("20105004", "S028- (TCS) NN C Jr Karachi");
            dt.Rows.Add("20105005", "S029- (TCS) NN D Jr Karachi");
            dt.Rows.Add("20105006", "S031- (TCS) NN Boys Campus Karachi");
            dt.Rows.Add("20105007", "S032- (TCS) NN Girls Campus Karachi");
            dt.Rows.Add("20105008", "S034- (TCS) NN A Level Karachi");
            dt.Rows.Add("20106001", "S035- (TCS) PAF Chapter Karachi");
            dt.Rows.Add("20106002", "S081- (TCS) PAF Malir Campus");
            dt.Rows.Add("20107001", "S036- (TCS) KAECHS Jr Karachi");
            dt.Rows.Add("20107002", "S037- (TCS) Bahadurabad Karachi");
            dt.Rows.Add("20107003", "S038- (TCS) Tipu Sultan Karachi");
            dt.Rows.Add("20107004", "S039- (TCS) PECHS III Karachi");
            dt.Rows.Add("20107005", "S069- (TCS) PECHS Jr C Karachi");
            dt.Rows.Add("20107006", "S071- (TCS) PECHS Campus");
            dt.Rows.Add("20108001", "S008- (TCS) Gulistan-e-Johar I Karachi");
            dt.Rows.Add("20108002", "S007- (TCS) Gulistan-e-Johar Campus Karachi");
            dt.Rows.Add("20108003", "S011- (TCS) Gulistan-e-Johar Uni Rd Camp");
            dt.Rows.Add("20108005", "S082- (TCS) Gulistan-e-Johar Uni Rd Branch");
            dt.Rows.Add("20108006", "S083- (TCS) Ink City Campus Hyderabad");
            dt.Rows.Add("20109001", "S010- (TCS) Gulshan-e-Maymar Campus Karachi");
            dt.Rows.Add("20110001", "S070- (TCS) Gulshan e Hadeed");
            dt.Rows.Add("20110002", "S079- (TCS) Gulshan-e-Hadeed Campus");
            dt.Rows.Add("20112001", "ITEP SR Branch");
            dt.Rows.Add("20112002", "ITEP PAF Chapter");
            dt.Rows.Add("20113001", "S077-(TCS) Quaid-E-Azam Campus Karachi");
            dt.Rows.Add("20114001", "S076- The City School International");
            dt.Rows.Add("20201001", "S063- (TCS) Quetta Cantt");
            dt.Rows.Add("20201002", "S064- (TCS) Quetta Cantt");
            dt.Rows.Add("20201003", "S065- (TCS) Quetta I");
            dt.Rows.Add("20201004", "S066- (TCS) Quetta II");
            dt.Rows.Add("20201005", "S067- (TCS) Quetta Campus");
            dt.Rows.Add("20201006", "S080- (TCS) Chiltan Quetta");
            dt.Rows.Add("20301001", "S046- TCS Qasimabad I Hyderabad");
            dt.Rows.Add("20301002", "S047- (TCS) Qasimabad II Hyderabad");
            dt.Rows.Add("20301003", "S048- (TCS) Jinnah Campus Qasimabad Hyd");
            dt.Rows.Add("20301004", "S049- (TCS) Latifabad I Hyderabad");
            dt.Rows.Add("20301005", "S050- (TCS) Pre-Junior Latifabad II Hyd");
            dt.Rows.Add("20301006", "S052- (TCS) Liaqat Campus Khosar Hyderabad");
            dt.Rows.Add("20301007", "S074- (TCS) III Jamshoro Hyderabad");
            dt.Rows.Add("20401001", "S060- (TCS) Indus Sukkur Campus");
            dt.Rows.Add("20501001", "S056- (TCS) Nawabshah Campus");
            dt.Rows.Add("20501002", "S073- (TCS) Nawabshah");
            dt.Rows.Add("20601001", "S057- (TCS) Khairpur1");
            dt.Rows.Add("20601002", "S058- (TCS) Khairpur");
            dt.Rows.Add("20601003", "S072- (TCS) Khairpur Campus");
            dt.Rows.Add("20701001", "S055- (TCS) Thatta Campus");
            dt.Rows.Add("20801001", "S053- (TCS) Mirpur Khas");
            dt.Rows.Add("20801002", "S054- (TCS) Mirpur Khas Campus");
            dt.Rows.Add("20901001", "S061- (TCS) Larkana Campus");
            dt.Rows.Add("20901002", "S062- (TCS) Larkana");
            dt.Rows.Add("21001001", "S078- (TCS) Jacobabad");
        }
        else if (regionid == "30000000")
        {
            dt.Rows.Add("30103001", "N046-(TCS) F-8 Jr ISD");
            dt.Rows.Add("30103002", "N038-(TCS) E-11 Campus ISD");
            dt.Rows.Add("30103003", "N043-(TCS) G-15 ISD");
            dt.Rows.Add("30103004", "N055-(TCS) Bahria Campus ISD");
            dt.Rows.Add("30103006", "N056-(TCS) PWD Campus RWP");
            dt.Rows.Add("30103007", "N013-(TCS) DHA Campus ISD");
            dt.Rows.Add("30103008", "N060-(TCS) B-17 RWP");
            dt.Rows.Add("30103009", "N055-(TCS) Bahria Campus ISD-Disabled");
            dt.Rows.Add("30104001", "N036-(TCS) Capital Campus ISD");
            dt.Rows.Add("30105001", "ITEP NR Branch");
            dt.Rows.Add("30201001", "N035-(TCS) Cantt Rwp");
            dt.Rows.Add("30201002", "N037-(TCS) Rawal Campus RWP");
            dt.Rows.Add("30201003", "N010-(TCS) Harley Campus Rwp");
            dt.Rows.Add("30201004", "N006-(TCS) Cantt Jr RWP");
            dt.Rows.Add("30201005", "N007-(TCS) Civil Line Jr RWP");
            dt.Rows.Add("30201006", "N020-(TCS) Gulrez Jr Rwp");
            dt.Rows.Add("30201007", "N058-(TCS) Rawal Campus RWP");
            dt.Rows.Add("30202001", "N003-(TCS) Satellite Town Campus Rwp");
            dt.Rows.Add("30301001", "N016-(TCS) Wah Jr");
            dt.Rows.Add("30401001", "N015-(TCS) Attock Campus");
            dt.Rows.Add("30501001", "N009-(TCS) Chakwal Campus");
            dt.Rows.Add("30501002", "N062-(TCS) Talagang");
            dt.Rows.Add("30601001", "N026-(TCS) Mianwali Campus");
            dt.Rows.Add("30601002", "N034-(TCS) Chashma Campus");
            dt.Rows.Add("30701001", "N045-(TCS) Abbottabad Campus");
            dt.Rows.Add("30701002", "N021-(TCS) Abbottabad");
            dt.Rows.Add("30701003", "N022-Orush Dale Nursery Campus Abbottabad");
            dt.Rows.Add("30801001", "N039-(TCS) Kohat Campus");
            dt.Rows.Add("30901001", "N014-(TCS) Bannu Campus");
            dt.Rows.Add("31001001", "N044-(TCS) D.I.Khan Campus");
            dt.Rows.Add("31101001", "N050-(TCS) Nowshera");
            dt.Rows.Add("31101002", "N001-(TCS) Nowshera Campus");
            dt.Rows.Add("31201001", "N052-(TCS) Mardan");
            dt.Rows.Add("31201002", "N024-(TCS) Mardan Campus");
            dt.Rows.Add("31301001", "N033-(TCS) Swabi Campus");
            dt.Rows.Add("31401001", "N004-(TCS) Swat");
            dt.Rows.Add("31401002", "N008-(TCS) Swat Campus");
            dt.Rows.Add("31401003", "N061-(TCS) Timergarah");
            dt.Rows.Add("31501001", "N028-(TCS) Cantt Sr Psw");
            dt.Rows.Add("31501002", "N017-(TCS) Hayatabad Psw");
            dt.Rows.Add("31501003", "N048-(TCS) Hayatabad Jr Psw");
            dt.Rows.Add("31501004", "N057-(TCS) Nishterabad Psw");
            dt.Rows.Add("31501005", "N029-(TCS) Peshawar Campus");
            dt.Rows.Add("31501006", "N012-(TCS) Uni Town Campus Psw");
            dt.Rows.Add("31501007", "N027-(TCS) Jr Uni Town Psw");
            dt.Rows.Add("31501008", "N040-(TCS) Warsak Campus Psw");
            dt.Rows.Add("31501009", "N059-(TCS) Phase 6 Hayatabad Psw");
            dt.Rows.Add("31601001", "N030-(TCS) Muzaffarabad Campus");
            dt.Rows.Add("31701001", "N019-(TCS) Mirpur Campus");
            dt.Rows.Add("80103001", "NS001-TSS I-8 Cmp ISD");
            dt.Rows.Add("80201001", "NS002-TSS Uni Twn Cmp Psw");
            dt.Rows.Add("80301001", "NS003-TSS Swat Campus");
            dt.Rows.Add("80301002", "TSS-COC Network");
        }
        else if (regionid == "40000000")
        {
            dt.Rows.Add("40101001", "Regional Office-Central");
            dt.Rows.Add("40102001", "Guest House-CR");
            dt.Rows.Add("40103001", "C002-(TCS) Shalimar Campus LHR");
            dt.Rows.Add("40103002", "C053-(TCS) Alpha Campus");
            dt.Rows.Add("40104001", "C007-(TCS) Shalimar A Levels");
            dt.Rows.Add("40104002", "C008-(TCS) Model Town LHR");
            dt.Rows.Add("40104003", "C010-(TCS) Model Town Campus LHR");
            dt.Rows.Add("40104004", "C014-(TCS) Model Town Link Rd LHR");
            dt.Rows.Add("40105001", "C013-(TCS)Boys Muslim Town");
            dt.Rows.Add("40105002", "C016-(TCS) Allama Iqbal Town LHR");
            dt.Rows.Add("40105003", "C009-(TCS) Shadman LHR");
            dt.Rows.Add("40105004", "C012-TCS Muslim Town Campus");
            dt.Rows.Add("40106001", "C004-(TCS) Ravi Campus LHR");
            dt.Rows.Add("40106002", "C052-(TCS) Bahria Town Campus");
            dt.Rows.Add("40107001", "C001-(TCS) DHA Campus LHR");
            dt.Rows.Add("40108001", "C003-(TCS) Paragon Campus LHR");
            dt.Rows.Add("40109001", "ITEP CR Branch");
            dt.Rows.Add("40109002", "ITEP HO Branch");
            dt.Rows.Add("40109003", "ITEP DHA");
            dt.Rows.Add("40109102", "TMC CR Branch");
            dt.Rows.Add("40201001", "C029-(TCS) Gujranwala DC Road");
            dt.Rows.Add("40201002", "C027-(TCS) Khanpur Sansi Campus");
            dt.Rows.Add("40201003", "C028-(TCS) Cantt Campus Gujranwala");
            dt.Rows.Add("40301001", "C041-(TCS) Sialkot Cantt. Jr.");
            dt.Rows.Add("40302001", "C043-(TCS) Sialkot Iqbal Campus");
            dt.Rows.Add("40401001", "C030-(TCS) Gujrat");
            dt.Rows.Add("40501001", "C025-(TCS) Jhelum Branch");
            dt.Rows.Add("40601001", "C031-(TCS) Kharian");
            dt.Rows.Add("40701001", "C018-(TCS) Joharabad Campus");
            dt.Rows.Add("40801001", "C017-(TCS) Jhang Sr. Branch");
            dt.Rows.Add("40801002", "C022-(TCS) Jhang");
            dt.Rows.Add("40901001", "C021-(TCS) Sargodha Campus");
            dt.Rows.Add("40901002", "C020-(TCS) College Campus Sargodha");
            dt.Rows.Add("41001001", "C019-(TCS) FSD Chenab Campus");
            dt.Rows.Add("41001002", "C024-(TCS) FSD Jr. Branch People's Colony");
            dt.Rows.Add("41001003", "C023-(TCS) FSD Jr. Branch Civil Lines");
            dt.Rows.Add("41101001", "C011-(TCS) Kasur");
            dt.Rows.Add("41201001", "C045-(TCS) Okara");
            dt.Rows.Add("41301001", "C044-(TCS) Sahiwal Campus");
            dt.Rows.Add("41401001", "C034-(TCS) Khanewal");
            dt.Rows.Add("41501001", "C039-(TCS) Multan");
            dt.Rows.Add("41601001", "C037-(TCS) KAPCO");
            dt.Rows.Add("41601002", "C040-(TCS) PARCO");
            dt.Rows.Add("41701001", "C047-(TCS) Burewala");
            dt.Rows.Add("41801001", "C046-(TCS) Vehari");
            dt.Rows.Add("41901001", "C035-(TCS) Bahawalpur");
            dt.Rows.Add("41901002", "C033-(TCS) Bahawalpur Campus");
            dt.Rows.Add("42001001", "C032-(TCS) Rahim Yar Khan");
            dt.Rows.Add("42101001", "C038-(TCS) D.G Khan Civil Lines");
            dt.Rows.Add("42101002", "C036-(TCS) D.G Khan Campus");
        }
        return dt;
    }

    //private DataTable GetKeyStageData()
    //{
    //    return objbll1.FetchKeyStages(); 
    //}
    //private DataTable GetClassData()
    //{
    //    return objbll1.GetClassData(); 
    //}

    protected void ddlEmpCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCat.Text == "Others")
        {
            txtEmpId.Visible = true;
            lblEmp.Visible = true;
            txtEmpId.Text = "";
            lblRegion.Visible = false;
            ddlRegion.Visible = false;
            txtEmpId.Focus();
            gvEmployeeList.DataSource = null;
            gvEmployeeList.DataBind();
            gvEmployeeList.Visible = false;
        }
        else
        {
           
            txtEmpId.Visible = false;
            lblEmp.Visible = false;
            lblRegion.Visible = true;
            ddlRegion.Visible = true;
            gvEmployeeList.DataSource = null;
            gvEmployeeList.DataBind();
            gvEmployeeList.Visible = false;
        }
    }

    protected void txtEmpId_TextChanged(object sender, EventArgs e)
    {
        BindSingleEmployeeGrid();
        gvEmployeeList.Visible = true;
    }
}