using System.Collections.Generic;

namespace Todo.WebApi.Models
{
    public class Users : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual List<Todo> Todos { get; set; }
    }
}