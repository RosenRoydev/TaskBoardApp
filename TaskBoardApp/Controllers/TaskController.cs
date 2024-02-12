using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Task;
using Task = TaskBoardApp.Data.Models.Task;




namespace TaskBoardApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskBoardAppDbContext data;

        public TaskController(TaskBoardAppDbContext _data)
        {
            data = _data;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            TaskFormModel taskModel = new TaskFormModel()
            {
                // Populate the Boards property
                Boards = GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public async Task <IActionResult> Create(TaskFormModel taskFormModel)
        {
            taskFormModel.Boards = GetBoards();

            if (!GetBoards().Any(b => b.Id == taskFormModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskFormModel), "Board does not exist");
            }

            string currentUserId = GetUserId();

            if (!ModelState.IsValid)
            {
                taskFormModel.Boards = GetBoards();

                return View (taskFormModel);
            }

            Task task = new Task()
            {
                Title = taskFormModel.Title,
                Description = taskFormModel.Description,
                CreatedOn = DateTime.Now,
                BoardId = taskFormModel.BoardId,
                OwnerId = currentUserId
            };

            var boards = data.Boards;

            await data.Tasks.AddAsync(task);
            await data.SaveChangesAsync();

            return RedirectToAction("All","Board");
        }
        private IEnumerable<TaskBoardModel> GetBoards()
         => data.Boards.Select(x => new TaskBoardModel
         {
             Id = x.Id,
             Name = x.Name,
         });

        private string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
