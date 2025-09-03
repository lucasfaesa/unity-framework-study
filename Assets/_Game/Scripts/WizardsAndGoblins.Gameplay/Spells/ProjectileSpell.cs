using System;
using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Spells
{
    public class ProjectileSpell : Entity, ISpell
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private SpellDataSO _spellDataSo;
        private float _lifetime;

        public void Initialize(SpellDataSO spellDataSo)
        {
            _spellDataSo = spellDataSo;
            _lifetime = spellDataSo.Lifetime;
        }

        public void Activate()
        {
            _rigidbody.linearVelocity = transform.forward * _spellDataSo.Speed;
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
