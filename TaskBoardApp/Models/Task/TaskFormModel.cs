using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required(ErrorMessage = RequiredField)]
        [StringLength(TitleMaxLength,MinimumLength = TitleMinLength,ErrorMessage = MinimumStringLength)]
        public string Title {  get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = MinimumStringLength)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Board")]
        
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; } = new List<TaskBoardModel>();

    }
}
