using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALDepartment
/// </summary>
public class _DALDepartment
{
    DALBase dalobj = new DALBase();
	public _DALDepartment()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public DataTable DepartmentSelectBySearchCriteria(BLLDepartment objbll)
    {
       
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("DepartmentSelectBySearchCriteria");
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