using UnityEngine;

namespace ECSHomework
{
    public class BaseInstaller : ComponentsInstaller
    {
        [SerializeField] private int health = 5;

        public override void Install()
        {
            Entity.SetData(new EntityObject { Value = Entity });
            Entity.SetData(new Health { Value = health });
        }
    }
}