using ADG.JQueryExtenders.Impromptu;
using System;
using System.Data;

public partial class ChPass : System.Web.UI.Page
{
    DALBase objbase = new DALBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeCode"] != null)
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
            if (!IsPostBack)
            {
                try
                {

                    txtUser.Text = Session["UserName"].ToString();

                }
                catch (Exception ex)
                {
                    Session["error"] = ex.Message;
                    Response.Redirect("ErrorPage.aspx", false);
                }

            }
            // }
            //else
            //    {
            //    Session["error"] = "You Are Not Authorized To Access This Page";
            //    Response.Redirect("ErrorPage.aspx", false);
            //    }
        }
        else
        {
            Response.Redirect("~/login.aspx");

        } 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        BLLUser objbll = new BLLUser();
        objbll.User_Name = txtUser.Text;
        objbll.Password = txtNPassC.Text;
        if (String.IsNullOrEmpty(objbll.Password))
        {
            ImpromptuHelper.ShowPrompt("Password cannot be blank.");
            return;
        }

        int AlreadyIn = objbll.UserUpdate(objbll);

        if (AlreadyIn == 0)
        {
            // drawMsgBox("Error to reset Password!",2);
            ImpromptuHelper.ShowPrompt("Error to reset Password!");

        }
        else if (AlreadyIn == 1)
        {
            // drawMsgBox("Password Changed Successfully.", 1);
            ImpromptuHelper.ShowPrompt("Password Changed Successfully."); 
        }
    }

    protected void drawMsgBox(string msg, int errType)
    {
        try
        {
            ImpromptuHelper.ShowPrompt(msg);
        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message;
            Response.Redirect("ErrorPage.aspx", false);
        }
    } 
}
