﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using RfidSPA.Helpers;
using Microsoft.AspNetCore.Identity;
using RfidSPA.Models;
using RfidSPA.Models.Entities;
using RfidSPA.Auth;
using Microsoft.Extensions.Options;
using RfidSPA.ViewModels;
using RfidSPA.Service;
using static RfidSPA.Helpers.Constants;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RfidSPA.Controllers.API
{
    [Route("api/[controller]")]
   
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IStoreRepository _storeRepository;

        public AuthController(UserManager<ApplicationUser> userManager, 
            IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions,
            IStoreRepository storeRepository)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _storeRepository =storeRepository;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user = await _userManager.FindByEmailAsync(credentials.UserName);
            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var userID = identity.Claims.Single(c => c.Type == "id").Value;
           var  userRole = await _userManager.GetRolesAsync(user);
            long? storeID = null;
            if(userRole.Contains(UserRolesConst.StoreAdministrator) || userRole.Contains(UserRolesConst.StoreOperator))
            {

                storeID = await _storeRepository.GetstoreIdByUser(userID);

            }




            // Serialize and return the response
            var response = new
            {

                Rfid_AppliactionUserID = userID, 
                userRoles = userRole,
                auth_token = await _jwtFactory.GenerateEncodedToken(credentials.UserName, identity),
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                store_id = storeID
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(userName);

                if (userToVerify != null)
                {
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
