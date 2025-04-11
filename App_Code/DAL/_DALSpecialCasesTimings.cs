using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALSpecialCasesTimings
/// </summary>
public class _DALSpecialCasesTimings
{


    DALBase dalobj = new DALBase();



	public _DALSpecialCasesTimings()
	{
		//
		// TODO: Add constructor logic here
		//
	}

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
    public DataTable fetchCenters(BLLSpecialCasesTimigs objBll)
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

    public int SpecialCasesTimingsInsert(BLLSpecialCasesTimigs objbll)
    {
        SqlParameter[] param = new SqlParameter[29];


        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[0].Value = objbll.Emp_Code;
       
        param[1] = new SqlParameter("@StartTime", SqlDbType.NVarChar); param[1].Value =  (objbll.Time_in != null) ? objbll.Time_in : "";
        param[2] = new SqlParameter("@EndTime", SqlDbType.NVarChar); param[2].Value = (objbll.Time_out != null) ? objbll.Time_out : "";
        param[3] = new SqlParameter("@Margin", SqlDbType.Int); param[3].Value = (objbll.Margin!=null )? objbll.Margin:0;
        param[4] = new SqlParameter("@FirStartTime", SqlDbType.NVarChar); param[4].Value = (objbll.Fri_Time_in!=null) ? objbll.Fri_Time_in:"";
        param[5] = new SqlParameter("@FriAbsentTime", SqlDbType.NVarChar); param[5].Value = (objbll.Fri_Absent_Time != null) ? objbll.Fri_Absent_Time : "";
        param[6] = new SqlParameter("@FriEndTime", SqlDbType.NVarChar); param[6].Value = (objbll.Fri_Time_out != null) ? objbll.Fri_Time_out : "";
        param[7] = new SqlParameter("@SatStartTime", SqlDbType.NVarChar); param[7].Value = (objbll.SaT_Time_in != null) ? objbll.SaT_Time_in : "";
        param[8] = new SqlParameter("@SatAbsentTime", SqlDbType.NVarChar); param[8].Value = (objbll.SaT_Absent_Time!=null )? objbll.SaT_Absent_Time:"";
        param[9] = new SqlParameter("@SatEndTime", SqlDbType.NVarChar); param[9].Value = (objbll.SaT_Time_out != null) ? objbll.SaT_Time_out : "";
     
        param[10] = new SqlParameter("@ActSTime", SqlDbType.NVarChar); param[10].Value = (objbll.Time_in!=null) ? objbll.Time_in:"";
        param[11] = new SqlParameter("@ActETime", SqlDbType.NVarChar); param[11].Value = (objbll.Time_out!=null)? objbll.Time_out:"";
        param[12] = new SqlParameter("@AbsentTime", SqlDbType.NVarChar); param[12].Value = (objbll.Absent_Time!=null) ?objbll.Absent_Time:"";
        param[13] = new SqlParameter("@Valid_From", SqlDbType.DateTime); param[13].Value = objbll.From_date;
        param[14] = new SqlParameter("@valid_To", SqlDbType.DateTime); param[14].Value = objbll.To_date;
        param[15] = new SqlParameter("@Inserted_by", SqlDbType.NVarChar); param[15].Value = objbll.Inserted_by;
        param[16] = new SqlParameter("@Reason", SqlDbType.NVarChar); param[16].Value = objbll.Reason;
       
        param[17] = new SqlParameter("@Saturday", SqlDbType.Bit); param[17].Value = objbll.Saturday;
        param[18] = new SqlParameter("@Sunday", SqlDbType.Bit); param[18].Value = objbll.Sunday;
        param[19] = new SqlParameter("@Monday", SqlDbType.Bit); param[19].Value = objbll.Monday;
        param[20] = new SqlParameter("@Tuesday", SqlDbType.Bit); param[20].Value = objbll.Tuesday;
        param[21] = new SqlParameter("@Wednesday", SqlDbType.Bit); param[21].Value = objbll.Wednesday;
        param[22] = new SqlParameter("@Thursday", SqlDbType.Bit); param[22].Value = objbll.Thursday;
        param[23] = new SqlParameter("@Friday", SqlDbType.Bit); param[23].Value = objbll.Friday;
        param[24] = new SqlParameter("@SpecialCase_Type_Id", SqlDbType.Int); param[24].Value = objbll.SpecialCase_Type;
        param[25] = new SqlParameter("@IsSpecificDays", SqlDbType.Int); param[25].Value = objbll.IsSpecificDays;
        param[24] = new SqlParameter("@SpecialCase_Type_Id", SqlDbType.Int); param[24].Value = objbll.SpecialCase_Type;
        param[25] = new SqlParameter("@IsSpecificDays", SqlDbType.Int); param[25].Value = objbll.IsSpecificDays;
        param[26] = new SqlParameter("@first_half_end", SqlDbType.NVarChar); param[26].Value = objbll.first_half_end;
        param[27] = new SqlParameter("@second_half_start", SqlDbType.NVarChar); param[27].Value = objbll.second_half_start;
       

        param[28] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[28].Direction = ParameterDirection.Output;


        dalobj.sqlcmdExecute("EmployeeShifts_SpecialCasesInsert", param);
        int k = (int)param[28].Value;
        return k;

    }
    public int SpecialCasesTimingsDelete(BLLSpecialCasesTimigs objbll)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@EmployeeShifts_SpecialCases_Id", SqlDbType.Int);
        param[0].Value = objbll.SpecialCases_id;
        param[1] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[1].Value = objbll.PMonth;
        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeShifts_SpecialCasesDelete", param);
        int k = (int)param[2].Value;
        return k;
    }

    public DataTable fetchSpecialCasesRegionCenter(BLLSpecialCasesTimigs objbll)
    {

        DataTable _dt = new DataTable();


        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NChar); param[0].Value = objbll.PMonth;
        param[1] = new SqlParameter("@Region_id", SqlDbType.NChar); param[1].Value = objbll.Region_id;
        param[2] = new SqlParameter("@Center_id", SqlDbType.NChar); param[2].Value = objbll.Center_id;


        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeShifts_SpecialCasesSelect", param);
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
    public DataTable SpecialCase_TypeSelectAll()
    {
        DataTable _dt = new DataTable();
        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SpecialCase_TypeSelectAll");
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
    public DataTable EmployeeShifts_SpecialCasesSelectDetail(int id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeShifts_SpecialCases_Id", SqlDbType.Int); param[0].Value = id;
        DataTable _dt = new DataTable();
        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeShifts_SpecialCasesSelectDetail", param);
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