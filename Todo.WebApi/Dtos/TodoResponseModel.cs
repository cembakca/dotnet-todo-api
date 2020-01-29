namespace Todo.WebApi.Dtos
{
    public class TodoResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public short Done { get; set; }
    }
}