using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatasetGenerator.BoundingBoxes;
using DatasetGenerator.ScenarioCreation;
using Rage;
using Graphics = Rage.Graphics;

namespace DatasetGenerator
{
    class DatasetAnnotator : Component
    {

        private Ped[] SpawnedPeds;
        private readonly DirectoryInfo DatasetDirectory = new DirectoryInfo("D:/dataset");

        private bool IsRecording = false;

        private const int WaitA = 3;
        private const int WaitB = 2;

        private int FrameID = 1;
        private Scenario Scenario { get; set; }

        public DatasetAnnotator(Scenario scenario)
        {
            Scenario = scenario;
        }

        protected override void Main()
        {
            while (true)
            {
                Game.LocalPlayer.WantedLevel = 0;
                Game.LocalPlayer.IsIgnoredByEveryone = true;
                Game.LocalPlayer.IsIgnoredByPolice = true;

                HandleKeyboardState();
                AnnotateScreen();
                
                GameFiber.Yield();
            }
        }

        protected override void HandleKeyboardState()
        {
            if (Game.IsKeyDown(Keys.F9))
                Game.IsPaused = true;
            else if (Game.IsKeyDown(Keys.F10))
                Game.IsPaused = false;

            if (Game.IsKeyDown(Keys.F6))
            {
                StartRecording();
            }

            if (Game.IsKeyDown(Keys.F7))
            {
                StopRecording();
            }
        }

        private void AnnotateScreen()
        {
            if (!IsRecording) return;
            var cameras = Scenario.CameraSettings.Cameras;
            foreach (var (cameraValue, index) in cameras.WithIndex())
            {
                using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
                {
                    var camera = disposableCamera.Camera;
                    var cameraDirectory = new DirectoryInfo(Path.Combine(DatasetDirectory.FullName, $"{index:D3}"));
                    cameraDirectory.Create();

                    camera.SetCameraValues(cameraValue);
                    camera.Active = true;
                    try
                    {
                        WaitTicks(WaitB, false);
                        WaitTicks(WaitA, true);
                    }
                    catch (RecordingInterruptedException)
                    {
                        return;
                    }

                    var detectedObjects = DetectObjects(SpawnedPeds, camera);

                    //create screen snapshot                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                    var bitmap = GetBitmapFromScreen();

                    var frameMetadata = new FrameMetadata(FrameID, bitmap, detectedObjects);
                    frameMetadata.SaveToFolder(cameraDirectory);
                }
            }

            ++FrameID;
        }

        //order == true, first pause and then unpause;
        //order == false; first unpause and then pause;
        private void WaitTicks(int ticks, bool order)
        {
            Game.IsPaused = order;
            for (int currentTick = 0; currentTick < ticks; ++currentTick)
            {
                var isRecording = IsRecording;
                HandleKeyboardState();
                if (isRecording != IsRecording)
                    throw new RecordingInterruptedException();
                GameFiber.Yield();
            }
            Game.IsPaused = !order;
        }

        private class RecordingInterruptedException : Exception
        {
        }

        private void StopRecording()
        {
            IsRecording = false;
            Game.LocalPlayer.Character.IsVisible = true;
            if(SpawnedPeds != null)
            {
                foreach (var spawnedPed in SpawnedPeds)
                {
                    spawnedPed.Delete();
                }

            }
            SpawnedPeds = null;
            Game.IsPaused = false;
            Game.DisplaySubtitle("Stop recording");
        }

        private void StartRecording()
        {
            DatasetDirectory.Empty(); 
            FrameID = 1;
            Game.LocalPlayer.Character.IsVisible = false;
            SpawnedPeds = PedSpawner.SpawnPedsFromScenario(Scenario, Game.LocalPlayer.Character.Position);
            Utility.WaitTicks(500);
            IsRecording = true;
            Game.DisplaySubtitle("Start recording");
        }

        private static Bitmap GetBitmapFromScreen()
        {
            Size resolution = Game.Resolution;
            var bitmap = new Bitmap(resolution.Width, resolution.Height, PixelFormat.Format32bppArgb);
            var graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, resolution);
            return bitmap;
        }

        private static List<DetectedObject> DetectObjects(IEnumerable<Ped> peds, Camera camera)
        {
            var detectedObjects = new List<DetectedObject>();

            foreach (var ped in peds)
            {
                var headBox = new HeadBoundingBox(ped);
                if (headBox.ShouldDraw(camera))
                {
                    var detectedHead = headBox.ToDetectedObject();
                    detectedObjects.Add(detectedHead);
                }

                var chestBox = new ChestBoundingBox(ped);
                if (chestBox.ShouldDraw(camera))
                {
                    var detectedChest = chestBox.ToDetectedObject();
                    detectedObjects.Add(detectedChest);
                }

                var weapon = ped.Inventory.EquippedWeaponObject;
                if (weapon && weapon.IsVisible)
                {
                    var weaponBox = new WeaponBoundingBox(weapon);
                    if (weaponBox.ShouldDraw(camera))
                    {
                        var detectedWeapon = weaponBox.ToDetectedObject();
                        detectedObjects.Add(detectedWeapon);
                    }
                }
            }


            return detectedObjects;
        }
    }
}
