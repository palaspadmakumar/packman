using com.hellion.tilesystem;
using com.hellion.tilesystem.utilities;
using UnityEngine;

namespace com.hellion.packaman
{
    public class Pinky : ICharecter
    {
        public TileObject GetNextTile(TileObject currentTile, Charecter charecter)
        {

            TileObject targetTile = null;

            Charecter player = GameManager.Instance.GetPlayer();
            if (player.GetELookDirection() == Charecter.ELookDirection.RIGHT)
            {
                targetTile = TileGenerator.GetRightTile(player.GetCurrentTie());
            }
            else if (player.GetELookDirection() == Charecter.ELookDirection.LEFT)
            {
                targetTile = TileGenerator.GetLeftTile(player.GetCurrentTie());
            }
            else if (player.GetELookDirection() == Charecter.ELookDirection.UP)
            {
                targetTile = TileGenerator.GetUpTile(player.GetCurrentTie());
            }
            else if (player.GetELookDirection() == Charecter.ELookDirection.DOWN)
            {
                targetTile = TileGenerator.GetDownTile(player.GetCurrentTie());
            }

            if (targetTile == null)
            {
                targetTile = player.GetCurrentTie();
            }

            TileObject left_tile = TileGenerator.GetLeftTile(currentTile);
            TileObject right_tile = TileGenerator.GetRightTile(currentTile);
            TileObject up_tile = TileGenerator.GetUpTile(currentTile);
            TileObject down_tile = TileGenerator.GetDownTile(currentTile);
            TileObject closerTile = null;
            float distance = float.MaxValue;
            if (left_tile != null)
            {
                float left_distance = Vector3.Distance(left_tile.transform.position, targetTile.transform.position);
                if (left_distance < distance)
                {
                    distance = left_distance;
                    closerTile = left_tile;
                }
            }
            if (right_tile != null)
            {
                float right_distance = Vector3.Distance(right_tile.transform.position, targetTile.transform.position);
                if (right_distance < distance)
                {
                    distance = right_distance;
                    closerTile = right_tile;
                }
            }
            if (up_tile != null)
            {
                float up_distance = Vector3.Distance(up_tile.transform.position, targetTile.transform.position);
                if (up_distance < distance)
                {
                    distance = up_distance;
                    closerTile = up_tile;
                }
            }
            if (down_tile != null)
            {
                float down_distance = Vector3.Distance(down_tile.transform.position, targetTile.transform.position);
                if (down_distance < distance)
                {
                    distance = down_distance;
                    closerTile = down_tile;
                }
            }

            return closerTile;
        }
    }
}
