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
            var camPosition = NativeFunction.Natives.GetGameplayCamCoord<Vector3>();
            //argument 2 is rotation order: pitch, roll, yaw
            Vector3 camRotationVector = NativeFunction.Natives.GetGameplayCamRot<Vector3>(2);
            var camRotator = new Rotator(camRotationVector.X, camRotationVector.Y, camRotationVector.Z);

            var camFov = NativeFunction.Natives.GetGameplayCamFov<float>();

            return new CameraValues
            {
                Position = camPosition,
                Rotation = camRotator,
                Fov = camFov
            };
        }

        public static void WaitTicks(int ticksWithPause)
        {
            for (int currentTick = 0; currentTick < ticksWithPause; ++currentTick)
            {
                GameFiber.Yield();
            }
        }
    }
}
