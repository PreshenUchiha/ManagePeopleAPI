namespace HttpClientLibrary.Model
{
    public class AccessTokenModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string RequestUrl { get; set; } = string.Empty;
        public string GrantType { get; set; } = "password";
    }
}
