using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Wizards
{
    public class Wizard : Entity
    {
        [Header("Spell Settings")]
        [SerializeField] private Transform spellCastPoint;
        
        private ISpellFactory _spellFactory;

        public void Setup(ISpellFactory spellFactory)
        {
            _spellFactory = spellFactory;
        }
        
        public void CastSpell()
        {
            ISpell spell = _spellFactory.CreateSpell("fireball", spellCastPoint.position, spellCastPoint.forward);
            spell.Activate();
        }

        [ContextMenu(nameof(CastSpell))]
        private void Cast()
        {
            CastSpell();       
        }
    }
}
