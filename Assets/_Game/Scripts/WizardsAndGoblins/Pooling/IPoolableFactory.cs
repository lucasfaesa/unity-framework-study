
namespace WizardsAndGoblins
{
    public interface IPoolableFactory
    {
        void ReturnToPool(IPoolableObject poolableObject);
    }
}
