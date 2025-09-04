using System;
using UnityEngine.InputSystem;

namespace WizardsAndGoblins
{
    [Serializable]
    public struct InputActionToSpellMap
    {
        public InputActionReference inputActionAsset;
        public SpellDataSO spellDataSo;        
    }
}
