using com.hellion.packman;
using com.hellion.tilesystem.utilities;
using UnityEngine;

namespace com.hellion.tilesystem
{
    [DefaultExecutionOrder(-1)]
    public class TileGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private SOGridData source;
        [SerializeField] private GameObject gridParent;
        private TileObject[,] grid;
        private float tileSize = 0;
        private static TileGenerator tileGenerator;

        private void Start()
        {
            tileGenerator = this;
            GenerateGrid();
        }

        public void GenerateGrid(SOGridData source)
        {
            this.source = source;
            GenerateGrid();
        }

        public static TileObject GetTile(int y, int x)
        {
            return tileGenerator.grid[y, x];
        }

        public static TileObject GetRightTile(TileObject currentTile)
        {
            int x = currentTile.GetIndex().x;
            int y = currentTile.GetIndex().y;
            if (x + 1 < tileGenerator.source._width)
            {
                if (tileGenerator.grid[y, x + 1].IsTileTypeWall())
                {
                    return null;
                }
                else
                {
                    return tileGenerator.grid[y, x + 1];
                }
            }
            else
            {
                return tileGenerator.grid[y, 0];
            }
        }

        public static TileObject GetLeftTile(TileObject currentTile)
        {
            int x = currentTile.GetIndex().x;
            int y = currentTile.GetIndex().y;
            if (x - 1 >= 0)
            {
                if (tileGenerator.grid[y, x - 1].IsTileTypeWall())
                {
                    return null;
                }
                else
                {
                    return tileGenerator.grid[y, x - 1];
                }
            }
            else
            {
                return tileGenerator.grid[y, tileGenerator.source._width - 1];
            }
        }

        public static TileObject GetUpTile(TileObject currentTile)
        {
            int x = currentTile.GetIndex().x;
            int y = currentTile.GetIndex().y;
            if (y + 1 < tileGenerator.source._height)
            {
                if (tileGenerator.grid[y + 1, x].IsTileTypeWall())
                {
                    return null;
                }
                else
                {
                    return tileGenerator.grid[y + 1, x];
                }
            }
            else
            {
                return tileGenerator.grid[0, x];
            }
        }

        public static TileObject GetDownTile(TileObject currentTile)
        {
            int x = currentTile.GetIndex().x;
            int y = currentTile.GetIndex().y;
            if (y - 1 >= 0)
            {
                if (tileGenerator.grid[y - 1, x].IsTileTypeWall())
                {
                    return null;
                }
                else
                {
                    return tileGenerator.grid[y - 1, x];
                }
            }
            else
            {
                return tileGenerator.grid[tileGenerator.source._height - 1, x];
            }
        }

        public void GenerateGrid()
        {
            if (source == null)
            {
                return;
            }

            grid = new TileObject[source._height, source._width];
            tileSize = Camera.main.pixelWidth / source._width;
            float startPos = -Camera.main.pixelWidth / 2 + tileSize / 2;
            for (int row = 0; row < source._height; row++)
            {
                for (int coloumn = 0; coloumn < source._width; coloumn++)
                {
                    RectTransform tile = Instantiate(tilePrefab, gridParent.transform).GetComponent<RectTransform>();
                    tile.localPosition = new Vector3(startPos + coloumn * tileSize, startPos + row * tileSize, 0);
                    TileObject tileObject = tile.GetComponent<TileObject>();
                    tileObject.SetSize(tileSize);
                    tileObject.SetIndex(coloumn, row);
                    tileObject.UpdateTileObject(source.tile_data[row].data[coloumn]._tile_type, source.tile_data[row].data[coloumn]._sprite);
                    tileObject.UpdatePellet(source.tile_data[row].data[coloumn]._pellet_type, source.tile_data[row].data[coloumn]._pellet);
                    grid[row, coloumn] = tileObject;
                }
            }
        }

        public static void ResetGrid()
        {
            for (int row = 0; row < tileGenerator.source._height; row++)
            {
                for (int coloumn = 0; coloumn < tileGenerator.source._width; coloumn++)
                {
                    if (tileGenerator.source.tile_data[row].data[coloumn]._pellet_type != EPelletType.None)
                    {
                        tileGenerator.grid[row, coloumn].SetPellet(tileGenerator.source.tile_data[row].data[coloumn]._pellet_type);
                    }
                }
            }
        }
    }
}
