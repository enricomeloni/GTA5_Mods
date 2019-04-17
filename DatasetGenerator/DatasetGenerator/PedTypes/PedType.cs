using System;
using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    public abstract class PedType : IEquatable<PedType>
    {
        public static readonly PedType[] PedTypes =
        {
            new AirWorker(),
            new DockWorkerM(),
            new DockWorkerY(),
            new Construct01(), 
            new Construct02()
        };

        public abstract PedClassifier GetPedClassifier(Ped ped);
        public abstract Model GetModel();

        public static PedType FromModel(Model model)
        {
            foreach (var pedType in PedTypes)
            {
                if (pedType.GetModel().Name.Equals(model.Name))
                    return pedType;
            }

            return null;
        }


        public virtual Ped SpawnPed(Vector3 position)
        {
            return new Ped(GetModel(), position, 0);
        }

        public string TypeName => GetType().Name;

        public abstract int[] GetHelmetProps();
        public abstract int[] GetFaceShieldProps();
        public abstract int[] GetHearingProtectionProps();
        public abstract int[] GetHighVisibilityVestVariation();
        public abstract int[] GetBareChestVariation();

        public bool Equals(PedType other)
        {
            if (other == null)
                return false;
            return TypeName == other.TypeName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PedType) obj);
        }

        public override int GetHashCode()
        {
            return TypeName.GetHashCode();
        }
    }
}
