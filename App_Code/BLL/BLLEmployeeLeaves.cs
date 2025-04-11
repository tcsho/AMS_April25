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
/// Summary description for BLLEmployeeLeaves
/// </summary>



public class BLLEmployeeLeaves
{
    public BLLEmployeeLeaves()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALEmployeeLeaves objdal = new _DALEmployeeLeaves();



    #region 'Start Properties Declaration'


    private int empLeave_Id;
    private string employeeCode;
    private int leaveType_Id;
    private int leaveDays;
    private string leaveFrom;
    private string leaveTo;
    private string leaveReason;
    private int hRBalances;
    private string hRRemarks;
    private bool hODApprove;
    private string hODRemakrs;
    private string aprvcategory;
    private int createBy;
    private DateTime modifiedOn;
    private DateTime createdOn;
    private int modifiedBy;
    private string pMonth;
    private int hODAPVBy;
    private DateTime hPDAPVOn;
    private bool islock;

    private bool bODApprove;
    private string bODRemarks;
    private int bODAPVBy;
    private DateTime bODAPVOn;

    public int EmpLeave_Id { get { return empLeave_Id; } set { empLeave_Id = value; } }
    public string EmployeeCode { get { return employeeCode; } set { employeeCode = value; } }
    public int LeaveType_Id { get { return leaveType_Id; } set { leaveType_Id = value; } }
    public int LeaveDays { get { return leaveDays; } set { leaveDays = value; } }
    public string LeaveFrom { get { return leaveFrom; } set { leaveFrom = value; } }
    public string LeaveTo { get { return leaveTo; } set { leaveTo = value; } }
    public string LeaveReason { get { return leaveReason; } set { leaveReason = value; } }
    public int HRBalances { get { return hRBalances; } set { hRBalances = value; } }
    public string HRRemarks { get { return hRRemarks; } set { hRRemarks = value; } }
    public bool HODApprove { get { return hODApprove; } set { hODApprove = value; } }
    public string HODRemakrs { get { return hODRemakrs; } set { hODRemakrs = value; } }
    public string AprvCategory { get { return aprvcategory; } set { aprvcategory = value; } }
    public int CreateBy { get { return createBy; } set { createBy = value; } }
    public DateTime ModifiedOn { get { return modifiedOn; } set { modifiedOn = value; } }
    public DateTime CreatedOn { get { return createdOn; } set { createdOn = value; } }
    public int ModifiedBy { get { return modifiedBy; } set { modifiedBy = value; } }
    public string PMonth { get { return pMonth; } set { pMonth = value; } }

    public int HODAPVBy { get { return hODAPVBy; } set { hODAPVBy = value; } }
    public DateTime HPDAPVOn { get { return hPDAPVOn; } set { hPDAPVOn = value; } }

    public bool isLock { get { return islock; } set { islock = value; } }

    public bool BODApprove { get { return bODApprove; } set { bODApprove = value; } }
    public string BODRemarks { get { return bODRemarks; } set { bODRemarks = value; } }
    public int BODAPVBy { get { return bODAPVBy; } set { bODAPVBy = value; } }
    public DateTime BODAPVOn { get { return bODAPVOn; } set { bODAPVOn = value; } }
    
    #endregion

    #region 'Start Executaion Methods'

    public int EmployeeLeavesAdd(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesAdd(_obj);
    }
    public int EmployeeLeavesUpdateEMP(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesUpdateEMP(_obj);
    }
    public int EmployeeLeavesUpdateHOD(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesUpdateHOD(_obj);
    }

    public int EmployeeLeavesUpdateEMPReturn(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesUpdateEMPReturn(_obj);
    }


    public int EmployeeLeavesDelete(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesDelete(_obj);

    }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmployeeLeavesFetchEMP(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesSelectEMP(_obj);
    }
    public DataTable SelectMaternityLeavesEMPEligible(BLLEmployeeLeaves _obj)
    {
        return objdal.SelectMaternityLeavesEMPEligible(_obj);
    }
    public DataTable Select_MaternityLeavesEmp(BLLEmployeeLeaves _obj)
    {
        return objdal.Select_MaternityLeavesEmp(_obj);
    }
    public DataTable EmployeeLeavesFetchHOD(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesSelectHOD(_obj);
    }
    public DataTable EmployeeLeavesFetchHODEMP(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesSelectHODEMP(_obj);
    }
    public DataTable EmployeeLeavesFetchRangeExist(BLLEmployeeLeaves _obj)
    {
        return objdal.EmployeeLeavesSelectRangeExist(_obj);
    }
    public DataTable EmployeeLeavesFetch(int _id)
    {
        return objdal.EmployeeLeavesSelect(_id);
    }
    public int EmployeeLeavesFetchField(int _Id)
    {
        return objdal.EmployeeLeavesSelectField(_Id);
    }

    public DataTable EmployeeLeavesSelectBOD(BLLEmployeeLeaves objbll)
    {
        return objdal.EmployeeLeavesSelectBOD(objbll);
    }

    public int EmployeeLeavesUpdateBOD(BLLEmployeeLeaves objbll)
    {
        return objdal.EmployeeLeavesUpdateBOD(objbll);
    }

    public int WebEmployeeLeaveUpdateHODReset(BLLEmployeeLeaves objbll)
    {
        return objdal.WebEmployeeLeaveUpdateHODReset(objbll);
    }

    public DataTable EmployeeResignationStatus(BLLEmployeeResignationTermination _obj)
    {
        return objdal.EmployeeResignationStatus(_obj);
    }
    #endregion

}
