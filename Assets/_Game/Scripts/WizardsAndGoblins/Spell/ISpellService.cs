using UnityEngine;

namespace WizardsAndGoblins
{
    public interface ISpellService
    {
        ISpell CreateSpell(SpellDataSO data, Vector3 pos, Vector3 dir);
    }
}
