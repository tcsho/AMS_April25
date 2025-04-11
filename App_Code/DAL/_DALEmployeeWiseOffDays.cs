using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALEmployeeWiseOffDays
/// </summary>
public class DALEmployeeWiseOffDays
{
    DALBase dalobj = new DALBase();


    public DALEmployeeWiseOffDays()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int EmployeeWiseOffDaysAdd(BLLEmployeeWiseOffDays objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[0] = new SqlParameter("@Employeecode", SqlDbType.NVarChar); 
        param[0].Value = objbll.Employeecode;
        
        param[1] = new SqlParameter("@FromDate", SqlDbType.DateTime); 
        param[1].Value = objbll.FromDate;
        
        param[2] = new SqlParameter("@ToDate", SqlDbType.DateTime); 
        param[2].Value = objbll.ToDate;
        
        param[3] = new SqlParameter("@Reason", SqlDbType.NVarChar); 
        param[3].Value = objbll.Reason;
        
        param[4] = new SqlParameter("@SunOff", SqlDbType.Bit); 
        param[4].Value = objbll.SunOff;
        
        param[5] = new SqlParameter("@MonOff", SqlDbType.Bit); 
        param[5].Value = objbll.MonOff;
        
        param[6] = new SqlParameter("@TueOff", SqlDbType.Bit); 
        param[6].Value = objbll.TueOff;
        
        param[7] = new SqlParameter("@WedOff", SqlDbType.Bit); 
        param[7].Value = objbll.WedOff;
        
        param[8] = new SqlParameter("@ThuOff", SqlDbType.Bit); 
        param[8].Value = objbll.ThuOff;
        
        param[9] = new SqlParameter("@FriOff", SqlDbType.Bit); 
        param[9].Value = objbll.FriOff;
        
        param[10] = new SqlParameter("@SatOff", SqlDbType.Bit); 
        param[10].Value = objbll.SatOff;
        
        param[11] = new SqlParameter("@Status_Id", SqlDbType.Int); 
        param[11].Value = objbll.Status_Id;
        
        param[12] = new SqlParameter("@CreatedBy", SqlDbType.Int); 
        param[12].Value = objbll.CreatedBy;
        
        param[13] = new SqlParameter("@CreatedDate", SqlDbType.DateTime); 
        param[13].Value = objbll.CreatedDate;
        
        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeWiseOffDaysInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int EmployeeWiseOffDaysUpdate(BLLEmployeeWiseOffDays objbll)
    {
        SqlParameter[] param = new SqlParameter[10];

 
        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeWiseOffDaysUpdate", param);
        int k = (int)param[9].Value;
        return k;
    }
    public int EmployeeWiseOffDaysDelete(BLLEmployeeWiseOffDays objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EWO_Id", SqlDbType.Int);
        param[0].Value = objbll.EWO_Id;


        int k = dalobj.sqlcmdExecute("EmployeeWiseOffDaysDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable EmployeeWiseOffDaysSelect(int _id)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
    param[0].Value = _id;


    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("EmployeeWiseOffDaysSelectById", param);
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
    
    public DataTable EmployeeWiseOffDaysSelect(BLLEmployeeWiseOffDays objbll)
    {
    SqlParameter[] param = new SqlParameter[2];

    param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
    param[0].Value = objbll.Region_Id;

    param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
    param[1].Value = objbll.Center_Id;

    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("EmployeeWiseOffDaysSelectAll", param);
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

    public DataTable EmployeeWiseOffDaysSelectByStatusID(BLLEmployeeWiseOffDays objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeWiseOffDaysSelectByStatusID");
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
