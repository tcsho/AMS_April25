using System;
using System.Collections.Generic;
using System.Data;
//using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLKPITemplate
/// </summary>
public class BLLKPITemplate
{
    public BLLKPITemplate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    _DALKPITemplate objdal = new _DALKPITemplate();

    #region 'Start Properties Declaration'
    
    public int TemplateId { get; set; }
    public string TemplateName { get; set; }
    public int Year { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int TotalWeight { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public List<BLLKPITemplateDetail> KPI_TemplateDetail { get; set; }
    public int templateId { get { return TemplateId; } set { TemplateId = value; } }
    public string templateName { get { return TemplateName; } set { TemplateName = value; } }

    public int year { get { return Year; } set { Year = value; } }
    public DateTime fromdate { get { return FromDate; } set { FromDate = value; } }
    public DateTime todate { get { return ToDate; } set { ToDate = value; } }
    public int totalweight { get { return TotalWeight; } set { TotalWeight = value; } }
    public string createdby { get { return CreatedBy; } set { CreatedBy = value; } }
    public DateTime createddate { get { return CreatedDate; } set { CreatedDate = value; } }

    
    #endregion

    public int KPITemplateAdd(BLLKPITemplate _obj)
    {
        return objdal.KPITemplateAdd(_obj);
    }

    public int KPITemplateDelete(BLLKPITemplate _obj, string delete)
    {
        return objdal.KPITemplateDelete(_obj, delete);
    }

    public DataTable KPITemplateFetch(BLLKPITemplate _obj)
    {
        return objdal.KPITemplateSelectAll();
    }

    public DataTable KPITemplateFetchbyID(int id)
    {
        return objdal.KPITemplateFetchbyID(id);
    }

    public int KPITemplateDetailDelete(BLLKPITemplate _obj)
    {
        return objdal.KPITemplateDetailDelete(_obj);
    }
    public DataTable KPIEmployeeFetchbyCatRegion(string category,string region)
    {
        return objdal.KPITemplateFetchbyCatRegion(category,region);
    }
    public DataTable KPIEmployeeFetchbyEmployeeCode(int EmpID)
    {
        return objdal.KPITemplateFetchbyEmpCode(EmpID);
    }
    public DataTable KPIEmployeeTemplateFetchbyEmployeeCode(int EmpID)
    {
        return objdal.KPIEmpTemplateFetchbyEmpCode(EmpID);
    }
}