using System;
using Gwen.Control;
using Rage;
using Rage.Forms;

namespace DatasetGenerator.ScenarioCreation.Forms
{
    class TimeForm : GwenForm
    {
        private Scenario Scenario { get; set; }

        private TextBox hourTextbox;
        private TextBox minuteTextbox;
        private Label hourLabel;
        private Label minuteLabel;


        public TimeForm(Scenario scenario) : base(typeof(TimeFormTemplate))
        {
            Scenario = scenario;
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();

            //var dayTime = World.TimeOfDay;

            hourTextbox.Text = Scenario.TimeSettings.Hour.ToString();
            minuteTextbox.Text = Scenario.TimeSettings.Minute.ToString();

            hourTextbox.SubmitPressed += hourTextbox_submit;
            minuteTextbox.SubmitPressed += minuteTextbox_submit;
        }

        private void hourTextbox_submit(Base sender, EventArgs arguments)
        {
            if (int.TryParse(hourTextbox.Text, out var hour))
            {
                Scenario.TimeSettings.Hour = hour;
                UpdateTime();
                hourLabel.Text = "Hour (ok)";
            }
            else
            {
                hourLabel.Text = "Hour (invalid)";
            }
        }

        private void minuteTextbox_submit(Base sender, EventArgs arguments)
        {
            if (int.TryParse(minuteTextbox.Text, out var minute))
            {
                Scenario.TimeSettings.Minute = minute;
                UpdateTime();
                minuteLabel.Text = "Minute (ok)";
            }
            else
            {
                minuteLabel.Text = "Minute (invalid)";
            }
        }

        private void UpdateTime()
        {
            GameFiber.StartNew(delegate { Scenario.TimeSettings.Apply(); });
        }
    }
}
