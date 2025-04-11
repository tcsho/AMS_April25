using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALFacialMachineStatus
/// </summary>
public class DALFacialMachineStatus
{
    DALBaseCommunication dalobj = new DALBaseCommunication();

    DALBase objbase = new DALBase();
    public DALFacialMachineStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int FacialMachineStatusDeviceStatus(BLLFacialMachineStatus objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@DeviceID", SqlDbType.Int);
        param[0].Value = objbll.DeviceID;
        param[1] = new SqlParameter("@Status", SqlDbType.NVarChar);
        param[1].Value = objbll.ConnectionStatus;
        int k = dalobj.sqlcmdExecute("[dbo].[DeviceStatusUpdate]", param);
        return k;
    }
    public int FacialMachineStatusAdd(BLLFacialMachineStatus objbll)
    {
        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@DeviceID", SqlDbType.SmallInt);
        param[0].Value = objbll.DeviceID;

        param[1] = new SqlParameter("@BranchCode", SqlDbType.Int);
        param[1].Value = objbll.BranchCode;

        param[2] = new SqlParameter("@DeviceName", SqlDbType.NVarChar);
        param[2].Value = (objbll.DeviceName != null) ? objbll.DeviceName : "";

        param[3] = new SqlParameter("@DeviceStatus", SqlDbType.NVarChar);
        param[3].Value = (objbll.DeviceStatus != null) ? objbll.DeviceStatus : "";

        param[4] = new SqlParameter("@RegionID", SqlDbType.Int);
        param[4].Value = objbll.RegionID;

        param[5] = new SqlParameter("@DeviceIP", SqlDbType.NVarChar);
        param[5].Value = (objbll.DeviceIP != null) ? objbll.DeviceIP : "";

        param[6] = new SqlParameter("@DevicePort", SqlDbType.Int);
        param[6].Value = objbll.DevicePort;

        param[7] = new SqlParameter("@DeviceSerialNo", SqlDbType.NVarChar);
        param[7].Value = (objbll.DeviceSerialNo != null) ? objbll.DeviceSerialNo : "";

        param[8] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[8].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("WebFacialMachineStatusAdd", param);
        int k = (int)param[8].Value;
        return k;

    }
    public int FacialMachineStatusUpdate(BLLFacialMachineStatus objbll)
    {
        SqlParameter[] param = new SqlParameter[9];

        param[0] = new SqlParameter("@DeviceID", SqlDbType.SmallInt);
        param[0].Value = objbll.DeviceID;

        param[1] = new SqlParameter("@BranchCode", SqlDbType.Int);
        param[1].Value = objbll.BranchCode;

        param[2] = new SqlParameter("@DeviceName", SqlDbType.NVarChar);
        param[2].Value = (objbll.DeviceName != null) ? objbll.DeviceName : "";

        param[3] = new SqlParameter("@DeviceStatus", SqlDbType.NVarChar);
        param[3].Value = (objbll.DeviceStatus != null) ? objbll.DeviceStatus : "";

        param[4] = new SqlParameter("@RegionID", SqlDbType.Int);
        param[4].Value = objbll.RegionID;

        param[5] = new SqlParameter("@DeviceIP", SqlDbType.NVarChar);
        param[5].Value = (objbll.DeviceIP != null) ? objbll.DeviceIP : "";

        param[6] = new SqlParameter("@DevicePort", SqlDbType.Int);
        param[6].Value = objbll.DevicePort;

        param[7] = new SqlParameter("@DeviceSerialNo", SqlDbType.NVarChar);
        param[7].Value = (objbll.DeviceSerialNo != null) ? objbll.DeviceSerialNo : "";

        param[8] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[8].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("WebFacialMachineStatusUpdate", param);
        int k = (int)param[8].Value;
        return k;
    }
    public int FacialMachineStatusDelete(BLLFacialMachineStatus objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@FacialMachineStatus_Id", SqlDbType.Int);
        //param[0].Value = objbll.FacialMachineStatus_Id;

        int k = dalobj.sqlcmdExecute("FacialMachineStatusDelete", param);

        return k;
    }

    public int FacialMachineStatusInsertRecord(BLLFacialMachineStatus objbll)
    {
        SqlParameter[] param = new SqlParameter[6];

        param[0] = new SqlParameter("@EmployeeTerminalID", SqlDbType.Int);
        param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@AttendanceDate", SqlDbType.DateTime);
        param[1].Value = objbll.Attendance;
        param[2] = new SqlParameter("@StatusID", SqlDbType.TinyInt);
        param[2].Value = objbll.StatusID;
        param[3] = new SqlParameter("@DeviceID", SqlDbType.SmallInt);
        param[3].Value = objbll.DeviceID;
        param[4] = new SqlParameter("@AttendanceMethod", SqlDbType.NVarChar);
        param[4].Value = "face";
        param[5] = new SqlParameter("@WorkType", SqlDbType.TinyInt);
        param[5].Value = objbll.WorkCode;

        int k = dalobj.sqlcmdExecute("Terminal.Attendance_Insert", param);

        return k;
    }
    public int FacialMachineStatusInsertLog(int device, string action, int user)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@UserId", SqlDbType.Int);
        param[0].Value = user;
        param[1] = new SqlParameter("@Action", SqlDbType.NVarChar);
        param[1].Value = action;
        param[2] = new SqlParameter("@DeviceId", SqlDbType.Int);
        param[2].Value = device;


