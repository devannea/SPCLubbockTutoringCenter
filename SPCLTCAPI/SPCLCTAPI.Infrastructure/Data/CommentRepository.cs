using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Infrastructure.Data
{
    class CommentRepository : Repository<Comment, int>
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
