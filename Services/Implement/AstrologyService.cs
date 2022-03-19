using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Implement
{
    public class AstrologyService : IAstrologyService ,IDisposable
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

        public static (int X, int Y) center = (319, 317);

        public static double planetRadius = 220D;

        public static double pointRadius = 240D;

        public static int houseOutterRadius = 240;

        public static int houseInnerRadius = 160;



        private static readonly (int, int) BlueSpecialScope1 = (55, 65);
        private static readonly (int, int) BlueSpecialScope2 = (111, 129);

        private static readonly (int, int) RedSpecialScope1 = (0, 9);
        private static readonly (int, int) RedSpecialScope2 = (81, 99);
        private static readonly (int, int) RedSpecialScope3 = (171, 180);

        private static readonly (int, int) GraySpecialScope1 = (43, 48);
        private static readonly (int, int) GraySpecialScope2 = (133, 137);

        private static readonly Pen BluePen = new(Color.Blue, 1);
        private static readonly Pen RedPen = new(Color.Red, 1);
        private static readonly Pen GrayPen = new(Color.Gray, 1);

        private readonly SwissEph _swiss;

        private readonly IUnitOfWork _work;

        private readonly IFirebaseService _firebase;
        public AstrologyService(IUnitOfWork work, IFirebaseService firebase)
        {
            _swiss = new SwissEph();
            _swiss.swe_set_ephe_path(null);
            _work = work;
            _firebase = firebase;
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
                Zodiac = null
            };

            Dictionary<string, double> planetPosition = new Dictionary<string, double>();
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

                    natalChartView.Zodiac = this._work.Zodiacs.Find(z => z.MainHouse == (int)Math.Ceiling(xx[0] / 30), z => z.MainHouse).FirstOrDefault();
                }

                string planetName = _swiss.swe_get_planet_name(planet);

                planetPosition[planetName] = -xx[0] + 180;

                natalChartView.PlanetInHouse[planetName] = (int)Math.Round(houseOfPlanet);

                int zodiacHouse = (int)Math.Ceiling((houseOfPlanet - diff + 12) % 12);

                if (zodiacHouse == 0) zodiacHouse = 12;

                Models.Zodiac z = _work.Zodiacs.Find(z => z.MainHouse == zodiacHouse, z => z.MainHouse).FirstOrDefault();

                if(planet != SwissEph.SE_EARTH && z != null)
                {
                    natalChartView.PlanetInZodiac[planetName] = z.Name;
                }
            }

            
            List<Models.Zodiac> zodiacs = _work.Zodiacs.GetAll(z => z.MainHouse).ToList();

            foreach (var zodiac in zodiacs)
            {
                int house = (zodiac.MainHouse - (int)Math.Ceiling(diff) + 12) % 12;
                house++;
                natalChartView.ZodiacInHouse[zodiac.Name] = house;
            }
            natalChartView.PlanetPositon = planetPosition;
            natalChartView.Diff = diff * 30;
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

        public Dictionary<string, PlanetPositionView> GetPlanetPosition(DateTime birthDate, double longtitude, double latitude)
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

            Dictionary<string, Planet> planetDic = new Dictionary<string, Planet>();

            IEnumerable<Planet> planets = _work.Planets.GetAll(p => p.Id);

            Dictionary<string, PlanetPositionView> planetPositionDic = new Dictionary<string, PlanetPositionView>();

            foreach (var planet in planets)
            {
                planetDic[planet.Tag.Trim().ToLower()] = planet;
            }

            string error = "";
            double[] planetPos = new double[2];

            double[] xx = new double[6]; //6 position values: longitude, latitude, distance, *long.speed, lat.speed, dist.speed 

            /*double birthPlace = 10.8231; // viet nam latitude
            double longtitude = 106.6297;*/

            double houseOffSet = longtitude / 30;

            double julianDay = _swiss.swe_julday(birthDate.Year, birthDate.Month, birthDate.Day, birthDate.Hour, SwissEph.SE_GREG_CAL);
            
            double diff = 0d; //the diffirent of zodiac and house
            double[] ascmc = new double[10];
            double[] cusps = new double[13];

            _swiss.swe_houses(julianDay, latitude, longtitude, 'A', cusps, ascmc);

            _swiss.swe_calc_ut(julianDay, SwissEph.SE_ECL_NUT, 0, xx, ref error);
            double eps_true = xx[0];

            for (int planet = SwissEph.SE_SUN; planet <= SwissEph.SE_PLUTO; planet++)
            {

                _swiss.swe_calc_ut(julianDay, planet, SwissEph.SEFLG_SPEED, xx, ref error);
                string planetName = _swiss.swe_get_planet_name(planet);

                planetPos[0] = xx[0];
                planetPos[1] = xx[1];

                double houseOfPlanet = _swiss.swe_house_pos(ascmc[2], latitude, eps_true, 'A', planetPos, ref error);
                houseOfPlanet--;

                if (planet == SwissEph.SE_SUN)
                {
                    diff = houseOfPlanet  - (xx[0] - longtitude) / 30;
                    Console.WriteLine("Diff: " + diff);
                }
                int houseNum = (int)(xx[0] / 30 + diff);

                if (houseNum <= 0) houseNum += 12;

                if (houseNum > 12) houseNum %= 12;

                Console.WriteLine($"planet {planetName} house {houseNum}");

                House house = houseDic[houseNum];
                PlanetPositionView planetPosition = new PlanetPositionView();

                if (house != null)
                {
                    planetPosition.HouseId = house.Id;
                    planetPosition.HouseName = house.Name;
                }

                int zHouse = (int)Math.Ceiling(xx[0] / 30);
                Models.Zodiac z = zodiacDic[zHouse];

                if (z != null)
                {
                    planetPosition.ZodiacName = z.Name;
                    planetPosition.ZodiacId = z.Id;
                }

                Planet p = planetDic[planetName.ToLower()];
                planetPosition.PlanetId = p.Id;

                planetPositionDic[planetName] = planetPosition;
            }
            return planetPositionDic;
        }

        public  (Dictionary<string, (double X, double Y, string planetName)> planetPos, double diff) GetPlanetCoordinate(DateTime birthDate, double longtitude, double latitude)
        {
            Dictionary<string, (double, double, string planetName)> planetPosition = new Dictionary<string, (double, double, string)>();
            string error = "";
            double[] planetPos = new double[2];

            double[] xx = new double[6]; //6 position values: longitude, latitude, distance, *long.speed, lat.speed, dist.speed 
            double julianDay = _swiss.swe_julday(birthDate.Year, birthDate.Month, birthDate.Day, birthDate.Hour, SwissEph.SE_GREG_CAL);

            
            double diff = 0d; //the diffirent of zodiac and house
            _swiss.swe_calc_ut(julianDay, SwissEph.SE_ECL_NUT, 0, xx, ref error);
            double eps_true = xx[0];

            for (int planet = SwissEph.SE_SUN; planet <= SwissEph.SE_PLUTO; planet++)
            {
                if (planet == SwissEph.SE_EARTH) continue;

                _swiss.swe_calc_ut(julianDay, planet, SwissEph.SEFLG_SPEED, xx, ref error);
                string planetName = _swiss.swe_get_planet_name(planet);
                planetPosition[planetName] = (-xx[0] + 180, xx[1], planetName.Substring(0, 2).ToLower());

                if (planet == SwissEph.SE_SUN)
                {
                    double[] ascmc = new double[10];
                    double[] cusps = new double[13];
                    _swiss.swe_houses(julianDay, latitude, longtitude, 'A', cusps, ascmc);
                    planetPos[0] = xx[0];
                    planetPos[1] = xx[1];
                    double houseOfPlanet = _swiss.swe_house_pos(ascmc[2], latitude, eps_true, 'A', planetPos, ref error);
                    Console.WriteLine(houseOfPlanet);
                    diff = --houseOfPlanet * 30 - xx[0] - longtitude;
                }

            }
            return (planetPosition, diff);

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public Image GetChart(DateTime birthDate, double longtitude, double latitude)
        {
            (Dictionary<string, (double X, double Y, string planetName)> planetPosition, double diff)  = this.GetPlanetCoordinate(birthDate, longtitude, latitude);

            Image image = Image.FromFile(@"resource\zodiacChart.png");
            var g = Graphics.FromImage(image);
            Image planetImg;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //draw house
            g.FillEllipse(Brushes.White, (int)(center.X - houseOutterRadius), center.Y - houseOutterRadius, houseOutterRadius * 2, houseOutterRadius * 2);
            g.DrawEllipse(new Pen(Brushes.Black) { Width = 3, DashCap = System.Drawing.Drawing2D.DashCap.Round }, center.X - houseInnerRadius, center.Y - houseInnerRadius, houseInnerRadius * 2, houseInnerRadius * 2);

            double angleDiff = diff * Math.PI / 180;

            for (int i = 12; i >= 1; i--)
            {
                angleDiff += Math.PI / 6;
                g.DrawLine(
                    new Pen(Brushes.Black) { Width = 2 },
                    new Point()
                    {
                        X = (int)((houseInnerRadius) * Math.Cos(angleDiff)) + center.X,
                        Y = (int)((houseInnerRadius) * Math.Sin(angleDiff)) + center.Y,
                    },
                    new Point()
                    {
                        X = (int)((houseOutterRadius + 5) * Math.Cos(angleDiff)) + center.X,
                        Y = (int)((houseOutterRadius + 5) * Math.Sin(angleDiff)) + center.Y,
                    });

                g.DrawString(i.ToString(), new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, new PointF()
                {
                    X = (float)(((houseInnerRadius + 20) * Math.Cos(angleDiff + Math.PI / 12)) + center.X - 15),
                    Y = (float)(((houseInnerRadius + 20) * Math.Sin(angleDiff + Math.PI / 12)) + center.Y - 15),
                });

            }

            //draw planet
            foreach (var key1 in planetPosition.Keys)
            {
                PointF point = new PointF();
                double angle = (planetPosition[key1].X / 180) * Math.PI;

                point.X = (float)((pointRadius * Math.Cos(angle)) + center.X);
                point.Y = (float)((pointRadius * Math.Sin(angle)) + center.Y);
                /*g.DrawString(planetPosition[planet].planetName, new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, point);*/
                try
                {
                    planetImg = Image.FromFile(@$"resource\{planetPosition[key1].planetName}.png");
                    planetImg = (Image)(new Bitmap(planetImg, new Size() { Width = 20, Height = 20 }));
                    g.DrawImage(
                            planetImg,
                            new Point()
                            {
                                X = (int)((planetRadius * Math.Cos(angle)) + center.X - 15),
                                Y = (int)((planetRadius * Math.Sin(angle)) + center.Y - 15)
                            }
                        );
                }
                catch
                {

                }

                g.FillRectangle(Brushes.Crimson, (float)point.X, (float)point.Y, 2, 2);

                point = new PointF()
                {
                    X = (float)(((houseInnerRadius) * Math.Cos(angle)) + center.X),
                    Y = (float)(((houseInnerRadius) * Math.Sin(angle)) + center.Y),
                };
                g.FillRectangle(Brushes.Crimson, (float)point.X, (float)point.Y, 2, 2);

                //draw aspect:

                foreach (var key2 in planetPosition.Keys)
                {
                    if (key1 == key2) continue;
                    var planet1 = planetPosition[key1];
                    var planet2 = planetPosition[key2];

                    double degree = Math.Abs(planet1.X - planet2.X);

                    if (degree > 180)
                    {
                        degree = 360 - degree;
                    }
                    double angle1 = (planet1.X / 180) * Math.PI;
                    double angle2 = (planet2.X / 180) * Math.PI;

                    if ((degree >= BlueSpecialScope1.Item1 && degree <= BlueSpecialScope1.Item2)
                        || (degree >= BlueSpecialScope2.Item1 && degree <= BlueSpecialScope2.Item2))
                    {
                        g.DrawLine(BluePen, new PointF()
                        {
                            X = (float)(((houseInnerRadius) * Math.Cos(angle1)) + center.X),
                            Y = (float)(((houseInnerRadius) * Math.Sin(angle1)) + center.Y),
                        }, new PointF()
                        {
                            X = (float)(((houseInnerRadius) * Math.Cos(angle2)) + center.X),
                            Y = (float)(((houseInnerRadius) * Math.Sin(angle2)) + center.Y),
                        });
                    }
                    else if ((degree >= RedSpecialScope1.Item1 && degree <= RedSpecialScope1.Item2) ||
                              (degree >= RedSpecialScope2.Item1 && degree <= RedSpecialScope2.Item2)
                              || (degree >= RedSpecialScope3.Item1 && degree <= RedSpecialScope3.Item2))
                    {
                        g.DrawLine(RedPen, new PointF()
                        {
                            X = (float)(((houseInnerRadius) * Math.Cos(angle1)) + center.X),
                            Y = (float)(((houseInnerRadius) * Math.Sin(angle1)) + center.Y),
                        }, new PointF()
                        {
                            X = (float)(((houseInnerRadius) * Math.Cos(angle2)) + center.X),
                            Y = (float)(((houseInnerRadius) * Math.Sin(angle2)) + center.Y),
                        });
                    }
                    else if ((degree >= GraySpecialScope1.Item1 && degree <= GraySpecialScope1.Item2)
                              || (degree >= GraySpecialScope2.Item1 && degree <= GraySpecialScope2.Item2))
                    {
                        g.DrawLine(GrayPen, new PointF()
                        {
                            X = (float)(((houseInnerRadius) * Math.Cos(angle1)) + center.X),
                            Y = (float)(((houseInnerRadius) * Math.Sin(angle1)) + center.Y),
                        }, new PointF()
                        {
                            X = (float)(((houseInnerRadius) * Math.Cos(angle2)) + center.X),
                            Y = (float)(((houseInnerRadius) * Math.Sin(angle2)) + center.Y),
                        });
                    }
                }
/*
                Console.WriteLine($"planet: {key1} angle: {angle}, point: {point}");*/
            }

            //draw earth
            planetImg = Image.FromFile(@$"resource\ea.png");
            g.FillEllipse(Brushes.White, center.X - 20, center.Y - 20, 39, 39);
            planetImg = (Image)(new Bitmap(planetImg, new Size() { Width = 40, Height = 40 }));
            g.DrawImage(planetImg, new Point() { X = center.X - 20, Y = center.Y - 20 });
            g.Flush();
            return image;
        }

        public string GetChartFile(DateTime birthDate, double longtitude, double latitude)
        {
            Image image = GetChart(birthDate, longtitude, latitude);
            var stream = new MemoryStream();
            string fileName = @$"resource\chart-{Guid.NewGuid()}.png";
            var file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            image.Save(file, ImageFormat.Png);
            file.Close();
            return file.Name;
        }

        public string GetChartLinkFirebase(DateTime birthDate, double longtitude, double latitude)
        {

            var fileName = this.GetChartFile(birthDate, longtitude, latitude);


            var file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            string link =  _firebase.UploadImage(file, fileName.Split('\\').Last()).Result;
            file.Close();

            System.IO.File.Delete(fileName);

            return link;
        }
    }
}
