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
/// Summary description for _DALEmployeeLeaves
/// </summary>
public class _DALEmployeeLeaves
{
    DALBase dalobj = new DALBase();


    public _DALEmployeeLeaves()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int EmployeeLeavesAdd(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[10];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@LeaveType_Id", SqlDbType.Int);
        param[1].Value = objbll.LeaveType_Id;

        param[2] = new SqlParameter("@LeaveDays", SqlDbType.Int);
        param[2].Value = objbll.LeaveDays;

        param[3] = new SqlParameter("@LeaveFrom", SqlDbType.NVarChar);
        param[3].Value = objbll.LeaveFrom;

        param[4] = new SqlParameter("@LeaveTo", SqlDbType.NVarChar);
        param[4].Value = objbll.LeaveTo;

        param[5] = new SqlParameter("@LeaveReason", SqlDbType.NVarChar);
        param[5].Value = objbll.LeaveReason;

        param[6] = new SqlParameter("@CreateBy", SqlDbType.Int);
        param[6].Value = objbll.CreateBy;

        param[7] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        param[7].Value = objbll.CreatedOn;

        param[8] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[8].Value = objbll.PMonth;

        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("WebEmployeeLeaveINSERT", param);
        int k = (int)param[9].Value;
        return k;

    }
    public int EmployeeLeavesUpdateEMP(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[9];

        param[0] = new SqlParameter("@EmpLeave_Id", SqlDbType.Int);
        param[0].Value = objbll.EmpLeave_Id;

        param[1] = new SqlParameter("@LeaveType_Id", SqlDbType.Int);
        param[1].Value = objbll.LeaveType_Id;

        param[2] = new SqlParameter("@LeaveDays", SqlDbType.Int);
        param[2].Value = objbll.LeaveDays;

        param[3] = new SqlParameter("@LeaveFrom", SqlDbType.NVarChar);
        param[3].Value = objbll.LeaveFrom;

        param[4] = new SqlParameter("@LeaveTo", SqlDbType.NVarChar);
        param[4].Value = objbll.LeaveTo;

        param[5] = new SqlParameter("@LeaveReason", SqlDbType.NVarChar);
        param[5].Value = objbll.LeaveReason;


        param[6] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        param[6].Value = objbll.ModifiedOn;

        param[7] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
        param[7].Value = objbll.ModifiedBy;

        param[8] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[8].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("WebEmployeeLeaveUpdateEmp", param);
        int k = (int)param[8].Value;
        return k;
    }


    public int EmployeeLeavesUpdateEMPReturn(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmpLeave_Id", SqlDbType.Int);
        param[0].Value = objbll.EmpLeave_Id;

        int k = dalobj.sqlcmdExecute("WebEmployeeLeaveUpdateEMPReset", param);
        return k;
    }

    public int EmployeeLeavesUpdateHOD(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[10];

        param[0] = new SqlParameter("@EmpLeave_Id", SqlDbType.Int);
        param[0].Value = objbll.EmpLeave_Id;

        param[1] = new SqlParameter("@HODApprove", SqlDbType.Bit);
        param[1].Value = objbll.HODApprove;

        param[2] = new SqlParameter("@HODRemakrs", SqlDbType.NVarChar);
        param[2].Value = objbll.HODRemakrs;

        param[3] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        param[3].Value = objbll.ModifiedOn;

        param[4] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
        param[4].Value = objbll.ModifiedBy;

        param[5] = new SqlParameter("@HODAPVBy", SqlDbType.Int);
        param[5].Value = objbll.HODAPVBy;

        param[6] = new SqlParameter("@HPDAPVOn", SqlDbType.DateTime);
        param[6].Value = objbll.HPDAPVOn;

        param[7] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[7].Value = objbll.isLock;

        param[8] = new SqlParameter("@AprvCategory", SqlDbType.NVarChar);
        param[8].Value = objbll.AprvCategory;

        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        int k = dalobj.sqlcmdExecute("WebEmployeeLeaveUpdateHOD", param);
        return k;
    }
    public int EmployeeLeavesDelete(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmpLeave_Id", SqlDbType.Int);
        param[0].Value = objbll.EmpLeave_Id;


        int k = dalobj.sqlcmdExecute("WebEmployeeLeaveDelete", param);

        return k;
    }



