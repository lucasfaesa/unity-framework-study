using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Skeletons
{
    public class SkeletonManager : Manager
    {
        [SerializeField] private Skeleton skeletonPrefab;
        [SerializeField] private DamageableSpawnedChannelSO damageableSpawnedChannel;
        
        public override void Setup()
        {
            base.Setup();
            CreateSkeleton();
        }
        
        private void CreateSkeleton()
        {
            var skeleton = Instantiate(skeletonPrefab, transform);
            skeleton.Setup();
            
            var damageable = skeleton.Damageable;
            damageableSpawnedChannel.Raise(damageable, damageable.HealthDisplayRefTransform);
        }
    }
}
