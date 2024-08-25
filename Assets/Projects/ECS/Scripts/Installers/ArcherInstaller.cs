using UnityEngine;

namespace ECSHomework
{
    public class ArcherInstaller : ComponentsInstaller
    {
        [SerializeField] private float speed = 0f;
        public override void Install()
        {
            Entity.SetData(new Position {Value = transform.position} );
            Entity.SetData(new MoveDirection {Value = transform.forward});
            // entity.SetData(new Rotation{Value = Quaternion.Euler(0f,-90f,0f)});
            Entity.SetData(new MoveSpeed{Value = speed});
            Entity.SetData(new TransformView {Value = transform});
            Entity.SetData(new TargetEntity());
        }
    }
}