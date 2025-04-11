using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for _DALEmployeeProfile
/// </summary>
public class _DALEmployeeProfile
{
    DALBase dalobj = new DALBase();
	public _DALEmployeeProfile()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable EmployeeProfileSelectByCode(BLLEmployeeProfile obj)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = obj.EmployeeCode;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeProfileSelectByCode", param);
            return dt;
        }
        catch (Exception _exception)
        {
            throw _exception;
        }
        finally
        {
            dalobj.CloseConnection();
        }

        return dt;
    }
}