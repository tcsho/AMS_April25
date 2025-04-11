using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALNetworkCenter
/// </summary>
public class _DALNetworkCenter
{
    DALBase dalobj = new DALBase();

	public _DALNetworkCenter()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region 'Start of Execution Methods'
    public int NetworkCenterAdd(BLLNetworkCenter objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

       
        param[0] = new SqlParameter("@Center_Id", SqlDbType.Int); 
        param[0].Value = objbll.Center_Id;

        param[1] = new SqlParameter("@NetworkRegion_Id", SqlDbType.Int); 
        param[1].Value = objbll.NetworkRegion_Id;


        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("NetworkCenterInsert", param);
        int k = (int)param[2].Value;
        return k;

    }
    public int NetworkCenterUpdate(BLLNetworkCenter objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@NetworkCenterId", SqlDbType.Int);
        param[0].Value = objbll.NetworkCenter_Id;

        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = objbll.Center_Id;

        param[2] = new SqlParameter("@NetworkRegion_Id", SqlDbType.Int);
        param[2].Value = objbll.NetworkRegion_Id;


        param[3] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[3].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("NetworkCenterUpdate", param);
        int k = (int)param[3].Value;
        return k;
    }
    public int NetworkCenterDelete(BLLNetworkCenter objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@NetworkCenterId", SqlDbType.Int);
        param[0].Value = objbll.NetworkCenter_Id;


        int k = dalobj.sqlcmdExecute("NetworkCenterDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable NetworkCenterSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@NetworkRegion_Id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkCenterSelectedByID", param);
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

    public DataTable NetworkCenterSelect(BLLNetworkCenter objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@Evaluation_Criteria_Type_Id", SqlDbType.Int);
        //  param[0].Value = objbll.Evaluation_Criteria_Type_Id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkCenterSelectAll", param);
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

    public DataTable NetworkCenterSelectByStatusID(BLLNetworkCenter objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkCenterSelectByStatusID");
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



    public DataTable NetworkCenterSelectByNetworkHOD(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.Int);
        param[0].Value = _id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkCenterSelectByNetworkHOD", param);
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
   

    #endregion
}