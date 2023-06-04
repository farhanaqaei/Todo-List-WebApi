using System.Security.Claims;

namespace TodoList.WebApi.Extensions;

public static class IdentityExtensions
{
	public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
	{
		if (claimsPrincipal != null)
		{
			var data = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
			if (data != null) return Convert.ToInt64(data.Value);
		}
		
		return default(long);
	}
}
