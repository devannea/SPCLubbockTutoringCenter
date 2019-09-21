using Microsoft.EntityFrameworkCore;
using SPCLCTAPI.Core.Models;
using SPCLCTAPI.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPCLCTAPI.Infrastructure.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _dbContext;

        public ProfileRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Profile Add(Profile profile)
        {
            _dbContext.Profiles.Add(profile);
            _dbContext.SaveChanges();
            return profile;
        }

        public IEnumerable<Profile> GetAll()
        {
            return _dbContext.Profiles
               .Include(p => p.Timesheets)
               .Include(p => p.User)
               .ToList();
        }

        public Profile Get(int id)
        {
            return _dbContext.Profiles
                 .Include(p => p.Timesheets)
                 .Include(p => p.User)
                 .SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Profile> GetAllForUser(string userId)
        {
            return _dbContext.Profiles
                .Include(p => p.User)
                .Where(p => p.UserId == userId) // only for the given user
                .ToList();
        }

        public Profile Update(Profile updatedProfile)
        {
            var currentProfile = _dbContext.Profiles.Find(updatedProfile.Id);
            if (currentProfile == null) return null;
            _dbContext.Entry(currentProfile)
                .CurrentValues
                .SetValues(updatedProfile);
            _dbContext.Profiles.Update(currentProfile);
            _dbContext.SaveChanges();
            return currentProfile;
        }

        public void Remove(int id)
        {
            var deletedProfile = Get(id);
            _dbContext.Profiles.Remove(deletedProfile);
            _dbContext.SaveChanges();
        }
    }
}
