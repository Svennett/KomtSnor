using KomtSnor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace KomtSnor.Controllers
{
    public class BaseController : Controller
    {
        protected FireBaseUser FireBaseUser
        {
            get { return (FireBaseUser)Session[Constants.Authentication.Firebase]; }
            set { Session[Constants.Authentication.Firebase] = value; }
        }

        protected GoogleUser GoogleUser
        {
            get { return (GoogleUser)Session[Constants.Authentication.Google]; }
            set { Session[Constants.Authentication.Google] = value; }
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
    }
}