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

        public static void BoundingBoxGraphicHandler(object sender, GraphicsEventArgs e)
        {
            if (IsRecording)
            {
                var me = Game.LocalPlayer.Character;

                var nearbyPeds = new List<Ped>(me.GetNearbyPeds(5));

                //nearbyPeds = new List<Ped>();
                //nearbyPeds.Add(me);

                

                var detectedObjects = new List<DetectedObject>();

                using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
                {
                    var camera = disposableCamera.Camera;
                    camera.SetCameraValues(Utility.GetGameplayCameraValues());

                    foreach (var ped in nearbyPeds)
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

                        var weapon = ped.Inventory.EquippedWeaponObject;
                        if (weapon && weapon.IsVisible)
        public static void DebugGraphicHandler(object sender, GraphicsEventArgs e)
        {
            var me = Game.LocalPlayer.Character;

            var nearbyPeds = new List<Ped>(me.GetNearbyPeds(5));

            nearbyPeds = new List<Ped> {me};

            var detectedObjects = new List<DetectedObject>();

            using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
            {
                var camera = disposableCamera.Camera;
                camera.SetCameraValues(Utility.GetGameplayCameraValues());

                PedBoneId[] bones =
                {
                    PedBoneId.Head, PedBoneId.Neck
                };

                Color[] colors =
                {
                    Color.Blue, Color.Red, Color.DarkOrange, Color.BlueViolet, Color.Black, Color.DarkTurquoise
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
                        headBox.Draw(e.Graphics, Color.Blue);
                    }

                    var chestBox = new ChestBoundingBox(ped);
                    if (chestBox.ShouldDraw(camera))
                    {
                        //chestBox.Draw(e.Graphics, Color.Red);
                    }

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
