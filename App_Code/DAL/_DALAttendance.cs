using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALAttendance
/// </summary>
public class _DALAttendance
{
    DALBase dalobj = new DALBase();


    public _DALAttendance()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'

    public int AttendanceProccessLogAdd(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[1].Value = objbll.Region_Id;

        param[2] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[2].Value = objbll.Center_Id;

        int k= dalobj.sqlcmdExecute("DailyAttendanceProcessLogInsert", param);
        return k;

    }
    public int AttendanceAdd(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("AttendanceInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int AttendanceUpdateEmpLeave(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[7];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@EmpLvType_Id", SqlDbType.Int);
        param[1].Value = objbll.EmpLvType_Id;

        param[2] = new SqlParameter("@EmpLvReason", SqlDbType.NVarChar);
        param[2].Value = objbll.EmpLvReason;

        param[3] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[3].Value = objbll.ModifyBy;

        param[4] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[4].Value = objbll.ModifyDate;

        param[5] = new SqlParameter("@EmpLvSubDate", SqlDbType.DateTime);
        param[5].Value = objbll.EmpLvSubDate;

        param[6] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[6].Value = objbll.Submit2HOD;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpLeave", param);

        return k;
    }

    public DataTable GetUpdateCasualLeavesByEmployeeCode(int EmployeeCode)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.Int);
        param[0].Value = EmployeeCode;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("GetCurrentLeavebalanceForHalfDay", param);
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
    public int AttendanceUpdateEmpHalfDays(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[8];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@EmpLvType_Id", SqlDbType.Int);
        param[1].Value = objbll.EmpLvType_Id;

        param[2] = new SqlParameter("@EmpLvReason", SqlDbType.NVarChar);
        param[2].Value = objbll.EmpLvReason;

        param[3] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[3].Value = objbll.ModifyBy;

        param[4] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[4].Value = objbll.ModifyDate;

        param[5] = new SqlParameter("@EmpLvSubDate", SqlDbType.DateTime);
        param[5].Value = objbll.EmpLvSubDate;

        param[6] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[6].Value = objbll.Submit2HOD;

        param[7] = new SqlParameter("@AttendanceTypeId", SqlDbType.Int);
        param[7].Value = objbll.AttendanceTypeId;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpHalfDays", param);

        return k;
    }
    public DataTable ERP_Final_Process_HistorySelectByCenter(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[0].Value = objbll.Center_Id;

        param[1] = new SqlParameter("@From_Date", SqlDbType.Date);
        param[1].Value = objbll.InsertDate;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("ERP_Final_Process_HistorySelectByCenter", param);
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
    public DataTable ERP_Final_Process_HistorySelectMonth(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.Region_Id;

        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = objbll.Center_Id;

        param[2] = new SqlParameter("@Pmonth", SqlDbType.NVarChar);
        param[2].Value = objbll.PMonthDesc;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("ERP_Final_Process_HistorySelectMonth", param);
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
    public int AttendanceUpdateEmpHODLvApv(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[9];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@HODLvAprv", SqlDbType.Bit);
        param[1].Value = objbll.HODLvAprv;

        param[2] = new SqlParameter("@HODLvRemarks", SqlDbType.NVarChar);
        param[2].Value = objbll.HODLvRemarks;

        param[3] = new SqlParameter("@HRLvType_Id", SqlDbType.Int);
        param[3].Value = objbll.HRLvType_Id;

        param[4] = new SqlParameter("@Submit2HR", SqlDbType.Bit);
        param[4].Value = objbll.Submit2HR;

        param[5] = new SqlParameter("@AppLvBy", SqlDbType.NVarChar);
        param[5].Value = objbll.AppLvBy;

        param[6] = new SqlParameter("@AppLvOn", SqlDbType.DateTime);
        param[6].Value = objbll.AppLvOn;

        param[7] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[7].Value = objbll.ModifyBy;

        param[8] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[8].Value = objbll.ModifyDate;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpHODLvApv", param);

        return k;
    }

    public int AttendanceUpdateEmpNeg(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[6];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@NegEmpReason", SqlDbType.NVarChar);
        param[1].Value = objbll.NegEmpReason;

        param[2] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[2].Value = objbll.ModifyBy;

        param[3] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[3].Value = objbll.ModifyDate;

        param[4] = new SqlParameter("@NegAttSubmit2HOD", SqlDbType.Bit);
        param[4].Value = objbll.NegAttSubmit2HOD;

        param[5] = new SqlParameter("@AttendanceTypeId", SqlDbType.Int);
        param[5].Value = objbll.AttendanceTypeId;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpNeg", param);

        return k;
    }

    public int AttendanceUpdateEmpNegMIO(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[6];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@MIOEmpReason", SqlDbType.NVarChar);
        param[1].Value = objbll.MIOEmpReason;

        param[2] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[2].Value = objbll.ModifyBy;

        param[3] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[3].Value = objbll.ModifyDate;

        param[4] = new SqlParameter("@MIOAttSubmit2HOD", SqlDbType.Bit);
        param[4].Value = objbll.MIOAttSubmit2HOD;

        param[5] = new SqlParameter("@AttendanceTypeId", SqlDbType.Int);
        param[5].Value = objbll.AttendanceTypeId;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpNegMIO", param);

        return k;
    }

    public int AttendanceUpdateHODNeg(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[9];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@HODApproval", SqlDbType.Bit);
        param[1].Value = objbll.HODApproval;

        param[2] = new SqlParameter("@ApprovalReason", SqlDbType.NVarChar);
        param[2].Value = objbll.ApprovalReason;

        param[3] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[3].Value = objbll.ModifyBy;

        param[4] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[4].Value = objbll.ModifyDate;

        param[5] = new SqlParameter("@AppNegOn", SqlDbType.DateTime);
        param[5].Value = objbll.AppNegOn;

        param[6] = new SqlParameter("@AppNegBy", SqlDbType.NVarChar);
        param[6].Value = objbll.AppNegBy;

        param[7] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[7].Value = objbll.isLock;

        param[8] = new SqlParameter("@NegHODApp_Id", SqlDbType.Int);
        param[8].Value = objbll.NegHODApp_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateHODNeg", param);

        return k;
    }

    public int AttendanceUpdateHODNegMIO(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[9];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@MIOHODAprv", SqlDbType.Bit);
        param[1].Value = objbll.MIOHODAprv;

        param[2] = new SqlParameter("@MIOApprovalReason", SqlDbType.NVarChar);
        param[2].Value = objbll.MIOApprovalReason;

        param[3] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[3].Value = objbll.ModifyBy;

        param[4] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[4].Value = objbll.ModifyDate;

        param[5] = new SqlParameter("@AppNegOn", SqlDbType.DateTime);
        param[5].Value = objbll.AppNegOn;

        param[6] = new SqlParameter("@AppNegBy", SqlDbType.NVarChar);
        param[6].Value = objbll.AppNegBy;

        param[7] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[7].Value = objbll.isLock;

        param[8] = new SqlParameter("@MIOHODApp_Id", SqlDbType.Int);
        param[8].Value = objbll.MIOHODApp_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateHODNegMIO", param);

        return k;
    }


    public int AttendanceUpdateEmpReturn(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpReturn", param);

        return k;
    }

    public int AttendanceUpdateEmpHalfDaysHR(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[8];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@EmpLvType_Id", SqlDbType.Int);
        param[1].Value = objbll.EmpLvType_Id;

        param[2] = new SqlParameter("@EmpLvReason", SqlDbType.NVarChar);
        param[2].Value = objbll.EmpLvReason;

        param[3] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[3].Value = objbll.ModifyBy;

        param[4] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[4].Value = objbll.ModifyDate;

        param[5] = new SqlParameter("@EmpLvSubDate", SqlDbType.DateTime);
        param[5].Value = objbll.EmpLvSubDate;

        param[6] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[6].Value = objbll.Submit2HOD;

        param[7] = new SqlParameter("@AttendanceTypeId", SqlDbType.Int);
        param[7].Value = objbll.AttendanceTypeId;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpHalfDaysHR", param);

        return k;
    }

    public int AttendanceUpdateEmpMIOHR(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[8];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@EmpLvType_Id", SqlDbType.Int);
        param[1].Value = objbll.EmpLvType_Id;

        param[2] = new SqlParameter("@EmpLvReason", SqlDbType.NVarChar);
        param[2].Value = objbll.EmpLvReason;

        param[3] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[3].Value = objbll.ModifyBy;

        param[4] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[4].Value = objbll.ModifyDate;

        param[5] = new SqlParameter("@EmpLvSubDate", SqlDbType.DateTime);
        param[5].Value = objbll.EmpLvSubDate;

        param[6] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[6].Value = objbll.Submit2HOD;

        param[7] = new SqlParameter("@AttendanceTypeId", SqlDbType.Int);
        param[7].Value = objbll.AttendanceTypeId;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpMIOHR", param);

        return k;
    }

    public int AttendanceUpdateEmpLateHR(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[8];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@EmpLvType_Id", SqlDbType.Int);
        param[1].Value = objbll.EmpLvType_Id;

        param[2] = new SqlParameter("@EmpLvReason", SqlDbType.NVarChar);
        param[2].Value = objbll.EmpLvReason;

        param[3] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[3].Value = objbll.ModifyBy;

        param[4] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[4].Value = objbll.ModifyDate;

        param[5] = new SqlParameter("@EmpLvSubDate", SqlDbType.DateTime);
        param[5].Value = objbll.EmpLvSubDate;

        param[6] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[6].Value = objbll.Submit2HOD;

        param[7] = new SqlParameter("@AttendanceTypeId", SqlDbType.Int);
        param[7].Value = objbll.AttendanceTypeId;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpLateHR", param);

        return k;
    }



    public int AttendanceUpdateEmpHDReturnHR(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpHDayReturnHR", param);

        return k;
    }
    public int AttendanceUpdateEmpLWPReturn(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpLWPReturn", param);

        return k;
    }
    public int AttendanceUpdateEmpNegReturn(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpNegReturn", param);

        return k;
    }
    public int AttendanceUpdateEmpNegReturnHR(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpNegReturnHR", param);

        return k;
    }


    public int AttendanceUpdateEmpNegReturnMIO(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpMIOReturn", param);

        return k;
    }

    public int AttendanceUpdateEmpNegReturnMIOHR(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpMIOReturnHR", param);

        return k;
    }

    public int AttendanceProcess(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[1].Value = objbll.Region_Id;

        param[2] = new SqlParameter("@cetner_Id", SqlDbType.Int);
        param[2].Value = objbll.Center_Id;

        int k = dalobj.sqlcmdExecute("_TSS_EmployeeProcess", param);

        return k;
    }


    public int AttendanceProcessShifts(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[1].Value = objbll.PMonthDesc;

        int k = dalobj.sqlcmdExecute("SingleEmployee_employeeShiftsDetailMOnthWise", param);

        return k;
    }

    public int AttendanceProcessSingleEmployee(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@employee_code", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        //dalobj.sqlcmdExecute("Attendance_UpdateHODFromERP", param);
        int k = dalobj.sqlcmdExecute("SingleEmployee_CompleteProcess", param);
        

        return k;
    }

    public int AttendanceProcessReservation(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@employee_code", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@Pmonth", SqlDbType.NVarChar);
        param[1].Value = objbll.PMonthDesc;

        int k = dalobj.sqlcmdExecute("SingleEmployee_11_EMPReservationProcess", param);

        return k;
    }
    public int AttendanceUpdate(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[7];

        param[0] = new SqlParameter("@Att_Id", SqlDbType.Int);
        param[0].Value = objbll.Att_Id;

        param[1] = new SqlParameter("@EmpLvType_Id", SqlDbType.Int);
        param[1].Value = objbll.EmpLvType_Id;

        param[2] = new SqlParameter("@EmpLvReason", SqlDbType.NVarChar);
        param[2].Value = objbll.EmpLvReason;

        param[3] = new SqlParameter("@ModifyBy", SqlDbType.NVarChar);
        param[3].Value = objbll.ModifyBy;

        param[4] = new SqlParameter("@ModifyDate", SqlDbType.DateTime);
        param[4].Value = objbll.ModifyDate;

        param[5] = new SqlParameter("@EmpLvSubDate", SqlDbType.DateTime);
        param[5].Value = objbll.EmpLvSubDate;

        param[6] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[6].Value = objbll.Submit2HOD;

        int k = dalobj.sqlcmdExecute("WebAtendanceUpdateEmpLeave", param);

        return k;
    }
    public int AttendanceDelete(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Attendance_Id", SqlDbType.Int);
        //   param[0].Value = objbll.Attendance_Id;


        int k = dalobj.sqlcmdExecute("AttendanceDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable AttendanceSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("AttendanceSelect", param);
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

    public DataTable AttendanceSelect(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HOD;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeAbsent", param);
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

    public DataTable EmployeeAvailedAndBalanceLeaveForTheYear(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeAvailedAndBalanceLeaveForTheYear", param);
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

    public DataTable GetTodaysTimeIn(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];
 

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("Atten_LogGetTodaysTimeIn", param);
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
    


    public DataTable HalfDaysSelect(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HOD;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeHalfDays", param);
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

    public DataTable HalfDaysSelectHR(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HOD;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeHalfDaysHR", param);
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

    public DataTable LWPSelect(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HOD;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeLWP", param);
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





    public DataTable AttendanceSelectHOD(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HR", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HR;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeLeaveHOD", param);
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


    public DataTable AttendanceSelectHODEMP(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HR", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HR;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeLeaveHODEMP", param);
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


    public DataTable HalfDaysSelectHOD(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HR", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HR;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeHalfDaysHOD", param);
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
    public DataTable HalfDaysSelectHODEMP(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HR", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HR;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeHalfDaysHODEMP", param);
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



    public DataTable AttendanceSelectDepartmentsByMonthUserIdUserTypeId(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@User_Id", SqlDbType.Int);
        param[1].Value = objbll.User_Id;

        param[2] = new SqlParameter("@UserType_id", SqlDbType.Int);
        param[2].Value = objbll.UserTypeId;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebDepartmentSelectByMonthUserIdUserTypeId", param);
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


    public DataTable AttendanceSelectDepartmentsByMonthRegionCenter(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[1].Value = objbll.Region_Id;

        param[2] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[2].Value = objbll.Center_Id;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebDepartmentSelectByMonthRegionCenter", param);
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

    public DataTable AttendanceSelectDesignationByDepartmentId(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];


        param[0] = new SqlParameter("@DeptCode", SqlDbType.Int);
        param[0].Value = objbll.DeptCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebDesignationByDepartmentCode", param);
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

    public DataTable AttendanceSelectNegEmp(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@NegAttSubmit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.NegAttSubmit2HOD;

        param[3] = new SqlParameter("@islock", SqlDbType.Bit);
        param[3].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeNeg", param);
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


    public DataTable AttendanceSelectNegEmpLate(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@NegAttSubmit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.NegAttSubmit2HOD;

        param[3] = new SqlParameter("@islock", SqlDbType.Bit);
        param[3].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAttendanceSelectEmployeeNegNewLate", param);
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
    public DataTable AttendanceSelectNegEmpMIO(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@MIOAttSubmit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.MIOAttSubmit2HOD;

        param[3] = new SqlParameter("@islock", SqlDbType.Bit);
        param[3].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAttendanceSelectEmployeeNegNewMIO", param);
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

    public DataTable AttendanceSelectNegEmpMIOHR(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@MIOAttSubmit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.MIOAttSubmit2HOD;

        param[3] = new SqlParameter("@islock", SqlDbType.Bit);
        param[3].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAttendanceSelectEmployeeNegNewMIOHR", param);
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


    public DataTable AttendanceSelectNegHOD(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[2].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectHODNeg", param);
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




    public DataTable AttendanceSelectNegHODLate(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[2].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectHODNegLate", param);
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

    public DataTable AttendanceSelectNegHODLateEMP(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[2].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectHODNegLateEMP", param);
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
    public DataTable AttendanceSelectNegHODMIO(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[2].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectHODNegMIO_New", param);
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

    public DataTable AttendanceSelectNegHODMIOEmp(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@isLock", SqlDbType.Bit);
        param[2].Value = objbll.isLock;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectHODEmployeeWiseNegMIO", param);
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









    public DataTable AttendanceSelectIsAnual(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@LeaveGroup", SqlDbType.NVarChar);
        param[0].Value = objbll.LeaveGroup;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectIsAnual", param);
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

    public DataTable AttendanceSelectLeaveBalace(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@Submit2HOD", SqlDbType.Bit);
        param[2].Value = objbll.Submit2HOD;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectEmployeeAbsent", param);
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

    public int AttendanceSelectField(int _Id)
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

    public DataTable AttendanceSelectSummary(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@ReportTo", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;

        param[2] = new SqlParameter("@UserType_id", SqlDbType.Int);
        param[2].Value = objbll.UserTypeId;

        param[3] = new SqlParameter("@DeptCode", SqlDbType.Int);
        param[3].Value = objbll.DeptCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectSummary", param);
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


    public DataTable AttendanceSelectSummaryNew(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[6];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[1].Value = objbll.Region_Id;

        param[2] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[2].Value = objbll.Center_Id;

        param[3] = new SqlParameter("@ReportTo", SqlDbType.NVarChar);
        param[3].Value = objbll.EmployeeCode;

        param[4] = new SqlParameter("@UserType_id", SqlDbType.Int);
        param[4].Value = objbll.UserTypeId;

        param[5] = new SqlParameter("@DeptCode", SqlDbType.Int);
        param[5].Value = objbll.DeptCode;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebAtendanceSelectSummaryNew", param);
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


    public DataTable GetHalfDaysUnOfficial(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("GetHalfDays_UnOfficial", param);
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



    public DataTable GetHalfDaysUnOfficial_Emp(BLLAttendance objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[0].Value = objbll.PMonthDesc;

        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = objbll.EmployeeCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("GetHalfDays_UnOfficial_Emp", param);
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
