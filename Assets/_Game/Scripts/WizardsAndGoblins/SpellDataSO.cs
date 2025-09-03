using UnityEditor;
using UnityEngine;

namespace WizardsAndGoblins
{
    [CreateAssetMenu(fileName = "New Spell", menuName = "Scriptable Objects/Spells/Spell Data")]
    public class SpellDataSO : ScriptableObject
    {
        [Header("Basic Info")] 
        [SerializeField] private string spellId;
        [SerializeField] private string spellName;
        [SerializeField] private GameObject spellPrefab;

        [Header("Projectile Properties")] 
        [SerializeField] private float speed = 10f;
        [SerializeField] private float damage = 25f;
        [SerializeField] private float lifetime = 5f;

        // Public properties
        public string SpellId => spellId;
        public string SpellName => spellName;
        public GameObject SpellPrefab => spellPrefab;
        public float Speed => speed;
        public float Damage => damage;
        public float Lifetime => lifetime;

        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(spellId))
                spellId = GUID.Generate().ToString();
        }
        #endif

    }
}
