using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomtSnor.Domain
{
    public class FireBaseUser
    {
        public string email;
        public string uid;
        public FireBaseUser(string email, string uid)
        {
            this.email = email;
            this.uid = uid;
        }

    }
}