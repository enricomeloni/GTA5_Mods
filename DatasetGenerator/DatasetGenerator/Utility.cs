using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Rage.Native;

namespace DatasetGenerator
{
    class Utility
    {
        public static CameraValues GetGameplayCameraValues()
        {
            float pitch = NativeFunction.Natives.GET_GAMEPLAY_CAM_RELATIVE_PITCH<float>();
            float heading = NativeFunction.Natives.GET_GAMEPLAY_CAM_RELATIVE_HEADING<float>();

            var camPosition = NativeFunction.Natives.GET_GAMEPLAY_CAM_COORD<Vector3>();

            return new CameraValues
            {
                Position = camPosition,
                Rotation = new Rotator(pitch, 0, heading)
            };
        }
    }
}
