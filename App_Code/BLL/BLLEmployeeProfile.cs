using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLEmployeeProfile
/// </summary>
public class BLLEmployeeProfile
{
    _DALEmployeeProfile objdal = new _DALEmployeeProfile();
	public BLLEmployeeProfile()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    public string EmployeeCode { get; set; }

    #region 'Start Fetch Methods'
  
    public DataTable EmployeeProfileFetchByCode(BLLEmployeeProfile obj)
    {
        return objdal.EmployeeProfileSelectByCode(obj);
    }


    #endregion
}