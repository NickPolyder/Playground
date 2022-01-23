namespace Playground.NETCORE.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public static User Create(string userName, string email)
        {
            return new User
            {
                Username = userName,
                Email = email
            };
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id},\n" +
                   $"{nameof(Username)}: {Username}\n" +
                   $"{nameof(Email)}: {Email}\n";
        }
    }
}