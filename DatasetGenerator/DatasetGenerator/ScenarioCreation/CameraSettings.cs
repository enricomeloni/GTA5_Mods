using System.Collections.Generic;

namespace DatasetGenerator.ScenarioCreation
{
    public class CameraSettings
    {
        public List<CameraValues> Cameras { get; set; } = new List<CameraValues>();

        public void Clear()
        {
            Cameras.Clear();
        }
    }
}
