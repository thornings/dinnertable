using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMadbordet.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() { }

        public AppRole(string name)
        {
            Name = name;
        }

    }
}
