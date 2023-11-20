using Microsoft.AspNetCore.Identity;

namespace project_3_quiz_api.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
