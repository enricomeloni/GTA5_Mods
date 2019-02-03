using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetGenerator.PedClassifiers;
using DatasetGenerator.PedSpawners;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class DockWorkerY : PedType
    {
        public static readonly Model Model = new Model("s_m_y_dockwork_01");
        public override Model GetModel()
        {
            return Model;
        }
        public override int[] GetPropComponentChoices()
        {
            return new[] { 0 };
        }

        public override int[] GetPropDrawableChoices(int componentId)
        {
            if (componentId == 0)
            {
                return new[] { -1, 1, 2 };
            }

            return new int[] { };
        }

        public override int[] GetPropTextureChoices(int componentId, int drawableId)
        {
            if (componentId == 0)
            {
                if (drawableId == 1)
                {
                    return new[] { 0, 1, 2 };
                }

                if (drawableId == 2)
                {
                    return new[] { 0, 1, 2 };
                }
            }

            return new int[] { };
        }

        public override int[] GetVariationComponentChoices()
        {
            return new[] { 8 };
        }

        public override int[] GetVariationDrawableChoices(int componentId)
        {
            if (componentId == 0)
            {
                return new[] { 0 };
            }

            return new int[] { };
        }

        public override int[] GetVariationTextureChoices(int componentId, int drawableId)
        {
            if (componentId == 0)
            {
                if (drawableId == 0)
                {
                    return new[] { 0, 1, 2 };
                }
            }

            return new int[] { };
        }

        public override PedClassifier GetPedClassifier(Ped ped)
        {
            return new DockWorkerYClassifier(ped);
        }
    }
}
