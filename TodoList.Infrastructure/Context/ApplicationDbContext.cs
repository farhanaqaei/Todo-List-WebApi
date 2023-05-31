using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Todo.Entities;
using TodoList.Domain.User.Entities;

namespace TodoList.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; } 
}
