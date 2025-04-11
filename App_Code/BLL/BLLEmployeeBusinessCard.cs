using System;
using System.Data;

/// <summary>
/// Summary description for BLLEmployeeBusinessCard
/// </summary>



public class BLLEmployeeBusinessCard
{
    public BLLEmployeeBusinessCard()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALEmployeeBusinessCard objdal = new _DALEmployeeBusinessCard();



    #region 'Start Properties Declaration'

    public int EmpBCard_Id { get; set; }
    public string EmployeeCode { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
    public int Quantity { get; set; }
    public string Other { get; set; }
    public decimal Cost { get; set; }
    public string Remarks { get; set; }
    public int Region_Id { get; set; }
    public int Center_Id { get; set; }
    public int DesigCode { get; set; }
    public int DeptCode { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string Web { get; set; }
    public string UAN { get; set; }
    public DateTime CreatedOn { get; set; }
    public string HODBy { get; set; }
    public int HOD_EmpBCardStatus_Id { get; set; }
    public DateTime HOD_StatusOn { get; set; }
    public string HODRemarks { get; set; }
    public string HR_RD_By { get; set; }
    public int HR_RD_EmpBCardStatus_Id { get; set; }
    public string HR_RD_Remarks { get; set; }
    public DateTime HR_RD_StatusOn { get; set; }
    public string CEO_By { get; set; }
    public int CEO_EmpBCardStatus_Id { get; set; }
    public DateTime CEO_StatusOn { get; set; }
    public string CEORemarks { get; set; }
    public int Status_Id { get; set; }
    public int ReceivedOn { get; set; }
    public int EmpBCardStatus_Id { get; set; }
    public int ApprovalTypeId { get; set; }



    #endregion

    #region 'Start Executaion Methods'

    public int EmployeeBusinessCardAdd(BLLEmployeeBusinessCard _obj)
    {
        return objdal.EmployeeBusinessCardAdd(_obj);
    }
    public int EmployeeBusinessCardUpdate(BLLEmployeeBusinessCard _obj)
    {
        return objdal.EmployeeBusinessCardUpdate(_obj);
    }
    public int EmployeeBusinessCardApprovalUpdate(BLLEmployeeBusinessCard _obj)
    {
        return objdal.EmployeeBusinessCardApprovalUpdate(_obj);
    }
    public int EmployeeBusinessCardDelete(BLLEmployeeBusinessCard _obj)
    {
        return objdal.EmployeeBusinessCardDelete(_obj);

    }
    public int EmployeeBusinessCardPendingOrderFetchByEmpCode(BLLEmployeeBusinessCard _obj)
    {
        return objdal.EmployeeBusinessCardPendingOrderSelectByEmpCode(_obj);

    }
    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmployeeBusinessCardFetch(BLLEmployeeBusinessCard _obj)
    {
        return objdal.EmployeeBusinessCardSelect(_obj);
    }

    public DataTable EmployeeBusinessCardFetchByStatusID(BLLEmployeeBusinessCard _obj)
    {
        return objdal.EmployeeBusinessCardSelectByStatusID(_obj);
    }



    public DataTable EmployeeBusinessCardFetch(int _id)
    {
        return objdal.EmployeeBusinessCardSelect(_id);
    }

    public DataTable EmployeeProfileForBCardFetchByCode(BLLEmployeeBusinessCard obj)
    {
        return objdal.EmployeeProfileForBCardSelectByCode(obj);
    }

    public DataTable EmployeeBusinessCardFetchAllByEmpCode(BLLEmployeeBusinessCard obj)
    {
        return objdal.EmployeeBusinessCardSelectAllByEmpCode(obj);
    }

    public DataTable EmployeeBusinessCardFetchAllByApprovalEmpCode(BLLEmployeeBusinessCard obj)
    {
        return objdal.EmployeeBusinessCardSelectAllByApprovalEmpCode(obj);
    }

    public DataTable EmployeeBusinessCardFetchAllForPrinting( )
    {
        return objdal.EmployeeBusinessCardSelectAllForPrinting();
    }
    #endregion

}
