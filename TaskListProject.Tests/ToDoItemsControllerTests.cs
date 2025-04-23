using Microsoft.EntityFrameworkCore;
using TaskListProject.Controllers;
using TaskListProject.Models;
using Xunit;

namespace TaskListProject.Tests;

public class ToDoItemsControllerTests
{
    private TaskListDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TaskListDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TaskListDbContext(options);
    }

    [Fact]
    public async Task GetTasks_ReturnsAllTasks()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        context.Tasks.Add(new Task { Title = "Test Task 1", Status = Enums.Status.NotStarted });
        context.Tasks.Add(new Task { Title = "Test Task 2", Status = Enums.Status.Completed });
        await context.SaveChangesAsync();

        var controller = new TaskController(context);

        // Act
        var result = await controller.GetTasks();

        // Assert
        Assert.Equal(2, result.Value.Count());
    }

    [Fact]
    public async Task GetTask_ReturnsTaskById()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var task = new Task { Title = "Test Task", Status = Enums.Status.InProgress };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var controller = new TaskController(context);

        // Act
        var result = await controller.GetTask(task.Id);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(task.Title, result.Value.Title);
    }

    [Fact]
    public async Task CreateTask_AddsNewTask()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var controller = new TaskController(context);
        var newTask = new Task { Title = "New Task", Status = Enums.Status.NotStarted };

        // Act
        var result = await controller.CreateTask(newTask);

        // Assert
        Assert.Equal(1, context.Tasks.Count());
        Assert.Equal("New Task", context.Tasks.First().Title);
    }

    [Fact]
    public async Task UpdateTask_UpdatesExistingTask()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var task = new Task { Title = "Old Task", Status = Enums.Status.NotStarted };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var controller = new TaskController(context);
        task.Title = "Updated Task";

        // Act
        var result = await controller.UpdateTask(task.Id, task);

        // Assert
        Assert.Equal("Updated Task", context.Tasks.First().Title);
    }

    [Fact]
    public async Task DeleteTask_RemovesTask()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var task = new Task { Title = "Task to Delete", Status = Enums.Status.Completed };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var controller = new TaskController(context);

        // Act
        var result = await controller.DeleteTask(task.Id);

        // Assert
        Assert.Empty(context.Tasks);
    }
}
