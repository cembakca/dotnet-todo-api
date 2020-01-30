namespace Todo.WebApi.Dtos
{
    public class TokenResponseModel
    {
        public string Token { get; set; }
        public UserResponseModel User { get; set; }
    }
}