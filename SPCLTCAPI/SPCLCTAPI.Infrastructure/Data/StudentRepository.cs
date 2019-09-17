using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Infrastructure.Data
{
    class StudentRepository : Repository<Student, int>
    {
        public StudentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
