using System;
using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Spells
{
    public class ProjectileSpell : Entity, ISpell
    {
        [SerializeField] private Rigidbody _rigidbody;
        private SpellData _spellData;
        private float _lifetime;

        public void Initialize(SpellData spellData)
        {
            _spellData = spellData;
            _lifetime = spellData.Lifetime;
        }

        public void Activate()
        {
            _rigidbody.linearVelocity = transform.forward * _spellData.Speed;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            
            _lifetime -= deltaTime;
            if (_lifetime <= 0)
                Dispose();       
        }

        public override void Dispose()
        {
            base.Dispose();
            Destroy(gameObject);       
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Spell hit {other.name}");
            Dispose();
        }
    }
}
