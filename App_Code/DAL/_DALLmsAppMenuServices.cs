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
using System.Collections.Generic;

/// <summary>
/// Summary description for _DALLmsAppMenuServices
/// </summary>
public class _DALLmsAppMenuServices
{
    DALBase dalobj = new DALBase();

    public _DALLmsAppMenuServices()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    public int LmsAppMenuServicesInsert(BLLLmsAppMenuServices objbll)
    {
        SqlParameter[] param = new SqlParameter[10];
       
        //param[0] = new SqlParameter("@Worksite_ID", SqlDbType.Int); 
        //param[0].Value = objbll.Worksite_ID;

        //param[1] = new SqlParameter("@WrkTool_ID", SqlDbType.Int); 
        //param[1].Value = objbll.WrkTool_ID;

        //param[2] = new SqlParameter("@StartDate", SqlDbType.DateTime); 
        //param[2].Value = objbll.StartDate;

        //param[3] = new SqlParameter("@EndDate", SqlDbType.DateTime); 
        //param[3].Value = objbll.EndDate;

        //param[4] = new SqlParameter("@ModuleTitle", SqlDbType.NVarChar); 
        //param[4].Value = objbll.ModuleTitle;

        //param[5] = new SqlParameter("@Description", SqlDbType.NVarChar); 
        //param[5].Value = objbll.Description;

        //param[6] = new SqlParameter("@CreatedOn", SqlDbType.DateTime); 
        //param[6].Value = objbll.CreatedOn;

        //param[7] = new SqlParameter("@CreatedBy", SqlDbType.Int); 
        //param[7].Value = objbll.CreatedBy;

        //param[8] = new SqlParameter("@AddToSchedule", SqlDbType.Bit); 
        //param[8].Value = objbll.AddToSchedule;

        //param[9] = new SqlParameter("@Status_Id", SqlDbType.Int); 
        //param[9].Value = objbll.Status_Id;

        int k = dalobj.sqlcmdExecute("LmsAppMenuServicesInsert", param);
        return k;

    }

    public int LmsAppMenuServicesUpdate(BLLLmsAppMenuServices objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@User_Type_Id", SqlDbType.Int);
        param[0].Value = objbll.User_Type_Id;

        //param[1] = new SqlParameter("@Menu_ID", SqlDbType.Int);
        //param[1].Value = objbll.Menu_ID;

        //param[2] = new SqlParameter("@isAllow", SqlDbType.Bit);
        //param[2].Value = objbll.IsAllow;         


        param[1] = new SqlParameter("@strMarkedObjectsIDs", SqlDbType.NVarChar);
        param[1].Value = objbll.MarkedObjectIds;
            
        param[2] = new SqlParameter("@strUnMarkedObjectsIDs", SqlDbType.NVarChar);
        param[2].Value = objbll.UnmarkedObjectIds;     

        int k = dalobj.sqlcmdExecute("LmsAppMenuServicesUpdate", param);
        return k;
    }

    public int LmsAppMenuServicesDelete(BLLLmsAppMenuServices objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        //param[0] = new SqlParameter("@Module_ID", SqlDbType.NVarChar);
        //param[0].Value = objbll.ModuleIdsString;  


        int k = dalobj.sqlcmdExecute("LmsAppMenuServicesDelete", param);

        return k;
    }     

    public DataTable LmsAppMenuServicesSelectByID(BLLLmsAppMenuServices objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        //param[0] = new SqlParameter("@Module_ID", SqlDbType.Int);
        //param[0].Value = objbll.Module_ID;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("LmsAppMenuServicesSelectByID", param);
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

    public DataTable LmsAppMenuServicesSelectByUserTypeID(BLLLmsAppMenuServices objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@User_Type_Id", SqlDbType.Int);
        param[0].Value = objbll.User_Type_Id;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("LmsAppMenuServicesSelectByUserTypeID", param);
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

  
    
    
}

