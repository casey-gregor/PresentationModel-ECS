using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

namespace ECSHomework
{
    public class ProjectileInstaller : ComponentsInstaller
    {
        [SerializeField] private int attackDamage = 1;

        private Rigidbody Rigidbody => Entity.GetComponent<Rigidbody>();

        public override void Install()
        {
            Entity.SetData( new RigidbodyComponent { Value = Rigidbody });
            Entity.SetData( new MoveDirection { Value = transform.forward });
            Entity.SetData( new TransformView { Value = this.transform });
            Entity.SetData( new EntityObject { Value = Entity });
            Entity.SetData( new UnitTypeComponent { Value = UnitTypes.Projectile });
            Entity.SetData( new TargetEntity());
            Entity.SetData( new TeamComponent());
            Entity.SetData( new AttackDamageValue { Value = attackDamage});
        }
    }
}