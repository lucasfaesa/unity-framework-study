
namespace WizardsAndGoblins.UI
{
    public class HealthDisplayController
    {
        private HealthDisplayView _view;
        private IDamageable _damageable;
        
        public HealthDisplayController(HealthDisplayView view, IDamageable damageable)
        {
            _view = view;
            _damageable = damageable;

            UpdateFrom(_damageable.CurrentHealth);
            
            _damageable.OnTakeDamage += UpdateFrom;
            _damageable.OnDeath += OnDeath;
        }

        private void UpdateFrom(int currentHealth)
        {
            float healthPercentage = (float)currentHealth / _damageable.MaxHealth;
            _view.UpdateHealthBar(healthPercentage);
        }
        
        private void OnDeath()
        {
            _view.Dispose();
        }
        
        public void Dispose()
        {
            _damageable.OnTakeDamage -= UpdateFrom;
            _damageable.OnDeath -= OnDeath;
        }
    }
}
