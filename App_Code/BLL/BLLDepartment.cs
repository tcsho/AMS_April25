using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLDepartment
/// </summary>
public class BLLDepartment
{
    _DALDepartment objdal = new _DALDepartment();

	public BLLDepartment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Declare Properties
    public int DeptCode { get; set; }

    public string DeptName { get; set; }
    public string InActive { get; set; }

    #endregion


    #region 'Start Fetch Methods'


    public DataTable DepartmentSelectBySearchCriteriaFetch(BLLDepartment _obj)
    {
        return objdal.DepartmentSelectBySearchCriteria(_obj);
    }




    #endregion
}