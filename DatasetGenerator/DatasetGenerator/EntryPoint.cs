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
using DatasetGenerator.PedTypes;
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


            var scenario = new Scenario();

            Components = new List<Component>
            {
                new DatasetAnnotator(scenario),
                new ScenarioCreator(scenario),
                new PlayerFly()
            };
        }


        public static Ped SpawnedPed = null;
        [ConsoleCommand]
        private static void Command_SpawnPed(int type, int component, int drawable, int texture)
        {
            if(SpawnedPed != null)
                SpawnedPed.Delete();
            var pedType = PedType.PedTypes[type];
            var ped = new Ped(pedType.GetModel(), Game.LocalPlayer.Character.Position + 2f* Game.LocalPlayer.Character.ForwardVector, 0);

            //let the game choose a random variation. Choose random props instead

            ped.SetPropIndex((PropComponentIds)component, drawable, texture);
            //ped.SetVariation(component, drawable, texture);
            SpawnedPed = ped;
        }

        [ConsoleCommand]
        private static void Command_GetPropDrawableVariations(int component)
        {
            Game.Console.Print(SpawnedPed.GetPropDrawableVariations(component).ToString());
        }

        [ConsoleCommand]
        private static void Command_GetPropTextureVariations(int component, int drawable)
        {
            Game.Console.Print(SpawnedPed.GetPropTextureVariations(component, drawable).ToString());
        }

        [ConsoleCommand]
        private static void Command_GetDrawableVariations(int component)
        {
            Game.Console.Print(SpawnedPed.GetDrawableVariationCount(component).ToString());
        }

        [ConsoleCommand]
        private static void Command_GetTextureVariations(int component, int drawable)
        {
            Game.Console.Print(SpawnedPed.GetTextureVariationCount(component, drawable).ToString());
        }
    }
}
