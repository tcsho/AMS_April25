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
public class _DALVacationTimings
{
	public _DALVacationTimings()
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
    public DataTable fetchCenters(BLLVacationTimigs objBll)
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

    public int VacationTimingsInsert(BLLVacationTimigs objbll)
    {
        SqlParameter[] param = new SqlParameter[11];

        
        param[0] = new SqlParameter("@Region_id", SqlDbType.Int); param[0].Value = objbll.Region_id;
        param[1] = new SqlParameter("@Center_id", SqlDbType.NChar); param[1].Value = objbll.strCenter_id;
        param[2] = new SqlParameter("@From_date", SqlDbType.NChar); param[2].Value = objbll.strFrom_date;
        param[3] = new SqlParameter("@To_date", SqlDbType.NChar); param[3].Value = objbll.strTo_date;
        param[4] = new SqlParameter("@Reason", SqlDbType.NVarChar); param[4].Value = objbll.Reason;
        param[5] = new SqlParameter("@Time_in", SqlDbType.NVarChar); param[5].Value = objbll.Time_in;
        param[6] = new SqlParameter("@Absent_time", SqlDbType.NVarChar); param[6].Value = objbll.Absent_Time;
        param[7] = new SqlParameter("@Time_out", SqlDbType.NVarChar); param[7].Value = objbll.Time_out;
        param[8] = new SqlParameter("@Inserted_by", SqlDbType.NVarChar); param[8].Value = objbll.Inserted_by;

        param[9] = new SqlParameter("@IsOffTeacher", SqlDbType.Bit); param[9].Value = objbll.IsOffteacher;
        param[10] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[10].Direction = ParameterDirection.Output;


        dalobj.sqlcmdExecute("VacationTimingsInsert", param);
        int k = (int)param[10].Value;
        return k;

    }
    public int UpdateVacationTimings(BLLVacationTimigs objbll)
    {
        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@VacationTimings_id", SqlDbType.Int); 
        param[0].Value = objbll.VacationTimings_id;
        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = objbll.Center_id;
        param[2] = new SqlParameter("@From_date", SqlDbType.DateTime); 
        param[2].Value = Convert.ToDateTime(objbll.From_date);
        param[3] = new SqlParameter("@To_date", SqlDbType.DateTime); 
        param[3].Value = Convert.ToDateTime(objbll.To_date);
        param[4] = new SqlParameter("@Reason", SqlDbType.NVarChar); 
        param[4].Value = objbll.Reason;
        param[5] = new SqlParameter("@Time_in", SqlDbType.NChar); 
        param[5].Value = objbll.Time_in;
        param[6] = new SqlParameter("@Absent_time", SqlDbType.NChar); 
        param[6].Value = objbll.Absent_Time;
        param[7] = new SqlParameter("@Time_out", SqlDbType.NChar);
        param[7].Value = objbll.Time_out;
        param[8] = new SqlParameter("@Last_updated_by", SqlDbType.NChar); 
        param[8].Value = objbll.Last_updated_by;
        param[9] = new SqlParameter("@IsOffTeacher", SqlDbType.Bit); 
        param[9].Value = objbll.IsOffteacher;
        param[10] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[10].Direction = ParameterDirection.Output;


        dalobj.sqlcmdExecute("VacationTimingsUpdate", param);
        int k = (int)param[10].Value;
        return k;

    }
    public int DeleteVacationTiming(int id)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@VacationTimings_id", SqlDbType.Int); param[0].Value = id;
        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;
        dalobj.sqlcmdExecute("VacationTimingDelete", param);
        int k = (int)param[1].Value;
        return k;
        

    }
    public int VacationTimingDeleteRegionWise(BLLVacationTimigs obj)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int); param[0].Value = obj.Region_id;
        param[1] = new SqlParameter("@From_date", SqlDbType.DateTime); param[1].Value = obj.From_date;
        param[2] = new SqlParameter("@To_date", SqlDbType.DateTime); param[2].Value = obj.To_date;
        param[3] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[3].Direction = ParameterDirection.Output;
        dalobj.sqlcmdExecute("VacationTimingDeleteRegionWise", param);
        int k = (int)param[3].Value;
        return k;
       

    }
    public DataTable fetchVacationsRegionCenter(BLLVacationTimigs objbll)
    {

        DataTable _dt = new DataTable();


        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NChar); param[0].Value = objbll.PMonth;
        param[1] = new SqlParameter("@Region_id", SqlDbType.NChar); param[1].Value = objbll.Region_id;
        param[2] = new SqlParameter("@Center_id", SqlDbType.NChar); param[2].Value = objbll.Center_id;


        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("fetchVacationsRegionCenter", param);
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
    public DataTable VacationTimingFillDate(BLLVacationTimigs objbll)
    {
        DataTable _dt = new DataTable();
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@PMonth", SqlDbType.Int); param[0].Value = objbll.PMonth;

        param[1] = new SqlParameter("@Region_Id", SqlDbType.Int); param[1].Value = objbll.Region_id;
        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("VacationTimingFillDate", param);
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