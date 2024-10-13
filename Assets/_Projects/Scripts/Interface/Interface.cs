namespace Interface
{
    public interface IData { }
    public interface IProperty : IData { }
    public interface IReference : IData, System.IDisposable { }

    public interface IEventable
    {
        void Start();
        void Update();
    }
    public interface IHandler : IEventable, System.IDisposable { }

    public interface IState { }
    public interface IFlag : System.IDisposable { }
    public interface IManager
    {
        static IManager Instance { get; set; }
        void OnStart();
        void OnUpdate();
    }
    public interface IGameManager : IManager
    {
        IState State { get; set; }
        IFlag Flag { get; }
    }

    public interface IEventer { }
}