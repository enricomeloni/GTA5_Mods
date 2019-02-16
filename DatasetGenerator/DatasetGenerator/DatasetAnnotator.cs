﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rage;

namespace DatasetGenerator
{
    class DatasetAnnotator : Component
    {

        private readonly List<Ped> spawnedPeds = new List<Ped>();
        private readonly FrameRenderHandler FrameRenderHandler = new FrameRenderHandler();

        

        protected override void Main()
        {
            while (true)
            {
                Game.LocalPlayer.WantedLevel = 0;
                Game.LocalPlayer.IsIgnoredByEveryone = true;
                Game.LocalPlayer.IsIgnoredByPolice = true;

                HandleKeyboardState();

                GameFiber.Yield();
            }
        }

        protected override void HandleKeyboardState()
        {
            if (Game.IsKeyDown(Keys.F9))
                Game.IsPaused = true;
            else if (Game.IsKeyDown(Keys.F10))
                Game.IsPaused = false;

            if (Game.IsKeyDown(Keys.F6))
            {
                FrameRenderHandler.StartRecording();
                Game.DisplaySubtitle("Start recording");
            }

            if (Game.IsKeyDown(Keys.F7))
            {
                FrameRenderHandler.StopRecording();
                Game.IsPaused = false;
                Game.DisplaySubtitle("Stop recording");
            }

            if (Game.IsKeyDown(Keys.F11))
            {
                var me = Game.LocalPlayer.Character;
                Vector3 pedPosition = me.LeftPosition - me.RightVector * 5 + me.ForwardVector * 2;

                var ped = PedSpawner.SpawnNewPed(pedPosition);
                ped.Tasks.Wander();
                spawnedPeds.Add(ped);
            }

            if (Game.IsKeyDown(Keys.F12))
            {
                foreach (var spawnedPed in spawnedPeds)
                {
                    spawnedPed.Delete();
                }

                spawnedPeds.Clear();
            }
        }
    }
}
