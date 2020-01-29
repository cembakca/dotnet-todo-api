using Todo.WebApi.Models;

namespace Todo.WebApi.Dtos
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}