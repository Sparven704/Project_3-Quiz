using project_3_quiz_api.Data;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Repositories.Interfaces;

namespace project_3_quiz_api.Repositories.Repository
{
    public class QuestionRepository : RepositoryBase<QuestionModel>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
