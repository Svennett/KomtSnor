using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KomtSnor.EntityFramework;

namespace KomtSnor.Models
{
    public class RegistrationViewModels
    {
        public IQueryable<tbl_Country> Countries;
    }
}