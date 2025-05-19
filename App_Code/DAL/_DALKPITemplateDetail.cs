using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _DALKPITemplateDetail
/// </summary>
public class _DALKPITemplateDetail
{
    DALBase dalobj = new DALBase();
    public _DALKPITemplateDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int KPITemplateDetailInsert(BLLKPITemplateDetail obj)
    {
        SqlParameter[] param = new SqlParameter[14];

        param[0] = new SqlParameter("@TemplateId", obj.templateId);
        param[1] = new SqlParameter("@KPIName", obj.kpiName);
        param[2] = new SqlParameter("@Weight", obj.weight);
        param[3] = new SqlParameter("@Grade5_Max", obj.grade5_max);
        param[4] = new SqlParameter("@Grade5_Min", obj.grade5_min);
        param[5] = new SqlParameter("@Grade4_Max", obj.grade4_max);
        param[6] = new SqlParameter("@Grade4_Min", obj.grade4_min);
        param[7] = new SqlParameter("@Grade3_Max", obj.grade3_max);
        param[8] = new SqlParameter("@Grade3_Min", obj.grade3_min);
        param[9] = new SqlParameter("@Grade2_Max", obj.grade2_max);
        param[10] = new SqlParameter("@Grade2_Min", obj.grade2_min);
        param[11] = new SqlParameter("@Grade1_Max", obj.grade1_max);
        param[12] = new SqlParameter("@Grade1_Min", obj.grade1_min);
        param[13] = new SqlParameter("@ReturnId", SqlDbType.Int);
        param[13].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("KPITemplateDetailInsert", param);
        return Convert.ToInt32(param[13].Value);
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

}