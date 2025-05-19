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
/// Summary description for DALBaseAims
/// </summary>
public class DALBaseAims
{
    protected static string strConnect;

    public SqlConnection _cn = new SqlConnection();
    public DALBaseAims()
    {
        //
        // TODO: Add constructor logic here
        //
        _cn = GetConnection();
    }
    static DALBaseAims()
    {
        strConnect = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["tcs_AimsConnectionString"]);
    }

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