using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for _DALEmployeeBusinessCard
/// </summary>
public class _DALEmployeeBusinessCard
{
    DALBase dalobj = new DALBase();


    public _DALEmployeeBusinessCard()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int EmployeeBusinessCardAdd(BLLEmployeeBusinessCard objbll)
    {
        SqlParameter[] param = new SqlParameter[7];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@ContactNumber", SqlDbType.NVarChar); param[1].Value = objbll.ContactNumber;
        param[2] = new SqlParameter("@Email", SqlDbType.NVarChar); param[2].Value = objbll.Email;
        param[3] = new SqlParameter("@Quantity", SqlDbType.Int); param[3].Value = objbll.Quantity;
        param[4] = new SqlParameter("@Other", SqlDbType.NVarChar); param[4].Value = objbll.Other;
       
        param[5] = new SqlParameter("@Remarks", SqlDbType.NVarChar); param[5].Value = objbll.Remarks;
        
        param[6] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[6].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeBusinessCardInsert", param);
        int k = (int)param[6].Value;
        return k;

    }

    public int EmployeeBusinessCardApprovalUpdate(BLLEmployeeBusinessCard objbll)
    {
        SqlParameter[] param = new SqlParameter[5];

        param[0] = new SqlParameter("@ApprovalTypeId", SqlDbType.Int); param[0].Value = objbll.ApprovalTypeId;
        param[1] = new SqlParameter("@EmpBCard_Id", SqlDbType.Int); param[1].Value = objbll.EmpBCard_Id;
        param[2] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[2].Value = objbll.EmployeeCode;
        param[3] = new SqlParameter("@EmpBCardStatus_Id", SqlDbType.Int); param[3].Value = objbll.EmpBCardStatus_Id;
         param[4] = new SqlParameter("@Remarks", SqlDbType.NVarChar); param[4].Value = objbll.Remarks;
       

         
        dalobj.sqlcmdExecute("EmployeeBusinessCardApprovalUpdate", param);
        return 1;
    }

    public int EmployeeBusinessCardUpdate(BLLEmployeeBusinessCard objbll)
    {
        SqlParameter[] param = new SqlParameter[29];
        param[0] = new SqlParameter("@EmpBCard_Id", SqlDbType.Int); param[0].Value = objbll.EmpBCard_Id;
        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar); param[1].Value = objbll.EmployeeCode;
        param[2] = new SqlParameter("@ContactNumber", SqlDbType.NVarChar); param[2].Value = objbll.ContactNumber;
        param[3] = new SqlParameter("@Email", SqlDbType.NVarChar); param[3].Value = objbll.Email;
        param[4] = new SqlParameter("@Quantity", SqlDbType.Int); param[4].Value = objbll.Quantity;
        param[5] = new SqlParameter("@Other", SqlDbType.NVarChar); param[5].Value = objbll.Other;
        param[6] = new SqlParameter("@Cost", SqlDbType.Decimal); param[6].Value = objbll.Cost;
        param[7] = new SqlParameter("@Remarks", SqlDbType.NVarChar); param[7].Value = objbll.Remarks;
        param[8] = new SqlParameter("@Region_Id", SqlDbType.Int); param[8].Value = objbll.Region_Id;
        param[9] = new SqlParameter("@Center_Id", SqlDbType.Int); param[9].Value = objbll.Center_Id;
        param[10] = new SqlParameter("@DesigCode", SqlDbType.Int); param[10].Value = objbll.DesigCode;
        param[11] = new SqlParameter("@DeptCode", SqlDbType.Int); param[11].Value = objbll.DeptCode;
        param[12] = new SqlParameter("@Phone", SqlDbType.NVarChar); param[12].Value = objbll.Phone;
        param[13] = new SqlParameter("@Fax", SqlDbType.NVarChar); param[13].Value = objbll.Fax;
        param[14] = new SqlParameter("@Web", SqlDbType.NVarChar); param[14].Value = objbll.Web;
        param[15] = new SqlParameter("@UAN", SqlDbType.NVarChar); param[15].Value = objbll.UAN;
        param[16] = new SqlParameter("@CreatedOn", SqlDbType.DateTime); param[16].Value = objbll.CreatedOn;
        param[13] = new SqlParameter("@HODBy", SqlDbType.NVarChar); param[13].Value = objbll.HODBy;
        param[14] = new SqlParameter("@HOD_EmpBCardStatus_Id", SqlDbType.Int); param[14].Value = objbll.HOD_EmpBCardStatus_Id;
        param[15] = new SqlParameter("@HOD_StatusOn", SqlDbType.DateTime); param[15].Value = objbll.HOD_StatusOn;
        param[16] = new SqlParameter("@HODRemarks", SqlDbType.NVarChar); param[16].Value = objbll.HODRemarks;
        param[17] = new SqlParameter("@HR_RD_By", SqlDbType.NVarChar); param[17].Value = objbll.HR_RD_By;
        param[18] = new SqlParameter("@HR_RD_EmpBCardStatus_Id", SqlDbType.Int); param[18].Value = objbll.HR_RD_EmpBCardStatus_Id;
        param[19] = new SqlParameter("@HR_RD_Remarks", SqlDbType.NVarChar); param[19].Value = objbll.HR_RD_Remarks;
        param[20] = new SqlParameter("@HR_RD_StatusOn", SqlDbType.DateTime); param[20].Value = objbll.HR_RD_StatusOn;
        param[21] = new SqlParameter("@CEO_By", SqlDbType.NVarChar); param[21].Value = objbll.CEO_By;
        param[22] = new SqlParameter("@CEO_EmpBCardStatus_Id", SqlDbType.Int); param[22].Value = objbll.CEO_EmpBCardStatus_Id;
        param[23] = new SqlParameter("@CEO_StatusOn", SqlDbType.DateTime); param[23].Value = objbll.CEO_StatusOn;
        param[24] = new SqlParameter("@CEORemarks", SqlDbType.NVarChar); param[24].Value = objbll.CEORemarks;
        param[25] = new SqlParameter("@Status_Id", SqlDbType.Int); param[25].Value = objbll.Status_Id;
        param[26] = new SqlParameter("@ReceivedOn", SqlDbType.Int); param[26].Value = objbll.ReceivedOn;
        param[27] = new SqlParameter("@EmpBCardStatus_Id", SqlDbType.Int); param[27].Value = objbll.EmpBCardStatus_Id;


