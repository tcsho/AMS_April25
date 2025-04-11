using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALEmployeeOnManualShiftDetail
/// </summary>
public class DALEmployeeOnManualShiftDetail
{
    DALBase dalobj = new DALBase();


    public DALEmployeeOnManualShiftDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region 'Start of Execution Methods'
    public int EmployeeOnManualShiftDetailAdd(BLLEmployeeOnManualShiftDetail objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@Employeecode", SqlDbType.NVarChar); 
        param[0].Value = (objbll.Employeecode != null) ? objbll.Employeecode : "";
        param[1] = new SqlParameter("@Reason", SqlDbType.NVarChar); 
        param[1].Value = (objbll.Reason != null) ? objbll.Reason : "";
       
        
        param[2] = new SqlParameter("@CreatedBy", SqlDbType.Int); 
        param[2].Value = objbll.CreatedBy;
          
        param[3] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[3].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeOnManualShiftDetailInsert", param);
        int k = (int)param[3].Value;
        return k;

    }
    public int EmployeeOnManualShiftDetailUpdate(BLLEmployeeOnManualShiftDetail objbll)
    {
        SqlParameter[] param = new SqlParameter[6];

        param[0] = new SqlParameter("@Employeecode", SqlDbType.NVarChar); 
        param[0].Value = (objbll.Employeecode != null) ? objbll.Employeecode : "";
        param[1] = new SqlParameter("@Reason", SqlDbType.NVarChar); 
        param[1].Value = (objbll.Reason != null) ? objbll.Reason : "";
        param[2] = new SqlParameter("@Status_Id", SqlDbType.Int); 
        param[2].Value = objbll.Status_Id;
        param[3] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        param[3].Value = objbll.CreatedOn;
        param[4] = new SqlParameter("@CreatedBy", SqlDbType.Int);
        param[4].Value = objbll.CreatedBy;
         
        param[5] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[5].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeOnManualShiftDetailUpdate", param);
        int k = (int)param[5].Value;
        return k;
    }
    public int EmployeeOnManualShiftDetailDelete(BLLEmployeeOnManualShiftDetail objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.Employeecode;


        int k = dalobj.sqlcmdExecute("EmployeeOnManualShiftDetailDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable EmployeeprofileSelectByRegionCenter(long r, long c)
    {
    SqlParameter[] param = new SqlParameter[2];

    param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
    param[0].Value = r;
    param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
    param[1].Value = c;

    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("EmployeeprofileSelectByRegionCenter", param);
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
    
    public DataTable EmployeeOnManualShiftDetailSelect(BLLEmployeeOnManualShiftDetail objbll)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@Evaluation_Criteria_Type_Id", SqlDbType.Int);
  //  param[0].Value = objbll.Evaluation_Criteria_Type_Id;


    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("EmployeeOnManualShiftDetailSelectAll", param);
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

    public DataTable EmployeeOnManualShiftDetailSelectByStatusID(BLLEmployeeOnManualShiftDetail objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeOnManualShiftDetailSelectByStatusID");
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
