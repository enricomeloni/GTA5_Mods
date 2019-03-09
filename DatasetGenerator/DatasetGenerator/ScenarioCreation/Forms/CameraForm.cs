using Gwen.Control;
using Rage;
using Rage.Forms;

namespace DatasetGenerator.ScenarioCreation.Forms
{
    class CameraForm : GwenForm
    {
        private Scenario Scenario { get; set; }
        private ListBox camerasListBox;
        private Button addCameraButton;
        private Button deleteCameraButton;
        private Button clearCamerasButton;
        private Button viewCameraButton;

        public CameraForm(Scenario scenario) : base(typeof(CameraFormTemplate))
        {
            Scenario = scenario;
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();

            UpdateCamerasListBox();

            clearCamerasButton.Clicked += clearCamerasButton_click;
            addCameraButton.Clicked += addCameraButton_click;
            camerasListBox.RowSelected += camerasListBox_rowSelected;
            deleteCameraButton.Clicked += deleteCameraButton_click;
            viewCameraButton.Clicked += viewCameraButton_click;
        }

        private void UpdateCamerasListBox()
        {
            camerasListBox.Clear();

            camerasListBox.AddRow("Gameplay camera", "gameplayCam", null);

            foreach (var (cameraValue, index) in Scenario.CameraSettings.Cameras.WithIndex())
            {
                string rowLabel = $"Camera {index}";
                string rowName = $"camera{index}";
                camerasListBox.AddRow(rowLabel, rowName, cameraValue);
            }

            if (camerasListBox.RowCount == 0)
            {
                deleteCameraButton.Disable();
                viewCameraButton.Disable();
            }
        }

        private void clearCamerasButton_click(Base sender, ClickedEventArgs arguments)
        {
            Scenario.CameraSettings.Cameras.Clear();
            UpdateCamerasListBox();
        }

        private void addCameraButton_click(Base sender, ClickedEventArgs arguments)
        {
            var gameplayCameraValues = Utility.GetGameplayCameraValues();
            Scenario.CameraSettings.Cameras.Add(gameplayCameraValues);
            UpdateCamerasListBox();
        }

        private void camerasListBox_rowSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            var selectedRow = (ListBoxRow) arguments.SelectedItem;

            if(selectedRow.Name != "gameplayCam")
                deleteCameraButton.Enable();

            viewCameraButton.Enable();
        }

        private void deleteCameraButton_click(Base sender, ClickedEventArgs arguments)
        {
            if (deleteCameraButton.IsDisabled)
                return;
            var selectedCamera = (CameraValues)camerasListBox.SelectedRow.UserData;
            Scenario.CameraSettings.Cameras.Remove(selectedCamera);
            UpdateCamerasListBox();
        }

        private void viewCameraButton_click(Base sender, ClickedEventArgs arguments)
        {
            if (viewCameraButton.IsDisabled)
                return;

            if (camerasListBox.SelectedRow.Name == "gameplayCam")
            {
                if(Camera.RenderingCamera != null)
                    Camera.RenderingCamera.Delete();
            }
            else
            {
                var camera = new Camera(DisposableCamera.DefaultScriptedCamera, false);
                camera.SetCameraValues((CameraValues)camerasListBox.SelectedRow.UserData);
                camera.Active = true;
            }

            this.Window.Close();
        }
    }
}
