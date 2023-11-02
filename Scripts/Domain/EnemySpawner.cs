using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Domain
{
    [System.Serializable]
    public class EnemyPrefabData
    {
        public GameObject enemyPrefab;
        public float spawnInterval;
    }

    public class EnemySpawner : MonoBehaviour
    {
        public List<EnemyPrefabData> enemyPrefabs;

        
        public float minSpawnInterval = 5f;

        private float timer = 0f; 
        private Dictionary<int, float> rowTimers = new Dictionary<int, float>(); 

        private void Update()
        {
            timer += Time.deltaTime;

            if (CanSpawnInRow() && timer >= GetSpawnInterval())
            {
                SpawnEnemy();
                timer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            int randomRow = GetRandomRow();
            float rowTimer = Time.time + minSpawnInterval;

            rowTimers[randomRow] = rowTimer; 

            EnemyPrefabData selectedEnemyData = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            GameObject enemyPrefab = selectedEnemyData.enemyPrefab;

            Vector3 position = TileMap.Instance.Tiles[randomRow, TileMap.Instance.TilemapSize.x - 1].transform.position;

            Instantiate(enemyPrefab, position, Quaternion.identity);
            AddRowTimer(randomRow, rowTimer);
        }

        private int GetRandomRow()
        {
            
            int randomRow = Random.Range(0, TileMap.Instance.TilemapSize.y - 1);
            int maxAttempts = TileMap.Instance.TilemapSize.y;

            while (rowTimers.ContainsKey(randomRow) && rowTimers[randomRow] > Time.time)
            {
                randomRow = Random.Range(0, TileMap.Instance.TilemapSize.y - 1);
                if (--maxAttempts <= 0)
                {
                    break;
                }
            }
            
            return randomRow;
        }

        private bool CanSpawnInRow()
        {
            int randomRow = GetRandomRow();

            
            return !rowTimers.ContainsKey(randomRow) || rowTimers[randomRow] <= Time.time;
        }

        private float GetSpawnInterval()
        {
            EnemyPrefabData selectedEnemyData = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            return selectedEnemyData.spawnInterval;
        }
        private void AddRowTimer(int row, float timer)
        {
            if (!rowTimers.ContainsKey(row))
            {
                rowTimers.Add(row, timer);
            }
            else
            {
                rowTimers[row] = timer;
            }
        }
        
            
        
    }

}