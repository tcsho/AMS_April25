using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLKPIEmployeeWiseDetail
/// </summary>
public class BLLKPIEmployeeWiseDetail
{
    public BLLKPIEmployeeWiseDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    _DALKPIEmployeeWiseDetail objdal = new _DALKPIEmployeeWiseDetail();
    public int AssignDetailID { get; set; }
    public int TemplateDetailID { get; set; }

    public string KPIName { get; set; }
    public string Weight { get; set; }

    public string Grade5_Max { get; set; }
    public string Grade5_Min { get; set; }

    public string Grade4_Max { get; set; }
    public string Grade4_Min { get; set; }

    public string Grade3_Max { get; set; }
    public string Grade3_Min { get; set; }

    public string Grade2_Max { get; set; }
    public string Grade2_Min { get; set; }

    public string Grade1_Max { get; set; }
    public string Grade1_Min { get; set; }

    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public int KPITemplateDetailAdd(BLLKPIEmployeeWiseDetail _obj)
    {
        return objdal.KPITemplateDetailInsert(_obj);
    }

    public int KPITemplateAssignDetailUpdate(BLLKPIEmployeeWiseDetail _obj)
    {
        return objdal.KPITemplateAssignDetailUpdate(_obj);
    }
    public DataTable KPITemplateFetchbyID(int id)
    {
        return objdal.KPITemplateFetchbyID(id);
    }
}