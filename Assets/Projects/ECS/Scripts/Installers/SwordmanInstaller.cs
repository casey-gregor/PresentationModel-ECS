using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    public class SwordmanInstaller : ComponentsInstaller
    {
        // [SerializeField] private Transform enemyBase;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float attackDistance = 1f;
        [SerializeField] private int health = 3;
        [SerializeField] private int attachDamage = 1;
        [SerializeField] private Animator animator;
        public override void Install()
        {
            Entity.SetData(new EntityObject { Value = Entity });
            Entity.SetData(new Position {Value = transform.position} );
            Entity.SetData(new MoveDirection {Value = transform.forward});
            Entity.SetData(new Rotation {Value = transform.rotation });
            Entity.SetData(new MoveSpeed{Value = speed});
            Entity.SetData(new TransformView {Value = transform});
            Entity.SetData(new TargetEntity());
            Entity.SetData(new AttackDistance { Value = attackDistance });
            Entity.SetData(new Health { Value = health });
            Entity.SetData(new AttackDamage {Value = attachDamage});
        }
    }
}