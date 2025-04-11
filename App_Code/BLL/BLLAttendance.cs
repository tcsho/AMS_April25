using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for BLLAttendance
/// </summary>



public class BLLAttendance
{
    public BLLAttendance()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALAttendance objdal = new _DALAttendance();



    #region 'Start Properties Declaration'

    private int att_Id;
    private string employeeCode;
    private DateTime attDate;
    private DateTime timeIn;
    private DateTime timeOut;
    private int deptCode;
    private int desigCode;
    private int region_Id;
    private int center_Id;
    private int totalMins;
    private bool lateArrival;
    private bool earlyLeft;
    private string processSts;
    private string dayType;
    private string pMonthDesc;
    private string insertBy;
    protected DateTime insertDate { get; set; }
    private string modifyBy;
    private DateTime modifyDate;
    private string attType;
    private DateTime actualTime;
    private DateTime actualOut;
    private int leaveReq;
    private bool hODApproval;
    private string approvalReason;
    private string satOff;
    private string offDay;
    private int leaveType_Id;
    private bool isabsent;
    private DateTime absentTime;
    private int empLvType_Id;
    private int attTypeId;
    private string hODLvRemarks;
    private int hRLvType_Id;
    private bool hRLvLock;
    private string empLvReason;
    private bool islock;
    private bool hODLvAprv;
    private bool hRLvAprv;
    private bool submit2HOD;
    private bool submit2HR;
    private string leaveGroup;
    private string appNegBy;
    private DateTime appNegOn;
    private string appLvBy;
    private DateTime appLvOn;
    private DateTime empLvSubDate;

    private bool negAttSubmit2HOD;
    private string negEmpReason;
    private int negHODApp_Id;

    private int userTypeId;
    private bool eRPProcessLock;

    private bool mioAttSubmit2HOD;
    private string mioEmpReason;
    private int mioHODApp_Id;

    private bool miohODAprv;
    private string mioapprovalReason;
    private int attendanceTypeId;



    public int Att_Id { get { return att_Id; } set { att_Id = value; } }
    public string EmployeeCode { get { return employeeCode; } set { employeeCode = value; } }
    public DateTime AttDate { get { return attDate; } set { attDate = value; } }
    public DateTime TimeIn { get { return timeIn; } set { timeIn = value; } }
    public DateTime TimeOut { get { return timeOut; } set { timeOut = value; } }
    public int DeptCode { get { return deptCode; } set { deptCode = value; } }
    public int DesigCode { get { return desigCode; } set { desigCode = value; } }
    public int Region_Id { get { return region_Id; } set { region_Id = value; } }
    public int Center_Id { get { return center_Id; } set { center_Id = value; } }
    public int TotalMins { get { return totalMins; } set { totalMins = value; } }
    public bool LateArrival { get { return lateArrival; } set { lateArrival = value; } }
    public bool EarlyLeft { get { return earlyLeft; } set { earlyLeft = value; } }
    public string ProcessSts { get { return processSts; } set { processSts = value; } }
    public string DayType { get { return dayType; } set { dayType = value; } }
    public string PMonthDesc { get { return pMonthDesc; } set { pMonthDesc = value; } }
    public string InsertBy { get { return insertBy; } set { insertBy = value; } }
    public DateTime InsertDate { get { return insertDate; } set { insertDate = value; } }
    public string ModifyBy { get { return modifyBy; } set { modifyBy = value; } }
    public DateTime ModifyDate { get { return modifyDate; } set { modifyDate = value; } }
    public string AttType { get { return attType; } set { attType = value; } }
    public DateTime ActualTime { get { return actualTime; } set { actualTime = value; } }
    public DateTime ActualOut { get { return actualOut; } set { actualOut = value; } }
    public int LeaveReq { get { return leaveReq; } set { leaveReq = value; } }
    public bool HODApproval { get { return hODApproval; } set { hODApproval = value; } }
    public string ApprovalReason { get { return approvalReason; } set { approvalReason = value; } }
    public string SatOff { get { return satOff; } set { satOff = value; } }
    public string OffDay { get { return offDay; } set { offDay = value; } }
    public int LeaveType_Id { get { return leaveType_Id; } set { leaveType_Id = value; } }
    public bool isAbsent { get { return isabsent; } set { isabsent = value; } }
    public DateTime AbsentTime { get { return absentTime; } set { absentTime = value; } }
    public int EmpLvType_Id { get { return empLvType_Id; } set { empLvType_Id = value; } }
    //public int AttTypeId { get { return attTypeId; } set { attTypeId = value; } }
    public string HODLvRemarks { get { return hODLvRemarks; } set { hODLvRemarks = value; } }
    public int HRLvType_Id { get { return hRLvType_Id; } set { hRLvType_Id = value; } }
    public bool HRLvLock { get { return hRLvLock; } set { hRLvLock = value; } }
    public string EmpLvReason { get { return empLvReason; } set { empLvReason = value; } }
    public bool isLock { get { return islock; } set { islock = value; } }
    public bool HODLvAprv { get { return hODLvAprv; } set { hODLvAprv = value; } }
    public bool HRLvAprv { get { return hRLvAprv; } set { hRLvAprv = value; } }
    public bool Submit2HOD { get { return submit2HOD; } set { submit2HOD = value; } }
    public bool Submit2HR { get { return submit2HR; } set { submit2HR = value; } }
    public string LeaveGroup { get { return leaveGroup; } set { leaveGroup = value; } }
    public string AppNegBy { get { return appNegBy; } set { appNegBy = value; } }
    public DateTime AppNegOn { get { return appNegOn; } set { appNegOn = value; } }
    public string AppLvBy { get { return appLvBy; } set { appLvBy = value; } }
    public DateTime AppLvOn { get { return appLvOn; } set { appLvOn = value; } }
    public DateTime EmpLvSubDate { get { return empLvSubDate; } set { empLvSubDate = value; } }

