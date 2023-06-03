using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Domain.Common.Entities;

public class CustomIdentityUser : IdentityUser, IBaseEntity
{
	[Key]
	public long Id { get; set; }
	public bool IsDeleted { get; set; } = false;
	public DateTime CreateDate { get; set; } = DateTime.Now;
	public DateTime LastUpdateDate { get; set; } = DateTime.Now;
}
