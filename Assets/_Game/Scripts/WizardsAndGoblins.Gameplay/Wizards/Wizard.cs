using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WizardsAndGoblins.Gameplay.Wizards
{
    public class Wizard : Entity
{
    [Header("Spell Settings")]
    [SerializeField] private Transform spellCastPoint;
    [Header("Input Settings")]
    [SerializeField] private List<InputActionToSpellMap> spellSlots; // think of it as UI slots mapping: slot 1 = spell 1, slot 2 = spell 2, etc.
    
    private ISpellFactory _spellFactory;
    private IInputManager _inputManager;
    // Dictionary O(1) lookup: hash(key) → bucket → compare keys in bucket
    // vs List O(n): check each item until found
    private Dictionary<InputAction, SpellDataSO> _inputToSpellLookup;


    public void Setup(ISpellFactory spellFactory, IInputManager inputManager)
    {
        _spellFactory = spellFactory;
        _inputManager = inputManager;
        CreateInputToSpellLookup();
        HookInputEvents();
    }

    public override void Dispose()
    {
        base.Dispose();
        UnHookInputEvents();
    }

    private void CreateInputToSpellLookup()
    {
        _inputToSpellLookup = new Dictionary<InputAction, SpellDataSO>();
        
        foreach (var mapping in spellSlots)
            _inputToSpellLookup[mapping.inputActionAsset.action] = mapping.spellDataSo;
    }

    private void HookInputEvents()
    {
        _inputManager.InputActionPerformed += OnInputPerformed;
    }

    private void UnHookInputEvents()
    {
        _inputManager.InputActionPerformed -= OnInputPerformed;
    }

    private void OnInputPerformed(InputAction.CallbackContext callbackContext)
    {
        if (_inputToSpellLookup.TryGetValue(callbackContext.action, out SpellDataSO spellData))
            CastSpell(spellData);
    }

    private void CastSpell(SpellDataSO spellDataSo)
    {
        ISpell spell = _spellFactory.CreateSpell(spellDataSo, spellCastPoint.position, spellCastPoint.forward);
        spell.Activate();
    }
}
}
