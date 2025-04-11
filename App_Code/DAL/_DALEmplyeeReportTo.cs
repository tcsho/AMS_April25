using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALEmplyeeReportTo
/// </summary>
/// 
public class _DALEmplyeeReportTo
{
    DALBase dalobj = new DALBase();


    public _DALEmplyeeReportTo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int EmplyeeReportToAdd(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmplyeeReportToInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int EmplyeeReportToUpdate(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[10];


        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmplyeeReportToUpdate", param);
        int k = (int)param[9].Value;
        return k;
    }
    public int EmplyeeReportToDelete(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmplyeeReportTo_Id", SqlDbType.Int);
        //   param[0].Value = objbll.EmplyeeReportTo_Id;


        int k = dalobj.sqlcmdExecute("EmplyeeReportToDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable EmplyeeReportToSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebReportToEmployeeList", param);
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

    public DataTable EmplyeeReportToSelectList(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@ReportTo", SqlDbType.NVarChar);
        param[0].Value = objbll.ReportTo;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebReportToEmployeeList", param);
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

    public DataTable EmplyeeReportToHOD(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@ReportTo", SqlDbType.NVarChar);
        param[0].Value = objbll.ReportTo;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebReportToHOD", param);
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



    public DataTable EmplyeeReportToSelectEmail(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebReportToEmployeeSelectEmail", param);
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
    public DataTable SelectEmployeeListforCafe(BLLEmplyeeReportTo objbll)
    {
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SelectEmployeeListforCafe");
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

    public DataTable EmplyeeReportToSelect(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[7];


        param[0] = new SqlParameter("@ReportTo", SqlDbType.NVarChar);
        param[0].Value = objbll.ReportTo;

        param[1] = new SqlParameter("@UserType_id", SqlDbType.Int);
        param[1].Value = objbll.UserType_id;

        param[2] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[2].Value = objbll.Region_id;

        param[3] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[3].Value = objbll.Center_id;

        param[4] = new SqlParameter("@Status_id", SqlDbType.Int);
        param[4].Value = objbll.Status_id;

        param[5] = new SqlParameter("@deptCode", SqlDbType.Int);
        param[5].Value = objbll.DeptCode;
        param[6] = new SqlParameter("@PMonthDesc", SqlDbType.NVarChar);
        param[6].Value = objbll.PMonthDesc;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebReportToEmployee", param);
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


    public DataTable EmployeeprofileSelectByRegionCenterDept(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[3];


        param[0] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[0].Value = objbll.Region_id;

        param[1] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[1].Value = objbll.Center_id;

        param[2] = new SqlParameter("@deptCode", SqlDbType.Int);
        param[2].Value = objbll.DeptCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeprofileSelectByRegionCenterDept", param);
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
    
    public DataTable EmployeeprofileSelectByRegionCenterDeptViewonly(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[3];


        param[0] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[0].Value = objbll.Region_id;

        param[1] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[1].Value = objbll.Center_id;

        param[2] = new SqlParameter("@deptCode", SqlDbType.Int);
        param[2].Value = objbll.DeptCode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeprofileSelectByRegionCenterDeptViewOnly", param);
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
    public DataTable EmployeeprofileSelectByRegionCenterDeptDesig(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[4];


        param[0] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[0].Value = objbll.Region_id;

        param[1] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[1].Value = objbll.Center_id;

        param[2] = new SqlParameter("@deptCode", SqlDbType.Int);
        param[2].Value = objbll.DeptCode;


        param[3] = new SqlParameter("@DesigCode", SqlDbType.Int);
        param[3].Value = objbll.DesigCode;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeprofileSelectByRegionCenterDeptDesig", param);
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
    public DataTable EmployeeprofileSelectByRegionCenter(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[2];


        param[0] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[0].Value = objbll.Region_id;

        param[1] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[1].Value = objbll.Center_id;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();

            _dt = dalobj.sqlcmdFetch("EmployeeprofileSelectByRegionCenter", param);

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


    public DataTable EmployeeprofileSelectByRegionCenter_shiftTimmings(BLLEmplyeeReportTo objbll)
    {
        SqlParameter[] param = new SqlParameter[2];


        param[0] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[0].Value = objbll.Region_id;

        param[1] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[1].Value = objbll.Center_id;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeprofileSelectByRegionCenter_shiftTimmings", param);
            //if (objbll.Center_id != 0 && objbll.Region_id != 0)
            //{
            //    _dt = dalobj.sqlcmdFetch("EmployeeprofileSelectByRegionCenter_shiftTimmings", param);
            //}
            //else
            //{
            //    _dt = dalobj.sqlcmdFetch("EmployeeprofileSelectByRegionCenter", param);
            //}
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

    public int EmplyeeReportToSelectField(int _Id)
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






    public DataTable EmployeeTimmingPolicyIdFetch(string employeecode)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@employee_code", SqlDbType.VarChar);
        param[0].Value = employeecode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("sp_getEmpActivePolicy", param);
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




    #endregion


    public DataTable SingleEmployeeDetails(string employeecode)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = employeecode;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SingleEmployeeDetails", param);
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
