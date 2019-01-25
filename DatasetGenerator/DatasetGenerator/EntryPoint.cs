using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;


[assembly: Rage.Attributes.Plugin("Dataset Generator", Description = "This plugin is used to generate a dataset for object detection training.", Author = "emeloni")]

namespace DatasetGenerator
{
    public class EntryPoint
    {

        static void BoundingBoxGraphicHandler(object sender, GraphicsEventArgs e)
        {
            var ped = Game.LocalPlayer.Character;
            Weapon wp = ped.Inventory.EquippedWeaponObject;

            var graphics = e.Graphics;

            if (wp)
            {
                BoundingBox bb = BoundingBox.FromWeapon(wp);
                BoundingRect br = bb.ToBoundingRect();

                BoundsDrawer.DrawBoundingBox(bb, graphics);
                BoundsDrawer.DrawBoundingRect(br, graphics);
            }
        }

        public static void Main()
        {
            Game.DisplaySubtitle("Dataset generator loaded");
            Game.RawFrameRender += BoundingBoxGraphicHandler;

            GameFiber.Hibernate();
        }
    }
}
