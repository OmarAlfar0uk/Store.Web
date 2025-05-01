using DomainLayer.Excptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class AuthenticationService(UserManager<ApplicationUser> _userManager  , IConfiguration _configuration) : IAuthenticationService
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
                    Token = await CreateTokenAsync(User)
                };
            }
            else
                throw new UnauthorizedException();

        }

        private  async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Name , user.UserName!),
                new Claim(ClaimTypes.NameIdentifier , user.Id!),
                
            };
            var Roles =await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));  
            var Creds = new SigningCredentials(Key , SecurityAlgorithms.HmacSha256);
             
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
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
                    Token =await CreateTokenAsync(User)

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
