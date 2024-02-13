namespace TaskBoardApp.Models.Task
{
    public class TaskDetailModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedOn {  get; set; } = string.Empty;
        public string Board { get; set; } = string.Empty;

        public string Owner {  get; set; } = string.Empty;
   
    }
}
