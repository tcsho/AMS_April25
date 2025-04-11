using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALHODs_for_daily_report
/// </summary>
public class DALHODs_for_daily_report
{
    DALBase dalobj = new DALBase();


    public DALHODs_for_daily_report()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int HODs_for_daily_reportInsert(BLLHODs_for_daily_report objbll)
    {
        SqlParameter[] param = new SqlParameter[3];
       
        param[0] = new SqlParameter("@EmployeeCode",SqlDbType.NVarChar);
        param[0].Value = (objbll.EmployeeCode != null) ? objbll.EmployeeCode : ""; 
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@CreatedBy",SqlDbType.Int);
        param[1].Value = (objbll.CreatedBy != null) ? objbll.CreatedBy:0; 
        param[1].Value = objbll.CreatedBy;
         
        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("HODs_for_daily_reportInsert", param);
        int k = (int)param[2].Value;
        return k;

    }
  
    public int HODs_for_daily_reportDelete(BLLHODs_for_daily_report objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@HODs_for_daily_report_id", SqlDbType.Int);
        param[0].Value = objbll.HODs_for_daily_report_id;


        int k = dalobj.sqlcmdExecute("HODs_for_daily_reportDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable HODs_for_daily_reportSelectAll()
    {
   
    DataTable dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        dt = dalobj.sqlcmdFetch("HODs_for_daily_reportSelectAll");
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
