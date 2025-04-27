using Microsoft.EntityFrameworkCore;
using TaskListProject.Controllers;
using TaskListProject.Models;
using TaskListProject.Enums;
using Xunit;

namespace TaskListProject.Tests;

public class ToDoItemsControllerTests : IDisposable
{
    private readonly TaskListDbContext _context;
    private readonly ToDoItemsController _controller;

    public ToDoItemsControllerTests()
    {
        var options = new DbContextOptionsBuilder<TaskListDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // 💡 unique name
        .Options;

        _context = new TaskListDbContext(options);
        _controller = new ToDoItemsController(_context);
    }

    [Fact]
    public async Task GetToDos_ReturnsAllToDos()
    {
        // Arrange
        _context.ToDoItems.Add(new ToDoItem { Title = "ToDo 1", Status = "Status.NotStarted" });
        _context.ToDoItems.Add(new ToDoItem { Title = "ToDo 2", Status = "Status.Completed" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetToDoItems();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count());
    }

    [Fact]
    public async Task GetToDo_ReturnsToDoById()
    {
        // Arrange
        var toDo = new ToDoItem { Title = "ToDo", Status = "Status.InProgress" };
        _context.ToDoItems.Add(toDo);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetToDoItem(toDo.Id);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(toDo.Title, result.Value.Title);
    }

    [Fact]
    public async Task CreateToDoItem_AddsNewToDoItem()
    {
        // Arrange
        var newToDo = new ToDoItem
        {
            Title = "New ToDo",
            Description = "Description",
            Status = "Status.NotStarted",
            DueDate = DateTime.UtcNow.ToString()
        };

        // Act
        var result = await _controller.CreateToDoItem(newToDo);

        // Assert
        Assert.Equal(1, _context.ToDoItems.Count());
        Assert.Equal("New ToDo", _context.ToDoItems.First().Title);
    }

    [Fact]
    public async Task UpdateToDo_UpdatesExistingToDo()
    {
        // Arrange
        var toDo = new ToDoItem { Title = "Old ToDo", Status = "Status.NotStarted" };
        _context.ToDoItems.Add(toDo);
        await _context.SaveChangesAsync();

        var updatedToDo = toDo;
        updatedToDo.Title = "Updated ToDo";
        updatedToDo.Description = "Updated Description";
        updatedToDo.Status = "Status.Completed";
        updatedToDo.DueDate = DateTime.UtcNow.ToString();

        // Act
        var result = await _controller.UpdateTodoItem(toDo.Id.ToString(), updatedToDo);

        // Assert
        var updatedEntity = _context.ToDoItems.First();
        Assert.Equal("Updated ToDo", updatedEntity.Title);
        Assert.Equal("Updated Description", updatedEntity.Description);
        Assert.Equal("Status.Completed", updatedEntity.Status);
    }

    [Fact]
    public async Task DeleteToDo_RemovesToDo()
    {
        // Arrange
        var toDo = new ToDoItem { Title = "ToDo to Delete", Status = "Status.Completed" };
        _context.ToDoItems.Add(toDo);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.DeleteToDoItem(toDo.Id);

        // Assert
        Assert.Empty(_context.ToDoItems);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
