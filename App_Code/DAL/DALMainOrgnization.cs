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
public class DALMainOrgnization : DALBase
{
    public DALMainOrgnization()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet get_MainOrgnizations()
    {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetAllOrgnizations", oConnection);
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
        SqlCommand oCommand = new SqlCommand("DeleteMainOrgnization", oConnection);
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

    public void Update(string strName, string strCode, string address, string phone, string email, string website, int ID, out int nAlreadyIn)
    {

        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("UpdateMainOrgnization", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraCode = new SqlParameter("@Code", SqlDbType.NVarChar, 10);
        paraCode.Value = strCode;
        oCommand.Parameters.Add(paraCode);

        SqlParameter paraAddress = new SqlParameter("@address", SqlDbType.NVarChar, 200);
        paraAddress.Value = address;
        oCommand.Parameters.Add(paraAddress);

        SqlParameter paraPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 25);
        paraPhone.Value = phone;
        oCommand.Parameters.Add(paraPhone);

        SqlParameter paraEmail = new SqlParameter("@email", SqlDbType.NVarChar, 50);
        paraEmail.Value = email;
        oCommand.Parameters.Add(paraEmail);

        SqlParameter paraWebsite = new SqlParameter("@website", SqlDbType.NVarChar, 50);
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

    public void Add(string strName, string strCode, string address, string phone, string email, string website, out int nAlreadyIn)
    {
        nAlreadyIn = 0;
        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("AddMainOrgnization", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraCode = new SqlParameter("@Code", SqlDbType.NVarChar, 10);
        paraCode.Value = strCode;
        oCommand.Parameters.Add(paraCode);

        SqlParameter paraAddress = new SqlParameter("@address", SqlDbType.NVarChar, 200);
        paraAddress.Value = address;
        oCommand.Parameters.Add(paraAddress);

        SqlParameter paraPhone = new SqlParameter("@phone", SqlDbType.NVarChar, 25);
        paraPhone.Value = phone;
        oCommand.Parameters.Add(paraPhone);

        SqlParameter paraEmail = new SqlParameter("@email", SqlDbType.NVarChar, 50);
        paraEmail.Value = email;
        oCommand.Parameters.Add(paraEmail);

        SqlParameter paraWebsite = new SqlParameter("@website", SqlDbType.NVarChar, 50);
        paraWebsite.Value = website;
        oCommand.Parameters.Add(paraWebsite);

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
