using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for _DALEmployeeReportToHOD
/// </summary>
public class _DALEmployeeReportToHOD
{
    DALBase dalobj = new DALBase();
	public _DALEmployeeReportToHOD()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable isEmployeeHOD(BLLEmployeeReportToHOD objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Employee_Code", SqlDbType.NChar);
        param[0].Value = objbll.EmployeeCode;



        DataTable _dt = dalobj.sqlcmdFetch("sp_isEmployeeHOD", param);

        return _dt;

    }

    public int MakeHODfromEmployee(BLLEmployeeReportToHOD objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Employee_Code", SqlDbType.NChar);
        param[0].Value = objbll.EmployeeCode;

        int k = dalobj.sqlcmdExecute("sp_MakeHODfromEmployee", param);

        return k;

    }

    public int MakeEmployeefromHOD(BLLEmployeeReportToHOD objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Employee_Code", SqlDbType.NChar);
        param[0].Value = objbll.EmployeeCode;

        int k = dalobj.sqlcmdExecute("sp_MakeEmployeefromHOD", param);

        return k;

    }


    public DataTable WebDepartmentSelectByMonthRegionCenter(BLLEmployeeReportToHOD objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@PMonth", SqlDbType.NChar);
        param[0].Value = objbll.PMonth;

        param[1] = new SqlParameter("@Region_id", SqlDbType.NChar);
        param[1].Value = objbll.regionId;

        param[2] = new SqlParameter("@Center_id", SqlDbType.NChar);
        param[2].Value = objbll.centerId;

        return dalobj.sqlcmdFetch("WebDepartmentSelectByMonthRegionCenter", param);
 

    }
}