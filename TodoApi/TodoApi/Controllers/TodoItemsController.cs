using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;


        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            TodoItem todoItem = new TodoItem();

            if (todoItemDTO.Type == "work" || todoItemDTO.Type == "personal")
            {
                todoItem.Id = 0;
                todoItem.Type = todoItemDTO.Type;
                todoItem.Description = todoItemDTO.Description;
                todoItem.DateOfCompletion = todoItemDTO.DateOfCompletion;
                todoItem.IsComplete = todoItemDTO.IsComplete;

                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("New task created!");
            
                return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: can't create todoitem, because type is incorrect. Try with \"work\" or \"personal\"\n");
                return NoContent();
            }
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            if (todoItemDTO.Type != "work" && todoItemDTO.Type != "personal")
            { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: can't change todoitem, because type is incorrect. Try with \"work\" or \"personal\"\n");
                return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
            }

            todoItem.Type = todoItemDTO.Type;
            todoItem.Description = todoItemDTO.Description;
            todoItem.DateOfCompletion = todoItemDTO.DateOfCompletion;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id)) { return NotFound(); }
                else { throw; }
            }

            return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
        }

        // PATCH: api/TodoItems/5 
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            if (todoItem.IsComplete == false) { todoItem.IsComplete = !todoItem.IsComplete; } 
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
        }
        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
        new TodoItemDTO
        {
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete,
            Type = todoItem.Type,
            DateOfCompletion = todoItem.DateOfCompletion
        };
    }
}
