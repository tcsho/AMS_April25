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
/// Summary description for _DALPeriod
/// </summary>
public class _DALPeriod
{
    DALBase dalobj = new DALBase();


    public _DALPeriod()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int PeriodAdd(BLLPeriod objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("PeriodInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int PeriodUpdate(BLLPeriod objbll)
    {
        SqlParameter[] param = new SqlParameter[10];

 
        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("PeriodUpdate", param);
        int k = (int)param[9].Value;
        return k;
    }
    public int PeriodDelete(BLLPeriod objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Period_Id", SqlDbType.Int);
     //   param[0].Value = objbll.Period_Id;


        int k = dalobj.sqlcmdExecute("PeriodDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable PeriodSelect(int _id)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
    param[0].Value = _id;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("PeriodSelect", param);
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
    
    public DataTable PeriodSelect(BLLPeriod objbll)
    {
    SqlParameter[] param = new SqlParameter[1];

    param[0] = new SqlParameter("@InActive", SqlDbType.NVarChar);
    param[0].Value = objbll.InActive;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("WebPeriodSelectByStatus", param);
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
    
    public DataTable PeriodSelectCurrentMonth()
        {

        DataTable _dt = new DataTable();

        try
            {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebPeriodSelectByCurrentMonth");
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

    
   public DataTable PeriodSelectMonthRangeDates(BLLPeriod objbll)
    {
    SqlParameter[] param = new SqlParameter[1];

    param[0] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
    param[0].Value = objbll.PMonth;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("WebPeriodSelectByDateRangeMonth", param);
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
    



    public int PeriodSelectField(int _Id)
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


    #endregion


}
