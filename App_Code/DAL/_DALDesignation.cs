using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALDesignation
/// </summary>
public class DALDesignation
{
    DALBase dalobj = new DALBase();


    public DALDesignation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int DesignationAdd(BLLDesignation objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@DesigName", SqlDbType.NVarChar);
        param[0].Value = objbll.DesigName;
        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("DesignationInsert", param);
        int k = (int)param[1].Value;
        return k;

    }
    public int DesignationUpdate(BLLDesignation objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@DesigCode", SqlDbType.Int);
        param[0].Value = objbll.DesigCode;

        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("DesignationUpdate", param);
        int k = (int)param[1].Value;
        return k;
    }
    public int DesignationDelete(BLLDesignation objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Designation_Id", SqlDbType.Int);
     //   param[0].Value = objbll.Designation_Id;


        int k = dalobj.sqlcmdExecute("DesignationDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable DesignationSelect(int _id)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
    param[0].Value = _id;


    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("DesignationSelectById", param);
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
    
    public DataTable DesignationSelect(BLLDesignation objbll)
    {

    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("DesignationSelectAll");
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

    public DataTable DesignationSelectByStatusID(BLLDesignation objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("DesignationSelectByStatusID");
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


    public DataTable DesignationSelectBySearchCriteria(BLLDesignation objbll)
    {
        
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("DesignationSelectBySearchCriteria");
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
