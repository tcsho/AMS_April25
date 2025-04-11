using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLSpecialCasesTimigs
/// </summary>
public class BLLSpecialCasesTimigs
{
	public BLLSpecialCasesTimigs()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    _DALSpecialCasesTimings objDAL = new _DALSpecialCasesTimings();

    public int SpecialCases_id { get; set; }
    public int Region_id { get; set; }
    public int Center_id { get; set; }
    public DateTime From_date { get; set; }
    public DateTime To_date { get; set; }
    public string Emp_Code { get; set; }
    public string Reason { get; set; }
    public string  Time_in{ get; set; }
    public string  Absent_Time { get; set; }
    public string  Time_out { get; set; }

    public int ? Margin { get; set; }

    public string Fri_Time_in { get; set; }
    public string Fri_Absent_Time { get; set; }
    public string Fri_Time_out { get; set; }
    public string first_half_end { get; set; }
    public string second_half_start { get; set; }
    public string SaT_Time_in { get; set; }
    public string SaT_Absent_Time { get; set; }
    public string SaT_Time_out { get; set; }
    public bool Saturday { get; set; }
    public string Inserted_by { get; set; }
    public DateTime Inserted_Date { get; set; }
    public string Last_updated_by { get; set; }
    public DateTime Last_updated_date { get; set; }
    public string PMonth { get; set; }
    public bool Sunday { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public int SpecialCase_Type { get; set; }
    public bool IsSpecificDays { get; set; }

    public DataTable fetchRegions()
    {
        return objDAL.fetchRegions();
    }

    public DataTable fetchCenters(BLLSpecialCasesTimigs objBll)
    {
        return objDAL.fetchCenters(objBll);
    }

    public int SpecialCasesTimingsInsert(BLLSpecialCasesTimigs objbll)
    {
        return objDAL.SpecialCasesTimingsInsert(objbll);
    }
    public int SpecialCasesTimingsDelete(BLLSpecialCasesTimigs objbll)
    {
        return objDAL.SpecialCasesTimingsDelete(objbll);
    }
    public DataTable fetchSpecialCasesRegionCenter(BLLSpecialCasesTimigs objbll)
    {
        return objDAL.fetchSpecialCasesRegionCenter(objbll);
    }
    public DataTable SpecialCase_TypeSelectAll( )
    {
        return objDAL.SpecialCase_TypeSelectAll();
    }
    public DataTable EmployeeShifts_SpecialCasesSelectDetail(int id)
    {
        return objDAL.EmployeeShifts_SpecialCasesSelectDetail(id);
    }
}