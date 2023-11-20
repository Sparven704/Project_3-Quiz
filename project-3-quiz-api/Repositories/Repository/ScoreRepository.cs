using project_3_quiz_api.Data;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Repositories.Interfaces;

namespace project_3_quiz_api.Repositories.Repository
{
    public class ScoreRepository : RepositoryBase<ScoreModel>, IScoreRepository
    {
        public ScoreRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
