using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Herb.Domain
{

    public class TileMap : Singleton<TileMap>
    {
        #region Unity Fields
        [SerializeField] GameObject tilePrefab;
        [SerializeField] Vector2Int tileMapSize;
        #endregion

        #region Fields
        Vector2 tileSize;
        Tile[,] tiles;
        #endregion

        #region Properties
        public Vector2Int TilemapSize => tileMapSize;
        public Tile[,] Tiles => tiles;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            tiles = new Tile[tileMapSize.y, tileMapSize.x];
            for (int i = 0; i < transform.childCount; i++)
            {
                var t = transform.GetChild(i).GetComponent<Tile>();
                tiles[t.mapPosition.x, t.mapPosition.y] = t;
            }
        }

#if UNITY_EDITOR
        public void GenerateTiles()
        {
            tileSize = tilePrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
            tiles = new Tile[tileMapSize.y,tileMapSize.x];
            for (int i = 0; i < tileMapSize.y; i++)
            {
                //Populate lanes with tiles
                for (int j = 0; j < tileMapSize.x; j++)
                {
                    var tileGameObject = (GameObject)PrefabUtility.InstantiatePrefab(tilePrefab, transform);
                    var tileScale = tilePrefab.transform.localScale;
                    tileGameObject.transform.position = new Vector3(j * tileSize.x * tileScale.x, i * tileSize.y * tileScale.y, 0f);
                    tileGameObject.name = "Tile " + i + "-" + j;
                    if ((j + i) % 2 == 0) tileGameObject.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.7f, 0.0f, 1);
                    tileGameObject.layer = 6;
                    tiles[i, j] = tileGameObject.GetComponent<Tile>();
                    tiles[i, j].mapPosition = new Vector2Int(i, j);
                }
            }
        }
#endif
        public void DestroyChilds()
        {
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        public List<Tile> GetHorizontalLane(Tile tile)
        {
            List<Tile> list = new List<Tile>();
            for (int i = 0; i < tileMapSize.x; i++)
            {
                list.Add(tiles[i, tile.mapPosition.y]);
            }
            return list;
        }
        public List<Tile> GetVerticallLane(Tile tile)
        {
            List<Tile> list = new List<Tile>();
            for (int i = 0; i < tileMapSize.y; i++)
            {
                list.Add(tiles[tile.mapPosition.x, i]);
            }
            return list;
        }

        public List<Tile> GetNeighborTiles(Tile tile,bool includeEdges = true)
        {
            List<Tile> list = new List<Tile>();
            int xPos = tile.mapPosition.x;
            int yPos = tile.mapPosition.y;
            if (includeEdges && yPos > 0 && xPos > 0) list.Add(tiles[xPos - 1, yPos - 1]);
            if (yPos > 0) list.Add(tiles[xPos, yPos - 1]);
            if (includeEdges && yPos > 0 && xPos < TilemapSize.y - 1) list.Add(tiles[xPos + 1, yPos - 1]);
            if (xPos > 0) list.Add(tiles[xPos-1, yPos]);
            if (xPos < TilemapSize.y - 1) list.Add(tiles[xPos +1, yPos]);
            if (includeEdges && xPos > 0 && yPos < TilemapSize.x - 1) list.Add(tiles[xPos - 1, yPos + 1]);
            if (yPos < TilemapSize.x - 1) list.Add(tiles[xPos, yPos + 1]);
            if (includeEdges && xPos < TilemapSize.y - 1 && yPos < TilemapSize.x - 1) list.Add(tiles[xPos + 1, yPos + 1]);

            return list;
        }


    }

}