using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for _DALTCSDirectory
/// </summary>
public class _DALTCSDirectory
{
    DALBase dalobj = new DALBase();
	public _DALTCSDirectory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int TCSDirectoryInsert(BLLTCSDirectory objbll)
    {
        SqlParameter[] param = new SqlParameter[6];


        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.Int); param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@Email", SqlDbType.NVarChar); param[1].Value = objbll.Email;
        param[2] = new SqlParameter("@MobileNo", SqlDbType.NVarChar); param[2].Value = objbll.MobileNo;
        
        param[3] = new SqlParameter("@ExtensionNo", SqlDbType.NVarChar); param[3].Value = objbll.ExtensionNo;
        param[4] = new SqlParameter("@AlreadyIn", SqlDbType.Int);   param[4].Direction = ParameterDirection.Output;
        param[5] = new SqlParameter("@LandlineNo", SqlDbType.NVarChar); param[5].Value = objbll.ExtensionNo;


        dalobj.sqlcmdExecute("TCSDirectorysAddUpdate", param);
        int k = (int)param[4].Value;
        return k;

    }

    public bool TCSDirectorySelectByEmployeeCode(BLLTCSDirectory obj)
    {
        SqlParameter[] param = new SqlParameter[1];
        bool flag = false;
        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.VarChar);
        param[0].Value = obj.EmployeeCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("TCSDirectorySelectByEmployeeCode", param);
            if (_dt.Rows.Count > 0)
                flag = true;
            else
                flag = false;

            return flag;
        }
        catch (Exception _exception)
        {
            throw _exception;
        }
        finally
        {
            dalobj.CloseConnection();
        }



    }
}