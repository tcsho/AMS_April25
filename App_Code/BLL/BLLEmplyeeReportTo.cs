using System.Data;

/// <summary>
/// Summary description for BLLEmplyeeReportTo
/// </summary>



public class BLLEmplyeeReportTo
{
    public BLLEmplyeeReportTo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALEmplyeeReportTo objdal = new _DALEmplyeeReportTo();



    #region 'Start Properties Declaration'

    private string employeeCode;
    private string reportTo;
    private int status_id;
    private int userType_id;
    private int regoin_id;
    private int center_id;
    private string email;
    private bool isemail;
    private int deptCode;
    private int desigCode;


    public string EmployeeCode { get { return employeeCode; } set { employeeCode = value; } }
    public string ReportTo { get { return reportTo; } set { reportTo = value; } }
    public int Status_id { get { return status_id; } set { status_id = value; } }
    public int UserType_id { get { return userType_id; } set { userType_id = value; } }
    public int Region_id { get { return regoin_id; } set { regoin_id = value; } }
    public int Center_id { get { return center_id; } set { center_id = value; } }
    public string Email { get { return email; } set { email = value; } }
    public bool isEmail { get { return isemail; } set { isemail = value; } }
    public int DeptCode { get { return deptCode; } set { deptCode = value; } }
    public int DesigCode { get { return desigCode; } set { desigCode = value; } }


    public string PMonthDesc { get; set; }
    #endregion

    #region 'Start Executaion Methods'

    public int EmplyeeReportToAdd(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmplyeeReportToAdd(_obj);
    }
    public int EmplyeeReportToUpdate(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmplyeeReportToUpdate(_obj);
    }
    public int EmplyeeReportToDelete(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmplyeeReportToDelete(_obj);

    }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmplyeeReportToFetch(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmplyeeReportToSelect(_obj);
    }
    public DataTable SelectEmployeeListforCafe(BLLEmplyeeReportTo _obj)
    {
        return objdal.SelectEmployeeListforCafe(_obj);
    }


    public DataTable EmplyeeReportToFetchList(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmplyeeReportToSelectList(_obj);
    }

    public DataTable EmplyeeReportToHOD(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmplyeeReportToHOD(_obj);
    }

    public DataTable EmplyeeReportToFetchEmail(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmplyeeReportToSelectEmail(_obj);
    }

    public DataTable EmployeeprofileSelectByRegionCenterDept(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmployeeprofileSelectByRegionCenterDept(_obj);
    }
    public DataTable EmployeeprofileSelectByRegionCenterDeptViewonly(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmployeeprofileSelectByRegionCenterDeptViewonly(_obj);
    }

    


    public DataTable EmployeeprofileSelectByRegionCenterDeptDesig(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmployeeprofileSelectByRegionCenterDeptDesig(_obj);
    }
    public DataTable EmployeeprofileSelectByRegionCenter(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmployeeprofileSelectByRegionCenter(_obj);
    }
    public DataTable EmployeeprofileSelectByRegionCenter_shiftTimmings(BLLEmplyeeReportTo _obj)
    {
        return objdal.EmployeeprofileSelectByRegionCenter_shiftTimmings(_obj);
    }



    public DataTable EmplyeeReportToFetch(int _id)
    {
        return objdal.EmplyeeReportToSelect(_id);
    }
    public int EmplyeeReportToFetchField(int _Id)
    {
        return objdal.EmplyeeReportToSelectField(_Id);
    }

    public DataTable EmployeeTimmingPolicyIdFetch(string employeecode)
    {
        return objdal.EmployeeTimmingPolicyIdFetch(employeecode);
    }

    public DataTable SingleEmployeeDetails(string employeecode)
    {
        return objdal.SingleEmployeeDetails(employeecode);
    }

    #endregion

}
