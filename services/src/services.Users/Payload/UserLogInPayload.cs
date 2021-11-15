namespace services.Users.Payload
{
    public class UserLogInPayload
    {
        public string EmailAddressOrUserName { get; set; }
        public string Password { get; set; }
    }
}