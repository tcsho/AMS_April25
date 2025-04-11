using System;
using System.Data;

/// <summary>
/// Summary description for BLLEmployeeOnManualShiftDetail
/// </summary>



public class BLLEmployeeOnManualShiftDetail
    {
    public BLLEmployeeOnManualShiftDetail()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    DALEmployeeOnManualShiftDetail objdal = new DALEmployeeOnManualShiftDetail();



    #region 'Start Properties Declaration'
    public string Employeecode { get; set; }
    public string Reason { get; set; }
    public int Status_Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int CreatedBy { get; set; }
 

    #endregion

    #region 'Start Executaion Methods'

    public int EmployeeOnManualShiftDetailAdd(BLLEmployeeOnManualShiftDetail _obj)
        {
        return objdal.EmployeeOnManualShiftDetailAdd(_obj);
        }
    public int EmployeeOnManualShiftDetailUpdate(BLLEmployeeOnManualShiftDetail _obj)
        {
        return objdal.EmployeeOnManualShiftDetailUpdate(_obj);
        }
    public int EmployeeOnManualShiftDetailDelete(BLLEmployeeOnManualShiftDetail _obj)
        {
        return objdal.EmployeeOnManualShiftDetailDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmployeeprofileSelectByRegionCenter(long r, long c)
        {
            return objdal.EmployeeprofileSelectByRegionCenter(r, c);
        }

    public DataTable EmployeeOnManualShiftDetailFetchByStatusID(BLLEmployeeOnManualShiftDetail _obj)
    {
        return objdal.EmployeeOnManualShiftDetailSelectByStatusID(_obj);
    }



    //public DataTable EmployeeOnManualShiftDetailFetch(int _id)
    //  {
    //    return objdal.EmployeeOnManualShiftDetailSelect(_id);
    //  }


    #endregion

    }
