using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using KomtSnor.EntityFramework;

namespace KomtSnor.Gateways
{
    public class SQLServer_EntityGateway
    {
        private KomtSnor_Entity_FrameworkConnectionSettings _komtSnorDb;
        public SQLServer_EntityGateway()
        {
            _komtSnorDb = new KomtSnor_Entity_FrameworkConnectionSettings();
        }

        private KomtSnor_Entity_FrameworkConnectionSettings KomtSnorDb
        {
            get { return _komtSnorDb;}
        }

        #region A
        #endregion

        #region B
        #endregion

        #region C
        #region Country

        public IQueryable<tbl_Country> GetAllCountries()
        {
            return KomtSnorDb.tbl_Country;
        } 
        #endregion

        #endregion

        #region D
        #endregion

        #region E
        #endregion

        #region F
        #endregion

        #region G
        #endregion

        #region H
        #endregion

        #region I
        #region Insert
        public bool InsertObject(Object o)
        {
            bool succesfullInsert = true;
            string fullObjectType = o.GetType().ToString();
            string objectType = fullObjectType.Split('.').Last();
            switch (objectType)
            {
                case "tbl_Country":
                    KomtSnorDb.tbl_Country.Add((tbl_Country)o);
                    break;
                case "tbl_User":
                    KomtSnorDb.tbl_User.Add((tbl_User)o);
                    break;
                case "tbl_Login":
                    KomtSnorDb.tbl_Login.Add((tbl_Login)o);
                    break;
                default:
                    succesfullInsert = false;
                    break;
            }
            KomtSnorDb.SaveChanges();
            return succesfullInsert;
        }
        #endregion

        #endregion

        #region J
        #endregion

        #region K
        #endregion

        #region L
        #region Login

        public IQueryable<tbl_Login> GetAllLogins()
        {
            return KomtSnorDb.tbl_Login;
        }
        public IQueryable<tbl_Login> GetLogins(string email, string encryptedPassword)
        {
            return KomtSnorDb.tbl_Login.Where(login => login.LoginEmail.Equals(email) && login.LoginPassword.Equals(encryptedPassword));
        }

        #endregion

        #endregion

        #region M
        #endregion

        #region N
        #endregion

        #region O
        #endregion

        #region P
        #endregion

        #region Q
        #endregion

        #region R
        #endregion

        #region S
        #endregion

        #region T
        #endregion

        #region U
        #region Users
        public IQueryable<tbl_User> GetAllUsers()
        {
            return KomtSnorDb.tbl_User;
        }


        #endregion

        #endregion

        #region V
        #endregion

        #region W
        #endregion

        #region X
        #endregion

        #region Y
        #endregion

        #region Z
        #endregion








    }
}