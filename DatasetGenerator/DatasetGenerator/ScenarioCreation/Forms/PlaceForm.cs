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
    class PlaceForm : GwenForm
    {
        private Scenario Scenario { get; set; }

        private ComboBox placeComboBox;
        private Button teleportButton;

        public PlaceForm(Scenario scenario) : base(typeof(PlaceFormTemplate))
        {
            Scenario = scenario;
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();

            foreach (var place in Place.Places)
            {
                placeComboBox.AddItem(place.Name, place.Name, place);
            }

            teleportButton.Disable();

            placeComboBox.ItemSelected += placeComboBox_itemSelected;
            teleportButton.Clicked += teleportButton_clicked;
        }

        private void teleportButton_clicked(Base sender, ClickedEventArgs arguments)
        {
            var selectedPlace = (Place)placeComboBox.SelectedItem.UserData;
            GameFiber.StartNew(delegate { Game.LocalPlayer.Character.Position = selectedPlace.Position; });
            this.Window.Close();
        }

        private void placeComboBox_itemSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            teleportButton.Enable();
        }




    }
}
