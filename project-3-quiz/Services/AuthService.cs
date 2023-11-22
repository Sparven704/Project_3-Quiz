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
            
            return token;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("JwtToken");
        }
    }
}
