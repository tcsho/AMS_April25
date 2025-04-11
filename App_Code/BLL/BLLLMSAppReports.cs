using System;
using System.Data;


/// <summary>
/// Summary description for BLLLmsAppReports
/// </summary>



public class BLLLmsAppReports
{
    public BLLLmsAppReports()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    DALLmsAppReports objdal = new DALLmsAppReports();



    #region 'Start Properties Declaration'

    private int rpt_Id;
    private string rpt_Name;
    private string rpt_Path;
    private string rpt_Caption;
    private int page_Id;
    private int status_Id;
    private string page_Name;

    public  int Rpt_Id{        get { return rpt_Id;}        set { rpt_Id= value;}    }
    public  string Rpt_Name{        get { return rpt_Name;}        set { rpt_Name= value;}    }
    public  string Rpt_Path{        get { return rpt_Path;}        set { rpt_Path= value;}    }
    public  string Rpt_Caption{        get { return rpt_Caption;}        set { rpt_Caption= value;}    }
    public  int Page_Id{        get { return page_Id;}        set { page_Id= value;}    }
    public  int Status_Id{        get { return status_Id;}        set { status_Id= value;}    }
    public string Page_Name { get { return page_Name; } set { page_Name = value; } }





    #endregion

    #region 'Start Executaion Methods'

    public int LmsAppReportsAdd(BLLLmsAppReports _obj)
    {
        return objdal.LmsAppReportsAdd(_obj);
    }
    public int LmsAppReportsUpdate(BLLLmsAppReports _obj)
    {
        return objdal.LmsAppReportsUpdate(_obj);
    }
    public int LmsAppReportsDelete(BLLLmsAppReports _obj)
    {
        return objdal.LmsAppReportsDelete(_obj);

    }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable LmsAppReportsFetch(BLLLmsAppReports _obj)
    {
        return objdal.LmsAppReportsSelect(_obj);
    }
    public DataTable LmsAppReportsFetch(int id)
    {
        return objdal.LmsAppReportsSelect(id);
    }

    public DataTable LmsAppReportsFetchByStatusID(BLLLmsAppReports _obj)
    {
        return objdal.LmsAppReportsSelectByStatusID(_obj);
    }




    public DataTable LmsAppReportsSelect(BLLLmsAppReports _obj)
    {
        return objdal.LmsAppReportsSelect(_obj);
        
    }

    public DataTable FetchLmsAppReportsControlsbyReportCaption(BLLLmsAppReports _obj)
    {
        return objdal.FetchLmsAppReportsControlsbyReportCaption(_obj);

    }

    public DataTable FetchLmsAppReportsControlsbyRpt_Id(BLLLmsAppReports _obj)
    {
          return objdal.FetchLmsAppReportsControlsbyRpt_Id(_obj);

    }
    
    
    #endregion

}
