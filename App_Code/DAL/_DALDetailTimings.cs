using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _DALDetailTimings
/// </summary>
public class _DALDetailTimings
{
    DALBase dalobj = new DALBase();
    public _DALDetailTimings()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int DetailTimingsAdd(BLLDetailTimings objbll,string centers,string designations)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[24];

            param[0] = new SqlParameter("@FromDate", SqlDbType.DateTime);
            param[0].Value = objbll.FromDate;

            param[1] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            param[1].Value = objbll.ToDate;

            param[2] = new SqlParameter("@Reason", SqlDbType.NVarChar);
            param[2].Value = objbll.Reason;

            param[3] = new SqlParameter("@TimeIn", SqlDbType.Time);
            param[3].Value = objbll.TimeIn;

            param[4] = new SqlParameter("@TimeOut", SqlDbType.Time);
            param[4].Value = objbll.TimeOut;

            param[5] = new SqlParameter("@AbsentTime", SqlDbType.Time);
            param[5].Value = objbll.AbsentTime;

            param[6] = new SqlParameter("@FriStartTime", SqlDbType.Time);
            param[6].Value = objbll.FriStartTime;

            param[7] = new SqlParameter("@FriAbsentTime", SqlDbType.Time);
            param[7].Value = objbll.FriAbsentTime;

            param[8] = new SqlParameter("@FriEndTime", SqlDbType.Time);
            param[8].Value = objbll.FriEndTime;

            param[9] = new SqlParameter("@SatStartTime", SqlDbType.Time);
            param[9].Value = objbll.SatStartTime;

            param[10] = new SqlParameter("@SatAbsentTime", SqlDbType.Time);
            param[10].Value = objbll.SatAbsentTime;

            param[11] = new SqlParameter("@SatEndTime", SqlDbType.Time);
            param[11].Value = objbll.SatEndTime;

            param[12] = new SqlParameter("@StatusId", SqlDbType.Int);
            param[12].Value = objbll.Status_Id;

            param[13] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
            param[13].Value = objbll.CreatedOn;

            param[14] = new SqlParameter("@CreatedBy", SqlDbType.Int);
            param[14].Value = objbll.CreatedBy;

            param[15] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
            param[15].Value = objbll.ModifiedOn;

            param[16] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
            param[16].Value = objbll.ModifiedBy;
           
            param[17] = new SqlParameter("@isLock", SqlDbType.Bit);
            param[17].Value = objbll.isLocked;

            param[18] = new SqlParameter("@LockedOn", SqlDbType.DateTime);
            param[18].Value = objbll.LockedOn;

            param[19] = new SqlParameter("@LockedBy", SqlDbType.Int);
            param[19].Value = objbll.LockedBy;

            param[20] = new SqlParameter("@Centers", SqlDbType.VarChar);
            param[20].Value = centers;

            param[21] = new SqlParameter("@Designations", SqlDbType.VarChar);
            param[21].Value = designations;

            param[22] = new SqlParameter("@Region_Id", SqlDbType.Int);
            param[22].Value = objbll.Region_id;
            // prepare data table for shiftcasedetail 

            DataTable dtShiftCaseDetail = new DataTable();
            dtShiftCaseDetail.Columns.Add(new DataColumn("AttDate", typeof(DateTime)));
            dtShiftCaseDetail.Columns.Add(new DataColumn("isOFF", typeof(bool)));

            foreach (BLLDateDetail detail in objbll.ShiftCaseDetailList)
            {
                DataRow pRow = dtShiftCaseDetail.NewRow();

                pRow["AttDate"] = detail.AttDate;
                pRow["isOFF"] = detail.isOff;

                dtShiftCaseDetail.Rows.Add(pRow);
            }

            SqlParameter paramShiftCaseDetail = new SqlParameter();
            paramShiftCaseDetail.ParameterName = "@ttShiftCaseDetail";
            paramShiftCaseDetail.SqlDbType = SqlDbType.Structured;
            paramShiftCaseDetail.Value = dtShiftCaseDetail;

            param[23] = paramShiftCaseDetail;


            dalobj.OpenConnection();
            int k = dalobj.sqlcmdExecute("DetailTimingsAdd", param);

            return k;
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

    public int EmployeesAppliedTimingDelete(int shiftCaseEmpId)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@shiftCaseEmpId", SqlDbType.Int);
        param[0].Value = shiftCaseEmpId;
        int k = dalobj.sqlcmdExecute("EmployeesAppliedTimingDelete", param);

        return k;
    }


    public DataTable EmployeesTimingAppliedTo(int shiftCaseId)
    {
        DataTable dt = new DataTable();

        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@ShiftCaseId", SqlDbType.Int);
        param[0].Value = shiftCaseId;
        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("EmployeesAppliedTimeTo_Select", param);
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

    public DataTable DetailTimingsSelect(BLLDetailTimings obj)
    {
        
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("DetailTimingsAdd_SelectAll");
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

    public DataTable fetchCenters(BLLDetailTimings objBll)
    {
        DataTable _dt = new DataTable();

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@region_id", SqlDbType.Int);
            param[0].Value = objBll.Region_id;

            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("GetAllCentersFromRegion", param);
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }

    public DataTable EmployeeAllDesignationSelectByRegionCenter(int Region_id)
    {
        DataTable _dt = new DataTable();

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@region_id", SqlDbType.Int);
            param[0].Value = Region_id;
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeAllDesignationSelectByRegionCenter", param);
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }


    public DataTable DesignationsSelectByCenters(int Region_id, string centers)
    {
        DataTable _dt = new DataTable();

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@region_id", SqlDbType.Int);
            param[0].Value = Region_id;

            param[1] = new SqlParameter("@Center_id", SqlDbType.VarChar);
            param[1].Value = centers;
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("EmployeeSpecificDesigSelectByRegionCenter", param);
            return _dt;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            dalobj.CloseConnection();
        }
    }
}