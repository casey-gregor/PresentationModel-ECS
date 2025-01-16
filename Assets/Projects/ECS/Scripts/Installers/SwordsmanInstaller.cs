using Leopotam.EcsLite;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace ECSProject
{
    public class SwordsmanInstaller : ComponentsInstaller
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool canAttack = true;
        [SerializeField] private float attackDistance = 1f;
        [SerializeField] private float attackTimer = 1f;
        [SerializeField] private int health = 3;
        [SerializeField] private int attackDamage = 1;
        [SerializeField] private Teams team;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem bloodParticles;
        
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip swordSounds;
        [SerializeField] private AudioSource audioSource;
       
        public override void Install()
        {
            Entity.SetData(new EntityObject { Value = Entity });
            Entity.SetData(new UnitTypeComponent { Value = UnitTypes.Swordsman });
            Entity.SetData(new TeamComponent { Value = team });
            Entity.SetData(new PositionComponent {Value = transform.position} );
            Entity.SetData(new PreviousPosition {Value = transform.position});
            Entity.SetData(new MoveDirection {Value = transform.forward});
            Entity.SetData(new RotationComponent {Value = transform.rotation });
            Entity.SetData(new MoveSpeed{Value = speed});
            Entity.SetData(new TransformView {Value = transform});
            Entity.SetData(new TargetEntity());
            Entity.SetData(new AttackDistance { Value = attackDistance });
            Entity.SetData(new Health { CurrentValue = health });
            Entity.SetData(new AttackDamageValue {Value = attackDamage});
            Entity.SetData(new Timer());
            Entity.SetData(new AttackInterval { Value = attackTimer});
            Entity.SetData(new AnimatorComponent {Value = animator});
            Entity.SetData(new CanMove {Value = canMove});
            Entity.SetData(new CanAttack {Value = canAttack});
            
            Entity.SetData(new AudioSourceComponent { Value = audioSource });
            Entity.SetData(new HurtSFX { Value = hurtSound });
            Entity.SetData(new WeaponSFX { Value = swordSounds });
            
            Entity.SetData(new BloodVFX { Value = bloodParticles });
        }
    }
}