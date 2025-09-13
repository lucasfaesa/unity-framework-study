
using UnityEngine;

namespace WizardsAndGoblins.UI
{
    public class HealthDisplayView : Entity
    {
        [SerializeField] private Transform healthBarTransform;
        private HealthDisplayController _healthDisplayController;
        private Vector3 _originalScale;

        public void Setup(IDamageable damageable)
        {
            _originalScale = healthBarTransform.localScale;
            _healthDisplayController = new HealthDisplayController(this, damageable);
        }
        
        public void UpdateHealthBar(float healthPercentage)
        {
            healthBarTransform.localScale = new Vector3(_originalScale.x * healthPercentage, _originalScale.y, _originalScale.z);
        }

        public override void Dispose()
        {
            base.Dispose();
            _healthDisplayController.Dispose();
        }
    }
}
