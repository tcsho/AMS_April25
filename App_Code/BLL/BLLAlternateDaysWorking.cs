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
public class BLLAlternateDaysWorking
{
    _DALAlternateDaysWorking objDAL = new _DALAlternateDaysWorking();

    public int VacationTimings_id { get; set; }
    public int Region_id { get; set; }
    public int Center_id { get; set; }
    public DateTime Off_day { get; set; }
    public DateTime Alternate_working_day { get; set; }
    public string Reason { get; set; }
    
    public string Inserted_by { get; set; }
    public DateTime Inserted_Date { get; set; }
    public string Last_updated_by { get; set; }
    public DateTime Last_updated_date { get; set; }
    public string PMonth { get; set; }








    public BLLAlternateDaysWorking()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable fetchRegions()
    {
        return objDAL.fetchRegions();
    }

    public DataTable fetchCenters(BLLAlternateDaysWorking objBll)
    {
        return objDAL.fetchCenters(objBll);
    }

    public int AlternateDaysWorkingInsert(BLLAlternateDaysWorking objbll)
    {
        return objDAL.AlternateDaysWorkingInsert(objbll);
    }

    public DataTable fetchAlternateDaysWorking(BLLAlternateDaysWorking objbll)
    {
        return objDAL.fetchAlternateDaysWorking(objbll);
    }







}