using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwen.Control;
using Rage;
using Rage.Forms;

namespace DatasetGenerator.ScenarioCreation.Forms
{
    class WeatherForm : GwenForm
    {
        private Scenario Scenario { get; set; }

        private ComboBox weatherCombobox;

        public WeatherForm(Scenario scenario) : base(typeof(WeatherFormTemplate))
        {
            Scenario = scenario;
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();

            foreach (var weather in Enum.GetValues(typeof(WeatherType)))
            {
                var name = Enum.GetName(typeof(WeatherType), weather);
                weatherCombobox.AddItem(name, name, weather);
            }

            weatherCombobox.SelectByUserData(Scenario.WeatherSettings.Weather);

            weatherCombobox.ItemSelected += weatherCombobox_itemSelected;
        }

        private void weatherCombobox_itemSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            Scenario.WeatherSettings.Weather = (WeatherType)arguments.SelectedItem.UserData;
            GameFiber.StartNew(delegate { Scenario.WeatherSettings.Apply(); });
        }
    }
}
