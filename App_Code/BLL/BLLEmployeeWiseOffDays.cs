using System;
using System.Data;

/// <summary>
/// Summary description for BLLEmployeeWiseOffDays
/// </summary>



public class BLLEmployeeWiseOffDays
    {
    public BLLEmployeeWiseOffDays()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    DALEmployeeWiseOffDays objdal = new DALEmployeeWiseOffDays();



    #region 'Start Properties Declaration'


    public int EWO_Id { get; set; }
    public string Employeecode { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string Reason { get; set; }
    public bool SunOff { get; set; }
    public bool MonOff { get; set; }
    public bool TueOff { get; set; }
    public bool WedOff { get; set; }
    public bool ThuOff { get; set; }
    public bool FriOff { get; set; }
    public bool SatOff { get; set; }
    public int Status_Id { get; set; }

    public int Region_Id { get; set; }
    public int Center_Id { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }


    #endregion

    #region 'Start Executaion Methods'

    public int EmployeeWiseOffDaysAdd(BLLEmployeeWiseOffDays _obj)
        {
        return objdal.EmployeeWiseOffDaysAdd(_obj);
        }
    public int EmployeeWiseOffDaysUpdate(BLLEmployeeWiseOffDays _obj)
        {
        return objdal.EmployeeWiseOffDaysUpdate(_obj);
        }
    public int EmployeeWiseOffDaysDelete(BLLEmployeeWiseOffDays _obj)
        {
        return objdal.EmployeeWiseOffDaysDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmployeeWiseOffDaysFetch(BLLEmployeeWiseOffDays _obj)
        {
        return objdal.EmployeeWiseOffDaysSelect(_obj);
        }

    public DataTable EmployeeWiseOffDaysFetchByStatusID(BLLEmployeeWiseOffDays _obj)
    {
        return objdal.EmployeeWiseOffDaysSelectByStatusID(_obj);
    }



    public DataTable EmployeeWiseOffDaysFetch(int _id)
      {
        return objdal.EmployeeWiseOffDaysSelect(_id);
      }


    #endregion

    }
