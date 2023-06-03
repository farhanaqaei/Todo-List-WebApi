using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoList.Domain.UserAggregate.Entities;

namespace TodoList.Application.UserServices.Interfaces;

public interface ITokenService
{

	string CreateToken(User user);
	//JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration);
	//List<Claim> CreateClaims(User user);
	//SigningCredentials CreateSigningCredentials();

}
