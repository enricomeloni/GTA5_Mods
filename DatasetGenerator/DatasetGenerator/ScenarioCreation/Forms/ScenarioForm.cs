﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwen.Control;
using Newtonsoft.Json;
using Rage.Forms;

namespace DatasetGenerator.ScenarioCreation.Forms
{
    class ScenarioForm : GwenForm
    {
        private Scenario Scenario { get; set; }
        private DirectoryInfo DefaultScenariosPath { get; set; }
        private TextBox scenariosPathTextbox;
        private Button saveScenarioButton;
        private Button loadScenarioButton;
        private Label scenariosPathLabel;
        private Label confirmationLabel;

        public ScenarioForm(Scenario scenario, DirectoryInfo scenariosDefaultPath) : base(typeof(ScenarioFormTemplate))
        {
            Scenario = scenario;
            DefaultScenariosPath = scenariosDefaultPath;
        }

        private void saveScenarioButton_click(Base sender, ClickedEventArgs e)
        {
            using (var fileStream = File.CreateText(scenariosPathTextbox.Text))
            {
                fileStream.WriteLine(Scenario.ToJson());
            }
            confirmationLabel.Text = "Saved!";
        }

        private void loadScenarioButton_click(Base sender, ClickedEventArgs e)
        {
            try
            {
                using (var fileStream = File.OpenText(scenariosPathTextbox.Text))
                {
                    Scenario = JsonConvert.DeserializeObject<Scenario>(fileStream.ReadToEnd());
                }

                confirmationLabel.Text = "Loaded!";
            }
            catch (Exception ex)
            {
                confirmationLabel.Text = ex.Message;
            }

        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();
            scenariosPathTextbox.Text = DefaultScenariosPath.FullName;

            saveScenarioButton.Clicked += saveScenarioButton_click;
            loadScenarioButton.Clicked += loadScenarioButton_click;
        }
    }
}
