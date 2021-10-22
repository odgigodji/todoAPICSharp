using System;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; } //identifier
        public string Type { get; set; }
        public string Description { get; set; }
        // public DateTime DateOfCompletion { get; set; }

        // public DateTime Date
        // {
        //     get { return Date; }
        //     set
        //     {
        //         // Date = DateTime.Now;
        //         // Console.WriteLine(Date);
        //         var dateAndTime = Date;
        //         Date = dateAndTime.Date;
        //     }
        // } 
    }
}
