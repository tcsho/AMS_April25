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
/// Summary description for BLLVacationTimigs
/// </summary>
public class BLLVacationTimigs
{
    _DALVacationTimings objDAL = new _DALVacationTimings();

    public int VacationTimings_id { get; set; }
    public int Region_id { get; set; }
    public int Center_id { get; set; }
    public string strCenter_id { get; set; }
    public DateTime From_date { get; set; }
    public DateTime To_date { get; set; }
    public string strFrom_date { get; set; }
    public string strTo_date { get; set; }
    public string Reason { get; set; }
    public string Time_in { get; set; }
    public string Absent_Time { get; set; }
    public string Time_out { get; set; }
    public string Inserted_by { get; set; }
    public DateTime Inserted_Date { get; set; }
    public string Last_updated_by { get; set; }
    public DateTime Last_updated_date { get; set; }
    public string PMonth { get; set; }
    public bool IsOffteacher { get; set; }







    public BLLVacationTimigs()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable fetchRegions()
    {
        return objDAL.fetchRegions();
    }

    public DataTable fetchCenters(BLLVacationTimigs objBll)
    {
        return objDAL.fetchCenters(objBll);
    }

    public int VacationTimingsInsert(BLLVacationTimigs objbll)
    {
        return objDAL.VacationTimingsInsert(objbll);
    }
    public int VacationTimingsUpdate(BLLVacationTimigs objbll)
    {
        return objDAL.UpdateVacationTimings(objbll);
    }
    public int DeleteVacationTiming(int id)
    {
        return objDAL.DeleteVacationTiming(id);
    }
    public int VacationTimingDeleteRegionWise(BLLVacationTimigs obj)
    {
       return objDAL.VacationTimingDeleteRegionWise(obj);
    }
    
    public DataTable fetchVacationsRegionCenter(BLLVacationTimigs objbll)
    {
        return objDAL.fetchVacationsRegionCenter(objbll);
    }
    
    public DataTable VacationTimingFillDate(BLLVacationTimigs objbll)
    {
        return objDAL.VacationTimingFillDate(objbll);
    }






}