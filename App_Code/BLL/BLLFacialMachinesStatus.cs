using System;
using System.Data;

/// <summary>
/// Summary description for BLLFacialMachineStatus
/// </summary>



public class BLLFacialMachineStatus
{
    public BLLFacialMachineStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    DALFacialMachineStatus objdal = new DALFacialMachineStatus();

    #region 'Start Properties Declaration'
    public int EmployeeCode { get; set; }
    public int WorkCode { get; set; }
    public string DeviceID { get; set; }
    public string BranchCode { get; set; }
    public string DeviceName { get; set; }
    public int RegionID { get; set; }
    public string DeviceStatus { get; set; }
    public string Comments { get; set; }
    
    public int CityID { get; set; }
    public string CommPassword { get; set; }
    public string ConfrmPassword { get; set; }
    public string DeviceIP { get; set; }
    public int DevicePort { get; set; }
    public string DeviceSerialNo { get; set; }
    public DateTime? PushLastConnection { get; set; }
    public DateTime? PullLastConnection { get; set; }
    public bool? Checked { get; set; }
    public bool? Status { get; set; }
    public int? StatusID { get; set; }
    public DateTime? Attendance { get; set; }
    public string AttendanceMethod { get; set; }
    public DateTime? From_Date { get; set; }
    public DateTime? To_Date { get; set; }
    public int? User_Id { get; set; }
    public string Gateway { get; set; }
    public string NetMask { get; set; }
    public string MacAddress { get; set; }
    public string Model { get; set; }
    public string Algorithm { get; set; }
    public string Firmware { get; set; }
    public int TotalAttendance { get; set; }
    public int AttendanceRecords { get; set; }
    public string ConnectionStatus { get; set; }
    #endregion

    #region 'Start Executaion Methods'
    public int FacialMachineStatusDeviceStatus(BLLFacialMachineStatus _obj)
    {
        return objdal.FacialMachineStatusDeviceStatus(_obj);

    }
    public int FacialMachineStatusAdd(BLLFacialMachineStatus _obj)
    {
        return objdal.FacialMachineStatusAdd(_obj);
    }
    public int FacialMachineStatusUpdate(BLLFacialMachineStatus _obj)
    {
        return objdal.FacialMachineStatusUpdate(_obj);
    }
    public int FacialMachineStatusDelete(BLLFacialMachineStatus _obj)
    {
        return objdal.FacialMachineStatusDelete(_obj);

    }
    public int FacialMachineStatusInsertRecord(BLLFacialMachineStatus _obj)
    {
        return objdal.FacialMachineStatusInsertRecord(_obj);

    }
    public int FacialMachineStatusInsertLog(int device, string action, int user)
    {
        return objdal.FacialMachineStatusInsertLog(device, action, user);

    }
    public int FacialMachineStatusCommentsUpdate(BLLFacialMachineStatus facial)
    {
        return objdal.FacialMachineStatusCommentsUpdate(facial);

    }

    #endregion
    #region 'Start Fetch Methods'
    public int FacialMachineStatusDeviceDetails(BLLFacialMachineStatus _obj)
    {
        return objdal.FacialMachineStatusDeviceDetails(_obj);
    }
    public DataTable DEVICEPORT()
    {
        return objdal.DEVICEPORT();
    }
    public DataTable FacialMachineStatusFetchDeviceByEmployeeCode(BLLFacialMachineStatus obj)
    {
        return objdal.FacialMachineStatusSelectDeviceByEmployeeCode(obj);
    }

    public DataTable FacialMachineStatusFetch(BLLFacialMachineStatus obj, int mode)
    {
        return objdal.FacialMachineStatusSelect(obj, mode);
    }

    public DataTable FacialMachineStatusSelectCity(BLLFacialMachineStatus _obj)
    {
        return objdal.FacialMachineStatusSelectCity(_obj);
    }

    public DataTable FacialMachineStatusSelectPullStatus(BLLFacialMachineStatus _obj)
    {
        return objdal.FacialMachineStatusSelectPullStatus(_obj);
    }
    public DataTable FacialMachineStatusSelectById(int DeviceId)
    {
        return objdal.FacialMachineStatusSelectById(DeviceId);
    }

    public DataTable FacialMachineStatusFetch(int _id)
    {
        return objdal.FacialMachineStatusSelect(_id);
    }

    public DataTable WebEmployeeProfileSelectByMachineID(string _id)
    {
        return objdal.WebEmployeeProfileSelectByMachineID(_id);
    }


    #endregion

}
