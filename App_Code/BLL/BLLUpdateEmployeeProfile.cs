using System;
using System.Data;

/// <summary>
/// Summary description for BLLUpdateEmployeeProfile
/// </summary>



public class BLLUpdateEmployeeProfile
    {
    public BLLUpdateEmployeeProfile()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    DALUpdateEmployeeProfile objdal = new DALUpdateEmployeeProfile();



    #region 'Start Properties Declaration'

     
    public  string EmployeeCode {get;set;}
    public  string FirstName {get;set;}
    public  string LastName {get;set;}
    public  string  FullName {get;set;}
    public  int Region_Id {get;set;}
    public  int Center_Id {get;set;}
    public  int  DeptCode {get;set;}
    public  int  DesigCode {get;set;}
    public  string  MaritalSts {get;set;}
    public  string  Gender {get;set;}
    public  Nullable <DateTime>  DOJ {get;set;}
    public Nullable<DateTime> DOB { get; set; }
    public string Inactive { get; set; }
    public string Email { get; set; }
    public Nullable<DateTime> ResignDate { get; set; }
    public Nullable<DateTime> RejoinDate { get; set; }


    //--------------------------------------//
    public string Remarks { get; set; }
    public float CasualLeave { get; set; }
    public float AnnualLeave { get; set; }
    //-----------------------------------------//
    #endregion

    #region 'Start Executaion Methods'

    public void EmployeeProfileUpdate(BLLUpdateEmployeeProfile obj)
    {
         objdal.EmployeeProfileUpdate(obj);
       
    }
  

    #endregion
    #region 'Start Fetch Methods'

    public DataTable EmployeeProfileResignedSelect_ForUpdateProfileAndLeaveBalances(BLLUpdateEmployeeProfile obj)
    {
        return objdal.EmployeeProfileResignedSelect_ForUpdateProfileAndLeaveBalances(obj);
    }
    public DataTable EmployeeProfileSelect_ForUpdateProfileAndLeaveBalances(BLLUpdateEmployeeProfile obj)
    {
        return objdal.EmployeeProfileSelect_ForUpdateProfileAndLeaveBalances(obj);
    }

    public DataTable EmployeeProfileDepartmentSelectAll()
    {
        return objdal.EmployeeProfileDepartmentSelectAll();
    }

    #endregion

    }
