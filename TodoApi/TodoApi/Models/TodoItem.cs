using System;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; } //identifier
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime date { get; set; }
    }
}
