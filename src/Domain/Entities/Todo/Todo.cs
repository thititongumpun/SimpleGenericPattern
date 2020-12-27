using System.ComponentModel.DataAnnotations;
using src.Domain.Entities;
using src.Domain.Entities.Enum;

namespace src
{
    public class Todo : Entity
    {
        [Required]
        public string Description { get; set; }
        public string Author { get; set; }

        [Required]
        public Priority Priority { get; set; }
        public bool Status { get; set; }
    }
}