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

/// <summary>
/// Summary description for _DALAddEmployee
/// </summary>
public class _DALAddEmployee
{
    DALBase dalobj = new DALBase();
	public _DALAddEmployee()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int EMPTRANS_ACTIVEInsert(BLLAddEmployee objbll)
    {
        SqlParameter[] param = new SqlParameter[14];


        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar);
        param[1].Value = objbll.FirstName;
        param[2] = new SqlParameter("@LastName", SqlDbType.NVarChar);
        param[2].Value = objbll.LastName;
        param[3] = new SqlParameter("@Name", SqlDbType.NVarChar);
        param[3].Value = objbll.Name;
        param[4] = new SqlParameter("@Region", SqlDbType.NVarChar);
        param[4].Value = objbll.Region;
        param[5] = new SqlParameter("@Branch", SqlDbType.NVarChar);
        param[5].Value = objbll.Branch;
        param[6] = new SqlParameter("@BranchCode", SqlDbType.NVarChar);
        param[6].Value = objbll.BranchCode;
        param[7] = new SqlParameter("@MStatus", SqlDbType.NVarChar);
        param[7].Value = objbll.MStatus;
        param[8] = new SqlParameter("@Gender", SqlDbType.NVarChar);
        param[8].Value = objbll.Gender;
        param[9] = new SqlParameter("@Designation", SqlDbType.NVarChar);
        param[9].Value = objbll.Designation;
        //param[10] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        //param[10].Direction = ParameterDirection.Output;
        if (objbll.DateOfBirth == null)
        {
            param[10] = new SqlParameter("@DOB", SqlDbType.DateTime);
            param[10].Value = null;
        }
        else
        {
            param[10] = new SqlParameter("@DOB", SqlDbType.DateTime);
            param[10].Value = objbll.DateOfBirth; ;
        }
        if (objbll.DateOfBirth == null)
        {
            param[11] = new SqlParameter("@DOJ", SqlDbType.DateTime);
            param[11].Value = null;
        }
        else
        {
            param[11] = new SqlParameter("@DOJ", SqlDbType.DateTime);
            param[11].Value = objbll.DateOfJoining;
        }
        param[12] = new SqlParameter("@Email", SqlDbType.NVarChar);
        param[12].Value = objbll.email;
        param[13] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[13].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EMPTRANS_ACTIVEInsert", param);
        int k = (int)param[13].Value;
        return k;
    }
    public int sp_empfromTemp_Trans()
    {
        int k = dalobj.sqlcmdExecute("sp_empfromTemp_Trans");
        return k;
    }
    
}
