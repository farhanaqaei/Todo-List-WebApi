using System.ComponentModel.DataAnnotations;

namespace TodoList.Domain.Common.Entities;

public interface IBaseEntity
{
	[Key]
	public long Id { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime CreateDate { get; set; }
	public DateTime LastUpdateDate { get; set; }
}
