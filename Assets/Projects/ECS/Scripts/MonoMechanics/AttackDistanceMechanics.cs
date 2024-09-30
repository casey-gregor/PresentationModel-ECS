using System;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework
{
    [RequireComponent (typeof (Entity))]
    public class AttackDistanceMechanics: MonoBehaviour
    {
        [SerializeField] private float attackDistance;
        [SerializeField] private LayerMask enemyLayerMask;

        private Entity Entity => GetComponent<Entity>();
        private EcsPool<ReadyForAttack> ReadyForAttackPool => Entity.World.GetPool<ReadyForAttack>();
        
        public void Update()
        {
            // Debug.DrawLine (transform.position, transform.position + _attackDistance * transform.forward, Color.red);
            ;
            if (Physics.Raycast(
                    Entity.transform.position, 
                    Entity.transform.forward, 
                    attackDistance,
                    enemyLayerMask))
            {
                // Debug.Log("found : " + hit.collider.name);
                if (!ReadyForAttackPool.Has(Entity.Id))
                {
                    ReadyForAttackPool.Add(Entity.Id);
                }
            }
            else if (ReadyForAttackPool.Has(Entity.Id))
            {
                ReadyForAttackPool.Del(Entity.Id);
            }
        }
    }
}