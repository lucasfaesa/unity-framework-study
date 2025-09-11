using System.Collections.Generic;
using UnityEngine;

namespace WizardsAndGoblins
{
    public class PooledSpellFactory : ISpellFactory, IPoolableFactory
    {
        private readonly ISpellFactory _spellFactory;
        private readonly SpellDatabaseSO _spellDatabase;
        private readonly Transform _container;
        // one pool per spell prefab/key
        private readonly Dictionary<string, Stack<GameObject>> _pools = new();

        public PooledSpellFactory(ISpellFactory spellFactory, SpellDatabaseSO spellDatabaseSo, Transform container)
        {
            _spellFactory = spellFactory;
            _spellDatabase = spellDatabaseSo;
            _container = container;
        }
        
        public ISpell CreateSpell(SpellDataSO spellDataSo, Vector3 position, Vector3 direction)
        {
            string key = spellDataSo.SpellId;
            GameObject spellObject = GetFromPool(key);
            if (spellObject == null)
            {
                ISpell created = _spellFactory.CreateSpell(spellDataSo, position, direction);
                if (created is IPoolableObject poolableObject)
                {
                    poolableObject.PoolKey = key;
                    poolableObject.SetPoolFactory(this);
                }

                return created;
            }
            
            Quaternion rot = direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
            spellObject.transform.SetPositionAndRotation(position, rot);
            spellObject.SetActive(true);
            
            var spell = spellObject.GetComponent<ISpell>();

            if (spell is IPoolableObject poolableSpell)
            {
                if(string.IsNullOrEmpty(poolableSpell.PoolKey))
                    poolableSpell.PoolKey = key;
                poolableSpell.SetPoolFactory(this);
                poolableSpell.OnTakenFromPool();
            }
            
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

        private GameObject GetFromPool(string key)
        {
            if (_pools.TryGetValue(key, out var stack) && stack.Count > 0)
                return stack.Pop();
            
            return null;
        }

        public void ReturnToPool(IPoolableObject poolableObject)
        {
            string key = poolableObject.PoolKey;
            GameObject gO = poolableObject.GameObject;
            
            poolableObject.OnReturnedToPool();
            
            gO.SetActive(false);
            gO.transform.SetParent(_container, false);
            if(!_pools.TryGetValue(key, out var stack))
            {
                stack = new Stack<GameObject>();
                _pools[key] = stack;
            }
            
            stack.Push(gO);
        }
    }
}
