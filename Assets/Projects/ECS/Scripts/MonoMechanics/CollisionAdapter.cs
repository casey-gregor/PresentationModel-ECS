using Leopotam.EcsLite;
using Projects.ECS.Scripts.ECSEngine.Components.UnitManagerComponents;
using UnityEngine;

namespace ECSHomework
{
    [RequireComponent(typeof(Entity))]
    public class CollisionAdapter : MonoBehaviour
    {
        [SerializeField] private LayerMask collidableLayer;
        private Entity Entity => GetComponent<Entity>();
        
        private void OnTriggerEnter(Collider collision)
        {
            collision.gameObject.TryGetComponent<Entity>(out Entity entity);

            if (entity != null)
            {
                Teams colliderTeam = entity.GetData<TeamComponent>().Value;
                Teams thisObjectTeam = Entity.GetData<TeamComponent>().Value;

                if (colliderTeam != thisObjectTeam)
                {
                    EcsWorld world = Entity.World;

                    EcsPool<DealDamageRequest> dealDamageRequestPool = world.GetPool<DealDamageRequest>();
                    if(!dealDamageRequestPool.Has(Entity.Id))
                        dealDamageRequestPool.Add(Entity.Id);
                    Entity.TrySetData(new ReturnToPool());
                }
            }
            else if (((1 << collision.gameObject.layer) & collidableLayer) != 0)
            {
                Entity.TrySetData(new ReturnToPool());
            } 
        }
    }
}