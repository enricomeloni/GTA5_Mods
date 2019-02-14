using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace DatasetGenerator.ScenarioCreator
{
    class CameraSettings
    {
        public bool Locked { get; set; }
        public Vector3 Position { get; set; }
        public bool Moving { get; set; }

        //todo: find better names
        public Vector2 A { get; set; }
        public Vector2 B { get; set; }
        public Vector2 C { get; set; }
        public Vector2 Teleport1 { get; set; }
        public Vector2 Teleport2 { get; set; }
    }
}
