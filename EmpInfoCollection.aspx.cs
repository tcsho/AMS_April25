using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmpInfoCollection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadEmployeeInformation();
            lblEmp.Text = Session["EmployeeCode"].ToString();
            lblName.Text = Session["First_Name"].ToString() + " " + Session["Middle_Name"].ToString();

            if (Session["RegionName"].ToString() == "")
            {
                lblRegion.Text = "Head Office";
            }
            else
            {
                lblRegion.Text = Session["RegionName"].ToString();
            }
            if (Session["CenterName"].ToString() == "")
            {
                lblCenter.Text = "Head Office";
            }
            else
            {
                lblCenter.Text = Session["CenterName"].ToString();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        if (Session["UserType"] != null )
        {
            Save(); //save record

            if (Session["UserType"].ToString() == "17" || Session["UserType"].ToString() == "18" || Session["UserType"].ToString() == "20" || Session["UserType"].ToString() == "21" || Session["UserType"].ToString() == "23" || Session["UserType"].ToString() == "24")
            {
                 Response.Redirect("EmployeeLeavesSubmissions.aspx", false);
            }
            else
            {
                 Response.Redirect("Default.aspx", false);
           
            }
        }
    }
    private void Save() 
    {
        BLLTCSDirectory obj = new BLLTCSDirectory();
        obj.EmployeeCode = Convert.ToInt32(Session["EmployeeCode"].ToString());
        
        if(txtEmail.Text.Trim()!="")
            obj.Email = txtEmail.Text.Trim();
        if (txtLandline.Text.Trim() != "")
            obj.LandlineNo = txtCell.Text.Trim();
        if (txtCell.Text.Trim() != "")
            obj.MobileNo = txtCell.Text.Trim();
        if (txtExt.Text.Trim() != "")
            obj.ExtensionNo = txtExt.Text.Trim();
        
        int k=obj.TCSDirectoryInsert(obj);

    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }

    private void LoadEmployeeInformation()
    {

        DataTable dt = new DataTable();

        BLLEmployeeBusinessCard objBll = new BLLEmployeeBusinessCard();

        objBll.EmployeeCode = Session["EmployeeCode"].ToString();
        dt = objBll.EmployeeProfileForBCardFetchByCode(objBll);
        if (dt.Rows.Count > 0)
        {
            //txtFName.Text = dt.Rows[0]["FullName"].ToString();
            //txtDesignation.Text = dt.Rows[0]["DesigName"].ToString();
            txtCell.Text = dt.Rows[0]["EmpContactNumber"].ToString();
            txtEmail.Text = dt.Rows[0]["EmpEmail"].ToString();
            
      }
         
    }
}