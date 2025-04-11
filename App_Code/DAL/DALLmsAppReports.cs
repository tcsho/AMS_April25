using System;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for _DALLmsAppReports
/// </summary>
public class DALLmsAppReports
{
    DALBase dalobj = new DALBase();


    public DALLmsAppReports()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int LmsAppReportsAdd(BLLLmsAppReports objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("LmsAppReportsInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int LmsAppReportsUpdate(BLLLmsAppReports objbll)
    {
        SqlParameter[] param = new SqlParameter[10];


        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("LmsAppReportsUpdate", param);
        int k = (int)param[9].Value;
        return k;
    }
    public int LmsAppReportsDelete(BLLLmsAppReports objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@LmsAppReports_Id", SqlDbType.Int);
        //   param[0].Value = objbll.LmsAppReports_Id;


        int k = dalobj.sqlcmdExecute("LmsAppReportsDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable LmsAppReportsSelect(BLLLmsAppReports objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Page_Id", SqlDbType.Int);
        param[0].Value = objbll.Page_Id;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebLmsAppReportsSelectByPageId", param);
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
    public DataTable LmsAppReportsSelect(int Rpt_Id)
        {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Rpt_Id", SqlDbType.Int);
        param[0].Value = Rpt_Id;


        DataTable _dt = new DataTable();

        try
            {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebLmsAppReportsSelectById", param);
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
    public DataTable FetchLmsAppReportsControlsbyReportCaption(BLLLmsAppReports objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Rpt_Caption", SqlDbType.VarChar);
        param[0].Value = objbll.Rpt_Caption ;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebLmsAppReportsControlsbyReportCaption", param);
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

public DataTable FetchLmsAppReportsControlsbyRpt_Id(BLLLmsAppReports objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Rpt_Id", SqlDbType.Int);
        param[0].Value = objbll.Rpt_Id ;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebLmsAppReportsControlsbyRpt_Id", param);
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
    public DataTable LmsAppReportsSelectByStatusID(BLLLmsAppReports objbll)
    {
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebLmsAppReportsSelectByStatusID");
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


}
