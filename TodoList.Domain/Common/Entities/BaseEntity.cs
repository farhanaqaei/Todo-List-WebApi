using System.ComponentModel.DataAnnotations;

namespace TodoList.Domain.Common.Entities;

public class BaseEntity : IBaseEntity
{
    [Key]
    public long Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
