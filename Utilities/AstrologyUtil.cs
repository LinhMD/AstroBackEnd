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
            double armc = 15 * (date.Hour + (date.Minute / 60D) + (date.Second / 3600D));
            int angleOfSet = 0;
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

                double houseOfPlanet = _swiss.swe_house_pos(armc, latitude, 23.437404, 'A', planetPos, ref error);

                houseOfPlanet = (houseOfPlanet + houseOffSet) % 12D;

                if (planet == SwissEph.SE_SUN)
                {
                    diff = houseOfPlanet - xx[0] / 30;
                    diff = (diff + 12) % 12;
                    angleOfSet = (int)Math.Round(diff);
                    natalChartView.zodiac = this._work.Zodiacs.Find(z => z.MainHouse == (int)Math.Ceiling(xx[0] / 30), z => z.MainHouse).FirstOrDefault();
                }

                string planetName = _swiss.swe_get_planet_name(planet);

                natalChartView.PlanetInHouse[planetName] = (int)Math.Round(houseOfPlanet);

                Models.Zodiac z = _work.Zodiacs.Find(z => z.MainHouse == (int)Math.Ceiling((houseOfPlanet - diff + 12) % 12), z => z.MainHouse).FirstOrDefault();
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
    }
}
