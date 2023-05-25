namespace Books.BlazorWasm.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public string UserName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public List<string> Roles { get; set; } = new();
    }
}
