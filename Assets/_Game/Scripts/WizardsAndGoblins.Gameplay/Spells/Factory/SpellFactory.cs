using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WizardsAndGoblins.Gameplay.Spells
{
    /// <summary>
    /// Simple data-driven spell factory for projectiles
    /// </summary>
    public class SpellFactory : ISpellFactory, IPoolableFactory
    {
        private readonly SpellDatabaseSO _spellDatabase;
        private readonly Transform _spellContainer;

        // Pool per spell key (SpellId)
        private readonly Dictionary<string, Stack<GameObject>> _pools = new();

        public SpellFactory(SpellDatabaseSO spellDatabase, Transform spellContainer = null)
        {
            _spellDatabase = spellDatabase;
            _spellContainer = spellContainer;
        }

        public ISpell CreateSpell(SpellDataSO spellDataSo, Vector3 position, Vector3 direction)
        {
            if (spellDataSo?.SpellPrefab == null)
                throw new Exception("not found");

            string key = spellDataSo.SpellId;
            GameObject spellObject = GetFromPool(key);

            if (spellObject == null)
            {
                // Instantiate
                Quaternion rotation = direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
                spellObject = Object.Instantiate(spellDataSo.SpellPrefab, position, rotation, _spellContainer);

                // Wire up pool info if supported
                if (spellObject.TryGetComponent<IPoolableObject>(out var poolable))
                {
                    poolable.PoolKey = key;
                    poolable.SetPoolFactory(this);
                }
            }
            else
            {
                // Reuse from pool
                Quaternion rotation = direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
                spellObject.transform.SetPositionAndRotation(position, rotation);
                spellObject.SetActive(true);

                if (spellObject.TryGetComponent<IPoolableObject>(out var poolable))
                {
                    if (string.IsNullOrEmpty(poolable.PoolKey))
                        poolable.PoolKey = key;
                    poolable.SetPoolFactory(this);
                    poolable.OnTakenFromPool();
                }
            }

            var spell = spellObject.GetComponent<ISpell>();
            spell.Initialize(spellDataSo);
            return spell;
        }

        public ISpell CreateSpell(string spellId, Vector3 position, Vector3 direction)
        {
            SpellDataSO spellDataSo = _spellDatabase.GetSpellData(spellId);
            if (spellDataSo == null)
            {
                Debug.LogError($"Spell '{spellId}' not found!");
                return null;
            }
            return CreateSpell(spellDataSo, position, direction);
        }
        
        public void Dispose()
        {
            foreach (var kv in _pools)
            {
                var stack = kv.Value;
                while (stack.Count > 0)
                {
                    var go = stack.Pop();
                    Object.Destroy(go);
                }
            }
        }

        // IPoolableFactory
        public void ReturnToPool(IPoolableObject poolableObject)
        {
            string key = poolableObject.PoolKey;
            GameObject go = poolableObject.GameObject;

            poolableObject.OnReturnedToPool();
            go.SetActive(false);
            if (_spellContainer != null)
                go.transform.SetParent(_spellContainer, worldPositionStays: false);

            if (!_pools.TryGetValue(key, out var stack))
            {
                stack = new Stack<GameObject>();
                _pools[key] = stack;
            }
            stack.Push(go);
        }

        private GameObject GetFromPool(string key)
        {
            if (_pools.TryGetValue(key, out var stack) && stack.Count > 0)
                return stack.Pop();
            return null;
        }
    }
}
