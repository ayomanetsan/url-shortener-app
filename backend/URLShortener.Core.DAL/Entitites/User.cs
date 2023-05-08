namespace URLShortener.Core.DAL.Entitites
{
    public class User
    {
        public uint Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public bool IsAdmin { get; set; }
    }
}
