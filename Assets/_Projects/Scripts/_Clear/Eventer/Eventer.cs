using Interface;
using UnityEngine;
//using Clear.Data;
using Clear.Manager;
using Clear.Handler;

namespace Clear.Eventer
{
    internal sealed class Eventer : MonoBehaviour, IEventer
    {
        private IReference[] datas;
        private IHandler[] handlers;
        private IManager manager => GameManager.Instance;

        public bool IsFirstUpdate { get; private set; } = true;

        private void OnEnable()
        {
            datas = new IReference[]
            {

            };

            handlers = new IHandler[]
            {
                new TitleTraveller()
            };
        }

        private void Update()
        {
            if (IsFirstUpdate)
            {
                IsFirstUpdate = false;

                manager.OnStart();
                foreach (IHandler handler in handlers) handler.Start();
            }

            manager.OnUpdate();
            foreach (IHandler handler in handlers) handler.Update();
        }

        private void OnDisable()
        {
            foreach (IHandler handler in handlers) handler.Dispose();
            System.Array.Clear(handlers, 0, handlers.Length);
            handlers = null;

            foreach (IReference data in datas) data.Dispose();
            System.Array.Clear(datas, 0, datas.Length);
            datas = null;


        }
    }
}