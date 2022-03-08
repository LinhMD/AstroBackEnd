using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HoroscopeRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class HoroscopeService : IHoroscopeService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public HoroscopeService(IUnitOfWork _work)
        {
            this._work = _work;
        }
        public Horoscope CreateHoroscope(CreateHoroscopeRequest request)
        {
            try
            {
                if (request.NumberLuck > 0)
                {
                    Horoscope horoscope = new Horoscope()
                    {
                        NumberLuck = request.NumberLuck,
                        ColorLuck = request.ColorLuck,
                        Love = request.Love,
                        Money = request.Money,
                        Work = request.Work,
                    };
                    return _work.Horoscopes.Add(horoscope);
                }
                else
                {
                    throw new Exception("Number lukcy must be than zero");
                }
            }catch (Exception ex)
            {
                throw new ArgumentException("HoroscopeService : " + ex.Message);
            }
        }

        public Horoscope DeleteHoroscope(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Horoscope horoscope = _work.Horoscopes.Get(id);
            if (horoscope != null)
            {
                _work.Horoscopes.Remove(horoscope);
                return horoscope;
            } else { throw new ArgumentException("This Horoscope not found"); }
        }

        public IEnumerable<Horoscope> FindHoroscope(FindHoroscopeRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<Horoscope, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkColorLuck = true;
                    bool checkNumberLuck = true;
                    bool checkWork = true;
                    bool checkLove = true;
                    bool checkMoney = true;
                    if (request.Id != 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (!string.IsNullOrWhiteSpace(request.ColorLuck))
                    {
                        if (!string.IsNullOrWhiteSpace(p.ColorLuck))
                        {
                            checkColorLuck = p.ColorLuck.ToLower().Contains(request.ColorLuck.ToLower());
                        }
                        else
                        {
                            checkColorLuck = false;
                        }
                    }
                    if (request.NumberLuck > 0)
                    {
                        checkNumberLuck = p.NumberLuck == request.NumberLuck;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Work))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Work))
                        {
                            checkWork = p.Work.ToLower().Contains(request.Work.ToLower());
                        }
                        else
                        {
                            checkWork = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Love))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Love))
                        {
                            checkLove = p.Love.ToLower().Contains(request.Love.ToLower());
                        }
                        else
                        {
                            checkLove = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Money))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Money))
                        {
                            checkMoney = p.Money.ToLower().Contains(request.Money.ToLower());
                        }
                        else
                        {
                            checkMoney = false;
                        }
                    }
                    return checkId && checkColorLuck && checkNumberLuck && checkWork && checkLove && checkMoney;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "NumberLuck":
                            return _work.Horoscopes.FindPaging(filter, p => p.NumberLuck, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "ColorLuck":
                            return _work.Horoscopes.FindPaging(filter, p => p.ColorLuck, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "Money":
                            return _work.Horoscopes.FindPaging(filter, p => p.Money, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.Horoscopes.FindPaging(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<Horoscope> result = _work.Horoscopes.Find(filter, p => p.Id);
                    total = result.Count();
                    return result;
                }
            } catch (Exception ex) { throw new ArgumentException("HoroscopeService : " + ex.Message); }
        }

        public Horoscope GetHoroscope(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Horoscope horoscope = _work.Horoscopes.Get(id);
            if (horoscope != null)
            {
                return horoscope;
            }
            else { throw new ArgumentException("This Horoscope not found"); }  
        }

        public Horoscope UpdateHoroscope(int id, UpdateHoroscopeRequest request)
        {

            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Horoscope horoscope = _work.Horoscopes.Get(id);
            if (horoscope != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Love))
                {
                    horoscope.Love = request.Love;
                }
                if (!string.IsNullOrWhiteSpace(request.Money))
                {
                    horoscope.Money = request.Money;
                }
                if (request.NumberLuck > 0)
                {
                    horoscope.NumberLuck = request.NumberLuck;
                }
                if (!string.IsNullOrWhiteSpace(request.ColorLuck))
                {
                    horoscope.ColorLuck = request.ColorLuck;
                }
                return horoscope;
            }
            else
            {
                throw new ArgumentException("This Horoscope not found");
            }
        }
        
        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
