using Microsoft.AspNetCore.Mvc;

namespace TodoApi
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        // .. FIELDS

        private readonly TodoService todoService;

        // .. CONSTRUCTION

        public TodoController(TodoService todoService)
        {
            this.todoService = todoService;
        }

        // .. PUBLIC

        // GET: api/TodoItems
        // [HttpGet]
        // public IEnumerable<TodoItem> GetTodoItems()
        // {
        //     var items = new List<TodoItem>
        //     {
        //         new TodoItem
        //         {
        //             Id = 1,
        //             IsComplete = false,
        //             Name = "This is my name"
        //         }
        //     };

        //     return items;
        // }

        [HttpGet]
        public async Task<List<TodoItem>> Get()
        {
            return await this.todoService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TodoItem>> Get(int id)
        {
            var TodoItem = await this.todoService.GetAsync(id);

            if (TodoItem is null)
            {
                return NotFound();
            }

            return TodoItem;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TodoItem newBook)
        {
            await this.todoService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(int id, TodoItem updatedBook)
        {
            var TodoItem = await this.todoService.GetAsync(id);

            if (TodoItem is null)
            {
                return NotFound();
            }

            updatedBook.Id = TodoItem.Id;

            await this.todoService.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var TodoItem = await this.todoService.GetAsync(id);

            if (TodoItem is null)
            {
                return NotFound();
            }

            await this.todoService.RemoveAsync(id);

            return NoContent();
        }
    }
}