using Rage;

namespace DatasetGenerator.ScenarioCreator
{
    public class ScenarioCreator
    {
        private GameFiber GameFiber { get; }
        public ScenarioCreator()
        {
            GameFiber = new GameFiber(Main);
            GameFiber.Start();
        }

        private void Main()
        {
            while (true)
            {
                Game.LocalPlayer.WantedLevel = 0;
                Game.LocalPlayer.IsIgnoredByEveryone = true;
                Game.LocalPlayer.IsIgnoredByPolice = true;

                HandleKeyboardState(Game.GetKeyboardState());

                GameFiber.Yield();
            }
        }

        private void HandleKeyboardState(KeyboardState keyboardState)
        {

        }
    }
}
