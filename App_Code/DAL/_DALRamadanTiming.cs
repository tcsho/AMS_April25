using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALRamadanTiming
/// </summary>
public class DALRamadanTiming
{
    DALBase dalobj = new DALBase();


    public DALRamadanTiming()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int RamadanTimingAdd(BLLRamadanTiming objbll)
    {
        SqlParameter[] param = new SqlParameter[24];

        param[0] = new SqlParameter("@FromDate", SqlDbType.DateTime);
        param[0].Value = objbll.StartDate;

        param[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
        param[1].Value = objbll.EndDate;
        param[2] = new SqlParameter("@Region_ID", SqlDbType.Int);
        param[2].Value = objbll.Region_ID;

        param[3] = new SqlParameter("@Center_ID", SqlDbType.Int);
        param[3].Value = objbll.Center_ID;

        param[4] = new SqlParameter("@StartTime", SqlDbType.NVarChar);
        param[4].Value = (objbll.StartTime != null) ? objbll.StartTime : "";

        param[5] = new SqlParameter("@EndTime", SqlDbType.NVarChar);
        param[5].Value = (objbll.EndTime != null) ? objbll.EndTime : "";

        param[6] = new SqlParameter("@FridayStartTime", SqlDbType.NVarChar);
        param[6].Value = (objbll.FridayStartTime != null) ? objbll.FridayStartTime : "";
        param[7] = new SqlParameter("@FridayEndTime", SqlDbType.NVarChar);
        param[7].Value = (objbll.FridayEndTime != null) ? objbll.FridayEndTime : "";
        param[8] = new SqlParameter("@AbsentTime", SqlDbType.NVarChar);
        param[8].Value = (objbll.AbsentTime != null) ? objbll.AbsentTime : "";
        param[9] = new SqlParameter("@TchrSTime", SqlDbType.NVarChar);
        param[9].Value = (objbll.TeacherStartTime != null) ? objbll.TeacherStartTime : "";
        param[10] = new SqlParameter("@TchrETime", SqlDbType.NVarChar);
        param[10].Value = (objbll.TeacherEndTime != null) ? objbll.TeacherEndTime : "";

        param[11] = new SqlParameter("@TchrFridaySTime", SqlDbType.NVarChar);
        param[11].Value = (objbll.TeacherFridayStartTime != null) ? objbll.TeacherFridayStartTime : "";
        param[12] = new SqlParameter("@TchrFridayETime", SqlDbType.NVarChar);
        param[12].Value = (objbll.TeacherFridayEndTime != null) ? objbll.TeacherFridayEndTime : "";

        param[13] = new SqlParameter("@NoSTime", SqlDbType.NVarChar);
        param[13].Value = (objbll.NOStart_Time != null) ? objbll.NOStart_Time : "";
        param[14] = new SqlParameter("@NoETime", SqlDbType.NVarChar);
        param[14].Value = (objbll.NOEnd_Time != null) ? objbll.NOEnd_Time : "";
        param[15] = new SqlParameter("@NOATime", SqlDbType.NVarChar);
        param[15].Value = (objbll.NOAbsentTime != null) ? objbll.NOAbsentTime : "";

        param[16] = new SqlParameter("@NoFridaySTime", SqlDbType.NVarChar);
        param[16].Value = (objbll.NOFridaySTime != null) ? objbll.NOFridaySTime : "";
        param[17] = new SqlParameter("@NoFridayETime", SqlDbType.NVarChar);
        param[17].Value = (objbll.NOFridayETime != null) ? objbll.NOFridayETime : "";

        param[18] = new SqlParameter("@CreatedBy", SqlDbType.Int);
        param[18].Value = objbll.CreatedBy;

        param[19] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
        param[19].Value = (objbll.Remarks != null) ? objbll.Remarks : "";

        param[20] = new SqlParameter("@SaturdaySTime", SqlDbType.NVarChar);
        param[20].Value = (objbll.SaturdayStartTime != null) ? objbll.SaturdayStartTime : "";

        param[21] = new SqlParameter("@SaturdayETime", SqlDbType.NVarChar);
        param[21].Value = (objbll.SaturdayEndTime != null) ? objbll.SaturdayEndTime : "";
        param[22] = new SqlParameter("@TeacherAbsentTime", SqlDbType.NVarChar);
        param[22].Value = (objbll.TeacherAbsentTime != null) ? objbll.TeacherAbsentTime: "";

        param[23] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[23].Direction = ParameterDirection.Output;
        dalobj.sqlcmdExecute("RamadanTimingInsertDetails", param);
        int k = (int)param[23].Value;
        return k;

    }

    public int RamadanTimingDelete(BLLRamadanTiming objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Id", SqlDbType.Int);
        param[0].Value = objbll.RT_Id;
        int k = dalobj.sqlcmdExecute("RamadanTimingDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable RamadanTimingSelect(BLLRamadanTiming objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.Region_ID;
        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = objbll.Center_ID;
        param[2] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[2].Value = objbll.Month;

        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("RamadanTiming_SelectAll", param);
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

        return dt;

    }

    #endregion


}
