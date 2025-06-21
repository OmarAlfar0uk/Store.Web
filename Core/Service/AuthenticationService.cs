using AutoMapper;
using DomainLayer.Excptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    internal class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration , IMapper _mapper) : IAuthenticationService
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

        public async Task<bool> CheckEmailAsync(string email)
        {
            var User  =await _userManager.FindByEmailAsync(email);    
            return User is not null;
        }

        public async Task<UserDto> GetCreanteUserAsync(string email)
        {
            var User =await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto() { DisplayName = User.DisplayName, Email = User.Email, Token = await CreateTokenAsync(User) };

        }

        public async Task<AddressDto> GetCreateUserAddressAsync(string email)
        {
            var  User =await _userManager.Users.Include(U=>U.Address)
                .FirstOrDefaultAsync(u => u.Email == email)??throw new UserNotFoundException(email);
          
                return _mapper.Map<Address, AddressDto>(User.Address);
           
        }

        public async Task<AddressDto> UpdateCreateUserAddressAsync(string email, AddressDto addressDto)
        {
            var User = await _userManager.Users.Include(U => U.Address)
                                         .FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);
            if(User is not null)    //Update
            {
                User.Address.FirstName=addressDto.FirstName;    
                User.Address.LastName=addressDto.LastName;    
                User.Address.City=addressDto.City;
                User.Address.Street=addressDto.Street;
                User.Address.Country=addressDto.Country;
                
                
            }
            else //Add New Address
            {
                User.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }


            await _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDto>(User.Address);

        }


    }
}
