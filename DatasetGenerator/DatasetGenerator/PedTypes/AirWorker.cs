using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class AirWorker : PedType
    {
        public static readonly Model Model = new Model("s_m_y_airworker");

        public override Model GetModel()
        {
            return Model;
        }

        public override int[] GetHelmetProps()
        {
            return null;
        }

        public override int[] GetFaceShieldProps()
        {
            return null;
        }

        public override int[] GetHearingProtectionProps()
        {
            return new[] {(int) PropComponentIds.Head, 1};
        }

        static readonly int[][] HighVisibilityVestVariations = {
            new[] {(int) VariationComponentIds.Torso, 0},
            new[] {(int) VariationComponentIds.Torso, 1}
        };
        public override int[] GetHighVisibilityVestVariation()
        {
            return HighVisibilityVestVariations.RandomElement();
        }

        public override int[] GetBareChestVariation()
        {
            return null;
        }

        public override PedClassifier GetPedClassifier(Ped ped)
        {
            return new AirWorkerClassifier(ped);
        }
    }
}
