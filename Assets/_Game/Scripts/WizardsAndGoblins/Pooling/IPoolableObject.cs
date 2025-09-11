using UnityEngine;

namespace WizardsAndGoblins
{
    public interface IPoolableObject
    {
        string PoolKey { get; set; }
        void SetPoolFactory(IPoolableFactory factory);
        void OnTakenFromPool();
        void OnReturnedToPool();
        GameObject GameObject { get; }
        IPoolableFactory PoolFactory { get; set; }
    }
}
