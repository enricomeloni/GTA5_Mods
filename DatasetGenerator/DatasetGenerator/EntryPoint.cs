using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DatasetGenerator.BoundingBoxes;
using DatasetGenerator.Logging;
using DatasetGenerator.PedClassifiers;
using DatasetGenerator.PedTypes;
using DatasetGenerator.ScenarioCreation;
using Rage;
using Rage.Attributes;


[assembly: Rage.Attributes.Plugin("Dataset Generator", Description = "This plugin is used to generate a dataset for object detection training.", Author = "emeloni", PrefersSingleInstance = true)]

namespace DatasetGenerator
{
    public class EntryPoint
    {
        private static List<Component> Components;
        private static readonly Logger Log = Logger.GetLogger(typeof(EntryPoint));



        public static void Main()
        {
            Log.Info("-----------------------------------------------------");
            Log.Info("Loaded Dataset Generator");
            var localPlayer = Game.LocalPlayer;
            localPlayer.Character.IsInvincible = true;
            localPlayer.IsIgnoredByEveryone = true;
            localPlayer.IsIgnoredByPolice = true;

            var scenario = new Scenario();

            Components = new List<Component>
            {
                new DatasetAnnotator(scenario),
                new ScenarioCreator(scenario),
                new PlayerFly(),
                new BBoxDebugger()
            };

            //Game.FrameRender += DebugGraphicHandler;
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

            //ped.SetPropIndex((PropComponentIds)component, drawable, texture);
            ped.SetVariation(component, drawable, texture);
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

        private static void Command_TestProjection()
        {
            var size = Game.Resolution;
            for (int i = 0; i < size.Width; ++i)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    var vector = new Vector2(i,j);
                }
            }
        }

        [ConsoleCommand]
        private static void Command_TestReadFiles()
        {
            DirectoryInfo RootScenariosDirectory = new DirectoryInfo("F:/scenarios");
            var Scenarios = new Queue<FileInfo>(RootScenariosDirectory.EnumerateFiles().Where(file => file.Name.EndsWith(".json")));

            foreach(var scenario in Scenarios)
            {
                Game.Console.Print(scenario.Name);
            }

        }

        public static void DebugGraphicHandler(object sender, GraphicsEventArgs e)
        {
            var me = Game.LocalPlayer.Character;

            var nearbyPeds = new List<Ped>(me.GetNearbyPeds(5)) {me};


            using (var disposableCamera = new DisposableCamera(DisposableCamera.DefaultScriptedCamera))
            {
                var camera = disposableCamera.Camera;
                camera.SetCameraValues(Utility.GetGameplayCameraValues());
                
                foreach (var ped in nearbyPeds)
                {
                    var pedBox = new PedBoundingBox(ped);
                    if (pedBox.ShouldDraw(camera))
                    {
                        pedBox.Draw(e.Graphics, Color.Red);
                        pedBox.ToDetectedObject()?.BoundingRect?.Draw(e.Graphics, Color.Blue);
                    }
                }
            }
        }
    }
}
