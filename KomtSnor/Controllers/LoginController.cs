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
using Microsoft.Ajax.Utilities;

namespace KomtSnor.Controllers
{
    public class LoginController : BaseController
    {
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



        public ActionResult FacebookLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FacebookLogin(FormCollection collection)
        {
            return View();
        }


        public ActionResult SQLServerLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SQLServerLogin(FormCollection collection)
        {
            string email = collection["Email"];
            string password = collection["Password"];
            string encryptedPassword = Encryptor1.Encrypt(password);
            SqlCommand queryCommand = CreateSQLCommand(email, encryptedPassword);

            ArrayList sqlValues = SQLServerGateway.ExecuteSelectCommand(queryCommand);

            SQLServerUser sqlServerUser = getSQLUserValuesFromDataReader(sqlValues);
            string loginResult = (sqlServerUser.email != Constants.Login.MultipleUsersFound && sqlServerUser.email != Constants.Login.NoUserFound) ? Constants.Login.Succes : sqlServerUser.email;
            this.SqlServerUser = loginResult == Constants.Login.Succes ? sqlServerUser : null;

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

        private SQLServerUser getSQLUserValuesFromDataReader(ArrayList sqlValue)
        {
            SQLServerUser sqlServerUser = new SQLServerUser(Constants.Login.NoUserFound, Constants.Login.NoUserFound, Constants.Login.NoUserFound);

            int readCounter = 1;
            foreach (Object[] row in sqlValue)
            {
                string userID = row[0].ToString();
                string userName = row[1].ToString();
                string userEmail = row[2].ToString();

                sqlServerUser = readCounter == 1
                   ? new SQLServerUser(userEmail, userID, userName)
                   : new SQLServerUser(Constants.Login.MultipleUsersFound, Constants.Login.MultipleUsersFound, Constants.Login.MultipleUsersFound);

                readCounter++;
            }
            return sqlServerUser;
        }


    }
}