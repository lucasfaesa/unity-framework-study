
using UnityEngine;

namespace WizardsAndGoblins
{
    public interface IHealthDisplay
    {
        void Setup(IDamageable damageable, Transform parentTransform);
        void Dispose();
    }
}
