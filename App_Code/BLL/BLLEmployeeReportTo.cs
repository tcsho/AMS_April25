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
/// Summary description for BLLEmployeeReportTo
/// </summary>
public class BLLEmployeeReportTo
{
    _DALEmployeeReportTo objDAL = new _DALEmployeeReportTo();

    public  int Tid{        get { return tid;}        set { tid= value;}    }	private int  tid;
    public  string EmployeeCode{        get { return employeeCode;}        set { employeeCode= value;}    }	private string  employeeCode;
    public  string ReportTo{        get { return reportTo;}        set { reportTo= value;}    }	private string reportTo;
    public  int Status_id{        get { return status_id;}        set { status_id= value;}    }	private int  status_id;
    public  int Region_Id{        get { return region_Id;}        set { region_Id= value;}    }	private int  region_Id;
    public  int Center_Id{        get { return center_Id;}        set { center_Id= value;}    }	private int  center_Id;
    public  bool Active{        get { return active;}        set { active= value;}    }	private bool  active;
    public  string HODEmail{        get { return hODEmail;}        set { hODEmail= value;}    }	private string hODEmail;
    public  bool IsEmail{        get { return isEmail;}        set { isEmail= value;}    }	private bool  isEmail;
    public int User_Type_Id { get { return user_Type_Id; } set { user_Type_Id = value; } }	private int user_Type_Id;
    

    public DataTable EmployeeReportToSelectAll()
    {
        return objDAL.EmployeeReportToSelectAll();
    }

	public BLLEmployeeReportTo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int EmployeeReportToInsert(BLLEmployeeReportTo objbll)
    {
        return objDAL.EmployeeReportToInsert(objbll);
    }

    public int EmployeeReportToUpdate(BLLEmployeeReportTo objbll)
    {
        return objDAL.EmployeeReportToUpdate(objbll);
    }

    public int EmployeeReportToDelete(BLLEmployeeReportTo objbll)
    {
        return objDAL.EmployeeReportToDelete(objbll);
    }

    public DataTable EmployeeReportToSelectByEmployeeCode(BLLEmployeeReportTo objbll)
    {
        return objDAL.EmployeeReportToSelectByEmployeeCode(objbll);
    }

    public DataTable UserSelectByUserTypeID(BLLEmployeeReportTo objbll)
    {
        return objDAL.UserSelectByUserTypeID(objbll);
    }

    public DataTable UserHODSelectByUserTypeIDRegionCenter(BLLEmployeeReportTo objbll)
    {
    return objDAL.UserHODSelectByUserTypeIDRegionCenter(objbll);
    }
    


    public DataTable EmployeeReportToHODSelectByEmployeeCode(BLLEmployeeReportTo objbll)
    {
        return objDAL.EmployeeReportToHODSelectByEmployeeCode(objbll);
    }
}
