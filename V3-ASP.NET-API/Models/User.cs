namespace SecureVaultAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public List<Password> Passwords { get; set; } = new();
        public List<Note> Notes { get; set; } = new();
    }
}
