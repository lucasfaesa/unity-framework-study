
using System.Collections.Generic;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace WizardsAndGoblins.UI
{
    public class HealthDisplayManager : Manager
    {
        [SerializeField] private DamageableSpawnedChannelSO damageableSpawnedChannel;
        [SerializeField] private HealthDisplayView healthDisplayPrefab;
        
        private IHealthDisplayFactory _healthDisplayFactory;
        
        public override void Setup()
        {
            base.Setup();
            
            GameObject container = new GameObject("HealthDisplay Pool");
            container.transform.SetParent(transform);

            _healthDisplayFactory = new HealthDisplayFactory(healthDisplayPrefab.gameObject, container.transform);
            damageableSpawnedChannel.OnDamageableSpawned += OnSpawned;
        }

        public override void Dispose()
        {
            base.Dispose();
            damageableSpawnedChannel.OnDamageableSpawned -= OnSpawned;
            _healthDisplayFactory.Dispose();
        }

        private void OnSpawned(IDamageable iDamageable, Transform spawnPosition)
        {
            _healthDisplayFactory.CreateHealthDisplay(healthDisplayPrefab.gameObject, iDamageable, spawnPosition);
        }
    }
}
