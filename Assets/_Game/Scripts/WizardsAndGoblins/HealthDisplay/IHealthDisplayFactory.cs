using UnityEngine;

namespace WizardsAndGoblins
{
    public interface IHealthDisplayFactory
    {
        IHealthDisplay CreateHealthDisplay(GameObject prefab, IDamageable iDamageable, Transform parentTransform);
        void Dispose();
    }
}
