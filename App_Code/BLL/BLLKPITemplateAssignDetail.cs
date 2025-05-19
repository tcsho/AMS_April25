using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLKPITemplateAssignDetail
/// </summary>
public class BLLKPITemplateAssignDetail
{
    public BLLKPITemplateAssignDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    _DALKPITemplateAssignDetail objdal = new _DALKPITemplateAssignDetail();
    #region 'Start Properties Declaration'
    public int ID { get; set; }
    public int AssignMasterID { get; set; }
    public string EmployeeID { get; set; }
    public string AssignCenters { get; set; }
    public string AssignSIQAKS { get; set; }
    public string AssignClass { get; set; }
    public bool IsAssigned { get; set; }
    public DateTime AssignedDate { get; set; }
    public string RemarksHR { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    #endregion

    public int KPITemplateAssignDetailAddWithReturn(BLLKPITemplateAssignDetail _obj)
    {
        return objdal.KPITemplateAssignDetailInsert(_obj);
    }
    public int KPITemplateAssignDetailUpdate(BLLKPITemplateAssignDetail _obj)
    {
        return objdal.KPITemplateAssignDetailUpdate(_obj);
    }
    public int KPITemplateAssignDetailDelete(BLLKPITemplateAssignDetail _obj)
    {
        return objdal.KPITemplateAssignDetailDelete(_obj);
    }
}