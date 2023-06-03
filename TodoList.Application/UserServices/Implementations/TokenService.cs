using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Application.UserServices.Interfaces;
using TodoList.Domain.UserAggregate.Entities;

namespace TodoList.Application.UserServices.Implementations;

public class TokenService : ITokenService
{
	private readonly IConfiguration _config;

	public TokenService(IConfiguration config)
	{
		_config = config;
	}

	public string CreateToken(User user)
	{
		var expiration = DateTime.Now.AddMinutes(double.Parse(_config.GetSection("TokenExpirationMinutes").Value));

		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("SecretKey").Value));

		var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

		var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.FullName),
				new Claim(ClaimTypes.Email, user.Email)
			};

		var tokenOptions = new JwtSecurityToken(
			issuer: _config.GetSection("ValidIssuer").Value,
			audience: _config.GetSection("ValidAudience").Value,
			claims: claims,
			expires: expiration,
			signingCredentials: signingCredentials
			);

		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}
}
