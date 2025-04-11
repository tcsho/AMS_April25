using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLSearchEmployee
/// </summary>
public class BLLSearchEmployee
{
    _DALSearchEmployee objdal = new _DALSearchEmployee();
    //public BLLSearchEmployee()
    //{
    ////    //
    ////    // TODO: Add constructor logic here
    ////    //
    //}

    #region 'Start Properties Declaration'

    public int SrNo { get; set; }
    public string InActive { get; set; }
    public int Region_Id { get; set; }
    public string Region_Name { get; set; }
    public int Center_Id { get; set; }
    public string Center_Name { get; set; }
    public int Dept_Id { get; set; }
    public string DeptName { get; set; }
    public int Desig_Id { get; set; }
    public string DesigName { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeGrade { get; set; }
    public string Gender_Id { get; set; }
    public string Gender { get; set; }
    public int Religion_Id { get; set; }
    public string Religion_Name { get; set; }
    public int? IsContracual { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string ExtensionNo { get; set; }





 

    #endregion


    public DataTable EmployeeprofileSelectBySearchCriteriasFetch(BLLSearchEmployee _obj)
    {
        return objdal.EmployeeSelectByCritria(_obj);
    }
}