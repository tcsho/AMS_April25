using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLNetworkTeam
/// </summary>
public class BLLNetworkTeam
{
    _DALNetworkTeam objdal = new _DALNetworkTeam();

	public BLLNetworkTeam()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region 'Start Properties Declaration'

    public int NetworkTeam_Id { get; set; }
    public int NetworkRegion_Id { get; set; }
    public string EmployeeCode { get; set; }
    public bool IsHOD { get; set; }
    public bool InSchool { get; set; }


    #endregion

    #region 'Start Executaion Methods'

    public int NetworkTeamAdd(BLLNetworkTeam _obj)
    {
        return objdal.NetworkTeamAdd(_obj);
    }
    //public int NetworkTeamUpdate(BLLNetworkTeam _obj)
    //{
    //    return objdal.NetworkTeamUpdate(_obj);
    //}
    public int NetworkTeamDelete(BLLNetworkTeam _obj)
    {
        return objdal.NetworkTeamDelete(_obj);

    }
    public int NetworkEmployeesinSchool_Insert(BLLNetworkTeam _obj)
    {
        return objdal.NetworkEmployeesinSchool_Insert(_obj);

    }
    
    #endregion
    #region 'Start Fetch Methods'


    public DataTable NetworkTeamFetch(BLLNetworkTeam _obj)
    {
        return objdal.NetworkTeamSelect(_obj);
    }

    public DataTable NetworkTeamFetchByStatusID(BLLNetworkTeam _obj)
    {
        return objdal.NetworkTeamSelectByStatusID(_obj);
    }
    public DataTable NetworkTeamSelectByLocation(BLLNetworkTeam _obj)
    {
        return objdal.NetworkTeamSelectByLocation(_obj);
    }


    public DataTable NetworkTeamFetch(int _id)
    {
        return objdal.NetworkTeamSelect(_id);
    }

    public DataTable NetworkTeamByRegion(BLLEmplyeeReportTo _obj)
    {
        return objdal.NetworkTeamByRegion(_obj);
    }


    #endregion
}