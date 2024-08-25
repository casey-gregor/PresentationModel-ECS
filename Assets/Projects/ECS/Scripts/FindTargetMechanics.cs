using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework
{

    public class FindTargetMechanics
    {
        private Entity _entity;
        private Transform _target;
        private LayerMask _enemyLayerMask;
        private float _radius;

        public FindTargetMechanics(Entity entity, float radius, LayerMask enemyLayerMask)
        {
            _entity = entity;
            _radius = radius;
            _enemyLayerMask = enemyLayerMask;
        }
        
        public void OnUpdate()
        {
            Collider[] colliders = Physics.OverlapSphere (_entity.transform.position, _radius, _enemyLayerMask);
            
            if (colliders.Length > 0)
            {
                
                float minDistance = float.MaxValue;
                Entity targetEntity = null;
                foreach (Collider collider in colliders)
                {
                    // Debug.Log("collider : " + collider.name);
                    float distance = Vector3.Distance (_entity.transform.position, collider.transform.position);
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
                    EcsPool<TargetEntity> targetPool = _entity.EcsStartup.GetWorld().GetPool<TargetEntity>();
                    targetPool.Get(_entity.Id).Value = targetEntity;
                }
            }
        }
    }
}