using System;
using System.Data;

/// <summary>
/// Summary description for BLLHODs_for_daily_report
/// </summary>



public class BLLHODs_for_daily_report
    {
    public BLLHODs_for_daily_report()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    DALHODs_for_daily_report objdal = new DALHODs_for_daily_report();



    #region 'Start Properties Declaration'

     public  int HODs_for_daily_report_id {get;set;}
     public  string    EmployeeCode {get;set;}
     public  string   Employee_Name {get;set;}
     public  string  Org_code {get;set;}
     public int Status_Id { get; set; }
     public int CreatedBy { get; set; }
     public int CreatedOn { get; set; }




    #endregion

    #region 'Start Executaion Methods'

    public int HODs_for_daily_reportInsert(BLLHODs_for_daily_report _obj)
        {
            return objdal.HODs_for_daily_reportInsert(_obj);
        }


    public int HODs_for_daily_reportDelete(BLLHODs_for_daily_report _obj)
        {
            return objdal.HODs_for_daily_reportDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable HODs_for_daily_reportSelectAll()
        {
            return objdal.HODs_for_daily_reportSelectAll();
        }


    #endregion

    }
