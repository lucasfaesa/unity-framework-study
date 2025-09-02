using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Wizards
{
    public class WizardManager : Manager
    {
        [SerializeField] private Wizard wizardPrefab;
        
        private ISpellFactory _spellFactory;
        private Wizard _wizard;
        
        public void Setup(ISpellFactory spellFactory)
        {
            _spellFactory = spellFactory;
            
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
            _wizard.Setup(_spellFactory);
        }
    }
}
