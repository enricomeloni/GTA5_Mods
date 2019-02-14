using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rage;

namespace DatasetGenerator
{
    class DatasetAnnotator
    {

        private readonly List<Ped> spawnedPeds = new List<Ped>();
        private Camera currentCamera = null;

        public GameFiber GameFiber { get; set; }
        private readonly FrameRenderHandler FrameRenderHandler = new FrameRenderHandler();

        public DatasetAnnotator()
        {
            GameFiber = new GameFiber(Main);
            GameFiber.Start();
        }

        private void Main()
        {
            while (true)
            {
                Game.LocalPlayer.WantedLevel = 0;
                Game.LocalPlayer.IsIgnoredByEveryone = true;
                Game.LocalPlayer.IsIgnoredByPolice = true;

                HandleKeyboardState(Game.GetKeyboardState());

                GameFiber.Yield();
            }
        }

        private void HandleKeyboardState(KeyboardState keyboardState)
        {
            if (keyboardState.PressedKeys.Contains(Keys.F9))
                Game.IsPaused = true;
            else if (keyboardState.PressedKeys.Contains(Keys.F10))
                Game.IsPaused = false;

            if (keyboardState.PressedKeys.Contains(Keys.F6))
            {
                FrameRenderHandler.StartRecording();
                Game.DisplaySubtitle("Start recording");
            }

            if (keyboardState.PressedKeys.Contains(Keys.F7))
            {
                FrameRenderHandler.StopRecording();
                Game.IsPaused = false;
                Game.DisplaySubtitle("Stop recording");
            }

            if (Game.IsKeyDown(Keys.F11))
            {
                var me = Game.LocalPlayer.Character;
                Vector3 pedPosition = me.LeftPosition - me.RightVector * 5 + me.ForwardVector * 2;

                var ped = PedSpawner.SpawnNewPed(pedPosition);
                ped.Tasks.Wander();
                spawnedPeds.Add(ped);
            }

            if (Game.IsKeyDown(Keys.F12))
            {
                foreach (var spawnedPed in spawnedPeds)
                {
                    spawnedPed.Delete();
                }

                spawnedPeds.Clear();
            }

            if (Game.IsKeyDown(Keys.K))
            {
                if (currentCamera == null)
                {
                    var me = Game.LocalPlayer.Character;
                    Game.DisplaySubtitle("Creating camera?");
                    var camera = new Camera(DisposableCamera.DefaultScriptedCamera, false);
                    camera.SetCameraValues(Utility.GetGameplayCameraValues());

                    camera.Position = me.Position;
                    currentCamera = camera;
                    currentCamera.Active = true;
                }
            }

            if (Game.IsKeyDown(Keys.J))
            {
                Game.DisplaySubtitle("Deleting camera?");
                if (currentCamera != null)
                {
                    //currentCamera.Active = false;
                    currentCamera.Delete();
                    currentCamera = null;
                }
            }

        }
    }
}
