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
using DatasetGenerator.ScenarioCreation;
using Rage;
using Rage.Attributes;
using Rage.Native;
using Graphics = System.Drawing.Graphics;


[assembly: Rage.Attributes.Plugin("Dataset Generator", Description = "This plugin is used to generate a dataset for object detection training.", Author = "emeloni")]

namespace DatasetGenerator
{
    public class EntryPoint
    {
        private static List<Component> Components;
        public static void Main()
        {
            Game.DisplaySubtitle("Dataset generator loaded");
            var localPlayer = Game.LocalPlayer;
            localPlayer.Character.IsInvincible = true;
            localPlayer.IsIgnoredByEveryone = true;
            localPlayer.IsIgnoredByPolice = true;

            Components = new List<Component>
            {
                new DatasetAnnotator(),
                new ScenarioCreator(),
                new PlayerFly()
            };
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
