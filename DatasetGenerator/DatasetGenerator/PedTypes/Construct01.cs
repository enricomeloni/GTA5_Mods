using System;
using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class Construct01 : PedType
    {
        private static readonly Model Model = new Model("s_m_y_construct_01");
        public override int[] GetVariationComponentChoices()
        {
            throw new NotImplementedException();
        }

        public override int[] GetVariationDrawableChoices(int componentId)
        {
            throw new NotImplementedException();
        }

        public override int[] GetVariationTextureChoices(int componentId, int drawableId)
        {
            throw new NotImplementedException();
        }

        public override int[] GetPropComponentChoices()
        {
            throw new NotImplementedException();
        }

        public override int[] GetPropDrawableChoices(int componentId)
        {
            throw new NotImplementedException();
        }

        public override int[] GetPropTextureChoices(int componentId, int drawableId)
        {
            throw new NotImplementedException();
        }

        public override PedClassifier GetPedClassifier(Ped ped)
        {
            return new Construct01Classifier(ped);
        }

        public override Model GetModel()
        {
            return Model;
        }
    }
}
