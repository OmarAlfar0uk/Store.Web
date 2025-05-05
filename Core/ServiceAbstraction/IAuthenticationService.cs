using Shared.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);

        Task<UserDto> RegisterAsync(RegisterDto registerDto);


        Task<bool> CheckEmailAsync(string email);   

        Task<AddressDto> GetCreateUserAddressAsync(string email );

        Task<AddressDto> UpdateCreateUserAddressAsync(string email, AddressDto addressDto);

        Task<UserDto> GetCreanteUserAsync(string email); 

    }
}
