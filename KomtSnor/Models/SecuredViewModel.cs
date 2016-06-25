using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace KomtSnor.Models
{
    public class SecuredViewModel
    {
        public IEnumerable<MethodInfo> MethodsInfos { get; set; }

    }
}