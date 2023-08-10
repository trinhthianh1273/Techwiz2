using Api.IService;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services;

public class AuthenticationService : IAuthenticationService
{
    protected readonly SoccerContext _context;

    public AuthenticationService(SoccerContext context)
    {
        _context = context;
    }
    public string CreateJwt(Customer user)
    {
        // để tạo jwt cần 3 thành phần: header, payload, signature
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("veryverysecret.....");
        // payload
        var identity = new ClaimsIdentity(new Claim[]
        {
                new Claim(ClaimTypes.Role, "Customer"),
                new Claim(ClaimTypes.Name, $"{user.Username}")
        });

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.Now.AddMinutes(15),
            SigningCredentials = credentials
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        return jwtTokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var key = Encoding.ASCII.GetBytes("veryverysecret.....");
        var tokenValidatorParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidatorParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
        {
            throw new SecurityTokenException("This is invalid token");
        }
        return principal;
    }


    public string CreateRefreshToken()
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(64);
        var refreshToken = Convert.ToBase64String(tokenBytes);

        var tokenInUser = _context.Customer
            .Any(a => a.Token == refreshToken);
        //.Any(a => a.RefreshToken == refreshToken);
        if (tokenInUser)
        {
            return CreateRefreshToken();
        }
        return refreshToken;
    }

    public async Task<bool> CheckUserNameExistAsync(string username)
    {
        return await _context.Customer.AnyAsync(x => x.Username == username);
    }

    public async Task<bool> CheckEmailExistAsync(string email)
    {
        return await _context.Customer.AnyAsync(x => x.Email == email);
    }

}
