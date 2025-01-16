using Leopotam.EcsLite;

namespace ECSProject
{
    public readonly struct EntityBuilder
    {
        private readonly int _entity;
        private readonly EcsWorld _world;

        public EntityBuilder(EcsWorld world)
        {
            _world = world;
            _entity = _world.NewEntity();
        }

        public EntityBuilder AddComponent<T>(T component) where T : struct
        {
            EcsPool<T> pool = _world.GetPool<T>();
            pool.Add(_entity) = component;
            
            return this;
        }
    }
}