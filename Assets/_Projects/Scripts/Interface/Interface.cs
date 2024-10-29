using UnityEngine;

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

    public interface IManager
    {
        static IManager Instance { get; set; }
        void OnStart();
        void OnUpdate();
    }

    public interface IEventer
    {
        bool IsFirstUpdate { get; }
    }

    public abstract class ASingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; } = null;

        protected virtual void Awake()
        {
            if (Instance == null) Instance = this as T;
            else Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}