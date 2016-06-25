using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomtSnor.Domain
{
    public class GoogleUser : LoginUser
    {
        public GoogleUser(string email, string uid, string name) : base(email, uid)
        {
            this.name = name;
        }
        public string name { get; set; }
    }
}