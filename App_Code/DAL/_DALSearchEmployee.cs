using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALSearchEmployee
/// </summary>
public class _DALSearchEmployee
{
    DALBase dalobj = new DALBase();
	public _DALSearchEmployee()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public DataTable EmployeeSelectByCritria(BLLSearchEmployee obj)
    {
        SqlParameter[] param = new SqlParameter[11];

        param[0] = new SqlParameter("@InActive", SqlDbType.NVarChar);
        if (obj.InActive == "-1"){
            obj.InActive = null;
        }
        param[0].Value = obj.InActive;
        
        param[1] = new SqlParameter("@Region_id", SqlDbType.Int);
        param[1].Value = obj.Region_Id;

        param[2] = new SqlParameter("@Center_id", SqlDbType.Int);
        param[2].Value = obj.Center_Id;

        param[3] = new SqlParameter("@DeptName", SqlDbType.NVarChar);
        if (obj.DeptName.Length == 0)
        {
            obj.DeptName = null;
        }
        param[3].Value = obj.DeptName;

        param[4] = new SqlParameter("@DesigName", SqlDbType.NVarChar);
        if (obj.DesigName.Length == 0)
        {
            obj.DesigName = null;
        }
        param[4].Value = obj.DesigName;

        param[5] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        if (obj.EmployeeCode.Length == 0) {
            obj.EmployeeCode = null;
        }
        param[5].Value = obj.EmployeeCode;

        param[6] = new SqlParameter("@EmployeeName", SqlDbType.NVarChar);
        if (obj.EmployeeName.Length == 0){
            obj.EmployeeName = null;
        }
        param[6].Value = obj.EmployeeName;

        param[7] = new SqlParameter("@EmployeeGrade", SqlDbType.NVarChar);
        if (obj.EmployeeGrade.Length == 0){
            obj.EmployeeGrade = null;
        }
        param[7].Value = obj.EmployeeGrade;

        param[8] = new SqlParameter("@Gender_Id", SqlDbType.NVarChar);
        if (obj.Gender_Id == "-1"){
           obj.Gender_Id = null;
        }
        param[8].Value = obj.Gender_Id;

        param[9] = new SqlParameter("@Religion_Name", SqlDbType.NVarChar);
        if (obj.Religion_Name == "-1")
        {
            obj.Religion_Name = null;
        }
        param[9].Value = obj.Religion_Name;

        param[10] = new SqlParameter("@IsContracual", SqlDbType.Bit);
        if (obj.IsContracual==-1){
            obj.IsContracual = null;
        }
        param[10].Value = obj.IsContracual;
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeprofileSelectBySearchCriteria", param);
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