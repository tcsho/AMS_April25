using System;
using System.Data;

/// <summary>
/// Summary description for BLLCenterShifts_SpecialCases
/// </summary>



public class BLLCenterShifts_SpecialCases
    {
    public BLLCenterShifts_SpecialCases()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    DALCenterShifts_SpecialCases objdal = new DALCenterShifts_SpecialCases();



    #region 'Start Properties Declaration'
    public int CenterShifts_SpecialCases_ID { get; set; }
    public DateTime AttDate { get; set; }
    public int Center_Id { get; set; }
    public int Region_Id { get; set; }
    public string  StartTime { get; set; }
    public string  EndTime { get; set; }
    public int? Margin { get; set; }
    public string  AbsentTime { get; set; }
    public string  TchrSTime { get; set; }
    public string  TchrETime { get; set; }
    public int? CreateBy { get; set; }
    public int Status_Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string  Remarks { get; set; }
    public string PMonth { get; set; }



    #endregion

    #region 'Start Executaion Methods'

    public int CenterShifts_SpecialCasesInsertDetails(BLLCenterShifts_SpecialCases _obj)
        {
            return objdal.CenterShifts_SpecialCasesInsertDetails(_obj);
        }
    public int CenterShifts_SpecialCasesUpdate(BLLCenterShifts_SpecialCases _obj)
        {
        return objdal.CenterShifts_SpecialCasesUpdate(_obj);
        }
    public int CenterShifts_SpecialCasesDelete(BLLCenterShifts_SpecialCases _obj)
        {
        return objdal.CenterShifts_SpecialCasesDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable CenterShifts_SpecialCasesSelectAll(BLLCenterShifts_SpecialCases _obj)
        {
            return objdal.CenterShifts_SpecialCasesSelectAll(_obj);
        }

    public DataTable CenterShifts_SpecialCasesFetchByStatusID(BLLCenterShifts_SpecialCases _obj)
    {
        return objdal.CenterShifts_SpecialCasesSelectByStatusID(_obj);
    }



    public DataTable CenterShifts_SpecialCasesFetch(BLLCenterShifts_SpecialCases _id)
      {
        return objdal.CenterShifts_SpecialCasesSelect(_id);
      }


    #endregion

    }
