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
    public abstract partial class PedSpawner
    {
        private static int PropStage = 0;
        private static int VariationStage = 0;

        public static Ped SpawnNewPed(Vector3 pedPosition)
        {
            /*var typeIndex = Random.Next(0, PedType.PedTypes.Length);
            var pedType = PedType.PedTypes[typeIndex];

            pedType = PedType.PedTypes[5];*/

            PedType[] propPicks;
            PedType[] variationPicks;

            switch (PropStage)
            {
                case 0:
                    //bare head
                    propPicks = PedType.PedTypes;
                    break;
                case 1:
                    //helmet
                    propPicks = HelmetTypes;
                    break;
                case 2:
                    //face shield
                    propPicks = FaceShieldTypes;
                    break;
                case 3:
                    //hearing protection
                    propPicks = HearingProtectionTypes;
                    break;
                default:
                    propPicks = new PedType[] { };
                    break;
            }

            switch (VariationStage)
            {
                case 0:
                    //bare chest
                    variationPicks = BareChestTypes;
                    break;
                case 1:
                    //hvv chest
                    variationPicks = HvvTypes;
                    break;
                default:
                    variationPicks = new PedType[] { };
                    break;
            }

            var intersection = propPicks.Intersect(variationPicks);

            var pedType = intersection.RandomElement();


            var spawnedPed = SpawnPedOfType(pedType, pedPosition);

            PropStage = (PropStage + 1) % 4;
            VariationStage = (VariationStage + 1) % 2;

            return spawnedPed;
        }

        public static Ped SpawnPedOfType(PedType pedType, Vector3 pedPosition)
        {
            var ped = pedType.SpawnPed(pedPosition);

            int[] variation;
            int[] props;

            switch (PropStage)
            {
                case 0:
                    //bare head
                    props = null;
                    break;
                case 1:
                    //helmet
                    props = pedType.GetHelmetProps();
                    break;
                case 2:
                    //face shield
                    props = pedType.GetFaceShieldProps();
                    break;
                case 3:
                    //hearing protection
                    props = pedType.GetHearingProtectionProps();
                    break;
                default:
                    props = null;
                    break;
            }

            switch (VariationStage)
            {
                case 0:
                    //bare chest
                    variation = pedType.GetBareChestVariation();
                    break;
                case 1:
                    //hvv chest
                    variation = pedType.GetHighVisibilityVestVariation();
                    break;
                default:
                    variation = null;
                    break;
            }

            Log.Debug($"Spawned ped of type {pedType.TypeName} with model {ped.Model.Name}.");

            
            if (props == null)
            {
                Log.Debug("No props for this ped");
            }
            else
            {
                Log.Debug("Setting random props for ped: " +
                          string.Join(",", props));

                var propTextures = ped.GetPropTextureVariations(props[0], props[1]);
                var textureId = Random.Next(0, propTextures);
                ped.SetPropIndex((PropComponentIds)props[0], props[1], textureId);
            }

            if (variation == null)
            {
                Log.Debug("No variation for this ped");
            }
            else
            {
                Log.Debug("Setting random props for ped: " +
                          string.Join(",", variation));

                var variationTextures = ped.GetTextureVariationCount(variation[0], variation[1]);
                var textureId = Random.Next(0, variationTextures);
                ped.SetVariation(variation[0], variation[1], textureId);
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
