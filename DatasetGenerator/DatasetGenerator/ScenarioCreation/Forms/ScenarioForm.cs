using System;
using System.IO;
using Gwen.Control;
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
        private Button clearButton;
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
                    Scenario.FromJson(fileStream.ReadToEnd());
                    //GameFiber.StartNew(delegate { Scenario.Apply(); });
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
            clearButton.Clicked += clearButton_click;
        }

        private void clearButton_click(Base sender, ClickedEventArgs arguments)
        {
            Scenario.Clear();
            confirmationLabel.Text = "Cleared";
        }
    }
}
