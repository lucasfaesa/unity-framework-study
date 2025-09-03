using System.Collections.Generic;
using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Spells
{
    public class SpellManager : Manager
    {
        [SerializeField] private SpellDatabaseSO spellDatabaseSo;

        private ISpellFactory _spellFactory;
        private List<Entity> _activeSpells = new();

        public ISpellFactory SpellFactory => _spellFactory;
        
        public override void Setup()
        {
            base.Setup();

            if (spellDatabaseSo == null)
            {
                Debug.LogError("SpellManager: SpellDatabase not set!");
                return;
            }
            
            GameObject container = new GameObject("Active Spells");
            container.transform.SetParent(transform);
            
            _spellFactory = new SpellFactory(spellDatabaseSo, container.transform);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            for (int i = _activeSpells.Count - 1; i >= 0; i--)
            {
                Entity spell = _activeSpells[i];

                if (spell == null)
                {
                    _activeSpells.RemoveAt(i);
                    continue;
                }

                spell.Tick(deltaTime);
            }
        }

        public ISpell CreateSpell(Vector3 position, Vector3 direction, string spellId)
        {
            ISpell spell = _spellFactory.CreateSpell(spellId, position, direction);

            if (spell is Entity entity)
            {
                _activeSpells.Add(entity);
            }

            return spell;
        }
    }
}
