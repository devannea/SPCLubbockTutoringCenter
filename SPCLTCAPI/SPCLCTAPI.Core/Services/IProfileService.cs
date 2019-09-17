using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Services
{
    public interface IProfileService
    {
        Profile Add(Profile newProfile);
        Profile Get(int id);
        IEnumerable<Profile> GetAll();
        Profile Update(Profile updatedProfile);
        void Remove(int id);
    }
}
