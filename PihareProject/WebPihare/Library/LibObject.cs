using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPihare.Models;

namespace WebPihare.Library
{
    public class LibObject
    {
        public String description, code;

        public UserData _userData;
        public UsersRoles _usersRoles;
        public IdentityError _identityError;

        public List<SelectListItem> _userRoles;

        public RoleManager<IdentityRole> _roleManager;
        public UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;

        public List<object[]> datalist = new List<object[]>();
    }
}
