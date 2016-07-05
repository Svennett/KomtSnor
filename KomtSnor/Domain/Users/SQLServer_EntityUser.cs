using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using KomtSnor.EntityFramework;

namespace KomtSnor.Domain.Users
{
    public class SQLServer_EntityUser : LoginUser
    {
        public SQLServer_EntityUser(string email, string uid, tbl_User tblUser) : base(email, uid)
        {
            this.TblUser = tblUser;
        }

        public tbl_User TblUser { get; set; }
    }
}