using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALUpdateEmployeeprofile
/// </summary>
public class DALUpdateEmployeeProfile
{
    DALBase dalobj = new DALBase();


    public DALUpdateEmployeeProfile()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int UpdateEmployeeProfileAdd(BLLUpdateEmployeeProfile objbll)
    {
         SqlParameter[] param = new SqlParameter[4];

        //param[0] = new SqlParameter("@Firstname", SqlDbType.NVarChar); 
        //param[0].Value = objbll.Firstname;
        
        //param[1] = new SqlParameter("@Lastname", SqlDbType.NVarChar); 
        //param[1].Value = objbll.Lastname;
        
        //param[2] = new SqlParameter("@Details", SqlDbType.NVarChar); 
        //param[2].Value = objbll.Details;

        //param[3] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        //param[3].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("UpdateEmployeeprofileInsert", param);
         int k = (int)param[3].Value;
         return k;

    }
    public void EmployeeProfileUpdate(BLLUpdateEmployeeProfile objbll)
    {
           
        SqlParameter[] param = new SqlParameter[15];

   
        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); 
        param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar); 
        param[1].Value = objbll.FirstName;
        param[2] = new SqlParameter("@LastName", SqlDbType.NVarChar); 
        param[2].Value = objbll.LastName;
        param[3] = new SqlParameter("@FullName", SqlDbType.NVarChar); 
        param[3].Value = objbll.FullName;
        param[4] = new SqlParameter("@Region_Id", SqlDbType.NVarChar); 
        param[4].Value = objbll.Region_Id;
        param[5] = new SqlParameter("@Center_Id", SqlDbType.Int); 
        param[5].Value = objbll.Center_Id;
        param[6] = new SqlParameter("@DeptCode", SqlDbType.Int); 
        param[6].Value = objbll.DeptCode;
        param[7] = new SqlParameter("@DesigCode", SqlDbType.Int); 
        param[7].Value = objbll.DesigCode;
        param[8] = new SqlParameter("@MStatus", SqlDbType.NVarChar); 
        param[8].Value = objbll.MaritalSts;
        param[9] = new SqlParameter("@Gender", SqlDbType.NVarChar); 
        param[9].Value = objbll.Gender;
        if (objbll.DOJ == null)
        {
            param[10] = new SqlParameter("@DOJ",  SqlDbType.Date);
            param[10].Value = null;
        }
        else
        {
            param[10] = new SqlParameter("@DOJ", SqlDbType.Date);
            param[10].Value = objbll.DOJ;
        }
        if (objbll.DOB == null)
        {
            param[11] = new SqlParameter("@DOB", SqlDbType.Date);
            param[11].Value = null;
        }
        else
        {
            param[11] = new SqlParameter("@DOB", SqlDbType.Date);
            param[11].Value = objbll.DOB;
        }
        param[12] = new SqlParameter("@Inactive", SqlDbType.NVarChar); 
        param[12].Value = objbll.Inactive;


        param[13] = new SqlParameter("@Email", SqlDbType.NVarChar);
        param[13].Value=objbll.Email;

        param[14] = new SqlParameter("@ResignDate", SqlDbType.DateTime);
        param[14].Value = objbll.ResignDate;
        dalobj.sqlcmdExecute("EmployeeProfileUpdate", param);
        //int k = (int)param[13].Value;
        //return k;
    }
    public int UpdateEmployeeProfileDelete(BLLUpdateEmployeeProfile objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@UpdateEmployeeprofile_Id", SqlDbType.Int);
     //   param[0].Value = objbll.UpdateEmployeeprofile_Id;


        int k = dalobj.sqlcmdExecute("UpdateEmployeeprofileDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable EmployeeProfileResignedSelect_ForUpdateProfileAndLeaveBalances(BLLUpdateEmployeeProfile obj)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = obj.Region_Id;
        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = obj.Center_Id;
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeProfileResignedSelect_ForUpdateProfileAndLeaveBalances", param);
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
    public DataTable EmployeeProfileSelect_ForUpdateProfileAndLeaveBalances(BLLUpdateEmployeeProfile obj)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = obj.Region_Id;
        param[1] = new SqlParameter("@Center_Id", SqlDbType.Int);
        param[1].Value = obj.Center_Id;
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeProfileSelect_ForUpdateProfileAndLeaveBalances", param);
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
 
    public DataTable UpdateEmployeeprofileSelectByStatusID(DALUpdateEmployeeProfile objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("UpdateEmployeeprofileSelectByStatusID");
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
    public DataTable EmployeeProfileDepartmentSelectAll()
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("DepartmentSelectAll");
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
