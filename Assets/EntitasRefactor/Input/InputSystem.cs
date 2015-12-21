using Entitas;

namespace Assets.EntitasRefactor.Input
{
    public abstract class InputSystem : ISetPool
    {
        protected Group InputGroup;
        protected Pool Pool;

        public void SetPool(Pool pool)
        {
            Pool = pool;
            InputGroup = pool.GetGroup(Matcher.Input);
        }
    }
}