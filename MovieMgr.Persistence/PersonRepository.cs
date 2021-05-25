using System.Collections.Generic;
using System.Linq;
using MovieMgr.Core.Contracts;
using MovieMgr.Core.Entities;

namespace MovieMgr.Persistence
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}