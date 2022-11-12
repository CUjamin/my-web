using Microsoft.AspNetCore.Mvc;
using my_web.Model.Todo;
using my_web.Service.Todo;

namespace my_web.Controllers
{
    [Route("api/todo")]
    public class TodoItemController : Controller
    {
        private readonly ILogger<TodoItemController> _logger;

        public TodoItemController(ILogger<TodoItemController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        public IList<TodoItemModel> GetTodoList([FromServices] TodoService todoService)
        {
            return todoService.GetALl();
        }

        [HttpGet("item")]
        public string AddTodoList(
            string id,
            string name,
            [FromServices] TodoService todoService)
        {
            todoService.AddTodo(new TodoItemModel { Id = id, Name = name });
            return "11111";
        }
    }
}