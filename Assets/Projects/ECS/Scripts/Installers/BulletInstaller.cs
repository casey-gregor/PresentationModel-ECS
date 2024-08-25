using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework
{
    public class BulletInstaller : ComponentsInstaller
    {
        [SerializeField] private float speed = 15f;
        [SerializeField] private Entity entity;
        
        public override void Install()
        {
            entity.SetData( new Position { Value = transform.position });
            entity.SetData( new MoveSpeed { Value = speed });
            entity.SetData( new MoveDirection { Value = new Vector3(1f, 0f, 0f) });
            entity.SetData( new TransformView { Value = this.transform });
        }
    }
}