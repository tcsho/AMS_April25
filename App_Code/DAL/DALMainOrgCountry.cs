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
/// Summary description for Country
/// </summary>
public class DALMainOrgCountry : DALBase
{
    public DALMainOrgCountry()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    

    public int Delete(int ID)
    {

        // Establish Connection
        SqlConnection oConnection = GetConnection();
        int statusCode = -1;

        // build the command
        SqlCommand oCommand = new SqlCommand("DeleteMainOrgCountry", oConnection);
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



    public void Add(int orgID, int countryID, out int nAlreadyIn)
    {
        nAlreadyIn = 0;
        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("AddCountryToOrg", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraOrgID = new SqlParameter("@orgID", SqlDbType.Int, 4);
        paraOrgID.Value = orgID;
        oCommand.Parameters.Add(paraOrgID);

        SqlParameter paraCountryID = new SqlParameter("@countryID", SqlDbType.Int, 4);
        paraCountryID.Value = countryID;
        oCommand.Parameters.Add(paraCountryID);

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




    public DataSet get_CountriesForOrg(int orgID)
    {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetCountryForMainOrganization", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter para_orgID = new SqlParameter("@OrgID", SqlDbType.Int, 4);
        para_orgID.Value = orgID;
        oCommand.Parameters.Add(para_orgID);


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
}
