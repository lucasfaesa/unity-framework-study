using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WizardsAndGoblins.Gameplay.Spells
{
    /// <summary>
    /// Simple data-driven spell factory for projectiles
    /// </summary>
    public class SpellFactory : ISpellFactory
    {
        private readonly SpellDatabaseSO _spellDatabase;
        private readonly Transform _spellContainer;

        public SpellFactory(SpellDatabaseSO spellDatabase, Transform spellContainer = null)
        {
            _spellDatabase = spellDatabase;
            _spellContainer = spellContainer;
        }

        public ISpell CreateSpell(SpellDataSO spellDataSo, Vector3 position, Vector3 direction)
        {
            if (spellDataSo?.SpellPrefab == null)
                throw new Exception("not found");
            
            Quaternion rotation = direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
            GameObject spellObject = Object.Instantiate(spellDataSo.SpellPrefab, position, rotation, _spellContainer);
            
            ISpell spell = spellObject.GetComponent<ISpell>();

            if (spell is ProjectileSpell projectile)
            {
                projectile.Initialize(spellDataSo);
            }

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
    }
}
