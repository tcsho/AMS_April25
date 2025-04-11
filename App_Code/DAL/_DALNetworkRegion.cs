using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALNetworkRegion
/// </summary>
public class _DALNetworkRegion
{
    DALBase dalobj = new DALBase();
	public _DALNetworkRegion()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region 'Start of Execution Methods'
    public int NetworkRegionAdd(BLLNetworkRegion objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

     

        param[0] = new SqlParameter("@NetworkName", SqlDbType.NVarChar); 
        param[0].Value = objbll.NetworkName;

        param[1] = new SqlParameter("@Region_Id", SqlDbType.Int); 
        param[1].Value = objbll.Region_Id;


        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("NetworkRegionInsert", param);
        int k = (int)param[2].Value;
        return k;

    }
    public int NetworkRegionUpdate(BLLNetworkRegion objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@NetworkRegion_Id", SqlDbType.Int);
        param[0].Value = objbll.NetworkRegion_Id;

        param[1] = new SqlParameter("@NetworkName", SqlDbType.NVarChar);
        param[1].Value = objbll.NetworkName;

        param[2] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[2].Value = objbll.Region_Id;


        param[3] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[3].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("NetworkRegionUpdate", param);
        int k = (int)param[3].Value;
        return k;
    }
    public int NetworkRegionDelete(BLLNetworkRegion objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@NetworkRegion_Id", SqlDbType.Int);
        param[0].Value = objbll.NetworkRegion_Id;

        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

         dalobj.sqlcmdExecute("NetworkRegionDelete", param);
         int k = (int)param[1].Value;

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'


    public DataTable NetworkRegionSelectRegionByID(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkRegionSelectedRegionByID", param);
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

    public DataTable NetworkRegionSelectByID(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@NetworkRegion_Id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkRegionSelectedByID", param);
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


    public DataTable NetworkRegionSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkRegionSelectedRegionByID", param);
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

    public DataTable NetworkRegionSelect(BLLNetworkRegion objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.Region_Id;

     

        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkRegionSelectAll", param);
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

    public DataTable NetworkRegionSelectByStatusID(BLLNetworkRegion objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkRegionSelectByStatusID");
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

    public DataTable NetworkCenterSelectByRegionID(int _id)
    {
        DataTable _dt = new DataTable();

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@Region_id", SqlDbType.Int);
            param[0].Value = _id;

            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("GetNetworkCenterFromRegion", param);
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }


    #endregion



}