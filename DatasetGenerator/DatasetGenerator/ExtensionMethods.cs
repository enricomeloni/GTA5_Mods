using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetGenerator.PedClassifiers;
using DatasetGenerator.PedTypes;
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
            return World.ConvertWorldPositionToScreenPosition(vector3);
        }

        public static void SetCameraValues(this Camera camera, CameraValues cameraValues)
        {
            camera.Position = cameraValues.Position;
            camera.Rotation = cameraValues.Rotation;
            camera.FOV = cameraValues.Fov;
        }

        public static void GetPropIndex(this Ped ped, PropComponentIds componentId, out int? drawableId, out int? textureId)
        {
            drawableId = NativeFunction.Natives.GetPedPropIndex<int>(ped, (int)componentId);
            textureId = NativeFunction.Natives.GetPedPropTextureIndex(ped, (int)componentId);
        }

        public static void SetPropIndex(this Ped ped, PropComponentIds componentId, int drawableId, int textureId)
        {
            NativeFunction.Natives.SetPedPropIndex(ped, (int)componentId, drawableId, textureId, true);
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            Random random = new Random();
            var enumerableArray = enumerable as T[] ?? enumerable.ToArray();
            var randomIndex = random.Next(0, enumerableArray.Length);
            return enumerableArray[randomIndex];
        }

        public static PedType GetPedType(this Ped ped)
        {
            return PedType.FromModel(ped.Model);
        }

        public static PedClassifier GetPedClassifier(this Ped ped)
        {
            var pedType = ped.GetPedType();
            if(pedType == null)
                return new RandomPedClassifier(ped);
            return pedType.GetPedClassifier(ped);
        }

        public static void Empty(this System.IO.DirectoryInfo directory)
        {
            foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

        public static void EnableFlying(this Ped ped)
        {
            NativeFunction.Natives.SetEntityHasGravity(ped, false);
            NativeFunction.Natives.SetEntityCollision(ped, false, false);
        }

        public static void DisableFlying(this Ped ped)
        {
            NativeFunction.Natives.SetEntityHasGravity(ped, true);
            NativeFunction.Natives.SetEntityCollision(ped, true, true);
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => 
            self?.Select((item, index) => (item, index)) ?? new List<(T, int)>();
    }
}
