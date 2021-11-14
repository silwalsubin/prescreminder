namespace services.Users.Payload
{
    public class UserLogInPayload
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    }
}