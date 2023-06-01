using System.ComponentModel.DataAnnotations;
using TodoList.Domain.Common.Entities;
using TodoList.Domain.TodoAggregate.Entities;

namespace TodoList.Domain.UserAggregate.Entities;

public class User : BaseEntity
{
    [Required]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public ICollection<Todo> Todos { get; set; }
}
