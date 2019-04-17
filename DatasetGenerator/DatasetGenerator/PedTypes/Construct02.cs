using System;
using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class Construct02 : PedType
    {
        private static readonly Model Model = new Model("s_m_y_construct_02");

        public override PedClassifier GetPedClassifier(Ped ped)
        {
            return new Construct02Classifier(ped);
        }

        public override Model GetModel()
        {
            return Model;
        }

        public override int[] GetHelmetProps()
        {
            return new[] {(int) PropComponentIds.Head, 0};
        }

        public override int[] GetFaceShieldProps()
        {
            return new[] { (int)PropComponentIds.Head, 1 };
        }

        public override int[] GetHearingProtectionProps()
        {
            return new[] { (int)PropComponentIds.Head, 2 };
        }

        public override int[] GetHighVisibilityVestVariation()
        {
            return new[] { (int)VariationComponentIds.Accessories, 0 };
        }

        static readonly int[][] BareChestVariations = {
            new[] {(int) VariationComponentIds.Torso, 1},
            new[] {(int) VariationComponentIds.Torso, 2}
        };
        public override int[] GetBareChestVariation()
        {
            return BareChestVariations.RandomElement();
        }
    }
}
