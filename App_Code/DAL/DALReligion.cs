using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DALReligion
/// </summary>
public class DALReligion
{
    DALBase dalobj = new DALBase();
	public DALReligion()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable ReligionSelectBySearchCriteria(BLLReligion objbll)
    {
       
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("ReligionSelectBySearchCriteria");
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