namespace MailMe.Backend.Carriers.Requests.Users
{
    public class UpdateUserRequestDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}