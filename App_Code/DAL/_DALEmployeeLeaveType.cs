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
/// Summary description for _DALEmployeeLeaveType
/// </summary>
public class _DALEmployeeLeaveType
{
    DALBase dalobj = new DALBase();


    public _DALEmployeeLeaveType()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int EmployeeLeaveTypeAdd(BLLEmployeeLeaveType objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeLeaveTypeInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int EmployeeLeaveTypeUpdate(BLLEmployeeLeaveType objbll)
    {
        SqlParameter[] param = new SqlParameter[10];

 
        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeLeaveTypeUpdate", param);
        int k = (int)param[9].Value;
        return k;
    }
    public int EmployeeLeaveTypeDelete(BLLEmployeeLeaveType objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeLeaveType_Id", SqlDbType.Int);
     //   param[0].Value = objbll.EmployeeLeaveType_Id;


        int k = dalobj.sqlcmdExecute("EmployeeLeaveTypeDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable EmployeeLeaveTypeSelect(int _id)
    {
    SqlParameter[] param = new SqlParameter[1];

    param[0] = new SqlParameter("@tab", SqlDbType.Int);
    param[0].Value = _id;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("WebEMPTYN", param);
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
    
    public DataTable EmployeeLeaveTypeSelect(BLLEmployeeLeaveType objbll)
    {
    SqlParameter[] param = new SqlParameter[1];

    param[0] = new SqlParameter("@Status_id", SqlDbType.Int);
    param[0].Value = objbll.Status_id;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("WebLeaveTypeSelectByStatus", param);
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

    public DataTable EmployeeLeaveTypeSelectUsed(BLLEmployeeLeaveType objbll)
        {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Status_id", SqlDbType.Int);
        param[0].Value = objbll.Status_id;

        param[1] = new SqlParameter("@Used_For", SqlDbType.VarChar);
        param[1].Value = objbll.Used_For;


        DataTable _dt = new DataTable();

        try
            {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebLeaveTypeSelectByUsedStatus", param);
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

    public DataTable EmployeeLeaveTypeSelectByID(BLLEmployeeLeaveType objbll)
        {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@LeaveType_Id", SqlDbType.Int);
        param[0].Value = objbll.LeaveType_Id;

        DataTable _dt = new DataTable();

        try
            {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebLeaveTypeSelectByLeaveTypeId", param);
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

    public int EmployeeLeaveTypeSelectField(int _Id)
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

    public DataTable EmployeeAttendanceType()
    {
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("AllActiveAttendanceType");
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
