using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskBoardApp.Data.DataConstants.BoardNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}

//•	Id – a unique integer, Primary Key
//•	Name – a string with min length 3 and max length 30 (required)
//•	Tasks – a collection of Task
