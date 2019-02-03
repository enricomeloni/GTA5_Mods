using Rage;

namespace DatasetGenerator.PedClassifiers
{
    abstract class PedClassifier
    {
        public Ped Ped { get; }
        protected PedClassifier(Ped ped)
        {
            Ped = ped;
        }

        public abstract bool HasHelmet();
        public abstract bool HasHighVisibilityVest();
    }
}
