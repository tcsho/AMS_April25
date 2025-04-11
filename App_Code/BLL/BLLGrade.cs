using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLGrade
/// </summary>
public class BLLGrade
{
   _DALGrade  objdal = new _DALGrade();
	public BLLGrade()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    #region Declare Properties

    public string EmpGrade { get; set; }
    
    public string InActive { get; set; }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable GradeSelectBySearchCriteriaFetch(BLLGrade _obj)
    {
        return objdal.GradeSelectBySearchCriteria(_obj);
    }




    #endregion
}