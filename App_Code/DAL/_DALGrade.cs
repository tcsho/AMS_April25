using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALGrade
/// </summary>
public class _DALGrade
{
    DALBase dalobj = new DALBase();
	public _DALGrade()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GradeSelectBySearchCriteria(BLLGrade objbll)
    {
        
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("GradeSelectBySearchCriteria");
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