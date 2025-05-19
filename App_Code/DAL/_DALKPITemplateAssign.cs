using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _DALKPITemplateAssign
/// </summary>
public class _DALKPITemplateAssign
{
    DALBase dalobj = new DALBase();
    DALBaseAims dalobjAims = new DALBaseAims();
    public _DALKPITemplateAssign()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int KPITemplateAssignAdd(BLLKPITemplateAssign objbll)
    {
        SqlParameter[] param = new SqlParameter[7];

        param[0] = new SqlParameter("@TemplateId", SqlDbType.Int);
        param[0].Value = objbll.TemplateId;

        param[1] = new SqlParameter("@EmpCategory", SqlDbType.NVarChar);
        param[1].Value = objbll.EmpCategory;

        param[2] = new SqlParameter("@RegionID", SqlDbType.Int);
        param[2].Value = objbll.RegionID;

        param[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar);
        param[3].Value = objbll.CreatedBy;

        param[4] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
        param[4].Value = objbll.CreatedDate;

        param[5] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[5].Direction = ParameterDirection.Output;

        param[6] = new SqlParameter("@ReturnId", SqlDbType.Int);
        param[6].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("KPITemplateAssignInsert", param);
        int alreadyIn = (int)param[5].Value;
        int AssignMasterId = (int)param[6].Value;

        // Insert detail list
        if (objbll.KPI_TemplateAssignDetail != null && objbll.KPI_TemplateAssignDetail.Count > 0)
        {
            foreach (var detail in objbll.KPI_TemplateAssignDetail)
            {
                SqlParameter[] detailParams = new SqlParameter[13];

                detailParams[0] = new SqlParameter("@AssignMasterID", SqlDbType.Int);
                detailParams[0].Value = AssignMasterId;

                detailParams[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                detailParams[1].Value = detail.EmployeeID;

                detailParams[2] = new SqlParameter("@AssignSIQAKS", SqlDbType.NVarChar);
                detailParams[2].Value = detail.AssignSIQAKS;

                detailParams[3] = new SqlParameter("@AssignClass", SqlDbType.NVarChar);
                detailParams[3].Value = detail.AssignClass;

                detailParams[4] = new SqlParameter("@IsAssigned", SqlDbType.Bit);
                detailParams[4].Value = detail.IsAssigned;

                detailParams[5] = new SqlParameter("@AssignedDate", SqlDbType.DateTime);
                detailParams[5].Value = detail.AssignedDate;

                detailParams[6] = new SqlParameter("@ReturnId", SqlDbType.Int); // Optional
                detailParams[6].Direction = ParameterDirection.Output;

                dalobj.sqlcmdExecute("KPITemplateAssignDetailInsert", detailParams);
            }
        }
        return AssignMasterId;
    }

    public DataTable FetchKeyStages()
    {

        DataTable _dt = new DataTable();

        try
        {
            dalobjAims.OpenConnection();
            _dt = dalobjAims.sqlcmdFetch("Get_All_GroupHead");
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobjAims.CloseConnection();
        }
    }

    public DataTable GetClassData()
    {

        DataTable _dt = new DataTable();

        try
        {
            dalobjAims.OpenConnection();
            _dt = dalobjAims.sqlcmdFetch("ClassSelectAll");
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobjAims.CloseConnection();
        }
    }
    #endregion
}