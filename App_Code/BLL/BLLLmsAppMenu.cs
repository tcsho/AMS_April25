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
/// Summary description for BLLLmsAppMenu
/// </summary>



public class BLLLmsAppMenu
    {
    public BLLLmsAppMenu()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    _DALLmsAppMenu objdal = new _DALLmsAppMenu();



    #region 'Start Properties Declaration'

    private int menu_ID;
    private string menuName;
    private string menuText;
    private int pageID;
    private string menuImage;
    private string shortCutKey;
    private int prntMenu_ID;
    private int user_Type_Id;
    private string employeecode;
    private int status_id;


    public int Menu_ID { get { return menu_ID; } set { menu_ID = value; } }
    public string MenuName { get { return menuName; } set { menuName = value; } }
    public string MenuText { get { return menuText; } set { menuText = value; } }
    public int PageID { get { return pageID; } set { pageID = value; } }
    public string MenuImage { get { return menuImage; } set { menuImage = value; } }
    public string ShortCutKey { get { return shortCutKey; } set { shortCutKey = value; } }
    public int PrntMenu_ID { get { return prntMenu_ID; } set { prntMenu_ID = value; } }
    public int User_Type_Id { get { return user_Type_Id; } set { user_Type_Id = value; } }

    public int Status_id { get { return status_id; } set { status_id = value; } }

    public string EmployeeCode { get { return employeecode; } set { employeecode = value; } }


    #endregion

    #region 'Start Executaion Methods'

    public int LmsAppMenuAdd(BLLLmsAppMenu _obj)
        {
        return objdal.LmsAppMenuAdd(_obj);
        }
    public int LmsAppMenuUpdate(BLLLmsAppMenu _obj)
        {
        return objdal.LmsAppMenuUpdate(_obj);
        }
    public int LmsAppMenuDelete(BLLLmsAppMenu _obj)
        {
        return objdal.LmsAppMenuDelete(_obj);

        }

    #endregion
    #region 'Start Fetch Methods'


    public DataTable LmsAppMenuFetch(BLLLmsAppMenu _obj)
        {
        return objdal.LmsAppMenuSelect(_obj);
        }

    public DataTable LmsAppMenuFetchByStatusId(BLLLmsAppMenu _obj)
        {
        return objdal.LmsAppMenuSelectByStatusId(_obj);
        }


    public DataTable LmsAppMenuFetch(int _id)
      {
        return objdal.LmsAppMenuSelect(_id);
      }
    public DataTable LmsAppMenuFetch()
        {
        return objdal.LmsAppMenuSelect();
        }

    public DataTable LmsAppPageFetch()
        {
        return objdal.LmsAppPageSelect();
        }
    
    public int LmsAppMenuFetchField(int _Id)
        {
        return objdal.LmsAppMenuSelectField(_Id);
        }


    #endregion


    public DataTable LmsAppMenuSelectByPrntMenuID(BLLLmsAppMenu objbll)
    {
        return objdal.LmsAppMenuSelectByPrntMenuID(objbll);
    }


    }