        int k = dalobj.sqlcmdExecute("FacialMachineLog_Insert", param);

        return k;
    }
    public int FacialMachineStatusCommentsUpdate(BLLFacialMachineStatus facial)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@DeviceId", SqlDbType.Int);
        param[0].Value = facial.DeviceID;
        param[1] = new SqlParameter("@Comments", SqlDbType.NVarChar);
        param[1].Value = facial.Comments;
        int k = dalobj.sqlcmdExecute("DeviceCommentsUpdate", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'

    public DataTable DEVICEPORT()
    {
         

        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("DEVICEPORT");
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
    public DataTable FacialMachineStatusSelectDeviceByEmployeeCode(BLLFacialMachineStatus obj)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.Int);
        param[0].Value = obj.EmployeeCode;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("WebSelectDeviceByEmployeeCode", param);
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
    public DataTable FacialMachineStatusSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("FacialMachineStatusSelectById", param);
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
    public DataTable WebEmployeeProfileSelectByMachineID(string _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeId", SqlDbType.NVarChar);
        param[0].Value = _id;

        DataTable dt = new DataTable();

        try
        {
            objbase.OpenConnection();
            dt = objbase.sqlcmdFetch("WebEmployeeProfileSelectByMachineID", param);
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
    public DataTable FacialMachineStatusSelect(BLLFacialMachineStatus obj, int mode)
    {
        SqlParameter[] param = new SqlParameter[3];
        DataTable dt = new DataTable();

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = obj.RegionID;
        param[1] = new SqlParameter("@BranchCode", SqlDbType.Int);
        param[1].Value = Convert.ToInt32(obj.BranchCode);

        param[2] = new SqlParameter("@Mode", SqlDbType.Int);
        param[2].Value = mode;

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("WebFacialMachineStatusSelectAll", param);
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

    public DataTable FacialMachineStatusSelectCity(BLLFacialMachineStatus objbll)
    {
        SqlParameter[] param = new SqlParameter[1];
        DataTable dt = new DataTable();

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.RegionID;


        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("WebFacialMachineStatusSelectCityByRegion", param);
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

    public DataTable FacialMachineStatusSelectPullStatus(BLLFacialMachineStatus objbll)
    {
        SqlParameter[] param = new SqlParameter[3];
        DataTable dt = new DataTable();

        param[0] = new SqlParameter("@From_Date", SqlDbType.DateTime);
        param[0].Value = objbll.From_Date;
        param[1] = new SqlParameter("@To_Date", SqlDbType.DateTime);
        param[1].Value = objbll.To_Date;
        param[2] = new SqlParameter("@Pulled_By", SqlDbType.Int);
        param[2].Value = objbll.User_Id;
        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("AttendancePull_LogInsert", param);
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
    
    public DataTable FacialMachineStatusSelectById(int DeviceId)
    {
        SqlParameter[] param = new SqlParameter[1];
        DataTable dt = new DataTable();

        param[0] = new SqlParameter("@DeviceId", SqlDbType.Int);
        param[0].Value = DeviceId;
        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("FacialMachineStatusSelectById", param);
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
    public int FacialMachineStatusDeviceDetails(BLLFacialMachineStatus objbll)
    {
        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@DeviceID", SqlDbType.Int);
        param[0].Value = objbll.DeviceID;
        param[1] = new SqlParameter("@Status", SqlDbType.NVarChar);
        param[1].Value = objbll.ConnectionStatus;
        param[2] = new SqlParameter("@Gateway", SqlDbType.NVarChar);
        param[2].Value = objbll.Gateway;
        param[3] = new SqlParameter("@NetMask", SqlDbType.NVarChar);
        param[3].Value = objbll.NetMask;
        param[4] = new SqlParameter("@Mac", SqlDbType.NVarChar);
        param[4].Value = objbll.MacAddress;
        param[5] = new SqlParameter("@Algorithm", SqlDbType.NVarChar);
        param[5].Value = objbll.Algorithm;
        param[6] = new SqlParameter("@Firmware", SqlDbType.NVarChar);
        param[6].Value = objbll.Firmware;
        param[7] = new SqlParameter("@TotalAttendance", SqlDbType.Int);
        param[7].Value = objbll.TotalAttendance;
        param[8] = new SqlParameter("@AttendanceRecords", SqlDbType.Int);
        param[8].Value = objbll.AttendanceRecords;
        param[9] = new SqlParameter("@SerialNumber", SqlDbType.NVarChar);
        param[9].Value = objbll.DeviceSerialNo;
        param[10] = new SqlParameter("@IP", SqlDbType.NVarChar);
        param[10].Value = objbll.DeviceIP;
        int k = dalobj.sqlcmdExecute("DeviceStatusUpdate", param);
        return k;

    }
    #endregion


}
