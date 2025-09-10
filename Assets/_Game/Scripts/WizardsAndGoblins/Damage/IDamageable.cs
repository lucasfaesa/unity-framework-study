
namespace WizardsAndGoblins
{
    public interface IDamageable
    {
        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }
        bool IsDead { get; set; }

        void InitializeHealth(int maxHealth);
        void TakeDamage(int damage);
        void Die();
    }
}
