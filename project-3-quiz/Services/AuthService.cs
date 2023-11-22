using Blazored.LocalStorage;


namespace project_3_quiz.Services
{
    public class AuthService
    {
        private readonly ILocalStorageService _localStorageService;

        public AuthService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<bool> IsUserLoggedIn()
        {
            var token = await _localStorageService.GetItemAsync<string>("JwtToken");
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string> GetUsername()
        {
            var token = await _localStorageService.GetItemAsync<string>("JwtToken");
            // Decode the token to get user information (based on your implementation)
            // Example: var user = DecodeToken(token);
            // return user?.Username;

            // For simplicity, let's assume the token itself contains the username
            return token;
        }

        public async Task Logout()
        {
            // Implement logout logic, such as removing the JWT token from local storage
            await _localStorageService.RemoveItemAsync("JwtToken");
        }
    }
}
