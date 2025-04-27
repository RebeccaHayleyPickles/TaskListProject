namespace TaskListProject.Models;

using TaskListProject.Enums;

public class ToDoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public string DueDate { get; set; } = string.Empty;
}