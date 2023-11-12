using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Data;
using ToDoListWeb.Filters;
using ToDoListWeb.Models;
using ToDoListWeb.Services;
using ToDoListWeb.Utility;

namespace ToDoListWeb.Controllers;

[AddRequestTimeHeaderFilter]
public class ToDoListController : Controller
{
    private readonly ITodoListService _todoListService;
    private readonly ILogger<ToDoListController> _logger;

    public ToDoListController(ITodoListService todoListService, ILogger<ToDoListController> logger)
    {
        _todoListService = todoListService;
        _logger = logger;
    }


    [HttpGet]
    public async Task<IActionResult> AllTasks()
    {
        var completedTasks = await _todoListService.GetCompletedAsync();
        var queuedTasks = await _todoListService.GetNewAsync();

        var model = new AllTasksModel
        {
            QueuedTasks = queuedTasks
                          .OrderByDescending(t => t.CreatedAt)
                          .Select(t => t.ToQueuedTaskModel())
                          .ToList(),
            CompletedTasks = completedTasks
                             .OrderByDescending(t => t.CompletedAt)
                             .Select(t => t.ToTaskModel())
                             .ToList(),
        };

        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Details([FromQuery] int id)
    {
        var task = await _todoListService.GetByIdAsync(id);
        if (task is null)
        {
            return RedirectToAction("AllTasks");
        }

        var queuedTasks = await _todoListService.GetNewAsync();
        var model = new DetailsModel
        {
            QueuedTasks = queuedTasks.Take(4)
                                     .OrderByDescending(t => t.CreatedAt)
                                     .Select(t => t.ToQueuedTaskModel())
                                     .ToList(),
            ToDoTask = task.ToTaskModel(),
        };

        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> AddNew()
    {
        var queuedTasks = await _todoListService.GetNewAsync();
        var model = new AddNewModel()
        {
            QueuedTasks = queuedTasks.Take(4)
                                     .OrderByDescending(t => t.CreatedAt)
                                     .Select(t => t.ToQueuedTaskModel())
                                     .ToList(),
        };

        return View("AddNew", model);
    }


    [HttpPost]
    public async Task<IActionResult> AddNew([FromForm] NewToDoTaskModel taskModel)
    {
        if (string.IsNullOrWhiteSpace(taskModel.Text))
        {
            TempData.Put("Alert", new AlertModel()
            {
                Success = false,
                Message = "New task text was empty"
            });
        }
        else
        {
            var task = new ToDoTask(taskModel.Text);
            await _todoListService.AddNewAsync(task);

            TempData.Put("Alert", new AlertModel()
            {
                Success = true,
                Message = "New task added successfully"
            });
        }

        return RedirectToRefererOrAllTasks();
    }


    [HttpPost]
    public async Task<IActionResult> Remove([FromForm] RemoveTaskModel taskModel)
    {
        var removed = (await _todoListService.RemoveByIdAsync(taskModel.TaskId)) is not null;

        TempData.PutAlert(new ()
        {
            Success = removed,
            Message = removed ? "Task removed successfully" : "Task is not present or already removed"
        });

        return RedirectToAction("AllTasks");
    }


    [HttpPost]
    public async Task<IActionResult> MarkAsCompleted([FromForm] MarkAsCompleteTaskModel taskModel)
    {
        var completed = (await _todoListService.MarkAsCompletedAsync(taskModel.TaskId, DateTime.Now)) is not null;

        TempData.PutAlert(new()
        {
            Success = completed,
            Message = completed ? "Task marked as completed successfully" : "Task is not present or already completed"
        });

        return RedirectToRefererOrAllTasks();
    }

    [HttpPost]
    public async Task<IActionResult> EditText([FromForm] UpdateTextModel taskModel)
    {
        var completed = (await _todoListService.UpdateTextAsync(taskModel.Id, taskModel.Text)) is not null;

        TempData.PutAlert(new()
        {
            Success = completed,
            Message = completed ? "Task text updated" : "Task text was not changed"
        });

        return RedirectToAction("AllTasks");
    }


    [NonAction]
    private IActionResult RedirectToRefererOrAllTasks()
    {
        var returnUrl = Request.Headers["Referer"];
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return RedirectToAction("AllTasks");
        }

        return Redirect(returnUrl);
    }
}