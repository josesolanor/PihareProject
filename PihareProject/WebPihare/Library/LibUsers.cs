using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPihare.Library
{
    public class LibUsers : LibObject
    {
        public LibUsers()
        {
        }
        public LibUsers(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _usersRoles = new UsersRoles();
        }
        public LibUsers(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _usersRoles = new UsersRoles();
        }

        internal async Task<List<object[]>> userLogin(string userName, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

    }
}
