using System.Collections.Generic;
using UnityEngine;

namespace ECSHomework
{
    public class Pool
    {
        private Queue<Entity> _objects;
        private Entity _prefab;
        private Transform _parent;
        private Transform _world;

        private int _count;

        public Pool(UnitConfig config, Transform world)
        {
            _objects = new Queue<Entity>();
            _prefab = config.Prefab;

            _parent = config.PoolParent;
            _world = world;
            
            for (int i = 0; i < config.NumOfUnits; i++)
            {
                Entity _object = GameObject.Instantiate(_prefab, _parent);
                _object.name = _prefab.name + _count;
                _count++;
                _objects.Enqueue(_object);
            }
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
            if(obj == null)
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