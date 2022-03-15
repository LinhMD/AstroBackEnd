using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace AstroBackEnd.Repositories.Implement
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(AstroDataContext dataContext) : base(dataContext)
        {
        }

        private AstroDataContext AstroDataContext { get { return base._context as AstroDataContext; } }
    }
}
