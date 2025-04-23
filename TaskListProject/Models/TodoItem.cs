namespace TaskListProject.Models;

using TaskListProject.Enums;

public class ToDoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }
}