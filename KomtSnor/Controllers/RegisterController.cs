using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KomtSnor.Domain.Encryptor;
using KomtSnor.Gateways;

namespace KomtSnor.Controllers
{
    public class RegisterController : BaseController
    {
        // GET: Register
        public ActionResult SQLServerRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SQLServerRegister(FormCollection collection)
        {
            string name = collection["name"];
            string email = collection["email"];
            string password = collection["password1"];
            string encryptedPassword = Encryptor1.Encrypt(password);

            SqlCommand insertCommand = CreateSQLCommand(name, email, encryptedPassword);
            SQLServerGateway.ExecuteInsertCommand(insertCommand);

            return RedirectToAction("Index", "Home");
        }

        private SqlCommand CreateSQLCommand(string name, string email, string encryptedPassword)
        {
            //create object
            SqlCommand myCommand = new SqlCommand();

            //set Paremeters
            SqlParameter[] commandParameters = new SqlParameter[3];
            commandParameters[0] = new SqlParameter("@Email", email);
            commandParameters[1] = new SqlParameter("@Password", encryptedPassword);
            commandParameters[2] = new SqlParameter("@Name", name);
            myCommand.Parameters.AddRange(commandParameters);

            //set command text
            string commandText = "INSERT INTO[dbo].[tbl_User] " +
                                 "([UserName],[UserEmail],[UserPassword])" +
                                 "VALUES (@name, @Email, @Password);";
            myCommand.CommandText = commandText;

            return myCommand;
        }

        [HttpPost]
        public ActionResult CheckSQLServerUserEmail(FormCollection collection)
        {
            string email = collection["email"];
            SqlCommand sqlCommand = CreateSqlServerEmailCheckCommand(email);
            ArrayList selectResult = SQLServerGateway.ExecuteSelectCommand(sqlCommand);
            int emailCount = GetSQLServerEmailCheckResult(selectResult);
            string emailResult = emailCount > 0 ? "Taken" : "Free";

            return Json(new { result = emailResult });
        }

        private SqlCommand CreateSqlServerEmailCheckCommand(string email)
        {
            //create object
            SqlCommand myCommand = new SqlCommand();

            //set Paremeters
            SqlParameter[] commandParameters = new SqlParameter[1];
            commandParameters[0] = new SqlParameter("@Email", email);
            myCommand.Parameters.AddRange(commandParameters);

            //set command text
            string commandText = "SELECT COUNT(*) " +
                                 "FROM [dbo].[tbl_User] " +
                                 "WHERE UserEmail = @Email;"
            ;
            myCommand.CommandText = commandText;

            return myCommand;
        }

        private int GetSQLServerEmailCheckResult(ArrayList sqlSelectResult)
        {
            Object[] firstRow = (Object[]) sqlSelectResult[0];
            int emailCount = Convert.ToInt32(firstRow[0]);
            return emailCount;
        }
    }
}