    public bool NegAttSubmit2HOD { get { return negAttSubmit2HOD; } set { negAttSubmit2HOD = value; } }
    public string NegEmpReason { get { return negEmpReason; } set { negEmpReason = value; } }
    public int NegHODApp_Id { get { return negHODApp_Id; } set { negHODApp_Id = value; } }

    public int UserTypeId { get { return userTypeId; } set { userTypeId = value; } }
    public bool ERPProcessLock { get { return eRPProcessLock; } set { eRPProcessLock = value; } }

    public int User_Id { get; set; }

    public bool MIOAttSubmit2HOD { get { return mioAttSubmit2HOD; } set { mioAttSubmit2HOD = value; } }
    public string MIOEmpReason { get { return mioEmpReason; } set { mioEmpReason = value; } }
    public int MIOHODApp_Id { get { return mioHODApp_Id; } set { mioHODApp_Id = value; } }

    public bool MIOHODAprv { get { return miohODAprv; } set { miohODAprv = value; } }
    public string MIOApprovalReason { get { return mioapprovalReason; } set { mioapprovalReason = value; } }
    public int AttendanceTypeId { get { return attendanceTypeId; } set { attendanceTypeId = value; } }


    #endregion

    #region 'Start Executaion Methods'

    public int AttendanceAdd(BLLAttendance _obj)
    {
        return objdal.AttendanceAdd(_obj);
    }
    public int AttendanceProccessLogAdd(BLLAttendance _obj)
    {
        return objdal.AttendanceProccessLogAdd(_obj);
    }
    public int AttendanceUpdate(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdate(_obj);
    }



    public int AttendanceProcess(BLLAttendance _obj)
    {
        return objdal.AttendanceProcess(_obj);
    }



    public int AttendanceProcessShifts(BLLAttendance _obj)
    {
        return objdal.AttendanceProcessShifts(_obj);
    }
    public int AttendanceProcessSingleEmployee(BLLAttendance _obj)
    {
        return objdal.AttendanceProcessSingleEmployee(_obj);
    }

    public int AttendanceProcessReservation(BLLAttendance _obj)
    {
        return objdal.AttendanceProcessReservation(_obj);
    }


    public int AttendanceUpdateEmpLeave(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpLeave(_obj);
    }
    public int AttendanceUpdateEmpHalfDays(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpHalfDays(_obj);
    }
    public DataTable GetUpdatedCasualLeave(int EmployeeCode)
    {
        return objdal.GetUpdateCasualLeavesByEmployeeCode(EmployeeCode);
    }
    public int AttendanceUpdateEmpHODLvApv(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpHODLvApv(_obj);
    }

    public int AttendanceUpdateEmpNeg(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpNeg(_obj);
    }

    public int AttendanceUpdateEmpNegMIO(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpNegMIO(_obj);
    }


    public int AttendanceUpdateHODNeg(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateHODNeg(_obj);
    }


    public int AttendanceUpdateHODNegMIO(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateHODNegMIO(_obj);
    }




    public int AttendanceUpdateEmpReturn(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpReturn(_obj);
    } 
    
    public int AttendanceUpdateEmpHalfDaysHR(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpHalfDaysHR(_obj);
    } 
    public int AttendanceUpdateEmpMIOHR(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpMIOHR(_obj);
    } 
    public int AttendanceUpdateEmpLateHR(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpLateHR(_obj);
    } 
    public int AttendanceUpdateEmpHDReturnHR(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpHDReturnHR(_obj);
    } 
    public int AttendanceUpdateEmpLWPReturn(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpLWPReturn(_obj);
    }

