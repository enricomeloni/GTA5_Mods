using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetGenerator.ScenarioCreation
{
    public class PedsSettings
    {
        public int PedsNumber { get; set; }
        public PedBehavior PedBehavior { get; set; }
        public bool PedShouldGroup { get; set; }
    }

    public enum PedBehavior
    {
        Stand,
        Phone,
        Cower,
        Wander,
        Chat,
        Combat,
        Cover,
        Move,
        Scenario
    }
}
