using Herb.Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.AI
{

    public class BeeNestAI : MonoBehaviour, IPlantable, IDamageable
    {
        [SerializeField] protected float produceTime;
        [SerializeField] GameObject honeyMark;
        SmartTimer produceTimer;
        Tile residingTile;
        List<Tile> neighborTiles;
        List<GameObject> markList = new List<GameObject>();
        int neighborPlantCount;
        [SerializeField] int health;
        int currentHealth;

        public Tile ResidingTile { get => residingTile; set => residingTile = value; }
        public PlantType plantType { get => PlantType.nonPlant; set { } }

        private void Awake()
        {
            currentHealth = health;
            produceTimer = new SmartTimer(produceTime, SmartTimer.Mode.Repeating);
            produceTimer.OnTimerFinish += GenerateMoney;
            produceTimer.StartTimer();
        }

        private void GenerateMoney()
        {
            PlanterManager.Instance.Money += 30 + 5 * neighborPlantCount;
        }

        private void Update()
        {
            produceTimer.Tick(Time.deltaTime);
        }
        public void Plant(Tile tile)
        {
            residingTile = tile;
            neighborTiles = TileMap.Instance.GetNeighborTiles(tile, false);

            neighborTiles.ForEach((x) =>
            {
                x.OnTilePlant += ListenPlanting;
                x.OnTileRemove += ListenRemoving;
                if (x.Plant != null && x.Plant.plantType != PlantType.nonPlant)
                {
                    neighborPlantCount++;
                }
                markList.Add(Instantiate(honeyMark, x.transform.position, Quaternion.identity, transform));
            });
        }

        void ListenPlanting(Tile tile, IPlantable plant)
        {
            if (plant.plantType != PlantType.nonPlant)
                neighborPlantCount++;
        }
        void ListenRemoving(Tile tile, IPlantable plant)
        {
            if (plant.plantType != PlantType.nonPlant)
                neighborPlantCount--;
        }

        public void Remove()
        {
            neighborTiles?.ForEach((x) =>
            {
                x.OnTilePlant -= ListenPlanting;
                x.OnTileRemove -= ListenRemoving;
            });

            for (int i = 0; i < markList.Count; i++)
            {
                Destroy(markList[i]);
            }
            Destroy(gameObject);
        }

        public MonoBehaviour GetPlantInstance() => this;

        public void TakeDamage(IDamageSource source, int damage)
        {
            if (currentHealth <= 0)
            {
                return;
            }

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }


        }

        void Die()
        {
            Destroy(gameObject);
        }
    }

}