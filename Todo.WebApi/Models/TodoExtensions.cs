using System.Collections.Generic;
using Todo.WebApi.Dtos;

namespace Todo.WebApi.Models
{
    public static class TodoExtensions
    {
        public static List<TodoResponseModel> ToTodoResponseModel(this List<Todo> todos)
        {
            List<TodoResponseModel> model = new List<TodoResponseModel>();
            foreach (var todo in todos)
            {
                model.Add(new TodoResponseModel()
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Body = todo.Body,
                    Done = todo.Done
                });
            }

            return model;
        }
        
        public static TodoResponseModel ToTodoResponseModel(this Todo todo)
        {
            return new TodoResponseModel()
            {
                Id = todo.Id,
                Title = todo.Title,
                Body = todo.Body,
                Done = todo.Done
            };
        }
        
    }
}