using System;
using Rage;

namespace DatasetGenerator.ScenarioCreation
{
    public class TimeSettings
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        public void Apply()
        {
            World.TimeOfDay = new TimeSpan(Hour, Minute, 0);
        }
    }
}
