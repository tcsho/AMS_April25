using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ADG.JQueryExtenders.Impromptu;
public partial class Login : System.Web.UI.Page
{
    DALBase objbase = new DALBase();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnlogin_Click1(object sender, EventArgs e)
    {
        BLLUser objbll = new BLLUser();
        DataTable dtUsers = new DataTable();

        objbll.User_Name = text_login.Text;
        objbll.Password = text_password.Text;

        try
        {
            dtUsers = objbll.UserFetch(objbll);
        }
        catch (ApplicationException ex)
        {
            Response.Redirect("~/ServerError.aspx");
        }
         
        if (dtUsers != null && dtUsers.Rows.Count > 0)
        {
            {
                Session["rightsRow"] = (DataRow)dtUsers.Rows[0];
                Session["UserName"] = dtUsers.Rows[0]["User_Name"].ToString();
                Session["CenterID"] = dtUsers.Rows[0]["Center_Id"].ToString();
                Session["RegionID"] = dtUsers.Rows[0]["Region_Id"].ToString();
                Session["RegionName"] = dtUsers.Rows[0]["RegName"].ToString();
                Session["CenterName"] = dtUsers.Rows[0]["CenName"].ToString();
                Session["EmployeeCode"] = dtUsers.Rows[0]["employeecode"].ToString();
                Session["UserType"] = dtUsers.Rows[0]["User_Type_Id"].ToString();
                Session["DepartID"] = dtUsers.Rows[0]["Dept_Id"].ToString();
                Session["LoginFrom"] = dtUsers.Rows[0]["LoginFrom"].ToString();
                Session["First_Name"] = dtUsers.Rows[0]["First_Name"].ToString();
                Session["Middle_Name"] = dtUsers.Rows[0]["Middle_Name"].ToString();
                Session["UserLevelID"] = dtUsers.Rows[0]["UserLevel_ID"].ToString();
                Session["CurrentMonth"] = objbase.GetCurrentMonth();
                Session["isExe"] = dtUsers.Rows[0]["isExe"].ToString();
                Session["probrationDate"] = dtUsers.Rows[0]["probrationDate"].ToString();
                Session["User_Id"] = dtUsers.Rows[0]["User_Id"].ToString();
                Session["Gender"] = dtUsers.Rows[0]["Gender"].ToString();
                Session["DOB"] = dtUsers.Rows[0]["DOB"].ToString();
                Session["Designation"] = dtUsers.Rows[0]["Designation"].ToString();
                Session["Password_Expiry_Date"] = dtUsers.Rows[0]["Password_Expiry_Date"].ToString();


                BLLEmplyeeReportTo bllemp = new BLLEmplyeeReportTo();
                bllemp.EmployeeCode = Session["EmployeeCode"].ToString();
                Session["EmailTable"] = bllemp.EmplyeeReportToFetchEmail(bllemp);

                DataTable dt_policy = bllemp.EmployeeTimmingPolicyIdFetch(Session["EmployeeCode"].ToString());

                if (dt_policy.Rows.Count > 0)
                {
                    Session["EmployeePolicyId"] = dt_policy.Rows[0]["Timing_Policy_id"];
                }
                else
                {
                    Session["EmployeePolicyId"] = 1;
                }


                if (dtUsers.Rows[0]["User_Type_Id"].ToString() == "17" || dtUsers.Rows[0]["User_Type_Id"].ToString() == "18" || dtUsers.Rows[0]["User_Type_Id"].ToString() == "20" || dtUsers.Rows[0]["User_Type_Id"].ToString() == "21" || dtUsers.Rows[0]["User_Type_Id"].ToString() == "23" || dtUsers.Rows[0]["User_Type_Id"].ToString() == "24")
                {
                    if (IsEmpInfoCollected())
                        Response.Redirect("EmployeeLeavesSubmissions.aspx", false);
                    else
                        Response.Redirect("EmpInfoCollection.aspx", false);

                }
                else
                {
                    if (IsEmpInfoCollected())
                        Response.Redirect("Default.aspx", false);
                    else
                        Response.Redirect("EmpInfoCollection.aspx", false);
                }
            }
        }
        else
        {
            if (!IsUserExists(text_login.Text))
            {
                drawMsgBox("Your Login has not been activated, please contact HR department.");
                lab_status.Text = "Your Login has not been activated, please contact HR department.";
            }
            else
            {
                drawMsgBox("Wrong credentials, Contact The City School's Administration!");
                lab_status.Text = "Wrong credentials, Contact The City School's Administration";
            }
        }

    }
    protected bool IsUserExists(string userName)
    {
        bool flag = false;
        DataTable dt = new DataTable();
        BLLUser objBll = new BLLUser();
        objBll.User_Name =userName;
        dt = objBll.UserFetchByUserName(objBll);
        if (dt != null && dt.Rows.Count > 0)
            flag = true;
        else
            flag = false;
        return flag;
    }

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

    protected bool IsEmpInfoCollected()
    {
        bool flag = false;

        if (Session["UserType"].ToString() == "25" || Session["UserType"].ToString() == "19" || Session["UserType"].ToString() == "22" || Session["UserType"].ToString() == "26")
        {
            flag = true;         // currently no need to get TCS employees info  so bypass it
        }
        else
        {

            BLLTCSDirectory objbll = new BLLTCSDirectory();
            objbll.EmployeeCode = Convert.ToInt32(Session["EmployeeCode"].ToString());
            flag = objbll.TCSDirectoryFetchByEmployeeCode(objbll);

            //string region_Id = Session["RegionID"].ToString().Trim();
            //switch (region_Id)
            //{
            //    case "":
            //    case "2000000":
            //    case "3000000":
            //    case "4000000":
            //    case "5000000":
            //    case "6000000":
            //    case "8000000":
            //        {
            //            flag = true;         // currently no need to get TCS employees info  so bypass it
            //            break;
            //        }
            //    default:
            //        {
            //            BLLTCSDirectory objbll = new BLLTCSDirectory();
            //            objbll.EmployeeCode = Convert.ToInt32(Session["EmployeeCode"].ToString());
            //            flag = objbll.TCSDirectoryFetchByEmployeeCode(objbll);
            //            break;
            //        }
            //}
        }
        
        return flag;
    }

}