using com.hellion.packaman;
using UnityEngine;
using UnityEngine.UI;

namespace com.hellion.tilesystem.utilities
{
    public class TileObject : MonoBehaviour
    {
        [SerializeField] private ETileType _tile_type;
        [SerializeField] private EPelletType _pellet_type;
        [SerializeField] private Image _sprite;
        [SerializeField] private Image _pellet;
        [SerializeField] private RawImage select;
        private Vector2Int index = new Vector2Int(0, 0);

        public void SetSprite(Sprite sprite)
        {
            _sprite.sprite = sprite;
        }

        public void SetSelected(bool selected)
        {
            select.gameObject.SetActive(selected);
        }

        public void SetSize(float size)
        {
            _sprite.rectTransform.sizeDelta = _pellet.rectTransform.sizeDelta = new Vector2(size, size);
            select.rectTransform.sizeDelta = new Vector2(size * 0.85f, size * 0.85f);
        }

        public void SetIndex(int _width, int _height)
        {
            index.x = _width;
            index.y = _height;
        }
        public Vector2Int GetIndex()
        {
            return index;
        }

        public bool IsTileTypeWall()
        {
            bool isWall = true;
            switch (_tile_type)
            {
                case ETileType.Wall_Blank:
                    isWall = false;
                    break;
                default:
                    isWall = true;
                    break;
            }
            return isWall;
        }

        public void UpdateTileObject(ETileType tile_type, Sprite sprite)
        {
            _tile_type = tile_type;
            _sprite.sprite = sprite;
        }
        public void UpdatePellet(EPelletType pellet_type, Sprite sprite)
        {
            if (pellet_type != EPelletType.None)
            {
                if (pellet_type == EPelletType.Pellet)
                {
                    if (Application.isPlaying)
                    {
                        GameManager.Instance.totalPelletCount += 1;
                    }
                }
                SetActivePellet(true);
            }
            _pellet_type = pellet_type;
            _pellet.sprite = sprite;
        }

        private void SetActivePellet(bool active)
        {
            _pellet.gameObject.SetActive(active);
            if (!active)
            {
                _pellet_type = EPelletType.None;
            }
        }

        public void SetPellet(EPelletType pellet_type)
        {
            if (pellet_type != EPelletType.None)
            {
                if (pellet_type == EPelletType.Pellet)
                {
                    GameManager.Instance.totalPelletCount += 1;
                }
                SetActivePellet(true);
            }
            else
            {
                SetActivePellet(false);
            }
            _pellet_type = pellet_type;
        }

        public EPelletType GetPelletType()
        {
            return _pellet_type;
        }
    }
}
