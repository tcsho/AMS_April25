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
using System.Collections.Generic;

/// <summary>
/// Summary description for _DALSearchStudent
/// </summary>
public class DALSearchStudent
{
    DALBase dalobj = new DALBase();


    public DALSearchStudent()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int SearchStudentAdd(BLLSearchStudent objbll)
    {
        SqlParameter[] param = new SqlParameter[15];

        param[14] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[14].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("SearchStudentInsert", param);
        int k = (int)param[14].Value;
        return k;

    }
    public int SearchStudentUpdate(BLLSearchStudent objbll)
    {
        SqlParameter[] param = new SqlParameter[10];

 
        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("SearchStudentUpdate", param);
        int k = (int)param[9].Value;
        return k;
    }
    public int SearchStudentDelete(BLLSearchStudent objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@SearchStudent_Id", SqlDbType.Int);
     //   param[0].Value = objbll.SearchStudent_Id;


        int k = dalobj.sqlcmdExecute("SearchStudentDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable SearchStudentSelect(int _id)
    {
    SqlParameter[] param = new SqlParameter[3];

    param[0] = new SqlParameter("@sp_student_id", SqlDbType.Int);
    param[0].Value = _id;


    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("SearchStudentSelectById", param);
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
    public DataTable SearchStudent_UnassignSubject(BLLSearchStudent objbll)
    {

        SqlParameter[] param = new SqlParameter[15];


        param[0] = new SqlParameter("@sp_firstName", SqlDbType.NVarChar);
        param[0].Value = objbll.First_Name;

        param[1] = new SqlParameter("@sp_lastName", SqlDbType.NVarChar);
        param[1].Value = objbll.Last_Name;

        param[2] = new SqlParameter("@sp_middleName", SqlDbType.NVarChar);
        param[2].Value = objbll.Middle_Name;

        param[3] = new SqlParameter("@sp_dateOfBirth", SqlDbType.NChar);
        param[3].Value = objbll.Date_Of_Birth;

        param[4] = new SqlParameter("@sp_gender", SqlDbType.NChar);
        param[4].Value = objbll.Gender_Id;

        param[5] = new SqlParameter("@sp_studentNo", SqlDbType.NVarChar);
        param[5].Value = objbll.Student_No;

        param[6] = new SqlParameter("@sp_region", SqlDbType.NChar);
        param[6].Value = objbll.Region_Id;

        param[7] = new SqlParameter("@sp_studentStatus", SqlDbType.NChar);
        param[7].Value = objbll.Student_Status_Id;

        param[8] = new SqlParameter("@sp_center", SqlDbType.NChar);
        param[8].Value = objbll.Center_Id;

        param[9] = new SqlParameter("@sp_class", SqlDbType.NVarChar);
        param[9].Value = objbll.Grade_Id;


        param[10] = new SqlParameter("@sp_section", SqlDbType.NChar);
        param[10].Value = objbll.Section_Id;

        param[11] = new SqlParameter("@sp_mainOrganisationID", SqlDbType.NChar);
        param[11].Value = objbll.Main_Organisation_Id;

        param[12] = new SqlParameter("@sp_teacher", SqlDbType.NChar);
        param[12].Value = objbll.Teacher_Id;

        param[13] = new SqlParameter("@sp_end_index", SqlDbType.NChar);
        param[13].Value = objbll.EndIndex;

        param[14] = new SqlParameter("@sp_start_index", SqlDbType.NChar);
        param[14].Value = objbll.StartIndex;


        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SearchStudent_UnassignSubject", param);
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

    
    public DataTable SearchStudentSelect(BLLSearchStudent objbll)
    {

    SqlParameter[] param = new SqlParameter[15];


    param[0] = new SqlParameter("@sp_firstName", SqlDbType.NVarChar);
    param[0].Value = objbll.First_Name;

    param[1] = new SqlParameter("@sp_lastName", SqlDbType.NVarChar);
    param[1].Value = objbll.Last_Name;

    param[2] = new SqlParameter("@sp_middleName", SqlDbType.NVarChar);
    param[2].Value = objbll.Middle_Name;

    param[3] = new SqlParameter("@sp_dateOfBirth", SqlDbType.NChar);
    param[3].Value = objbll.Date_Of_Birth;

    param[4] = new SqlParameter("@sp_gender", SqlDbType.NChar);
    param[4].Value = objbll.Gender_Id;

    param[5] = new SqlParameter("@sp_studentNo", SqlDbType.NVarChar);
    param[5].Value = objbll.Student_No;

    param[6] = new SqlParameter("@sp_region", SqlDbType.NChar);
    param[6].Value = objbll.Region_Id;

    param[7] = new SqlParameter("@sp_studentStatus", SqlDbType.NChar);
    param[7].Value = objbll.Student_Status_Id;

    param[8] = new SqlParameter("@sp_center", SqlDbType.NChar);
    param[8].Value = objbll.Center_Id;

    param[9] = new SqlParameter("@sp_class", SqlDbType.NVarChar);
    param[9].Value = objbll.Grade_Id;


    param[10] = new SqlParameter("@sp_section", SqlDbType.NChar);
    param[10].Value = objbll.Section_Id;

    param[11] = new SqlParameter("@sp_mainOrganisationID", SqlDbType.NChar);
    param[11].Value = objbll.Main_Organisation_Id;

    param[12] = new SqlParameter("@sp_teacher", SqlDbType.NChar);
    param[12].Value = objbll.Teacher_Id;

    param[13] = new SqlParameter("@sp_end_index", SqlDbType.NChar);
    param[13].Value = objbll.EndIndex;

    param[14] = new SqlParameter("@sp_start_index", SqlDbType.NChar);
    param[14].Value = objbll.StartIndex;
            

    DataTable _dt = new DataTable();

    try
        {
        dalobj.OpenConnection();
        _dt = dalobj.sqlcmdFetch("SearchStudent_New", param);
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
    public DataTable SearchStudentResultCard(BLLSearchStudent objbll)
    {

        SqlParameter[] param = new SqlParameter[1];


        param[0] = new SqlParameter("@Student_Id", SqlDbType.Int);
        param[0].Value = objbll.Student_Id;
         
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SearchStudentResultCard", param);
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


    public DataTable SearchStudentSubjectData(BLLSearchStudent objbll)
    {

        SqlParameter[] param = new SqlParameter[2];


        param[0] = new SqlParameter("@Student_Id", SqlDbType.Int);
        param[0].Value = objbll.Student_Id;



        param[1] = new SqlParameter("@Session_Id", SqlDbType.Int);
        param[1].Value = objbll.Session_Id;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SearchStudentSubjectList", param);
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

    public DataTable SearchStudentSelectExport(BLLSearchStudent objbll)
        {

        SqlParameter[] param = new SqlParameter[13];


        param[0] = new SqlParameter("@sp_firstName", SqlDbType.NVarChar);
        param[0].Value = objbll.First_Name;

        param[1] = new SqlParameter("@sp_lastName", SqlDbType.NVarChar);
        param[1].Value = objbll.Last_Name;

        param[2] = new SqlParameter("@sp_middleName", SqlDbType.NVarChar);
        param[2].Value = objbll.Middle_Name;

        param[3] = new SqlParameter("@sp_dateOfBirth", SqlDbType.NChar);
        param[3].Value = objbll.Date_Of_Birth;

        param[4] = new SqlParameter("@sp_gender", SqlDbType.NChar);
        param[4].Value = objbll.Gender_Id;

        param[5] = new SqlParameter("@sp_studentNo", SqlDbType.NVarChar);
        param[5].Value = objbll.Student_No;

        param[6] = new SqlParameter("@sp_region", SqlDbType.NChar);
        param[6].Value = objbll.Region_Id;

        param[7] = new SqlParameter("@sp_studentStatus", SqlDbType.NChar);
        param[7].Value = objbll.Student_Status_Id;

        param[8] = new SqlParameter("@sp_center", SqlDbType.NChar);
        param[8].Value = objbll.Center_Id;

        param[9] = new SqlParameter("@sp_class", SqlDbType.NVarChar);
        param[9].Value = objbll.Grade_Id;


        param[10] = new SqlParameter("@sp_section", SqlDbType.NChar);
        param[10].Value = objbll.Section_Id;

        param[11] = new SqlParameter("@sp_mainOrganisationID", SqlDbType.NChar);
        param[11].Value = objbll.Main_Organisation_Id;

        param[12] = new SqlParameter("@sp_teacher", SqlDbType.NChar);
        param[12].Value = objbll.Teacher_Id;


        DataTable _dt = new DataTable();

        try
            {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SearchStudent_Export", param);
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
    
    public DataTable SearchStudentSelectCount(BLLSearchStudent objbll)
        {



        SqlParameter[] param = new SqlParameter[13];


        param[0] = new SqlParameter("@sp_firstName", SqlDbType.NVarChar);
        param[0].Value = objbll.First_Name;

        param[1] = new SqlParameter("@sp_lastName", SqlDbType.NVarChar);
        param[1].Value = objbll.Last_Name;

        param[2] = new SqlParameter("@sp_middleName", SqlDbType.NVarChar);
        param[2].Value = objbll.Middle_Name;
        
        param[3] = new SqlParameter("@sp_dateOfBirth", SqlDbType.NChar);
        param[3].Value = objbll.Date_Of_Birth;

        param[4] = new SqlParameter("@sp_gender", SqlDbType.NChar);
        param[4].Value = objbll.Gender_Id;

        param[5] = new SqlParameter("@sp_studentNo", SqlDbType.NVarChar);
        param[5].Value = objbll.Student_No;
        
        param[6] = new SqlParameter("@sp_region", SqlDbType.NChar);
        param[6].Value = objbll.Region_Id;

        param[7] = new SqlParameter("@sp_studentStatus", SqlDbType.NChar);
        param[7].Value = objbll.Student_Status_Id;

        param[8] = new SqlParameter("@sp_center", SqlDbType.NChar);
        param[8].Value = objbll.Center_Id;

        param[9] = new SqlParameter("@sp_class", SqlDbType.NVarChar);
        param[9].Value = objbll.Grade_Id;


        param[10] = new SqlParameter("@sp_section", SqlDbType.NChar);
        param[10].Value = objbll.Section_Id;

        param[11] = new SqlParameter("@sp_mainOrganisationID", SqlDbType.NChar); 
        param[11].Value = objbll.Main_Organisation_Id;

        param[12] = new SqlParameter("@sp_teacher", SqlDbType.NChar); 
        param[12].Value = objbll.Teacher_Id;

        
        DataTable _dt = new DataTable();

        try
            {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SearchStudentCount_New", param);
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
    public DataTable SearchStudentSelectByStatusID(BLLSearchStudent objbll)
    {
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("SearchStudentSelectByStatusID");
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




    #endregion


}
