using UnityEngine;

namespace WizardsAndGoblins
{
    /// <summary>
    /// Simple factory interface for creating spells from data
    /// </summary>
    public interface ISpellFactory
    {
        ISpell CreateSpell(SpellData spellData, Vector3 position, Vector3 direction);
        ISpell CreateSpell(string spellId, Vector3 position, Vector3 direction);
        
    }
}
