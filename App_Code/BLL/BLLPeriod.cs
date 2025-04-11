using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for BLLPeriod
/// </summary>



public class BLLPeriod
    {
    public BLLPeriod()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    _DALPeriod objdal = new _DALPeriod();



    #region 'Start Properties Declaration'

    private DateTime pDate;
    private string pMonth;
    private string pMonthDesc;
    private string inactive;

    public DateTime PDate { get { return pDate; } set { pDate = value; } }
    public string PMonth { get { return pMonth; } set { pMonth = value; } }
    public string PMonthDesc { get { return pMonthDesc; } set { pMonthDesc = value; } }
    public string InActive { get { return inactive; } set { inactive = value; } }


    #endregion

    #region 'Start Executaion Methods'

    public int PeriodAdd(BLLPeriod _obj)
        {
        return objdal.PeriodAdd(_obj);
        }
    public int PeriodUpdate(BLLPeriod _obj)
        {
        return objdal.PeriodUpdate(_obj);
        }
    public int PeriodDelete(BLLPeriod _obj)
        {
        return objdal.PeriodDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable PeriodFetch(BLLPeriod _obj)
        {
        return objdal.PeriodSelect(_obj);
        }

    public DataTable PeriodFetchMonthRangeDates(BLLPeriod _obj)
        {
        return objdal.PeriodSelectMonthRangeDates(_obj);
        }

    public DataTable PeriodFetchCurrentMonth()
        {
        return objdal.PeriodSelectCurrentMonth();
        }

    public DataTable PeriodFetch(int _id)
      {
        return objdal.PeriodSelect(_id);
      }
    public int PeriodFetchField(int _Id)
        {
        return objdal.PeriodSelectField(_Id);
        }


    #endregion

    }
