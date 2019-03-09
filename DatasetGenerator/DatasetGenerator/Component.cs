using Rage;

namespace DatasetGenerator
{
    public abstract class Component
    {
        protected GameFiber GameFiber { get; set; }
        protected abstract void Main();
        protected abstract void HandleKeyboardState();

        protected Component()
        {
            GameFiber = new GameFiber(Main);
            GameFiber.Start();
        }
    }
}
