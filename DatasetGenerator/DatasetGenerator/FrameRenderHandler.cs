using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            
            var me = Game.LocalPlayer.Character;
            


            List<Ped> nearbyPeds = new List<Ped>(me.GetNearbyPeds(16));
            nearbyPeds.Add(me);

            //Vector2 spine2 = World.ConvertWorldPositionToScreenPosition(ped.GetBonePosition(PedBoneId.Spine2));
            //graphics.DrawFilledCircle(spine2, 5, Color.Orange);

            foreach (var ped in nearbyPeds)
            {

                BoundingBox headBox = BoundingBox.FromHead(ped);
                BoundingRect headRect = headBox.ToBoundingRect();
                BoundsDrawer.DrawBoundingRect(headRect, graphics);
                Weapon wp = ped.Inventory.EquippedWeaponObject;
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
}
