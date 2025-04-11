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
/// Summary description for BLLAppPageServices
/// </summary>



public class BLLAppPageServices
    {
    public BLLAppPageServices()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    _DALAppPageServices objdal = new _DALAppPageServices();



    #region 'Start Properties Declaration'

    private int appServPId;
    private int pageId;
    private int user_type_Id;
    private bool isallow;
    private string pageName;

    public int AppServPId { get { return appServPId; } set { appServPId = value; } }
    public int PageId { get { return pageId; } set { pageId = value; } }
    public int User_type_Id { get { return user_type_Id; } set { user_type_Id = value; } }
    public bool isAllow { get { return isallow; } set { isallow = value; } }
    public string PageName { get { return pageName; } set { pageName = value; } }


    #endregion

    #region 'Start Executaion Methods'

    public int AppPageServicesAdd(BLLAppPageServices _obj)
        {
        return objdal.AppPageServicesAdd(_obj);
        }
    public int AppPageServicesUpdate(BLLAppPageServices _obj)
        {
        return objdal.AppPageServicesUpdate(_obj);
        }
    public int AppPageServicesDelete(BLLAppPageServices _obj)
        {
        return objdal.AppPageServicesDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable AppPageServicesFetch(BLLAppPageServices _obj)
        {
        return objdal.AppPageServicesSelect(_obj);
        }

    public DataTable AppPageServicesFetch(int _id)
      {
        return objdal.AppPageServicesSelect(_id);
      }
    public int AppPageServicesFetchField(int _Id)
        {
        return objdal.AppPageServicesSelectField(_Id);
        }


    #endregion

    }
