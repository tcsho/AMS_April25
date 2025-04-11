using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
//using Microsoft.ApplicationBlocks.Data;
using System.Collections;

/// <summary>
/// Summary description for DALBaseCommunication
/// </summary>
public class DALBaseCommunication
{
     protected static string strConnect;

    public SqlConnection _cn = new SqlConnection();

	public DALBaseCommunication()
	{
		//
		// TODO: Add constructor logic here
		//
        _cn = GetConnection();
	}
    static DALBaseCommunication()
    {
        strConnect = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["tcs_DCConnectionString"]);
    }

    /// <summary>
    /// Gets a SqlConnection to the local sqlserver
    /// </summary>
    /// <returns>SqlConnection</returns> 

    protected SqlConnection GetConnection()
    {
        SqlConnection oConnection = new SqlConnection(strConnect);
        return oConnection;
    }



    //=================================== Inventory Code =========================

    public void OpenConnection()
    {
        //   SqlConnection oConnection = GetConnection();
        try
        {
            if (_cn.State != ConnectionState.Open)
            {
                _cn.Open();
            }
            else
            {

            }
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    public void CloseConnection()
    {

        try
        {
            if (_cn.State != ConnectionState.Closed)
            {
                _cn.Close();
            }
        }
        catch
        {

            throw;
        }
    }

    public int sqlcmdExecute(string procedurename)
    {
        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.CommandText = procedurename;
        sqlcmd.Connection = _cn;
        sqlcmd.CommandTimeout = 1800;

        try
        {
            OpenConnection();
            return sqlcmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            CloseConnection();
        }
    }

    public int sqlcmdExecute(string procedurename, SqlParameter[] param)
    {
        //SqlConnection oConnection = GetConnection();

        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.CommandText = procedurename;
        sqlcmd.Connection = _cn;
        sqlcmd.CommandTimeout = 1800;
        for (int i = 0; i < param.Length; i++)
        {
            sqlcmd.Parameters.Add(param[i]);
        }
        try
        {
            OpenConnection();
            return sqlcmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            CloseConnection();
        }
    }

    public DataTable sqlcmdFetch(string procedurename, SqlParameter[] param)
    {
        //SqlConnection oConnection = GetConnection();

        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.CommandText = procedurename;
        sqlcmd.Connection = _cn;

        SqlDataAdapter myAdapter = new SqlDataAdapter();

        DataTable dt = new DataTable();

        for (int i = 0; i < param.Length; i++)
        {
            sqlcmd.Parameters.Add(param[i]);
        }

        try
        {
            OpenConnection();

            myAdapter.SelectCommand = sqlcmd;
            myAdapter.Fill(dt);
        }

        catch (Exception ex)
        {
            string output;
            output = ex.Message.ToString();
        }
        CloseConnection();
        return dt;
    }

    public DataTable sqlcmdFetch(string procedurename)
    {
        SqlConnection oConnection = GetConnection();

        SqlCommand sqlcmd = new SqlCommand();
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.CommandText = procedurename;
        sqlcmd.Connection = oConnection;
        SqlDataAdapter myAdapter = new SqlDataAdapter();

        DataTable dt = new DataTable();
        try
        {
            OpenConnection();

            myAdapter.SelectCommand = sqlcmd;
            myAdapter.Fill(dt);
        }

        catch (Exception ex)
        {
            string output;
            output = ex.Message.ToString();
        }
        CloseConnection();
        return dt;
    }

    //public void FillControlLabel( _gb, DataTable _fdt)
    //{
    //    int _index = 1;

    //    foreach (Control ctr in _gb.Controls)
    //    {
    //        if (ctr is Label)
    //        {
    //            if (ctr.Name.Substring(0, 5) == "Field")
    //            {
    //                int _len = (ctr.Name.Length - 5);
    //                _index = Convert.ToInt32(ctr.Name.Substring(5, _len)) - 1;

    //                if (ctr.Name == _fdt.Rows[_index]["Field"].ToString())
    //                {

    //                    ctr.Text = _fdt.Rows[_index]["FieldDes"].ToString();
    //                }
    //            }

    //        }
    //        //else if (ctr is TextBox || ctr is ComboBox || ctr is MaskedTextBox)
    //        //{
    //        //    if (ctr.Name.Substring(0, 3) == "txt" || ctr.Name.Substring(0, 3) == "cmb")
    //        //    {
    //        //        for (int i = 0; i < _fdt.Rows.Count; i++)
    //        //        {
    //        //            if (ctr.Name == Convert.ToString(_fdt.Rows[i]["ControlField"]) && Convert.ToBoolean(_fdt.Rows[i]["isMendatory"]) == true)
    //        //            {
    //        //                //ctr.BackColor = Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(224)))), ((int)(((byte)(250)))));
    //        //                ctr.BackColor = Color.FromArgb(((int)(((byte)(GetColor("Red"))))), ((int)(((byte)(GetColor("Green"))))), ((int)(((byte)(GetColor("Blue"))))));
    //        //            }
    //        //        }

    //        //    }
    //        //}
    //    }
    //}




    //=============================================================================

    public DataTable ApplyPageAccessSettingsTable(string strPageName, int userTypeID)
    {
        {
            DataTable _dt = new DataTable();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@User_type_ID", SqlDbType.Int); param[0].Value = userTypeID;
            param[1] = new SqlParameter("@PageName", SqlDbType.NVarChar); param[1].Value = strPageName;

            try
            {
                OpenConnection();
                _dt = sqlcmdFetch("LmsAppMenuServicesSelectByPageAndUserType", param);
            }
            catch (Exception oException)
            {
                throw oException;
            }
            finally
            {
                CloseConnection();
            }

            return _dt;
        }


        //==============================================================================================
    }
    //public void ApplicationSettings(int _catId, string _CntntPlcHldr, HtmlForm Form,DataTable _dt)
    //{

    //    BLLINV_FieldMap bllAppObj = new BLLINV_FieldMap();

    //    //int _part_Id = Int32.Parse(_cntctID);
    //    //int _PartRP = 0;

    //    //DataTable dtpart = bllWrkPrt.LmsWrkPartTypeSelect(_part_Id);
    //    //if (dtpart.Rows.Count > 0)
    //    //{
    //    //    _PartRP = Int32.Parse(dtpart.Rows[0]["WrkTp_PartTp_ID"].ToString());
    //    //}
    //    bllAppObj.ItemCat_ID = _catId;
    //    DataTable dt = bllAppObj.INV_FiledMapSelectByCatId(bllAppObj);
    //    if (dt.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            //if (dr["Object"].ToString() == "LinkButton")
    //            //{
    //                string _ctrl = dr["FieldMap"].ToString();
    //                ContentPlaceHolder MainContent = Form.FindControl(_CntntPlcHldr) as ContentPlaceHolder;
    //                Label lbl = (Label)MainContent.FindControl(_ctrl);

    //                lbl.Text = dr["FieldDes"].ToString();
    //                lbl.Visible = true;
    //            //}
    //            //else if (dr["Object"].ToString() == "Button")
    //            //{
    //            //    string _ctrl = dr["ObjectName"].ToString();
    //            //    ContentPlaceHolder MainContent = Form.FindControl(_CntntPlcHldr) as ContentPlaceHolder;
    //            //    Button lnk = (Button)MainContent.FindControl(_ctrl);
    //            //    lnk.Text = dr["Description"].ToString();
    //            //    lnk.Visible = Convert.ToBoolean(dr["isAllow"].ToString());
    //            //}
    //            //else if (dr["Object"].ToString() == "GridView")
    //            //{
    //            //    string _ctrl = dr["ObjectName"].ToString();
    //            //    ContentPlaceHolder MainContent = Form.FindControl(_CntntPlcHldr) as ContentPlaceHolder;
    //            //    GridView lnk = (GridView)MainContent.FindControl(_ctrl);
    //            //    // lnk.Text = dr["Description"].ToString();
    //            //    lnk.Visible = Convert.ToBoolean(dr["isAllow"].ToString());
    //            //}
    //            //else if (dr["Object"].ToString() == "ImageButton")
    //            //{
    //            //    string _ctrl = dr["ObjectName"].ToString();
    //            //    ContentPlaceHolder MainContent = Form.FindControl(_CntntPlcHldr) as ContentPlaceHolder;
    //            //    ImageButton lnk = (ImageButton)MainContent.FindControl(_ctrl);
    //            //    lnk.ToolTip = dr["Description"].ToString();
    //            //    lnk.Visible = Convert.ToBoolean(dr["isAllow"].ToString());
    //            //}
    //            //else if (dr["Object"].ToString() == "HTMLImage")
    //            //{
    //            //    string _ctrl = dr["ObjectName"].ToString();
    //            //    ContentPlaceHolder MainContent = Form.FindControl(_CntntPlcHldr) as ContentPlaceHolder;
    //            //    HtmlImage lnk = (HtmlImage)MainContent.FindControl(_ctrl);
    //            //    lnk.Alt = dr["Description"].ToString();
    //            //    lnk.Visible = Convert.ToBoolean(dr["isAllow"].ToString());
    //            //}
    //        }
    //    }
    //}

    ///*public void ApplicationSettings(int _catId, HtmlForm Form)
    //{

    //    Reset(Form);

    //    BLLINV_FieldMap bllAppObj = new BLLINV_FieldMap();

    //    bllAppObj.ItemCat_ID = _catId;
    // DataTable dt = bllAppObj.INV_FiledMapSelectByCatId(bllAppObj);
    // if (dt.Rows.Count > 0)
    // {
    //     foreach (DataRow dr in dt.Rows)
    //     {
    //         string _ctrl = dr["FieldMap"].ToString();
    //         Label lbl = (Label)Form.FindControl(_ctrl);
    //         lbl.Text = dr["FieldDes"].ToString();
    //         lbl.Visible = true;
    //         string _ctrltxt;
    //                _ctrltxt = "txt"+_ctrl.Substring(5);

    //         TextBox txt = (TextBox)Form.FindControl(_ctrltxt);
    //         txt.Text = "";
    //         txt.Visible = true;
    //     }
    // }

    //}
    //private void Reset(HtmlForm _form)
    //{
    //    for (int i = 1; i <= 15; i++)
    //    {
    //        string _ctrl = "Field" + i.ToString();
    //        Label lbl = (Label)_form.FindControl(_ctrl);
    //        lbl.Text = "";
    //        lbl.Visible = false;

    //        _ctrl = "txt" + i.ToString();
    //        TextBox txt = (TextBox)_form.FindControl(_ctrl);
    //        txt.Text = "";
    //        txt.Visible = false;


    //    }

    //}*/

    //public void ApplicationSettings(int _catId, HtmlForm Form)
    //{

    //    Reset(Form);

    //    BLLINV_FieldMap bllAppObj = new BLLINV_FieldMap();

    //    bllAppObj.ItemCat_ID = _catId;
    //    DataTable dt = bllAppObj.INV_FiledMapSelectByCatId(bllAppObj);
    //    if (dt.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            string _ctrl = dr["FieldMap"].ToString();
    //            Label lbl = (Label)Form.FindControl("ContentPlaceHolder1").FindControl(_ctrl);
    //            lbl.Text = dr["FieldDes"].ToString();
    //            lbl.Visible = true;
    //            string _ctrltxt;
    //            _ctrltxt = "txt" + _ctrl.Substring(5);

    //            TextBox txt = (TextBox)Form.FindControl("ContentPlaceHolder1").FindControl(_ctrltxt);
    //            txt.Text = "";
    //            txt.Visible = true;
    //        }
    //    }

    //}

    private void Reset(HtmlForm _form)
    {
        for (int i = 1; i <= 15; i++)
        {
            string _ctrl = "Field" + i.ToString();
            Label lbl = (Label)_form.FindControl("ContentPlaceHolder1").FindControl(_ctrl);
            lbl.Text = "";
            lbl.Visible = false;

            _ctrl = "txt" + i.ToString();
            TextBox txt = (TextBox)_form.FindControl("ContentPlaceHolder1").FindControl(_ctrl);
            txt.Text = "";
            txt.Visible = false;


        }

    }

    public void FillCombo(DataTable _dt, DropDownList _ddl, string _strcode, string _strdesc)
    {
        _ddl.Items.Clear();
        if (_dt != null && _dt.Rows.Count > 0)
        {

            _ddl.DataSource = _dt;
            _ddl.DataValueField = _dt.Columns[_strcode].ToString();
            _ddl.DataTextField = _dt.Columns[_strdesc].ToString();
            _ddl.DataBind();

        }
    }

    public string GetNextDocNumber(int transferTypeID)
    {
        SqlParameter[] param = new SqlParameter[2];


        param[0] = new SqlParameter("@TransferType_ID", SqlDbType.Int);
        param[0].Value = transferTypeID;


        param[1] = new SqlParameter("@DocumentNo", SqlDbType.NVarChar, 50);
        param[1].Direction = ParameterDirection.Output;

        int k = sqlcmdExecute("GetNextDocNumber", param);
        string strDocNumb = param[1].Value.ToString();
        return strDocNumb;
    }

    public string GetCurrentMonth()
    {

        string _str = "";
        BLLPeriod obj = new BLLPeriod();

        DataTable dt = new DataTable();
        dt = obj.PeriodFetchCurrentMonth();
        if (dt.Rows.Count > 0)
        {
            _str = dt.Rows[0]["PMonth"].ToString();
        }

        return _str;
    }

    public int IsAnual(string _LvGroup)
    {

        int _val = 0;
        BLLAttendance obj = new BLLAttendance();

        DataTable dt = new DataTable();
        obj.LeaveGroup = _LvGroup;
        dt = obj.AttendanceFetchIsAnual(obj);
        if (dt.Rows.Count > 0)
        {
            _val = Convert.ToInt32(dt.Rows[0]["counter"].ToString());
        }

        return _val;
    }

    public int ApplicationSettings(string _pageName, int _part_Id)
    {

        int _PartRP = 0;

        BLLAppPageServices bllAppObj = new BLLAppPageServices();
        bllAppObj.User_type_Id = _part_Id;
        bllAppObj.PageName = _pageName;

        DataTable dt = bllAppObj.AppPageServicesFetch(bllAppObj);
        if (dt.Rows.Count > 0)
        {
            bool lnk = Convert.ToBoolean(dt.Rows[0]["isAllow"].ToString());

            if (lnk == true)
            {
                _PartRP = 1;
            }
            else
            {
                _PartRP = 0;
            }
        }
        return _PartRP;
    }

    public void FillDropDown(DataTable _dt, DropDownList _ddl, string _strcode, string _strdesc)
    {
        if (_dt != null && _dt.Rows.Count > 0)
        {

            _ddl.DataSource = _dt;
            _ddl.DataValueField = _dt.Columns[_strcode].ToString();
            _ddl.DataTextField = _dt.Columns[_strdesc].ToString();
            _ddl.DataBind();


        }
        else
        {
            _ddl.Items.Clear();
        }
        if (_ddl.Items.FindByValue("0") == null)
            _ddl.Items.Insert(0, new ListItem("Select", "0"));
    }



}