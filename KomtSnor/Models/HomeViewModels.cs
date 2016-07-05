using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KomtSnor.EntityFramework;

namespace KomtSnor.Models
{
    public class HomeViewModels
    {
        public List<string> authenticationList { get; set; }

        public IQueryable<tbl_Country> countries;
    }
}