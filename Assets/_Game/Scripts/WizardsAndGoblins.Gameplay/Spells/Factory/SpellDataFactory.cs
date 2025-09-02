using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WizardsAndGoblins.Gameplay.Spells
{
    /// <summary>
    /// Simple data-driven spell factory for projectiles
    /// </summary>
    public class SpellDataFactory : ISpellFactory
    {
        private readonly ISpellDatabase _spellDatabase;
        private readonly Transform _spellContainer;

        public SpellDataFactory(ISpellDatabase spellDatabase, Transform spellContainer = null)
        {
            _spellDatabase = spellDatabase;
            _spellContainer = spellContainer;
        }

        public ISpell CreateSpell(SpellData spellData, Vector3 position, Vector3 direction)
        {
            if (spellData?.SpellPrefab == null)
                throw new Exception("not found");
            
            Quaternion rotation = direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
            GameObject spellObject = Object.Instantiate(spellData.SpellPrefab, position, rotation, _spellContainer);
            
            ISpell spell = spellObject.GetComponent<ISpell>();

            if (spell is ProjectileSpell projectile)
            {
                projectile.Initialize(spellData);
            }

            return spell;
        }

        public ISpell CreateSpell(string spellId, Vector3 position, Vector3 direction)
        {
            SpellData spellData = _spellDatabase.GetSpellData(spellId);

            if (spellData == null)
            {
                Debug.LogError($"Spell '{spellId}' not found!");
                return null;
            }
            
            return CreateSpell(spellData, position, direction);
        }
    }
}
