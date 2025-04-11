using System;
using System.Data;

/// <summary>
/// Summary description for BLLRamadanTiming
/// </summary>



public class BLLRamadanTiming
{
    public BLLRamadanTiming()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    DALRamadanTiming objdal = new DALRamadanTiming();



    #region 'Start Properties Declaration'

    public int RT_Id { get; set; }
    public int Region_ID { get; set; }
    public int Center_ID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Remarks { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int Status_ID { get; set; }
    public string FridayStartTime { get; set; }
    public string FridayEndTime { get; set; }
    public string SaturdayStartTime { get; set; }
    public string SaturdayEndTime { get; set; }
    public string TeacherFridayStartTime { get; set; }
    public string TeacherFridayEndTime { get; set; }
    public string AbsentTime { get; set; }
    public string TeacherStartTime { get; set; }
    public string TeacherEndTime { get; set; }
    public string NOStart_Time { get; set; }
    public string NOEnd_Time { get; set; }
    public string NOFridaySTime { get; set; }
    public string NOFridayETime { get; set; }
    public string NOAbsentTime { get; set; }
    public string TeacherAbsentTime { get; set; }
    public string Month { get; set; }

    #endregion

    #region 'Start Executaion Methods'

    public int RamadanTimingAdd(BLLRamadanTiming _obj)
    {
        return objdal.RamadanTimingAdd(_obj);
    }
     
    public int RamadanTimingDelete(BLLRamadanTiming _obj)
    {
        return objdal.RamadanTimingDelete(_obj);

    }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable RamadanTimingFetch(BLLRamadanTiming _obj)
    {
        return objdal.RamadanTimingSelect(_obj);
    }

    #endregion

}
