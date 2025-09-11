using System;
using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Spells
{
    public class ProjectileSpell : Entity, ISpell, IPoolableObject
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private SpellDataSO _spellDataSo;
        private float _lifetime;
        private float _elapseTime;
        
        //PoolableObject stuff
        public IPoolableFactory PoolFactory { get; set; }
        public string PoolKey { get; set; }
        public GameObject GameObject => this.gameObject;
        

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
            
            _elapseTime += deltaTime;
            
            if (_elapseTime >= _lifetime)
                PoolFactory.ReturnToPool(this);         
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_spellDataSo.Damage);
            }
            
            PoolFactory.ReturnToPool(this);    
        }

        public void SetPoolFactory(IPoolableFactory factory)
        {
            PoolFactory = factory;
        }

        public void OnTakenFromPool()
        {
            _elapseTime = 0f;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReturnedToPool()
        {
            
        }

    }
}
