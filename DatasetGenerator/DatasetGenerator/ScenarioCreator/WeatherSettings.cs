using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetGenerator.ScenarioCreator
{
    public class WeatherSettings
    {
        public Weathers Weather { get; set; }
    }

    public enum Weathers
    {
        Wind,
        Extrasunny,
        Clear,
        Clouds,
        Smog,
        Foggy,
        Overcast,
        Rain,
        Thunder,
        Clearing,
        Neutral,
        Snow,
        Blizzard,
        Snowlight
    }
}
