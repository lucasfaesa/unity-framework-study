using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace WizardsAndGoblins.Gameplay.Skeletons
{
    public class Skeleton : Entity, IDamageable
    {
        [SerializeField] private CharacterDataSO characterData; 
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }
        
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

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
        }

        public void Die()
        {
            IsDead = true;
        }
    }
}