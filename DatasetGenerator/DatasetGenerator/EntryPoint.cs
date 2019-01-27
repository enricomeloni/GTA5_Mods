using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                GameFiber.Yield();
            }
        }

        {
            Game.DisplaySubtitle("Dataset generator loaded");
            Game.RawFrameRender += BoundingBoxGraphicHandler;

            GameFiber.Hibernate();
        }
    }
}
