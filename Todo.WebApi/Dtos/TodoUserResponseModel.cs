using Todo.WebApi.Models;

namespace Todo.WebApi.Dtos
{
    public class TodoUserResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public short Done { get; set; }
        public UserResponseModel Users { get; set; }
    }
}