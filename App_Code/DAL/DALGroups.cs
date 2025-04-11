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
/// Summary description for DALGroups
/// </summary>
public class DALGroups : DALBase
{
	public DALGroups()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet get_AllGroups(int userLevel_ID)
    {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetAllGroups", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        //SqlParameter para_city = new SqlParameter("@User_Type_ID", SqlDbType.Int);
        SqlParameter para_city = new SqlParameter("@UserLevel_ID", SqlDbType.Int);
        para_city.Value = userLevel_ID;
        oCommand.Parameters.Add(para_city);


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

    public DataSet get_AllGroupsGrid(int userLevel_ID)
        {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetAllGroupsByHeadOffice", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        //SqlParameter para_city = new SqlParameter("@User_Type_ID", SqlDbType.Int);
        SqlParameter para_city = new SqlParameter("@UserLevel_ID", SqlDbType.Int);
        para_city.Value = userLevel_ID;
        oCommand.Parameters.Add(para_city);


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

    public DataSet get_UserLevelByType_Id(int UserType_Id)
        {
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("GetUserLevelByUserType", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        SqlParameter para_userType = new SqlParameter("@User_Type_Id", SqlDbType.Int);
        para_userType.Value = UserType_Id;
        oCommand.Parameters.Add(para_userType);


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
        SqlCommand oCommand = new SqlCommand("DeleteGroup", oConnection);
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

    public void Update(string strName, string strCode, string description, RadioButtonList cbl, int ID, int ARightsID, out int nAlreadyIn)
    {

        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("UpdateGroup", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraCode = new SqlParameter("@Code", SqlDbType.NVarChar, 50);
        paraCode.Value = strCode;
        oCommand.Parameters.Add(paraCode);

        SqlParameter paraDesc = new SqlParameter("@description", SqlDbType.NVarChar, 250);
        paraDesc.Value = description;
        oCommand.Parameters.Add(paraDesc);

        
        if (cbl.Items[0].Selected == true)
        {
            SqlParameter paraAdmin = new SqlParameter("@admin", SqlDbType.Bit, 1);
            paraAdmin.Value = 1;
            oCommand.Parameters.Add(paraAdmin);
        }
        else
        {
            SqlParameter paraAdmin = new SqlParameter("@admin", SqlDbType.Bit, 1);
            paraAdmin.Value = 0;
            oCommand.Parameters.Add(paraAdmin);
        }

        if (cbl.Items[1].Selected == true)
        {
            SqlParameter paraMOrg = new SqlParameter("@MOrg", SqlDbType.Bit, 1);
            paraMOrg.Value = 1;
            oCommand.Parameters.Add(paraMOrg);
        }
        else
        {
            SqlParameter paraMOrg = new SqlParameter("@MOrg", SqlDbType.Bit, 1);
            paraMOrg.Value = 0;
            oCommand.Parameters.Add(paraMOrg);
        }


        if (cbl.Items[2].Selected == true)
        {
            SqlParameter paraRegion = new SqlParameter("@region", SqlDbType.Bit, 1);
            paraRegion.Value = 1;
            oCommand.Parameters.Add(paraRegion);
        }
        else
        {
            SqlParameter paraRegion = new SqlParameter("@region", SqlDbType.Bit, 1);
            paraRegion.Value = 0;
            oCommand.Parameters.Add(paraRegion);
        }

        if (cbl.Items[3].Selected == true)
        {
            SqlParameter paraCenter = new SqlParameter("@center", SqlDbType.Bit, 1);
            paraCenter.Value = 1;
            oCommand.Parameters.Add(paraCenter);
        }
        else
        {
            SqlParameter paraCenter = new SqlParameter("@center", SqlDbType.Bit, 1);
            paraCenter.Value = 0;
            oCommand.Parameters.Add(paraCenter);
        }

        if (cbl.Items[4].Selected == true)
        {
            SqlParameter paraTeacher = new SqlParameter("@teacher", SqlDbType.Bit, 1);
            paraTeacher.Value = 1;
            oCommand.Parameters.Add(paraTeacher);
        }
        else
        {
            SqlParameter paraTeacher = new SqlParameter("@teacher", SqlDbType.Bit, 1);
            paraTeacher.Value = 0;
            oCommand.Parameters.Add(paraTeacher);
        }

        if (cbl.Items[5].Selected == true)
        {
            SqlParameter paraMLocker = new SqlParameter("@MLocker", SqlDbType.Bit, 1);
            paraMLocker.Value = 1;
            oCommand.Parameters.Add(paraMLocker);
        }
        else
        {
            SqlParameter paraMLocker = new SqlParameter("@MLocker", SqlDbType.Bit, 1);
            paraMLocker.Value = 0;
            oCommand.Parameters.Add(paraMLocker);
        }


        SqlParameter paraID = new SqlParameter("@ID", SqlDbType.Int, 4);
        paraID.Value = ID;
        oCommand.Parameters.Add(paraID);

        SqlParameter paraAR = new SqlParameter("@aRightsID", SqlDbType.Int, 4);
        paraAR.Value = ARightsID;
        oCommand.Parameters.Add(paraAR);

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

    public void Add(string strName, string strCode, string description,int userLevel_ID, RadioButtonList cbl, out int nAlreadyIn)
    {
        nAlreadyIn = 0;
        // Establish Connection
        SqlConnection oConnection = GetConnection();

        // build the command
        SqlCommand oCommand = new SqlCommand("AddGroup", oConnection);
        oCommand.CommandType = CommandType.StoredProcedure;

        // Parameters
        SqlParameter paraName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
        paraName.Value = strName;
        oCommand.Parameters.Add(paraName);

        SqlParameter paraCode = new SqlParameter("@Code", SqlDbType.NVarChar, 50);
        paraCode.Value = strCode;
        oCommand.Parameters.Add(paraCode);

        SqlParameter paraDesc = new SqlParameter("@description", SqlDbType.NVarChar, 250);
        paraDesc.Value = description;
        oCommand.Parameters.Add(paraDesc);

        if (cbl.Items[0].Selected == true)
        {
            SqlParameter paraAdmin = new SqlParameter("@admin", SqlDbType.Bit, 1);
            paraAdmin.Value = 1;
            oCommand.Parameters.Add(paraAdmin);
        }
        else
        {
            SqlParameter paraAdmin = new SqlParameter("@admin", SqlDbType.Bit, 1);
            paraAdmin.Value = 0;
            oCommand.Parameters.Add(paraAdmin);
        }

        if (cbl.Items[1].Selected == true)
        {
            SqlParameter paraMOrg = new SqlParameter("@MOrg", SqlDbType.Bit, 1);
            paraMOrg.Value = 1;
            oCommand.Parameters.Add(paraMOrg);
        }
        else
        {
            SqlParameter paraMOrg = new SqlParameter("@MOrg", SqlDbType.Bit, 1);
            paraMOrg.Value = 0;
            oCommand.Parameters.Add(paraMOrg);
        }


        if (cbl.Items[2].Selected == true)
        {
            SqlParameter paraRegion = new SqlParameter("@region", SqlDbType.Bit, 1);
            paraRegion.Value = 1;
            oCommand.Parameters.Add(paraRegion);
        }
        else
        {
            SqlParameter paraRegion = new SqlParameter("@region", SqlDbType.Bit, 1);
            paraRegion.Value = 0;
            oCommand.Parameters.Add(paraRegion);
        }

        if (cbl.Items[3].Selected == true)
        {
            SqlParameter paraCenter = new SqlParameter("@center", SqlDbType.Bit, 1);
            paraCenter.Value = 1;
            oCommand.Parameters.Add(paraCenter);
        }
        else
        {
            SqlParameter paraCenter = new SqlParameter("@center", SqlDbType.Bit, 1);
            paraCenter.Value = 0;
            oCommand.Parameters.Add(paraCenter);
        }

        if (cbl.Items[4].Selected == true)
        {
            SqlParameter paraTeacher = new SqlParameter("@teacher", SqlDbType.Bit, 1);
            paraTeacher.Value = 1;
            oCommand.Parameters.Add(paraTeacher);
        }
        else
        {
            SqlParameter paraTeacher = new SqlParameter("@teacher", SqlDbType.Bit, 1);
            paraTeacher.Value = 0;
            oCommand.Parameters.Add(paraTeacher);
        }

        if (cbl.Items[5].Selected == true)
        {
            SqlParameter paraMLocker = new SqlParameter("@MLocker", SqlDbType.Bit, 1);
            paraMLocker.Value = 1;
            oCommand.Parameters.Add(paraMLocker);
        }
        else
        {
            SqlParameter paraMLocker = new SqlParameter("@MLocker", SqlDbType.Bit, 1);
            paraMLocker.Value = 0;
            oCommand.Parameters.Add(paraMLocker);
        }
        //Updated 23-Jan-2013
        if (cbl.SelectedValue!= "")
        {
            SqlParameter paraUserType = new SqlParameter("@UserLevel_ID", SqlDbType.Int);
            paraUserType.Value = userLevel_ID;
            oCommand.Parameters.Add(paraUserType);
        }
        else
        {
            SqlParameter paraUserType = new SqlParameter("@UserLevel_ID", SqlDbType.Int);
            paraUserType.Value = 0;
            oCommand.Parameters.Add(paraUserType);
        }

        //

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
