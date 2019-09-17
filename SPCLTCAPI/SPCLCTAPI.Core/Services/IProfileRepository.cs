using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Services
{
    public interface IProfileRepository
    {
        Profile Add(Profile profile);
        Profile Update(Profile profile);
        Profile Get(int id);
        IEnumerable<Profile> GetAll();
        void Remove(int id);
    }
}
