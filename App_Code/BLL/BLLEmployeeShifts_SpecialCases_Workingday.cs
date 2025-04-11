using System;
using System.Data;

/// <summary>
/// Summary description for BLLEmployeeShifts_SpecialCases_WorkingDay
/// </summary>



public class BLLEmployeeShifts_SpecialCases_WorkingDay
    {
    public BLLEmployeeShifts_SpecialCases_WorkingDay()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    DALEmployeeShifts_SpecialCases_WorkingDay objdal = new DALEmployeeShifts_SpecialCases_WorkingDay();



    #region 'Start Properties Declaration'

    public int WorkingDay_Id { get; set; }
    public int Region_Id { get; set; }
    public int Center_Id { get; set; }
    public DateTime? WorkingDate { get; set; }
    public int Status_Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? CreatedBy { get; set; }
    public string Gender { get; set; }
    public string Remarks { get; set; }
    public bool isApplyCenters { get; set; }
    #endregion

    #region 'Start Executaion Methods'

    public int EmployeeShifts_SpecialCases_WorkingDayAdd(BLLEmployeeShifts_SpecialCases_WorkingDay _obj)
        {
            return objdal.EmployeeShifts_SpecialCases_WorkingDayAdd(_obj);
        }
    public int EmployeeShifts_SpecialCases_WorkingDayUpdate(BLLEmployeeShifts_SpecialCases_WorkingDay _obj)
        {
        return objdal.EmployeeShifts_SpecialCases_WorkingDayUpdate(_obj);
        }
    public int EmployeeShifts_SpecialCases_WorkingDayDelete(BLLEmployeeShifts_SpecialCases_WorkingDay _obj)
        {
        return objdal.EmployeeShifts_SpecialCases_WorkingDayDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmployeeShifts_SpecialCases_WorkingDaySelectAll(BLLEmployeeShifts_SpecialCases_WorkingDay _obj)
        {
            return objdal.EmployeeShifts_SpecialCases_WorkingDaySelectAll(_obj);
        }




    public DataTable EmployeeShifts_SpecialCases_WorkingDayFetch(int _id)
      {
        return objdal.EmployeeShifts_SpecialCases_WorkingDaySelect(_id);
      }


    #endregion

    }
