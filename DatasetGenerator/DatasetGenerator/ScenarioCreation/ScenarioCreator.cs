using System.Windows.Forms;
using Rage;
using Keys = System.Windows.Forms.Keys;

namespace DatasetGenerator.ScenarioCreation
{
    public class ScenarioCreator : Component
    {
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
                if (CameraLocked)
                {
                    var gameplayCameraValues = Utility.GetGameplayCameraValues();
                    Camera = new Camera(DisposableCamera.DefaultScriptedCamera, false);
                    Camera.SetCameraValues(gameplayCameraValues);
                    Camera.Active = true;
                }
                else
                {
                    Camera.Delete();
                    Camera = null;
                }

                Game.DisplaySubtitle("Camera " + (CameraLocked ? "locked" : "unlocked"));
            }
            
        }
    }
}
