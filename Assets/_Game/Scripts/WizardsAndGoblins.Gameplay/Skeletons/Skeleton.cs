
using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Skeletons
{
    public class Skeleton : Entity
    {
        [SerializeField] private Damageable damageable;
        
        public Damageable Damageable => damageable;
        
        public override void Setup()
        {
            base.Setup();
            damageable.Setup();
        }
    }
}