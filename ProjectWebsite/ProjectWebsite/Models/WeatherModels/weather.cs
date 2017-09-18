using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectWebsite.Models.WeatherModels
{
    public class weather
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public double temp { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public double humidity { get; set; }
        public DateTime dateTime { get; set; }
    }
}