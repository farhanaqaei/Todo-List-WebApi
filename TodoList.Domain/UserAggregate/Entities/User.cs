using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Common.Entities;
using TodoList.Domain.Common.Interfaces;
using TodoList.Domain.TodoAggregate.Entities;

namespace TodoList.Domain.UserAggregate.Entities;

public class User : CustomIdentityUser
{
	[Required]
    public string FullName { get; set; }

    public ICollection<Todo> Todos { get; set; }
}
