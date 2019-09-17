using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPCLTCAPI.ApiModels
{
    public static class ProfileMappingExtensions
    {
        public static ProfileModel ToApiModel(this Profile profile)
        {
            return new ProfileModel
            {
                Id = profile.Id,
                Name = profile.Name
            };
        }

        public static Profile ToDomainModel(this ProfileModel profileModel)
        {
            return new Profile
            {
                Id = profileModel.Id,
                Name = profileModel.Name
            };
        }

        public static IEnumerable<ProfileModel> ToApiModels(this IEnumerable<Profile> Users)
        {
            return Users.Select(u => u.ToApiModel());
        }

        public static IEnumerable<Profile> ToDomainModels(this IEnumerable<ProfileModel> UserModels)
        {
            return UserModels.Select(u => u.ToDomainModel());
        }
    }
}
