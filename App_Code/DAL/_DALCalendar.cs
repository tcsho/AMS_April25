using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// Summary description for _DALCalendar
/// </summary>
public class _DALCalendar
{
    DALBase dalobj = new DALBase();


    public _DALCalendar()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int CalendarAdd(BLLCalendar objbll)
    {
        SqlParameter[] param = new SqlParameter[6];



        param[0] = new SqlParameter("@CalenderDate", SqlDbType.NVarChar);
        param[0].Value = objbll.CalenderDate;

        param[1] = new SqlParameter("@Description", SqlDbType.NVarChar);
        param[1].Value = objbll.Description;


        param[2] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[2].Value = objbll.Region_Id;


        param[3] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[3].Value = objbll.Center_Id;

        param[4] = new SqlParameter("@Center_list", SqlDbType.NChar);
        param[4].Value = objbll.centerstring;

        param[5] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[5].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("WebCalendarINSERT", param);
        int k = (int)param[5].Value;
        return k;

    }
    public int CalendarUpdate(BLLCalendar objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@CalId", SqlDbType.Int);
        param[0].Value = objbll.CalId;

        param[1] = new SqlParameter("@Description", SqlDbType.NVarChar);
        param[1].Value = objbll.Description;

        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("WebCalendarUpdate", param);
        int k = (int)param[2].Value;
        return k;

    }
    public int CalendarDelete(BLLCalendar objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@CalId", SqlDbType.Int);
        param[0].Value = objbll.CalId;


        int k = dalobj.sqlcmdExecute("WebCalendarDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable CalendarSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@CalId", SqlDbType.Int);
        param[0].Value = _id;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebCalendarSelectById", param);
            return _dt;
        }
        catch (Exception _exception)
        {
            throw _exception;
        }
        finally
        {
            dalobj.CloseConnection();
        }

        return _dt;
    }

    public DataTable CalendarSelect(BLLCalendar objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.NVarChar);
        param[0].Value = objbll.Region_Id;

        param[1] = new SqlParameter("@Center_Id", SqlDbType.NVarChar);
        param[1].Value = objbll.Center_Id;

        param[2] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[2].Value = objbll.PMonth;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebCalendarSelect", param);
            return _dt;
        }
        catch (Exception _exception)
        {
            throw _exception;
        }
        finally
        {
            dalobj.CloseConnection();
        }

        return _dt;

    }


    public int CalendarSelectField(int _Id)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Section_Id", SqlDbType.Int);
        param[0].Value = _Id;

        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("", param);
        int k = (int)param[1].Value;
        return k;

    }


    public DataTable CalendarSelectAllDetails(BLLCalendar obj)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = obj.Region_Id;
        param[1] = new SqlParameter("@Pmonth", SqlDbType.NVarChar);
        param[1].Value = obj.PMonth;
        DataTable _dt = new DataTable();
        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebCalenderSelectAll", param);
            return _dt;
        }
        catch (Exception _exception)
        {
            throw _exception;
        }
        finally
        {
            dalobj.CloseConnection();
        }

        return _dt;
    }


    #endregion



    public int CalendarAlreadyExistInRange(DateTime fromDate, DateTime toDate)
    {

        SqlParameter[] param = new SqlParameter[5];



        //param[0] = new SqlParameter("@CalenderFromDate", SqlDbType.DateTime);
        //param[0].Value = fromDate;

        //param[1] = new SqlParameter("@CalenderToDate", SqlDbType.DateTime);
        //param[1].Value = toDate;


        //param[2] = new SqlParameter("@Region_Id", SqlDbType.Int);
        //param[2].Value = objbll.Region_Id;


        //param[3] = new SqlParameter("@Center_Id", SqlDbType.Int);
        //param[3].Value = objbll.Center_Id;


        //param[4] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        //param[4].Direction = ParameterDirection.Output;

        //dalobj.sqlcmdExecute("CalendarAlreadyExistInRange", param);
        //int k = (int)param[4].Value;
        //return k;
        return 0;
    }

}
