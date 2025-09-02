
namespace WizardsAndGoblins
{
    /// <summary>
    /// Database interface for managing spell definitions
    /// </summary>
    public interface ISpellDatabase
    {
        SpellData GetSpellData(string spellId);
        bool ContainsSpell(string spellId);
    }
}
