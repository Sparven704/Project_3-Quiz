using project_3_quiz_api.Data;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Repositories.Interfaces;

namespace project_3_quiz_api.Repositories.Repository
{
    public class UserRepository : RepositoryBase<UserModel>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
