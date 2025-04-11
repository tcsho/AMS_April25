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
/// Summary description for BLLEmployeeLeaveBalance
/// </summary>



public class BLLEmployeeLeaveBalance
    {
    public BLLEmployeeLeaveBalance()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    _DALEmployeeLeaveBalance objdal = new _DALEmployeeLeaveBalance();



    #region 'Start Properties Declaration'

    private string employeeCode;
    private string year;
    private Decimal casualLeave;
    private int annulaLeave;
    private int tCasualLeave;
    private int tAnnulaLeave;

    public string EmployeeCode { get { return employeeCode; } set { employeeCode = value; } }
    public string Year { get { return year; } set { year = value; } }
    public string PMonth { get; set; }
    public decimal CasualLeave { get { return casualLeave; } set { casualLeave = value; } }
    public int AnnulaLeave { get { return annulaLeave; } set { annulaLeave = value; } }
    public int TCasualLeave { get { return tCasualLeave; } set { tCasualLeave = value; } }
    public int TAnnulaLeave { get { return tAnnulaLeave; } set { tAnnulaLeave = value; } }
    public string Remarks { get; set; }



    #endregion

    #region 'Start Executaion Methods'

    public int EmployeeLeaveBalanceAdd(BLLEmployeeLeaveBalance _obj)
        {
        return objdal.EmployeeLeaveBalanceAdd(_obj);
        }
    public void EmployeeLeaveBalanceUpdate(BLLEmployeeLeaveBalance _obj)
        {
          objdal.EmployeeLeaveBalanceUpdate(_obj);
        }
    public int EmployeeLeaveBalanceDelete(BLLEmployeeLeaveBalance _obj)
        {
        return objdal.EmployeeLeaveBalanceDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmployeeLeaveBalanceFetch(BLLEmployeeLeaveBalance _obj)
        {
        return objdal.EmployeeLeaveBalanceSelect(_obj);
        }

    public DataTable EmployeeLeaveBalanceFetch_LastYears(BLLEmployeeLeaveBalance _obj)
    {
        return objdal.EmployeeLeaveBalanceSelect_LastYears(_obj);
    }

    public DataTable EmployeeLeaveBalanceFurlough(BLLEmployeeLeaveBalance _obj)
    {
        return objdal.EmployeeLeaveBalanceFurlough(_obj);
    }



    public DataTable EmployeeLeaveBalanceFetch(int _id)
      {
        return objdal.EmployeeLeaveBalanceSelect(_id);
      }
    public int EmployeeLeaveBalanceFetchField(int _Id)
        {
        return objdal.EmployeeLeaveBalanceSelectField(_Id);
        }


    #endregion

    }
