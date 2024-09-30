using ECSHomework.VFX;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

namespace ECSHomework
{
    public class ArcherInstaller : ComponentsInstaller
    {
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool canAttack = true;
        [SerializeField] private Entity arrowPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private int health = 3;
        [SerializeField] private float fireRate = 4f;
        [SerializeField] private Teams team;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem bloodParticles;
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip bowSounds;
        [SerializeField] private AudioSource audioSource;
        
        public override void Install()
        {
            Entity.SetData(new EntityObject
            {
                Value = Entity
            });
            Entity.SetData(new UnitTypeComponent
            {
                Value = UnitTypes.Archer
            });
            Entity.SetData(new TeamComponent { Value = team });
            Entity.SetData(new Position {Value = transform.position} );
            Entity.SetData(new PreviousPosition {Value = transform.position});
            Entity.SetData(new MoveDirection {Value = transform.forward});
            Entity.SetData(new Rotation {Value = transform.rotation });
            Entity.SetData(new Health {CurrentValue = health});
            Entity.SetData(new TransformView {Value = transform});
            Entity.SetData(new TargetEntity());
            Entity.SetData(new ShootingWeapon
            {
                Firepoint = firePoint, 
                ProjectileType = UnitTypes.Projectile,
                FireRate = fireRate,
            });
            Entity.SetData(new AttackInterval { Value = fireRate});
            Entity.SetData(new Timer());
            Entity.SetData(new AnimatorComponent { Value = animator });
            Entity.SetData(new CanMove {Value = canMove});
            Entity.SetData(new CanAttack {Value = canAttack});

            Entity.SetData(new AudioSourceComponent { Value = audioSource });
            Entity.SetData(new HurtSFX { Value = hurtSound });
            Entity.SetData(new WeaponSFX { Value = bowSounds });
            
            Entity.SetData(new BloodVFX { Value = bloodParticles });
        }
    }
}