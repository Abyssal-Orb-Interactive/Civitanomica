namespace Frameworks_And_Libraries.Atomic_Framework.Atomic_Behaviour
{
    public interface ILogic
    {
    }

    public interface IEnable : ILogic
    {
        void Enable();
    }

    public interface IDisable : ILogic
    {
        void Disable();
    }

    public interface IUpdate : ILogic
    {
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdate : ILogic
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface ILateUpdate : ILogic
    {
        void OnLateUpdate(float deltaTime);
    }
}