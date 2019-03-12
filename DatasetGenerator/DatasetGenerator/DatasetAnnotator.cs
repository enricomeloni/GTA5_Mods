using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DatasetGenerator.BoundingBoxes;
using DatasetGenerator.ScenarioCreation;
using Rage;
using Rage.Native;

namespace DatasetGenerator
{
    class DatasetAnnotator : Component
    {

        private Ped[] SpawnedPeds
        {
            get => Scenario.SpawnedPeds;
            set => Scenario.SpawnedPeds = value;
        }

        public static readonly DirectoryInfo RootDatasetDirectory = new DirectoryInfo("D:/dataset");
        public static readonly DirectoryInfo RootScenariosDirectory = new DirectoryInfo("G:/scenarios");
        private DirectoryInfo CurrentDatasetSession;

        private bool IsRecording = false;

        private const int WaitA = 1;
        private const int WaitB = 3;

        private int FrameID = 1;
        private readonly int FramesPerScenario = 900;
        private Scenario Scenario { get; set; }

        public DatasetAnnotator(Scenario scenario)
        {
            Scenario = scenario;
            var scenario3Path = Path.Combine(RootScenariosDirectory.FullName, "scen3.json");
            using (var fileStream = File.OpenText(scenario3Path))
            {
                Scenario.FromJson(fileStream.ReadToEnd());
            }

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

                if(FrameID >= FramesPerScenario + 1)
                    StopRecording();

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
                    var cameraDirectory = new DirectoryInfo(Path.Combine(CurrentDatasetSession.FullName, $"{index:D3}"));
                    if(!cameraDirectory.Exists)
                        cameraDirectory.Create();

                    camera.SetCameraValues(cameraValue);
                    camera.Active = true;
                    try
                    {
                        WaitTicks(WaitA, false);
                        WaitTicks(WaitB, true);
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

        //paused == true, first pause and then unpause;
        //paused == false; first unpause and then pause;
        private void WaitTicks(int ticks, bool paused)
        {
            Game.IsPaused = paused;
            for (int currentTick = 0; currentTick < ticks; ++currentTick)
            {
                var isRecording = IsRecording;
                HandleKeyboardState();
                if (isRecording != IsRecording)
                    throw new RecordingInterruptedException();
                GameFiber.Yield();
            }
            Game.IsPaused = !paused;
        }

        private class RecordingInterruptedException : Exception
        {
        }

        private void StopRecording()
        {
            if (!IsRecording)
                return;
            IsRecording = false;
            NativeFunction.Natives.DisplayHud(true);
            NativeFunction.Natives.DisplayRadar(true);
            Game.LocalPlayer.Character.IsVisible = true;
            if(SpawnedPeds != null)
            {
                foreach (var spawnedPed in SpawnedPeds)
                {
                    spawnedPed.Delete();
                }

            }
            Game.IsPaused = false;
            Game.DisplaySubtitle("Stop recording");
        }

        private void StartRecording()
        {
            if (IsRecording)
                return;

            Game.DisplaySubtitle("Start recording");

            World.CleanWorld(true, true, true, true, true, false);

            //DatasetDirectory.Empty(); 
            
            var dateString = $"{DateTime.Now:dd_MM_yy_HH_mm_ss}";
            
            CurrentDatasetSession = new DirectoryInfo(Path.Combine(RootDatasetDirectory.FullName, dateString));

            FrameID = 1;
            Game.LocalPlayer.Character.IsVisible = false;
            Scenario.Apply();
            NativeFunction.Natives.DisplayHud(false);
            NativeFunction.Natives.DisplayRadar(false);
            Utility.WaitTicks(500);
            IsRecording = true;
        }

        private static Bitmap GetBitmapFromScreen()
        {
            Size resolution = Game.Resolution;
            var bitmap = new Bitmap(resolution.Width, resolution.Height, PixelFormat.Format24bppRgb);
            var graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, resolution);
            return bitmap;
        }

        private static List<DetectedObject> DetectObjects(IEnumerable<Ped> peds, Camera camera)
        {
            List<DetectedObject> DetectObjectsForPed(Ped ped)
            {
                List<DetectedObject> detectedObjects = new List<DetectedObject>();

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

                return detectedObjects;
            }

            return peds
                .Select(DetectObjectsForPed)
                .SelectMany(objs => objs)
                .ToList();
        }
    }
}
