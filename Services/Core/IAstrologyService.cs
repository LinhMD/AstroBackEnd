using AstroBackEnd.Models;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Core
{
    public interface IAstrologyService
    {
        public Image GetChart(DateTime birthDate, double longtitude, double latitude);

        public (Dictionary<string, (double X, double Y, string planetName)> planetPos, double diff) GetPlanetCoordinate(DateTime birthDate, double longtitude, double latitude);

        public Dictionary<string, PlanetPositionView> GetPlanetPosition(DateTime birthDate, double longtitude, double latitude);

        public Zodiac GetZodiac(DateTime birthDate, double longtitude, double latitude);

        public NatalChartView GetHousePosOfPlanets(DateTime date, double longtitude, double latitude);

        public string GetChartFile(DateTime birthDate, double longtitude, double latitude);

        public string GetChartLinkFirebase(DateTime birthDate, double longtitude, double latitude);

    }
}
