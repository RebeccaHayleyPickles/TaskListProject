namespace TaskListProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskListProject.Enums;
using TaskListProject.Models;

[ApiController]
[Route("api/ToDoItems")]
public class ToDoItemsController : ControllerBase
{
    private readonly TaskListDbContext _context;

    public ToDoItemsController(TaskListDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
    {
        return await _context.ToDoItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItem>> GetToDoItem(Guid id)
    {
        var task = await _context.ToDoItems.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItem>> CreateTask(ToDoItem toDoItem)
    {
        _context.ToDoItems.Add(toDoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStatus(Guid id, Status status)
    {
        var toDoItem = await _context.ToDoItems.FindAsync(id);
        if (toDoItem == null)
        {
            return NotFound();
        }

        toDoItem.Status = status;

        _context.Entry(toDoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ToDoItems.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoItem(Guid id)
    {
        var toDoItem = await _context.ToDoItems.FindAsync(id);
        if (toDoItem == null)
        {
            return NotFound();
        }

        _context.ToDoItems.Remove(toDoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
