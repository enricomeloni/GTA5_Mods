using Rage;

namespace DatasetGenerator.PedClassifiers
{
    class DockWorkerYClassifier : PedClassifier
    {
        public DockWorkerYClassifier(Ped ped) : base(ped)
        {
        }

        public override bool HasHelmet()
        {
            Ped.GetPropIndex(PropComponentIds.Head, out var propIndex, out var _);
            return propIndex == 1;
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
            return false;
        }
    }
}
