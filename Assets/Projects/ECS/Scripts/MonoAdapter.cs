using UnityEngine;
using UnityEngine.Serialization;

namespace ECSHomework
{
    [RequireComponent(typeof(Entity))]
    public class MonoAdapter : MonoBehaviour
    {
        [SerializeField] private float searchRadius;
        [SerializeField] private LayerMask enemyLayerMask;
        [SerializeField] float attackDistance;
        
        [SerializeField] private Transform _target;
        
        private Entity _entity;

        private FindTargetMechanics _findTargetMechanics;
        private AttackDistanceMechanics _attackDistanceMechanics;
        
        private void Awake()
        {
            _entity = GetComponent<Entity>();

            _findTargetMechanics = new FindTargetMechanics(_entity, searchRadius, enemyLayerMask);
            _attackDistanceMechanics = new AttackDistanceMechanics(_entity, attackDistance, enemyLayerMask);
        }

        private void Update()
        {
            _findTargetMechanics.OnUpdate();
            _attackDistanceMechanics.OnUpdate();
        }
    }
}