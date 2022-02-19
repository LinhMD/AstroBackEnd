﻿using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Implement
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public ProfileService(IUnitOfWork work, AstroDataContext astroData) 
        {
            this._work = work;
            this._astroData = astroData;
        }

        public Profile CreateProfile(CreateProfileRequest request)
        {
            var profile = new Profile()
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
                BirthPlace = request.BirthPlace,
                ProfilePhoto = request.ProfilePhoto
            };

            var user = _work.Users.GetAllUserData(request.UserId);

            user.Profiles.Add(profile);

            _work.Profiles.Add(profile);

            return profile;
        }

        public void DeleteProfile(int id)
        {
            _work.Profiles.Remove(_work.Profiles.Get(id));
        }

        public IEnumerable<Profile> FindProfile(FindProfileRequest request)
        {
            Func<Profile, bool> filter = p =>
            {
                bool checkName = true;
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    checkName = p.Name.Contains(request.Name);
                }

                bool checkBirthPlace = true;
                if (!string.IsNullOrWhiteSpace(request.BirthPlace))
                {
                    checkBirthPlace = p.BirthPlace.Contains(request.BirthPlace);
                }

                bool checkZodiac = request.ZodiacId == null ? true : p.Zodiac.Id == request.ZodiacId;

                bool checkBirthDate = true;
                if(request.BirthDateEnd != null && request.BirthDateStart != null)
                {
                    checkBirthDate = p.BirthDate <= request.BirthDateEnd && p.BirthDate >= request.BirthDateStart;
                }
                return checkName && checkBirthDate && checkZodiac && checkBirthPlace;
            };

            IEnumerable<Profile> result = null;

            if (request.PagingRequest != null)
            {
                switch (request.PagingRequest.SortBy)
                {
                    case "Name":
                        result = _work.Profiles.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "BirthDate":
                        result = _work.Profiles.FindPaging(filter, p => p.BirthDate, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "BirthPlace":
                        result = _work.Profiles.FindPaging(filter, p => p.BirthPlace, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    default:
                        result = _work.Profiles.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                }
            }
            else
            {
                result = _work.Profiles.Find(filter, p => p.Name);
            }
                
            return result;
        }

        public IEnumerable<Profile> GetAllProfile()
        {
            
            return _work.Profiles.GetAll(p => p.Name); 
        }

        public Profile GetProfile(int id)
        {
            return _work.Profiles.Get(id);
        }

        public Profile UpdateProfile(int id, CreateProfileRequest request)
        {
            var profile = _work.Profiles.Get(id);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                profile.Name = request.Name;
            }
            if (!string.IsNullOrWhiteSpace(request.ProfilePhoto))
            {
                profile.ProfilePhoto = request.ProfilePhoto;
            }
            if (!string.IsNullOrWhiteSpace(request.BirthPlace))
            {
                profile.BirthPlace = request.BirthPlace;
            }
            if(request.BirthDate != profile.BirthDate)
            {
                profile.BirthDate = request.BirthDate;
            }
            return profile;
        }
    }
}