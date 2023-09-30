using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ASPNETMVCCRUD.Controllers
{
    public class TasksController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;
        private readonly ILogger<TasksController> _logger;

        public TasksController(MVCDemoDbContext mvcDemoDbContext, ILogger<TasksController> logger)
        {
            this. mvcDemoDbContext = mvcDemoDbContext;
            this._logger = logger;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var tasks = await mvcDemoDbContext.Tasks.ToListAsync();
        //     return View(tasks);
        //}

        [HttpGet]
        public async Task<IActionResult> Index(string SearchById, string SearchByName, string SearchByAssigned)
        {
            //var tasks = await mvcDemoDbContext.Tasks.ToListAsync();
            //return View(tasks);
            _logger.LogInformation("Index Method  Executed");

            ViewData["CurrentName"] = SearchByName;
            ViewData["CurrentId"] = SearchById;
            ViewData["CurrentAssigned"] = SearchByAssigned;

            var tasks =( from t in mvcDemoDbContext.Tasks select t);

            if (!String.IsNullOrEmpty(SearchById))
            {
                _logger.LogInformation("SearchById : "+SearchById);
                tasks = tasks.Where(t => t.TaskId.Equals(Int16.Parse(SearchById)));
            }
            if (!String.IsNullOrEmpty(SearchByName))
            {
                _logger.LogInformation("SearchByName : "+SearchByName);
                tasks = tasks.Where(t => t.TaskName.Contains(SearchByName));
            }
            if (!String.IsNullOrEmpty(SearchByAssigned))
            {
                _logger.LogInformation("SearchByAssigned : "+SearchByAssigned);
                tasks = tasks.Where(t => t.AssignedTo.Contains(SearchByAssigned));
            }

            return View(tasks);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewModel addTaskRequest)
        {
            _logger.LogInformation("Add Method  Executed");
            var task = new  Models.Domain.Task()
            {
                TaskName = addTaskRequest.TaskName,
                AssignedTo = addTaskRequest.AssignedTo,
                StartDate = addTaskRequest.StartDate,
                EndDate = addTaskRequest.EndDate,
                Priorty = addTaskRequest.Priorty
            };
            await mvcDemoDbContext.Tasks.AddAsync(task);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            _logger.LogInformation("View Method  Executed", id);
            var task = await mvcDemoDbContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);

            if (task != null)
            { 

            var viewModel = new UpdateTaskViewModel()
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                AssignedTo = task.AssignedTo,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Priorty = task.Priorty

            };
                return await System.Threading.Tasks.Task.Run(() => View("View", viewModel));
                ////return await View(viewModel);
            }

            return RedirectToAction("Index");
        }

        

        [HttpPost]
        public async Task<IActionResult> View(UpdateTaskViewModel model)
        {
            _logger.LogInformation("View Update Method  Executed");
            var task = await mvcDemoDbContext.Tasks.FindAsync(model.TaskId);
            if (task != null)
            {
                task.TaskName = model.TaskName;
                task.AssignedTo =model.AssignedTo;
                task.StartDate =model.StartDate;
                task.EndDate =model.EndDate;
                task.Priorty = model.Priorty;

               await mvcDemoDbContext.SaveChangesAsync();
             
               return RedirectToAction("Index");      

            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateTaskViewModel model)
        {
            _logger.LogInformation("Delete Method  Executed");
            var task = await mvcDemoDbContext.Tasks.FindAsync(model.TaskId);

            if (task != null)
            {
                mvcDemoDbContext.Tasks.Remove(task);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }
    }
}
