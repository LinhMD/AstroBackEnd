using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.ViewsModel;
using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Utilities
{
    public class AstrologyUtil : IDisposable
    {

        public static Dictionary<string, int> PLANET_ID = new Dictionary<string, int>() //bad design but this is PRM391 not SWD391
        {
            {"Sun", 1},
            {"Moon", 2},
            {"Mercury", 3},
            {"Venus", 4},
            {"Mars", 5},
            {"Jupiter", 6},
            {"Saturn", 7},
            {"Uranus", 8},
            {"Neptune", 9},
            {"Pluto", 10},
            {"Earth", 11}
        };
        
        private readonly SwissEph _swiss;

        private readonly IUnitOfWork _work;
        public AstrologyUtil(IUnitOfWork work)
        {
            _swiss = new SwissEph();
            _swiss.swe_set_ephe_path(null);
            _work = work;
            
        }

        public void Dispose()
        {
            _swiss.swe_close();
        }

        public NatalChartView GetHousePosOfPlanets(DateTime date, double longtitude, double latitude)
        {
            NatalChartView natalChartView = new NatalChartView() {
                ZodiacInHouse = new Dictionary<string, int>(),
                PlanetInZodiac = new Dictionary<string, string>(),
                PlanetInHouse = new Dictionary<string, int>(),
                zodiac = null
            };
            string error = "";
            double[] planetPos = new double[2];

            double[] xx = new double[6]; //6 position values: longitude, latitude, distance, *long.speed, lat.speed, dist.speed 

            /*double birthPlace = 10.8231; // viet nam latitude
            double longtitude = 106.6297;*/

            double houseOffSet = longtitude / 30;

            double julianDay = _swiss.swe_julday(date.Year, date.Month, date.Day, date.Hour, SwissEph.SE_GREG_CAL);
            long iflgret;
            double diff = 0d; //the diffirent of zodiac and house
            double[] ascmc = new double[10];
            double[] cusps = new double[13];

            _swiss.swe_houses(julianDay, latitude, longtitude, 'A', cusps, ascmc);

            for (int planet = SwissEph.SE_SUN; planet <= SwissEph.SE_EARTH; planet++)
            {
                /*if (planet == SwissEph.SE_EARTH) continue;*/

                iflgret = _swiss.swe_calc_ut(julianDay, planet, SwissEph.SEFLG_SPEED, xx, ref error);

                if (iflgret < 0)
                {
                    Console.WriteLine(error);
                }

                planetPos[0] = xx[0];
                planetPos[1] = xx[1];

                double houseOfPlanet = _swiss.swe_house_pos(ascmc[2], latitude, 23.437404, 'A', planetPos, ref error);

                houseOfPlanet = (houseOfPlanet + houseOffSet) % 12D;

                if (planet == SwissEph.SE_SUN)
                {
                    diff = houseOfPlanet - xx[0] / 30;
                    diff = (diff + 12) % 12;
                    natalChartView.zodiac = this._work.Zodiacs.Find(z => z.MainHouse == (int)Math.Ceiling(xx[0] / 30), z => z.MainHouse).FirstOrDefault();
                }

                string planetName = _swiss.swe_get_planet_name(planet);

                natalChartView.PlanetInHouse[planetName] = (int)Math.Round(houseOfPlanet);
                int zodiacHouse = (int)Math.Ceiling((houseOfPlanet - diff + 12) % 12);
                if (zodiacHouse == 0) zodiacHouse = 12;
                Models.Zodiac z = _work.Zodiacs.Find(z => z.MainHouse == zodiacHouse, z => z.MainHouse).FirstOrDefault();
                if(planet != SwissEph.SE_EARTH)
                {
                    natalChartView.PlanetInZodiac[planetName] = z.Name;
                }
            }

            
            List<Models.Zodiac> zodiacs = _work.Zodiacs.GetAll(z => z.MainHouse).ToList();

            foreach (var zodiac in zodiacs)
            {
                int house = (zodiac.MainHouse - (int)Math.Ceiling(diff) + 12) % 12;
                house++;
                natalChartView.ZodiacInHouse[zodiac.Name] = house ;
            }

            return natalChartView;
        }

        public Zodiac GetZodiac(DateTime birthDate, double longtitude, double latitude)
        {
            double[] planetPos = new double[2];

            double[] xx = new double[6]; //6 position values: longitude, latitude, distance, *long.speed, lat.speed, dist.speed 

            /*double birthPlace = 10.8231; // viet nam latitude
            double longtitude = 106.6297;*/

            double julianDay = _swiss.swe_julday(birthDate.Year, birthDate.Month, birthDate.Day, birthDate.Hour, SwissEph.SE_GREG_CAL);
            long iflgret;
            double[] ascmc = new double[10];
            double[] cusps = new double[13];

            _swiss.swe_houses(julianDay, latitude, longtitude, 'A', cusps, ascmc);
            string error = "";

            iflgret = _swiss.swe_calc_ut(julianDay, SwissEph.SE_SUN, SwissEph.SEFLG_SPEED, xx, ref error);

            if (iflgret < 0)
            {
                Console.WriteLine(error);
            }

            planetPos[0] = xx[0];
            planetPos[1] = xx[1];
            double houseOfPlanet = _swiss.swe_house_pos(ascmc[2], latitude, 23.437404, 'A', planetPos, ref error);
           
            Zodiac zodiac = this._work.Zodiacs.Find(z => z.MainHouse == (int)Math.Ceiling(xx[0] / 30), z => z.MainHouse).FirstOrDefault();
            return zodiac;
        }

        public Dictionary<string, PlanetPosition> GetHouseSnapshot(DateTime birthDate, double longtitude, double latitude)
        {
            Dictionary<int, House> houseDic = new Dictionary<int, House>();

            IEnumerable<House> houses = _work.Houses.GetAll(h => h.Id);

            foreach (House house in houses)
            {
                int h = int.Parse(house.Tag);
                houseDic[h] = house;
            }

            Dictionary<int, Zodiac> zodiacDic = new Dictionary<int, Zodiac>();

            IEnumerable<Zodiac> zodiacs = _work.Zodiacs.GetAll(z => z.MainHouse);

            foreach (var zodiac in zodiacs)
            {
                zodiacDic[zodiac.MainHouse] = zodiac;
            }

            Dictionary<string, PlanetPosition> planetDic = new Dictionary<string, PlanetPosition>();



            string error = "";
            double[] planetPos = new double[2];

            double[] xx = new double[6]; //6 position values: longitude, latitude, distance, *long.speed, lat.speed, dist.speed 

            /*double birthPlace = 10.8231; // viet nam latitude
            double longtitude = 106.6297;*/

            double houseOffSet = longtitude / 30;

            double julianDay = _swiss.swe_julday(birthDate.Year, birthDate.Month, birthDate.Day, birthDate.Hour, SwissEph.SE_GREG_CAL);
            long iflgret;
            double diff = 0d; //the diffirent of zodiac and house
            double[] ascmc = new double[10];
            double[] cusps = new double[13];

            _swiss.swe_houses(julianDay, latitude, longtitude, 'A', cusps, ascmc);

            for (int planet = SwissEph.SE_SUN; planet <= SwissEph.SE_PLUTO; planet++)
            {

                PlanetPosition planetPosition = new PlanetPosition();
                iflgret = _swiss.swe_calc_ut(julianDay, planet, SwissEph.SEFLG_SPEED, xx, ref error);

                if (iflgret < 0)
                {
                    Console.WriteLine(error);
                }

                planetPos[0] = xx[0];
                planetPos[1] = xx[1];

                double houseOfPlanet = _swiss.swe_house_pos(ascmc[2], latitude, 23.437404, 'A', planetPos, ref error);

                houseOfPlanet = (houseOfPlanet + houseOffSet) % 12D;

                if (planet == SwissEph.SE_SUN)
                {
                    diff = houseOfPlanet - xx[0] / 30;
                    diff = (diff + 12) % 12;
                    Zodiac zodiac = zodiacDic[(int)Math.Ceiling(xx[0] / 30)];
                }


                string planetName = _swiss.swe_get_planet_name(planet);
                int zodiacHouse = (int)Math.Ceiling((houseOfPlanet - diff + 12) % 12);
                if (zodiacHouse == 0) zodiacHouse = 12;

                House house = houseDic[zodiacHouse];


                if(house != null)
                {
                    planetPosition.HouseId = house.Id;
                    planetPosition.HouseName = house.Name;
                }

                int zId = (int)Math.Ceiling((houseOfPlanet - diff + 12) % 12);
                Models.Zodiac z = zodiacDic[zId];

                if (z != null)
                {
                    planetPosition.ZodiacName = z.Name;

                    planetPosition.ZodiacId = z.Id;
                }

                planetDic[planetName] = planetPosition;
            }

            return planetDic;
        }
    }
}
