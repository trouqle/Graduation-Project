using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.AI
{

    public class NettleAI : PlantBehaviour, IDamageSource
    {
        [SerializeField] Vector2 checkScale = new Vector2(0.9f, 0.3f);
        [SerializeField] Vector3 offset;
        [SerializeField] int damage = 1;
        Animator anim;
        protected override void Awake()
        {
            base.Awake();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            anim.SetBool("Found", CheckForEnemy());
        }

        bool CheckForEnemy()
        {
            Collider2D hit = Physics2D.OverlapBox(transform.position + offset, checkScale, 0f, enemyLayer);
            return hit != null;

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position + offset, checkScale);
        }

        public void Attack_AnimCallBack()
        {
            var found = Physics2D.OverlapBoxAll(transform.position + offset, checkScale, 0f, enemyLayer);
            foreach (var enemy in found)
            {
                if (enemy.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(this,damage);
                }
            }
        }

    }

}