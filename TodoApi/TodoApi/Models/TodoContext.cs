/*
    Контекст базы данных —это основной класс, который координирует функциональные
    возможности Entity Framework для модели данных.
    Этот класс является производным от класса Microsoft.EntityFrameworkCore.DbContext.
*/

using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}