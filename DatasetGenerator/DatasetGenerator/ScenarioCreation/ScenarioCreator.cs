using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Rage;
using Keys = System.Windows.Forms.Keys;

namespace DatasetGenerator.ScenarioCreation
{
    public class ScenarioCreator : Component
    {
        private Scenario Scenario { get; set; } = new Scenario();
        private readonly DirectoryInfo ScenariosDirectory = new DirectoryInfo("D:/dataset/scenarios");
        protected override void Main()
        {
            while (true)
            {
                Game.LocalPlayer.WantedLevel = 0;
                Game.LocalPlayer.IsIgnoredByEveryone = true;
                Game.LocalPlayer.IsIgnoredByPolice = true;

                HandleKeyboardState();

                var cameraValues = Utility.GetGameplayCameraValues();
                var position = cameraValues.Position;
                var rotator = cameraValues.Rotation;
                var fov = cameraValues.Fov;
                Game.DisplaySubtitle($"pos: ({position.X:F2}, {position.Y:F2}, {position.Z:F2}; rot: ({rotator.Pitch:F2}, {rotator.Roll:F2}, {rotator.Yaw:F2}); fov: {fov:F2}");

                GameFiber.Yield();
            }
        }

        public bool CameraLocked => Camera == null;
        private Camera Camera { get; set; }
        protected override void HandleKeyboardState()
        {
            if (Game.IsKeyDown(Keys.O))
            {
                var gameplayCameraValues = Utility.GetGameplayCameraValues();
                Scenario.CameraSettings.Cameras.Add(gameplayCameraValues);
            }

            if (Game.IsKeyDown(Keys.K))
            {
                Game.Console.Print(ScenariosDirectory.FullName);
                using (var fileStream = File.CreateText(Path.Combine(ScenariosDirectory.FullName, "scenario1.json")))
                {
                    fileStream.WriteLine(Scenario.ToJson());
                }
            }

            if (Game.IsKeyDown(Keys.J))
            {
                Camera.DeleteAllCameras();
            }
        }
    }
}
