using System;
using System.Data;

/// <summary>
/// Summary description for BLLEmplyeeReportTo
/// </summary>



public class BLLEmployeeResignationTermination
{
    public BLLEmployeeResignationTermination()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALEmplyeeResignationTermination objdal = new _DALEmplyeeResignationTermination();



    #region 'Start Properties Declaration'

    private string employeeCode;
    private string category;
    private DateTime submissionDate;
    private DateTime lastWorkingDate;
    private DateTime approvedDate;
    private int noticePeriod;
    private string reason;
    private string comments;
    private DateTime createdOn;
    private string createdBy;
    private DateTime modifiedOn;
    private string modifiedBy;
    private bool isEmailSent;
    private string reportTo;
    private int statusId;
    private bool hodApprove;
    private string hodRemarks;
    private DateTime hodProposeDate;
    private string lastWorkingDateRemarks;
    private bool hodSupervisorApprove;
    private string hodSupervisorRemarks;
    private DateTime hodSupervisorApprovedDate;
    private bool isSentToERP;
    private string hrRemarks;


    public string EmployeeCode { get { return employeeCode; } set { employeeCode = value; } }
    public string Category { get { return category; } set { category = value; } }
    public DateTime SubmissionDate { get { return submissionDate; } set { submissionDate = value; } }
    public DateTime LastWorkingDate { get { return lastWorkingDate; } set { lastWorkingDate = value; } }
    public DateTime ApprovedDate { get { return approvedDate; } set { approvedDate = value; } }
    public int NoticePeriod { get { return noticePeriod; } set { noticePeriod = value; } }
    public string Reason { get { return reason; } set { reason = value; } }
    public string Comments { get { return comments; } set { comments = value; } }
    public DateTime CreatedOn { get { return createdOn; } set { createdOn = value; } }
    public string CreatedBy { get { return createdBy; } set { createdBy = value; } }
    public DateTime ModifiedOn { get { return modifiedOn; } set { modifiedOn = value; } }
    public string ModifiedBy { get { return modifiedBy; } set { modifiedBy = value; } }
    public bool IsEmailSent { get { return isEmailSent; } set { isEmailSent = value; } }
    public string ReportTo { get { return reportTo; } set { reportTo = value; } }
    public int StatusId { get { return statusId; } set { statusId = value; } }
    public bool HODApprove { get { return hodApprove; } set { hodApprove = value; } }
    public string HODRemarks { get { return hodRemarks; } set { hodRemarks = value; } }
    public string HRRemarks { get { return hrRemarks; } set { hrRemarks = value; } }
    public DateTime HODProposeDate { get { return hodProposeDate; } set { hodProposeDate = value; } }
    public string LastWorkingDateRemarks { get { return lastWorkingDateRemarks; } set { lastWorkingDateRemarks = value; } }
    public bool HODSupervisorApprove { get { return hodSupervisorApprove; } set { hodSupervisorApprove = value; } }
    public string HODSupervisorRemarks { get { return hodSupervisorRemarks; } set { hodSupervisorRemarks = value; } }
    public DateTime HODSupervisorApprovedDate { get { return hodSupervisorApprovedDate; } set { hodSupervisorApprovedDate = value; } }
    public bool IsSentToERP { get { return isSentToERP; } set { isSentToERP = value; } }   
    public string PMonthDesc { get; set; }
    public int Region_Id { get; set; }
    #endregion

    #region 'Start Executaion Methods'

    public int ResignationTerminationAdd(BLLEmployeeResignationTermination _obj)
    {
        return objdal.ResignationTerminationAdd(_obj);
    }

    public int EmployeeResignationUpdateHOD(BLLEmployeeResignationTermination _obj)
    {
        return objdal.EmployeeResignationUpdateHOD(_obj);
    }
    
    public int EmployeeResignationEmailUpdate(BLLEmployeeResignationTermination _obj)
    {
        return objdal.EmployeeResignationEmailUpdate(_obj);
    }

    public int EmployeeResignationERPUpdate(BLLEmployeeResignationTermination _obj)
    {
        return objdal.EmployeeResignationERPUpdate(_obj);
    }

    public int ResignationTerminationReversalUpdate(BLLEmployeeResignationTermination _obj)
    {
        return objdal.ResignationTerminationReversalUpdate(_obj);
    }

    public int UpdateEmployeeLastWorkingDate(BLLEmployeeResignationTermination _obj)
    {
        return objdal.UpdateEmployeeLastWorkingDate(_obj);
    }

    public int EmployeeTerminationUpdateHODSupervisor(BLLEmployeeResignationTermination _obj)
    {
        return objdal.EmployeeTerminationUpdateHODSupervisor(_obj);
    }

    #endregion
    #region 'Start Fetch Methods'

    public DataTable ResignationSelectHOD(BLLEmployeeResignationTermination _obj)
    {
        return objdal.ResignationSelectHOD(_obj);
    }

    public DataTable ResignationTerminationReversalSelectEmployee(BLLEmployeeResignationTermination _obj)
    {
        return objdal.ResignationTerminationReversalSelectEmployee(_obj);
    }

    public DataTable SingleEmployeeDetails(string employeecode)
    {
        return objdal.SingleEmployeeDetails(employeecode);
    }
    
    public DataTable ResignationSelectForAutoApproval()
    {
        return objdal.ResignationSelectForAutoApproval();
    }

    public string EmployeeResignationDataToERP(BLLEmployeeResignationTermination _obj)
    {
        return objdal.EmployeeResignationDataToERP(_obj);
    }

    public string UpdateEmployeeLastWorkingDateToERP(BLLEmployeeResignationTermination _obj)
    {
        return objdal.UpdateEmployeeLastWorkingDateToERP(_obj);
    }

    public string ReverseEmployeeResignationTerminationInERP(BLLEmployeeResignationTermination _obj)
    {
        return objdal.ReverseEmployeeResignationTerminationInERP(_obj);
    }

    public object GetCenterAndRegionIdByEmployee(string employeeCode)
    {
        return objdal.GetCenterAndRegionIdByEmployee(employeeCode);
    }

    public DataTable ResignationSelectHODSupervisor(BLLEmployeeResignationTermination _obj)
    {
        return objdal.ResignationSelectHODSupervisor(_obj);
    }

    #endregion

}
