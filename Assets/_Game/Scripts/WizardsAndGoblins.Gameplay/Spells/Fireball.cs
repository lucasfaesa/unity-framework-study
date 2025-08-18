
using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Spells
{
    internal class Fireball : Entity, ISpell
    {
        [SerializeField] private Rigidbody rigidBody;
        
        public void Activate()
        {
            rigidBody.AddRelativeForce(transform.forward * 10f, ForceMode.Impulse);
        }
    }
}
