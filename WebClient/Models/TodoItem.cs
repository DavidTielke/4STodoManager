using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class TodoItem
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Beschriebung muss gesetzt sein")]
        [RegularExpression(@"^.*$")]
        [DisplayName("Beschreibung")]
        public string Text { get; set; }
        public bool IsDone { get; set; }

        [DisplayName("Fälligkeit")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
    }
}