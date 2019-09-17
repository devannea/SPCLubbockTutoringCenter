using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public Profile Add(Profile newProfile)
        {
            return _profileRepository.Add(newProfile);
        }

        public Profile Get(int id)
        {
            return _profileRepository.Get(id);
        }

        public IEnumerable<Profile> GetAll()
        {
            return _profileRepository.GetAll();
        }

        public Profile Update(Profile updatedProfile)
        {
            return _profileRepository.Update(updatedProfile);
        }

        public void Remove(int id)
        {
            _profileRepository.Remove(id);
        }
    }
}
