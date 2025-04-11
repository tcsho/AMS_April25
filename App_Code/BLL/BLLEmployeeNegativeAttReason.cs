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
/// Summary description for BLLEmployeeNegativeAttReason
/// </summary>



public class BLLEmployeeNegativeAttReason
    {
    public BLLEmployeeNegativeAttReason()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    _DALEmployeeNegativeAttReason objdal = new _DALEmployeeNegativeAttReason();



    #region 'Start Properties Declaration'

    private int negHODApp_Id;
    private string negHODAppReason;
    private int status_id;
    public int center_id;
    public int region_id;

    public int NegHODApp_Id { get { return negHODApp_Id; } set { negHODApp_Id = value; } }
    public string NegHODAppReason { get { return negHODAppReason; } set { negHODAppReason = value; } }
    public int Status_id { get { return status_id; } set { status_id = value; } }
    public int Center_id { get { return center_id; } set { center_id = value; } }
    public int Region_id { get { return region_id; } set { region_id = value; } }




    #endregion

    #region 'Start Executaion Methods'

    public int EmployeeNegativeAttReasonAdd(BLLEmployeeNegativeAttReason _obj)
        {
        return objdal.EmployeeNegativeAttReasonAdd(_obj);
        }
    public int EmployeeNegativeAttReasonUpdate(BLLEmployeeNegativeAttReason _obj)
        {
        return objdal.EmployeeNegativeAttReasonUpdate(_obj);
        }
    public int EmployeeNegativeAttReasonDelete(BLLEmployeeNegativeAttReason _obj)
        {
        return objdal.EmployeeNegativeAttReasonDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable EmployeeNegativeAttReasonFetch(BLLEmployeeNegativeAttReason _obj)
        {
        return objdal.EmployeeNegativeAttReasonSelect(_obj);
        }

    public DataTable EmployeeNegativeAttReasonFetch(int _id)
        {
        return objdal.EmployeeNegativeAttReasonSelect(_id);
        }
    public int EmployeeNegativeAttReasonFetchField(int _Id)
        {
        return objdal.EmployeeNegativeAttReasonSelectField(_Id);
        }


    #endregion

    }
