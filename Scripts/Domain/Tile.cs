using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herb.Domain
{
    public class Tile : MonoBehaviour
    {
        #region Fields
        IPlantable plant;
        public Vector2Int mapPosition;
        #endregion

        #region Properties
        public bool IsEmpty => plant == null;
        public IPlantable Plant => plant;
        #endregion

        #region Events
        public event System.Action<Tile,IPlantable> OnTilePlant;
        public event System.Action<Tile,IPlantable> OnTileRemove;
        #endregion

        public void PlantPlantable(IPlantable plant)
        {
            this.plant = plant;
            plant.Plant(this);
            OnTilePlant?.Invoke(this,plant);
        }

        public void RemovePlantable()
        {
            OnTileRemove?.Invoke(this,plant);
            plant.Remove();
            this.plant = null;
        }
    }

}