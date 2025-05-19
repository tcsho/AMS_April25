using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _DALKPITemplateAssignDetail
/// </summary>
public class _DALKPITemplateAssignDetail
{
    DALBase dalobj = new DALBase();
    public _DALKPITemplateAssignDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int KPITemplateAssignDetailInsert(BLLKPITemplateAssignDetail obj)
    {
        SqlParameter[] param = new SqlParameter[8];

        param[0] = new SqlParameter("@AssignMasterID", obj.AssignMasterID);
        param[1] = new SqlParameter("@EmployeeID", obj.EmployeeID);
        param[2] = new SqlParameter("@AssignCenter", obj.AssignCenters);
        param[3] = new SqlParameter("@AssignSIQAKS", obj.AssignSIQAKS);
        param[4] = new SqlParameter("@AssignClass", obj.AssignClass);
        param[5] = new SqlParameter("@IsAssigned", obj.IsAssigned);
        param[6] = new SqlParameter("@AssignedDate", obj.AssignedDate);
        param[7] = new SqlParameter("@ReturnId", SqlDbType.Int);
        param[7].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("KPITemplateAssignDetailInsert", param);
        return Convert.ToInt32(param[7].Value);
    }

    public int KPITemplateAssignDetailUpdate(BLLKPITemplateAssignDetail obj)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@EmployeeID", obj.EmployeeID);
        param[1] = new SqlParameter("@RemarksHR", obj.RemarksHR);
        param[2] = new SqlParameter("ModifiedBy", obj.ModifiedBy);
        param[3] = new SqlParameter("@ModifiedDate", obj.ModifiedDate);
        
        int k = dalobj.sqlcmdExecute("KPITemplateAssignDetailUpdate", param);
        return k;
    }
    public int KPITemplateAssignDetailDelete(BLLKPITemplateAssignDetail objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@AssignMasterID", SqlDbType.Int);
        param[0].Value = objbll.AssignMasterID;


        int k = dalobj.sqlcmdExecute("KPITemplateAssignDetailDelete", param);

        return k;
    }

}