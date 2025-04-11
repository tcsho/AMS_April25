using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLResetLeavesEmployeewise
/// </summary>
public class BLLEmployeeLeavesAdjustment
{
    public BLLEmployeeLeavesAdjustment()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALEmployeeLeavesAdjustment objDAL = new _DALEmployeeLeavesAdjustment();

    public int LeaveResetLog_id { get { return leaveResetLog_id; } set { leaveResetLog_id = value; } }	private int leaveResetLog_id;

    public string EmployeeCode { get { return employeeCode; } set { employeeCode = value; } }	private string employeeCode;

    public DateTime LeaveFrom { get { return leaveFrom; } set { leaveFrom = value; } }	private DateTime leaveFrom;

    public DateTime LeaveTo { get { return leaveTo; } set { leaveTo = value; } }	private DateTime leaveTo;

    public string ResetBy { get { return resetBy; } set { resetBy = value; } }	private string resetBy;

    public DateTime ResetOn { get { return resetOn; } set { resetOn = value; } }	private DateTime resetOn;

    public bool Is_reserved { get { return is_reserved; } set { is_reserved = value; } }	private bool is_reserved;

    public int EmpLeave_Id { get { return empLeave_Id; } set { empLeave_Id = value; } }	private int empLeave_Id;

    public string PMonth { get { return pMonth; } set { pMonth = value; } }	private string pMonth;

    public int DepartmentId { get { return departmentId; } set { departmentId = value; } }	private int departmentId;

    public int Center_Id { get { return center_Id; } set { center_Id = value; } }	private int center_Id;

    public int Region_Id { get { return region_Id; } set { region_Id = value; } }	private int region_Id;

    public string LeaveGroup { get { return leaveGroup; } set { leaveGroup = value; } }	private string leaveGroup;





    public DataTable FetchLeavesEmployeewise(BLLEmployeeLeavesAdjustment objBll)
    {
        return objDAL.FetchLeavesEmployeewise(objBll);
    }


    public int ResetEmployeeLeaves(BLLEmployeeLeavesAdjustment objBll)
    {
        return objDAL.ResetEmployeeLeaves(objBll);
    }


     
}