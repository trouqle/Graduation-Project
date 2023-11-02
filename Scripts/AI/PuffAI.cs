using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.AI
{
    public class PuffAI : PlantBehaviourBasicShooter,IDamageSource
    {
        SmartTimer shootTimer;
        bool isReadyToShoot;
        protected override void Awake()
        {
            base.Awake();
            shootTimer = new SmartTimer(shootTime);
            shootTimer.OnTimerFinish += () => isReadyToShoot = true;
            shootTimer.StartTimer();
        }

        private void Update()
        {
            var foundEnemy = CheckForEnemy();
            shootTimer.Tick(Time.deltaTime);
            if (isReadyToShoot && foundEnemy)
            {
                Shoot();
            }
        }

        public override void Shoot()
        {
            base.Shoot();
            isReadyToShoot = false;
        }

        public override void AnimationCallBackShoot()
        {
            base.AnimationCallBackShoot();
            shootTimer.StartTimer();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * shootDistance);
        }
    }
}

