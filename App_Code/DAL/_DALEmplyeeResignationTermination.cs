using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for _DALEmplyeeReportTo
/// </summary>
/// 
public class _DALEmplyeeResignationTermination
{
    DALBase dalobj = new DALBase();

    protected static string strERPConnect;
    public _DALEmplyeeResignationTermination()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region 'Start of Execution Methods'
    public int ResignationTerminationAdd(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[16];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@Category", SqlDbType.NVarChar);
        param[1].Value = objbll.Category;

        param[2] = new SqlParameter("@SubmissionDate", SqlDbType.DateTime);
        param[2].Value = objbll.SubmissionDate;

        param[3] = new SqlParameter("@LastWorkingDate", SqlDbType.DateTime);
        param[3].Value = objbll.LastWorkingDate;

        if (objbll.Category == "Resignation")
        {
            param[4] = new SqlParameter("@ApprovedDate", SqlDbType.DateTime);
            param[4].Value = DBNull.Value;

            param[5] = new SqlParameter("@HODApprove", SqlDbType.Bit);
            param[5].Value = DBNull.Value;

            param[6] = new SqlParameter("@LastWorkingDateRemarks", SqlDbType.NVarChar);
            param[6].Value = objbll.LastWorkingDateRemarks;
        }
        else
        {
            param[4] = new SqlParameter("@ApprovedDate", SqlDbType.DateTime);
            param[4].Value = objbll.ApprovedDate;

            param[5] = new SqlParameter("@HODApprove", SqlDbType.Bit);
            param[5].Value = objbll.HODApprove;

            param[6] = new SqlParameter("@LastWorkingDateRemarks", SqlDbType.NVarChar);
            param[6].Value = DBNull.Value;
        }
        param[7] = new SqlParameter("@NoticePeriod", SqlDbType.Int);
        param[7].Value = objbll.NoticePeriod;

        param[8] = new SqlParameter("@Reason", SqlDbType.NVarChar);
        param[8].Value = objbll.Reason;

        param[9] = new SqlParameter("@Comments", SqlDbType.NVarChar);
        param[9].Value = objbll.Comments;

        param[10] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        param[10].Value = objbll.CreatedOn;

        param[11] = new SqlParameter("@CreateBy", SqlDbType.NVarChar);
        param[11].Value = objbll.CreatedBy;

        param[12] = new SqlParameter("@ReportTo", SqlDbType.NVarChar);
        param[12].Value = objbll.ReportTo;

        param[13] = new SqlParameter("@StatusId", SqlDbType.NVarChar);
        param[13].Value = objbll.StatusId;

        param[14] = new SqlParameter("@IsEmailSent", SqlDbType.Bit);
        param[14].Value = objbll.IsEmailSent;

        param[15] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[15].Direction = ParameterDirection.Output;

        dalobj.sqlcmdExecute("WebResignationTerminationINSERT", param);
        int k = (int)param[15].Value;
        return k;

    }

    public int EmployeeResignationUpdateHOD(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[11];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@HODApprove", SqlDbType.Bit);
        param[1].Value = objbll.HODApprove;

        param[2] = new SqlParameter("@HODRemarks", SqlDbType.NVarChar);
        param[2].Value = objbll.HODRemarks;

        param[3] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        param[3].Value = objbll.ModifiedOn;

        param[4] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar);
        param[4].Value = objbll.ModifiedBy;

        param[5] = new SqlParameter("@StatusId", SqlDbType.Int);
        param[5].Value = objbll.StatusId;

        param[6] = new SqlParameter("@ApprovedDate", SqlDbType.DateTime);
        param[6].Value = objbll.ApprovedDate;

        param[7] = new SqlParameter("@LastWorkingDate", SqlDbType.DateTime);
        param[7].Value = objbll.LastWorkingDate;

        param[8] = new SqlParameter("@NoticePeriod", SqlDbType.Int);
        param[8].Value = objbll.NoticePeriod;

        param[9] = new SqlParameter("@HODProposeDate", SqlDbType.DateTime);
        param[9].Value = objbll.HODProposeDate;

        param[10] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[10].Direction = ParameterDirection.Output;