    public DataTable EmployeeLeavesSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmpLeave_Id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebEmployeeLeavesSelectByID", param);
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
    public DataTable SelectMaternityLeavesEMPEligible(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        //param[1] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        //param[1].Value = objbll.PMonth;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SelectMaternityLeavesEMPEligible", param);
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
    public DataTable EmployeeLeavesSelectEMP(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        //param[1] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        //param[1].Value = objbll.PMonth;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebEmployeeLeavesSelectEmp", param);
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

    public DataTable EmployeeLeavesSelectHOD(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@HODApprove", SqlDbType.Bit);
        param[1].Value = objbll.HODApprove;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebEmployeeLeavesSelectHOD", param);
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


    public DataTable Select_MaternityLeavesEmp(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        //param[1] = new SqlParameter("@HODApprove", SqlDbType.Bit);
        //param[1].Value = objbll.HODApprove;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SelectMaternityLeavesEMP", param);
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

    public DataTable EmployeeLeavesSelectHODEMP(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@HODApprove", SqlDbType.Bit);
        param[1].Value = objbll.HODApprove;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebEmployeeLeavesSelectHODEMP", param);
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

    public DataTable EmployeeLeavesSelectBOD(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        /*param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;*/

        param[0] = new SqlParameter("@BODApprove", SqlDbType.Bit);
        param[0].Value = objbll.BODApprove;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebEmployeeLeavesSelectBOD", param);
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

    public DataTable EmployeeLeavesSelectRangeExist(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@fromdate", SqlDbType.NVarChar);
        param[1].Value = objbll.LeaveFrom;

        param[2] = new SqlParameter("@todate", SqlDbType.NVarChar);
        param[2].Value = objbll.LeaveTo;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebEmployeeLeavesSelectByRangeExist", param);
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

    public int EmployeeLeavesSelectField(int _Id)
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

    public int EmployeeLeavesUpdateBOD(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[9];

        param[0] = new SqlParameter("@EmpLeave_Id", SqlDbType.Int);
        param[0].Value = objbll.EmpLeave_Id;

        param[1] = new SqlParameter("@BODApprove", SqlDbType.Bit);
        param[1].Value = objbll.BODApprove;

        param[2] = new SqlParameter("@BODRemakrs", SqlDbType.NVarChar);
        param[2].Value = objbll.BODRemarks;

        param[3] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        param[3].Value = objbll.ModifiedOn;

        param[4] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
        param[4].Value = objbll.ModifiedBy;

        param[5] = new SqlParameter("@BODAPVBy", SqlDbType.Int);
        param[5].Value = objbll.BODAPVBy;

        param[6] = new SqlParameter("@BODAPVOn", SqlDbType.DateTime);
        param[6].Value = objbll.BODAPVOn;

        param[7] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[7].Value = objbll.isLock;

        param[8] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[8].Direction = ParameterDirection.Output;

        int k = dalobj.sqlcmdExecute("WebEmployeeLeaveUpdateBOD", param);
        return k;
    }

    //WebEmployeeLeaveUpdateHODReset

    public int WebEmployeeLeaveUpdateHODReset(BLLEmployeeLeaves objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmpLeave_Id", SqlDbType.Int);
        param[0].Value = objbll.EmpLeave_Id;

        int k = dalobj.sqlcmdExecute("WebEmployeeLeaveUpdateHODReset", param);
        return k;
    }
    public DataTable EmployeeResignationStatus(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        //param[0] = new SqlParameter("@EmployeeId", SqlDbType.NVarChar);
        //param[0].Value = Convert.ToInt32(objbll.EmployeeCode);
        param[0] = new SqlParameter("@CreateBy", SqlDbType.NVarChar);
        param[0].Value = objbll.CreatedBy;

        //param[1] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        //param[1].Value = objbll.PMonth;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("AMS_EmployeeResignationStatus", param);
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
    } 
}
