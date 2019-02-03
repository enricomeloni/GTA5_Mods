using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class AirWorker : PedType
    {
        public static readonly Model Model = new Model("s_m_y_airworker");

        public override Model GetModel()
        {
            return Model;
        }

        public override int[] GetPropComponentChoices()
        {
            return new[] {0};
        }

        public override int[] GetPropDrawableChoices(int componentId)
        {
            if (componentId == 0)
            {
                return new[] { -1, 1 };
            }

            return new int[] { };
        }

        public override int[] GetPropTextureChoices(int componentId, int drawableId)
        {
            if (componentId == 0)
            {
                if (drawableId == 1)
                {
                    return new[] {0, 1};
                }
            }

            return new int[] {};
        }

        public override int[] GetVariationComponentChoices()
        {
            return new[] { 3 };
        }

        public override int[] GetVariationDrawableChoices(int componentId)
        {
            if (componentId == 0)
            {
                return new[] { 0, 1 };
            }

            return new int[] { };
        }

        public override int[] GetVariationTextureChoices(int componentId, int drawableId)
        {
            if (componentId == 0)
            {
                if (drawableId == 0)
                {
                    return new[] { 0, 1, 2, 3 };
                }

                if (drawableId == 1)
                {
                    return new[] {0, 1, 2};
                }
            }

            return new int[] { };
        }

        public override PedClassifier GetPedClassifier(Ped ped)
        {
            return new AirWorkerClassifier(ped);
        }
    }
}
