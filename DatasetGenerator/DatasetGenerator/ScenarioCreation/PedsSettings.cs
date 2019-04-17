using Rage;

namespace DatasetGenerator.ScenarioCreation
{
    public class PedsSettings
    {
        public int PedsNumber { get; set; }
        public PedBehavior PedBehavior { get; set; }
        public bool PedShouldGroup { get; set; }

        public Ped[] Apply()
        {
            return PedSpawner.SpawnPedsFromPedSettings(this, Game.LocalPlayer.Character.Position);
        }

        public void Clear()
        {
            PedsNumber = 0;
            PedBehavior = 0;
            PedShouldGroup = false;
        }
    }

    public enum PedBehavior
    {
        Stand,
        Phone,
        Cower,
        Wander,
        Chat,
        Combat,
        Cover,
        Move,
        Scenario
    }
}
