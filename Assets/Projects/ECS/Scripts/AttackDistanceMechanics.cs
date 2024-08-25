using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    public class AttackDistanceMechanics
    {
        private Entity _entity;
        float _attackDistance;
        LayerMask _layerMask;

        public AttackDistanceMechanics(Entity entity, float attackDistance, LayerMask layerMask)
        {
            _entity = entity;
            _attackDistance = attackDistance;
            _layerMask = layerMask;
        }

        public void OnUpdate()
        {
            // Debug.DrawLine (transform.position, transform.position + _attackDistance * transform.forward, Color.red);
            if (Physics.Raycast(_entity.transform.position, _entity.transform.forward, out RaycastHit hit, _attackDistance, _layerMask))
            {
                // Debug.Log("found : " + hit.collider.name);
                EcsPool<ReadyForAttack> readyForAttackPool = _entity.EcsStartup.GetWorld().GetPool<ReadyForAttack>();
                if (!readyForAttackPool.Has(_entity.Id))
                {
                    readyForAttackPool.Add(_entity.Id);
                }

            }
        }
    }
}