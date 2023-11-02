using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Herb.AI
{
    public class EnemyBase : MonoBehaviour, IDamageSource, IDamageable
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] protected int enemyHP;
        [SerializeField] protected int enemyAD;
        [SerializeField] LayerMask plantLayer;

        IDamageable foundPlant;

        protected Rigidbody2D rb;
        protected BoxCollider2D enemyCollider;

        protected int currentHP;

        private bool isFoundPlant;

        SmartTimer attackTimer;

        protected Vector2 direction = Vector2.left;

        protected Animator anim;

        void Start()
        {
            currentHP = enemyHP;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            StartMoving();

            enemyCollider = GetComponent<BoxCollider2D>();
            attackTimer = new SmartTimer(1.2f, tickCondition: () => isFoundPlant, stopMode: SmartTimer.StopMode.Reset, mode: SmartTimer.Mode.Repeating);
            attackTimer.StartTimer();
            attackTimer.OnTimerFinish += Attack;
        }






        protected virtual void Update()
        {
            attackTimer.Tick(Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + enemyCollider.offset, direction, 0.25f, plantLayer);
            if (!isFoundPlant && hit.collider != null)
            {
                foundPlant = hit.collider.GetComponent<IDamageable>();
            }
            isFoundPlant = hit.collider != null;
            if (isFoundPlant)
            {
                rb.velocity = Vector2.zero;
                
            }
            else
            {
                StartMoving();
            }
            anim.SetBool("Attack", isFoundPlant);
        }

        private void StartMoving()
        {
            rb.velocity = direction * movementSpeed;
        }

        
        private void Attack()
        {
            foundPlant.TakeDamage(this, enemyAD);
        }

        public void TakeDamage(IDamageSource source, int damage)
        {
            if(currentHP <= 0)
            {
                return;
            }
            currentHP -= damage;
            if(currentHP<= 0)
            {
                Die();
            }
            Debug.Log(currentHP);
        }
        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }

}

