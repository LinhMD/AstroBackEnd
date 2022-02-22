using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Services.Core;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Implement
{
    public class ZodiacService : IZodiacService, IDisposable
    {
        private readonly IUnitOfWork _work;

        public ZodiacService(IUnitOfWork _work)
        {
            this._work = _work; 
        }
        public Zodiac CreateZodiac(CreateZodiacRequest request)
        {
            Zodiac zodiac = new Zodiac()
            {
                Name = request.ZodiacName,
                ZodiacDayStart = request.ZodiacDayStart,
                ZodiacMonthStart = request.ZodiacMonthStart,
                ZodiacDayEnd = request.ZodiacDayEnd,
                ZodiacMonthEnd = request.ZodiacMonthEnd,
                Icon = request.ZodiacIcon,
                MainContent = request.ZodiacMainContent,
                Descreiption = request.ZodiacDescription
                
            };
            System.Console.WriteLine(zodiac.Name.ToString());
            return _work.Zodiacs.Add(zodiac);
        }

        public string RemoveZodiac(int id)
        {
            Zodiac zodiac = _work.Zodiacs.Get(id);
            _work.Zodiacs.Remove(zodiac);
            return zodiac.Name;
        }


        public Zodiac GetZodiac(int id)
        {
            return _work.Zodiacs.Get(id);
        }

        public Zodiac UpdateZodiac(int id, UpdateZodiacRequest updateZodiac)
        {
            var zodiac = _work.Zodiacs.Get(id);
            if (zodiac.Name != null)
            {
                zodiac.Name = updateZodiac.ZodiacName;
            }
            if (zodiac.ZodiacDayStart != 0)
            {
                zodiac.ZodiacDayStart = updateZodiac.ZodiacDayStart;
            }
            if (zodiac.ZodiacMonthStart != 0)
            {
                zodiac.ZodiacMonthStart = updateZodiac.ZodiacMonthStart;
            }
            if (zodiac.ZodiacDayEnd != 0)
            {
                zodiac.ZodiacDayEnd = updateZodiac.ZodiacDayEnd;
            }
            if (zodiac.ZodiacMonthEnd != 0)
            {
                zodiac.ZodiacMonthEnd = updateZodiac.ZodiacMonthEnd;
            }
            if (zodiac.Descreiption != null)
            {
                zodiac.Descreiption = updateZodiac.ZodiacDescription;
            }
            if (zodiac.Icon != null)
            {
                zodiac.Icon = updateZodiac.ZodiacIcon;
            }
            if(zodiac.MainContent != null)
            {
                zodiac.MainContent = updateZodiac.ZodiacMainContent;
            }

            return zodiac;
        }

        public IEnumerable<Zodiac> FindZodiac(FindZodiacRequest request)
        {
            Func<Zodiac, bool> filter = p =>
            {
                bool checkName = true;
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    checkName = p.Name.Contains(request.Name);
                }
                return checkName;
            };

            IEnumerable<Zodiac> result = null;

            if (request.PagingRequest != null)
            {
                switch (request.PagingRequest.SortBy)
                {
                    case "Name":
                        result = _work.Zodiacs.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    default:
                        result = null;
                        break;
                }
            }
            else
            {
                result = _work.Zodiacs.Find(filter, p => p.Name);
            }

            return result;
        }

        public IEnumerable<Zodiac> GetAllZodiac()
        {
            return _work.Zodiacs.GetAll<String>(p => p.Name);
        }

        public void Dispose()
        {
            this._work.Complete();
        }


    }
}
