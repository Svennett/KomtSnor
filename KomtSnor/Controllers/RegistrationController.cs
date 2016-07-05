using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KomtSnor.Domain;
using KomtSnor.Domain.Encryptor;
using KomtSnor.EntityFramework;
using KomtSnor.Gateways;
using KomtSnor.Models;

namespace KomtSnor.Controllers
{
    public class RegistrationController : BaseController
    {

        #region SQLServer_Entity registration
        // GET: Register
        public ActionResult SQLServer_EntityRegistration()
        {
            RegistrationViewModels registrationModel = new RegistrationViewModels();
            registrationModel.Countries = SqlsEntityGateway.GetAllCountries();
            return View(registrationModel);
        }

        [HttpPost]
        public ActionResult SQLServer_EntityRegistration(FormCollection collection)
        {
            string name = collection["name"];
            string email = collection["email"];
            string password = collection["password1"];
            string encryptedPassword = Encryptor1.Encrypt(password);
            string gender = collection["gender"];
            string country = collection["country"];

            HttpPostedFileBase file = Request.Files["imageInput"];
            if (file != null)
            {
                byte[] fileInBytes = new byte[file.InputStream.Length];
                file.InputStream.Read(fileInBytes, 0, fileInBytes.Length);
            }

            tbl_User newUser = new tbl_User()
            {
                UserName = name,
                UserGender = gender, 
            };

            int userID = newUser.UserID;

            tbl_Login newLogin = new tbl_Login()
            {
                LoginEmail = email,
                LoginPassword = encryptedPassword,
                LoginStatus = Constants.AccountStatus.NoEmail,
                LoginCreationDate = DateTime.Now,
                UserID = newUser.UserID
            };

            SqlsEntityGateway.InsertObject(newUser);
            SqlsEntityGateway.InsertObject(newLogin);

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
            IQueryable<tbl_Login> loginsWithEmail = SqlsEntityGateway.GetAllLogins().Where(login => login.LoginEmail.Equals(email));
            int emailCount = loginsWithEmail.Count();
            string emailResult = emailCount > 0 ? "Taken" : "Free";
            return Json(new { result = emailResult });
        }

        #endregion

        #region Country registration
        public ActionResult CountryRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CountryRegistration(FormCollection collection)
        {
            string countryName = collection["countryName"];
            string countryCode = collection["countryCode"];

            HttpPostedFileBase file = Request.Files["imageInput"];
            byte[] fileInBytes = new byte[file.InputStream.Length];
            file.InputStream.Read(fileInBytes, 0, fileInBytes.Length);

            tbl_Country newCountry = new tbl_Country();
            newCountry.CountryName = countryName;
            newCountry.CountryCode = countryCode;
            newCountry.CountryFlag = fileInBytes;

            SqlsEntityGateway.InsertObject(newCountry);

            return View();
        }
        #endregion
    }
}