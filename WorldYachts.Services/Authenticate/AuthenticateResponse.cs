namespace WorldYachts.Services.Authenticate
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Data.Entities.User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            Token = token;
        }
    }
}
