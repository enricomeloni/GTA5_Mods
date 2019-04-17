using System;
using System.Collections;
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

        public static readonly DirectoryInfo RootDatasetDirectory = new DirectoryInfo("F:/dataset");
        public static readonly DirectoryInfo RootScenariosDirectory = new DirectoryInfo("F:/scenarios");
        private DirectoryInfo CurrentDatasetSession;

        private bool IsRecording = false;

        private const int WaitA = 2;
        private const int WaitB = 4;
        private const int WaitC = 10;

        private int FrameID = 1;
        private readonly int FramesPerScenario = 900;
        private Scenario Scenario { get; set; }
        private Queue<FileInfo> Scenarios { get; set; }

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

                if (IsRecording && FrameID > FramesPerScenario)
                {
                    StopScenario();
                    if (Scenarios.Count != 0)
                        StartScenario();
                    else
                    {
                        StopRecording();
                    }
                }

                GameFiber.Yield();
            }
        }

        private void StartScenario()
        {
            if (Scenarios.Count == 0)
                throw new Exception("Scenarios empty");
            Scenario.FromJson(Scenarios.Dequeue());
            World.CleanWorld(true, true, true, true, true, false);

            var dateString = $"{DateTime.Now:dd_MM_yy_HH_mm_ss}";
            CurrentDatasetSession = new DirectoryInfo(Path.Combine(RootDatasetDirectory.FullName, dateString));
            FrameID = 1;
            Scenario.Apply();

            Utility.WaitTicks(2000);
        }

        private void StopScenario()
        {
            if (SpawnedPeds != null)
            {
                foreach (var spawnedPed in SpawnedPeds)
                {
                    spawnedPed.Delete();
                }

                SpawnedPeds = null;
            }
            Game.IsPaused = false;
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

            if (Game.IsKeyDown(Keys.F8))
            {
                PauseRecording();
            }

            if (Game.IsKeyDown(Keys.F7))
            {
                StopRecording();
            }
        }

        private void PauseRecording()
        {
            var pauseState = Game.IsPaused;
            Game.IsPaused = true;
            while (true)
            {
                if (Game.IsKeyDown(Keys.F8))
                {
                    break;
                }
                GameFiber.Yield();
            }

            Game.IsPaused = pauseState;
        }

        private void AnnotateScreen()
        {
            if (!IsRecording) return;

            //it happens that map is randomly redrawn, this is just a precaution
            Game.LocalPlayer.Character.IsVisible = false;
            NativeFunction.Natives.DisplayHud(false);
            NativeFunction.Natives.DisplayRadar(false);

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
                        Game.IsPaused = false;
                        WaitTicks(WaitA);
                        Game.IsPaused = true;
                        WaitTicks(WaitB);
                        
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

            Game.IsPaused = false;
            WaitTicks(WaitC);
            Game.IsPaused = true;
        }

        private void WaitTicks(int ticks)
        {
            for (int currentTick = 0; currentTick < ticks; ++currentTick)
            {
                var isRecording = IsRecording;
                HandleKeyboardState();
                if (isRecording != IsRecording)
                    throw new RecordingInterruptedException();
                GameFiber.Yield();
            }
        }

        private class RecordingInterruptedException : Exception
        {
        }

        private void StopRecording()
        {
            if (!IsRecording)
                return;
            StopScenario(); //to be sure peds are deleted
            IsRecording = false;
            NativeFunction.Natives.DisplayHud(true);
            NativeFunction.Natives.DisplayRadar(true);
            Game.LocalPlayer.Character.IsVisible = true;
            Game.DisplaySubtitle("Stop recording");
        }

        private void StartRecording()
        {
            if (IsRecording)
                return;

            Scenarios = new Queue<FileInfo>(RootScenariosDirectory.EnumerateFiles().Where(file => file.Name.EndsWith(".json")));
            if (Scenarios.Count == 0)
            {
                Game.Console.Print("No scenarios in directory");
                return;
            }

            Game.DisplaySubtitle("Start recording");
            
            Game.LocalPlayer.Character.IsVisible = false;
            NativeFunction.Natives.DisplayHud(false);
            NativeFunction.Natives.DisplayRadar(false);
            IsRecording = true;
            StartScenario();
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

                var pedBox = new PedBoundingBox(ped);

                if (pedBox.ShouldDraw(camera))
                {
                    var detectedPed = pedBox.ToDetectedObject();
                    detectedObjects.Add(detectedPed);
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
