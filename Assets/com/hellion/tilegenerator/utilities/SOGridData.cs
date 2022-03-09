using System.Collections.Generic;
using com.hellion.packaman;
using UnityEngine;

namespace com.hellion.tilesystem.utilities
{
    [System.Serializable, CreateAssetMenu(fileName = "Tile Generator/Tile Grid Data")]
    public class SOGridData : ScriptableObject
    {
        [SerializeField] public int _width = 28;
        [SerializeField] public int _height = 31;
        [SerializeField] public List<SOGridDataSerializableList> tile_data = new List<SOGridDataSerializableList>();
    }

    [System.Serializable]
    public class SOGridDataSerializableList
    {
        [SerializeField] public List<SOGridDataSerializable> data = new List<SOGridDataSerializable>();
    }

    [System.Serializable]
    public class SOGridDataSerializable
    {
        public ETileType _tile_type;
        public EPelletType _pellet_type;
        public Sprite _sprite;
        public Sprite _pellet;

        public SOGridDataSerializable(ETileType _tile_type, Sprite _sprite)
        {
            this._tile_type = _tile_type;
            this._sprite = _sprite;
        }

        public void SetTileType(ETileType tile_type, Sprite sprite)
        {
            _sprite = sprite;
            _tile_type = tile_type;
        }

        public void SetPellet(EPelletType pelletType, Sprite pellet)
        {
            _pellet_type = pelletType;
            _pellet = pellet;
        }

    }
}
