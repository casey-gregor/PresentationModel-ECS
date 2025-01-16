using UnityEngine;

namespace ECSProject
{
    public class PlayerInstaller : ComponentsInstaller
    {
        [SerializeField] private Entity entity;
        [SerializeField] private float speed = 5f;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Entity bulletPrefab;
        
        public override void Install()
        {
            entity.SetData(new MoveDirection());
            entity.SetData(new MoveSpeed { Value = speed });
            entity.SetData(new PositionComponent { Value = transform.position });
            entity.SetData(new TransformView { Value = transform });
            entity.SetData(new ShootingWeapon { Firepoint = firePoint});
        }
    }
}