using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for _DALNetworkTeam
/// </summary>
public class _DALNetworkTeam
{
    DALBase dalobj = new DALBase();

	public _DALNetworkTeam()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region 'Start of Execution Methods'

    public int NetworkTeamAdd(BLLNetworkTeam objbll)
    {

        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@NetworkRegion_Id", SqlDbType.Int);
        param[0].Value = objbll.NetworkRegion_Id;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@IsHOD", SqlDbType.Bit);
        param[2].Value = objbll.IsHOD;

     

        int k =dalobj.sqlcmdExecute("NetworkTeamInsert", param);
       
        return k;

    }
    //public int NetworkTeamUpdate(BLLNetworkTeam objbll)
    //{
    //    SqlParameter[] param = new SqlParameter[10];


    //    param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
    //    param[9].Direction = ParameterDirection.Output;

    //    dalobj.sqlcmdExecute("NetworkTeamUpdate", param);
    //    int k = (int)param[9].Value;
    //    return k;
    //}
    public int NetworkTeamDelete(BLLNetworkTeam objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@NetworkTeam_Id", SqlDbType.Int);
        param[0].Value = objbll.NetworkTeam_Id;


        int k = dalobj.sqlcmdExecute("NetworkTeamDelete", param);

        return k;
    }
    public int NetworkEmployeesinSchool_Insert(BLLNetworkTeam objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.Int);
        param[0].Value = Convert.ToInt32(objbll.EmployeeCode);
        param[1] = new SqlParameter("@inSchool", SqlDbType.Int);
        param[1].Value = objbll.InSchool;

        int k = dalobj.sqlcmdExecute("NetworkEmployeesinSchool_Insert", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable NetworkTeamSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@NetworkRegion_Id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkTeamSelectById", param);
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

    public DataTable NetworkTeamSelect(BLLNetworkTeam objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@Evaluation_Criteria_Type_Id", SqlDbType.Int);
        //  param[0].Value = objbll.Evaluation_Criteria_Type_Id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkTeamSelectAll", param);
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
    public DataTable NetworkTeamSelectByLocation(BLLNetworkTeam objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.NetworkRegion_Id;

        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkEmployeesinSchoolSelectAll",param);
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

    public DataTable NetworkTeamSelectByStatusID(BLLNetworkTeam objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("NetworkTeamSelectByStatusID");
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

    public DataTable NetworkTeamByRegion(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[3];


        param[0] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[0].Value = objbll.Region_id;

        param[1] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[1].Value = objbll.Center_id;

        param[2] = new SqlParameter("@deptCode", SqlDbType.Int);
        param[2].Value = objbll.DeptCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("NetworkTeamByRegion", param);
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




    #endregion

}