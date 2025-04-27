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
        var toDoItem = await _context.ToDoItems.FindAsync(id);

        if (toDoItem == null)
        {
            return NotFound();
        }

        return toDoItem;
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItem>> CreateToDoItem(ToDoItem toDoItem)
    {
        _context.ToDoItems.Add(toDoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(string id, ToDoItem toDoItem)
    {
        if (!Guid.TryParse(id, out Guid inputId) || inputId != toDoItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(toDoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ToDoItems.Any(e => e.Id == inputId))
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
