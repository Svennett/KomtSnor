using KomtSnor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using KomtSnor.Domain.Encryptor;
using KomtSnor.Domain.Users;
using KomtSnor.Gateways;

namespace KomtSnor.Controllers
{
    public class BaseController : Controller
    {
        protected FireBaseUser FireBaseUser
        {
            get { return (FireBaseUser)Session[Constants.Authentication.FireBaseAuthentication]; }
            set { Session[Constants.Authentication.FireBaseAuthentication] = value; }
        }

        protected GoogleUser GoogleUser
        {
            get { return (GoogleUser)Session[Constants.Authentication.GoogleAuthentication]; }
            set { Session[Constants.Authentication.GoogleAuthentication] = value; }
        }

        protected SQLServerUser SqlServerUser
        {
            get { return (SQLServerUser)Session[Constants.Authentication.SqlServerAuthentication]; }
            set { Session[Constants.Authentication.SqlServerAuthentication] = value; }
        }


        protected List<string> GetConstants(Type tClass, Type restrictionType)
        {
            List<string> constantsFields = new List<string>();

            var fields = tClass.GetFields();
            var restrictedFields = fields.Where(xField => xField.FieldType == restrictionType);
            var constantValues = restrictedFields.Select(yField => yField.GetRawConstantValue());
            constantsFields = constantValues.Select(xValue => xValue.ToString()).ToList();

            return constantsFields;
        }
        protected List<string> GetConstants(Type tClass)
        {
            List<string> constantsFields = new List<string>();

            var fields = tClass.GetFields();
            var constantValues = fields.Select(yField => yField.GetRawConstantValue());
            constantsFields = constantValues.Select(xValue => xValue.ToString()).ToList();

            return constantsFields;
        }

        protected SQLServerGateway SQLServerGateway => new SQLServerGateway();
    }
}