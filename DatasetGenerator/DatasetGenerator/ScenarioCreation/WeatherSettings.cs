using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace DatasetGenerator.ScenarioCreation
{
    public class WeatherSettings
    {
        public WeatherType Weather { get; set; }

        public void Apply()
        {
            World.Weather = Weather;
        }
    }
}
