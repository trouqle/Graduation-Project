using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.AI
{
    public class MintAI : PlantBehaviourBasicShooter,IDamageSource
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
            isReadyToShoot = false;
            base.Shoot();
        }
        public override void AnimationCallBackShoot()
        {
            base.AnimationCallBackShoot();
            shootTimer.StartTimer();
        }
    }

}
