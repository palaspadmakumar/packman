using com.hellion.tilesystem.utilities;

namespace com.hellion.packman
{
    public interface ICharecter
    {
        TileObject GetNextTile(TileObject currentTile, Charecter charecter);
    }
}
