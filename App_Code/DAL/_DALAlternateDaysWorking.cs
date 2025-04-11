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
/// Summary description for _DALVacationTimings
/// </summary>
public class _DALAlternateDaysWorking
{
    public _DALAlternateDaysWorking()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    DALBase dalobj = new DALBase();



    public DataTable fetchRegions()
    {
        DataTable _dt = new DataTable();

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@pv_moc_id", SqlDbType.Int); 
            param[0].Value = 1;

            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("GetRegionFromCountry", param);
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
    public DataTable fetchCenters(BLLAlternateDaysWorking objBll)
    {
        DataTable _dt = new DataTable();

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@pv_region_id", SqlDbType.Int);
            param[0].Value = objBll.Region_id;

            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("GetCenterFromRegion", param);
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

    public int AlternateDaysWorkingInsert(BLLAlternateDaysWorking objbll)
    {
        SqlParameter[] param = new SqlParameter[7];

        
        param[0] = new SqlParameter("@Region_id", SqlDbType.Int); param[0].Value = objbll.Region_id;
        param[1] = new SqlParameter("@Center_id", SqlDbType.Int); param[1].Value = objbll.Center_id;
        param[2] = new SqlParameter("@Off_day", SqlDbType.DateTime); param[2].Value = objbll.Off_day;
        param[3] = new SqlParameter("@Alternate_working_day", SqlDbType.DateTime); param[3].Value = objbll.Alternate_working_day;
        param[4] = new SqlParameter("@Reason", SqlDbType.NVarChar); param[4].Value = objbll.Reason;

        param[5] = new SqlParameter("@Inserted_by", SqlDbType.NVarChar); param[5].Value = objbll.Inserted_by;

        param[6] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[6].Direction = ParameterDirection.Output;


        dalobj.sqlcmdExecute("InsertAlternateWorkingDays", param);
        int k = (int)param[6].Value;
        return k;

    }

    public DataTable fetchAlternateDaysWorking(BLLAlternateDaysWorking objbll)
    {

        DataTable _dt = new DataTable();


        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NChar); param[0].Value = objbll.PMonth;
        param[1] = new SqlParameter("@Region_id", SqlDbType.NChar); param[1].Value = objbll.Region_id;
        param[2] = new SqlParameter("@Center_id", SqlDbType.NChar); param[2].Value = objbll.Center_id;


        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("fetchAlternateWorkingDays", param);
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