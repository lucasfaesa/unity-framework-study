using System;
using UnityEngine;

namespace WizardsAndGoblins
{
    [CreateAssetMenu(fileName = "DamageableSpawnedChannel", menuName = "Scriptable Objects/Events/DamageableSpawnedChannel")]
    public class DamageableSpawnedChannelSO : ScriptableObject
    {
        public event Action<IDamageable, Transform> OnDamageableSpawned;
        public void Raise(IDamageable damageable, Transform spawnPoint) => OnDamageableSpawned?.Invoke(damageable, spawnPoint);
    }
}
