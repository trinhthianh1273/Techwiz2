using Api.Models;
using System.Security.Claims;

namespace Api.IService;

public interface ICustomerAuthenticationService
{
    string CreateJwt(Customer user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    string CreateRefreshToken();
    Task<bool> CheckUserNameExistAsync(string username);
    Task<bool> CheckEmailExistAsync(string email);
}
