using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLNetworkRegion
/// </summary>
public class BLLNetworkRegion
{
	public BLLNetworkRegion()
	{
		//
		// TODO: Add constructor logic here
		//
	}



  

        _DALNetworkRegion objdal = new _DALNetworkRegion();
      

        #region 'Start Properties Declaration'

        public int NetworkRegion_Id { get; set; }

        public string NetworkName { get; set; }

        public int Region_Id { get; set; }


        #endregion

        #region 'Start Executaion Methods'

        public int NetworkRegionAdd(BLLNetworkRegion _obj)
        {
            return objdal.NetworkRegionAdd(_obj);
        }
        public int NetworkRegionUpdate(BLLNetworkRegion _obj)
        {
            return objdal.NetworkRegionUpdate(_obj);
        }
        public int NetworkRegionDelete(BLLNetworkRegion _obj)
        {
            return objdal.NetworkRegionDelete(_obj);

        }

        #endregion
        #region 'Start Fetch Methods'


        public DataTable NetworkRegionFetch(BLLNetworkRegion _obj)
        {
            return objdal.NetworkRegionSelect(_obj);
        }

        public DataTable NetworkRegionFetchByStatusID(BLLNetworkRegion _obj)
        {
            return objdal.NetworkRegionSelectByStatusID(_obj);
        }



        public DataTable NetworkRegionFetchByRegionID(int _id)
        {
            return objdal.NetworkRegionSelectRegionByID(_id);
        }

        public DataTable NetworkRegionFetchByID(int _id)
        {
            return objdal.NetworkRegionSelectByID(_id);
        }

        public DataTable NetworkCenterSelectByRegionID(int _id)
        {
            return objdal.NetworkCenterSelectByRegionID(_id);
        }

   

        #endregion

    

}