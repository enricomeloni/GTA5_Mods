using System.IO;
using Newtonsoft.Json;
using Rage;

namespace DatasetGenerator.ScenarioCreation
{
    public class Scenario
    {
        public CameraSettings CameraSettings { get; set; } = new CameraSettings();
        public PedsSettings PedsSettings { get; set; } = new PedsSettings();
        public PlaceSettings PlaceSettings { get; set; } = new PlaceSettings();
        public TimeSettings TimeSettings { get; set; } = new TimeSettings();
        public WeatherSettings WeatherSettings { get; set; } = new WeatherSettings();

        [JsonIgnore]
        public Ped[] SpawnedPeds; 
        public string ToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public void FromJson(string jsonSerialization)
        {
            var loadedScenario = JsonConvert.DeserializeObject<Scenario>(jsonSerialization);
            CameraSettings = loadedScenario.CameraSettings;
            PedsSettings = loadedScenario.PedsSettings;
            PlaceSettings = loadedScenario.PlaceSettings;
            TimeSettings = loadedScenario.TimeSettings;
            WeatherSettings = loadedScenario.WeatherSettings;
        }

        public void FromJson(FileInfo jsonFile)
        {
            using (var fileStream = jsonFile.OpenText())
            {
                FromJson(fileStream.ReadToEnd());
            }
        }

        public void Apply()
        {
            PlaceSettings.Apply();
            TimeSettings.Apply();
            WeatherSettings.Apply();

            SpawnedPeds = PedsSettings.Apply();
        }

        public void Clear()
        {
            CameraSettings.Clear();
            PedsSettings.Clear();
            PlaceSettings.Clear();
            TimeSettings.Clear();
            WeatherSettings.Clear();
        }
    }
}
