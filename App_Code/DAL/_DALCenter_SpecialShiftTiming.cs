using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALCenterShifts_SpecialCases
/// </summary>
public class DALCenterShifts_SpecialCases
{
    DALBase dalobj = new DALBase();


    public DALCenterShifts_SpecialCases()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int CenterShifts_SpecialCasesInsertDetails(BLLCenterShifts_SpecialCases objbll)
    {
        SqlParameter[] param = new SqlParameter[12];

        //param[0] = new SqlParameter("@CenterShifts_SpecialCases_ID", SqlDbType.Int); 
        //param[0].Value = objbll.CenterShifts_SpecialCases_ID;

        param[0] = new SqlParameter("@AttDate", SqlDbType.DateTime); 
        param[0].Value = objbll.AttDate;
       
        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int); 
        param[1].Value = objbll.Center_Id;
        
        param[2] = new SqlParameter("@Region_Id", SqlDbType.Int); 
        param[2].Value = objbll.Region_Id;
        
        param[3] = new SqlParameter("@StartTime", SqlDbType.NVarChar); 
        param[3].Value = (objbll.StartTime != null) ? objbll.StartTime : "";
        
        param[4] = new SqlParameter("@EndTime", SqlDbType.NVarChar); 
        param[4].Value = (objbll.EndTime != null) ? objbll.EndTime : "";
        
        param[5] = new SqlParameter("@Margin", SqlDbType.Int); 
        param[5].Value = objbll.Margin;
        
        param[6] = new SqlParameter("@AbsentTime", SqlDbType.NVarChar); 
        param[6].Value = (objbll.AbsentTime != null) ? objbll.AbsentTime : "";
        
        param[7] = new SqlParameter("@TchrSTime", SqlDbType.NVarChar); 
        param[7].Value = (objbll.TchrSTime != null) ? objbll.TchrSTime : "";
        
        param[8] = new SqlParameter("@TchrETime", SqlDbType.NVarChar); 
        param[8].Value = (objbll.TchrETime != null) ? objbll.TchrETime : "";
       
        param[9] = new SqlParameter("@CreatedBy", SqlDbType.Int); 
        param[9].Value = objbll.CreateBy;
      
        param[10] = new SqlParameter("@Remarks", SqlDbType.NVarChar); 
        param[10].Value = (objbll.Remarks != null) ? objbll.Remarks : "";

        param[11] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[11].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("CenterShifts_SpecialCasesInsertDetails", param);
        int k = (int)param[11].Value;
        return k;

    }
    public int CenterShifts_SpecialCasesUpdate(BLLCenterShifts_SpecialCases objbll)
    {
        SqlParameter[] param = new SqlParameter[11];
        param[0]= new SqlParameter("@CenterShifts_SpecialCases_ID",SqlDbType.Int);
        param[0].Value = objbll.CenterShifts_SpecialCases_ID; 
        
        param[1] = new SqlParameter("@AttDate", SqlDbType.DateTime);
        param[1].Value = objbll.AttDate;

        param[2] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[2].Value = objbll.Center_Id;

        param[3] = new SqlParameter("@StartTime", SqlDbType.NVarChar);
        param[3].Value = (objbll.StartTime != null) ? objbll.StartTime : "";

        param[4] = new SqlParameter("@EndTime", SqlDbType.NVarChar);
        param[4].Value = (objbll.EndTime != null) ? objbll.EndTime : "";

        param[5] = new SqlParameter("@Margin", SqlDbType.Int);
        param[5].Value = objbll.Margin;

        param[6] = new SqlParameter("@AbsentTime", SqlDbType.NVarChar);
        param[6].Value = (objbll.AbsentTime != null) ? objbll.AbsentTime : "";

        param[7] = new SqlParameter("@TchrSTime", SqlDbType.NVarChar);
        param[7].Value = (objbll.TchrSTime != null) ? objbll.TchrSTime : "";

        param[8] = new SqlParameter("@TchrETime", SqlDbType.NVarChar);
        param[8].Value = (objbll.TchrETime != null) ? objbll.TchrETime : "";

        param[9] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
        param[9].Value = (objbll.Remarks != null) ? objbll.Remarks : "";

        param[10] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[10].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("CenterShifts_SpecialCasesUpdateDetails", param);
        int k = (int)param[10].Value;
        return k;
    }
    public int CenterShifts_SpecialCasesDelete(BLLCenterShifts_SpecialCases objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@CenterShifts_SpecialCases_ID", SqlDbType.Int);
        param[0].Value = objbll.CenterShifts_SpecialCases_ID;
        int k = dalobj.sqlcmdExecute("CenterShifts_SpecialCasesDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable CenterShifts_SpecialCasesSelectAll(BLLCenterShifts_SpecialCases objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.Region_Id;

        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = objbll.Center_Id;

        param[2] = new SqlParameter("@PMonth", SqlDbType.NVarChar);
        param[2].Value = objbll.PMonth;

    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("CenterShifts_SpecialCasesSelectAll", param);
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
    
    public DataTable CenterShifts_SpecialCasesSelect(BLLCenterShifts_SpecialCases objbll)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@Evaluation_Criteria_Type_Id", SqlDbType.Int);
  //  param[0].Value = objbll.Evaluation_Criteria_Type_Id;


    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("CenterShifts_SpecialCasesSelectAll", param);
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

    public DataTable CenterShifts_SpecialCasesSelectByStatusID(BLLCenterShifts_SpecialCases objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("CenterShifts_SpecialCasesSelectByStatusID");
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
