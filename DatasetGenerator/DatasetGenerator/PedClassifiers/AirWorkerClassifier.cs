using Rage;

namespace DatasetGenerator.PedClassifiers
{
    class AirWorkerClassifier : PedClassifier
    {
        public AirWorkerClassifier(Ped ped) : base(ped)
        {
        }

        public override bool HasHelmet()
        {
            return false;
        }

        public override bool HasHighVisibilityVest()
        {
            Ped.GetVariation((int)VariationComponentIds.Torso, out var drawableIndex, out var _);
            return drawableIndex == 0 || drawableIndex == 1;
        }

        public override bool HasHearingProtection()
        {
            Ped.GetPropIndex(PropComponentIds.Head, out var propIndex, out var _);
            return propIndex == 1;
        }

        public override bool HasFaceShield()
        {
            return false;
        }
    }
}
