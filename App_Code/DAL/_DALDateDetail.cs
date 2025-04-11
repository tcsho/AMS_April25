using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _DALDateDetail
/// </summary>
public class _DALDateDetail
{
    DALBase dalobj = new DALBase();
    public _DALDateDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable DateDetailSelectAll(int shiftCaseId)
    {

        DataTable dt = new DataTable();

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ShiftCaseId", SqlDbType.Int);
            param[0].Value = shiftCaseId;

            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("DateDetailTiming_SelectAll", param);
            return dt;
        }
        catch (Exception _exception)
        {
            throw _exception;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }
    public DataTable DateDetailSelect(int shiftCaseDateId)
    {

        DataTable dt = new DataTable();

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ShiftCaseDateId", SqlDbType.Int);
            param[0].Value = shiftCaseDateId;

            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("DateDetailTiming_Select", param);
            return dt;
        }
        catch (Exception _exception)
        {
            throw _exception;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }

    public int DateDetailTimingsUpdate(BLLDateDetail objDateDetail)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@ShiftCaseDateId", SqlDbType.Int);
            param[0].Value = objDateDetail.ShiftCaseDateId;

            param[1] = new SqlParameter("@TimeIn", SqlDbType.Time);
            param[1].Value = objDateDetail.TimeIn;

            param[2] = new SqlParameter("@TimeOut", SqlDbType.Time);
            param[2].Value = objDateDetail.TimeOut;

            param[3] = new SqlParameter("@AbsentTime", SqlDbType.Time);
            param[3].Value = objDateDetail.AbsentTime;

            dalobj.OpenConnection();
            int k= dalobj.sqlcmdExecute("DateDetailTiming_Update", param);
            return k;
        }
        catch (Exception _exception)
        {
            throw _exception;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }
}