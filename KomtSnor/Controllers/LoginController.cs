using KomtSnor.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KomtSnor.Domain.Encryptor;
using KomtSnor.Domain.Users;
using KomtSnor.EntityFramework;
using KomtSnor.Gateways;
using Microsoft.Ajax.Utilities;

namespace KomtSnor.Controllers
{
    public class LoginController : BaseController
    {
        private ActionResult CheckSessionForCurrentActionDescriptor()
        {
            ActionDescriptor actionDescriptor = null;
            string previousController;
            string previousPage;
            try
            {
                actionDescriptor = (ActionDescriptor)Session[Constants.Authentication.CurrentActionDiscription];
            }
            catch
            {
                // ignored
            }
            if (actionDescriptor != null)
            {
                previousController = actionDescriptor.ControllerDescriptor.ControllerName;
                previousPage = actionDescriptor.ActionName;
            }
            else
            {
                previousController = "Home";
                previousPage = "Index";
            }
            return RedirectToAction(previousPage, previousController);
        }

        #region FireBaseLogin
        public ActionResult FireBaseLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FireBaseLogin(FormCollection collection)
        {
            string email = collection["email"];
            string uid = collection["uid"];

            FireBaseUser = new FireBaseUser(email, uid);
            ActionResult previousPageActionResult = CheckSessionForCurrentActionDescriptor();
            return previousPageActionResult;
        }
        #endregion

        #region GoogleLogin
        public ActionResult GoogleLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GoogleLogin(FormCollection collection)
        {
            var id_token = collection["id_token"];
            var name = collection["name"];
            var email = collection["email"];
            GoogleUser = new GoogleUser(email, id_token, name);
            ActionResult previousPageActionResult = CheckSessionForCurrentActionDescriptor();
            return Json(new {result = "succes", actionResult = previousPageActionResult });
        }
        #endregion

        #region FacebookLogin
        public ActionResult FacebookLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FacebookLogin(FormCollection collection)
        {
            return View();
        }
        #endregion

        #region SQLServer_LoginLogin
        public ActionResult SQLServer_LoginLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SQLServer_LoginLogin(FormCollection collection)
        {
            string email = collection["Email"];
            string password = collection["Password"];
            string encryptedPassword = Encryptor1.Encrypt(password);
            SqlCommand queryCommand = CreateSQLCommand(email, encryptedPassword);

            ArrayList sqlValues = SqlServerLoginGateway.ExecuteSelectCommand(queryCommand);

            SQLServer_LoginUser sqlServerUser = getSQLUserValuesFromDataReader(sqlValues);
            string loginResult = (sqlServerUser.email != Constants.Login.MultipleUsersFound && sqlServerUser.email != Constants.Login.NoUserFound) ? Constants.Login.Succes : sqlServerUser.email;
            this.SqlServer_LoginUser = loginResult == Constants.Login.Succes ? sqlServerUser : null;

            ActionResult previousPageActionResult = CheckSessionForCurrentActionDescriptor();
            return Json(new { result = loginResult, actionResult = previousPageActionResult });
        }

        private SqlCommand CreateSQLCommand(string email, string password)
        {
            //create object
            SqlCommand myCommand = new SqlCommand();

            //set Paremeters
            SqlParameter[] commandParameters = new SqlParameter[2];
            commandParameters[0] = new SqlParameter("@Email", email);
            commandParameters[1] = new SqlParameter("@Password", password);
            myCommand.Parameters.AddRange(commandParameters);
            
            //set command text
            string commandText = "SELECT * " +
                             "FROM[dbo].[tbl_User] " +
                             "WHERE LOWER(UserEmail) =LOWER(@Email) AND UserPassword =@Password";
            myCommand.CommandText = commandText;

            return myCommand;
        }

        private SQLServer_LoginUser getSQLUserValuesFromDataReader(ArrayList sqlValue)
        {
            SQLServer_LoginUser sqlServerUser = new SQLServer_LoginUser(Constants.Login.NoUserFound, Constants.Login.NoUserFound, Constants.Login.NoUserFound);

            int readCounter = 1;
            foreach (Object[] row in sqlValue)
            {
                string userID = row[0].ToString();
                string userName = row[1].ToString();
                string userEmail = row[2].ToString();

                sqlServerUser = readCounter == 1
                   ? new SQLServer_LoginUser(userEmail, userID, userName)
                   : new SQLServer_LoginUser(Constants.Login.MultipleUsersFound, Constants.Login.MultipleUsersFound, Constants.Login.MultipleUsersFound);

                readCounter++;
            }
            return sqlServerUser;
        }
        #endregion

        #region SQLServer_EntityLogin

        public ActionResult SQLServer_EntityLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SQLServer_EntityLogin(FormCollection collection)
         {
            string email = collection["Email"];
            string password = collection["Password"];
            string encryptedPassword = Encryptor1.Encrypt(password);

            IQueryable<tbl_Login> logins = SqlsEntityGateway.GetLogins(email, encryptedPassword);
            SQLServer_EntityUser sqlsUserResult = GetSqls_EntityLoginResult(logins);
            SetSessionUser(sqlsUserResult);

            string constantResult = !sqlsUserResult.email.Equals(Constants.Login.MultipleUsersFound) &&
                                    !sqlsUserResult.email.Equals(Constants.Login.NoUserFound)
                ? Constants.Login.Succes
                : sqlsUserResult.email;

            ActionResult previousPageActionResult = CheckSessionForCurrentActionDescriptor();
            return Json(new { result = constantResult, actionResult = previousPageActionResult });
        }

        private SQLServer_EntityUser GetSqls_EntityLoginResult(IQueryable<tbl_Login> tblLogins)
        {
            int numberOfRetrievedLogins = tblLogins.Count();
            if (numberOfRetrievedLogins == 1)
            {
                tbl_Login firstLogin = tblLogins.FirstOrDefault();
                tbl_User loginUser = firstLogin.tbl_User;
                tbl_User bobberdieanus = firstLogin.tbl_User1;

                string email = firstLogin.LoginEmail;
                string userID = loginUser.UserID.ToString();
                SQLServer_EntityUser sqlsUser = new SQLServer_EntityUser(email, userID, loginUser);
                return sqlsUser;
            }
            else if (numberOfRetrievedLogins < 1)
            {
                string noUserFound = Constants.Login.NoUserFound;
                tbl_User emptyUser = new tbl_User();
                SQLServer_EntityUser emptySqlsUser = new SQLServer_EntityUser(noUserFound, noUserFound, emptyUser);
                return emptySqlsUser;
            }
            else //numberOfRetrievedLogins > 1
            {
                string multipleUserFound = Constants.Login.MultipleUsersFound;
                tbl_User emptyUser = new tbl_User();
                SQLServer_EntityUser emptySqlsUser = new SQLServer_EntityUser(multipleUserFound, multipleUserFound, emptyUser);
                return emptySqlsUser;
            }
        }

        private void SetSessionUser(SQLServer_EntityUser sqlsUser)
        {
            if (!sqlsUser.email.Equals(Constants.Login.NoUserFound) &&
                !sqlsUser.email.Equals(Constants.Login.MultipleUsersFound))
            {
                SqlServer_EntityUser = sqlsUser;
            }
        }

        #endregion

    }
}