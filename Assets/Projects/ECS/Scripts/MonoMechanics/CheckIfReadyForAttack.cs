using System;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSProject
{
    [RequireComponent (typeof (Entity))]
    public class CheckIfReadyForAttack: MonoBehaviour
    {
        [SerializeField] private float attackDistance;
        [SerializeField] private LayerMask enemyLayerMask;

        private Entity Entity => GetComponent<Entity>();
        
        public void Update()
        {
            if (Physics.Raycast(Entity.transform.position, Entity.transform.forward, attackDistance, enemyLayerMask))
            {
                Entity.TrySetData(new ReadyForAttack());
            }
            else
            {
                Entity.TryDeleteData<ReadyForAttack>();
            }
        }
    }
}