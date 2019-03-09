using Rage;

namespace DatasetGenerator.PedClassifiers
{
    class Construct02Classifier : PedClassifier
    {
        public Construct02Classifier(Ped ped) : base(ped)
        {
        }

        public override bool HasHelmet()
        {
            Ped.GetPropIndex(PropComponentIds.Head, out var propIndex, out var _);
            return propIndex == 0; 
        }

        public override bool HasHighVisibilityVest()
        {
            Ped.GetVariation((int)VariationComponentIds.Accessories, out var drawableIndex, out var _);
            return drawableIndex == 0;
        }

        public override bool HasHearingProtection()
        {
            Ped.GetPropIndex(PropComponentIds.Head, out var propIndex, out var _);
            return propIndex == 2;
        }

        public override bool HasFaceShield()
        {
            Ped.GetPropIndex(PropComponentIds.Head, out var propIndex, out var _);
            return propIndex == 1;
        }
    }
}
