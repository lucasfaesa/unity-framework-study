using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsAndGoblins
{
    [CreateAssetMenu(fileName = "SpellDatabase", menuName = "Scriptable Objects/Spells/SpellDatabase")]
    public class SpellDatabase : ScriptableObject, ISpellDatabase
    {
        [SerializeField] private List<SpellData> spells = new();
        
        private Dictionary<string, SpellData> _spellsLookup = new();

        private void OnEnable()
        {
            BuildLookupTable();
        }

        private void BuildLookupTable()
        {
            _spellsLookup = new Dictionary<string, SpellData>();

            foreach (var spell in spells)
            {
                if(spell != null && !string.IsNullOrEmpty(spell.SpellId))
                    _spellsLookup[spell.SpellId] = spell;
            }
        }

        public SpellData GetSpellData(string spellId)
        {
            _spellsLookup.TryGetValue(spellId, out SpellData spellData);
            return spellData;
        }
        
        public bool ContainsSpell(string spellId)
        {
            return _spellsLookup.ContainsKey(spellId);
        }
    }
}
