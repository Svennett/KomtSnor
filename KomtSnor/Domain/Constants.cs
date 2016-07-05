using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KomtSnor.Domain
{
    public static class Constants
    {
        public static class Authentication
        {
            public const string FireBaseAuthentication = "FireBase";
            public const string GoogleAuthentication = "Google";
            public const string SqlServer_LoginAuthentication = "SQLServer_Login";
            public const string SqlServer_EntityAuthentication = "SQLServer_Entity";
            public const string CurrentActionDiscription = "currentActionDiscription_for_after_authentication";
        }

        public static class Login
        {
            public const string NoUserFound = "NoUserFound";
            public const string MultipleUsersFound = "MultipleUsersFound";
            public const string Succes = "Succes";
        }

        public static class AccountStatus
        {
            public const string Enabled = "Enabled";
            public const string Disabled = "Disabled";
            public const string NoEmail = "NoEmal";


        }
    }
}