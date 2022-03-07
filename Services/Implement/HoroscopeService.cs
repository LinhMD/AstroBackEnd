using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HoroscopeRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

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

        public IEnumerable<Horoscope> FindHoroscope(FindHoroscopeRequest request)
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
                            checkColorLuck = p.ColorLuck.Contains(request.ColorLuck);
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
                            checkWork = p.Work.Contains(request.Work);
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
                            checkLove = p.Love.Contains(request.Love);
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
                            checkMoney = p.Money.Contains(request.Money);
                        }
                        else
                        {
                            checkMoney = false;
                        }

                    }
                    return checkId && checkColorLuck && checkNumberLuck && checkWork && checkLove && checkMoney;
                };
                PagingRequest paging = request.PagingRequest;
                Validation.ValidNumberThanZero(paging.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(paging.PageSize, "PageSize must be than zero");
                if (paging == null || paging.SortBy == null)
                {
                    return _work.Horoscopes.Find(filter, p => p.Id);
                }
                else
                {
                    switch (paging.SortBy)
                    {
                        case "NumberLuck":
                            return _work.Horoscopes.FindPaging(filter, p => p.NumberLuck, paging.Page, paging.PageSize);
                        case "ColorLuck":
                            return _work.Horoscopes.FindPaging(filter, p => p.ColorLuck, paging.Page, paging.PageSize);
                        case "Money":
                            return _work.Horoscopes.FindPaging(filter, p => p.Money, paging.Page, paging.PageSize);
                        default:
                            return _work.Horoscopes.FindPaging(filter, p => p.Id, paging.Page, paging.PageSize);
                    }
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
