using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// Summary description for _DALEmployeeLeaveBalance
/// </summary>
public class _DALEmployeeLeaveBalance
{
    DALBase dalobj = new DALBase();


    public _DALEmployeeLeaveBalance()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int EmployeeLeaveBalanceAdd(BLLEmployeeLeaveBalance objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeLeaveBalanceInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public void EmployeeLeaveBalanceUpdate(BLLEmployeeLeaveBalance objbll)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); 
        param[0].Value = objbll.EmployeeCode;
        
        param[1] = new SqlParameter("@CasualLeave", SqlDbType.Float); 
        param[1].Value = objbll.CasualLeave;
        param[2] = new SqlParameter("@AnnulaLeave", SqlDbType.Int); 
        param[2].Value = objbll.AnnulaLeave;
        param[3] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
        param[3].Value = objbll.Remarks;

        dalobj.sqlcmdExecute("EmployeeLeaveBalanceUpdate", param);
        
    }
    public int EmployeeLeaveBalanceDelete(BLLEmployeeLeaveBalance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeLeaveBalance_Id", SqlDbType.Int);
     //   param[0].Value = objbll.EmployeeLeaveBalance_Id;


        int k = dalobj.sqlcmdExecute("EmployeeLeaveBalanceDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable EmployeeLeaveBalanceSelect(int _id)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
    param[0].Value = _id;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("EmployeeLeaveBalanceSelect", param);
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
    
    public DataTable EmployeeLeaveBalanceSelect(BLLEmployeeLeaveBalance objbll)
    {
    SqlParameter[] param = new SqlParameter[2];

    param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
    param[0].Value = objbll.EmployeeCode;

    param[1] = new SqlParameter("@Year", SqlDbType.NVarChar);
    param[1].Value = objbll.Year;

    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("WebEmployeeLeaveBalanceSelectByEmp", param);
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

    


        public DataTable EmployeeLeaveBalanceFurlough(BLLEmployeeLeaveBalance objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@employeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[1].Value = objbll.PMonth;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("CheckFurloughLeaveBalance", param);
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

    public DataTable EmployeeLeaveBalanceSelect_LastYears(BLLEmployeeLeaveBalance objbll)
    {
    SqlParameter[] param = new SqlParameter[2];

    param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
    param[0].Value = objbll.EmployeeCode;

    param[1] = new SqlParameter("@Year", SqlDbType.NVarChar);
    param[1].Value = objbll.Year;

    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("WebEmployeeLeaveBalanceSelectByEmp_LastYears", param);
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


    public int EmployeeLeaveBalanceSelectField(int _Id)
        {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Section_Id", SqlDbType.Int);
        param[0].Value = _Id;

        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("", param);
        int k = (int)param[1].Value;
        return k;

        }


    #endregion


}
