using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Wizards
{
    public class WizardManager : Manager
    {
        [SerializeField] private Wizard wizardPrefab;
        
        private ISpellFactory _spellFactory;
        private IInputManager _inputManager;
        private Wizard _wizard;
        
        public void Setup(ISpellFactory spellFactory, IInputManager inputManager)
        {
            _spellFactory = spellFactory;
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
            _wizard.Setup(_spellFactory, _inputManager);
        }
    }
}
