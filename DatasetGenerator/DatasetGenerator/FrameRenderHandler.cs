using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DatasetGenerator.BoundingBoxes;
using DatasetGenerator.PedClassifiers;
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
            var me = Game.LocalPlayer.Character;

            var nearbyPeds = new List<Ped>(me.GetNearbyPeds(5));

            //nearbyPeds = new List<Ped>();
            //nearbyPeds.Add(me);

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
                        Color color;
                        switch (detectedHead.ObjectClass)
                        {
                            case ObjectClass.BareHead:
                                color = Color.Blue;
                                break;
                            case ObjectClass.HeadWithHelmet:
                                color = Color.Red;
                                break;
                            case ObjectClass.HeadWithFaceShield:
                                color = Color.Aqua;
                                break;
                            case ObjectClass.HeadWithHearingProtection:
                                color = Color.BlueViolet;
                                break;
                            default:
                                color = Color.Black;
                                break;
                        }

                        detectedHead.BoundingRect.Draw(graphics, color);
                    }

                    var chestBox = new ChestBoundingBox(ped);
                    if (chestBox.ShouldDraw(camera))
                    {
                        var detectedChest = chestBox.ToDetectedObject();
                        
                        Color color;
                        if (detectedChest.ObjectClass == ObjectClass.BareChest)
                            color = Color.Blue;
                        else
                        {
                            color = Color.BlueViolet;
                        }

                        detectedChest.BoundingRect.Draw(graphics, color);
                    }

                    var weapon = ped.Inventory.EquippedWeaponObject;

                    if (weapon && weapon.IsVisible)
                    {
                        var weaponBox = new WeaponBoundingBox(weapon);
                        if (weaponBox.ShouldDraw(camera))
                        {
                            var detectedWeapon = weaponBox.ToDetectedObject();
                            detectedWeapon.BoundingRect.Draw(graphics, Color.Red);
                        }
                    }
                }
            }
        }
    }
}
