using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
