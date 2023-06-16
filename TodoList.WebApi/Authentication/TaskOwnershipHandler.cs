using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing.Patterns;
using TodoList.Application.TodoServices.Interfaces;
using TodoList.WebApi.Extensions;

namespace TodoList.WebApi.Authentication;

public class TaskOwnershipHandler : AuthorizationHandler<TaskOwnershipRequirement>
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ITodoService _todoService;

	public TaskOwnershipHandler(IHttpContextAccessor httpContextAccessor, ITodoService todoService)
	{
		_httpContextAccessor = httpContextAccessor;
		_todoService = todoService;
	}

	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TaskOwnershipRequirement requirement)
	{
		var httpContext = _httpContextAccessor.HttpContext;
		var httpMethod = httpContext.Request.Method;

		try
		{
			if (httpMethod.Equals(HttpMethods.Patch, StringComparison.OrdinalIgnoreCase))
			{
				// Get the todoId from the route parameters
				if (httpContext.Request.RouteValues.TryGetValue("todoId", out var todoIdValue) &&
					long.TryParse(todoIdValue.ToString(), out var todoId))
				{
					// Get todo from todoService
					var todo = await _todoService.GetTaskById(todoId);

					// Check if the user has permission to access the todo
					if (todo.Data.UserId == context.User.GetUserId())
					{
						context.Succeed(requirement);
						return;
					}
				}
			}
			else if (httpMethod.Equals(HttpMethods.Delete, StringComparison.OrdinalIgnoreCase))
			{
				// Get the todoId from the query string
				if (httpContext.Request.Query.TryGetValue("todoId", out var todoIdValue) &&
					long.TryParse(todoIdValue, out var todoId))
				{
					// Get todo from todoService
					var todo = await _todoService.GetTaskById(todoId);

					// Check if the user has permission to access the todo
					if (todo.Data.UserId == context.User.GetUserId())
					{
						context.Succeed(requirement);
						return;
					}
				}
			}

			context.Fail();
		}
		catch (Exception)
		{
			context.Fail();
		}

	}
}