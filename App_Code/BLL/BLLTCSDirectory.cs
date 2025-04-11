using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLTCSDirectory
/// </summary>
public class BLLTCSDirectory
{
	public BLLTCSDirectory()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    _DALTCSDirectory objdal = new _DALTCSDirectory();

    #region 'Start Properties Declaration'
    public int EmployeeCode { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string ExtensionNo { get; set; }
    public string LandlineNo { get; set; }

    #endregion

    #region 'Start Executaion Methods'
    public int TCSDirectoryInsert(BLLTCSDirectory objbll)
    {
        return objdal.TCSDirectoryInsert(objbll);
    }
    #endregion

    #region 'Start Fetch Methods'
    public bool TCSDirectoryFetchByEmployeeCode(BLLTCSDirectory obj)
    {
        return objdal.TCSDirectorySelectByEmployeeCode(obj);
    }
    #endregion
}