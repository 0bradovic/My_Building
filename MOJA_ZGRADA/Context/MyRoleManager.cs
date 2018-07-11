using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Context
{
    public class MyRoleManager : IdentityRole
    {
        public MyRoleManager() : base()
        {
        }

        public MyRoleManager(string options) : base(options)
        {
        }
    }
}
