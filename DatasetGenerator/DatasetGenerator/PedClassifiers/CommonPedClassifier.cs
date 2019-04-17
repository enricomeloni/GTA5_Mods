using Rage;

namespace DatasetGenerator.PedClassifiers
{
    class CommonPedClassifier : PedClassifier
    {
        public CommonPedClassifier(Ped ped) : base(ped)
        {
        }

        public override bool HasHelmet()
        {
            return false;
        }

        public override bool HasHighVisibilityVest()
        {
            return false;
        }

        public override bool HasHearingProtection()
        {
            return false;
        }

        public override bool HasFaceShield()
        {
            return false;
        }
    }
}