        int k = dalobj.sqlcmdExecute("WebEmployeeResignationUpdateHOD", param);
        return k;
    }

    internal object GetCenterAndRegionIdByEmployee(string employeeCode)
    {
        throw new NotImplementedException();
    }

    public int EmployeeResignationEmailUpdate(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@IsEmailSent", SqlDbType.Bit);
        param[1].Value = objbll.IsEmailSent;

        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        int k = dalobj.sqlcmdExecute("WebEmployeeResignationEmailUpdate", param);
        return k;
    }

    public int EmployeeResignationERPUpdate(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@IsSentToERP", SqlDbType.Bit);
        param[1].Value = objbll.IsSentToERP;

        param[2] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        int k = dalobj.sqlcmdExecute("WebEmployeeResignationERPUpdate", param);
        return k;
    }

    public int ResignationTerminationReversalUpdate(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[3];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar);
        param[1].Value = objbll.ModifiedBy;
        param[2] = new SqlParameter("@HRRemarks", SqlDbType.NVarChar);
        param[2].Value = objbll.HRRemarks;

        return dalobj.sqlcmdExecute("ResignationTerminationReversalUpdate", param);
    }

    public int UpdateEmployeeLastWorkingDate(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[4];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar);
        param[1].Value = objbll.ModifiedBy;
        param[2] = new SqlParameter("@LastWorkingDate", SqlDbType.DateTime);
        param[2].Value = objbll.LastWorkingDate;
        param[3] = new SqlParameter("@HRRemarks", SqlDbType.NVarChar);
        param[3].Value = objbll.HRRemarks;

        return dalobj.sqlcmdExecute("UpdateEmployeeLastWorkingDate", param);
    }

    public int EmployeeTerminationUpdateHODSupervisor(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[10];

        param[0] = new SqlParameter("@EmployeeCode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@HODSupervisorApprove", SqlDbType.Bit);
        param[1].Value = objbll.HODSupervisorApprove;

        param[2] = new SqlParameter("@HODSupervisorRemarks", SqlDbType.NVarChar);
        param[2].Value = objbll.HODSupervisorRemarks;

        param[3] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        param[3].Value = objbll.ModifiedOn;

        param[4] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar);
        param[4].Value = objbll.ModifiedBy;

        param[5] = new SqlParameter("@StatusId", SqlDbType.Int);
        param[5].Value = objbll.StatusId;

        param[6] = new SqlParameter("@LastWorkingDate", SqlDbType.DateTime);
        param[6].Value = objbll.LastWorkingDate;

        param[7] = new SqlParameter("@NoticePeriod", SqlDbType.Int);
        param[7].Value = objbll.NoticePeriod;

        param[8] = new SqlParameter("@HODSupervisorApprovedDate", SqlDbType.DateTime);
        param[8].Value = objbll.HODSupervisorApprovedDate;

        param[9] = new SqlParameter("@AlreadyIn", SqlDbType.Int);
        param[9].Direction = ParameterDirection.Output;

        int k = dalobj.sqlcmdExecute("WebEmployeeTerminationUpdateHOD", param);
        return k;
    }

    #endregion

    #region 'Start of Fetch Methods'

    public DataTable ResignationSelectHOD(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@HODApprove", SqlDbType.Bit);
        param[1].Value = objbll.HODApprove;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebResignationSelectHOD", param);
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

    public DataTable ResignationTerminationReversalSelectEmployee(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@employeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;
        param[1] = new SqlParameter("@RegionId", SqlDbType.Int);
        param[1].Value = objbll.Region_Id;
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("ResignationTerminationReversalSelectEmployee", param);
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

    public DataTable ResignationSelectForAutoApproval()
    {
        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("ResignationSelectForAutoApproval");
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

    public string EmployeeResignationDataToERP(BLLEmployeeResignationTermination objbll)
    {
        string returnOutput = string.Empty;
        string conn = ConfigurationManager.ConnectionStrings["ERPDB"].ConnectionString;

        using (OracleConnection cnx = new OracleConnection(conn))
        {
            using (OracleCommand commProc = new OracleCommand())
            {
                commProc.Connection = cnx;
                commProc.CommandText = "APPS.cust_end_emp";
                commProc.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                commProc.Parameters.Add(new OracleParameter("P_empno", OracleDbType.Int32)
                {
                    Value = objbll.EmployeeCode,
                    Direction = ParameterDirection.Input
                });

                commProc.Parameters.Add(new OracleParameter("P_business_group_id", OracleDbType.Int32)
                {
                    Value = 4129,
                    Direction = ParameterDirection.Input
                });

                commProc.Parameters.Add(new OracleParameter("P_reason", OracleDbType.NVarchar2, 50)
                {
                    Value = objbll.Reason == "Deceased" ? "D" :
                        objbll.Reason == "Gross misconduct" ? "G" :
                        objbll.Reason == "Retirement" ? "R" :
                        objbll.Reason == "Resigned" ? "EMP_TRANS" :
                        objbll.Reason == "Terminated" ? "TERMINATED" :
                        objbll.Reason == "Terminated on performance" ? "TER_PERF" :
                        objbll.Reason == "Contract ended" ? "CONTRACT_ENDED" :
                        ""
                });

                commProc.Parameters.Add(new OracleParameter("P_effective_date", OracleDbType.Date)
                {
                    Value = objbll.SubmissionDate,
                    Direction = ParameterDirection.Input
                });

                commProc.Parameters.Add(new OracleParameter("P_last_working_date", OracleDbType.Date)
                {
                    Value = objbll.LastWorkingDate,
                    Direction = ParameterDirection.Input
                });

                commProc.Parameters.Add(new OracleParameter("p_status", OracleDbType.Varchar2, 255)
                {
                    Direction = ParameterDirection.Output
                });

                try
                {
                    cnx.Open();
                    commProc.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    if (commProc.Parameters["p_status"].Value != DBNull.Value)
                    {
                        returnOutput = commProc.Parameters["p_status"].Value.ToString();
                    }
                    else
                    {
                        returnOutput = "No status returned";
                    }
                }
                catch (Exception ex)
                {
                    // Wrap exception with more context
                    throw new Exception("Error in EmployeeResignationDataToERP", ex);
                }
            }
        }

        return returnOutput;
    }


    //public string EmployeeResignationDataToERP(BLLEmployeeResignationTermination objbll)
    //{
    //    string returnOutput = "";
    //    string conn = ConfigurationManager.ConnectionStrings["ERPDB"].ConnectionString;

    //    using (OracleConnection cnx = new OracleConnection(conn))
    //    {
    //        OracleCommand commProc = new OracleCommand();
    //        commProc.Connection = cnx;
    //        commProc.CommandText = "APPS.cust_end_emp";
    //        commProc.CommandType = System.Data.CommandType.StoredProcedure;

    //        commProc.Parameters.Add(new OracleParameter("P_empno", OracleDbType.Int32)
    //        {
    //            Value = objbll.EmployeeCode
    //        });

    //        commProc.Parameters.Add(new OracleParameter("P_business_group_id", OracleDbType.Int32)
    //        {
    //            Value = 4129
    //        });

    //        commProc.Parameters.Add(new OracleParameter("P_reason", OracleDbType.NVarchar2)
    //        {
    //            Value = objbll.Reason == "Deceased" ? "D" :
    //                    objbll.Reason == "Gross misconduct" ? "G" :
    //                    objbll.Reason == "Retirement" ? "R" :
    //                    objbll.Reason == "Resigned" ? "EMP_TRANS" :
    //                    objbll.Reason == "Terminated" ? "TERMINATED" :
    //                    objbll.Reason == "Terminated on performance" ? "TER_PERF" :
    //                    objbll.Reason == "Contract ended" ? "CONTRACT_ENDED" :
    //                    ""
    //        });

    //        commProc.Parameters.Add(new OracleParameter("P_effective_date", OracleDbType.Date)
    //        {
    //            Value = objbll.SubmissionDate
    //        });

    //        commProc.Parameters.Add(new OracleParameter("P_last_working_date", OracleDbType.Date)
    //        {
    //            Value = objbll.LastWorkingDate
    //        });

    //        // Assuming there is an output parameter named "p_status"
    //        commProc.Parameters.Add(new OracleParameter("p_status", OracleDbType.Varchar2, 255)
    //        {
    //            Direction = ParameterDirection.Output
    //        });

    //        try
    //        {
    //            cnx.Open();
    //            //commProc.ExecuteNonQuery();

    //            // Assuming p_status is a string output parameter
    //            string outputV1 = commProc.Parameters["p_status"].Value.ToString();

    //            // Modify test based on the outcome of the stored procedure execution
    //            returnOutput = outputV1; // or any other value indicating success

    //            // Console.WriteLine($"Command executed successfully");
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //            // Handle the exception as needed
    //        }
    //        return returnOutput;
    //    }
    //}


    //public string UpdateEmployeeLastWorkingDateToERP(BLLEmployeeResignationTermination objbll)
    //{
    //    string returnOutput = "";
    //    string conn = ConfigurationManager.ConnectionStrings["ERPDB"].ConnectionString;

    //    using (OracleConnection cnx = new OracleConnection(conn))
    //    {
    //        OracleCommand commProc = new OracleCommand();
    //        commProc.Connection = cnx;
    //        commProc.CommandText = "APPS.cust_end_emp";
    //        commProc.CommandType = System.Data.CommandType.StoredProcedure;

    //        commProc.Parameters.Add(new OracleParameter("P_empno", OracleDbType.Int32)
    //        {
    //            Value = objbll.EmployeeCode
    //        });

    //        commProc.Parameters.Add(new OracleParameter("P_business_group_id", OracleDbType.Int32)
    //        {
    //            Value = 4129
    //        });

    //        commProc.Parameters.Add(new OracleParameter("P_reason", OracleDbType.NVarchar2)
    //        {
    //            Value = objbll.Reason == "Deceased" ? "D" :
    //                    objbll.Reason == "Gross misconduct" ? "G" :
    //                    objbll.Reason == "Retirement" ? "R" :
    //                    objbll.Reason == "Resigned" ? "EMP_TRANS" :
    //                    objbll.Reason == "Terminated" ? "TERMINATED" :
    //                    objbll.Reason == "Terminated on performance" ? "TER_PERF" :
    //                    objbll.Reason == "Contract ended" ? "CONTRACT_ENDED" :
    //                    ""
    //        });

    //        commProc.Parameters.Add(new OracleParameter("P_effective_date", OracleDbType.Date)
    //        {
    //            Value = objbll.SubmissionDate
    //        });

    //        commProc.Parameters.Add(new OracleParameter("P_last_working_date", OracleDbType.Date)
    //        {
    //            Value = objbll.LastWorkingDate
    //        });

    //        // Assuming there is an output parameter named "p_status"
    //        commProc.Parameters.Add(new OracleParameter("p_status", OracleDbType.Varchar2, 255)
    //        {
    //            Direction = ParameterDirection.Output
    //        });

    //        try
    //        {
    //            cnx.Open();
    //            //commProc.ExecuteNonQuery();

    //            // Assuming p_status is a string output parameter
    //            string outputV1 = commProc.Parameters["p_status"].Value.ToString();

    //            // Modify test based on the outcome of the stored procedure execution
    //            returnOutput = outputV1; // or any other value indicating success

    //            // Console.WriteLine($"Command executed successfully");
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //            // Handle the exception as needed
    //        }
    //        return returnOutput;
    //    }
    //}

    public string UpdateEmployeeLastWorkingDateToERP(BLLEmployeeResignationTermination objbll)
    {
        string returnOutput = string.Empty;
        string conn = ConfigurationManager.ConnectionStrings["ERPDB"].ConnectionString;

        using (OracleConnection cnx = new OracleConnection(conn))
        {
            using (OracleCommand commProc = new OracleCommand())
            {
                commProc.Connection = cnx;
                commProc.CommandText = "APPS.cust_end_emp";
                commProc.CommandType = CommandType.StoredProcedure;

                // Adding parameters
                commProc.Parameters.Add(new OracleParameter("P_empno", OracleDbType.Int32)
                {
                    Value = objbll.EmployeeCode,
                    Direction = ParameterDirection.Input
                });

                commProc.Parameters.Add(new OracleParameter("P_business_group_id", OracleDbType.Int32)
                {
                    Value = 4129,
                    Direction = ParameterDirection.Input
                });

                commProc.Parameters.Add(new OracleParameter("P_reason", OracleDbType.NVarchar2, 50)
                {
                    Value = objbll.Reason == "Deceased" ? "D" :
                        objbll.Reason == "Gross misconduct" ? "G" :
                        objbll.Reason == "Retirement" ? "R" :
                        objbll.Reason == "Resigned" ? "EMP_TRANS" :
                        objbll.Reason == "Terminated" ? "TERMINATED" :
                        objbll.Reason == "Terminated on performance" ? "TER_PERF" :
                        objbll.Reason == "Contract ended" ? "CONTRACT_ENDED" :
                        ""
                });

                commProc.Parameters.Add(new OracleParameter("P_effective_date", OracleDbType.Date)
                {
                    Value = objbll.SubmissionDate,
                    Direction = ParameterDirection.Input
                });

                commProc.Parameters.Add(new OracleParameter("P_last_working_date", OracleDbType.Date)
                {
                    Value = objbll.LastWorkingDate,
                    Direction = ParameterDirection.Input
                });

                commProc.Parameters.Add(new OracleParameter("p_status", OracleDbType.Varchar2, 255)
                {
                    Direction = ParameterDirection.Output
                });

                try
                {
                    cnx.Open();
                    commProc.ExecuteNonQuery();

                    // Retrieve the output parameter value after execution
                    if (commProc.Parameters["p_status"].Value != DBNull.Value)
                    {
                        returnOutput = commProc.Parameters["p_status"].Value.ToString();
                    }
                    else
                    {
                        returnOutput = "No status returned";
                    }
                }
                catch (Exception ex)
                {
                    // Wrap exception with more context
                    throw new Exception("Error in UpdateEmployeeLastWorkingDateToERP", ex);
                }
            }
        }

        return returnOutput;
    }

    public string ReverseEmployeeResignationTerminationInERP(BLLEmployeeResignationTermination objbll)
    {
        string returnOutput = string.Empty;
        string conn = ConfigurationManager.ConnectionStrings["ERPDB"].ConnectionString;

        using (OracleConnection cnx = new OracleConnection(conn))
        {
            using (OracleCommand commProc = new OracleCommand())
            {
                commProc.Connection = cnx;
                commProc.CommandText = "APPS.CUST_RESIGNATION_REVERSAL_PRO";
                commProc.CommandType = CommandType.StoredProcedure;

                // Input parameter
                commProc.Parameters.Add(new OracleParameter("EMP_NO", OracleDbType.Int32)
                {
                    Value = objbll.EmployeeCode,
                    Direction = ParameterDirection.Input
                });

                // Output parameter
                commProc.Parameters.Add(new OracleParameter("p_status", OracleDbType.Varchar2, 200) // Specify size for VARCHAR2
                {
                    Direction = ParameterDirection.Output
                });

                try
                {
                    cnx.Open();
                    commProc.ExecuteNonQuery();

                    // Retrieve the output parameter value after execution
                    if (commProc.Parameters["p_status"].Value != DBNull.Value)
                    {
                        returnOutput = commProc.Parameters["p_status"].Value.ToString();
                    }
                    else
                    {
                        returnOutput = "No status returned";
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception if required
                    throw new Exception("Error while executing ReverseEmployeeResignationTerminationInERP", ex);
                }
            }
        }

        return returnOutput;
    }

    //public string ReverseEmployeeResignationTerminationInERP(BLLEmployeeResignationTermination objbll)
    //{
    //    string returnOutput = "";
    //    string conn = ConfigurationManager.ConnectionStrings["ERPDB"].ConnectionString;

    //    using (OracleConnection cnx = new OracleConnection(conn))
    //    {
    //        OracleCommand commProc = new OracleCommand();
    //        commProc.Connection = cnx;
    //        commProc.CommandText = "APPS.CUST_RESIGNATION_REVERSAL_PRO";
    //        commProc.CommandType = System.Data.CommandType.StoredProcedure;

    //        commProc.Parameters.Add(new OracleParameter("EMP_NO", OracleDbType.Int32)
    //        {
    //            Value = objbll.EmployeeCode
    //        });

    //        // Assuming there is an output parameter named "p_status"
    //        commProc.Parameters.Add(new OracleParameter("p_status", OracleDbType.Varchar2)
    //        {
    //            Direction = ParameterDirection.Output
    //        });

    //        try
    //        {
    //            cnx.Open();
    //            //commProc.ExecuteNonQuery();

    //            // Assuming p_status is a string output parameter
    //            string outputV1 = commProc.Parameters["p_status"].Value.ToString();

    //            // Modify test based on the outcome of the stored procedure execution
    //            returnOutput = outputV1; // or any other value indicating success

    //            // Console.WriteLine($"Command executed successfully");
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //            // Handle the exception as needed
    //        }
    //        return returnOutput;
    //    }
    //} 
    public DataTable ResignationSelectHODSupervisor(BLLEmployeeResignationTermination objbll)
    {
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@SupervisorEmployeecode", SqlDbType.NVarChar);
        param[0].Value = objbll.EmployeeCode;

        param[1] = new SqlParameter("@HODSupervisorApprove", SqlDbType.Bit);
        param[1].Value = objbll.HODSupervisorApprove;

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("WebResignationSelectHODSupervisor", param);
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