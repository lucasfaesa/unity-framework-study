
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace WizardsAndGoblins.UI
{
    public class HealthDisplayView : Entity, IHealthDisplay, IPoolableObject 
    {
        [SerializeField] private Transform healthBarTransform;
        private HealthDisplayController _healthDisplayController;
        private readonly Vector3 _originalScale = Vector3.one;

        //poolableObject stuff
        public string PoolKey { get; set; }
        public IPoolableFactory PoolFactory { get; set; }
        public GameObject GameObject => this.gameObject;
        
        public void Setup(IDamageable damageable, Transform parentTransform)
        {
            this.transform.SetParent(parentTransform, false);
            _healthDisplayController = new HealthDisplayController(this, damageable);
        }
        
        public void UpdateHealthBar(float healthPercentage)
        {
            healthBarTransform.localScale = new Vector3(_originalScale.x * healthPercentage, _originalScale.y, _originalScale.z);
        }

        public override void Dispose()
        {
            base.Dispose();
            _healthDisplayController?.Dispose();
            _healthDisplayController = null;
            PoolFactory.ReturnToPool(this);
        }
        
        public void SetPoolFactory(IPoolableFactory factory) => PoolFactory = factory;

        public void OnTakenFromPool()
        {
            gameObject.SetActive(true);
            healthBarTransform.localScale = _originalScale;
        }

        public void OnReturnedToPool()
        {
            _healthDisplayController?.Dispose();
            _healthDisplayController = null;
            gameObject.SetActive(false);
        }

       
    }
}
