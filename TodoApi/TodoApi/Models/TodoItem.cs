﻿using System;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Type {  get; set; }
        public string Description { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public bool Iscomplete { get; set; }
    }
}
