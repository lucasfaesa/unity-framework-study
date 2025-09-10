using UnityEngine;

namespace WizardsAndGoblins
{
    [CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Scriptable Objects/Character/CharacterDataSO")]
    public class CharacterDataSO : ScriptableObject
    {
        [field:SerializeField] public int MaxHealth { get; set; }
    }
}
