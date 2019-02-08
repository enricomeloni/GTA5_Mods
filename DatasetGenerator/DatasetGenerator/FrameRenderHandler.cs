using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DatasetGenerator.BoundingBoxes;
using DatasetGenerator.PedClassifiers;
using Rage;
using Rage.Native;
using Graphics = System.Drawing.Graphics;

namespace DatasetGenerator
{
    class FrameRenderHandler
    {
        public static Camera OldCamera { get; set; }

        public static bool IsRecording { get; set; } = false;
        private static int FrameID = 1;
        private static readonly DirectoryInfo DatasetDirectory = new DirectoryInfo("D:/dataset");

        private static readonly int TicksWithPause = 5;
        private static int CurrentTick = 0;
        public static void BoundingBoxGraphicHandler(object sender, GraphicsEventArgs e)
        {
            if (IsRecording)
            {
                if (CurrentTick == 1)
                {
                    Game.IsPaused = true;
                }
                else if (CurrentTick == TicksWithPause - 1)
                {
                    Game.IsPaused = false;
                }
                else if(CurrentTick == TicksWithPause -2)
                {
                    var me = Game.LocalPlayer.Character;

                    var nearbyPeds = new List<Ped>(me.GetNearbyPeds(5));

                    nearbyPeds = new List<Ped>();
                    nearbyPeds.Add(me);

                    var detectedObjects = DetectObjects(nearbyPeds);

                    //create screen snapshot
                    var bitmap = GetBitmapFromScreen();

                    var frameMetadata = new FrameMetadata(FrameID, bitmap, detectedObjects);
                    frameMetadata.SaveToFolder(DatasetDirectory);
                    ++FrameID;
                }

                CurrentTick = (CurrentTick + 1) % TicksWithPause;
            }
        }


        private static Bitmap GetBitmapFromScreen()
        {
            Size resolution = Game.Resolution;
            var bitmap = new Bitmap(resolution.Width, resolution.Height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, resolution);
            return bitmap;
        }

        private static List<DetectedObject> DetectObjects(List<Ped> peds)
        {
            var detectedObjects = new List<DetectedObject>();

            using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
            {
                var camera = disposableCamera.Camera;
                camera.SetCameraValues(Utility.GetGameplayCameraValues());

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
            }

            return detectedObjects;
        }

        public static void StartRecording()
        {
            if (!IsRecording)
            {
                DatasetDirectory.Empty();
                IsRecording = true;
                FrameID = 1;
            }
        }

        public static void StopRecording()
        {
            if (IsRecording)
            {
                IsRecording = false;
            }
        }

        public static void DebugGraphicHandler(object sender, GraphicsEventArgs e)
        {
            var me = Game.LocalPlayer.Character;

            var nearbyPeds = new List<Ped> {me};

            var detectedObjects = new List<DetectedObject>();

            using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
            {
                var camera = disposableCamera.Camera;
                camera.SetCameraValues(Utility.GetGameplayCameraValues());

                PedBoneId[] bones =
                {
                    PedBoneId.Head, PedBoneId.Neck, PedBoneId.Spine2, PedBoneId.Spine3, 
                };

                Color[] colors =
                {
                    Color.Blue, Color.Red, Color.DarkOrange, Color.BlueViolet, Color.DarkTurquoise
                };

                for (int i = 0; i < bones.Length; ++i)
                {
                    Vector2 boneProjected = me.GetBonePosition(bones[i]).ProjectToScreen();
                    e.Graphics.DrawCircle(boneProjected, 5f, colors[i]);
                }

                foreach (var ped in nearbyPeds)
                {
                    var headBox = new HeadBoundingBox(ped);
                    if (headBox.ShouldDraw(camera))
                    {
                        //headBox.Draw(e.Graphics, Color.Blue);
                    }

                    var chestBox = new ChestBoundingBox(ped);
                    if (chestBox.ShouldDraw(camera))
                    {
                        chestBox.Draw(e.Graphics, Color.Red);
                    }

                    var pedBox = new PedBoundingBox(ped);
                    pedBox.Draw(e.Graphics, Color.Blue);

                    var weapon = ped.Inventory.EquippedWeaponObject;
                    if (weapon && weapon.IsVisible)
                    {
                        var weaponBox = new WeaponBoundingBox(weapon);
                        if (weaponBox.ShouldDraw(camera))
                        {
                            //weaponBox.Draw(e.Graphics, Color.BlueViolet);
                        }
                    }
                }
            }
        }
    }
}
