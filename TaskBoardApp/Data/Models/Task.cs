using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskBoardApp.Data.DataConstants.TitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(TaskBoardApp.Data.DataConstants.DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(BoardId))]
        public int BoardId { get; set; }

        public Board? Board { get; set; }

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public string OwnerId { get; set; } = null!;

        public IdentityUser Owner { get; set; } = null!;




    }
}
