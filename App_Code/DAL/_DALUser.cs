using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALUser
/// </summary>
public class _DALUser
{
    DALBase dalobj = new DALBase();


    public _DALUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int UserAdd(BLLUser objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("UserInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int UserUpdate(BLLUser objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@User_Name", SqlDbType.NVarChar);
        param[0].Value = objbll.User_Name;
        param[1] = new SqlParameter("@Password", SqlDbType.NVarChar);
        param[1].Value = objbll.Password;
        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("WebUserUpdate", param);
        int k = (int)param[2].Value;
        return k;
    }
    public int UserDelete(BLLUser objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@User_Id", SqlDbType.Int);
        //   param[0].Value = objbll.User_Id;


        int k = dalobj.sqlcmdExecute("UserDelete", param);

        return k;
    }
    public int UserPasswordUpdate(BLLUser objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@User_Name", SqlDbType.NVarChar);
        param[0].Value = objbll.User_Name;

        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        int k = dalobj.sqlcmdExecute("WebUserSetDefaultPassword", param);
        k = (int)param[1].Value;
        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable UserSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("UserSelect", param);
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

    public DataTable UserSelectByUserName(BLLUser objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@UserName", SqlDbType.NVarChar);
        param[0].Value = objbll.User_Name;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("UserSelectByUserName", param);
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



    }

    public DataTable UserSelect(BLLUser objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@User_Name", SqlDbType.NVarChar);
        param[0].Value = objbll.User_Name;


        param[1] = new SqlParameter("@Password", SqlDbType.NVarChar);
        param[1].Value = objbll.Password;

        param[2] = new SqlParameter("@IpAddress", SqlDbType.NVarChar);
        param[2].Value = objbll.IpAddress;

        DataTable _dt = new DataTable();


        //DataTable table = new DataTable(); using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString)) using (var cmd = new SqlCommand("usp_GetABCD", con)) using (var da = new SqlDataAdapter(cmd))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    da.Fill(table);
        //}
        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebUserSelectAll", param);
            return _dt;
        }
        catch (Exception _exception)
        {
            //throw _exception;
            throw new ApplicationException("An error occurred in the DAL.", _exception);
        }
        finally
        {
            dalobj.CloseConnection();
        }

        return _dt;

    }

    public DataTable UserSelectByUserTypeId(BLLUser obj)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@User_type_Id", SqlDbType.Int);
        param[0].Value = obj.User_Type_Id;

        param[1] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[1].Value = obj.Region_Id;

        param[2] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[2].Value = obj.Center_Id;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("sp_ListOfUsersByUserType_Id", param);
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

    public DataTable UserSelectAll(BLLUser obj)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[0].Value = obj.Region_Id;

        param[1] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[1].Value = obj.Center_Id;

        param[2] = new SqlParameter("@DeptCode", SqlDbType.Int);
        param[2].Value = obj.DeptCode;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("UserSelectAll", param);
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
    public int UserSelectField(int _Id)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@Section_Id", SqlDbType.Int);
        param[0].Value = _Id;

        param[1] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("", param);
        int k = (int)param[1].Value;
        return k;

    }


    #endregion


}
