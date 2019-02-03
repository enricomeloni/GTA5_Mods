using System;
using System.Collections.Generic;
using DatasetGenerator.PedClassifiers;
using DatasetGenerator.PedTypes;
using Rage;

namespace DatasetGenerator.PedSpawners
{
    public abstract class PedSpawner
    {
        private static readonly Random Random = new Random();

        private static readonly PedType[] PedTypes =
        {
            new AirWorker(),
            new DockWorkerM(),
            new DockWorkerY()
        };

        public static void SpawnNewPed(Vector3 pedPosition)
        {
            var typeIndex = Random.Next(0, PedTypes.Length);
            var pedType = PedTypes[typeIndex];

            var ped = new Ped(pedType.GetModel(), pedPosition, 0);

            //let the game choose a random variation. Choose random props instead

            List<int[]> randomProps = pedType.GetRandomProps();
            foreach (var prop in randomProps)
            {
                ped.SetPropIndex((PropComponentIds)prop[0], prop[1], prop[2]);
            }


            Game.DisplaySubtitle(pedType.GetModel().Name);
        }
    }
}
