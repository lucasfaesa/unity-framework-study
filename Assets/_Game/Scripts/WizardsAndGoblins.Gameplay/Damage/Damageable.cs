using System;
using UnityEngine;

namespace WizardsAndGoblins.Gameplay
{
    public class Damageable : Entity, IDamageable
    {
        [SerializeField] private CharacterDataSO characterData;
        [SerializeField] private Transform healthDisplayRefTransform;
        
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }
        
        public Action OnDeath { get; set; }
        public Action<int> OnTakeDamage { get; set; }

        public Transform HealthDisplayRefTransform => healthDisplayRefTransform;
        
        public override void Setup()
        {
            base.Setup();
            InitializeHealth(characterData.MaxHealth);
        }
        
        public void InitializeHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
            IsDead = false;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;
            
            CurrentHealth -= damage;
            Debug.Log($"<color=red> Took {damage} damage. Current health: {CurrentHealth} </color>");
            OnTakeDamage?.Invoke(CurrentHealth);
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
        }

        public void Die()
        {
            IsDead = true;
            OnDeath?.Invoke();
        }
    }
}
