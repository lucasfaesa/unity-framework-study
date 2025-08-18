using UnityEngine;
using WizardsAndGoblins.Gameplay.Spells.Manager;
using WizardsAndGoblins.Gameplay.Wizards.Manager;

namespace WizardsAndGoblins.Gameplay.Systems
{
    public class GameplaySystem : System
    {
        protected override void SetupManagers()
        {
            base.SetupManagers();

            SpellManager spellManager = GetManager<SpellManager>();
            WizardManager wizardManager = GetManager<WizardManager>();
            
            wizardManager.Setup(spellManager);
        }
    }
}
