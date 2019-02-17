using Newtonsoft.Json;

namespace DatasetGenerator.ScenarioCreation
{
    public class Scenario
    {
        public CameraSettings CameraSettings { get; set; } = new CameraSettings();
        public PedsSettings PedsSettings { get; set; } = new PedsSettings();
        public PlaceSettings PlaceSettings { get; set; } = new PlaceSettings();
        public TimeSettings TimeSettings { get; set; } = new TimeSettings();
        public string ToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public static Scenario FromJson(string jsonSerialization)
        {
            return JsonConvert.DeserializeObject<Scenario>(jsonSerialization);
        }
    }
}
