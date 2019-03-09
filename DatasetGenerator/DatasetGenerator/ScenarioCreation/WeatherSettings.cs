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
