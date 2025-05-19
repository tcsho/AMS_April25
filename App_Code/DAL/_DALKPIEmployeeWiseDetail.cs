using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _DALKPIEmployeeWiseDetail
/// </summary>

public class _DALKPIEmployeeWiseDetail
{
    DALBase dalobj = new DALBase();
    public _DALKPIEmployeeWiseDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int KPITemplateDetailInsert(BLLKPIEmployeeWiseDetail obj)
    {
        SqlParameter[] param = new SqlParameter[14];

        param[0] = new SqlParameter("@AssignDetailID", obj.AssignDetailID);
        param[1] = new SqlParameter("@TemplateDetailID", obj.TemplateDetailID);
        param[2] = new SqlParameter("@KPIName", obj.KPIName);
        param[3] = new SqlParameter("@Weight", obj.Weight);
        param[4] = new SqlParameter("@Grade5_Max", obj.Grade5_Max);
        param[5] = new SqlParameter("@Grade5_Min", obj.Grade5_Min);
        param[6] = new SqlParameter("@Grade4_Max", obj.Grade4_Max);
        param[7] = new SqlParameter("@Grade4_Min", obj.Grade4_Min);
        param[8] = new SqlParameter("@Grade3_Max", obj.Grade3_Max);
        param[9] = new SqlParameter("@Grade3_Min", obj.Grade3_Min);
        param[10] = new SqlParameter("@Grade2_Max", obj.Grade2_Max);
        param[11] = new SqlParameter("@Grade2_Min", obj.Grade2_Min);
        param[12] = new SqlParameter("@Grade1_Max", obj.Grade1_Max);
        param[13] = new SqlParameter("@Grade1_Min", obj.Grade1_Min);
        

        dalobj.sqlcmdExecute("KPITemplateDetailInsert_EmpWise", param);
        return Convert.ToInt32(param[13].Value);
    }

    public int KPITemplateAssignDetailUpdate(BLLKPIEmployeeWiseDetail obj)
    {
        SqlParameter[] param = new SqlParameter[14];

        param[0] = new SqlParameter("@AssignDetailID", obj.AssignDetailID);
        param[1] = new SqlParameter("@TemplateDetailID", obj.TemplateDetailID);
        param[2] = new SqlParameter("@Grade5_Max", obj.Grade5_Max);
        param[3] = new SqlParameter("@Grade5_Min", obj.Grade5_Min);
        param[4] = new SqlParameter("@Grade4_Max", obj.Grade4_Max);
        param[5] = new SqlParameter("@Grade4_Min", obj.Grade4_Min);
        param[6] = new SqlParameter("@Grade3_Max", obj.Grade3_Max);
        param[7] = new SqlParameter("@Grade3_Min", obj.Grade3_Min);
        param[8] = new SqlParameter("@Grade2_Max", obj.Grade2_Max);
        param[9] = new SqlParameter("@Grade2_Min", obj.Grade2_Min);
        param[10] = new SqlParameter("@Grade1_Max", obj.Grade1_Max);
        param[11] = new SqlParameter("@Grade1_Min", obj.Grade1_Min);
        param[12] = new SqlParameter("@ModifiedBy", obj.ModifiedBy);
        param[13] = new SqlParameter("@ModifiedDate", obj.ModifiedDate);

        int k = dalobj.sqlcmdExecute("KPITemplateAssignDetailUpdate_ByEmp", param);
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
}