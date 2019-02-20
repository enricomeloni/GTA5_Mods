using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetGenerator.PedClassifiers;
using Rage;

namespace DatasetGenerator.PedTypes
{
    class Construct02 : PedType
    {
        private static Model Model = new Model("s_m_y_construct_02");

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
            throw new NotImplementedException();
        }

        public override Model GetModel()
        {
            return Model;
        }
    }
}
