using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetGenerator.PedClassifiers;
using DatasetGenerator.PedSpawners;
using Rage;

namespace DatasetGenerator.PedTypes
{
    abstract class PedType
    {
        public abstract int[] GetVariationComponentChoices();
        public abstract int[] GetVariationDrawableChoices(int componentId);
        public abstract int[] GetVariationTextureChoices(int componentId, int drawableId);
        public abstract int[] GetPropComponentChoices();
        public abstract int[] GetPropDrawableChoices(int componentId);
        public abstract int[] GetPropTextureChoices(int componentId, int drawableId);
        public abstract PedClassifier GetPedClassifier(Ped ped);
        public abstract Model GetModel();

        public List<int[]> GetRandomProps()
        {
            Random random = new Random();
            
            var randomProps = new List<int[]>();
            foreach(var componentId in GetPropComponentChoices())
            {
                var randomDrawable = GetPropDrawableChoices(componentId).RandomElement();
                //when drawable is -1 it means we do not add it
                if (randomDrawable != -1)
                {
                    var randomTexture = GetPropTextureChoices(componentId, randomDrawable).RandomElement();

                    randomProps.Add(new[] {componentId, randomDrawable, randomTexture});
                }
            }

            return randomProps;
        }
    }
}
