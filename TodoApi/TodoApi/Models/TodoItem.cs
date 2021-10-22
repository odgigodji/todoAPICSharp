using System;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; } //identifier
        public string Type {  get; set; }
        //     get 
        //     {
        //         return this.Type;
        //     }
        //     set
        //     {
        //         if(value != "work" || value != "personal") {
        //             this.Type = "unidentified";
        //         } else { this.Type = value; }
        //     }
        // }
        public string Description { get; set; }
        public DateTime DateOfCompletion { get; set; }
    }
}
