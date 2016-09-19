using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime DueDate { get; set; }
    }
}