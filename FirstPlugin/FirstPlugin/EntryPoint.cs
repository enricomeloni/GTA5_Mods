using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Rage.Attributes;

[assembly: Rage.Attributes.Plugin("My First Plugin", Description = "This is my first plugin.", Author = "emeloni")]

namespace FirstPlugin
{
    public class EntryPoint
    {
        public static void Main()
        {
            Game.DisplayNotification("Loaded plugin");

            while (true)
            {
                var ped = Game.LocalPlayer.Character;
                Weapon wp = ped.Inventory.EquippedWeaponObject;
                if (wp)
                {
                    //The vector obtained are offset relative to the origin of the model. 
                    //But the origin is not the center of the model
                    Vector3 weaponBottomLeft;
                    Vector3 weaponTopRight;
                    wp.Model.GetDimensions(out weaponBottomLeft, out weaponTopRight);

                    //Compute the size of the three sides of the box
                    Vector3 size = (weaponTopRight - weaponBottomLeft);

                    //Compute the offset of the weapon center relative to the origin of the model
                    Vector3 centerOffset = (weaponTopRight + weaponBottomLeft) / 2.0f;

                    //Now we must rotate the center offset computed on the model, to match the entity orientation
                    var rotatedCenterOffset = Transform(centerOffset, wp.Orientation);

                    //Show the entity origin
                    Debug.DrawSphere(wp.Position, 0.025f, Color.Blue);

                    //Show the bounding box
                    Debug.DrawWireBox(wp.Position + rotatedCenterOffset, wp.Orientation, size, Color.Red);
                }

                GameFiber.Yield();
            }
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
            Game.DisplayNotification("Here you go bro");
        }
    }
}
