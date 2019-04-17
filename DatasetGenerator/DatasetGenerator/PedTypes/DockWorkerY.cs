using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class DockWorkerY : PedType
    {
        public static readonly Model Model = new Model("s_m_y_dockwork_01");
        public override Model GetModel()
        {
            return Model;
        }

        public override int[] GetHelmetProps()
        {
            return new[] { (int)PropComponentIds.Head, 1 };
        }

        public override int[] GetFaceShieldProps()
        {
            return null;
        }

        public override int[] GetHearingProtectionProps()
        {
            return new[] { (int)PropComponentIds.Head, 2 };
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
            return new DockWorkerYClassifier(ped);
        }
    }
}
