﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Interop.CS.Models.Helpers
{
    public class CustomUserValidator<TUser> : IIdentityValidator<TUser> where TUser : class, IUser
    {
        private readonly UserManager<TUser> _userManager;

        public CustomUserValidator(UserManager<TUser> manager)
        {
            _userManager = manager;
        }

        //Опис: Методот проверува дали корисничкото име што се додава веќе постои
        //Влезни параметри: Објект од класата TUser
        public async Task<IdentityResult> ValidateAsync(TUser user)
        {
            var errors = new List<string>();

            if (_userManager != null)
            {
                //check username availability. and add a custom error message to the returned errors list.
                var existingAccount = await _userManager.FindByNameAsync(user.UserName);
                if (existingAccount != null && existingAccount.Id != user.Id)
                    errors.Add("User name already in use ...");
            }

            //set the returned result (pass/fail) which can be read via the Identity Result returned from the CreateUserAsync
            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }
}
