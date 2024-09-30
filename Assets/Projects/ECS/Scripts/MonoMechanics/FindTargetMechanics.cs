using System;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework
{
    [RequireComponent(typeof(Entity))]
    public class FindTargetMechanics : MonoBehaviour
    {
        private Entity Entity => GetComponent<Entity>();
        private int Health => Entity.GetData<Health>().CurrentValue; 
        
        [SerializeField] private Transform _target;
        [SerializeField] private LayerMask enemyLayerMask;
        [SerializeField] private float radius;
        

        public void Update()
        {
            if (Health > 0)
            {
                Collider[] colliders = Physics.OverlapSphere (Entity.transform.position, radius, enemyLayerMask);
                // Debug.DrawRay (_entity.transform.position, _entity.transform.forward * radius, Color.red);
                if (colliders.Length > 0)
                {
                    
                    float minDistance = float.MaxValue;
                    Entity targetEntity = null;
                    
                    foreach (Collider collider in colliders)
                    {
                        // Debug.Log("collider : " + collider.name);
                        float distance = Vector3.Distance (Entity.transform.position, collider.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            _target = collider.gameObject.transform;
                        }
                    }
                    _target.TryGetComponent<Entity>(out targetEntity);
                    // Debug.Log("_target : " + _target.name);
                    if (targetEntity != null)
                    {
                        EcsPool<TargetEntity> targetPool = Entity.World.GetPool<TargetEntity>();
                        targetPool.Get(Entity.Id).Value = targetEntity;
                        // Debug.Log("Target entity found : " + targetEntity);
                    }
                }
            }
        }
    }
}