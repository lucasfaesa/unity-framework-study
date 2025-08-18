using System.Collections.Generic;
using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Spells.Manager
{
    public class SpellManager : WizardsAndGoblins.Manager, ISpellFactory
    {
        [SerializeField] private GameObject spellPrefab;
        
        private List<Entity> _spells = new List<Entity>();

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            foreach (var spell in _spells)
            {
                spell.Tick(deltaTime);
            }
        }

        public ISpell CreateSpell(Vector3 position, Vector3 direction)
        {
            if (!spellPrefab.TryGetComponent(out ISpell spell))
            {
                Debug.LogError($"Prefab '{spellPrefab.name}' is not a spell!");
                return null;
            }
            
            spell = Instantiate(spellPrefab, position, Quaternion.LookRotation(direction)).GetComponent<ISpell>();
            
            if(spell is Entity entity)
                _spells.Add(entity);
            
            return spell;
        }
    }
}
