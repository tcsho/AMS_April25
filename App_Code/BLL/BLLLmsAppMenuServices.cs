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
/// Summary description for BLLLmsAppMenuServices
/// </summary>



public class BLLLmsAppMenuServices
{
    public BLLLmsAppMenuServices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    _DALLmsAppMenuServices objdal = new _DALLmsAppMenuServices();


    private int menuSerices_ID;
    private int user_Type_Id;
    private int menu_ID;
    private bool isAllow;
    private string strMarkedObjectIds;
    private string strUnmarkedObjectIds;

    public int MenuSerices_ID { get { return menuSerices_ID; } set { menuSerices_ID = value; } }
    public int User_Type_Id { get { return user_Type_Id; } set { user_Type_Id = value; } }
    public int Menu_ID { get { return menu_ID; } set { menu_ID = value; } }
    public bool IsAllow { get { return isAllow; } set { isAllow = value; } }

    public string MarkedObjectIds
    {
        get { return strMarkedObjectIds; }
        set { strMarkedObjectIds = value; }
    }

    public string UnmarkedObjectIds
    {
        get { return strUnmarkedObjectIds; }
        set { strUnmarkedObjectIds = value; }
    }





    public int LmsAppMenuServicesInsert(BLLLmsAppMenuServices objbll)
    {
        return objdal.LmsAppMenuServicesInsert(objbll);
    }

    public int LmsAppMenuServicesUpdate(BLLLmsAppMenuServices objbll)
    {
        return objdal.LmsAppMenuServicesUpdate(objbll); ;
    }

    public int LmsAppMenuServicesDelete(BLLLmsAppMenuServices objbll)
    {
        return objdal.LmsAppMenuServicesDelete(objbll);

    }

    public DataTable LmsAppMenuServicesSelectByID(BLLLmsAppMenuServices objbll)
    {
        return objdal.LmsAppMenuServicesSelectByID(objbll);
    }

    public DataTable LmsAppMenuServicesSelectByUserTypeID(BLLLmsAppMenuServices objbll)
    {
        return objdal.LmsAppMenuServicesSelectByUserTypeID(objbll);
    }

  

 
    

    
    


    

}

