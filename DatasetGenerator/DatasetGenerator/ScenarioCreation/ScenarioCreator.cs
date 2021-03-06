﻿using DatasetGenerator.ScenarioCreation.Forms;
using Rage;
using Rage.Forms;
using Keys = System.Windows.Forms.Keys;

namespace DatasetGenerator.ScenarioCreation
{
    public class ScenarioCreator : Component
    {
        private Scenario Scenario { get; set; } = new Scenario();

        public ScenarioCreator(Scenario scenario)
        {
            Scenario = scenario;
        }

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

        public bool CameraLocked => Camera == null;
        private Camera Camera { get; set; }
        protected override void HandleKeyboardState()
        {
            if (Game.IsKeyDown(Keys.O))
            {
                var gameplayCameraValues = Utility.GetGameplayCameraValues();
                Scenario.CameraSettings.Cameras.Add(gameplayCameraValues);
            }

            if (Game.IsKeyDown(Keys.K))
            {
                var gwenForm = new WeatherForm(Scenario);
                ShowForm(gwenForm);
            }

            if (Game.IsKeyDown(Keys.J))
            {
                Camera.DeleteAllCameras();
            }

            if (Game.IsKeyDown(Keys.NumPad7))
            {
                var gwenForm = new ScenarioForm(Scenario, DatasetAnnotator.RootScenariosDirectory);
                ShowForm(gwenForm);
            }
            if (Game.IsKeyDown(Keys.NumPad9))
            {
                var gwenForm = new CameraForm(Scenario);
                ShowForm(gwenForm);
            }

            if (Game.IsKeyDown(Keys.NumPad1))
            {
                var gwenForm = new PedsForm(Scenario);
                ShowForm(gwenForm);
            }

            if (Game.IsKeyDown(Keys.NumPad3))
            {
                var gwenForm = new PlaceForm(Scenario);
                ShowForm(gwenForm);
            }

            if (Game.IsKeyDown(Keys.NumPad4))
            {
                var gwenForm = new TimeForm(Scenario);
                ShowForm(gwenForm);
            }
        }

        private void ShowForm(GwenForm gwenForm)
        {
            Game.IsPaused = true;
            gwenForm.Show();

            var screenSize = Game.Resolution;
            var formSize = gwenForm.Size;

            var formX = screenSize.Width / 2 - formSize.Width / 2;
            var formY = screenSize.Height / 3 - formSize.Height / 2;

            gwenForm.Position = new System.Drawing.Point(formX, formY);

            while (gwenForm.Window.IsVisible)
            {
                GameFiber.Yield();
            }

            Game.IsPaused = false;
        }
    }
}
