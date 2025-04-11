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
/// Summary description for DALRegion
/// </summary>
public class DALRegion : DALBase
{
    DALBase dalobj = new DALBase();
	public DALRegion()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet get_countries()
    {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetAllCountries", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        //SqlParameter para_city = new SqlParameter("@para_city", SqlDbType.Char, 50);
        //para_city.Value = location;
        //oCommand.Parameters.Add(para_city);


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

    public int Delete(int ID)
    {

        // Establish Connection
        SqlConnection oConnection = GetConnection();
        int statusCode = -1;

        // build the command
        SqlCommand oCommand = new SqlCommand("DeleteRegion", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters

        SqlParameter paraID = new SqlParameter("@ID", SqlDbType.Int, 4);
        paraID.Value = ID;
        oCommand.Parameters.Add(paraID);

        SqlParameter paraStatusCode = new SqlParameter("@StatusCode", SqlDbType.Int, 4);
        paraStatusCode.Direction = ParameterDirection.Output;
        oCommand.Parameters.Add(paraStatusCode);

        try
        {
            oConnection.Open();
            oCommand.ExecuteNonQuery();
            statusCode = (int)paraStatusCode.Value;
        }
        catch (Exception oException)
        {
            throw oException;
        }
        finally
        {
            oConnection.Close();
        }

        return statusCode;
    }

    public void Update(string strName, string strCode, string address, string phone, string email, string website, int ID, out int nAlreadyIn)
    {

        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("UpdateRegion", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraCode = new SqlParameter("@Code", SqlDbType.NVarChar, 10);
        paraCode.Value = strCode;
        oCommand.Parameters.Add(paraCode);

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

    public void Add(string strName, string strCode, string address, string phone, string email, string website, int mocID, out int nAlreadyIn, int RegionID)
    {
        nAlreadyIn = 0;
        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("AddRegion", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraCode = new SqlParameter("@Code", SqlDbType.NVarChar, 10);
        paraCode.Value = strCode;
        oCommand.Parameters.Add(paraCode);

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

        SqlParameter paraMocID = new SqlParameter("@mocID", SqlDbType.Int, 4);
        paraMocID.Value = mocID;
        oCommand.Parameters.Add(paraMocID);

        SqlParameter paraAlreadyIn = new SqlParameter("@AlreadyIn", SqlDbType.Int, 4);
        paraAlreadyIn.Direction = ParameterDirection.Output;
        oCommand.Parameters.Add(paraAlreadyIn);

        //SqlParameter paraRegionID = new SqlParameter("@RegionID", SqlDbType.Int, 4);
        //paraRegionID.Value = RegionID;
        //oCommand.Parameters.Add(paraRegionID);

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

    public DataSet get_RegionFromCountry(int MOrgCountryID)
    {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetRegionFromCountry", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter para_MOrgCountryID = new SqlParameter("@pv_moc_id", SqlDbType.Int, 4);
        para_MOrgCountryID.Value = MOrgCountryID;
        oCommand.Parameters.Add(para_MOrgCountryID);


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

    public DataTable RegionSelect(BLLRegion objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("WebAtendanceSelectRegion");
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


    public DataTable RegionSelectAddEmployee(BLLRegion objbll)
    {
        DataTable dt = new DataTable();

        try
        {
            dalobj.OpenConnection();
            dt = dalobj.sqlcmdFetch("WebAtendanceSelectRegionAddEmployee");
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
