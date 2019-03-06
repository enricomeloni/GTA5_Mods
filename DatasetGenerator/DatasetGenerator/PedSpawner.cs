using DatasetGenerator.PedClassifiers;
using DatasetGenerator.PedTypes;
using DatasetGenerator.ScenarioCreation;
using Rage;
using Rage.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatasetGenerator
{
    public abstract class PedSpawner
    {
        private const float MaxDeviation = 10f;
        private const int TaskDuration = 1000000;
        private const float WanderRadius = 5f;
        private const float WanderMinimalLength = 1f;
        private const float WanderTimeBetweenWalks = 2f;
        private static readonly Random Random = new Random();
        
        public static Ped SpawnNewPed(Vector3 pedPosition)
        {
            var typeIndex = Random.Next(0, PedType.PedTypes.Length);
            var pedType = PedType.PedTypes[typeIndex];

            return SpawnPedOfType(pedType, pedPosition);
        }

        public static Ped SpawnPedOfType(PedType pedType, Vector3 pedPosition)
        {
            var ped = new Ped(pedType.GetModel(), pedPosition, 0);

            //let the game choose a random variation. Choose random props instead

            //List<int[]> randomProps = pedType.GetRandomProps();
            List<int[]> randomProps = new List<int[]> {ped.GetRandomProps()};
            foreach (var prop in randomProps.Where(prop => prop != null))
            {
                ped.SetPropIndex((PropComponentIds) prop[0], prop[1], prop[2]);
            }

            return ped;
        }

        public static Ped[] SpawnPedsFromPedSettings(PedsSettings pedsSettings, Vector3 spawnPosition)
        {
            var spawnedPeds = new List<Ped>();

            for (int i = 0; i < pedsSettings.PedsNumber; ++i)
            {
                var randomPosition = Utility.Randomize(spawnPosition, MaxDeviation);

                var ped = SpawnNewPed(randomPosition);
                spawnedPeds.Add(ped);
            }

            Ped PickRandomPed(Ped spawnedPed)
            {
                Ped randomPed;
                do
                {
                    randomPed = spawnedPeds.RandomElement();
                } while (randomPed == spawnedPed);

                return randomPed;
            }

            foreach (var spawnedPed in spawnedPeds)
            {
                switch (pedsSettings.PedBehavior)
                {
                    case PedBehavior.Stand:
                        spawnedPed.Tasks.StandStill(TaskDuration);
                        break;
                    case PedBehavior.Phone:
                        NativeFunction.Natives.TaskUseMobilePhoneTimed(spawnedPed, TaskDuration);
                        break;
                    case PedBehavior.Cower:
                        spawnedPed.Tasks.Cower(TaskDuration);
                        break;
                    case PedBehavior.Wander:
                        var wanderRadius = Utility.Randomize(WanderRadius);
                        var wanderLength = Utility.Randomize(WanderMinimalLength);
                        var wanderTime = Utility.Randomize(WanderTimeBetweenWalks, 0.8f);
                        NativeFunction.Natives.TaskWanderInArea(
                            spawnedPed,
                            spawnedPed.Position,
                            wanderRadius,
                            wanderLength,
                            wanderTime);
                        break;
                    case PedBehavior.Chat:
                        //not clear what these parameters do, in game scripts they are always like this.
                        NativeFunction.Natives.TaskChatToPed(
                            spawnedPed,
                            PickRandomPed(spawnedPed),
                            16, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
                        break;
                    case PedBehavior.Combat:
                        spawnedPed.Tasks.FightAgainst(PickRandomPed(spawnedPed));
                        break;
                    case PedBehavior.Cover:
                        NativeFunction.Natives.TaskStayInCover(spawnedPed);
                        break;
                    case PedBehavior.Move:
                        throw new NotImplementedException();
                        break;
                    case PedBehavior.Scenario:
                        throw new NotImplementedException();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return spawnedPeds.ToArray();
        }
    }
}
