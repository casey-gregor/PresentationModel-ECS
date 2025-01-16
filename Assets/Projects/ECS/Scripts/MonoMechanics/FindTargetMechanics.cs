using UnityEngine;

namespace ECSProject
{
    [RequireComponent(typeof(Entity))]
    public class FindTargetMechanics : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private LayerMask enemyLayerMask;
        [SerializeField] private float radius;
        private Entity Entity => GetComponent<Entity>();
        private int Health => Entity.GetData<Health>().CurrentValue;
        

        public void Update()
        {
            if (Health > 0)
            {
                Collider[] colliders = Physics.OverlapSphere (Entity.transform.position, radius, enemyLayerMask);
                
                if (colliders.Length > 0)
                {
                    float minDistance = float.MaxValue;

                    foreach (Collider colliderObj in colliders)
                    {
                        float distance = Vector3.Distance (Entity.transform.position, colliderObj.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            target = colliderObj.gameObject.transform;
                        }
                    }
                    var targetEntity = target.GetComponentInParent<Entity>();
                    
                    if (targetEntity != null)
                    {
                        Entity.TrySetData(new TargetEntity { Value = targetEntity });
                    }
                }
            }
        }
    }
}