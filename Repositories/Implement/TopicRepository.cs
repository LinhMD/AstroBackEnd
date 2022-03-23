using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(AstroDataContext dataContext) : base(dataContext)
        {
        }

        private AstroDataContext AstroDataContext { get { return base._context as AstroDataContext; } }

        public override IQueryable<Topic> WithAllData()
        {
            return AstroDataContext.Topics.AsQueryable();
        }
    }
}
