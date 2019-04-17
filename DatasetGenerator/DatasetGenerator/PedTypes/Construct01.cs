using System;
using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class Construct01 : PedType
    {
        private static readonly Model Model = new Model("s_m_y_construct_01");
        public override PedClassifier GetPedClassifier(Ped ped)
        {
            return new Construct01Classifier(ped);
        }

        public override Model GetModel()
        {
            return Model;
        }

        public override int[] GetHelmetProps()
        {
            return new[] { (int) PropComponentIds.Head, 1 };
        }

        public override int[] GetFaceShieldProps()
        {
            return new[] { (int)PropComponentIds.Head, 2 };
        }

        public override int[] GetHearingProtectionProps()
        {
            return new[] { (int)PropComponentIds.Head, 3 };
        }

        public override int[] GetHighVisibilityVestVariation()
        {
            return new[] { (int)VariationComponentIds.Accessories, 0 };
        }

        public override int[] GetBareChestVariation()
        {
            return new[] { (int)VariationComponentIds.Accessories, 1 };
        }
    }
}
