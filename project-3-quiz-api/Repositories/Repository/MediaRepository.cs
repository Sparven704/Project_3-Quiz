using project_3_quiz_api.Data;
using project_3_quiz_api.Models.DBModels;
using project_3_quiz_api.Repositories.Interfaces;

namespace project_3_quiz_api.Repositories.Repository
{
    public class MediaRepository : RepositoryBase<MediaModel>, IMediaRepository
    {
        public MediaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
