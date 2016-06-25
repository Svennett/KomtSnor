using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomtSnor.Domain.Users
{
    public class SQLServerUser : LoginUser
    {
        public string userName { get; }
        public SQLServerUser(string email, string uid, string userName ) : base(email, uid)
        {
            this.email = email;
            this.uid = uid;
            this.userName = userName;
        }

    }
}