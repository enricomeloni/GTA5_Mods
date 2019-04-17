using System;
using DatasetGenerator.Logging;
using DatasetGenerator.PedTypes;

namespace DatasetGenerator
{
    public abstract partial class PedSpawner
    {
        private const float MaxDeviation = 10f;
        private const int TaskDuration = 1000000;
        private const float WanderRadius = 5f;
        private const float WanderMinimalLength = 1f;
        private const float WanderTimeBetweenWalks = 2f;
        private static readonly Random Random = new Random();
        private static readonly Logger Log = Logger.GetLogger(typeof(PedSpawner));

        private static readonly PedType[] HvvTypes =
        {
            new AirWorker(),
            new Construct01(),
            new Construct02(),
            new DockWorkerM(), 
            new DockWorkerY(), 
        };

        private static readonly PedType[] BareChestTypes =
        {
            new Construct01(),
            new Construct02(),
            new DockWorkerM(),
            new DockWorkerY(),
        };

        private static readonly PedType[] HelmetTypes =
        {
            new Construct01(),
            new Construct02(),
            new DockWorkerM(),
            new DockWorkerY(),
        };

        private static readonly PedType[] FaceShieldTypes =
        {
            new Construct01(),
            new Construct02(),
            new DockWorkerM(),
        };

        private static readonly PedType[] HearingProtectionTypes =
        {
            new AirWorker(),
            new Construct01(),
            new Construct02(),
            new DockWorkerY(),
        };
    }
}