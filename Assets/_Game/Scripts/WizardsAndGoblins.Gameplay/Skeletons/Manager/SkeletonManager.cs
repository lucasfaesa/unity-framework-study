using UnityEngine;

namespace WizardsAndGoblins.Gameplay.Skeletons
{
    public class SkeletonManager : Manager
    {
        [SerializeField] private Skeleton skeletonPrefab;
        private Skeleton _skeleton;

        public override void Setup()
        {
            base.Setup();
            CreateSkeleton();
        }
        
        private void CreateSkeleton()
        {
            _skeleton = Instantiate(skeletonPrefab, transform);
            _skeleton.Setup();
        }
    }
}
