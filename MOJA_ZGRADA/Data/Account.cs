using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Data
{
    public class Account : IdentityUser
    {
        public string First_Name { get; set; }

        public string Last_Name { get; set; }

    }
}
