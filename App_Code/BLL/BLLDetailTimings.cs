using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for BLLDetailTimings
/// </summary>
public class BLLDetailTimings
{
    public BLLDetailTimings()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    _DALDetailTimings objdal = new _DALDetailTimings();

    #region 'Start Properties Declaration'
    public int Region_id { get; set; }
    public int? Center_id { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string Reason { get; set; }
    public TimeSpan TimeIn { get; set; }
    public TimeSpan TimeOut { get; set; }
    public TimeSpan AbsentTime { get; set; }
    public TimeSpan FriStartTime { get; set; }
    public TimeSpan FriAbsentTime { get; set; }
    public TimeSpan FriEndTime { get; set; }
    public TimeSpan SatStartTime { get; set; }
    public TimeSpan SatAbsentTime { get; set; }
    public TimeSpan SatEndTime { get; set; }
    public int Status_Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int? ModifiedBy { get; set; }
    public bool isLocked { get; set; }
    public DateTime? LockedOn { get; set; }
    public int? LockedBy { get; set; }

    public List<BLLDateDetail> ShiftCaseDetailList { get; set; }
    #endregion

    #region 'Start Executaion Methods'
    public int DetailTimingsAdd(string centers,string designations)
    {
        return objdal.DetailTimingsAdd(this, centers,designations);
    }
    #endregion

    #region 'Start Fetch Methods'


    public DataTable DetailTimingsFetch(BLLDetailTimings _obj)
    {
        return objdal.DetailTimingsSelect(_obj);
    }

    #endregion

    public DataTable fetchCenters(BLLDetailTimings _obj)
    {
        return objdal.fetchCenters(_obj);
    }

    public DataTable EmployeeAllDesignationSelectByRegionCenter()
    {
        return objdal.EmployeeAllDesignationSelectByRegionCenter(Region_id);
    }
    public DataTable EmployeeDesignationsSelectByCenters(string centers)
    {
        return objdal.DesignationsSelectByCenters(Region_id, centers);
    }

    public DataTable EmployeesTimingAppliedTo(int shiftCaseId)
    {
        return objdal.EmployeesTimingAppliedTo(shiftCaseId);
    }

    public int EmployeesAppliedTimingDelete(int shiftCaseEmpId)
    {
        return objdal.EmployeesAppliedTimingDelete(shiftCaseEmpId);
    }
}