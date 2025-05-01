using DomainLayer.Excptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // Check If Email Is Exists
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
           // Check Password
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            // Check UserDto
            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = CreateTokenAsync(User)
                };
            }
            else
                throw new UnauthorizedException();

        }

        private static string CreateTokenAsync(ApplicationUser user)
        {
            return "TOKEN TODO";
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {

            // Mapping Register Dto =>Application User]
            var User = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.DisplayName
            };


            // Create User [Application User ]
            var Result =await _userManager.CreateAsync(User , registerDto.Password);
            if (Result.Succeeded) 
            {
                // Return UserDto
                return new UserDto() 
                {
                    DisplayName= User.DisplayName,
                    Email = User.Email,
                    Token = CreateTokenAsync(User)

                };
            }
            else
            {
                var Errors =  Result.Errors.Select(E=>E.Description).ToList();
                throw new BadRequestException(Errors);
            }     
        }
    }
}
