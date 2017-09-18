using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectWebsite.Models
{
    public class WeatherDBContext : DbContext 
    {
        public WeatherDBContext() : base("name=ConnName")
        {

        }

        public DbSet<WeatherModels.weather> Weather { get; set; }
    }
}