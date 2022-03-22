using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.ProfileRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Implement
{
    public class ProfileService : IProfileService, IDisposable
    {
        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;

        private readonly IAstrologyService _astro;
        public ProfileService(IUnitOfWork work, AstroDataContext astroData, IAstrologyService astrology) 
        {
            this._work = work;
            this._astroData = astroData;
            this._astro = astrology;
        }

        public Profile CreateProfile(CreateProfileRequest request)
        {
            var profile = new Profile()
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
                BirthPlace = request.BirthPlace,
                ProfilePhoto = request.ProfilePhoto,
                Latitude = request.Latitude,
                Longitude = request.Longtitude,
                Gender = request.Gender
            };

            profile.Zodiac = _astro.GetZodiac(profile.BirthDate, profile.Longitude, profile.Latitude);

            var user = _work.Users.GetAllUserData(request.UserId);
            if (user == null) throw new ArgumentException("User ID not found");

            BirthChart birthChart = new BirthChart();

            birthChart.ImgLink = _astro.GetChartLinkFirebase(profile.BirthDate, profile.Longitude, profile.Latitude);

            birthChart.Content = JsonConvert.SerializeObject(_astro.GetPlanetPosition(profile.BirthDate, profile.Longitude, profile.Latitude));
            
            profile.UserId = user.Id;

            profile.BirthChart = birthChart;

            _work.Profiles.Add(profile);

            return profile;
        }

        public void DeleteProfile(int id)
        {
            _work.Profiles.Remove(GetProfile(id));
        }

        public void Dispose()
        {
            this._work.Complete();
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
                bool checkUserId = true;
                if(request.UserId != null )
                {
                    checkUserId = request.UserId == p.UserId;
                }
                return checkName && checkBirthDate && checkZodiac && checkBirthPlace && checkUserId;
            };

            IEnumerable<Profile> result = null;

            if (request.PagingRequest != null)
            {
                result = request.PagingRequest.SortBy switch
                {
                    "Name"          => _work.Profiles.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize),
                    "BirthDate"     => _work.Profiles.FindPaging(filter, p => p.BirthDate, request.PagingRequest.Page, request.PagingRequest.PageSize),
                    "BirthPlace"    => _work.Profiles.FindPaging(filter, p => p.BirthPlace, request.PagingRequest.Page, request.PagingRequest.PageSize),
                    _               => _work.Profiles.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize),
                };
            }
            else
            {
                result = _work.Profiles.Find(filter, p => p.Name);
            }
                
            return result;
        }

        public IEnumerable<Profile> FindProfile(FindProfileRequest request, out int total)
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
                if (request.BirthDateEnd != null && request.BirthDateStart != null)
                {
                    checkBirthDate = p.BirthDate <= request.BirthDateEnd && p.BirthDate >= request.BirthDateStart;
                }

                bool checkUserId = true;
                if (request.UserId != null)
                {
                    checkUserId = request.UserId == p.UserId;
                }
                return checkName && checkBirthDate && checkZodiac && checkBirthPlace && checkUserId;
            };

            IEnumerable<Profile> result = null;

            if (request.PagingRequest != null)
            {
                result = request.PagingRequest.SortBy switch
                {
                    "Name" => _work.Profiles.FindPaging(filter, p => p.Name, out total, request.PagingRequest.Page, request.PagingRequest.PageSize),
                    "BirthDate" => _work.Profiles.FindPaging(filter, p => p.BirthDate, out total, request.PagingRequest.Page, request.PagingRequest.PageSize),
                    "BirthPlace" => _work.Profiles.FindPaging(filter, p => p.BirthPlace, out total, request.PagingRequest.Page, request.PagingRequest.PageSize),
                    _ => _work.Profiles.FindPaging(filter, p => p.Name, out total, request.PagingRequest.Page, request.PagingRequest.PageSize),
                };
            }
            else
            {
                result = _work.Profiles.Find(filter, p => p.Name);
                total = result.Count();
            }

            return result;
        }

        public IEnumerable<Profile> GetAllProfile()
        {
            
            return _work.Profiles.GetAll(p => p.Name); 
        }

        public Profile GetProfile(int id)
        {
            Profile profile = _work.Profiles.GetProfileWithAllData(id);
            if (profile == null) throw new ArgumentException("Profile not found");
            return profile;
        }

        public BirthChartView GetBirthChart(int id) 
        {
            Profile profile = this.GetProfile(id);
            if (profile.BirthChart == null) throw new ArgumentException("Birth Chart Not found!!");

            var birthChart = new BirthChartView(profile.BirthChart);

            if(!string.IsNullOrWhiteSpace(profile.BirthChart.Content))
            {
                var planetPositions = JsonConvert.DeserializeObject<Dictionary<string, PlanetPositionView>>(profile.BirthChart.Content);
                foreach (var key in planetPositions.Keys)
                {
                    var planetPos = planetPositions[key];
                    PlanetHouse planetHouse = _work.PlanetHouses.FindAtDBPaging(p => p.PlanetId == planetPos.PlanetId && p.HouseId == planetPos.HouseId, p => p.Id, out int total).FirstOrDefault();
                    PlanetZodiac planetZodiac = _work.PlanetZodiacs.FindAtDBPaging(pz => pz.PlanetId == planetPos.PlanetId && pz.ZodiacId == planetPos.ZodiacId, pz => pz.Id, out total).FirstOrDefault();
                    Planet planet = _work.Planets.Get(planetPos.PlanetId);

                    try
                    {
                        var chartItem = new ChartItemView(planetZodiac, planetHouse, planet);
                        birthChart.Items[key] = chartItem;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }


            return birthChart;
        }
        public Profile UpdateProfile(int id, UpdateProfileRequest request)
        {
            var profile = GetProfile(id);

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
            if(request.Gender != null)
            {
                profile.Gender = request.Gender.Value;
            }
            bool checkZodiacChange = false;
            if (request.BirthDate != profile.BirthDate)
            {
                profile.BirthDate = (DateTime)request.BirthDate;
                checkZodiacChange = true;
            }

            if(request.Latitude != null)
            {
                profile.Latitude = (double)request.Latitude;
                checkZodiacChange = true;
            }

            if(request.Longtitude != null)
            {
                profile.Longitude = (double)request.Longtitude;
                checkZodiacChange = true;
            }

            if (checkZodiacChange)
            {
                profile.Zodiac = _astro.GetZodiac(profile.BirthDate, profile.Longitude, profile.Latitude);

                profile.BirthChart.ImgLink = _astro.GetChartLinkFirebase(profile.BirthDate, profile.Longitude, profile.Latitude);

                profile.BirthChart.Content = JsonConvert.SerializeObject(_astro.GetPlanetPosition(profile.BirthDate, profile.Longitude, profile.Latitude));

            }

            return profile;
        }
    }
}
