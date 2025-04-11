using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLEmployeeReportToHOD
/// </summary>
public class BLLEmployeeReportToHOD
{
    _DALEmployeeReportToHOD objDAL = new _DALEmployeeReportToHOD();
    public string EmployeeCode { get; set; }

    public string PMonth { get; set; }
    public int regionId { get; set; }
    public int centerId { get; set; }


	public BLLEmployeeReportToHOD()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    public bool isEmployeeHOD(BLLEmployeeReportToHOD objbll)
    {
        if (objDAL.isEmployeeHOD(objbll).Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int MakeHODfromEmployee(BLLEmployeeReportToHOD objbll)
    {
        return objDAL.MakeHODfromEmployee(objbll);
    }
    public int MakeEmployeefromHOD(BLLEmployeeReportToHOD objbll)
    {
        return objDAL.MakeEmployeefromHOD(objbll);
    }

    public DataTable WebDepartmentSelectByMonthRegionCenter(BLLEmployeeReportToHOD objbll)
    {
        return objDAL.WebDepartmentSelectByMonthRegionCenter(objbll);
    }

}