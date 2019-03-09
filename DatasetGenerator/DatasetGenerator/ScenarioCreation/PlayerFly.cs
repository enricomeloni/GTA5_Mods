using System.Windows.Forms;
using Rage;

namespace DatasetGenerator.ScenarioCreation
{
    class PlayerFly : Component
    {
        private bool IsFlying { get; set; }
        protected override void Main()
        {
            Game.DisplaySubtitle("Loaded Player Fly Component");

            while (true)
            {
                Game.LocalPlayer.WantedLevel = 0;
                Game.LocalPlayer.IsIgnoredByEveryone = true;
                Game.LocalPlayer.IsIgnoredByPolice = true;

                HandleKeyboardState();

                var velocity = Game.LocalPlayer.Character.Velocity;
                //Game.DisplaySubtitle($"({velocity.X:F2}, {velocity.Y:F2}, {velocity.Z:F2}); in air: {(Game.LocalPlayer.Character.IsInAir ? "yes" : "no")}");

                GameFiber.Yield();
            }
        }

        protected override void HandleKeyboardState()
        {
            var localPlayerCharacter = Game.LocalPlayer.Character;
            if (Game.IsKeyDown(Keys.NumPad5))
            {
                IsFlying = !IsFlying;

                if (IsFlying)
                {
                    localPlayerCharacter.EnableFlying();
                }
                else
                {
                    localPlayerCharacter.DisableFlying();
                }

                Game.DisplaySubtitle("Flying: " + (IsFlying ? "active" : "disabled"));
            }

            if (Game.IsKeyDown(Keys.NumPad2))
            {
                localPlayerCharacter.ApplyForce(new Vector3(0,0,-50), Vector3.Zero, true, true);

            }

            if (Game.IsKeyDown(Keys.NumPad8))
            {
                localPlayerCharacter.ApplyForce(new Vector3(0, 0, +50), Vector3.Zero, true, true);
            }

            if (Game.IsKeyDown(Keys.NumPad6))
            {
                localPlayerCharacter.Velocity = Vector3.Zero;
            }
        }
    }
}
