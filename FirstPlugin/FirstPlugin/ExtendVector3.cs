using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace FirstPlugin
{
    public static class ExtendVector3
    {
        //Vector3.Transform returns always a Vector4, whose fourth component can be safely ignored
        public static Vector3 Rotate(this Vector3 vector3, Quaternion orientation)
        {
            Vector4 transformedVector4 = Vector3.Transform(vector3, orientation);
            return new Vector3(transformedVector4.X, transformedVector4.Y, transformedVector4.Z);
        }
    }
}
