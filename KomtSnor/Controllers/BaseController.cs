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
        private SQLServer_EntityGateway _sqlsEntityGateway;

        public BaseController()
        {
           _sqlsEntityGateway = new SQLServer_EntityGateway();
        }

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

        protected SQLServer_LoginUser SqlServer_LoginUser
        {
            get { return (SQLServer_LoginUser)Session[Constants.Authentication.SqlServer_LoginAuthentication]; }
            set { Session[Constants.Authentication.SqlServer_LoginAuthentication] = value; }
        }

        protected SQLServer_EntityUser SqlServer_EntityUser
        {
            get { return (SQLServer_EntityUser)Session[Constants.Authentication.SqlServer_EntityAuthentication]; }
            set { Session[Constants.Authentication.SqlServer_EntityAuthentication] = value; }
        }

        protected SQLServer_EntityGateway SqlsEntityGateway
        {
            get { return _sqlsEntityGateway;}
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

        protected SQLServer_LoginGateway SqlServerLoginGateway => new SQLServer_LoginGateway();
    }
}