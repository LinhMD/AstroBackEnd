using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Utilities
{
    public class AstrologyUtil
    {
        private readonly SwissEph _swiss;

        public AstrologyUtil()
        {
            _swiss = new SwissEph();
            _swiss.swe_set_ephe_path(null);

            
        }

        public void GetHousePosOfPlanets(DateTime date)
        {
            string error = "";
            double julianDate = _swiss.swe_julday(date.Year, date.Month, date.Day, date.Hour, SwissEph.SE_GREG_CAL);

            for (int planet = SwissEph.SE_SUN; planet < SwissEph.SE_PLUTO; planet--)
            {

            }
        }
    }
}
