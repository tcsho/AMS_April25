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
/// Summary description for BLLSearchStudent
/// </summary>



public class BLLSearchStudent
{
    public BLLSearchStudent()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    DALSearchStudent objdal = new DALSearchStudent();



    #region 'Start Properties Declaration'

    public int Student_Id { get; set; }
    public int Class_Id { get; set; }
    public int Section_ID { get; set; }
    public int Session_Id { get; set; }
    public string Aims_Id { get; set; }
    public string Student_Status_Id { get; set; }
    public string Main_Organisation_Id { get; set; }
    public string Student_No { get; set; }
    public string First_Name { get; set; }
    public string Middle_Name { get; set; }
    public string Last_Name { get; set; }
    public string Date_Of_Birth { get; set; }
    public string Address { get; set; }
    public string Telephone_No { get; set; }
    public string Gender_Id { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Postal_Code { get; set; }
    public string Comments { get; set; }
    public string Region_Id { get; set; }
    public string Center_Id { get; set; }
    public string Grade_Id { get; set; }
    public DateTime Approval_Date { get; set; }
    public DateTime Application_Date { get; set; }
    public DateTime Transfer_Date { get; set; }
    public DateTime Drop_Date { get; set; }
    public bool isChaProcess { get; set; }
    public string Section_Id { get; set; }
    public string Teacher_Id { get; set; }
    public string EndIndex { get; set; }
    public string StartIndex { get; set; }


    #endregion

    #region 'Start Executaion Methods'

    public int SearchStudentAdd(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentAdd(_obj);
    }
    public int SearchStudentUpdate(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentUpdate(_obj);
    }
    public int SearchStudentDelete(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentDelete(_obj);

    }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable SearchStudentFetch(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentSelect(_obj);
    }
    public DataTable SearchStudent_UnassignSubject(BLLSearchStudent _obj)
    {
        return objdal.SearchStudent_UnassignSubject(_obj);
    }
    
    public DataTable SearchStudentResultCard(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentResultCard(_obj);
    }

    public DataTable SearchStudentSubjectData(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentSubjectData(_obj);
    }

    

    public DataTable SearchStudentFetchExport(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentSelectExport(_obj);
    }
    public DataTable SearchStudentFetchCount(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentSelectCount(_obj);
    }

    public DataTable SearchStudentFetchByStatusID(BLLSearchStudent _obj)
    {
        return objdal.SearchStudentSelectByStatusID(_obj);
    }



    public DataTable SearchStudentFetch(int _id)
    {
        return objdal.SearchStudentSelect(_id);
    }


    #endregion

}
