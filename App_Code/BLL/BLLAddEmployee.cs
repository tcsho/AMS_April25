using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BLLAddEmployee
/// </summary>
public class BLLAddEmployee
{
    public BLLAddEmployee()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    _DALAddEmployee objdal = new _DALAddEmployee();

    private string employeeCode;
    private string firstName;
    private string lastName;
    private string name;
    private string mStatus;
    private string gender;
    private string designation;
    private string region;
    private string  branch;
    private string branchCode;
    public Nullable <DateTime> DateOfBirth{get;set;}
    public Nullable<DateTime> DateOfJoining { get; set; }
    public string email { get; set; }

    public string EmployeeCode { get { return employeeCode; } set { employeeCode = value; } }
    public string FirstName { get { return firstName; } set { firstName = value; } }
    public string LastName { get { return lastName; } set { lastName = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string MStatus { get { return mStatus; } set { mStatus = value; } }
    public string Gender { get { return gender; } set { gender = value; } }
    public string Region { get { return region; } set { region = value; } }
    public string Branch { get { return branch; } set { branch = value; } }
    public string BranchCode { get { return branchCode; } set { branchCode = value; } }
     public string Designation { get { return designation; } set { designation = value; } }
   

    public int EMPTRANS_ACTIVEInsert(BLLAddEmployee objbll)
    {
        return objdal.EMPTRANS_ACTIVEInsert(objbll);
    }
    public int sp_empfromTemp_Trans()
    {
        return objdal.sp_empfromTemp_Trans();
    }
    
}