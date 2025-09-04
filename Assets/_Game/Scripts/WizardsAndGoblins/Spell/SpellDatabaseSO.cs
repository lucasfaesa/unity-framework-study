using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsAndGoblins
{
    [CreateAssetMenu(fileName = "SpellDatabase", menuName = "Scriptable Objects/Spells/SpellDatabase")]
    public class SpellDatabaseSO : ScriptableObject
    {
        [SerializeField] private List<SpellDataSO> spells = new();
        
        private Dictionary<string, SpellDataSO> _spellsLookup = new();

        private void OnEnable()
        {
            BuildLookupTable();
        }

        private void BuildLookupTable()
        {
            _spellsLookup = new Dictionary<string, SpellDataSO>();

            foreach (var spell in spells)
            {
                if(spell != null && !string.IsNullOrEmpty(spell.SpellId))
                    _spellsLookup[spell.SpellId] = spell;
            }
        }

        public SpellDataSO GetSpellData(string spellId)
        {
            _spellsLookup.TryGetValue(spellId, out SpellDataSO spellData);
            return spellData;
        }
        
        public bool ContainsSpell(string spellId)
        {
            return _spellsLookup.ContainsKey(spellId);
        }
    }
}
