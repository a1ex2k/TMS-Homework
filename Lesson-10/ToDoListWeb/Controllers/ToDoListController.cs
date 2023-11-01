using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoListWeb.Data;
using ToDoListWeb.Data;
using ToDoListWeb.Services;
using ToDoListWeb.Utility;

namespace ToDoListWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;
        private readonly ILogger<ToDoListController> _logger;

        public ToDoListController(ITodoListService todoListService, ILogger<ToDoListController> logger)
        {
            _todoListService = todoListService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var allTasks = await _todoListService.GetAllAsync();
            return Ok(allTasks.ToDto());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var task = await _todoListService.GetByIdAsync(id);
            if (task is null)
            {
                return NotFound("There isn't any task with such ID");
            }

            return Ok(task.ToDto());
        }

        [HttpGet]
        [Route("queued")]
        public async Task<IActionResult> GetQueuedAsync()
        {
            var tasks = await _todoListService.GetNewAsync();
            return Ok(tasks.ToDto());
        }

        [HttpGet]
        [Route("completed")]
        public async Task<IActionResult> GetCompletedAsync()
        {
            var tasks = await _todoListService.GetCompletedAsync();
            return Ok(tasks.ToDto());
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddTaskAsync([FromBody] NewToDoTask taskDto)
        {
            var newTask = new ToDoTask(taskDto.Text, taskDto.CreatedAt, taskDto.CompletedAt);
            var createdTask = await _todoListService.AddNewAsync(newTask);
            return Ok(new TaskInfo()
            {
                TaskId = createdTask.Id
            });
        }

        [HttpPost]
        [Route("markAsCompleted")]
        public async Task<IActionResult> AddTaskAsync([FromBody] CompletedTask completedTask)
        {
            var editedTask =
                await _todoListService.MarkAsCompletedAsync(completedTask.TaskId, completedTask.CompletionDateTime);

            if (editedTask is null)
            {
                return NotFound("There isn't any task with such ID");
            }

            return Ok(new TaskInfo()
            {
                TaskId = editedTask.Id
            });
        }

        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemoveTaskAsync([FromBody] TaskInfo taskInfo)
        {
            var removedTask =
                await _todoListService.RemoveByIdAsync(taskInfo.TaskId);

            if (removedTask is null)
            {
                return NotFound("There isn't any task with such ID");
            }

            return Ok(new TaskInfo()
            {
                TaskId = removedTask.Id
            });
        }
    }
}