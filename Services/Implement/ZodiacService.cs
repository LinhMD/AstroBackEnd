using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class ZodiacService : IZodiacService, IDisposable
    {
        private readonly IUnitOfWork _work;

        public ZodiacService(IUnitOfWork _work)
        {
            this._work = _work; 
        }

        public Zodiac GetZodiac(int id)
        {
           Validation.ValidNumberThanZero(id, "Id must be than zero");
            var zodiac = _work.Zodiacs.Get(id);
            if(zodiac != null)
            {
                return zodiac;
            }
            else { throw new ArgumentException("This Zodiac not found"); }
        }
        public Zodiac CreateZodiac(CreateZodiacRequest request)
        {
            Validation.ValidDayMonth(request.ZodiacDayStart, request.ZodiacMonthStart);
            Validation.ValidDayMonth(request.ZodiacDayEnd, request.ZodiacMonthEnd);
            try
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
                return _work.Zodiacs.Add(zodiac);
            }catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public Zodiac RemoveZodiac(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            var zodiac = _work.Zodiacs.Get(id);
            if (zodiac != null)
            {
                _work.Zodiacs.Remove(zodiac);
                return zodiac;
            }
            else { throw new ArgumentException("This Zodiac not found"); }    
        }

        public Zodiac UpdateZodiac(int id, UpdateZodiacRequest updateZodiac)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Zodiac zodiac = _work.Zodiacs.Get(id);
            if(zodiac != null)
            {
                if (updateZodiac.ZodiacName != null)
                {
                    zodiac.Name = updateZodiac.ZodiacName;
                }
                if (updateZodiac.ZodiacDayStart > 0)
                {
                    zodiac.ZodiacDayStart = updateZodiac.ZodiacDayStart;
                }
                if (updateZodiac.ZodiacMonthStart > 0)
                {
                    zodiac.ZodiacMonthStart = updateZodiac.ZodiacMonthStart;
                }
                if (updateZodiac.ZodiacDayEnd > 0)
                {
                    zodiac.ZodiacDayEnd = updateZodiac.ZodiacDayEnd;
                }
                if (updateZodiac.ZodiacMonthEnd > 0)
                {
                    zodiac.ZodiacMonthEnd = updateZodiac.ZodiacMonthEnd;
                }
                if (updateZodiac.ZodiacDescription != null)
                {
                    zodiac.Descreiption = updateZodiac.ZodiacDescription;
                }
                if (updateZodiac.ZodiacIcon != null)
                {
                    zodiac.Icon = updateZodiac.ZodiacIcon;
                }
                if (updateZodiac.ZodiacMainContent != null)
                {
                    zodiac.MainContent = updateZodiac.ZodiacMainContent;
                }
                Validation.ValidDayMonth(zodiac.ZodiacDayStart, zodiac.ZodiacMonthStart);
                Validation.ValidDayMonth(zodiac.ZodiacDayEnd, zodiac.ZodiacMonthEnd);
                return zodiac;
            }
            else
            {
                throw new ArgumentException("This Zodiac not found");
            }
        }

        public IEnumerable<Zodiac> FindZodiac(FindZodiacRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<Zodiac, bool> filter = p =>
                {
                    bool checkName = true;
                    bool checkId = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Name))
                        {
                            checkName = p.Name.ToLower().Contains(request.Name.ToLower());
                        }
                        else
                        {
                            checkName = false;
                        }

                    }
                    return checkName && checkId;
                };
                Validation.ValidNumberThanZero(request.PagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(request.PagingRequest.PageSize, "PageSize must be than zero");
                if (request.PagingRequest != null)
                {
                    switch (request.PagingRequest.SortBy)
                    {
                        case "Name":
                            return _work.Zodiacs.FindPaging(filter, p => p.Name, out total, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        default:
                            return _work.Zodiacs.FindPaging(filter, p => p.MainHouse, out total, request.PagingRequest.Page, request.PagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<Zodiac> result = _work.Zodiacs.Find(filter, p => p.Id);
                    total = result.Count();
                    return result;
                }
            }
            catch(Exception ex) { throw new ArgumentException("ZodiacService : " + ex.Message); }
        }

        public void Dispose()
        {
            this._work.Complete();
        }


    }
}
