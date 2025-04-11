using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for BLLUser
/// </summary>



public class BLLUser
{
    public BLLUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALUser objdal = new _DALUser();



    #region 'Start Properties Declaration'

    public int User_Id { get { return user_Id; } set { user_Id = value; } }	private int user_Id;
    public int User_Type_Id { get { return user_Type_Id; } set { user_Type_Id = value; } }	private int user_Type_Id;
    public string First_Name { get { return first_Name; } set { first_Name = value; } }	private string first_Name;
    public string Middle_Name { get { return middle_Name; } set { middle_Name = value; } }	private string middle_Name;
    public string Last_Name { get { return last_Name; } set { last_Name = value; } }	private string last_Name;
    public DateTime Date_Of_Birth { get { return date_Of_Birth; } set { date_Of_Birth = value; } }	private DateTime date_Of_Birth;
    public string Address { get { return address; } set { address = value; } }	private string address;
    public string Home_Phone { get { return home_Phone; } set { home_Phone = value; } }	private string home_Phone;
    public string Mobile_Phone { get { return mobile_Phone; } set { mobile_Phone = value; } }	private string mobile_Phone;
    public string Email { get { return email; } set { email = value; } }	private string email;
    public string State { get { return state; } set { state = value; } }	private string state;
    public string Postal_Code { get { return postal_Code; } set { postal_Code = value; } }	private string postal_Code;
    public string City { get { return city; } set { city = value; } }	private string city;
    public string Country { get { return country; } set { country = value; } }	private string country;
    public string User_Name { get { return user_Name; } set { user_Name = value; } }	private string user_Name;
    public string Password { get { return password; } set { password = value; } }	private string password;
    public int Main_Organisation_Id { get { return main_Organisation_Id; } set { main_Organisation_Id = value; } }	private int main_Organisation_Id;
    public int Gender_Id { get { return gender_Id; } set { gender_Id = value; } }	private int gender_Id;
    public int Center_Id { get { return center_Id; } set { center_Id = value; } }	private int center_Id;
    public int Region_Id { get { return region_Id; } set { region_Id = value; } }	private int region_Id;
    public int Status_Id { get { return status_Id; } set { status_Id = value; } }	private int status_Id;
    public int Grade_id { get { return grade_id; } set { grade_id = value; } }	private int grade_id;
    public string EmployeeCode { get { return employeeCode; } set { employeeCode = value; } }	private string employeeCode;
    public int DeptCode { get; set; }

    public string IpAddress { get { return ipAddress; } set { ipAddress = value; } }
    private string ipAddress;



    #endregion

    #region 'Start Executaion Methods'

    public int UserAdd(BLLUser _obj)
    {
        return objdal.UserAdd(_obj);
    }
    public int UserUpdate(BLLUser _obj)
    {
        return objdal.UserUpdate(_obj);
    }
    public int UserDelete(BLLUser _obj)
    {
        return objdal.UserDelete(_obj);

    }
    public int UserPasswordUpdate(BLLUser _obj)
    {
        return objdal.UserPasswordUpdate(_obj);

    }


    #endregion
    #region 'Start Fetch Methods'


    public DataTable UserFetch(BLLUser _obj)
    {
        return objdal.UserSelect(_obj);
    }

    public DataTable UserFetchByUserName(BLLUser _obj)
    {
        return objdal.UserSelectByUserName(_obj);
    }

    public DataTable UserFetch(int _id)
    {
        return objdal.UserSelect(_id);
    }
    public int UserFetchField(int _Id)
    {
        return objdal.UserSelectField(_Id);
    }

    public DataTable UserFetchByUserTypeId(BLLUser obj)
    {
        return objdal.UserSelectByUserTypeId(obj);
    }
    public DataTable UserSelectAll(BLLUser obj)
    {
        return objdal.UserSelectAll(obj);
    }

    #endregion

}
