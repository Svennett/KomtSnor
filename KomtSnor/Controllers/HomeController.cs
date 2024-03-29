﻿using KomtSnor.Controllers;
using KomtSnor.Domain;
using KomtSnor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<string> authenticationConstants = GetConstants(typeof(Constants.Authentication));
            List<string> loggedInAuthenticationConstants = GetLoggedInAuthentications(authenticationConstants);

            HomeViewModels homeViewModel = new HomeViewModels();
            homeViewModel.authenticationList = loggedInAuthenticationConstants;

            return View(homeViewModel);
        }

        private List<string> GetLoggedInAuthentications(List<string> authenticationConstants)
        {
            return authenticationConstants.Where(authenticationConstant => Session[authenticationConstant] != null).ToList();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}