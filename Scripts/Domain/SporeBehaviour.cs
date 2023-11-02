using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Domain
{
    public class SporeBehaviour : BulletBehaviour
    {
        [SerializeField] float tickTime;

        ParticleSystem ps;
        CircleCollider2D coll;


        protected override void InitBullet()
        {
            base.InitBullet();
            ps = GetComponent<ParticleSystem>();
            coll = GetComponent<CircleCollider2D>();
            ps.Play();
        }

        protected override void Update()
        {
            base.Update();
            if (initalized && !ps.isPlaying)
            {
                Die();
            }

        }


        protected override void OnTriggerEnter2D(Collider2D collision)
        {
        }
    }

}