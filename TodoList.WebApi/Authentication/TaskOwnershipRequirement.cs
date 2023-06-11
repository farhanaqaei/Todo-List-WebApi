using Microsoft.AspNetCore.Authorization;

namespace TodoList.WebApi.Authentication;

public class TaskOwnershipRequirement : IAuthorizationRequirement
{
}
