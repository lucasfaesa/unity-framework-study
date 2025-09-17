using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Wizards
{
    public class WizardManager : Manager
    {
        [SerializeField] private Wizard wizardPrefab;
        
        private ISpellService _spellService;
        private IInputManager _inputManager;
        private Wizard _wizard;
        
        public void Setup(ISpellService spellService, IInputManager inputManager)
        {
            _spellService = spellService;
            _inputManager = inputManager;
  
            CreateWizard();
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            _wizard.Tick(deltaTime);
        }

        private void CreateWizard()
        {
            _wizard = Instantiate(wizardPrefab, transform);
            _wizard.Setup(_spellService, _inputManager);
        }
    }
}
