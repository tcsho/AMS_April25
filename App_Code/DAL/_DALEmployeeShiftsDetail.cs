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
/// Summary description for _DALEmployeeShiftsDetail
/// </summary>
public class _DALEmployeeShiftsDetail
{
    DALBase dalobj = new DALBase();

    public _DALEmployeeShiftsDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable EmployeeShiftsDetailSelectAll()
    {
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeShiftsDetailSelectAll");
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

    public int EmployeeShiftsDetailUpdate(BLLEmployeeShiftsDetail objbll)
    {
        SqlParameter[] param = new SqlParameter[6];

        param[0] = new SqlParameter("@ActSTime", SqlDbType.NChar); param[0].Value = objbll.ActSTime;
        param[1] = new SqlParameter("@ActETime", SqlDbType.NChar); param[1].Value = objbll.ActETime;
        param[2] = new SqlParameter("@AbsentTime", SqlDbType.NChar); param[2].Value = objbll.AbsentTime;
        param[3] = new SqlParameter("@EmployeeCode", SqlDbType.NChar); param[3].Value = objbll.EmployeeCode;
        param[4] = new SqlParameter("@PMonth", SqlDbType.NChar); param[4].Value = objbll.PMonth;
        param[5] = new SqlParameter("@AttDate", SqlDbType.DateTime); param[5].Value = objbll.AttDate;

         

        int k = dalobj.sqlcmdExecute("EmployeeShiftsDetailUpdate", param);

        return k;

    }

    public DataTable EmployeeShiftsDetailSelectByEmpAndMonth(BLLEmployeeShiftsDetail objbll)
    {

        DataTable _dt = new DataTable();


        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NChar); param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@PMOnth", SqlDbType.NChar); param[1].Value = objbll.PMonth;


        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeShiftsDetailSelectByEmpAndMonth", param);
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

    public int SingleEmployee_CompleteProcess(BLLEmployeeShiftsDetail objbll)
    {
        SqlParameter[] param = new SqlParameter[2];
        
        param[0] = new SqlParameter("@PMonth", SqlDbType.NChar); param[0].Value = objbll.PMonth;
        param[1] = new SqlParameter("@Employee_Code", SqlDbType.NChar); param[1].Value = objbll.EmployeeCode;

        int k = dalobj.sqlcmdExecute("SingleEmployee_CompleteProcess", param);

        return k;

    }


    

    public DataTable isLeaveDeduction(BLLEmployeeShiftsDetail objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Employee_Code", SqlDbType.NChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@Att_Date", SqlDbType.NChar);
        param[1].Value = objbll.AttDate;

        DataTable _dt = dalobj.sqlcmdFetch("sp_isLeaveDeuction", param);

        return _dt;
    }


    public int ResetLeave(BLLEmployeeShiftsDetail objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NChar);
        param[0].Value = objbll.EmployeeCode;


        param[1] = new SqlParameter("@PDate", SqlDbType.DateTime);
        param[1].Value = objbll.AttDate;


        param[2] = new SqlParameter("@ProcessType", SqlDbType.NChar);
        param[2].Value = 'C';

        int k = dalobj.sqlcmdExecute("_UpdateEmpLeaveReset", param);

        return k;

    }

}
