namespace TaskListProject.Models;

using Microsoft.EntityFrameworkCore;

public class TaskListDbContext : DbContext
{
    public TaskListDbContext(DbContextOptions<TaskListDbContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
}
