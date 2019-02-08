using System;
using System.Collections.Generic;
using DatasetGenerator.PedClassifiers;
using DatasetGenerator.PedTypes;
using Rage;

namespace DatasetGenerator
{
    public abstract class PedSpawner
    {
        private static readonly Random Random = new Random();
        
        public static Ped SpawnNewPed(Vector3 pedPosition)
        {
            var typeIndex = Random.Next(0, PedType.PedTypes.Length);
            var pedType = PedType.PedTypes[typeIndex];

            var ped = new Ped(pedType.GetModel(), pedPosition, 0);

            //let the game choose a random variation. Choose random props instead

            List<int[]> randomProps = pedType.GetRandomProps();
            foreach (var prop in randomProps)
            {
                ped.SetPropIndex((PropComponentIds)prop[0], prop[1], prop[2]);
            }

            return ped;
        }
    }
}
