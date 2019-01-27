using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Rage.Native;

namespace DatasetGenerator
{
    public static class ExtensionMethods
    {
        //Vector3.Transform returns always a Vector4, whose fourth component can be safely ignored
        public static Vector3 Rotate(this Vector3 vector3, Quaternion orientation)
        {
            Vector4 transformedVector4 = Vector3.Transform(vector3, orientation);
            return new Vector3(transformedVector4.X, transformedVector4.Y, transformedVector4.Z);
        }

        public static Vector2 ProjectToScreen(this Vector3 vector3)
        {
            float vector_x;
            float vector_y;

            //NativeFunction.Natives.xF9904D11F1ACBEC3(vector3.X, vector3.Y, vector3.Z, out vector_x, out vector_y);
            //return new Vector2(vector_x, vector_y);


            return World.ConvertWorldPositionToScreenPosition(vector3);
        }
    }
}
