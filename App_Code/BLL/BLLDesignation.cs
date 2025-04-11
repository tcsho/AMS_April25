using System;
using System.Data;

/// <summary>
/// Summary description for BLLDesignation
/// </summary>



public class BLLDesignation
    {
    public BLLDesignation()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    DALDesignation objdal = new DALDesignation();



    #region 'Start Properties Declaration'
    public int DesigCode { get; set; }
    public string DesigName { get; set; }
    public string InActive { get; set; }


    #endregion

    #region 'Start Executaion Methods'

    public int DesignationAdd(BLLDesignation _obj)
        {
        return objdal.DesignationAdd(_obj);
        }
    public int DesignationUpdate(BLLDesignation _obj)
        {
        return objdal.DesignationUpdate(_obj);
        }
    public int DesignationDelete(BLLDesignation _obj)
        {
        return objdal.DesignationDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable DesignationFetch(BLLDesignation _obj)
        {
        return objdal.DesignationSelect(_obj);
        }

    public DataTable DesignationFetchByStatusID(BLLDesignation _obj)
    {
        return objdal.DesignationSelectByStatusID(_obj);
    }


    
    public DataTable DesignationFetch(int _id)
      {
        return objdal.DesignationSelect(_id);
      }

    public DataTable DesignationSelectBySearchCriteriaFetch(BLLDesignation _obj)
        {
            return objdal.DesignationSelectBySearchCriteria(_obj);
        }

    #endregion

    }
