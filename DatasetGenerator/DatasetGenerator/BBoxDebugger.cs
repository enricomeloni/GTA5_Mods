using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatasetGenerator.BoundingBoxes;
using Rage;
using Rage.Native;

namespace DatasetGenerator
{
    class BBoxDebugger : Component
    {
        bool active = true;
        private bool hudActive = true;
        List<Ped> peds = new List<Ped>();


        protected override void HandleKeyboardState()
        {
            if(Game.IsKeyDown(Keys.F11))
            {
                ToggleActive();
                //Game.DisplaySubtitle("Bounding Box debugging " + (active ? "active" : "disabled"));
            }

            if (Game.IsKeyDown(Keys.U))
            {
                Game.DisplaySubtitle("Spawning new ped");
                var ped = new Ped(Game.LocalPlayer.Character.FrontPosition);
                var wanderRadius = Utility.Randomize(0.5f);
                var wanderLength = Utility.Randomize(1f);
                var wanderTime = Utility.Randomize(2f, 0.8f);
                NativeFunction.Natives.TaskWanderInArea(
                    ped,
                    ped.Position,
                    wanderRadius,
                    wanderLength,
                    wanderTime);
                peds.Add(ped);
            }

            if (Game.IsKeyDown(Keys.Y))
            {
                ToggleHud();
            }
        }

        protected override void Main()
        {
            Game.DisplaySubtitle("Loaded BBox debugger");
            peds.Add(Game.LocalPlayer.Character);

            while (true)
            {
                HandleKeyboardState();

                if (active)
                    DrawBBoxes();
                
                GameFiber.Yield();
            }
        }

        private void ToggleActive()
        {
            active = !active;
        }

        private void ToggleHud()
        {
            hudActive = !hudActive;
            NativeFunction.Natives.DisplayHud(!hudActive);
            NativeFunction.Natives.DisplayRadar(!hudActive);
        }


        private void DrawBBoxes()
        {
            foreach (var ped in peds)
            {
                using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
                {
                    var camera = disposableCamera.Camera;
                    var headBox = new HeadBoundingBox(ped);
                    if (headBox.ShouldDraw(camera))
                    {
                        headBox.DebugDraw3D(Color.Red);
                    }

                    var chestBox = new ChestBoundingBox(ped);
                    if (chestBox.ShouldDraw(camera))
                    {
                        chestBox.DebugDraw3D(Color.Blue);
                    }

                    var pedBox = new PedBoundingBox(ped);

                    if (pedBox.ShouldDraw(camera))
                    {
                        pedBox.DebugDraw3D(Color.Green);
                    }

                    var weapon = ped.Inventory.EquippedWeaponObject;
                    if (weapon && weapon.IsVisible)
                    {
                        var weaponBox = new WeaponBoundingBox(weapon);
                        if (weaponBox.ShouldDraw(camera))
                        {
                            weaponBox.DebugDraw3D(Color.Purple);
                        }
                    }
                }
            }
        }
    }
}
