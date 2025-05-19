using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLKPITemplateDetail
/// </summary>
public class BLLKPITemplateDetail
{
    public BLLKPITemplateDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    _DALKPITemplateDetail objdal = new _DALKPITemplateDetail();

    #region 'Start Properties Declaration'
    //public int id { get; set; }
    public int? templateId { get; set; }
    public string kpiName { get; set; }
    public string weight { get; set; }
    public string grade5_max { get; set; }
    public string grade5_min { get; set; }
    public string grade4_max { get; set; }
    public string grade4_min { get; set; }
    public string grade3_max { get; set; }
    public string grade3_min { get; set; }
    public string grade2_max { get; set; }
    public string grade2_min { get; set; }
    public string grade1_max { get; set; }
    public string grade1_min { get; set; }
    #endregion

    public int KPITemplateDetailAdd(BLLKPITemplateDetail _obj)
    {
        return objdal.KPITemplateDetailInsert(_obj);
    }

    public DataTable KPITemplateFetchbyID(int id)
    {
        return objdal.KPITemplateFetchbyID(id);
    }
}