using ECSProject.VFX;
using UnityEngine;

namespace ECSProject
{
    public class BaseInstaller : ComponentsInstaller
    {
        [SerializeField] private int health = 2;
        [SerializeField] private Teams team;
        [SerializeField] private ParticleSystem smallFire;
        [SerializeField] private ParticleSystem mediumFire;
        [SerializeField] private ParticleSystem buildingDestroy;
        [SerializeField] private Collider baseCollider;
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private AudioClip fireSound;
        [SerializeField] private AudioClip explodeSound;
        [SerializeField] private AudioSource audioSource;
        
        
        public override void Install()
        {
            Entity.SetData(new EntityObject { Value = Entity });
            Entity.SetData(new UnitTypeComponent { Value = UnitTypes.Base });
            Entity.SetData(new TeamComponent { Value = team });
            Entity.SetData(new Health { InitialValue = health, CurrentValue = health });
            Entity.SetData(new IsBase());
            Entity.SetData(new ColliderComponent { Value = baseCollider });
            Entity.SetData(new MeshRendererComponent { Value = meshRenderer });
            
            Entity.SetData(new FireSmallVFX { Value = smallFire });
            Entity.SetData(new FireMediumVFX { Value = mediumFire });
            Entity.SetData(new BuildingDestroyVFX { Value = buildingDestroy });
            
            Entity.SetData(new AudioSourceComponent { Value = audioSource });
            Entity.SetData(new FireSFX { Value = fireSound });
            Entity.SetData(new ExplodeSFX { Value = explodeSound });
            
            
            
        }
    }
}