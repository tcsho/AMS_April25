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
/// Summary description for DALCenter
/// </summary>
public class DALCenter : DALBase
{
    DALBase dalobj = new DALBase();

    public DALCenter()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Delete(int ID, out int nAlreadyIn)
    {
        nAlreadyIn = 0;
        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("DeleteCenter", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters

        SqlParameter paraID = new SqlParameter("@ID", SqlDbType.Int, 4);
        paraID.Value = ID;
        oCommand.Parameters.Add(paraID);

        SqlParameter paraAlreadyIn = new SqlParameter("@AlreadyIn", SqlDbType.Int, 4);
        paraAlreadyIn.Direction = ParameterDirection.Output;
        oCommand.Parameters.Add(paraAlreadyIn);

        try
        {
            oConnection.Open();
            oCommand.ExecuteNonQuery();
            nAlreadyIn = (int)paraAlreadyIn.Value;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            oConnection.Close();
        }


    }

    //public void Update(string strName, string address, string phone, string email, string website, string startMon, string endMon, string centerStringId, int ID, out int nAlreadyIn)
    //{

    //    // Establish Connection
    //    SqlConnection oConnection = GetConnection();

    //    // build the command
    //    SqlCommand oCommand = new SqlCommand("UpdateCenter", oConnection);
    //    oCommand.CommandType = CommandType.StoredProcedure;

    //    // Parameters
    //    SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
    //    paraName.Value = strName;
    //    oCommand.Parameters.Add(paraName);

    //    SqlParameter paraAddress = new SqlParameter("@address", SqlDbType.NVarChar, 250);
    //    paraAddress.Value = address;
    //    oCommand.Parameters.Add(paraAddress);

    //    SqlParameter paraPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 25);
    //    paraPhone.Value = phone;
    //    oCommand.Parameters.Add(paraPhone);

    //    SqlParameter paraEmail = new SqlParameter("@email", SqlDbType.NVarChar, 50);
    //    paraEmail.Value = email;
    //    oCommand.Parameters.Add(paraEmail);

    //    SqlParameter paraWebsite = new SqlParameter("@website", SqlDbType.NVarChar, 100);
    //    paraWebsite.Value = website;
    //    oCommand.Parameters.Add(paraWebsite);

    //    SqlParameter paraSMon = new SqlParameter("@sMon", SqlDbType.NVarChar, 15);
    //    paraSMon.Value = startMon;
    //    oCommand.Parameters.Add(paraSMon);

    //    SqlParameter paraEMon = new SqlParameter("@eMon", SqlDbType.NVarChar, 15);
    //    paraEMon.Value = endMon;
    //    oCommand.Parameters.Add(paraEMon);

    //    SqlParameter centerStrId = new SqlParameter("@centerStringId", SqlDbType.NVarChar, 50);
    //    centerStrId.Value = centerStringId;
    //    oCommand.Parameters.Add(centerStrId);

    //    SqlParameter paraID = new SqlParameter("@ID", SqlDbType.Int, 4);
    //    paraID.Value = ID;
    //    oCommand.Parameters.Add(paraID);

    //    SqlParameter paraAlreadyIn = new SqlParameter("@AlreadyIn", SqlDbType.Int, 4);
    //    paraAlreadyIn.Direction = ParameterDirection.Output;
    //    oCommand.Parameters.Add(paraAlreadyIn);

    //    try
    //    {
    //        oConnection.Open();
    //        oCommand.ExecuteNonQuery();
    //        nAlreadyIn = (int)paraAlreadyIn.Value;

    //    }
    //    catch (Exception oException)
    //    {
    //        throw oException;
    //    }
    //    finally
    //    {
    //        oConnection.Close();
    //    }


    //}


    public void Update(string strName, string address, string phone, string email, string website, string centerStringId, int ID,int bankID,int cityID,out int nAlreadyIn)
    {

        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("UpdateCenter", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraAddress = new SqlParameter("@address", SqlDbType.NVarChar, 250);
        paraAddress.Value = address;
        oCommand.Parameters.Add(paraAddress);

        SqlParameter paraPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 25);
        paraPhone.Value = phone;
        oCommand.Parameters.Add(paraPhone);

        SqlParameter paraEmail = new SqlParameter("@email", SqlDbType.NVarChar, 50);
        paraEmail.Value = email;
        oCommand.Parameters.Add(paraEmail);

        SqlParameter paraWebsite = new SqlParameter("@website", SqlDbType.NVarChar, 100);
        paraWebsite.Value = website;
        oCommand.Parameters.Add(paraWebsite);

        //SqlParameter paraSMon = new SqlParameter("@sMon", SqlDbType.NVarChar, 15);
        //paraSMon.Value = startMon;
        //oCommand.Parameters.Add(paraSMon);

        //SqlParameter paraEMon = new SqlParameter("@eMon", SqlDbType.NVarChar, 15);
        //paraEMon.Value = endMon;
        //oCommand.Parameters.Add(paraEMon);

        SqlParameter centerStrId = new SqlParameter("@centerStringId", SqlDbType.NVarChar, 50);
        centerStrId.Value = centerStringId;
        oCommand.Parameters.Add(centerStrId);

        SqlParameter paraID = new SqlParameter("@ID", SqlDbType.Int, 4);
        paraID.Value = ID;
        oCommand.Parameters.Add(paraID);

        SqlParameter paraBankID = new SqlParameter("@Bank_ID", SqlDbType.Int, 4);
        paraBankID.Value = bankID;
        oCommand.Parameters.Add(paraBankID);

        //SqlParameter paraProgID = new SqlParameter("@Program_Id", SqlDbType.Int, 4);
        //paraProgID.Value = Program_Id;
        //oCommand.Parameters.Add(paraProgID);

        SqlParameter paraCityID = new SqlParameter("@City_ID", SqlDbType.Int, 4);
        paraCityID.Value = cityID;
        oCommand.Parameters.Add(paraCityID);

        SqlParameter paraAlreadyIn = new SqlParameter("@AlreadyIn", SqlDbType.Int, 4);
        paraAlreadyIn.Direction = ParameterDirection.Output;
        oCommand.Parameters.Add(paraAlreadyIn);

        try
        {
            oConnection.Open();
            oCommand.ExecuteNonQuery();
            nAlreadyIn = (int)paraAlreadyIn.Value;

        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            oConnection.Close();
        }


    }


    //public int Add(string strName, string address, string phone, string email, string website, string startMon, string endMon, string centerStringId, int regID,int clusterID, out int nAlreadyIn,int centerid)
    //{
    //    nAlreadyIn = 0;
    //    // Establish Connection
    //    SqlConnection oConnection = GetConnection();

    //    // build the command
    //    SqlCommand oCommand = new SqlCommand("AddCenter", oConnection);
    //    oCommand.CommandType = CommandType.StoredProcedure;

    //    // Parameters
    //    SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
    //    paraName.Value = strName;
    //    oCommand.Parameters.Add(paraName);

    //    SqlParameter paraAddress = new SqlParameter("@address", SqlDbType.NVarChar, 250);
    //    paraAddress.Value = address;
    //    oCommand.Parameters.Add(paraAddress);

    //    SqlParameter paraPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 25);
    //    paraPhone.Value = phone;
    //    oCommand.Parameters.Add(paraPhone);

    //    SqlParameter paraEmail = new SqlParameter("@email", SqlDbType.NVarChar, 50);
    //    paraEmail.Value = email;
    //    oCommand.Parameters.Add(paraEmail);

    //    SqlParameter paraWebsite = new SqlParameter("@website", SqlDbType.NVarChar, 100);
    //    paraWebsite.Value = website;
    //    oCommand.Parameters.Add(paraWebsite);

    //    //SqlParameter paraSMon = new SqlParameter("@sMon", SqlDbType.Int, 4);
    //    SqlParameter paraSMon = new SqlParameter("@sMon", SqlDbType.NVarChar, 15);
    //    paraSMon.Value = startMon;
    //    oCommand.Parameters.Add(paraSMon);

    //    //SqlParameter paraEMon = new SqlParameter("@eMon", SqlDbType.Int, 4);
    //    SqlParameter paraEMon = new SqlParameter("@eMon", SqlDbType.NVarChar, 15);
    //    paraEMon.Value = endMon;
    //    oCommand.Parameters.Add(paraEMon);

    //    SqlParameter paraCenterStringId = new SqlParameter("@centerStringID", SqlDbType.NVarChar, 50);
    //    paraCenterStringId.Value = centerStringId;
    //    oCommand.Parameters.Add(paraCenterStringId);

    //    SqlParameter paraRID = new SqlParameter("@regionID", SqlDbType.Int, 4);
    //    paraRID.Value = regID;
    //    oCommand.Parameters.Add(paraRID);

    //    SqlParameter paraClusterID = new SqlParameter("@Cluster_Id", SqlDbType.Int, 4);
    //    paraClusterID.Value = clusterID;
    //    oCommand.Parameters.Add(paraClusterID);

    //    SqlParameter paraAlreadyIn = new SqlParameter("@AlreadyIn", SqlDbType.Int, 4);
    //    paraAlreadyIn.Direction = ParameterDirection.Output;
    //    oCommand.Parameters.Add(paraAlreadyIn);

    //    SqlParameter paraCenterId = new SqlParameter("@centerid", SqlDbType.Int, 4);
    //    paraCenterId.Value = centerid;
    //    oCommand.Parameters.Add(paraCenterId);


    //    try
    //    {
    //        oConnection.Open();
    //        object id = oCommand.ExecuteScalar();
    //        if (id != null)
    //        {
    //            nAlreadyIn = (int)paraAlreadyIn.Value;

    //            return Int32.Parse(id.ToString());
    //        }

    //    }
    //    catch (Exception oException)
    //    {
    //        throw oException;
    //    }
    //    finally
    //    {
    //        oConnection.Close();
    //    }
    //    return -1;

    //}


    public int Add(string strName, string address, string phone, string email, string website, string centerStringId, int regID, int clusterID, int bankID, int Program_Id,int cityID, out int nAlreadyIn)
    {
        nAlreadyIn = 0;
        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("AddCenter", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraAddress = new SqlParameter("@address", SqlDbType.NVarChar, 250);
        paraAddress.Value = address;
        oCommand.Parameters.Add(paraAddress);

        SqlParameter paraPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 200);
        paraPhone.Value = phone;
        oCommand.Parameters.Add(paraPhone);

        SqlParameter paraEmail = new SqlParameter("@email", SqlDbType.NVarChar, 50);
        paraEmail.Value = email;
        oCommand.Parameters.Add(paraEmail);

        SqlParameter paraWebsite = new SqlParameter("@website", SqlDbType.NVarChar, 100);
        paraWebsite.Value = website;
        oCommand.Parameters.Add(paraWebsite);

        ////SqlParameter paraSMon = new SqlParameter("@sMon", SqlDbType.Int, 4);
        //SqlParameter paraSMon = new SqlParameter("@sMon", SqlDbType.NVarChar, 15);
        //paraSMon.Value = startMon;
        //oCommand.Parameters.Add(paraSMon);

        ////SqlParameter paraEMon = new SqlParameter("@eMon", SqlDbType.Int, 4);
        //SqlParameter paraEMon = new SqlParameter("@eMon", SqlDbType.NVarChar, 15);
        //paraEMon.Value = endMon;
        //oCommand.Parameters.Add(paraEMon);

        SqlParameter paraCenterStringId = new SqlParameter("@centerStringID", SqlDbType.NVarChar, 50);
        paraCenterStringId.Value = centerStringId;
        oCommand.Parameters.Add(paraCenterStringId);

        SqlParameter paraRID = new SqlParameter("@regionID", SqlDbType.Int, 4);
        paraRID.Value = regID;
        oCommand.Parameters.Add(paraRID);

        SqlParameter paraClusterID = new SqlParameter("@Cluster_Id", SqlDbType.Int, 4);
        paraClusterID.Value = clusterID;
        oCommand.Parameters.Add(paraClusterID);

        SqlParameter paraBankID = new SqlParameter("@Bank_ID", SqlDbType.Int, 4);
        paraBankID.Value = bankID;
        oCommand.Parameters.Add(paraBankID);

        SqlParameter paraProgID = new SqlParameter("@Program_Id", SqlDbType.Int, 4);
        paraProgID.Value = Program_Id;
        oCommand.Parameters.Add(paraProgID);


        SqlParameter paraCityID = new SqlParameter("@City_ID", SqlDbType.Int, 4);
        paraCityID.Value = cityID;
        oCommand.Parameters.Add(paraCityID);

        SqlParameter paraAlreadyIn = new SqlParameter("@AlreadyIn", SqlDbType.Int, 4);
        paraAlreadyIn.Direction = ParameterDirection.Output;
        oCommand.Parameters.Add(paraAlreadyIn);

        //SqlParameter paraCenterId = new SqlParameter("@centerid", SqlDbType.Int , 4);
        //paraCenterId.Value = centerid;
        //oCommand.Parameters.Add(paraCenterId);


        try
        {
            oConnection.Open();
            object id = oCommand.ExecuteScalar();
            if (id != null)
            {
                nAlreadyIn = (int)paraAlreadyIn.Value;

                return Int32.Parse(id.ToString());
            }

        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            oConnection.Close();
        }
        return -1;

    }
    
    public DataSet get_CenterFromRegion(int ID)
    {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetCenterFromRegion", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter para_ID = new SqlParameter("@pv_region_id", SqlDbType.Int, 4);
        para_ID.Value = ID;
        oCommand.Parameters.Add(para_ID);


        // Adapter and DataSet
        SqlDataAdapter oAdapter = new SqlDataAdapter();
        oAdapter.SelectCommand = oCommand;
        DataSet oDataSet = new DataSet();

        try
        {
            oConnection.Open();
            oAdapter.Fill(oDataSet, "obj");
            return oDataSet;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            oConnection.Close();
        }

    }

    public DataSet get_CenterFromCluster(int ID)
    {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetCenterFromCluster", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter para_ID = new SqlParameter("@pv_Cluster_id", SqlDbType.Int, 4);
        para_ID.Value = ID;
        oCommand.Parameters.Add(para_ID);


        // Adapter and DataSet
        SqlDataAdapter oAdapter = new SqlDataAdapter();
        oAdapter.SelectCommand = oCommand;
        DataSet oDataSet = new DataSet();

        try
        {
            oConnection.Open();
            oAdapter.Fill(oDataSet, "obj");
            return oDataSet;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            oConnection.Close();
        }

    }

    public string CenterSelectNextID(int RegionID)
    {

        string strCenterIDString = "";

        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("CenterSelectNextID", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters

        SqlParameter paraID = new SqlParameter("@Region_ID", SqlDbType.Int, 4);
        paraID.Value = RegionID;
        oCommand.Parameters.Add(paraID);

        SqlParameter paraAlreadyIn = new SqlParameter("@CenterIDString", SqlDbType.NVarChar, 50);
        paraAlreadyIn.Direction = ParameterDirection.Output;
        oCommand.Parameters.Add(paraAlreadyIn);

        try
        {
            oConnection.Open();
            oCommand.ExecuteNonQuery();
            strCenterIDString = paraAlreadyIn.Value.ToString();
            return strCenterIDString;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            oConnection.Close();
        }
    }

    public DataSet get_CenterFromClusterFeePackage(int ID)
    {

        // Establish Connection
        SqlConnection oConnection = GetConnection();
        // build the command
        SqlCommand oCommand = new SqlCommand("GetCenterFromClusterFeePackage", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter para_ID = new SqlParameter("@pv_Cluster_id", SqlDbType.Int, 4);
        para_ID.Value = ID;
        oCommand.Parameters.Add(para_ID);


        // Adapter and DataSet
        SqlDataAdapter oAdapter = new SqlDataAdapter();
        oAdapter.SelectCommand = oCommand;
        DataSet oDataSet = new DataSet();

        try
        {
            oConnection.Open();
            oAdapter.Fill(oDataSet, "obj");
            return oDataSet;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            oConnection.Close();
        }
    }

    public DataTable CenterSelectAll()
    {

        DataTable _dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            _dt = dalobj.sqlcmdFetch("CenterSelectAll");
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

    public DataTable CenterSelect(BLLCenter objbll)
    {
        SqlParameter[] param = new SqlParameter[1];

        param[0] = new SqlParameter("@Region_Id", SqlDbType.Int);
        param[0].Value = objbll.Region_Id;


        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("WebAtendanceSelectCenterByRegionid", param);
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



}
