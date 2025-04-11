using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALEmployeeShifts_SpecialCases_WorkingDay
/// </summary>
public class DALEmployeeShifts_SpecialCases_WorkingDay
{
    DALBase dalobj = new DALBase();


    public DALEmployeeShifts_SpecialCases_WorkingDay()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int EmployeeShifts_SpecialCases_WorkingDayAdd(BLLEmployeeShifts_SpecialCases_WorkingDay objbll)
    {
        SqlParameter[] param = new SqlParameter[8];
        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.Region_Id;
        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = objbll.Center_Id;
        param[2] = new SqlParameter("@WorkingDate", SqlDbType.DateTime); 
        param[2].Value = objbll.WorkingDate;
        param[3] = new SqlParameter("@CreatedBy", SqlDbType.Int); 
        param[3].Value = objbll.CreatedBy;
        param[4] = new SqlParameter("@Gender", SqlDbType.NVarChar);
        param[4].Value = objbll.Gender;
        param[5] = new SqlParameter("@remarks", SqlDbType.NVarChar);
        param[5].Value = objbll.Remarks;
        param[6] = new SqlParameter("@isApplyCenter", SqlDbType.Bit);
        param[6].Value = objbll.isApplyCenters  ;
        param[7] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[7].Direction = ParameterDirection.Output;
        dalobj.sqlcmdExecute("EmployeeShifts_SpecialCases_WorkingDay_Insert", param);
        int k = (int)param[7].Value;
        return k;

    }
    public int EmployeeShifts_SpecialCases_WorkingDayUpdate(BLLEmployeeShifts_SpecialCases_WorkingDay objbll)
    {
        SqlParameter[] param = new SqlParameter[5];

        param[0] = new SqlParameter("@WorkingDay_Id", SqlDbType.Int); 
        param[0].Value = objbll.WorkingDay_Id;
        param[1] = new SqlParameter("@WorkingDate", SqlDbType.DateTime); 
        param[1].Value = objbll.WorkingDate;
        param[2] = new SqlParameter("@Gender", SqlDbType.NVarChar);
        param[2].Value = objbll.Gender;
        param[3] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
        param[3].Value = objbll.Remarks;
        param[4] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[4].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeShifts_SpecialCases_WorkingDayUpdate", param);
        int k = (int)param[4].Value;
        return k;
    }
    public int EmployeeShifts_SpecialCases_WorkingDayDelete(BLLEmployeeShifts_SpecialCases_WorkingDay objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@WorkingDay_Id", SqlDbType.Int);
        param[0].Value = objbll.WorkingDay_Id;
        param[1] = new SqlParameter("@WorkingDate", SqlDbType.DateTime); 
        param[1].Value = objbll.WorkingDate;
        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;
        
        dalobj.sqlcmdExecute("EmployeeShifts_SpecialCases_WorkingDayDelete", param);
        int k = (int)param[2].Value;
        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable EmployeeShifts_SpecialCases_WorkingDaySelect(int _id)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
    param[0].Value = _id;


    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("EmployeeShifts_SpecialCases_WorkingDaySelectById", param);
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

    
    }

    public DataTable EmployeeShifts_SpecialCases_WorkingDaySelectAll(BLLEmployeeShifts_SpecialCases_WorkingDay objbll)
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
            dt = dalobj.sqlcmdFetch("EmployeeShifts_SpecialCases_WorkingDaySelectAll", param);
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

         
    
    }

    #endregion


}
