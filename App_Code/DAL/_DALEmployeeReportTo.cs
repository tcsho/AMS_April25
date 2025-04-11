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

/// <summary>
/// Summary description for _DALEmployeeReportTo
/// </summary>
public class _DALEmployeeReportTo
{
    DALBase dalobj = new DALBase();

    public _DALEmployeeReportTo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable EmployeeReportToSelectAll()
    {

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeReportToSelectAll");
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }

    public int EmployeeReportToInsert(BLLEmployeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[5];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@ReportTo", SqlDbType.NVarChar); param[1].Value = objbll.ReportTo;
        //param[2] = new SqlParameter("@Region_Id", SqlDbType.Int); param[2].Value = objbll.Region_Id;
        //param[3] = new SqlParameter("@Center_Id", SqlDbType.Int); param[3].Value = objbll.Center_Id;
        param[2] = new SqlParameter("@HODEmail", SqlDbType.NVarChar); param[2].Value = objbll.HODEmail;
        param[3] = new SqlParameter("@isEmail", SqlDbType.Bit); param[3].Value = objbll.IsEmail;
        param[4] = new SqlParameter("@Status_Id", SqlDbType.Int); param[4].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeReportToInsert", param);
        int k = (int)param[4].Value;
        return k;

    }

    public int EmployeeReportToUpdate(BLLEmployeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[6];

        param[0] = new SqlParameter("@tid", SqlDbType.Int); param[0].Value = objbll.Tid;
        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[1].Value = objbll.EmployeeCode;
        param[2] = new SqlParameter("@ReportTo", SqlDbType.NVarChar); param[2].Value = objbll.ReportTo;
        param[3] = new SqlParameter("@status_id", SqlDbType.Int); param[3].Value = objbll.Status_id;
        param[4] = new SqlParameter("@HODEmail", SqlDbType.NVarChar); param[4].Value = objbll.HODEmail;
        param[5] = new SqlParameter("@isEmail", SqlDbType.Bit); param[5].Value = objbll.IsEmail;

        int k = dalobj.sqlcmdExecute("EmployeeReportToUpdate", param);

        return k;

    }

    

    public int EmployeeReportToDelete(BLLEmployeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@tid", SqlDbType.Int); param[0].Value = objbll.Tid;
        /*param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[1].Value = objbll.EmployeeCode;
        param[2] = new SqlParameter("@ReportTo", SqlDbType.NVarChar); param[2].Value = objbll.ReportTo;
        param[3] = new SqlParameter("@status_id", SqlDbType.Int); param[3].Value = objbll.Status_id;
        param[4] = new SqlParameter("@Active", SqlDbType.Bit); param[4].Value = objbll.Active;*/

        int k = dalobj.sqlcmdExecute("EmployeeReportToDelete", param);

        return k;
    }

    public DataTable EmployeeReportToSelectByEmployeeCode(BLLEmployeeReportTo objbll)
    {

        DataTable _dt = new DataTable();


        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[0].Value = objbll.EmployeeCode;


        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeReportToSelectByEmployeeCode", param);
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }

    public DataTable UserSelectByUserTypeID(BLLEmployeeReportTo objbll)
    {

        DataTable _dt = new DataTable();

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@User_Type_ID", SqlDbType.Int); param[0].Value = objbll.User_Type_Id;

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("UserSelectByUserTypeID", param);
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }

    public DataTable UserHODSelectByUserTypeIDRegionCenter(BLLEmployeeReportTo objbll)
    {

        DataTable _dt = new DataTable();

        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.Region_Id;

        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = objbll.Center_Id;

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("UserHODSelectByUserTypeIDRegionCenter", param);
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }


    

    public DataTable EmployeeReportToHODSelectByEmployeeCode(BLLEmployeeReportTo objbll)
    {

        DataTable _dt = new DataTable();


        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[0].Value = objbll.EmployeeCode;

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeReportToHODSelectByEmployeeCode", param);
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }
}
