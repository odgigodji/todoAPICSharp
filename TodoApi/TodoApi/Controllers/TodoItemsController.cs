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

        // static int staticId = 0;
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
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            // var todoItem = await _context.TodoItems.FindAsync(id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("id is " + todoItem.Id + "\n");

            if (todoItem.Type == "work" || todoItem.Type == "personal")
            {
                // todoItem.Type = todoIte.Type;
                // todoItem.Description = todoItemDTO.Description;
                // todoItem.DateOfCompletion = todoItemDTO.DateOfCompletion;
                // todoItem.IsComplete = todoItemDTO.IsComplete;

                // staticId++;

                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: can't create todoitem, because type is incorrect. Try with \"work\" or \"personal\"\n");
                return NoContent();
            }
        }

        // GET: api/TodoItems/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        // {
        //     var todoItem = await _context.TodoItems.FindAsync(id);

        //     if (todoItem == null)
        //     {
        //         Console.ForegroundColor = ConsoleColor.Red;
        //         Console.WriteLine("Error:Todoitem with id=" + id + " not found");
        //         return NotFound();
        //     }

        //     return todoItem;
        // }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            if (todoItemDTO.Type != "work" || todoItemDTO.Type != "personal")
            { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: can't change todoitem, because type is incorrect. Try with \"work\" or \"personal\"\n");
                return NoContent();
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

            return NoContent();
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

            return NoContent();
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

            return NoContent();
        }
        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
        new TodoItemDTO
        {
            // Id = todoItem.Id,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete,
            Type = todoItem.Type,
            DateOfCompletion = todoItem.DateOfCompletion
        };
    }
}

/*
[HttpPatch("{id}")]
public async Task<IActionResult> PatchCard(long id, CardBalanceDto balanceDto)
{
    var card = new Card() { Id = id, Balance = balanceDto.Balance};
    _context.Cards.Attach(Card);
    _context.Entry(card).Property(x => x.Balance).IsModified = true;
    return Ok(await _context.SaveChangesAsync());
}
Your DTO would look something like this:

public class CardBalanceDto {
    public decimal Balance { get; set; }
}
*/