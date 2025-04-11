using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALResetLeavesEmployeewise
/// </summary>
public class _DALEmployeeLeavesAdjustment
{
	public _DALEmployeeLeavesAdjustment()
	{
		//
		// TODO: Add constructor logic here
		//
	}




    DALBase dalobj = new DALBase();

    public DataTable FetchLeavesEmployeewise(BLLEmployeeLeavesAdjustment objBll)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@Employeecode", SqlDbType.NVarChar); 
            param[0].Value = objBll.EmployeeCode;

            param[1] = new SqlParameter("@PMonth", SqlDbType.NVarChar); 
            param[1].Value = objBll.PMonth;

            param[2] = new SqlParameter("@DepartmentId", SqlDbType.Int);
            param[2].Value = objBll.DepartmentId;

            param[3] = new SqlParameter("@Center_Id", SqlDbType.Int);
            param[3].Value = objBll.Center_Id;

            param[4] = new SqlParameter("@Region_Id", SqlDbType.Int);
            param[4].Value = objBll.Region_Id;

            //param[4] = new SqlParameter("@DepartmentId", SqlDbType.Int); 
            //param[4].Direction = ParameterDirection.Output;

            DataTable _dt = new DataTable();


            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("LeaveResetFetchForAdjustment", param);
            //_dt = dalobj.sqlcmdFetch("LeaveResetLogFetch", param);
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



    public int ResetEmployeeLeaves(BLLEmployeeLeavesAdjustment objBll)
    {
        SqlParameter[] param = new SqlParameter[6];

        param[0] = new SqlParameter("@Employeecode", SqlDbType.NVarChar);
        param[0].Value = objBll.EmployeeCode;
        param[1] = new SqlParameter("@FromDate", SqlDbType.DateTime);
        param[1].Value = objBll.LeaveFrom;
        param[2] = new SqlParameter("@ToDate", SqlDbType.DateTime);
        param[2].Value = objBll.LeaveTo;
        param[3] = new SqlParameter("@ResetBy", SqlDbType.NVarChar);
        param[3].Value = objBll.ResetBy;
        param[4] = new SqlParameter("@EmpLeave_Id", SqlDbType.Int);
        param[4].Value = objBll.EmpLeave_Id;
        param[5] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[5].Value = objBll.PMonth;

        int k = dalobj.sqlcmdExecute("ResetEmployeeLeaves", param);

        return k;

    }

    

    










}