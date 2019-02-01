using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DatasetGenerator.BoundingBoxes;
using Rage;
using Rage.Native;

namespace DatasetGenerator
{
    class FrameRenderHandler
    {
        public static Camera OldCamera { get; set; }

        public static void BoundingBoxGraphicHandler(object sender, GraphicsEventArgs e)
        {
            var graphics = e.Graphics;
            var cameraValues = Utility.GetGameplayCameraValues();
            var me = Game.LocalPlayer.Character;

            var nearbyPeds = new List<Ped>(me.GetNearbyPeds(5));

            using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
            {
                var camera = disposableCamera.Camera;
                camera.SetCameraValues(Utility.GetGameplayCameraValues());

                foreach (var ped in nearbyPeds)
                {
                    var headBox = new HeadBoundingBox(ped);
                    if (headBox.ShouldDraw(camera))
                    {
                        headBox.ToBoundingRect().Draw(graphics, Color.Blue);
                    }

                    var chestBox = new ChestBoundingBox(ped);
                    if (chestBox.ShouldDraw(camera))
                    {
                        chestBox.ToBoundingRect().Draw(graphics, Color.Violet);
                    }

                    var weapon = ped.Inventory.EquippedWeaponObject;

                    if (weapon && weapon.IsVisible)
                    {
                        var weaponBox = new WeaponBoundingBox(weapon);
                        if (weaponBox.ShouldDraw(camera))
                        {
                            weaponBox.ToBoundingRect().Draw(graphics, Color.Red);
                        }
                    }
                }
            }
        }
    }
}
