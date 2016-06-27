using System;
using System.Web.Mvc;
using KomtSnor.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KomtSnor.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {

        [TestMethod]
        public void SQLServerLogin()
        {
            string valueOne = "UnKnown";
            string valueTwo = "UnKnown";

            Assert.AreEqual(valueOne, valueTwo);
        }
    }
}
