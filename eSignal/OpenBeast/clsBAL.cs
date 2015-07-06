using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DAL;

namespace OpenBeast
{
    public class clsBAL
    {
        clsDAL objclsDAL = new clsDAL(false);

        public void SaveUserInfo(string sFirstName, string sLastName, string stEmailId, string sPass, int chngPswd, short userType)
        {
            objclsDAL.CreateUserWithMinInfo(sFirstName, sLastName, stEmailId, sPass, chngPswd, userType);
        }

        public void AssignUsertoGroup(long groupid,DataTable usrlist, int flag)
        {
            objclsDAL.Submit_AppStore_UserIn_Group(groupid, usrlist, flag);
        }
    }

   
}