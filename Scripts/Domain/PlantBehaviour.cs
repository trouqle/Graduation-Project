using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Domain
{
    public abstract class PlantBehaviour : MonoBehaviour, IPlantable, IDamageable
    {
        protected Tile residingTile;
        protected static LayerMask enemyLayer = 128; // 2^7
        [SerializeField] int health = 3;
        protected int currentHealth = 5;

        public Tile ResidingTile { get => residingTile; set => residingTile = value; }
        public PlantType plantType { get => PlantType.defaultPlant; set { } }


        protected virtual void Awake()
        {
            currentHealth = health;
        }

        public virtual void Plant(Tile tile)
        {
            residingTile = tile;
        }

        public virtual void Remove()
        {
            Destroy(gameObject);
        }

        public virtual void TakeDamage(IDamageSource source, int damage)
        {
            if (currentHealth <= 0) return;


            Debug.Log(gameObject.name + "takes dmg: " + damage);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            residingTile.RemovePlantable();
        }

        public MonoBehaviour GetPlantInstance() => this;
    }

    public interface IDamageable
    {
        public void TakeDamage(IDamageSource source, int damage);
    }
    public interface IDamageSource
    {
    }

    public interface IShooter
    {
        public void Shoot();
    }

}
