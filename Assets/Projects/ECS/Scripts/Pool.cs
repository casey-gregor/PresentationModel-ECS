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
            
            // for (int i = 0; i < config.numOfUnitsInPool; i++)
            // {
            //     Entity item = GameObject.Instantiate(_prefab, _parent);
            //     item.name = _prefab.name + _count;
            //     _count++;
            //     _objects.Enqueue(item);
            // }
        }

        public void Enqueue(Entity obj)
        {
            //Debug.Log("enqueue : " + obj.name);
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
                //Debug.Log("instantiate new : " + _object.name);
            }

            obj.transform.SetParent(_world);
            //Debug.Log("get : " + _object.name);
            return obj;
        }
    }
}