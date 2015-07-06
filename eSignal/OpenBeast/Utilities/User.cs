using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data;



namespace OpenBeast.Utilities
{
    public class User
    {
        clsDAL objclsDAL = new clsDAL(false);

        public void CreateUser(string FirstName, string LastName,string EmailID,string Password,int ChangePassword,short UserType)
        {
            //objclsDAL.CreateUserWithMinInfo(FirstName, LastName, EmailID, Password, ChangePassword, UserType);
        }

        public void AssignUsertoGroup(long GroupID, DataTable UserList,int Flag)
        {
           // objclsDAL.Submit_AppStore_UserIn_Group(GroupID, UserList, Flag);
        }

    }
}