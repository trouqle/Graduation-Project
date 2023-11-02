using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Herb.Domain;

namespace Herb.AI
{
    public class DandelionAI : PlantBehaviourBasicShooter, IDamageSource
    {
        SmartTimer shootTimer;

        protected override void Awake()
        {
            base.Awake();
            shootTimer = new SmartTimer(shootTime * Random.Range(0.95f,1.05f), SmartTimer.Mode.Repeating,CheckForEnemy,SmartTimer.StopMode.Reset);
            shootTimer.OnTimerFinish += Shoot;
            shootTimer.StartTimer();
        }

        private void Update()
        {
            shootTimer.Tick(Time.deltaTime);
        }

        public override void Shoot()
        {
            anim.SetTrigger(ANIM_SHOOT_TRIGGER);
        }

        public override void AnimationCallBackShoot()
        {
            CreateBullet().Shoot(this, bulletSpeed, transform.right, bulletLifeTime, BulletKernel);
        }


        void BulletKernel(BulletBehaviour bullet)
        {
            var sin = (float)System.Math.Sin(bullet.RemainingLife * 3f + bullet.BulletRandom) - 0.2f;
            bullet.Sp.transform.rotation = Quaternion.Euler(0, 0, 25 * sin);
            bullet.Rb.velocity = new Vector2(bullet.Rb.velocity.x, sin * 0.3f);
        }
    }
}
