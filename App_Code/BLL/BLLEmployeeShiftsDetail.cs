using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BLLEmployeeShiftsDetail
/// </summary>
public class BLLEmployeeShiftsDetail
{
    _DALEmployeeShiftsDetail objDAL = new _DALEmployeeShiftsDetail();

    public  string EmployeeCode{        get { return employeeCode;}        set { employeeCode= value;}    }	private string  employeeCode;
    public  DateTime AttDate{        get { return attDate;}        set { attDate= value;}    }	private DateTime attDate;
    public  string StartTime{        get { return startTime;}        set { startTime= value;}    }	private string startTime;
    public  string EndTime{        get { return endTime;}        set { endTime= value;}    }	private string endTime;
    public  int Margin{        get { return margin;}        set { margin= value;}    }	private int  margin;
    public  int Status{        get { return status;}        set { status= value;}    }	private int  status;
    public  string FriEndTime{        get { return friEndTime;}        set { friEndTime= value;}    }	private string friEndTime;
    public  string ActSTime{        get { return actSTime;}        set { actSTime= value;}    }	private string actSTime;
    public  string ActETime{        get { return actETime;}        set { actETime= value;}    }	private string actETime;
    public  string AbsentTime{        get { return absentTime;}        set { absentTime= value;}    }	private string absentTime;
    public  bool IsOff{        get { return isOff;}        set { isOff= value;}    }	private bool  isOff;
    public  string PMonth{        get { return pMonth;}        set { pMonth= value;}    }	private string  pMonth;
    public  DateTime CreatedOn{        get { return createdOn;}        set { createdOn= value;}    }	private DateTime createdOn;
    public  int CreatedBy{        get { return createdBy;}        set { createdBy= value;}    }	private int  createdBy;
    public  DateTime ModifiedOn{        get { return modifiedOn;}        set { modifiedOn= value;}    }	private DateTime modifiedOn;
    public  int ModifiedBy{        get { return modifiedBy;}        set { modifiedBy= value;}    }	private int  modifiedBy;


    public BLLEmployeeShiftsDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    public DataTable EmployeeShiftsDetailSelectAll()
    {
        return objDAL.EmployeeShiftsDetailSelectAll();
    }	    

    public int EmployeeShiftsDetailUpdate(BLLEmployeeShiftsDetail objbll)
    {
        return objDAL.EmployeeShiftsDetailUpdate(objbll);
    }

    public DataTable EmployeeShiftsDetailSelectByEmpAndMonth(BLLEmployeeShiftsDetail objbll)
    {
        return objDAL.EmployeeShiftsDetailSelectByEmpAndMonth(objbll);
    }
    public int SingleEmployee_CompleteProcess(BLLEmployeeShiftsDetail objbll)
    {
        return objDAL.SingleEmployee_CompleteProcess(objbll);
    }

   

   
    public bool isLeaveDeduction(BLLEmployeeShiftsDetail objbll)
    {
        if (objDAL.isLeaveDeduction(objbll).Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int ResetLeave(BLLEmployeeShiftsDetail objbll)
    {
        return objDAL.ResetLeave(objbll);
    }
}
