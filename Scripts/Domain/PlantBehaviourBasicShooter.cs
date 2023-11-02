using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Domain
{

    public class PlantBehaviourBasicShooter : PlantBehaviour, IShooter
    {
        public const string ANIM_SHOOT_TRIGGER = "Shoot";

        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected Transform shootPoint;
        [SerializeField] protected float shootTime = 3;
        [SerializeField] protected float shootDistance;

        [SerializeField] protected float bulletSpeed;
        [SerializeField] protected float bulletLifeTime = 5;

        protected Animator anim;

        protected override void Awake()
        {
            base.Awake();
            anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Plant is starting to shoot.
        /// </summary>
        public virtual void Shoot()
        {
            anim.SetTrigger(ANIM_SHOOT_TRIGGER);
        }

        /// <summary>
        /// Only call from animations.
        /// </summary>
        public virtual void AnimationCallBackShoot()
        {
            CreateBullet().Shoot(this as IDamageSource, bulletSpeed, transform.right, bulletLifeTime);
        }

        protected virtual BulletBehaviour CreateBullet()
        {
            return Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity).GetComponent<BulletBehaviour>();
        }

        protected virtual bool CheckForEnemy()
        {
            var hit = Physics2D.Raycast(shootPoint.position, transform.right, shootDistance, enemyLayer);
            return hit.collider != null;
        }
    }

}