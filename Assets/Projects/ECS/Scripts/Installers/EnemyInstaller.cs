using ECSHomework;
using Leopotam.EcsLite;
using UnityEngine;

public class EnemyInstaller : ComponentsInstaller
{
    [SerializeField] private Entity entity;
    [SerializeField] private int health = 1;
    
    public override void Install()
    {
        entity.SetData(new Health{ CurrentValue = health });
        entity.SetData(new EntityObject { Value = entity });
    }
}
