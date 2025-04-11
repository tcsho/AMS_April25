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
public class DALCountry:DALBase
{
    public DALCountry()
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

    public void Delete(int ID)
    {

        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("DeleteCountry", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
     
        SqlParameter paraID = new SqlParameter("@ID", SqlDbType.Int, 4);
        paraID.Value = ID;
        oCommand.Parameters.Add(paraID);

        try
        {
            oConnection.Open();
            oCommand.ExecuteNonQuery();

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

    public void Update(string strName, string strCode, int ID, out int nAlreadyIn)
    {
       
        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("UpdateCountry", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraCode = new SqlParameter("@Code", SqlDbType.NVarChar, 10);
        paraCode.Value = strCode;
        oCommand.Parameters.Add(paraCode);

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

    public void Add(string strName, string strCode, out int nAlreadyIn)
    {
        nAlreadyIn = 0;
        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("AddCountry", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraCode = new SqlParameter("@Code", SqlDbType.NVarChar, 10);
        paraCode.Value = strCode;
        oCommand.Parameters.Add(paraCode);

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

   

}
