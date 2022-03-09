using AstroBackEnd.Models;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.ProfileRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Core
{
    public interface IProfileService
    {
        public Profile GetProfile(int id);

        public IEnumerable<Profile> FindProfile(FindProfileRequest request);

        public IEnumerable<Profile> FindProfile(FindProfileRequest request, out int total);

        public IEnumerable<Profile> GetAllProfile();

        public void DeleteProfile(int id);

        public Profile CreateProfile(CreateProfileRequest request);

        public Profile UpdateProfile(int id, UpdateProfileRequest request);
    }
}
