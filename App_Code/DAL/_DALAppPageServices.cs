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
using System.Collections.Generic;

/// <summary>
/// Summary description for _DALAppPageServices
/// </summary>
public class _DALAppPageServices
{
    DALBase dalobj = new DALBase();


    public _DALAppPageServices()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int AppPageServicesAdd(BLLAppPageServices objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("AppPageServicesInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int AppPageServicesUpdate(BLLAppPageServices objbll)
    {
        SqlParameter[] param = new SqlParameter[10];

 
        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("AppPageServicesUpdate", param);
        int k = (int)param[9].Value;
        return k;
    }
    public int AppPageServicesDelete(BLLAppPageServices objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@AppPageServices_Id", SqlDbType.Int);
     //   param[0].Value = objbll.AppPageServices_Id;


        int k = dalobj.sqlcmdExecute("AppPageServicesDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable AppPageServicesSelect(int _id)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
    param[0].Value = _id;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("AppPageServicesSelect", param);
        return _dt;
        }
    catch (Exception _exception)
        {
        throw _exception;
        }
    finally
        {
        dalobj.CloseConnection();
        }

    return _dt;
    }
    
    public DataTable AppPageServicesSelect(BLLAppPageServices objbll)
    {
    SqlParameter[] param = new SqlParameter[2];

    param[0] = new SqlParameter("@PageName", SqlDbType.NVarChar);
    param[0].Value = objbll.PageName;

    param[1] = new SqlParameter("@UserType_id", SqlDbType.Int);
    param[1].Value = objbll.User_type_Id;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("WebAppObjectServicesGetPriviliges", param);
        return _dt;
        }
    catch (Exception _exception)
        {
        throw _exception;
        }
    finally
        {
        dalobj.CloseConnection();
        }

    return _dt;
    
    }


    public int AppPageServicesSelectField(int _Id)
        {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Section_Id", SqlDbType.Int);
        param[0].Value = _Id;

        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("", param);
        int k = (int)param[1].Value;
        return k;

        }


    #endregion


}
