using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.WebApi.Dtos;
using Todo.WebApi.Models;

namespace Todo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private TodoDbContext _context;

        public TodoController(TodoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Models.Todo> todos = _context.Todo.ToList();

            List<TodoResponseModel> viewModel = todos.ToTodoResponseModel();
            
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Models.Todo todo = _context.Todo.FirstOrDefault(p => p.Id == id);
            
            if (todo == null)
            {
                return NotFound();
            }
            
            TodoResponseModel viewModel = todo.ToTodoResponseModel();
            return Ok(viewModel);
        }
        
        [HttpGet("user/{userId}/todos")]
        public IActionResult GetUserTodos(int userId)
        {
            var todos = (from t in _context.Todo
                join u in _context.Users on t.UserId equals u.Id
                where t.UserId == userId
                select new TodoUserResponseModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Body = t.Body,
                    Done = t.Done,
                    Users = new UserResponseModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Email = u.Email
                    }
                });
            
            if (!todos.Any())
            {
                return NotFound();
            }
            
            return Ok(todos);
        }

        [HttpPost]
        public IActionResult Add(Models.Todo todo)
        {
            _context.Todo.Add(todo);
            _context.SaveChanges();
            return Ok("Success!");
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Todo todo)
        {
            
            Models.Todo selectedTodo =  _context.Todo.FirstOrDefault(p => p.Id == id);
            if (selectedTodo == null)
            {
                return NotFound();
            }

            _context.Todo.Update(todo);
            _context.SaveChanges();
            return Ok(todo.ToTodoResponseModel());
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            Models.Todo selectedTodo =  _context.Todo.FirstOrDefault(p => p.Id == id);
            if (selectedTodo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(selectedTodo);
            _context.SaveChanges();
            return Ok("Deleted Todo.");
        }
    }
}