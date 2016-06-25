using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomtSnor.Domain
{
    public class LoginUser
    {
        public string email;
        public string uid;

        public LoginUser(string email, string uid)
        {
            this.email = email;
            this.uid = uid;
        }
    }
}