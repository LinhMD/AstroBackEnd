using AstroBackEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class BirthChartView
    {
        public BirthChartView(BirthChart birthChart)
        {
            this.ImgLink = birthChart.ImgLink;
            this.Items = new Dictionary<string, ChartItemView>();
        }

        public string ImgLink { get; set; }

        public Dictionary<string, ChartItemView> Items { get; set; }

        
    }
}
