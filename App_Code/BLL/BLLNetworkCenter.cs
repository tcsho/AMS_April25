using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLNetworkCenter
/// </summary>
public class BLLNetworkCenter
{
	public BLLNetworkCenter()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    _DALNetworkCenter objdal = new _DALNetworkCenter();



    #region 'Start Properties Declaration'

    public int NetworkCenter_Id { get; set; }
    public int Center_Id { get; set; }
    public int NetworkRegion_Id { get; set; }


    #endregion

    #region 'Start Executaion Methods'

    public int NetworkCenterAdd(BLLNetworkCenter _obj)
    {
        return objdal.NetworkCenterAdd(_obj);
    }
    public int NetworkCenterUpdate(BLLNetworkCenter _obj)
    {
        return objdal.NetworkCenterUpdate(_obj);
    }
    public int NetworkCenterDelete(BLLNetworkCenter _obj)
    {
        return objdal.NetworkCenterDelete(_obj);

    }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable NetworkCenterFetch(BLLNetworkCenter _obj)
    {
        return objdal.NetworkCenterSelect(_obj);
    }

    public DataTable NetworkCenterFetchByStatusID(BLLNetworkCenter _obj)
    {
        return objdal.NetworkCenterSelectByStatusID(_obj);
    }



    public DataTable NetworkCenterFetch(int _id)
    {
        return objdal.NetworkCenterSelect(_id);
    }

    public DataTable NetworkCenterSelectByNetworkHOD(int _id)
    {
        return objdal.NetworkCenterSelectByNetworkHOD(_id);
    }


    #endregion
}