using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rage;
using Rage.Attributes;
using Rage.Native;


[assembly: Rage.Attributes.Plugin("Dataset Generator", Description = "This plugin is used to generate a dataset for object detection training.", Author = "emeloni")]

namespace DatasetGenerator
{
    public class EntryPoint
    {
        public static void Main()
        {
            Game.DisplaySubtitle("Dataset generator loaded");
            Game.FrameRender += FrameRenderHandler.BoundingBoxGraphicHandler;

            while (true)
            {
                var keyboardState = Game.GetKeyboardState();

                if (keyboardState.PressedKeys.Contains(Keys.F9))
                    Game.IsPaused = true;
                else if (keyboardState.PressedKeys.Contains(Keys.F10))
                    Game.IsPaused = false;

                Camera camera = null;

                /*var disposableCamera = new DisposableCamera("DEFAULT_SCRIPTED_CAMERA");

                var camera = disposableCamera.Camera;
                var cameraValues = Utility.GetGameplayCameraValues();

                cameraValues.Position += new Vector3(5, 5, 0);


                camera.SetCameraValues(cameraValues);
                */

                if (keyboardState.PressedKeys.Contains(Keys.F6))
                {
                    /*Game.DisplaySubtitle("Pressing f6");

                    if (camera == null)
                    {
                        var disposableCamera = new DisposableCamera("DEFAULT_SCRIPTED_CAMERA");
                        camera = disposableCamera.Camera;
                        var cameraValues = Utility.GetGameplayCameraValues();

                        cameraValues.Position += new Vector3(5, 5, 0);


                        camera.SetCameraValues(cameraValues);
                    }

                    if (camera.IsValid())
                    {
                        camera.Active = true;
                    }

                    //camera.Shake(1000);
                    */
                }

                if (keyboardState.PressedKeys.Contains(Keys.F7))
                {
                    /*if (camera != null && camera.IsValid())
                    {
                        camera.Active = false;
                    }
                    */
                }

                if (keyboardState.PressedKeys.Contains(Keys.F11))
                {
                    Model workModel = new Model("s_m_y_airworker");
                    Vector3 pedPosition = Game.LocalPlayer.Character.FrontPosition;
                    var ped = new Ped(workModel, pedPosition, 0);
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
