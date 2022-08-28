namespace AdminProjectsDemo.DTOs.Accounts.Response
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
