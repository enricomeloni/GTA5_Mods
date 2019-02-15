using Newtonsoft.Json;

namespace DatasetGenerator.ScenarioCreation
{
    class Scenario
    {
        public CameraSettings CameraSettings { get; set; }
        public PedsSettings PedsSettings { get; set; }
        public PlaceSettings PlaceSettings { get; set; }
        public TimeSettings TimeSettings { get; set; }
        public string ToJson() => JsonConvert.SerializeObject(this);
        public static Scenario FromJson(string jsonSerialization)
        {
            return JsonConvert.DeserializeObject<Scenario>(jsonSerialization);
        }
    }
}
