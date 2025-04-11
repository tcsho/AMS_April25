using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLDateDetail
/// </summary>
public class BLLDateDetail
{
    public BLLDateDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    _DALDateDetail objdal = new _DALDateDetail();

    #region 'Start Properties Declaration'
    public int ShiftCaseDateId { get; set; }
    public int ShiftCaseId { get; set; }
    public DateTime AttDate { get; set; }
    public TimeSpan TimeIn { get; set; }
    public TimeSpan TimeOut { get; set; }
    public TimeSpan AbsentTime { get; set; }
    public bool isOff { get; set; }
    public int Margin { get; set; }
    #endregion

    #region 'Start Fetch Methods'


    public DataTable DetailTimingsFetchAll(int shiftCaseId)
    {
        return objdal.DateDetailSelectAll(shiftCaseId);
    }

    #endregion


    #region 'Start Update Methods'


    public int DateDetailTimingsUpdate()
    {
        return objdal.DateDetailTimingsUpdate(this);
    }

    #endregion

}