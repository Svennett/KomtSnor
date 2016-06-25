using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomtSnor.Domain
{
    public class FireBaseUser : LoginUser
    {
        public FireBaseUser(string email, string uid) : base(email, uid)
        {
            this.email = email;
            this.uid = uid;
        }

    }
}