using System.Collections.Generic;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;
using Object = UnityEngine.Object;

namespace WizardsAndGoblins.UI
{
    public class HealthDisplayFactory : IHealthDisplayFactory, IPoolableFactory
    {
        private const string PoolKey = "HealthDisplay";
        private readonly Stack<GameObject> _pool = new(); //if we have different prefabs/types for health display, we can have a pool per prefab/type
        
        private readonly GameObject _prefab;
        private readonly Transform _poolContainer;   // hidden parent for inactive items
        
        public HealthDisplayFactory(GameObject prefab, Transform poolContainer)
        {
            _prefab = prefab;
            _poolContainer = poolContainer;
        }
        
        public IHealthDisplay CreateHealthDisplay(GameObject prefab, IDamageable iDamageable, Transform parentTransform)
        {
            GameObject go = GetFromPoolOrInstantiate();
            
            // On reuse, let the object reset its visuals
            if (go.TryGetComponent<IPoolableObject>(out var poolable))
            {
                if (string.IsNullOrEmpty(poolable.PoolKey)) 
                    poolable.PoolKey = PoolKey;
                poolable.SetPoolFactory(this);
                poolable.OnTakenFromPool();
            }

            var iHealthDisplay = go.GetComponent<IHealthDisplay>();
            iHealthDisplay.Setup(iDamageable, parentTransform);
            return iHealthDisplay;
        }
        
        public void Dispose()
        {
            while (_pool.Count > 0)
            {
                var go = _pool.Pop();
                Object.Destroy(go);
            }
        }
        
        private GameObject GetFromPoolOrInstantiate()
        {
            if (_pool.Count > 0)
                return _pool.Pop();

            var newObj = Object.Instantiate(_prefab, _poolContainer);
            if (newObj.TryGetComponent<IPoolableObject>(out var poolableObj))
            {
                poolableObj.PoolKey = PoolKey;
                poolableObj.SetPoolFactory(this);
            }
            return newObj;
        }

        public void ReturnToPool(IPoolableObject poolableObject)
        {
            var go = poolableObject.GameObject;

            poolableObject.OnReturnedToPool();
            go.SetActive(false);

            // send to pool parent so it’s out of the layout until reused
            if (_poolContainer != null)
                go.transform.SetParent(_poolContainer, worldPositionStays: false);

            _pool.Push(go);
        }
        
    }
}
