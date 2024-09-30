using System;
using UnityEngine;

namespace ECSHomework
{
    [RequireComponent(typeof(Entity))]
    public class DealDamageAnimationAdapter : MonoBehaviour
    {
        private Entity Entity => GetComponent<Entity>();
        private void OnAttackEvent()
        {
            Entity.SetData(new DealDamageRequest());
        }
    }
}