using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLReligion
/// </summary>
public class BLLReligion
{
    DALReligion objdal = new DALReligion();
	public BLLReligion()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    #region 'Start Properties Declaration'

    public int Religion_Id { get; set; }
    public string Religion_Name { get; set; }
    public string InActive { get; set; }

    #endregion



    #region 'Start Fetch Methods'


    public DataTable ReligionSelectBySearchCriteriaFetch(BLLReligion _obj)
    {
        return objdal.ReligionSelectBySearchCriteria(_obj);
    }

   


    #endregion

}