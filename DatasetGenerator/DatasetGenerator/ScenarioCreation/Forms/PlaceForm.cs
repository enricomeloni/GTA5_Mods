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
        private Button currentPositionButton;
        private TextBox positionXtextBox;
        private TextBox positionYtextBox;
        private TextBox positionZtextBox;
        private Label basePositionLabel;

        public PlaceForm(Scenario scenario) : base(typeof(PlaceFormTemplate))
        {
            Scenario = scenario;
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();

            placeComboBox.AddItem("CUSTOM", "CUSTOM");

            foreach (var place in Place.Places)
            {
                placeComboBox.AddItem(place.Name, place.Name, place);
            }

            if (Scenario.PlaceSettings.Place != null)
            {

                placeComboBox.SelectByName(Scenario.PlaceSettings.Place.Name);
                PrintPositionToTextBoxes(Scenario.PlaceSettings.Place.Position);
            }

            teleportButton.Disable();

            positionXtextBox.Disable();
            positionYtextBox.Disable();
            positionZtextBox.Disable();


            placeComboBox.ItemSelected += placeComboBox_itemSelected;
            teleportButton.Clicked += teleportButton_clicked;
            currentPositionButton.Clicked += currentPositionButton_clicked;
        }

        private void currentPositionButton_clicked(Base sender, ClickedEventArgs arguments)
        {
            var characterPosition = Game.LocalPlayer.Character.Position;
            Scenario.PlaceSettings.Place = new Place("CUSTOM", characterPosition);

            PrintPositionToTextBoxes(characterPosition);

            placeComboBox.SelectByName("CUSTOM");
            teleportButton.Enable();
        }

        private void PrintPositionToTextBoxes(Vector3 characterPosition)
        {
            positionXtextBox.Text = $"{characterPosition.X:F3}";
            positionYtextBox.Text = $"{characterPosition.Y:F3}";
            positionZtextBox.Text = $"{characterPosition.Z:F3}";
        }

        private void teleportButton_clicked(Base sender, ClickedEventArgs arguments)
        {
            if (teleportButton.IsDisabled)
                return;
            var selectedPlace = Scenario.PlaceSettings.Place;
            //Scenario.PlaceSettings.Place = selectedPlace;
            GameFiber.StartNew(delegate { Scenario.PlaceSettings.Apply(); });
            Window.Close();
        }

        private void placeComboBox_itemSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            teleportButton.Enable();
            var selectedPlace = (Place) arguments.SelectedItem.UserData;
            if (selectedPlace != null) //if CUSTOM is not selected
                Scenario.PlaceSettings.Place = selectedPlace;
        }
    }
}
