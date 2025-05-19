using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _DALKPITemplate
/// </summary>
public class _DALKPITemplate
{
    DALBase dalobj = new DALBase();
    public _DALKPITemplate()
    {
        
    }
    #region 'Start of Execution Methods'
    public int KPITemplateAdd(BLLKPITemplate objbll)
    {
        SqlParameter[] param = new SqlParameter[9];

        param[0] = new SqlParameter("@TemplateName", SqlDbType.NVarChar);
        param[0].Value = objbll.templateName;

        param[1] = new SqlParameter("@Year", SqlDbType.Int);
        param[1].Value = objbll.year;

        param[2] = new SqlParameter("@FromDate", SqlDbType.DateTime);
        param[2].Value = objbll.fromdate;

        param[3] = new SqlParameter("@ToDate", SqlDbType.DateTime);
        param[3].Value = objbll.todate;

        param[4] = new SqlParameter("@TotalWeight", SqlDbType.Int);
        param[4].Value = objbll.totalweight;

        param[5] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar);
        param[5].Value = objbll.createdby;

        param[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
        param[6].Value = objbll.createddate;

        param[7] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[7].Direction = ParameterDirection.Output;

        param[8] = new SqlParameter("@ReturnId", SqlDbType.Int);
        param[8].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("KPITemplateInsert", param);
        int alreadyIn = (int)param[7].Value;
        int templateId = (int)param[8].Value;

        // Insert detail list
        if (objbll.KPI_TemplateDetail != null && objbll.KPI_TemplateDetail.Count > 0)
        {
            foreach (var detail in objbll.KPI_TemplateDetail)
            {
                SqlParameter[] detailParams = new SqlParameter[14];

                detailParams[0] = new SqlParameter("@TemplateId", SqlDbType.Int);
                detailParams[0].Value = templateId;

                detailParams[1] = new SqlParameter("@KPIName", SqlDbType.NVarChar);
                detailParams[1].Value = detail.kpiName;

                detailParams[2] = new SqlParameter("@Weight", SqlDbType.Int);
                detailParams[2].Value = detail.weight;

                detailParams[3] = new SqlParameter("@Grade5_Max", SqlDbType.NVarChar);
                detailParams[3].Value = detail.grade5_max;
                
                detailParams[4] = new SqlParameter("@Grade5_Min", SqlDbType.NVarChar);
                detailParams[4].Value = detail.grade5_min;

                detailParams[5] = new SqlParameter("@Grade4_Max", SqlDbType.NVarChar);
                detailParams[5].Value = detail.grade4_max;
                
                detailParams[6] = new SqlParameter("@Grade4_Min", SqlDbType.NVarChar);
                detailParams[6].Value = detail.grade4_min;

                detailParams[7] = new SqlParameter("@Grade3_Max", SqlDbType.NVarChar);
                detailParams[7].Value = detail.grade3_max;
                
                detailParams[8] = new SqlParameter("@Grade3_Min", SqlDbType.NVarChar);
                detailParams[8].Value = detail.grade3_min;

                detailParams[9] = new SqlParameter("@Grade2_Max", SqlDbType.NVarChar);
                detailParams[9].Value = detail.grade2_max;
                
                detailParams[10] = new SqlParameter("@Grade2_Min", SqlDbType.NVarChar);
                detailParams[10].Value = detail.grade2_min;

                detailParams[11] = new SqlParameter("@Grade1_Max", SqlDbType.NVarChar);
                detailParams[11].Value = detail.grade1_max;
                
                detailParams[12] = new SqlParameter("@Grade1_Min", SqlDbType.NVarChar);
                detailParams[12].Value = detail.grade1_min;

                detailParams[13] = new SqlParameter("@ReturnId", SqlDbType.Int); // Optional
                detailParams[13].Direction = ParameterDirection.Output;

                dalobj.sqlcmdExecute("KPITemplateDetailInsert", detailParams);
            }
        }

        return templateId;

    }


    public int KPITemplateDelete(BLLKPITemplate objbll , string deleteby)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@TemplateId", SqlDbType.Int);
        param[0].Value = objbll.TemplateId;

        param[1] = new SqlParameter("@DeletedBy", SqlDbType.Int);
        param[1].Value = deleteby;

        int k = dalobj.sqlcmdExecute("KPITemplateDelete", param);

        return k;
    }


    public int KPITemplateDetailDelete(BLLKPITemplate objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@TemplateId", SqlDbType.Int);
        param[0].Value = objbll.TemplateId;


        int k = dalobj.sqlcmdExecute("KPITemplateDetailDelete", param);

        return k;
    }

    public DataTable KPITemplateFetchbyID(int templateId)
    {
        SqlParameter[] param = new SqlParameter[]
        {
        new SqlParameter("@TemplateId", SqlDbType.Int)
        {
            Value = templateId
        }
        };

        return dalobj.sqlcmdFetch("KPITemplateSelectById_ForEdit", param);
    }
    public DataTable KPITemplateFetchbyCatRegion(string category,string region)
    {
        SqlParameter[] param = new SqlParameter[]
    {
        new SqlParameter("@Category", SqlDbType.NVarChar)
        {
            Value = category
        },
        new SqlParameter("@Region", SqlDbType.NVarChar)
        {
            Value = region
        }
    };

        return dalobj.sqlcmdFetch("KPITemplateAssign_ByCatRegion", param);
    }

    public DataTable KPITemplateFetchbyEmpCode(int EmpID)
    {
        SqlParameter[] param = new SqlParameter[]
    {
        new SqlParameter("@EmployeeID", SqlDbType.Int)
        {
            Value = EmpID
        }
    };

        return dalobj.sqlcmdFetch("KPITemplateAssign_ByEmployee", param);
    }

    public DataTable KPIEmpTemplateFetchbyEmpCode(int EmpID)
    {
        SqlParameter[] param = new SqlParameter[]
    {
        new SqlParameter("@EmployeeID", SqlDbType.Int)
        {
            Value = EmpID
        }
    };

        return dalobj.sqlcmdFetch("KPIEmployeeTemplate_ByEmpCode", param);
    }
    public DataTable KPITemplateSelectAll()
    {

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("KPITemplateSelectAll");
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
    #endregion
}