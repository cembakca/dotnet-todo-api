namespace Todo.WebApi.Models
{
    public class Todo : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public short Done { get; set; }

        public virtual Users User { get; set; }
    }
}