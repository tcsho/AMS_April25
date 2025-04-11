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
/// Summary description for BLLCalendar
/// </summary>



public class BLLCalendar
{
    public BLLCalendar()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALCalendar objdal = new _DALCalendar();



    #region 'Start Properties Declaration'

    private int calId;
    private string calenderDate;
    private string description;
    private int region_Id;
    private int center_Id;
    private string gender;
    private string pMonth;
    public string centerstring { get; set; }
    public int CalId { get { return calId; } set { calId = value; } }
    public string CalenderDate { get { return calenderDate; } set { calenderDate = value; } }
    public string Description { get { return description; } set { description = value; } }
    public int Region_Id { get { return region_Id; } set { region_Id = value; } }
    public int Center_Id { get { return center_Id; } set { center_Id = value; } }
    public string Gender { get { return gender; } set { gender = value; } }
    public string PMonth { get { return pMonth; } set { pMonth = value; } }


    #endregion

    #region 'Start Executaion Methods'

    public int CalendarAdd(BLLCalendar _obj)
    {
        return objdal.CalendarAdd(_obj);
    }
    public int CalendarUpdate(BLLCalendar _obj)
    {
        return objdal.CalendarUpdate(_obj);
    }
    public int CalendarDelete(BLLCalendar _obj)
    {
        return objdal.CalendarDelete(_obj);

    }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable CalendarFetch(BLLCalendar _obj)
    {
        return objdal.CalendarSelect(_obj);
    }

    public DataTable CalendarFetch(int _id)
    {
        return objdal.CalendarSelect(_id);
    }
    public int CalendarFetchField(int _Id)
    {
        return objdal.CalendarSelectField(_Id);
    }
    public int CalendarAlreadyExistInRange(DateTime fromDate, DateTime toDate)
    {
        return objdal.CalendarAlreadyExistInRange(fromDate, toDate);
    }
    public DataTable CalendarFetchAllDetails(BLLCalendar _obj)
    {
        return objdal.CalendarSelectAllDetails(_obj);
    }

    #endregion

}
