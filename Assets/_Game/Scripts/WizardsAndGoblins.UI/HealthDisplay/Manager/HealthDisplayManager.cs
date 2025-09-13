
using System.Collections.Generic;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace WizardsAndGoblins.UI
{
    public class HealthDisplayManager : Manager
    {
        [SerializeField] private DamageableSpawnedChannelSO damageableSpawnedChannel;
        [SerializeField] private HealthDisplayView healthDisplayPrefab;

        private readonly List<HealthDisplayView> _healthDisplayViews = new();

        public override void Setup()
        {
            base.Setup();
            damageableSpawnedChannel.OnDamageableSpawned += OnSpawned;
        }

        public override void Dispose()
        {
            base.Dispose();
            damageableSpawnedChannel.OnDamageableSpawned -= OnSpawned;
            
            foreach (var healthDisplayView in _healthDisplayViews)
                healthDisplayView.Dispose();
        }

        private void OnSpawned(IDamageable iDamageable, Transform spawnPosition)
        {
            var view = Instantiate(healthDisplayPrefab, spawnPosition);
            view.Setup(iDamageable);
            _healthDisplayViews.Add(view);
        }
    }
}
