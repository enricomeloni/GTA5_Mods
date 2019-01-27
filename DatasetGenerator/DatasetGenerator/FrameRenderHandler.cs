using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetGenerator.BoundingBoxes;
using Rage;

namespace DatasetGenerator
{
    class FrameRenderHandler
    {
        public static void BoundingBoxGraphicHandler(object sender, GraphicsEventArgs e)
        {

            var graphics = e.Graphics;
            
            var ped = Game.LocalPlayer.Character;
            Weapon wp = ped.Inventory.EquippedWeaponObject;


            //Vector2 spine2 = World.ConvertWorldPositionToScreenPosition(ped.GetBonePosition(PedBoneId.Spine2));
            //graphics.DrawFilledCircle(spine2, 5, Color.Orange);

            BoundingBox headBox = BoundingBox.FromBone(ped, PedBoneId.Head);
            BoundingRect headRect = headBox.ToBoundingRect();
            BoundsDrawer.DrawBoundingRect(headRect, graphics);

            if (wp && wp.IsVisible)
            {
                BoundingBox bb = BoundingBox.FromWeapon(wp);
                BoundingRect br = bb.ToBoundingRect();

                //BoundsDrawer.DrawBoundingBox(bb, graphics);
                BoundsDrawer.DrawBoundingRect(br, graphics);
            }

            BoundingBox chestBox = BoundingBox.FromChest(ped);
            //BoundsDrawer.DrawBoundingBox(chestBox, graphics);
            BoundsDrawer.DrawBoundingRect(chestBox.ToBoundingRect(), graphics);

            BoundingBox pedBox = BoundingBox.FromPed(ped);
            //BoundsDrawer.DrawBoundingBox(pedBox, graphics);

        }
    }
}
