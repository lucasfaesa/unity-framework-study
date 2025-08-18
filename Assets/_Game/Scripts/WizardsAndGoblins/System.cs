using System.Collections.Generic;
using UnityEngine;

namespace WizardsAndGoblins
{
    public abstract class System : MonoBehaviour
    {
        public enum SetupTimings{ Awake, Start, Custom}
        
        [SerializeField] private SetupTimings setupTiming = SetupTimings.Start;
        [SerializeField] private List<Manager> managers = new List<Manager>();

        public SetupTimings SetupTiming
        {
            get => setupTiming;
            set => setupTiming = value;
        }

        public T GetManager<T>() where T : Manager
        {
            return managers.Find(manager => manager is T) as T;
        }

        public bool HasManager<T>() where T : Manager
        {
            return managers.Find(manager => manager is T) != null;
        }
        
        public void RegisterManager(Manager manager)
        {
            managers.Add(manager);
        }
        
        public void UnregisterManager(Manager manager)
        {
            managers.Remove(manager);
        }

        public virtual void Setup()
        {
            SetupManagers();
        }

        public virtual void Tick(float deltaTime)
        {
            TickManagers(deltaTime);
        }

        public virtual void Dispose()
        {
            DisposeManagers();
        }

        protected virtual void SetupManagers()
        {
            foreach (var manager in managers)
            {
                manager.Setup();
            }
        }
        
        protected virtual void TickManagers(float deltaTime)
        {
            foreach (var manager in managers)
            {
                manager.Tick(deltaTime);
            }
        }

        protected virtual void DisposeManagers()
        {
            for (int i = managers.Count - 1; i >= 0; i--)
            {
                managers[i].Dispose();
            }
        }

        private void Awake()
        {
            if(SetupTiming == SetupTimings.Awake)
                Setup();
        }
        
        private void Start()
        {
            if(SetupTiming == SetupTimings.Start)
                Setup();
        }
        
        private void OnDestroy()
        {
            Dispose();
        }
        
        private void Update()
        {
            Tick(Time.deltaTime);
        }
        
        
    }
}
