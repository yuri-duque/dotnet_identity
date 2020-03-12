using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityForum.Models
{
    public class Usuario : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}