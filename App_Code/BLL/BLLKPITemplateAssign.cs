using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLKPITemplateAssign
/// </summary>
public class BLLKPITemplateAssign
{
    public BLLKPITemplateAssign()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    _DALKPITemplateAssign objdal = new _DALKPITemplateAssign();
    #region 'Start Properties Declaration'
    public int ID { get; set; }
    public int TemplateId { get; set; }
    public string EmpCategory { get; set; }
    public int RegionID { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    public List<BLLKPITemplateAssignDetail> KPI_TemplateAssignDetail { get; set; }
    #endregion

    public int KPITemplateAssignAdd(BLLKPITemplateAssign _obj)
    {
        return objdal.KPITemplateAssignAdd(_obj);
    }

    public DataTable FetchKeyStages()
    {
        return objdal.FetchKeyStages();
    }
    public DataTable GetClassData()
    {
        return objdal.GetClassData();
    }
}