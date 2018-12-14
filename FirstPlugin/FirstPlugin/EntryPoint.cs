using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Rage;
using Rage.Attributes;
using Graphics = Rage.Graphics;

[assembly: Rage.Attributes.Plugin("My First Plugin", Description = "This is my first plugin.", Author = "emeloni")]

namespace FirstPlugin
{
    public class EntryPoint
    {
        private const float BoundingBoxScaleFactor = 1.05f;

        static void BoundingBoxGraphicHandler(object sender, GraphicsEventArgs e)
        {
            var ped = Game.LocalPlayer.Character;
            Weapon wp = ped.Inventory.EquippedWeaponObject;

            var graphics = e.Graphics;

            if (wp)
            {
                wp.Model.GetDimensions(out var weaponBottomLeft, out var weaponTopRight);

                //Compute the size of the three sides of the box
                Vector3 size = (weaponTopRight - weaponBottomLeft)*BoundingBoxScaleFactor;

                //Compute the offset of the weapon center relative to the origin of the model
                Vector3 centerOffset = (weaponTopRight + weaponBottomLeft) / 2.0f;

                //Now we must rotate the center offset computed on the model, to match the entity orientation
                var rotatedCenterOffset = Transform(centerOffset, wp.Orientation);
                
                var wireBoxCenter = wp.Position + rotatedCenterOffset;

                //We compute the wirebox, by computing the edges starting from the wireboxCenter

                var coefficients = new List<int> {-1, 1};
                var wireboxEdges = new List<Vector3>();

                /*
                 * The wirebox edges can be computed by summing or subtracting half the size of the wirebox
                 * We have 8 possible combinations, (-1,-1,-1), (-1,-1,+1) etc
                 */
                foreach (var coefficientX in coefficients)
                {
                    foreach (var coefficientY in coefficients)
                    {
                        foreach (var coefficientZ in coefficients)
                        {
                            //this is an offset from the wirebox center, not yet rotated to match the entity orientation
                            Vector3 edgeOffsetFromWireBoxCenter = new Vector3(
                                size.X / 2f * coefficientX,
                                size.Y / 2f * coefficientY, 
                                size.Z / 2f * coefficientZ
                            );

                            //rotate the offset to match the entity orientation
                            Vector3 rotatedEdgeOffset = Transform(edgeOffsetFromWireBoxCenter, wp.Orientation);
                            var edge = wireBoxCenter + rotatedEdgeOffset;
                            wireboxEdges.Add(edge);
                        }
                    }
                }

                /*
                 * Then we compute the BoundingRect by projecting the BoundingBox on the screen
                 * To always be sure of bounding the full weapon, we take the min and max coordinate for each axis
                 */

                //Pay attention: screen position is not accurate if the game is not fullscreen (it ignores the title bar)
                var screenProjectedEdges = wireboxEdges.Select(World.ConvertWorldPositionToScreenPosition).ToList();

                //max/min x and max/min y can be used to compute the BoundingRect size
                var maxX = screenProjectedEdges.Max(edge => edge.X);
                var maxY = screenProjectedEdges.Max(edge => edge.Y);
                var minX = screenProjectedEdges.Min(edge => edge.X);
                var minY = screenProjectedEdges.Min(edge => edge.Y);

                var rectEdges = new List<Vector2>
                {
                    new Vector2(minX, maxY),
                    new Vector2(minX, minY),
                    new Vector2(maxX, minY),
                    new Vector2(maxX, maxY)
                };
                
                graphics.DrawLine(rectEdges[0], rectEdges[1], Color.Red);
                graphics.DrawLine(rectEdges[1], rectEdges[2], Color.Red);
                graphics.DrawLine(rectEdges[2], rectEdges[3], Color.Red);
                graphics.DrawLine(rectEdges[3], rectEdges[0], Color.Red);    
            }
        }


        public static void Main()
        {
            Game.DisplayNotification("Loaded plugin");

            Game.RawFrameRender += BoundingBoxGraphicHandler;

            GameFiber.Hibernate();

        }

        //Vector3.Transform returns always a Vector4, whose fourth component can be safely ignored
        private static Vector3 Transform(Vector3 vector, Quaternion orientation)
        {
            Vector4 transformedVector4 = Vector3.Transform(vector, orientation);
            return new Vector3(transformedVector4.X, transformedVector4.Y, transformedVector4.Z);
        }

        [ConsoleCommand]
        private static void Command_AddWeapons()
        {
            Ped me = Game.LocalPlayer.Character;

            List<String> weapons = new List<string>
            {
                "WEAPON_PUMPSHOTGUN",
                "WEAPON_APPISTOL",
                "WEAPON_BAT",
                "WEAPON_SNIPERRIFLE",
                "WEAPON_GRENADE"
            };

            foreach(var weapon in weapons)
            {
                me.Inventory.GiveNewWeapon(weapon, 100, false);
            }
        }

        [ConsoleCommand]
        private static void Command_ClearWantedState()
        {
            Ped me = Game.LocalPlayer.Character;

            Game.LocalPlayer.WantedLevel = 0;
            Game.DisplayHelp("Here you go bro");
        }
    }
}
