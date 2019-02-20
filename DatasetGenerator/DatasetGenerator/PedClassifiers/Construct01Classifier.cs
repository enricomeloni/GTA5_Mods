using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace DatasetGenerator.PedClassifiers
{
    class Construct01Classifier : PedClassifier
    {
        public Construct01Classifier(Ped ped) : base(ped)
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
            return propIndex == 3;
        }

        public override bool HasFaceShield()
        {
            Ped.GetPropIndex(PropComponentIds.Head, out var propIndex, out var _);
            return propIndex == 2;
        }
    }
}
