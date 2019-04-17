using System.Collections.Generic;
using Rage;

namespace DatasetGenerator.ScenarioCreation
{
    public class PlaceSettings
    {
        public Place Place { get; set; }

        public void Apply()
        {
            if(Place != null)
                Game.LocalPlayer.Character.Position = Place.Position;
        }

        public void Clear()
        {
            Place = null;
        }
    }

    public class Place
    {
        public string Name { get; set; } = "";
        public Vector3 Position { get; set; } = Vector3.Zero;

        public Place(string name, Vector3 position)
        {
            Name = name;
            Position = position;
        }

        public static readonly List<Place> Places = new List<Place>
        {
            new Place("MICHAEL'S HOUSE", new Vector3(-852.4f, 160.0f, 65.6f)),
            new Place("FRANKLIN'S HOUSE", new Vector3(7.9f, 548.1f, 175.5f)),
            new Place("TREVOR'S TRAILER", new Vector3(1985.7f, 3812.2f, 32.2f)),
            new Place("AIRPORT ENTRANCE", new Vector3(-1034.6f, -2733.6f, 13.8f)),
            new Place("AIRPORT FIELD", new Vector3(-1336.0f, -3044.0f, 13.9f)),
            new Place("ELYSIAN ISLAND", new Vector3(338.2f, -2715.9f, 38.5f)),
            new Place("JETSAM", new Vector3(760.4f, -2943.2f, 5.8f)),
            new Place("STRIPCLUB", new Vector3(127.4f, -1307.7f, 29.2f)),
            new Place("ELBURRO HEIGHTS", new Vector3(1384.0f, -2057.1f, 52.0f)),
            new Place("FERRIS WHEEL", new Vector3(-1670.7f, -1125.0f, 13.0f)),
            new Place("CHUMASH", new Vector3(-3192.6f, 1100.0f, 20.2f)),
            new Place("WINDFARM", new Vector3(2354.0f, 1830.3f, 101.1f)),
            new Place("MILITARY BASE", new Vector3(-2047.4f, 3132.1f, 32.8f)),
            new Place("MCKENZIE AIRFIELD", new Vector3(2121.7f, 4796.3f, 41.1f)),
            new Place("DESERT AIRFIELD", new Vector3(1747.0f, 3273.7f, 41.1f)),
            new Place("CHILLIAD", new Vector3(425.4f, 5614.3f, 766.5f)),
            new Place("SHIPYARD", new Vector3(920.835266f,-3060.08643f, 5.900765f))
        };
    }

}
