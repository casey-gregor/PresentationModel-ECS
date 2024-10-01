using System.Collections.Generic;
using UnityEngine;

namespace ECSHomework
{
    public class Pool
    {
        private readonly Queue<Entity> _objects;
        private readonly Entity _prefab;
        private readonly Transform _parent;
        private readonly Transform _world;

        private int _count;

        public Pool(UnitConfig config, Transform world, Transform parent)
        {
            _objects = new Queue<Entity>();
            _prefab = config.prefab;

            _parent = parent;
            _world = world;
        }

        public void Enqueue(Entity obj)
        {
            obj.transform.SetParent(_parent);
            _objects.Enqueue(obj);
        }

        public Entity GetObject()
        {
            _objects.TryDequeue(out Entity obj);
            if (obj == null)
            {
                obj = GameObject.Instantiate(_prefab, _parent);
                obj.name = _prefab.name + _count;
                _count++;
            }

            obj.transform.SetParent(_world);
            return obj;
        }
    }
}