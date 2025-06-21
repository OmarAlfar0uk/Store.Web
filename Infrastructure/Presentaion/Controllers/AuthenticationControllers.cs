using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class AuthenticationControllers(IServiceManger _serviceManger) : ApiBaseControlar
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =await _serviceManger.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = _serviceManger.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);    
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheakEmailAsync(string Email)
        {
            var Result =await _serviceManger.AuthenticationService.CheckEmailAsync(Email);
            return Ok(Result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser =await  _serviceManger.AuthenticationService.GetCreanteUserAsync(email);
            return Ok(AppUser); 
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddres()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address =await _serviceManger.AuthenticationService.GetCreateUserAddressAsync(email);
            return Ok(Address); 

        }


        [Authorize]
        [HttpPut("Address")]

        public async Task<ActionResult<AddressDto>>UpdateCurrentUserAsync(AddressDto AddressDto)
        {
            var email =User.FindFirstValue(ClaimTypes.Email);   
            var UpdateAddress =await _serviceManger.AuthenticationService.UpdateCreateUserAddressAsync(email , AddressDto);
            return Ok(UpdateAddress);
        }
    }
}

