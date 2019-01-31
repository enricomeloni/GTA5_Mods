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
            var wp = me.Inventory.EquippedWeaponObject;

            /*using (var disposableCamera = new DisposableCamera("DEFAULT_SCRIPTED_CAMERA"))
            {
                var camera = disposableCamera.Camera;

                camera.Position = cameraValues.Position;
                camera.Rotation = cameraValues.Rotation;
                
            }*/

            if (wp && wp.IsVisible)
            {
                var wpBox = BoundingBox.FromWeapon(wp);

                using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
                {
                    var camera = disposableCamera.Camera;
                    camera.SetCameraValues(Utility.GetGameplayCameraValues());

                    if (wpBox.ShouldDraw(camera))
                    {
                        wpBox.ToBoundingRect().Draw(graphics, Color.Red);
                    }
                }
                
            }


        }
    }
}