    public int AttendanceUpdateEmpNegReturn(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpNegReturn(_obj);
    } 
    public int AttendanceUpdateEmpNegReturnHR(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpNegReturnHR(_obj);
    }

    public int AttendanceUpdateEmpNegReturnMIO(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpNegReturnMIO(_obj);
    }
    public int AttendanceUpdateEmpNegReturnMIOHR(BLLAttendance _obj)
    {
        return objdal.AttendanceUpdateEmpNegReturnMIOHR(_obj);
    }
    public int AttendanceDelete(BLLAttendance _obj)
    {
        return objdal.AttendanceDelete(_obj);

    }

    #endregion

    #region 'Start Fetch Methods'


    public DataTable AttendanceFetch(BLLAttendance _obj)
    {
        return objdal.AttendanceSelect(_obj);
    }

    public DataTable EmployeeAvailedAndBalanceLeaveForTheYear(BLLAttendance _obj)
    {
        return objdal.EmployeeAvailedAndBalanceLeaveForTheYear(_obj);
    }

    public DataTable GetTodaysTimeIn(BLLAttendance _obj)
    {
        return objdal.GetTodaysTimeIn(_obj);
    }

    


    public DataTable HalfDaysSelect(BLLAttendance _obj)
    {
        return objdal.HalfDaysSelect(_obj);
    }
    public DataTable HalfDaysSelectHR(BLLAttendance _obj)
    {
        return objdal.HalfDaysSelectHR(_obj);
    }
    
    public DataTable LWPSelect(BLLAttendance _obj)
    {
        return objdal.LWPSelect(_obj);
    }




    public DataTable AttendanceFetchHOD(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectHOD(_obj);
    }

    public DataTable AttendanceFetchHODEMP(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectHODEMP(_obj);
    }

    public DataTable HalfDaysFetchHOD(BLLAttendance _obj)
    {
        return objdal.HalfDaysSelectHOD(_obj);
    }
    public DataTable HalfDaysFetchHODEMP(BLLAttendance _obj)
    {
        return objdal.HalfDaysSelectHODEMP(_obj);
    }



    public DataTable AttendanceSelectDepartmentsByMonthUserIdUserTypeId(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectDepartmentsByMonthUserIdUserTypeId(_obj);
    }

    public DataTable AttendanceSelectDepartmentsByMonthRegionCenter(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectDepartmentsByMonthRegionCenter(_obj);
    }

    public DataTable AttendanceSelectDesignationByDepartmentId(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectDesignationByDepartmentId(_obj);
    }

    public DataTable AttendanceFetchNegEmp(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegEmp(_obj);
    }


    public DataTable AttendanceSelectNegEmpLate(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegEmpLate(_obj);
    }

    public DataTable AttendanceSelectNegEmpMIO(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegEmpMIO(_obj);
    }
    public DataTable AttendanceSelectNegEmpMIOHR(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegEmpMIOHR(_obj);
    }

    public DataTable AttendanceFetchNegHOD(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegHOD(_obj);
    }

    public DataTable AttendanceFetchNegHODLate(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegHODLate(_obj);
    }

    public DataTable AttendanceFetchNegHODLateEMP(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegHODLateEMP(_obj);
    }
    public DataTable AttendanceFetchNegHODMIO(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegHODMIO(_obj);
    }

    public DataTable AttendanceSelectNegHODMIOEmp(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectNegHODMIOEmp(_obj);
    }


    public DataTable ERP_Final_Process_HistorySelectByCenter(BLLAttendance obj)
    {
        return objdal.ERP_Final_Process_HistorySelectByCenter(obj);
    }

    public DataTable ERP_Final_Process_HistorySelectMonth(BLLAttendance obj)
    {
        return objdal.ERP_Final_Process_HistorySelectMonth(obj);
    }


    public DataTable AttendanceFetchIsAnual(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectIsAnual(_obj);
    }

    public DataTable AttendanceFetchSummary(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectSummary(_obj);
    }
    public DataTable AttendanceFetchSummaryNew(BLLAttendance _obj)
    {
        return objdal.AttendanceSelectSummaryNew(_obj);
    }
    public DataTable AttendanceFetch(int _id)
    {
        return objdal.AttendanceSelect(_id);
    }

    public DataTable GetHalfDaysUnOfficial(BLLAttendance _obj)
    {
        return objdal.GetHalfDaysUnOfficial(_obj);
    }

    public DataTable GetHalfDaysUnOfficial_Emp(BLLAttendance _obj)
    {
        return objdal.GetHalfDaysUnOfficial_Emp(_obj);
    }

    public int AttendanceFetchField(int _Id)
    {
        return objdal.AttendanceSelectField(_Id);
    }


   

    #endregion


    
}
