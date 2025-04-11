using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;

public partial class EmployeeShifts : System.Web.UI.Page
{
    BLLEmployeeShiftsDetail objBll = new BLLEmployeeShiftsDetail();
    DALBase objbase = new DALBase();

    int UserLevel, UserType;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["employeeCode"] == null)
            {
                Response.Redirect("~/login.aspx");
            }

            UserLevel = Convert.ToInt32(Session["UserLevelID"].ToString());
            UserType = Convert.ToInt32(Session["UserType"].ToString());



            #region 'Roles&Priviliges'


            if (Session["EmployeeCode"] != null)
            {
                //string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                //System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                //string sRet = oInfo.Name;

                //int _part_Id = Convert.ToInt32(Session["UserType"].ToString());

                //int _result = objbase.ApplicationSettings(sRet, _part_Id);


                //if (_result == 1)
                //{
                if (!IsPostBack)
                {
                    try
                    {

                        //======== Page Access Settings ========================
                        DALBase objBase = new DALBase();
                        DataRow row = (DataRow)Session["rightsRow"];
                        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                        string sRet = oInfo.Name;


                        DataTable _dtSettings = objBase.ApplyPageAccessSettingsTable(sRet, Convert.ToInt32(row["User_Type_Id"].ToString()));
                        this.Page.Title = _dtSettings.Rows[0]["PageTitle"].ToString();
                        //tdFrmHeading.InnerHtml = _dtSettings.Rows[0]["PageCaption"].ToString();
                        if (Convert.ToBoolean(_dtSettings.Rows[0]["isAllow"]) == false)
                        {
                            Session.Abandon();
                            Response.Redirect("~/login.aspx");
                        }

                        //====== End Page Access settings ======================
                        ViewState["mode"] = "Add";
                        ViewState["SortDirection"] = "ASC";
                        ViewState["tMood"] = "check";
                        loadMonths();
                        loadEmployees();
                        //bindgrid();

                    }
                    catch (Exception ex)
                    {
                        Session["error"] = ex.Message;
                        Response.Redirect("ErrorPage.aspx", false);
                    }

                }
            #endregion
                //}
                /*else
                    {
                    Session["error"] = "You Are Not Authorized To Access This Page";
                    Response.Redirect("ErrorPage.aspx", false);
                    }
                }*/



            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void loadMonths()
    {

        BLLPeriod obj = new BLLPeriod();

        DataTable dt = new DataTable();
        obj.InActive = "n";
        dt = obj.PeriodFetch(obj);
        if (dt.Rows.Count > 0)
        {
            ddlMonths.DataTextField = "PMonthDesc";
            ddlMonths.DataValueField = "PMonth";
            ddlMonths.DataSource = dt;
            ddlMonths.DataBind();
        }

        ddlMonths.SelectedValue = Session["CurrentMonth"].ToString();
    }

    protected void btnReSubmit_Click(object sender, ImageClickEventArgs e)
    {
        /*ImageButton imgbtn = (ImageButton)sender;
        bllObj.Att_Id = Convert.ToInt32(imgbtn.CommandArgument);
        bllObj.AttendanceUpdateEmpReturn(bllObj);

        ViewState["dtMain"] = null;
        ViewState["dtMainShow"] = null;
        bindgrid();*/

    }


    protected void loadEmployees()
    {

        BLLEmplyeeReportTo obj = new BLLEmplyeeReportTo();
        DataTable dt = new DataTable();


        if (UserLevel == 4)
        {
            obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
            obj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
        }
        else if (UserLevel == 3)
        {
            obj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
            obj.Center_id = 0;
        }
        else if (UserLevel == 1 || UserLevel == 2)
        {
            obj.Region_id = 0;
            obj.Center_id = 0;
        }

        dt = obj.EmployeeprofileSelectByRegionCenter_shiftTimmings(obj);
        objbase.FillDropDown(dt, ddlEmp, "EmployeeCode", "CodeName");

    }



    protected void gvShifts_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtMain"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();

            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            ViewState["dtMain"] = null;
            bindgrid();

        }
        catch (Exception ex)
        {
            //Session["error"] = ex.Message;
            //Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvShifts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvShifts.PageIndex = e.NewPageIndex;

            ViewState["dtMain"] = null;
            bindgrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvShow_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            DataTable _dt = (DataTable)ViewState["dtMainShow"];
            _dt.DefaultView.Sort = e.SortExpression + " " + ViewState["SortDirection"].ToString();

            if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else
            {
                ViewState["SortDirection"] = "ASC";
            }
            ViewState["dtMainShow"] = null;
            bindgrid();

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //gvShow.PageIndex = e.NewPageIndex;

            ViewState["dtMainShow"] = null;
            bindgrid();
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void gvShifts_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        /*if (e.Row.RowIndex != -1)
        {
            DropDownList ddlRoleType = (DropDownList)e.Row.FindControl("ddlRoleType");
            BLLEmployeeLeaveType objBLL = new BLLEmployeeLeaveType();
            objBLL.Status_id = 1;
            objBLL.Used_For = "LVS";
            DataTable objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetchUsed(objBLL);
            ddlRoleType.DataSource = objDt;
            ddlRoleType.DataValueField = "LeaveType_Id";
            ddlRoleType.DataTextField = "LeaveType";
            ddlRoleType.DataBind();
            ddlRoleType.SelectedValue = e.Row.Cells[2].Text;
            DropDownList ddlAprv = (DropDownList)e.Row.FindControl("ddlAprv");
            objDt = new DataTable();

            objDt = objBLL.EmployeeLeaveTypeFetch(1);
            ddlAprv.DataSource = objDt;
            ddlAprv.DataValueField = "Sat";
            ddlAprv.DataTextField = "Descr";
            ddlAprv.DataBind();
        
        }*/

        //    string str=e.Row.Cells[3].Text.Substring(1, 3);


        //if ( str== "Fri") // if Data Locked after ERP PRocesss
        //    {
        //    e.Row.BackColor = System.Drawing.Color.Beige;
        //    e.Row.Enabled = false;
        //    }
    }



    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEdit = (ImageButton)sender;
        int AttID = Convert.ToInt32(btnEdit.CommandArgument);

        GridViewRow grv = (GridViewRow)btnEdit.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlAprv");
        DropDownList ddlLeave = (DropDownList)ctl;

        ctl = (Control)grv.FindControl("ddlRoleType");
        DropDownList ddlLeaveN = (DropDownList)ctl;

        string _reason;
        string _leave;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;
        string _LeaveN = ddlLeaveN.SelectedValue;


        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlTy = null;
        TextBox txtReasonInner = null;
        foreach (GridViewRow gvRow in gvShifts.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlAprv");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            ddlTy = (DropDownList)gvRow.FindControl("ddlRoleType");

            cb = (CheckBox)gvRow.FindControl("CheckBox1");
            if (cb.Checked)
            {
                if (_leave == "False")
                {
                    //gvRow.Cells[14].Enabled = true;
                }
                else
                {
                    // gvRow.Cells[14].Enabled = false;
                    ddlTy.SelectedValue = gvRow.Cells[2].Text;
                }
                ddlLeaveInner.SelectedValue = _leave;
                txtReasonInner.Text = _reason;
                //ddlTy.SelectedValue = _LeaveN;
                cb.Checked = false;
            }
        }

    }

    protected void AssignToAll(string _lvt, string _reas, string _cvr)
    {
        DropDownList ddlLeaveInner = new DropDownList();
        TextBox txtReasonInner = new TextBox();
        CheckBox cb = new CheckBox();
        foreach (GridViewRow gvRow in gvShifts.Rows)
        {
            int _index = gvRow.RowIndex;
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
            txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");
            if (cb.Checked)
            {
                if (gvRow.Cells[1].Text == "1")
                {
                    ddlLeaveInner.SelectedValue = "73";
                    txtReasonInner.Text = "System attachement void, after submission of " + _cvr + ".";
                    cb.Checked = false;
                }
                else
                {
                    ddlLeaveInner.SelectedValue = _lvt;
                    txtReasonInner.Text = _reas;
                    cb.Checked = false;
                }
            }
        }

    }

    protected void drawMsgBox(string msg, int errType)
    {
        try
        {
            ADG.JQueryExtenders.Impromptu.ImpromptuHelper.ShowPrompt(msg);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }
    protected void bindgrid()
    {
        //loadEmpleave();
        gvShifts.DataSource = null;
        //gvShow.DataSource = null;
        try
        {

            DataTable dt = new DataTable();

            objBll.PMonth = ddlMonths.SelectedValue.ToString();
            objBll.EmployeeCode = ddlEmp.SelectedValue;//Session["EmployeeCode"].ToString().Trim();

            if (ViewState["dtMain"] == null)
                dt = objBll.EmployeeShiftsDetailSelectByEmpAndMonth(objBll);
            else
                dt = (DataTable)ViewState["dtMain"];

            gvShifts.DataSource = dt;
            gvShifts.DataBind();
            ViewState["dtMain"] = dt;
        }

        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }

    }

    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dtMain"] = null;
        ViewState["dtMainShow"] = null;
        bindgrid();
    }

    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dtMain"] = null;
        bindgrid();
        if (ddlEmp.SelectedValue != "0")
        {
            btnChangeShift.Attributes.CssStyle.Add("display", "inline");

            //btnMakeHOD_Employee.Attributes.CssStyle.Add("display", "inline");

            objBll.EmployeeCode = ddlEmp.SelectedValue;

            //if (objBll.isEmployeeHOD(objBll))
            //{
            //    btnMakeHOD_Employee.Text = "Make Employee.";
            //}
            //else
            //{
            //    btnMakeHOD_Employee.Text = "Make HOD.";
            //}
            if (gvShifts.Rows.Count <=0)
            {
                startProcess();
            }
        }
        else
        {
            btnChangeShift.Attributes.CssStyle.Add("display", "none");

            //btnMakeHOD_Employee.Attributes.CssStyle.Add("display", "none");
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        /*ImageButton btnEdit = (ImageButton)sender;
        int _id = Convert.ToInt32(btnEdit.CommandArgument);

        GridViewRow grv = (GridViewRow)btnEdit.NamingContainer;

        Control ctl = (Control)grv.FindControl("txtReason");
        TextBox txtReason = (TextBox)ctl;

        ctl = (Control)grv.FindControl("ddlRoleType");
        DropDownList ddlLeave = (DropDownList)ctl;


        string _reason;
        string _leave;

        _reason = txtReason.Text;
        _leave = ddlLeave.SelectedValue;

        if (_reason != string.Empty)
            {

            bllObj.Att_Id = _id;
            bllObj.EmpLvType_Id = Convert.ToInt32(ddlLeave.SelectedValue);
            bllObj.EmpLvReason = txtReason.Text;
            bllObj.ModifyBy = Session["EmployeeCode"].ToString();
            bllObj.ModifyDate = DateTime.Now;
            bllObj.EmpLvSubDate = DateTime.Now;
            bllObj.Submit2HOD = true;

            int dt = bllObj.AttendanceUpdateEmpLeave(bllObj);
            if (dt >= 1)
                {
                //drawMsgBox("Leave Submitted to HOD Successfully!");
                ViewState["dtMain"] = null;
                ViewState["dtMainShow"] = null;
                bindgrid();
                }
            }
        else
            {
            drawMsgBox("Please Must Enter Leave Reason",3);
            }*/

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        CheckBox cb = null;
        DropDownList ddlLeaveInner = null;
        DropDownList ddlAprv = null;
        TextBox txtReasonInner = null;
        Control ctl = null;
        foreach (GridViewRow gvRow in gvShifts.Rows)
        {
            int _id = Convert.ToInt32(gvRow.Cells[0].Text.ToString());
            //ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
            //txtReasonInner = (TextBox)gvRow.FindControl("txtReason");
            //ddlAprv = (DropDownList)gvRow.FindControl("ddlAprv");
            cb = (CheckBox)gvRow.FindControl("CheckBox1");

            if (cb.Checked)
            {

                /*if (txtReasonInner.Text != string.Empty)
                    {

                    bllObj.Att_Id = _id;

                    bllObj.HODLvAprv = Convert.ToBoolean(ddlAprv.SelectedValue);
                    bllObj.HODLvRemarks = txtReasonInner.Text;

                    bllObj.HRLvType_Id = Convert.ToInt32(ddlLeaveInner.SelectedValue);
                    bllObj.Submit2HR = true;

                    bllObj.AppLvBy = Session["EmployeeCode"].ToString();
                    bllObj.AppLvOn = DateTime.Now;

                    bllObj.ModifyBy = Session["EmployeeCode"].ToString();
                    bllObj.ModifyDate = DateTime.Now;

                    int dt = bllObj.AttendanceUpdateEmpHODLvApv(bllObj);
                    if (dt >= 1)
                        {
                        //drawMsgBox("Leave Submitted to HOD Successfully!");
                        ViewState["dtMain"] = null;
                        ViewState["dtMainShow"] = null;
                        ddlEmp.SelectedValue = "0";
                        bindgrid();
                        }
                    }*/
            }
        }
    }

    protected void gvShifts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "toggleCheck")
            {
                CheckBox cb = null;
                string mood = ViewState["tMood"].ToString();

                foreach (GridViewRow gvr in gvShifts.Rows)
                {
                    //if (gvr.Cells[1].Text == "True")
                    //{

                    cb = (CheckBox)gvr.FindControl("CheckBox1");
                    if (mood == "" || mood == "check")
                    {
                        cb.Checked = true;
                        ViewState["tMood"] = "uncheck";
                    }
                    else
                    {
                        cb.Checked = false;
                        ViewState["tMood"] = "check";
                    }
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    }

    protected void loadEmpleave()
    {

        //BLLEmployeeLeaveBalance obj = new BLLEmployeeLeaveBalance();

        //DataTable dt = new DataTable();
        //obj.EmployeeCode = Session["EmployeeCode"].ToString();
        //obj.Year = DateTime.Now.Year.ToString();
        //dt = obj.EmployeeLeaveBalanceFetch(obj);
        //if (dt.Rows.Count > 0)
        //    {
        //    lblCas.Text = dt.Rows[0]["balCasual"].ToString();
        //    lblAnu.Text = dt.Rows[0]["balAnnual"].ToString();
        //    }
    }

    protected void ddlRoleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCurrentDropDownList = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlCurrentDropDownList.Parent.Parent);
        LeavePolicyImple(ddlCurrentDropDownList, grdrDropDownRow);
    }

    protected void ddlAprv_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlAprc = (DropDownList)sender;
        GridViewRow grdrDropDownRow = ((GridViewRow)ddlAprc.Parent.Parent);


        if (ddlAprc.SelectedValue == "False")
        {
            //grdrDropDownRow.Cells[14].Enabled = true;
        }
        else
        {
            //grdrDropDownRow.Cells[14].Enabled = false;
            ddlAprc.SelectedValue = grdrDropDownRow.Cells[1].Text;
        }
    }


    private void LeavePolicyImple(DropDownList ddlCurrentDropDownList, GridViewRow grdrDropDownRow)
    {
        int _valCasul = 0;
        int _valAnual = 0;
        int _valisAnl = 0;

        int _index = grdrDropDownRow.RowIndex;
        _valCasul = LeaveBalanceCounter("61");
        _valAnual = LeaveBalanceCounter("62");

        if (ddlCurrentDropDownList.SelectedValue == "61")   /*Casual Leaves*/
        {

            if (_valCasul > Convert.ToInt32(grdrDropDownRow.Cells[8].Text))
            {
                drawMsgBox("Maximum " + grdrDropDownRow.Cells[8].Text + " casual leaves are allowed.", 3);
                ddlCurrentDropDownList.SelectedValue = "67";
            }
        }
        else if (ddlCurrentDropDownList.SelectedValue == "62") /*Anual Leaves*/
        {
            _valisAnl = objbase.IsAnual(grdrDropDownRow.Cells[3].Text);
            _valAnual = LeaveBalanceCounter("62");

            if (Convert.ToInt32(grdrDropDownRow.Cells[9].Text) < 3)
            {
                if (_valCasul < Convert.ToInt32(grdrDropDownRow.Cells[8].Text))
                {
                    ddlCurrentDropDownList.SelectedValue = "61";
                }
                else
                {
                    ddlCurrentDropDownList.SelectedValue = "67";
                }
            }
            else
            {
                if (_valisAnl < 3)
                {
                    if (_valCasul < Convert.ToInt32(grdrDropDownRow.Cells[8].Text))
                    {
                        ddlCurrentDropDownList.SelectedValue = "61";
                    }
                    else
                    {
                        ddlCurrentDropDownList.SelectedValue = "67";
                    }
                }
                else
                {

                    if (_valAnual >= Convert.ToInt32(grdrDropDownRow.Cells[9].Text))
                    {
                        //Cehck for Casul Balance then Assign
                        if (_valCasul < Convert.ToInt32(grdrDropDownRow.Cells[8].Text))
                        {
                            ddlCurrentDropDownList.SelectedValue = "61";

                        }
                        else
                        {
                            ddlCurrentDropDownList.SelectedValue = "67";
                        }
                    }
                    else
                    {
                        ddlCurrentDropDownList.SelectedValue = "62";
                    }
                }

            }


        }
        else if (ddlCurrentDropDownList.SelectedValue == "73") /*Off Day*/
        {
        }
        else if (ddlCurrentDropDownList.SelectedValue == "2") /*Present*/
        {
            //        OffDaySettings(_index, "73", ddlCurrentDropDownList.SelectedItem.ToString());
        }
        else if (ddlCurrentDropDownList.SelectedValue == "68") /*Offical Tour*/
        {
            //      OffDaySettings(_index, "73", ddlCurrentDropDownList.SelectedItem.ToString());
        }
        else if (ddlCurrentDropDownList.SelectedValue == "72") /*Lieu Off*/
        {
            //    OffDaySettings(_index, "73", ddlCurrentDropDownList.SelectedItem.ToString());
        }
        else if (ddlCurrentDropDownList.SelectedValue == "66") /*Special Leave*/
        {
            //   OffDaySettings(_index, "73", ddlCurrentDropDownList.SelectedItem.ToString());
        }
        else if (ddlCurrentDropDownList.SelectedValue == "67") /*Leave Without Pay*/
        {
            //  OffDaySettings(_index, "67", ddlCurrentDropDownList.SelectedItem.ToString());
        }
    }

    ////private void OffDaySettings(int _index, string _lv)
    ////    {
    ////    string _lvgroup = gvShifts.Rows[_index].Cells[2].Text;
    ////    string _lvOff = gvShifts.Rows[_index].Cells[1].Text;
    ////    int _valCasul = 0;
    ////    int _valAnual = 0;


    ////    foreach (GridViewRow gvRow in gvShifts.Rows)
    ////        {
    ////        if (gvRow.Cells[2].Text == _lvgroup && _index - 1 == gvRow.RowIndex)
    ////            {
    ////            if (gvRow.Cells[1].Text == "1")
    ////                {
    ////                DropDownList ddlI = (DropDownList)gvRow.Cells[7].FindControl("ddlRoleType");
    ////                if (_lv == "61")
    ////                    {
    ////                    _valCasul = LeaveBalanceCounter("61");

    ////                    if (_valCasul > Convert.ToInt32(lblCas.Text))
    ////                        {
    ////                        ddlI.SelectedValue = "67";
    ////                        }
    ////                    else
    ////                        {
    ////                        ddlI.SelectedValue = _lv;
    ////                        }
    ////                    }
    ////                else if (_lv == "62")
    ////                    {
    ////                    _valAnual = LeaveBalanceCounter("62");

    ////                    if (_valAnual > Convert.ToInt32(lblAnu.Text))
    ////                        {
    ////                        ddlI.SelectedValue = "67";
    ////                        }
    ////                    else
    ////                        {
    ////                        ddlI.SelectedValue = _lv;
    ////                        }
    ////                    }
    ////                else
    ////                    {
    ////                    ddlI.SelectedValue = _lv;
    ////                    }



    ////                }
    ////            }
    ////        else if (gvRow.Cells[2].Text == _lvgroup && _index + 1 == gvRow.RowIndex)
    ////            {

    ////            if (gvRow.Cells[1].Text == "1")
    ////                {
    ////                DropDownList ddlI = (DropDownList)gvRow.Cells[7].FindControl("ddlRoleType");
    ////                if (_lv == "61")
    ////                    {
    ////                    _valCasul = LeaveBalanceCounter("61");

    ////                    if (_valCasul >= Convert.ToInt32(lblCas.Text))
    ////                        {
    ////                        ddlI.SelectedValue = "67";
    ////                        }
    ////                    else
    ////                        {
    ////                        ddlI.SelectedValue = _lv;
    ////                        }
    ////                    }
    ////                else if (_lv == "62")
    ////                    {
    ////                    _valAnual = LeaveBalanceCounter("62");

    ////                    if (_valAnual >= Convert.ToInt32(lblAnu.Text))
    ////                        {
    ////                        ddlI.SelectedValue = "67";
    ////                        }
    ////                    else
    ////                        {
    ////                        ddlI.SelectedValue = _lv;
    ////                        }
    ////                    }
    ////                else
    ////                    {
    ////                    ddlI.SelectedValue = _lv;
    ////                    }
    ////                }
    ////            }
    ////        }
    ////    }
    ////private void OffDaySettings(int _index, string _lv, string _cvr)
    ////    {
    ////    string _lvgroup = gvShifts.Rows[_index].Cells[2].Text;
    ////    string _lvOff = gvShifts.Rows[_index].Cells[1].Text;

    ////    foreach (GridViewRow gvRow in gvShifts.Rows)
    ////        {
    ////        if (gvRow.Cells[2].Text == _lvgroup && _index - 1 == gvRow.RowIndex)
    ////            {
    ////            if (gvRow.Cells[1].Text == "1")
    ////                {
    ////                DropDownList ddlI = (DropDownList)gvRow.Cells[7].FindControl("ddlRoleType");
    ////                TextBox txtI = (TextBox)gvRow.Cells[8].FindControl("txtReason");

    ////                ddlI.SelectedValue = _lv;
    ////                txtI.Text = "System attachement void, after submission of " + _cvr + ".";
    ////                }
    ////            }
    ////        else if (gvRow.Cells[2].Text == _lvgroup && _index + 1 == gvRow.RowIndex)
    ////            {

    ////            if (gvRow.Cells[1].Text == "1")
    ////                {
    ////                DropDownList ddlI = (DropDownList)gvRow.Cells[7].FindControl("ddlRoleType");
    ////                TextBox txtI = (TextBox)gvRow.Cells[8].FindControl("txtReason");
    ////                txtI.Text = "System attachement void, after submission of " + _cvr + ".";
    ////                ddlI.SelectedValue = _lv;
    ////                }
    ////            }
    ////        }
    ////    }
    protected int LeaveBalanceCounter(string _val)
    {
        int count = 0;
        DropDownList ddlLeaveInner = null;
        foreach (GridViewRow gvRow in gvShifts.Rows)
        {
            ddlLeaveInner = (DropDownList)gvRow.FindControl("ddlRoleType");
            if (ddlLeaveInner.SelectedValue == _val)
            {
                count = count + 1;
            }
        }
        return count;
    }


    //==================================================================================================================================
    protected void Save()
    {

        bool isChecked = false;
        CheckBox CheckBox1;
        foreach (GridViewRow gvr in gvShifts.Rows)
        {
            CheckBox1 = (CheckBox)gvr.FindControl("CheckBox1");
            if (CheckBox1.Checked)
            {
                isChecked = true;
                break;
            }
        }
        if (isChecked)
        {
            objBll.ActSTime = txtStartTime.Text;
            objBll.ActETime = txtEndTime.Text;
            objBll.AbsentTime = txtAbsentTime.Text;
            objBll.EmployeeCode = ddlEmp.SelectedValue;
            objBll.PMonth = ddlMonths.SelectedValue;

            foreach (GridViewRow gvr in gvShifts.Rows)
            {
                CheckBox1 = (CheckBox)gvr.FindControl("CheckBox1");
                if (CheckBox1.Checked)
                {
                    //objBll.AttDate = DateTime.Parse(gvr.Cells[4].Text);

		    string dateString = gvr.Cells[4].Text.ToString().Trim();

                    DateTime parsedDate;
                    string format = "dddd MM/dd/yyyy";
                    DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                    objBll.AttDate = parsedDate;
                    objBll.EmployeeShiftsDetailUpdate(objBll);
                }
            }
            objBll.SingleEmployee_CompleteProcess(objBll);

            //MPEShift.Hide();
            ViewState["dtMain"] = null;
            bindgrid();
            drawMsgBox("Employee shift has been updated successfully.", 3);


            SingleEmpProcess();
            pan_New.Attributes.CssStyle.Add("display", "none");

        }
        else
        {
            drawMsgBox("Please select a record to change!", 3);
        }
    }

    protected void btnSaveShift_Click(object sender, EventArgs e)
    {
        //drawMsgBox("Shift time has been updated for the selected dates.",3);
        Save();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {




    }

    protected void startProcess()
    {

        BLLAttendance bllobj = new BLLAttendance();

        bllobj.PMonthDesc = ddlMonths.SelectedValue;
        bllobj.EmployeeCode = ddlEmp.SelectedValue;

        //if (UserLevel == 4)
        //    {
        //    bllobj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
        //    bllobj.Center_id = Convert.ToInt32(Session["CenterID"].ToString());
        //    }
        //else if (UserLevel == 3)
        //    {
        //    bllobj.Region_id = Convert.ToInt32(Session["RegionID"].ToString());
        //    bllobj.Center_id = 0;
        //    }
        //else if (UserLevel == 1 || UserLevel == 2)
        //    {
        //    bllobj.Region_id = 0;
        //    bllobj.Center_id = 0;
        //    }

        int AlreadyIn = 0;

        AlreadyIn = bllobj.AttendanceProcessShifts(bllobj);

        if (AlreadyIn > 0)
        {
            ViewState["dtMain"] = null;
            bindgrid();
            drawMsgBox("Shifts generated successfully.", 1);
        }
        else if (AlreadyIn == 0)
        {
            drawMsgBox("Error in process!", 2);
        }

    }
    //protected void btnGenerateProcess_Click(object sender, EventArgs e)
    //{
    //    startProcess();

    //}

    protected void btnChangeShift_Click(object sender, EventArgs e)
    {

        pan_New.Attributes.CssStyle.Add("display", "inline");

    }


    protected void btnCancelShift_Click(object sender, EventArgs e)
    {

        pan_New.Attributes.CssStyle.Add("display", "none");

    }

    protected void btnSingleEmpProcess_Click(object sender, EventArgs e)
    {
        SingleEmpProcess();
    }

    protected void SingleEmpProcess()
    {

        BLLAttendance bllobj = new BLLAttendance();

        bllobj.PMonthDesc = ddlMonths.SelectedValue;
        bllobj.EmployeeCode = ddlEmp.SelectedValue;

        int AlreadyIn = 0;

        AlreadyIn = bllobj.AttendanceProcessSingleEmployee(bllobj);

        if (AlreadyIn > 0)
        {
            drawMsgBox("Attendance Process successfully completed.", 1);
        }
        else if (AlreadyIn == 0)
        {
            drawMsgBox("Error in process!", 2);
        }

    }
    //protected void btnMakeHOD_Employee_Click(object sender, EventArgs e)
    //{
    //    if (ddlEmp.SelectedValue == "0")
    //    {
    //        drawMsgBox("Select an Employee to Make HOD.", 2);

    //    }
    //    else
    //    {
    //        objBll.EmployeeCode = ddlEmp.SelectedValue;

    //        if (objBll.isEmployeeHOD(objBll))
    //        {
    //            objBll.MakeEmployeefromHOD(objBll);

    //            drawMsgBox("HOD changed to Employee Successfully.", 0);
    //        }
    //        else
    //        {
    //            objBll.MakeHODfromEmployee(objBll);

    //            drawMsgBox("Employee changed to HOD Successfully.", 0);
    //        }

    //    }

    //    if (objBll.isEmployeeHOD(objBll))
    //    {
    //        btnMakeHOD_Employee.Text = "Make Employee.";
    //    }
    //    else
    //    {
    //        btnMakeHOD_Employee.Text = "Make HOD.";
    //    }
    //}



    //protected void btnResetLeaves_Click(object sender, ImageClickEventArgs e)
    //{

    //    ImageButton btnReset = (ImageButton)sender;
    //    string emp_code = Convert.ToString(btnReset.CommandArgument);

    //    GridViewRow grv = (GridViewRow)btnReset.NamingContainer;

        
    //    DateTime attDate = Convert.ToDateTime(grv.Cells[4].Text);

    //    objBll.EmployeeCode = emp_code;
    //    objBll.AttDate = attDate;

    //    if (objBll.isLeaveDeduction(objBll))
    //    {
    //        drawMsgBox("Cannot reset deductions.", 0);
    //    }
    //    else
    //    {
    //        objBll.ResetLeave(objBll);

    //        drawMsgBox("Leave reset successfully.", 0);
    //    }

    //}



}
