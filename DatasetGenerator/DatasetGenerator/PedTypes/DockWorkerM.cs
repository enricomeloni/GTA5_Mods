using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class DockWorkerM : PedType
    {
        public static readonly Model Model = new Model("s_m_m_dockwork_01");
        public override Model GetModel()
        {
            return Model;
        }

        public override int[] GetHelmetProps()
        {
            return new[] { (int)PropComponentIds.Head, 0 };
        }

        public override int[] GetFaceShieldProps()
        {
            return new[] { (int)PropComponentIds.Head, 2 };
        }

        public override int[] GetHearingProtectionProps()
        {
            return null;
        }

        public override int[] GetHighVisibilityVestVariation()
        {
            return new[] { (int)VariationComponentIds.Accessories, 0 };
        }

        public override int[] GetBareChestVariation()
        {
            return new[] { (int)VariationComponentIds.Accessories, 1 };
        }

        public override PedClassifier GetPedClassifier(Ped ped)
        {
            return new DockWorkerMClassifier(ped);
        }
    }
}
