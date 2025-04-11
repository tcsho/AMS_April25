using System;
using System.Data;

/// <summary>
/// Summary description for BLLCenter
/// </summary>



public class BLLCenter
    {
    public BLLCenter()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    DALCenter objdal = new DALCenter();



    #region 'Start Properties Declaration'

    public int Center_Id { get; set; }
    public string Center_Name { get; set; }
    public string Center_String_Id { get; set; }
    public int Region_Id { get; set; }
    public int Status_Id { get; set; }
    public string Address { get; set; }
    public string Telephone_No { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public DateTime Academic_Year_Start_Month { get; set; }
    public DateTime Academic_Year_End_Month { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int StartMargin { get; set; }
    public int EndMargin { get; set; }



    #endregion

    #region 'Start Executaion Methods'

    //public int CenterAdd(BLLCenter _obj)
    //    {
    //    return objdal.CenterAdd(_obj);
    //    }
    //public int CenterUpdate(BLLCenter _obj)
    //    {
    //    return objdal.CenterUpdate(_obj);
    //    }
    //public int CenterDelete(BLLCenter _obj)
    //    {
    //    return objdal.CenterDelete(_obj);

    //    }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable CenterFetch(BLLCenter _obj)
        {
        return objdal.CenterSelect(_obj);
        }





    #endregion

    }
