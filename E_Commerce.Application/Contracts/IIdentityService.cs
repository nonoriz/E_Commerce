using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default!);

        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default!);

        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default!);

        Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default!);

        Task<Result<bool>> EmailExistsAsync(string email, CancellationToken ct= default!);

        Task<Result<AddressDto>> GetUserAddressByEmailAync(string email, CancellationToken ct = default!);

        Task<Result<AddressDto>> UpdateOrInsertUserAddresssAsync(string email, AddressDto addressDto, CancellationToken ct = default);


    }
}
