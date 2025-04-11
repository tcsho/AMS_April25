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
/// Summary description for BLLEmployeeLeaveType
/// </summary>



public class BLLEmployeeLeaveType
    {
    public BLLEmployeeLeaveType()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    _DALEmployeeLeaveType objdal = new _DALEmployeeLeaveType();



    #region 'Start Properties Declaration'

    private int leaveType_Id;
    private int attendanceType_Id;
    private string leaveType;
    private int status_id;
    private string used_For;    


    public int LeaveType_Id { get { return leaveType_Id; } set { leaveType_Id = value; } }
    public int AttendanceTypeId { get { return attendanceType_Id; } set { attendanceType_Id = value; } }
    public string LeaveType { get { return leaveType; } set { leaveType = value; } }
    public int Status_id { get { return status_id; } set { status_id = value; } }
    public string Used_For{ get { return used_For; } set { used_For = value; } }


    #endregion

    #region 'Start Executaion Methods'

    public int EmployeeLeaveTypeAdd(BLLEmployeeLeaveType _obj)
        {
        return objdal.EmployeeLeaveTypeAdd(_obj);
        }
    public int EmployeeLeaveTypeUpdate(BLLEmployeeLeaveType _obj)
        {
        return objdal.EmployeeLeaveTypeUpdate(_obj);
        }
    public int EmployeeLeaveTypeDelete(BLLEmployeeLeaveType _obj)
        {
        return objdal.EmployeeLeaveTypeDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmployeeLeaveTypeFetch(BLLEmployeeLeaveType _obj)
        {
        return objdal.EmployeeLeaveTypeSelect(_obj);
        }
    public DataTable EmployeeLeaveTypeFetchUsed(BLLEmployeeLeaveType _obj)
        {
        return objdal.EmployeeLeaveTypeSelectUsed(_obj);
        }
    public DataTable EmployeeLeaveTypeFetchByID(BLLEmployeeLeaveType _obj)
        {
        return objdal.EmployeeLeaveTypeSelectByID(_obj);
        }
    public DataTable EmployeeLeaveTypeFetch(int _id)
      {
        return objdal.EmployeeLeaveTypeSelect(_id);
      }
    public int EmployeeLeaveTypeFetchField(int _Id)
        {
        return objdal.EmployeeLeaveTypeSelectField(_Id);
        }
    public DataTable EmployeeAttendanceType()
    {
        return objdal.EmployeeAttendanceType();
    }

    #endregion

}
