using System;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; } //identifier
        public string Type { get; set; }
        public string Descriprion { get; set; }
        // public bool IsComplete { get; set; }
        public DateTime DateOfCompletion { get; set; }
        
    }
}
