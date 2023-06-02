using System.ComponentModel.DataAnnotations;

namespace TodoList.Domain.UserAggregate.Dtos;

public class RegisterUserDTO
{
    [Required]
    public string FullName { get; set; }

	[Required]
	public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
