using System;
using Gwen.Control;
using Rage.Forms;

namespace DatasetGenerator.ScenarioCreation.Forms
{
    class PedsForm : GwenForm
    {
        private Scenario Scenario { get; set; }

        private ComboBox pedsBehaviorCombobox;
        private TextBox pedsNumberTextbox;
        private CheckBox pedsGroupCheckbox;
        private Label pedsNumberLabel;

        public PedsForm(Scenario scenario) : base(typeof(PedsFormTemplate))
        {
            Scenario = scenario;
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();

            foreach (var pedBehavior in Enum.GetValues(typeof(PedBehavior)))
            {
                var pedBehaviorName = Enum.GetName(typeof(PedBehavior), pedBehavior);
                pedsBehaviorCombobox.AddItem(pedBehaviorName, pedBehaviorName, pedBehavior);
            }

            var selectedPedBehavior = Enum.GetName(typeof(PedBehavior), Scenario.PedsSettings.PedBehavior);
            pedsBehaviorCombobox.SelectByName(selectedPedBehavior);

            pedsNumberTextbox.Text = Scenario.PedsSettings.PedsNumber.ToString();

            pedsGroupCheckbox.IsChecked = Scenario.PedsSettings.PedShouldGroup;

            pedsBehaviorCombobox.ItemSelected += pedsBehaviorCombobox_itemSelected;
            pedsGroupCheckbox.CheckChanged += pedsGroupCheckbox_checkChanged;
            pedsNumberTextbox.SubmitPressed += pedsNumberTextbox_submitPressed;
        }

        private void pedsNumberTextbox_submitPressed(Base sender, EventArgs arguments)
        {
            if(int.TryParse(pedsNumberTextbox.Text, out var pedsNumber))
            {
                Scenario.PedsSettings.PedsNumber = pedsNumber;
                pedsNumberLabel.Text = "Number of Peds (ok)";
            }
            else
            {
                pedsNumberLabel.Text = "Number of Peds (invalid)";
            }

            pedsNumberLabel.UpdateColors();
        }

        private void pedsGroupCheckbox_checkChanged(Base sender, EventArgs arguments)
        {
            Scenario.PedsSettings.PedShouldGroup = pedsGroupCheckbox.IsChecked;
        }

        private void pedsBehaviorCombobox_itemSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            var selectedItem = (MenuItem)arguments.SelectedItem;
            Scenario.PedsSettings.PedBehavior = (PedBehavior)selectedItem.UserData;
        }
    }
}
