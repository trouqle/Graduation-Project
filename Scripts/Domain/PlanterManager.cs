using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Herb.Domain
{
    public class PlanterManager : Singleton<PlanterManager>
    {
        [SerializeField] LayerMask tileLayer;
        [SerializeField] GameObject selectedPlant;
        int money;
        Camera cam;
        public Tile howeringTile;
        Collider2D prevColl;

        bool TileFound => howeringTile != null;
        public int Money { get { return money; } set { money = value; OnMoneyChange?.Invoke(money); } }
        public GameObject SelectedPlant { get { return selectedPlant; } set { selectedPlant = value; } }

        public event System.Action<int> OnMoneyChange;

        protected override void Awake()
        {
            base.Awake();
            cam = Camera.main;
        }


        void Update()
        {
            FindHoweringTile();

            if (Input.GetMouseButtonDown(0) && TileFound)
            {
                if (howeringTile.IsEmpty)
                {
                    Debug.Log("Plant");
                    PlantToTile(howeringTile);
                }
                else
                {
                    Debug.Log("Tile is occupied");
                }
            }else if(Input.GetMouseButtonDown(1) && TileFound)
            {
                if (!howeringTile.IsEmpty)
                {
                    howeringTile.RemovePlantable();
                }
            }

        }

        private void PlantToTile(Tile tile)
        {
            var obj 
                = Instantiate(selectedPlant, tile.transform.position + Vector3.down * 0.5f, Quaternion.identity);
            var plant = obj.GetComponent<IPlantable>();
            tile.PlantPlantable(plant);

            var renderers = obj.GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in renderers)
            {
                renderer.sortingOrder -= tile.mapPosition.x * 30;
            }
            Money -= 10;
        }

        void FindHoweringTile()
        {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, 99, tileLayer);
            if (hit.collider != null)
            {
                if (prevColl != hit.collider)
                {
                    howeringTile = hit.collider.GetComponent<Tile>();
                }
            }
            else
            {
                howeringTile = null;
            }
            prevColl = hit.collider;
        }

        /// <summary>
        /// REMOVE THIS!
        /// </summary>
        public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public interface IPlantable
    {
        public void Plant(Tile tile);
        public void Remove();
        public Tile ResidingTile { get; set; }
        public MonoBehaviour GetPlantInstance();
        public PlantType plantType { get; set; }
    }

    public enum PlantType { defaultPlant,nonPlant}
}
