using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatasetGenerator.PedClassifiers;
using Rage;
using Rage.Attributes;
using Rage.Native;
using Graphics = System.Drawing.Graphics;


[assembly: Rage.Attributes.Plugin("Dataset Generator", Description = "This plugin is used to generate a dataset for object detection training.", Author = "emeloni")]

namespace DatasetGenerator
{
    public class EntryPoint
    {
        private static List<Ped> spawnedPeds = new List<Ped>();
        public static void Main()
        {
            Game.DisplaySubtitle("Dataset generator loaded");
            Game.LocalPlayer.IsIgnoredByEveryone = true;
            Game.LocalPlayer.IsIgnoredByPolice = true;

            Game.FrameRender += FrameRenderHandler.BoundingBoxGraphicHandler;
            Game.FrameRender += FrameRenderHandler.DebugGraphicHandler;

            while (true)
            {
                Game.LocalPlayer.WantedLevel = 0;

                var keyboardState = Game.GetKeyboardState();

                if (keyboardState.PressedKeys.Contains(Keys.F9))
                    Game.IsPaused = true;
                else if (keyboardState.PressedKeys.Contains(Keys.F10))
                    Game.IsPaused = false;
                
                if (keyboardState.PressedKeys.Contains(Keys.F6))
                {
                    FrameRenderHandler.StartRecording();
                    Game.DisplaySubtitle("Start recording");
                }

                if (keyboardState.PressedKeys.Contains(Keys.F7))
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

                GameFiber.Yield();
            }
        }


        [ConsoleCommand]
        private static void Command_GetDress(int variation)
        {
            Ped ped = Game.LocalPlayer.Character;

            int drawableIndex;
            int textureIndex;
            ped.GetVariation(variation, out drawableIndex, out textureIndex);

            Game.Console.Print($"Variation is: {drawableIndex}, {textureIndex}");
        }

        [ConsoleCommand]
        private static void Command_SetDress(int componentId, int drawableIndex, int textureIndex)
        {

            Ped ped = Game.LocalPlayer.Character;
            ped.SetVariation(componentId, drawableIndex, textureIndex);

            ped.SetVariation(componentId, drawableIndex, textureIndex);
        }

        [ConsoleCommand]
        private static void Command_GetProp(int componentId)
        {
            Ped ped = Game.LocalPlayer.Character;
            int propIndex = NativeFunction.Natives.GetPedPropIndex<int>(ped, componentId);
            Game.Console.Print($"Prop attached is: {propIndex}");
        }

        [ConsoleCommand]
        private static void Command_SetProp(int componentId, int drawableId, int textureId)
        {
            Ped ped = Game.LocalPlayer.Character;
            int propIndex = NativeFunction.Natives.SetPedPropIndex<int>(ped, componentId, drawableId, textureId, true);
        }

        [ConsoleCommand]
        private static void Command_Project()
        {
            float vector_x;
            float vector_y;

            var vector3 = Game.LocalPlayer.Character.Position;

            NativeFunction.Natives.xF9904D11F1ACBEC3(vector3.X, vector3.Y, vector3.Z, out vector_x, out vector_y);

            Game.DisplaySubtitle($"Projected is ({vector_x},{vector_y})");
        }

    }
}
