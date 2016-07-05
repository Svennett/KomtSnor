using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomtSnor.Domain.Users
{
    public class SQLServer_LoginUser : LoginUser
    {
        public string userName { get; }
        public SQLServer_LoginUser(string email, string uid, string userName ) : base(email, uid)
        {
            this.email = email;
            this.uid = uid;
            this.userName = userName;
        }

    }
}