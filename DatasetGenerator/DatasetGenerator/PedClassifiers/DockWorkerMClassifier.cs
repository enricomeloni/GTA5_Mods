using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace DatasetGenerator.PedClassifiers
{
    class DockWorkerMClassifier : PedClassifier
    {
        public DockWorkerMClassifier(Ped ped) : base(ped)
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
    }
}
