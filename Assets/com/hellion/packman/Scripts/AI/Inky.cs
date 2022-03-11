using com.hellion.tilesystem;
using com.hellion.tilesystem.utilities;
using UnityEngine;

namespace com.hellion.packman
{
    public class Inky : ICharecter
    {
        public TileObject GetNextTile(TileObject currentTile, Charecter charecter)
        {
            TileObject left_tile = TileGenerator.GetLeftTile(currentTile);
            TileObject right_tile = TileGenerator.GetRightTile(currentTile);
            TileObject up_tile = TileGenerator.GetUpTile(currentTile);
            TileObject down_tile = TileGenerator.GetDownTile(currentTile);
            Charecter player = GameManager.Instance.GetPlayer(); ;
            TileObject closerTile = null;
            float distance = float.MaxValue;
            int x = Random.Range(0, 4);
            if (x <= 1)
            {
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
            }
            else
            {
                if (left_tile != null)
                {
                    float left_distance = Vector3.Distance(left_tile.transform.position, player.transform.position);
                    if (left_distance < distance)
                    {
                        distance = left_distance;
                        closerTile = left_tile;
                    }
                }
                if (right_tile != null)
                {
                    float right_distance = Vector3.Distance(right_tile.transform.position, player.transform.position);
                    if (right_distance < distance)
                    {
                        distance = right_distance;
                        closerTile = right_tile;
                    }
                }
                if (up_tile != null)
                {
                    float up_distance = Vector3.Distance(up_tile.transform.position, player.transform.position);
                    if (up_distance < distance)
                    {
                        distance = up_distance;
                        closerTile = up_tile;
                    }
                }
                if (down_tile != null)
                {
                    float down_distance = Vector3.Distance(down_tile.transform.position, player.transform.position);
                    if (down_distance < distance)
                    {
                        distance = down_distance;
                        closerTile = down_tile;
                    }
                }
            }

            return closerTile;
        }
    }
}
