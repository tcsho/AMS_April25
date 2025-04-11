using System;
using System.Data;
using System.Web.UI.WebControls;
using ADG.JQueryExtenders.Impromptu;

public partial class LmsAppmenuServices : System.Web.UI.Page
{
    int worksiteID = 0;
    DALBase objBase = new DALBase();
    string[] nodeIDs = new string[2];
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
            Response.Redirect("~/ErrorPage.aspx", false);
        }

        if (!IsPostBack)
        {
            //======== Page Access Settings ========================
            /*DALBase objBase = new DALBase();
            isl_amso.dt_AuthenticateUserRow row = (isl_amso.dt_AuthenticateUserRow)Session["rightsRow"];
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            if (objBase.ApplyPageAccessSettings(sRet, row.User_Type_Id) == false)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }*/

            //====== End Page Access settings ======================


            #region 'Roles&Priviliges'


            /*DALBase objbase = new DALBase();
            string _tempCntct = Session["ContactID"].ToString();
            HtmlForm frm = new HtmlForm();
            frm = Form;
            objbase.ApplicationSettings(sRet, "ContentPlaceHolder1", _tempCntct, frm);*/



            #endregion
            loadGroups();


            //wrkTitle.InnerText = "Manage Menu Permissions";
            worksiteID = Convert.ToInt32(Session["wrkSiteID"]);

            ViewState["mode"] = "add";


        }
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/PresentationLayer/LMS/LmsWrkSite.aspx");
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PresentationLayer/LMS/LmsWrkSpace.aspx");
    }

    //protected void wdtModules_NodeClick(object sender, DataTreeNodeClickEventArgs e)
    //{
    //    DataTreeNode dtn = e.Node;
    //    if (dtn.ParentNode == null)
    //    {
    //        ViewState["SelectedModID"] = dtn.Key;
    //        //ToggleDisplay(1);
    //        //LoadModuleDetail(Convert.ToInt32(dtn.Key));
    //        ViewState["mode"] = "edit";
    //    }
    //    else
    //    {
    //        ViewState["SelectedSecID"] = dtn.Key;
    //        //ToggleDisplay(2);
    //        //LoadSectionAndContent(Convert.ToInt32(dtn.Key));
    //        ViewState["mode"] = "edit";
    //    }
    //    //ViewState["SelectedNode"] = dtn;
    //}

    protected void drawMsgBox(string msg)
    {
        try
        {
            ImpromptuHelper.ShowPrompt(msg);

        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("~/ErrorPage.aspx", false);
        }
    }



    protected void loadGroups()
    {
        DALGroups oDALGroups = new DALGroups();
        DataSet ods = new DataSet();
        //isl_amso.dt_AuthenticateUserRow row = (isl_amso.dt_AuthenticateUserRow)Session["rightsRow"];
        ods = null;
        //ods = oDALGroups.get_AllGroups(row.UserLevel_ID);
        ods = oDALGroups.get_AllGroups(Convert.ToInt32(Session["UserLevelID"]));
        list_groupName.Items.Clear();

        objBase.FillDropDown(ods.Tables[0], list_groupName, "User_Type_Id", "GroupName");
        ViewState["dsGroup"] = ods;

    }

    protected void list_groupName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (list_groupName.SelectedValue != "0")
        {

            Load_tree();
            //btnSave.Visible = true;
            treeSection.Style.Add("display", "inline");
        }
        else
        {
            drawMsgBox("Please select a user role to set permissions.");
            MenuTreeView.Nodes.Clear();
            //btnSave.Visible = false;
            treeSection.Style.Add("display", "none");
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        /*if (list_groupName.SelectedValue != "0")
        {
            BLLLmsAppMenuServices objBll = new BLLLmsAppMenuServices();
            string strMarkedObjectIds = "";
            string strUnMarkedObjectIds = "";
            objBll.User_Type_Id = Convert.ToInt32(list_groupName.SelectedValue);

            foreach (TreeNode dtn in MenuTreeView.Nodes)
            {
                if (dtn.Checked)
                {
                    strMarkedObjectIds += dtn.Value + ",";
                }
                else
                {
                    strUnMarkedObjectIds += dtn.Value + ",";
                }
                foreach (TreeNode childNode in dtn.ChildNodes)
                {
                    if (childNode.Checked)
                    {
                        strMarkedObjectIds += childNode.Value + ",";
                    }
                    else
                    {
                        strUnMarkedObjectIds += childNode.Value + ",";
                    }
                }
            }

            if (strMarkedObjectIds != "" && strMarkedObjectIds.Contains(","))
            {
                strMarkedObjectIds = strMarkedObjectIds.Remove(strMarkedObjectIds.LastIndexOf(","));
            }

            if (strUnMarkedObjectIds != "" && strUnMarkedObjectIds.Contains(","))
            {
                strUnMarkedObjectIds = strUnMarkedObjectIds.Remove(strUnMarkedObjectIds.LastIndexOf(","));
            }


            objBll.MarkedObjectIds = strMarkedObjectIds;
            objBll.UnmarkedObjectIds = strUnMarkedObjectIds;
            objBll.LmsAppMenuServicesUpdate(objBll);
            treeSection.Attributes.CssStyle.Add("display", "none");
            drawMsgBox("Permissions saved successfully");
        }*/
        TraverseTree();

    }

    protected DataTable ReadData()
    {
        DataTable dtModules = new DataTable();
        BLLLmsAppMenuServices objBll = new BLLLmsAppMenuServices();
        objBll.User_Type_Id = Convert.ToInt32(list_groupName.SelectedValue);
        dtModules = objBll.LmsAppMenuServicesSelectByUserTypeID(objBll);

        return dtModules;
    }

    protected DataTable GetMenuData(int prntMenuID)
    {
        DataTable dtmenu = new DataTable();
        BLLLmsAppMenu objBll = new BLLLmsAppMenu();
        objBll.PrntMenu_ID = prntMenuID;
        objBll.User_Type_Id = Convert.ToInt32(list_groupName.SelectedValue);
        dtmenu = objBll.LmsAppMenuSelectByPrntMenuID(objBll);
        return dtmenu;
    }

    public void Load_tree()
    {
        MenuTreeView.Nodes.Clear();
        DataTable PrSet = new DataTable();
        PrSet = ReadData();
        ViewState["MenuData"] = PrSet;
        foreach (DataRow dr in PrSet.Rows)
        {
            if (dr["PrntMenu_ID"] != DBNull.Value)
            {
                if (Convert.ToInt32(dr["PrntMenu_ID"]) == 0)
                {
                    TreeNode tnParent = new TreeNode();
                    tnParent.Text = dr["MenuText"].ToString();
                    tnParent.Value = dr["Menu_ID"].ToString();
                    string value = dr["Menu_ID"].ToString();
                    tnParent.Checked = Convert.ToBoolean(dr["isAllow"]);

                    tnParent.Expand();
                    MenuTreeView.Nodes.Add(tnParent);
                    FillChild(tnParent, value);
                }
            }
        }
    }

    public int FillChild(TreeNode parent, string IID)
    {
        DataTable dtM = new DataTable();
        dtM = GetMenuData(Convert.ToInt32(IID));
        if (dtM.Rows.Count > 0)
        {
            foreach (DataRow dr in dtM.Rows)
            {
                TreeNode child = new TreeNode();
                child.Text = dr["MenuText"].ToString();
                child.Value = dr["menu_ID"].ToString();
                string temp = dr["Menu_ID"].ToString();
                child.Checked = Convert.ToBoolean(dr["isAllow"]);

                child.Collapse();
                parent.ChildNodes.Add(child);
                FillChild(child, temp);
            }
            return 0;
        }
        else
        {
            return 0;
        }

    }

    //public int FillChild(DataTreeNode parent, string IID)
    //{
    //    DataTable dtM = new DataTable();
    //    dtM = GetMenuData(Convert.ToInt32(IID));
    //    if (dtM.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in dtM.Rows)
    //        {
    //            DataTreeNode child = new DataTreeNode();
    //            child.Text = dr["MenuText"].ToString();
    //            string temp = dr["Menu_ID"].ToString();
    //            if (child.Nodes.Count > 0)
    //            {
    //                child.CollapseChildren();
    //            }
    //            parent.Nodes.Add(child);

    //            FillChild(child, temp);
    //        }
    //        return 0;
    //    }
    //    else
    //    {
    //        return 0;
    //    }

    //}



    protected string[] GetNodeSelection(TreeNode tn)
    {
        //string strMarkedObjectIds = "";
        //string strUnMarkedObjectIds = "";
        //string[] nodeIDs = new string[2];
        if (tn.Checked)
        {
            //strMarkedObjectIds += tn.Value + ",";
            nodeIDs[0] += tn.Value + ",";
        }

        else
        {
            nodeIDs[1] += tn.Value + ",";
        }
        foreach (TreeNode cn in tn.ChildNodes)
        {
            GetNodeSelection(cn);
        }
        //nodeIDs[0] = strMarkedObjectIds;
        //nodeIDs[1] = strUnMarkedObjectIds;
        return nodeIDs;


        //}



    }

    protected void TraverseTree()
    {
        if (list_groupName.SelectedValue != "0")
        {
            BLLLmsAppMenuServices objBll = new BLLLmsAppMenuServices();
            string strMarkedObjectIds = "";
            string strUnMarkedObjectIds = "";
            objBll.User_Type_Id = Convert.ToInt32(list_groupName.SelectedValue);
            string[] str = new string[2];

            foreach (TreeNode dtn in MenuTreeView.Nodes)
            {
                str = GetNodeSelection(dtn);
            }
            strMarkedObjectIds = str[0];
            strUnMarkedObjectIds = str[1];


            if (strMarkedObjectIds != "" && strMarkedObjectIds.Contains(","))
            {
                strMarkedObjectIds = strMarkedObjectIds.Remove(strMarkedObjectIds.LastIndexOf(","));
            }

            if (strUnMarkedObjectIds != "" && strUnMarkedObjectIds.Contains(","))
            {
                strUnMarkedObjectIds = strUnMarkedObjectIds.Remove(strUnMarkedObjectIds.LastIndexOf(","));
            }


            objBll.MarkedObjectIds = strMarkedObjectIds;
            objBll.UnmarkedObjectIds = strUnMarkedObjectIds;
            objBll.LmsAppMenuServicesUpdate(objBll);
            treeSection.Attributes.CssStyle.Add("display", "none");
            list_groupName.SelectedValue = "0";
            drawMsgBox("Permissions saved successfully");
        }
    }




}
