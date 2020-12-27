using System.ComponentModel.DataAnnotations;
using src.Domain.Entities.Enum;

namespace src.Domain.DTOs.Todo
{
    public class InsertTodoDTO
    {
        [Required(ErrorMessage = "Cant Blank")]
        public string Description { get; set; }
        public string Author { get; set; }

        [Required(ErrorMessage = "Cant Blank")]
        public Priority Priority { get; set; }
        public bool Status { get; set; }
    }
}