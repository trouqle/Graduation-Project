using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Domain
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] LayerMask enemyLayerMask;
        [SerializeField] GameObject deathParticleEffect;
        [SerializeField] GameObject trailParticleEffect;
        
        protected SpriteRenderer sp;
        protected Rigidbody2D rb;

        protected float speed;
        protected float lifeSpan;
        [SerializeField] protected int damage;
        protected int pierceCount = 1;
        protected IDamageSource damageSource;


        protected Vector3 direction;

        public bool initalized;

        public delegate void BulletKernel(BulletBehaviour b);
        public BulletKernel kernel;

        public Rigidbody2D Rb => rb;
        public SpriteRenderer Sp => sp;
        public float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
                rb.velocity = direction * speed;
            }
        }
        public float RemainingLife { get { return lifeSpan; } set { lifeSpan = value; } }
        public Vector3 Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
                rb.velocity = direction * speed;
            }
        }
        public int PierceCount { get { return pierceCount; } set { pierceCount = value; } }

        public float BulletRandom { get; private set; }

        protected virtual void InitBullet()
        {
            rb = GetComponent<Rigidbody2D>();
            sp = GetComponentInChildren<SpriteRenderer>();
            BulletRandom = Random.Range(-1f, 1f);
            initalized = true;
        }

        protected virtual void Update()
        {
            if (initalized && kernel != null) kernel?.Invoke(this);
            if (initalized) lifeSpan -= Time.deltaTime;
        }

        public virtual void Shoot(IDamageSource damageSource, float speed, Vector3 direction, float lifeSpan, BulletKernel kernel = null)
        {
            if (!initalized) InitBullet();

            this.damageSource = damageSource;
            this.speed = speed + BulletRandom * 0.1f;
            this.direction = direction;
            this.kernel = kernel;
            this.lifeSpan = lifeSpan;

            rb.velocity = direction.normalized * speed;
        }

        public virtual void Die()
        {
            if (trailParticleEffect != null) trailParticleEffect.transform.parent = null;
            if(deathParticleEffect != null)Instantiate(deathParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (enemyLayerMask.Contains(collision.gameObject.layer))
            {
                if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(damageSource, damage);
                }
                if (pierceCount > 0)
                {
                    pierceCount--;
                    if (pierceCount <= 0)
                    {
                        Die();
                    }
                }
            }
        }

    }

}