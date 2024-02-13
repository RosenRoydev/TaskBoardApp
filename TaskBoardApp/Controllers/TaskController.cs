using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Task;
using Task = TaskBoardApp.Data.Models.Task;




namespace TaskBoardApp.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create()
        {
            var model = new TaskFormModel();
            model.Boards = await GetBoards();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            model.Boards = await GetBoards();

            if (!(await GetBoards()).Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model), "Board does not exist");
            }

            string currentUserId = GetUserId();

            if (!ModelState.IsValid)
            {
                model.Boards = await GetBoards();

                return View(model);
            }

            var entity = new Task()
            {
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                BoardId = model.BoardId,
                OwnerId = currentUserId
            };

            var boards = data.Boards;

            await data.Tasks.AddAsync(entity);
            await data.SaveChangesAsync();

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = await data.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName

                }).FirstOrDefaultAsync();
            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskFormModel model = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = await GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Edit(int id,TaskFormModel task)
        {
            var model = await data.Tasks.FindAsync(id);
            if (model == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();

            if (currentUserId != model.OwnerId) 
            { 
              
                return Unauthorized();
            
            }
           


            if (!ModelState.IsValid)
            {
                task.Boards = await GetBoards();

                return View(task);  
            }

            model.Title = task.Title;
            model.Description = task.Description;
            model.BoardId = task.BoardId;

            await data.SaveChangesAsync();
            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await data.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel model = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id,TaskViewModel model)
        {
            var task = await data.Tasks.FindAsync(id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            data.Tasks.Remove(task);
            await data.SaveChangesAsync();

            return RedirectToAction("All", "Board");
        }
        public async Task<IEnumerable<TaskBoardModel>> GetBoards()
        {
            return await data.Boards.Select(x => new TaskBoardModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
        }
        private string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }



}