        param[28] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[28].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("EmployeeBusinessCardUpdate", param);
        int k = (int)param[28].Value;
        return k;
    }
    public int EmployeeBusinessCardDelete(BLLEmployeeBusinessCard objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmpBCard_Id", SqlDbType.Int);
        param[0].Value = objbll.EmpBCard_Id;


        int k = dalobj.sqlcmdExecute("EmployeeBusinessCardDelete", param);

        return k;
    }
    #endregion

    #region 'Start of Fetch Methods'
    public DataTable EmployeeBusinessCardSelect(int _id)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmpBCard_Id", SqlDbType.Int);
        param[0].Value = _id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeBusinessCardSelectById", param);
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

    public DataTable EmployeeBusinessCardSelect(BLLEmployeeBusinessCard objbll)
    {
        
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeBusinessCardSelectAll");
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

    public DataTable EmployeeBusinessCardSelectByStatusID(BLLEmployeeBusinessCard objbll)
    {
        DataTable dt = new DataTable();
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Status_ID", SqlDbType.Int);
        param[0].Value = objbll.Status_Id;
        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeBusinessCardSelectByStatusID",param);
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

    public DataTable EmployeeProfileForBCardSelectByCode(BLLEmployeeBusinessCard obj)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = obj.EmployeeCode;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeProfileForBusinessCardSelectByCode", param);
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

    public DataTable EmployeeBusinessCardSelectAllByEmpCode(BLLEmployeeBusinessCard obj)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = obj.EmployeeCode;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeBusinessCardSelectAllByEmpCode", param);
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

    public DataTable EmployeeBusinessCardSelectAllForPrinting( )
    {
        

        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeBusinessCardSelectAllForPrinting");
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

    public DataTable EmployeeBusinessCardSelectAllByApprovalEmpCode(BLLEmployeeBusinessCard obj)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@ApprovalTypeId", SqlDbType.Int);
        param[0].Value = obj.ApprovalTypeId;
        param[1] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[1].Value = obj.EmployeeCode.Trim();

        param[2] = new SqlParameter("@EmpBCardStatus_Id", SqlDbType.Int);
        param[2].Value = obj.EmpBCardStatus_Id;

        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeeBusinessCardSelectAllByApprovalEmpCode", param);
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

    public int EmployeeBusinessCardPendingOrderSelectByEmpCode(BLLEmployeeBusinessCard objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.Int);
        param[0].Value = objbll.EmployeeCode;

        int k=1;
        DataTable dt = dalobj.sqlcmdFetch("EmployeeBusinessCardIsPendingOrderSelectByEmpCode", param);
        if (dt.Rows.Count > 0)
        {
            k = Convert.ToInt32(dt.Rows[0][0].ToString());
            if (k == 0)
            {
                k = 1; // no pending request
            }
            else
            {
                k = 0; //having pending card request}
            }
        }
        else
            k = 1; // no pending request
      
           
        return k;
    }

    #endregion


}
