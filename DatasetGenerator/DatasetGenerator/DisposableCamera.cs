using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gwen.Control.Layout;
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
