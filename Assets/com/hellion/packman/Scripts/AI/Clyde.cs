using com.hellion.tilesystem;
using com.hellion.tilesystem.utilities;
using UnityEngine;

namespace com.hellion.packaman
{
    public class Clyde : ICharecter
    {
        public TileObject GetNextTile(TileObject currentTile, Charecter charecter)
        {
            TileObject left_tile = TileGenerator.GetLeftTile(currentTile);
            TileObject right_tile = TileGenerator.GetRightTile(currentTile);
            TileObject up_tile = TileGenerator.GetUpTile(currentTile);
            TileObject down_tile = TileGenerator.GetDownTile(currentTile);
            int y = Random.Range(0, 4);
            if (y == 0)
            {
                if (left_tile != null)
                {
                    return left_tile;
                }
            }
            else if (y == 1)
            {
                if (right_tile != null)
                {
                    return right_tile;
                }
            }
            else if (y == 2)
            {
                if (up_tile != null)
                {
                    return up_tile;
                }
            }
            else if (y == 3)
            {
                if (down_tile != null)
                {
                    return down_tile;
                }
            }
            else
            {
                if (left_tile != null)
                {
                    return left_tile;
                }
                else if (right_tile != null)
                {
                    return right_tile;
                }
                else if (up_tile != null)
                {
                    return up_tile;
                }
                else if (down_tile != null)
                {
                    return down_tile;
                }
            }
            return null;

        }
    }
}
