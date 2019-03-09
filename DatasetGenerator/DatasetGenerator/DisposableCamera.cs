using System;
using Rage;

namespace DatasetGenerator
{
    class DisposableCamera : IDisposable
    {
        public Camera Camera { get; set; }

        public const string DefaultScriptedCamera = "DEFAULT_SCRIPTED_CAMERA";

        public DisposableCamera(string cameraName)
        {
            Camera = new Camera(cameraName, false);
        }

        public void Dispose()
        {
            if(Camera.IsValid())
                Camera.Delete();
        }
    }
}
